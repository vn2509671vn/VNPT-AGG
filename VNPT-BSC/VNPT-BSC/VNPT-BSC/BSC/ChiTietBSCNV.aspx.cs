using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Globalization;

namespace VNPT_BSC.BSC
{
    public partial class ChiTietBSCNV : System.Web.UI.Page
    {
        public static DataTable dtChiTiet = new DataTable();
        public static string ten_nhanvien = "";
        public static int thang, nam;

        public static string getTenNhanVien(int id_nv) {
            string szName = "";
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select nhanvien_hoten from nhanvien where nhanvien_id = '" + id_nv + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                szName = tmp.Rows[0][0].ToString();
            }

            return szName;
        }

        public static DataTable ChiTiet(int id_nv, int thang, int nam) {
            Connection cn = new Connection();
            DataTable dtResult = new DataTable();
            string sql = "select kpi.kpi_ten, nhom.ten_nhom , bsc.*, dvt.dvt_ten ";
            sql += "from bsc_nhanvien bsc, kpi, nhom_kpi nhom, donvitinh dvt ";
            sql += "where bsc.nhanviennhan = '" + id_nv + "' ";
            sql += "and bsc.thang = '" + thang + "' ";
            sql += "and bsc.nam = '" + nam + "' ";
            sql += "and bsc.kpi = kpi.kpi_id ";
            sql += "and bsc.nhom_kpi = nhom.id ";
            sql += "and bsc.donvitinh = dvt.dvt_id ";
            sql += "order by nhom.thutuhienthi asc";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            return dtResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Chi tiết bsc của nhân viên";
            //if (!IsPostBack)
            //{
                try
                {
                    int id_nv = Convert.ToInt32(Request.QueryString["nhanviennhan"]);
                    thang = Convert.ToInt32(Request.QueryString["thang"]);
                    nam = Convert.ToInt32(Request.QueryString["nam"]);
                    ten_nhanvien = getTenNhanVien(id_nv);
                    dtChiTiet = ChiTiet(id_nv, thang, nam);
                }
                catch
                {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            //}
        }
    }
}