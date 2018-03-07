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
    public partial class KiemNhiem : System.Web.UI.Page
    {
        public DataTable dtChucVuKN = new DataTable();

        public static DataTable getListChucVuKN()
        {
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

        [WebMethod]
        public static bool SaveData(string ten_chucvu, decimal heso)
        {
            Connection cn = new Connection();
            bool bResult = false;
            string sql = "insert into qlns_phucapkiemnhiem(chucvu_kiemnhiem, heso_phucap) values(N'" + ten_chucvu + "', '" + heso + "')";
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
        public static bool EditData(int id, string ten_chucvu, decimal heso)
        {
            Connection cn = new Connection();
            bool bResult = false;
            string sql = "update qlns_phucapkiemnhiem set chucvu_kiemnhiem = N'" + ten_chucvu + "', heso_phucap = '" + heso + "' where id = '" + id + "'";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                this.Title = "Quản Lý Chức Vụ Kiêm Nhiệm";
                dtChucVuKN = getListChucVuKN();
            //}
        }
    }
}