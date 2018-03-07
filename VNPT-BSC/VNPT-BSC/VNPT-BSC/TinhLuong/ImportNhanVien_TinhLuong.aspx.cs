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

namespace VNPT_BSC.TinhLuong
{
    public partial class PhatTrienThueBao : System.Web.UI.Page
    {
        protected void btnGetDate_Click(object sender, EventArgs e)
        {
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            DropDownListMonth.SelectedValue = month;
            DropDownListYear.SelectedValue = year;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(DropDownListMonth.SelectedValue.ToString());
            int nam = Convert.ToInt32(DropDownListYear.SelectedValue.ToString());

            if (excelUpload.HasFile)
            {
                string FileName = Path.GetFileName(excelUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(excelUpload.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPathNhanVien"];

                string FilePath = Server.MapPath(FolderPath + "ImportNhanVien-" + DateTime.Now.ToString("yyyyMMddHHmmss") + Extension);
                excelUpload.SaveAs(FilePath);
                insertDatabase(FilePath, Extension, "Yes", thang, nam);
            }
        }

        private void insertDatabase(string FilePath, string Extension, string isHDR, int thang, int nam) {
            string conStr = "";
            bool bInsert = true;
            DateTime timekey = new DateTime(nam, 1, thang);
            Connection cn = new Connection();

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
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT id, ten_nhanvien, donvi, chucdanh, id_nhom_donvi, heso_bacluong, ngaycong_thucte, ngaycong_bhxh, phatsinhthem, heso_luongcb, luong_cb From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dtResult);
            connExcel.Close();

            string sqlDel = "delete qlns_lichsu_nhanvien_thugon where format(thoigian_apdung,'yyyy-MM-dd') = '" + timekey.ToString("yyyy-dd-MM") + "'";
            cn.ThucThiDL(sqlDel);
            for (int i = 0; i < dtResult.Rows.Count; i++) {
                string id_nhanvien = dtResult.Rows[i]["id"].ToString();
                string ten_nhanvien = dtResult.Rows[i]["ten_nhanvien"].ToString();
                string donvi = dtResult.Rows[i]["donvi"].ToString();
                string chucdanh = dtResult.Rows[i]["chucdanh"].ToString();
                string id_nhom_donvi = dtResult.Rows[i]["id_nhom_donvi"].ToString();
                string heso_bacluong = dtResult.Rows[i]["heso_bacluong"].ToString();
                string ngaycong_thucte = dtResult.Rows[i]["ngaycong_thucte"].ToString();
                string ngaycong_bhxh = dtResult.Rows[i]["ngaycong_bhxh"].ToString();
                string phatsinhthem = dtResult.Rows[i]["phatsinhthem"].ToString();
                string heso_luongcb = dtResult.Rows[i]["heso_luongcb"].ToString();
                string luong_cb = dtResult.Rows[i]["luong_cb"].ToString();
                try
                {
                    string sql = "insert into qlns_lichsu_nhanvien_thugon values('" + id_nhanvien + "', N'" + ten_nhanvien + "', '" + donvi + "', '" + chucdanh + "', '" + id_nhom_donvi + "', '" + heso_bacluong + "', '" + timekey + "', '" + ngaycong_thucte + "', '" + ngaycong_bhxh + "', '" + phatsinhthem + "', '" + heso_luongcb + "', '" + luong_cb + "')";
                    cn.ThucThiDL(sql);
                }
                catch (Exception ex) {
                    bInsert = false;
                    throw ex;
                }
            }

            if (bInsert) {
                Response.Write("<script>alert('Import dữ liệu nhân viên " + thang + "-" + nam + " thành công!!!')</script>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Import nhân viên để tính lương";
            if (!IsPostBack)
            {
                Nhanvien nhanvien = new Nhanvien();

                //nhanvien = Session.GetCurrentUser();
                nhanvien = (Nhanvien)Session["nhanvien"];

                if (nhanvien.nhanvien_id != 196)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../index.aspx';</script>");
                }
            }
        }
    }
}