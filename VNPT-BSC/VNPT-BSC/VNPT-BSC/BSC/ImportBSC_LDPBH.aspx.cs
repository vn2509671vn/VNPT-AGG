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
    public partial class ImportBSC_LDPBH : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public string gNguoitao;
        public string gDonvi;

        private DataTable dtLDPBH(string ma_pbh)
        {
            DataTable dtResult = new DataTable();
            string sql = "select nv.nhanvien_id, nv.nhanvien_manv, nv.nhanvien_hoten, dv.ten_donvi, nv.nhanvien_didong, nv.nhanvien_chucdanh ";
            sql += "from nhanvien nv, qlns_donvi dv ";
            sql += "where nv.nhanvien_donvi = dv.id ";
            sql += "and nv.nhanvien_chucdanh in (1,2) ";
            sql += "and dv.ma_donvi = '" + ma_pbh + "' ";
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

        private bool insertGiaoBSCNhanVien(string nhanviengiao, string nhanviennhan, string thang, string nam, string chucdanh)
        {
            bool bResult = false;
            string sql = "insert into giaobscnhanvien(nhanviengiao, nhanviennhan, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaidongy_kqtd, trangthaiketthuc, ngaytao, loaimau) ";
            if (chucdanh == "1")
            {
                // Giao chính thức cho giám đốc pbh
                sql += "values('" + nhanviengiao + "', '" + nhanviennhan + "', '" + thang + "', '" + nam + "', 1, 0, 0, 0, 0, GETDATE(), 18)";
            }
            else { 
                // Tạm giao cho phó gđ pbh sau đó gđ pbh sẽ vào giao lại
                sql += "values('" + nhanviengiao + "', '" + nhanviennhan + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0, 0, GETDATE(), 18)";
            }

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

        private DataTable getKPIDeTail(string kpi_id)
        {
            DataTable dtResult = new DataTable();
            string sql = "select kpi.*, nhom_kpi.*  from kpi, nhom_kpi where kpi.kpi_thuoc_kpo = 2025 and nhom_kpi.id = kpi.nhom_kpi and kpi.kpi_id = '" + kpi_id + "'";
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

        private string getGiamDocPBH(string ma_nhanvien) {
            DataTable tmp = new DataTable();
            string sqlDonVi = "select nhanvien_donvi from nhanvien where nhanvien_id = '" + ma_nhanvien + "'";
            
            try
            {
                tmp = cn.XemDL(sqlDonVi);
                string sql = "select * from nhanvien where nhanvien_donvi = '" + tmp.Rows[0][0].ToString() + "' and nhanvien_chucdanh = 1";
                try
                {
                    tmp = cn.XemDL(sql);
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return tmp.Rows[0]["nhanvien_id"].ToString();
        }

        private bool insertBSC_NhanVien(string nhanviengiao, string nhanviennhan, string thang, string nam, string kpi_id, double kehoach, string nNhomKPI, string trongso, string chucdanh_nguoinhan)
        {
            DataTable dtKPIDetai = new DataTable();
            bool bResult = false;
            dtKPIDetai = getKPIDeTail(kpi_id);
            if (dtKPIDetai.Rows.Count > 0)
            {
                try
                {
                    string nhanvienthamdinh = nhanviengiao;
                    if (nNhomKPI == "75" && chucdanh_nguoinhan != "1")
                    {
                        nhanvienthamdinh = getGiamDocPBH(nhanviennhan);
                    }
                    string donvitinh = "17";
                    //string trongso = dtKPIDetai.Rows[0]["tytrong"].ToString().Trim();
                    string sql = "insert into bsc_nhanvien(nhanviengiao, nhanviennhan, thang, nam, kpi, nhanvienthamdinh, donvitinh, trongso, kehoach, thuchien, thamdinh, trangthaithamdinh, kq_thuchien, diem_kpi, hethong_thuchien, loaimau, nhom_kpi, donvi) ";
                    sql += "values('" + nhanviengiao + "', '" + nhanviennhan + "', '" + thang + "', '" + nam + "', '" + kpi_id + "', '" + nhanvienthamdinh + "', '" + donvitinh + "', '" + trongso + "', '" + kehoach + "', 0, 0, 0, 0, 0, 0, 18, '" + nNhomKPI + "', '" + gDonvi + "')";
                    cn.ThucThiDL(sql);
                    bResult = true;
                }
                catch (Exception ex)
                {
                    bResult = false;
                }
            }
            return bResult;
        }

        private void delBSC_NV(string nhanviengiao, string thang, string nam)
        {
            string sql = "delete bsc_nhanvien where nhanviengiao = '" + nhanviengiao + "' and thang = '" + thang + "' and nam = '" + nam + "' and loaimau = '18'";
            try
            {
                cn.ThucThiDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void delGiaoBSCNV(string nhanviengiao, string thang, string nam)
        {
            string sql = "delete giaobscnhanvien where nhanviengiao = '" + nhanviengiao + "' and thang = '" + thang + "' and nam = '" + nam + "' and loaimau = '18'";
            try
            {
                cn.ThucThiDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getNhomKPI(string kpi_id) {
            DataTable dtNhomKPI = new DataTable();
            string sql = "select nhom_kpi from kpi where kpi_id = '" + kpi_id + "'";
            try
            {
                dtNhomKPI = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (dtNhomKPI.Rows.Count > 0) {
                return dtNhomKPI.Rows[0][0].ToString();
            }

            return "";
        }

        private string CreateAndGetKPI(string ten_kpi) {
            DataTable dtTmp = new DataTable();
            string sqlInsert = "insert into kpi(kpi_ten, kpi_mota, kpi_ngaytao, kpi_nguoitao, kpi_thuoc_kpo, kpi_ma, nhom_kpi, hienthi) ";
            sqlInsert += "values(N'" + ten_kpi + "', N'" + ten_kpi + "', GETDATE(), '"+gNguoitao+"', 2025, 8, 75, 1)";

            string sqlSearch = "select top 1 * from kpi where kpi_ten like N'%" + ten_kpi + "%' ";
            dtTmp = cn.XemDL(sqlSearch);
            if (dtTmp.Rows.Count > 0)
            {
                return dtTmp.Rows[0]["kpi_id"].ToString();
            }
            else {
                try
                {
                    cn.ThucThiDL(sqlInsert);
                    dtTmp = cn.XemDL(sqlSearch);
                    return dtTmp.Rows[0]["kpi_id"].ToString();
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Giao BSC cho LĐ PBH";
            Nhanvien nhanvien = new Nhanvien();


            //nhanvien = Session.GetCurrentUser();
            nhanvien = (Nhanvien)Session["nhanvien"];

            // Khai báo các biến cho việc kiểm tra quyền
            List<int> quyenHeThong = new List<int>();
            bool nFindResult = false;
            //quyenHeThong = Session.GetRole();
            quyenHeThong = (List<int>)Session["quyenhethong"];

            /*Kiểm tra nếu không có quyền giao bsc pbh (id của quyền là 2) thì đẩy ra trang đăng nhập*/
            nFindResult = quyenHeThong.Contains(2);

            if (!nFindResult)
            {
                Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }

            gNguoitao = nhanvien.nhanvien_id.ToString();
            gDonvi = nhanvien.nhanvien_donvi_id.ToString();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string thang = DropDownListMonth.SelectedValue.ToString();
            string nam = DropDownListYear.SelectedValue.ToString();

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

        private void Import_To_Grid(string FilePath, string Extension, string isHDR, string thang, string nam)
        {
            bool bImport = false;
            bool isExist = false;
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

            List<string> headers = new List<string>();
            foreach (DataColumn col in dtResult.Columns)
            {
                String szTmp = col.ColumnName.ToString().Trim();
                headers.Add(szTmp);
            }

            // Insert data to db
            // Vòng lặp lấy ra danh sách các nhân viên
            for (int nColIndex = 3; nColIndex < headers.Count; nColIndex++)
            {
                bImport = false;

                string ma_donvi = headers[nColIndex];
                DataTable dtLanhDao = new DataTable();
                bool bResultGiaoBSCNV = false;
                dtLanhDao = dtLDPBH(ma_donvi);
                for (int i = 0; i < dtLanhDao.Rows.Count; i++) {
                    bResultGiaoBSCNV = insertGiaoBSCNhanVien(gNguoitao, dtLanhDao.Rows[i]["nhanvien_id"].ToString(), thang, nam, dtLanhDao.Rows[i]["nhanvien_chucdanh"].ToString());
                    if (bResultGiaoBSCNV == false)
                    {
                        isExist = true;
                        Response.Write("<script>alert('Import không thành công!!! Có thể bạn đã giao BSC cho thời gian này rồi')</script>");
                        break;
                    }

                    // Vòng lặp lấy ra các KPI và chỉ tiêu KPI ứng với nhân viên
                    for (int nRowIndex = 0; nRowIndex < dtResult.Rows.Count; nRowIndex++)
                    {
                        bool bResultBSCNV = false;
                        string kpi_id;
                        double kehoach;
                        string szKeHoach = dtResult.Rows[nRowIndex][nColIndex].ToString().Trim();
                        string szTrongso = dtResult.Rows[nRowIndex][2].ToString().Trim();
                        
                        if (szKeHoach == "")
                        {
                            kehoach = 0;
                        }
                        else
                        {
                            double tmpKehoach = Convert.ToDouble(dtResult.Rows[nRowIndex][nColIndex].ToString().Trim());
                            kehoach = Convert.ToDouble(tmpKehoach);
                        }

                        kpi_id = dtResult.Rows[nRowIndex][0].ToString().Trim();
                        if (kpi_id == "") {
                            kpi_id = CreateAndGetKPI(dtResult.Rows[nRowIndex][1].ToString().Trim());
                        }

                        string nNhomKPI = getNhomKPI(kpi_id);

                        bResultBSCNV = insertBSC_NhanVien(gNguoitao, dtLanhDao.Rows[i]["nhanvien_id"].ToString(), thang, nam, kpi_id, kehoach, nNhomKPI, szTrongso, dtLanhDao.Rows[i]["nhanvien_chucdanh"].ToString());
                        if (bResultBSCNV == false)
                        {
                            delBSC_NV(gNguoitao, thang, nam);
                            bImport = false;
                            break;
                        }
                        bImport = true;
                    }

                    if (!bImport)
                    {
                        break;
                    }
                }
            }

            if (!isExist)
            {
                if (bImport)
                {
                    // Send sms thông báo tới các đơn vị
                    Message msg = new Message();
                    for (int nMaDV = 3; nMaDV < headers.Count; nMaDV++)
                    {
                        string ma_dv = headers[nMaDV];
                        DataTable dtSDT = dtLDPBH(ma_dv);
                        for (int i = 0; i < dtSDT.Rows.Count; i++)
                        {
                            string szSDT = dtSDT.Rows[i]["nhanvien_didong"].ToString().Trim();
                            string szContent = "Ban vua nhan duoc BSC/KPI " + thang + "-" + nam + ". Vui long vao kiem tra va xac nhan!!!";
                            msg.SendSMS_VTTP(szSDT, szContent);
                        }
                    }

                    Response.Write("<script>alert('Import BSC thành công!!!')</script>");
                }
                else
                {
                    delGiaoBSCNV(gNguoitao, thang, nam);
                    Response.Write("<script>alert('Import không thành công!!! Cấu trúc hoặc dữ liệu trong file không đúng định dạng')</script>");
                }
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