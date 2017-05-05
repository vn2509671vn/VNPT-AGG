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
            string sql = "select * from qlns_nhanvien";
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
        public static bool saveData(int id_nv, int donvi, int nhomdonvi, int chucdanh, int bacluong, string stk, double hesoluong, bool chinhthuc, bool thaisan)
        {
            bool bResult = false;
            Connection cn = new Connection();
            double luong_p3 = 0;
            double luong_duytri = 0;
            luong_p3 = hesoluong * 1150000;
            luong_duytri = hesoluong * 3500000;
            string sql = "update qlns_nhanvien set donvi = '" + donvi + "', chucdanh = '" + chucdanh + "', sotaikhoan = '" + stk + "', hesoluong = '" + hesoluong + "', luong_p3 = '" + luong_p3 + "', luong_duytri = '" + luong_duytri + "', id_bacluong = '" + bacluong + "', id_nhom_donvi = '" + nhomdonvi + "', chinhthuc = '" + chinhthuc + "', thaisan = '" + thaisan + "' where id = '" + id_nv + "'";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "Quản Lý Nhân Sự";
                dtBacLuong = getListBacLuong();
                dtChucDanh = getListChucDanh();
                dtDonvi = getListDonvi();
                dtNhomDonvi = getListNhomDonvi();
                dtNhanVien = getListNhanvien();
            }
        }
    }
}