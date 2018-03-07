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
    public partial class QuanLyNhanSu : System.Web.UI.Page
    {
        public DataTable dtNhanVien = new DataTable();

        public static DataTable getListNhanvien()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select nv.*, bl.ten_bacluong, cd.ten_chucdanh, dv.ten_donvi, nhom.ten_nhom ";
            sql += "from qlns_nhanvien nv, qlns_bacluong bl, qlns_chucdanh cd, qlns_donvi dv, qlns_nhom_donvi nhom ";
            sql += "where nv.nghiviec = 0 ";
            sql += "and nv.chucdanh = cd.id ";
            sql += "and nv.id_bacluong = bl.id ";
            sql += "and nv.donvi = dv.id ";
            sql += "and nv.id_nhom_donvi = nhom.id ";
            sql += "order by dv.ten_donvi asc ";

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

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Quản Lý Nhân Sự";
            dtNhanVien = getListNhanvien();
        }
    }
}