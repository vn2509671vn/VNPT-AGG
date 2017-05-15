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
    public partial class ChamCong : System.Web.UI.Page
    {
        public static DataTable dtBacLuong = new DataTable();
        public static DataTable dtChucDanh = new DataTable();
        public static DataTable getListBacLuong() {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select bluong.*, chucdanh.ten_chucdanh from qlns_bacluong bluong, qlns_chucdanh chucdanh where bluong.id_chucdanh = chucdanh.id order by bluong.id_chucdanh, bluong.ten_bacluong asc";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return tmp;
        }

        public static DataTable getListChucDanh() {
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

        [WebMethod]
        public static bool SaveData(string ten_bacluong, decimal heso_bacluong, int id_chucdanh)
        {
            Connection cn = new Connection();
            bool bResult = false;
            string sql = "insert into qlns_bacluong(ten_bacluong, heso_bacluong, id_chucdanh) values(N'" + ten_bacluong + "', '" + heso_bacluong + "', '" + id_chucdanh + "')";
            try {
                cn.ThucThiDL(sql);
                bResult = true;
            }
            catch (Exception ex){
                throw ex;
            }
            return bResult;
        }

        [WebMethod]
        public static bool EditData(int id_bacluong ,string ten_bacluong, decimal heso_bacluong, int id_chucdanh)
        {
            Connection cn = new Connection();
            bool bResult = false;
            string sql = "update qlns_bacluong set ten_bacluong = N'" + ten_bacluong + "', heso_bacluong = '" + heso_bacluong + "', id_chucdanh = '" + id_chucdanh + "' where id = '" + id_bacluong + "'";
            string sql_lichsu = "insert into qlns_lichsu_dieuchinh_bacluong(thoigian_dieuchinh, bacluong, hesomoi) values (getdate(), '" + id_bacluong + "', '" + heso_bacluong + "')";
            try
            {
                cn.ThucThiDL(sql);
                cn.ThucThiDL(sql_lichsu);
                bResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                this.Title = "Quản Lý Bậc Lương";
                dtBacLuong = getListBacLuong();
                dtChucDanh = getListChucDanh();
            //}
        }
    }
}