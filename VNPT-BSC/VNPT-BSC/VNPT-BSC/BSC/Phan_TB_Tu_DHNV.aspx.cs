using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Data.OleDb;

namespace VNPT_BSC.BSC
{
    public partial class Phan_TB_Tu_DHNV : System.Web.UI.Page
    {
        Connection cn = new Connection();

        private bool checkDonVi(string id_donvi)
        {
            DataTable dtResult = new DataTable();
            string sql = "select * from qlns_donvi where map_id_donvi = '" + id_donvi + "'";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtResult.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        private bool updateDonViNhan(string donvinhan, string timekey, string so_dt)
        {
            bool bResult = false;
            string sql = "update thuebao_tratruoc set donvi_id = '" + donvinhan + "' ";
            sql += "where donvi_id_pt = '12' and so_dt = '" + so_dt + "' and left(timekey,6) = '" + timekey + "'";
            try
            {
                cn.ThucThiDL(sql);
                bResult = true;
            }
            catch
            {
                bResult = false;
            }
            return bResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Phân thuê bao cho PBH bằng excel";
            Nhanvien nhanvien = new Nhanvien();


            //nhanvien = Session.GetCurrentUser();
            nhanvien = (Nhanvien)Session["nhanvien"];

            if (nhanvien == null || nhanvien.nhanvien_id != 164)
            {
                Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(DropDownListMonth.SelectedValue.ToString());
            int nam = Convert.ToInt32(DropDownListYear.SelectedValue.ToString());

            if (excelUpload.HasFile)
            {
                string FileName = Path.GetFileName(excelUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(excelUpload.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);
                excelUpload.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension, "Yes", thang, nam);
            }
        }

        private void Import_To_Grid(string FilePath, string Extension, string isHDR, int thang, int nam)
        {
            bool bImport = false;
            string timekey = nam.ToString() + thang.ToString("00");
            List<string> arrMaDonVi = new List<string>();
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dtResult = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[dtExcelSchema.Rows.Count - 1]["TABLE_NAME"].ToString(); // Trừ 1 phòng trường hợp file excel có filter, tạo ra 1 sheet tạm
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dtResult);
            connExcel.Close();

            //Bind Data to gridview
            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dtResult;
            GridView1.DataBind();

            // Vòng lặp phân thuê bao tới nhân nhân viên
            for (int nRowIndex = 0; nRowIndex < dtResult.Rows.Count; nRowIndex++)
            {
                bool bDonVi = false;
                string id_donvi = dtResult.Rows[nRowIndex][1].ToString().Trim();
                string szSDT = dtResult.Rows[nRowIndex][2].ToString().Trim();

                id_donvi = id_donvi.Replace("\u200C", "");
                szSDT = szSDT.Replace("\u200C", "");

                bDonVi = checkDonVi(id_donvi);
                if (bDonVi == false)
                {
                    break;
                }

                bImport = updateDonViNhan(id_donvi, timekey, szSDT);
                if (!arrMaDonVi.Contains(id_donvi))
                {
                    arrMaDonVi.Add(id_donvi);
                }
            }


            if (bImport)
            {
                // Send sms thông báo tới các đơn vị
                Message msg = new Message();
                for (int i = 0; i < arrMaDonVi.Count; i++)
                {
                    DataTable dtSDT = dtSDTByMaDV(arrMaDonVi[i]);
                    for (int j = 0; j < dtSDT.Rows.Count; j++)
                    {
                        string szSDT = dtSDT.Rows[j]["nhanvien_didong"].ToString().Trim();
                        string szContent = "PBH vua nhan duoc danh sach thue bao " + thang + "-" + nam + ". Vui long vao kiem tra!!!";
                        msg.SendSMS_VTTP(szSDT, szContent);
                    }
                }

                Response.Write("<script>alert('Phân phối thuê bao thành công!!!')</script>");
            }
            else
            {
                Response.Write("<script>alert('Phân phối thuê bao không thành công!!! Cấu trúc hoặc dữ liệu trong file không đúng định dạng')</script>");
            }
        }

        private DataTable dtSDTByMaDV(string id_donvi)
        {
            DataTable dtResult = new DataTable();
            string sql = "select nv.nhanvien_didong from nhanvien nv, qlns_donvi dv where nv.nhanvien_donvi = dv.id and dv.map_id_donvi = '" + id_donvi + "' and nv.nhanvien_chucdanh in (1,3)";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtResult;
        }

        protected void btnGetDate_Click(object sender, EventArgs e)
        {
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            DropDownListMonth.SelectedValue = month;
            DropDownListYear.SelectedValue = year;
        }
    }
}