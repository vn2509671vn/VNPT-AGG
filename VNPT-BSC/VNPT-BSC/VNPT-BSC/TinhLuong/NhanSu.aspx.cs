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
    public partial class NhanSu : System.Web.UI.Page
    {
        public static DataTable dtBacLuong = new DataTable();
        public static DataTable dtChucDanh = new DataTable();
        public static DataTable dtDonvi = new DataTable();
        public static DataTable dtNhomDonvi = new DataTable();
        public static DataTable dtNhanVien = new DataTable();
        public static DataTable dtKiemNhiem = new DataTable();

        public static DataTable getListKiemNhiem() {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_phucapkiemnhiem";
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

        public static DataTable getListBacLuong()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select bluong.*, chucdanh.ten_chucdanh from qlns_bacluong bluong, qlns_chucdanh chucdanh where bluong.id_chucdanh = chucdanh.id order by bluong.id_chucdanh, bluong.ten_bacluong asc";
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

        public static DataTable getListChucDanh()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_chucdanh";
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

        public static DataTable getListDonvi()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_donvi";
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

        public static DataTable getListNhomDonvi()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_nhom_donvi";
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

        public static DataTable getListNhanvien()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_nhanvien where nghiviec = 0";
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
        public static bool saveData(int id_nv, int donvi, int nhomdonvi, int chucdanh, int bacluong, string stk, double hesoluong, bool chinhthuc, bool dangvien)
        {
            bool bResult = false;
            Connection cn = new Connection();
            double luongcoban = 0;
            luongcoban = hesoluong * 3500000;
            string sql = "update qlns_nhanvien set donvi = '" + donvi + "', chucdanh = '" + chucdanh + "', sotaikhoan = '" + stk + "', hesoluong = '" + hesoluong + "', luongcoban = '" + luongcoban + "', id_bacluong = '" + bacluong + "', id_nhom_donvi = '" + nhomdonvi + "', chinhthuc = '" + chinhthuc + "', dangvien = '" + dangvien + "' where id = '" + id_nv + "'";
            try
            {
                cn.ThucThiDL(sql);
                bResult = true;
            }
            catch (Exception ex) {
                throw ex;
            }
            return bResult;
        }

        [WebMethod]
        public static bool delNV(int id_nv)
        {
            bool bResult = false;
            Connection cn = new Connection();
            string sql = "update qlns_nhanvien set nghiviec = '1' where id = '" + id_nv + "'";
            try
            {
                cn.ThucThiDL(sql);
                bResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bResult;
        }

        [WebMethod]
        public static List<int> loadKiemNhiem(int id_nv)
        {
            List<int> lResult = new List<int>();
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_kiemnhiem_nhanvien where id_nhanvien = '" + id_nv + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            for (int i = 0; i < tmp.Rows.Count; i++) {
                lResult.Add(Convert.ToInt32(tmp.Rows[i]["id_kiemnhiem"].ToString()));
            }

            return lResult;
        }

        [WebMethod]
        public static bool SaveKiemNhiem(int id_nv, int[] chucvu) {
            bool bResult = false;
            Connection cn = new Connection();
            string sqlDel = "delete qlns_kiemnhiem_nhanvien where id_nhanvien = '" + id_nv + "'";
            try
            {
                cn.ThucThiDL(sqlDel);
                for (int i = 0; i < chucvu.Length; i++) {
                    string sql = "insert into qlns_kiemnhiem_nhanvien(id_nhanvien, id_kiemnhiem) values('" + id_nv + "', '" + chucvu[i] + "')";
                    try
                    {
                        cn.ThucThiDL(sql);
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
                bResult = true;
            }
            catch (Exception ex) {
                throw ex;
            }

            return bResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                this.Title = "Quản Lý Nhân Sự";
                dtBacLuong = getListBacLuong();
                dtChucDanh = getListChucDanh();
                dtDonvi = getListDonvi();
                dtNhomDonvi = getListNhomDonvi();
                dtNhanVien = getListNhanvien();
                dtKiemNhiem = getListKiemNhiem();
            //}
        }
    }
}