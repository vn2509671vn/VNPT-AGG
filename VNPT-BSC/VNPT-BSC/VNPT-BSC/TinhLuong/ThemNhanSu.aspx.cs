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

namespace VNPT_BSC.TinhLuong
{
    public partial class ThemNhanSu : System.Web.UI.Page
    {
        public  DataTable dtDonvi = new DataTable();
        public  DataTable dtChucdanh = new DataTable();
        public  DataTable dtNhomDonvi = new DataTable();

        private DataTable getDonVi() {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_donvi where id != 1 order by id asc";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            return tmp;
        }

        private DataTable getChucDanh()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_chucdanh order by ten_chucdanh asc";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmp;
        }

        private DataTable getNhomDV()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_nhom_donvi order by ten_nhom asc";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmp;
        }

        [WebMethod]
        public static Dictionary<String, String> loadNhanSu(string cmnd)
        {
            Dictionary<String, String> dicOutput = new Dictionary<String, String>();
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select nhanvien_id, nhanvien_manv, nhanvien_hoten, nhanvien_donvi from nhanvien where nhanvien_cmnd = '" + cmnd + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("id", tmp.Rows[0]["nhanvien_id"].ToString().Trim());
                dicOutput.Add("manv", tmp.Rows[0]["nhanvien_manv"].ToString().Trim());
                dicOutput.Add("hoten", tmp.Rows[0]["nhanvien_hoten"].ToString().Trim());
                dicOutput.Add("donvi", tmp.Rows[0]["nhanvien_donvi"].ToString().Trim());
            }
            else {
                dicOutput.Add("id", "");
                dicOutput.Add("manv", "");
                dicOutput.Add("hoten", "");
                dicOutput.Add("donvi", "");
            }
            return dicOutput;
        }

        [WebMethod]
        public static string loadBacLuongTheoChucDanh(int chucdanh) {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string szResult = "";
            string sql = "select * from qlns_bacluong where id_chucdanh = '" + chucdanh + "' order by ten_bacluong asc";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                for (int n = 0; n < tmp.Rows.Count; n++) {
                    szResult += "<option value='" + tmp.Rows[n]["id"].ToString().Trim() + "'> Bậc " + tmp.Rows[n]["ten_bacluong"].ToString().Trim() + "</option>";
                }
            }
            return szResult;
        }

        public static bool existNV(int id)
        {
            DataTable tmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select nhanvien_id from nhanvien where nhanvien_id = '" + id + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                return true;
            }
            return false;
        }

        [WebMethod]
        public static Dictionary<string, string> luuThongTinChung(int id, string cmnd, string manv, string hoten, string bank, bool chinhthuc, bool dangvien, int donvi, int chucdanh, int bacluong, int nhomdonvi, string ngaykyhd)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string newDate = "";
            DateTime dt = DateTime.Parse(ngaykyhd);
            newDate = dt.ToString("dd/MM/yyyy");

            bool isInsert = existNV(id);
            if (isInsert)
            {
                string sql = "insert into qlns_nhanvien(id, ma_nhanvien, ten_nhanvien, donvi, chucdanh, sotaikhoan, id_bacluong, id_nhom_donvi, chinhthuc, dangvien, ngaykyhd, cmnd, nghiviec, thaisan, hesoluong, luongcoban) ";
                sql += "values('" + id + "', '" + manv + "', N'" + hoten + "', '" + donvi + "', '" + chucdanh + "', '" + bank + "', '" + bacluong + "', '" + nhomdonvi + "', '" + chinhthuc + "', '" + dangvien + "', '" + newDate + "', '" + cmnd + "', 0, 0, 0, 0)";
                try
                {
                    cn.ThucThiDL(sql);
                    dicResult.Add("status", "ok");
                    dicResult.Add("message", "Thêm nhân viên thành công!");
                }
                catch {
                    dicResult.Add("status", "error");
                    dicResult.Add("message", "Nhân viên này đã tồn tại!");
                }
            }
            else {
                dicResult.Add("status","error");
                dicResult.Add("message", "Thêm nhân viên không thành công! Vui lòng thử lại.");
            }
            return dicResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Thêm nhân sự";
            dtDonvi = getDonVi();
            dtChucdanh = getChucDanh();
            dtNhomDonvi = getNhomDV();
        }
    }
}