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
    public partial class ImportThamDinhBSC : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public int gDonvithamdinh;
        public int gNguoithamdinh;

        /*List loại mẫu bsc*/
        private DataTable dsMauBSC()
        {
            DataTable dsMauBSC = new DataTable();
            string sqlMauBSC = "select * from loaimaubsc";
            try
            {
                dsMauBSC = cn.XemDL(sqlMauBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsMauBSC;
        }

        private int countKPI(int thang, int nam, int loaiMauBSC)
        {
            int nTong = 0;
            DataTable dtResult = new DataTable();
            string sql = "select * from danhsachbsc where thang = '" + thang + "' and nam = '" + nam + "' and nguoitao in (select nhanvien.nhanvien_id from nhanvien, chucvu, nhanvien_chucvu, quyen_cv where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id and chucvu.chucvu_id = nhanvien_chucvu.chucvu_id and chucvu.chucvu_id = quyen_cv.chucvu_id and quyen_cv.quyen_id = 2) and bscduocgiao = '' and maubsc = '" + loaiMauBSC + "'";
            try
            {
                dtResult = cn.XemDL(sql);
                nTong = dtResult.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return nTong;
        }

        private DataTable getListDV()
        {
            DataTable dtResult = new DataTable();
            string sql = "select * from donvi";
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

        private DataTable getChuyenVienBSC()
        {
            DataTable dtResult = new DataTable();
            string sql = "select nhanvien.* from nhanvien, nhanvien_chucvu where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id and nhanvien_chucvu.chucvu_id = 10";
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

        private int getDvIdByMaDV(string ma_dv)
        {
            int donvi_id = 0;
            DataTable dtResult = new DataTable();
            string sql = "select donvi_id from donvi where donvi_ma = '" + ma_dv + "'";
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
                donvi_id = Convert.ToInt32(dtResult.Rows[0][0].ToString());
            }

            return donvi_id;
        }

        private bool insertBSC_DonVi(int donvigiao, int donvinhan, int thang, int nam, int kpi_id, double thamdinh, int loaiMauBSC)
        {
            DataTable dtKPIDetai = new DataTable();
            bool bResult = false;
            try
            {
                    
                string sql = "update bsc_donvi set thamdinh = '" + thamdinh + "', thoigian_thamdinh = GETDATE() ";
                sql += "where thang = '" + thang + "' ";
                sql += "and nam = '" + nam + "' ";
                sql += "and donvinhan = '" + donvinhan + "' ";
                sql += "and donvithamdinh = '" + gDonvithamdinh + "' ";
                sql += "and kpi = '" + kpi_id + "'";
                    
                cn.ThucThiDL(sql);
                bResult = true;
            }
            catch (Exception ex)
            {
                bResult = false;
            }
            return bResult;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Import Thẩm định BSC đơn vị";
            if (!IsPostBack)
            {
                DataTable dtMauBSC = new DataTable();
                dtMauBSC = dsMauBSC();

                DropDownListLoaiMauBSC.DataSource = dtMauBSC;
                DropDownListLoaiMauBSC.DataTextField = "loai_ten";
                DropDownListLoaiMauBSC.DataValueField = "loai_id";
                DropDownListLoaiMauBSC.DataBind();
            }

            Nhanvien nhanvien = new Nhanvien();
            DataTable dtDonvi = new DataTable();
            DataTable dtChuyenVienBSC = new DataTable();

            //nhanvien = Session.GetCurrentUser();
            nhanvien = (Nhanvien)Session["nhanvien"];

            // Khai báo các biến cho việc kiểm tra quyền
            List<int> quyenHeThong = new List<int>();
            bool nFindResult = false;
            //quyenHeThong = Session.GetRole();
            quyenHeThong = (List<int>)Session["quyenhethong"];

            /*Kiểm tra nếu không có quyền giao bsc đơn vị (id của quyền là 2) thì đẩy ra trang đăng nhập*/
            nFindResult = quyenHeThong.Contains(3);

            if (!nFindResult)
            {
                Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }

            gDonvithamdinh = nhanvien.nhanvien_donvi_id;
            gNguoithamdinh = nhanvien.nhanvien_id;
            dtDonvi = getListDV();
            dtChuyenVienBSC = getChuyenVienBSC();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(DropDownListMonth.SelectedValue.ToString());
            int nam = Convert.ToInt32(DropDownListYear.SelectedValue.ToString());
            int loaiMauBSC = Convert.ToInt32(DropDownListLoaiMauBSC.SelectedValue.ToString());

            if (excelUpload.HasFile)
            {
                string FileName = Path.GetFileName(excelUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(excelUpload.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);
                excelUpload.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension, "Yes", thang, nam, loaiMauBSC);
            }
        }

        private void Import_To_Grid(string FilePath, string Extension, string isHDR, int thang, int nam, int loaiMauBSC)
        {
            bool bImport = false;
            int nTongKPI = 0;
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
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
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

            nTongKPI = countKPI(thang, nam, loaiMauBSC);
            if (dtResult.Rows.Count != nTongKPI)
            {
                Response.Write("<script>alert('File đã chọn có cấu trúc không giống với mẫu trong database!!!')</script>");
                return;
            }

            List<string> headers = new List<string>();
            foreach (DataColumn col in dtResult.Columns)
            {
                headers.Add(col.ColumnName);
            }

            // Insert data to db
            // Vòng lặp lấy ra danh sách 11 đơn vị huyện thị và KHTCDN
            for (int nColIndex = 4; nColIndex < headers.Count; nColIndex++)
            {
                bImport = false;

                string ma_dv = headers[nColIndex];
                int donvinhan_id = 0;
                donvinhan_id = getDvIdByMaDV(ma_dv);
                
                // Vòng lặp lấy ra các KPI và chỉ tiêu KPI ứng với đơn vị
                for (int nRowIndex = 0; nRowIndex < dtResult.Rows.Count; nRowIndex++)
                {
                    bool bResultBSCDV = false;
                    int kpi_id;
                    double kehoach;
                    string szKeHoach = dtResult.Rows[nRowIndex][nColIndex].ToString();
                    if (szKeHoach == "")
                    {
                        kehoach = 0;
                    }
                    else
                    {
                        double tmpKehoach = Convert.ToDouble(dtResult.Rows[nRowIndex][nColIndex].ToString().Trim());
                        kehoach = Convert.ToDouble(tmpKehoach);
                    }
                    kpi_id = Convert.ToInt32(dtResult.Rows[nRowIndex][0].ToString().Trim());

                    bResultBSCDV = insertBSC_DonVi(gDonvithamdinh, donvinhan_id, thang, nam, kpi_id, kehoach, loaiMauBSC);
                    if (bResultBSCDV == false)
                    {
                        bImport = false;
                        //Response.Write("<script>alert('Import không thành công!!! Cấu trúc hoặc dữ liệu trong file không đúng định dạng')</script>");
                        break;
                    }
                    bImport = true;
                }

                if (!bImport)
                {
                    break;
                }
            }

            if (bImport)
            {
                Response.Write("<script>alert('Import BSC thành công!!!')</script>");
            }
            else
            {
                Response.Write("<script>alert('Import không thành công!!! Cấu trúc hoặc dữ liệu trong file không đúng định dạng')</script>");
            }
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