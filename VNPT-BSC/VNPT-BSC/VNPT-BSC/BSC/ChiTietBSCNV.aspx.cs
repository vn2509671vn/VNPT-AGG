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
    public partial class ChiTietBSCNV1 : System.Web.UI.Page
    {
        public DataTable dtChiTiet = new DataTable();
        public DataTable dtInfo = new DataTable();
        public int thang, nam;

        public static DataTable getNhanVien(int id_nv, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select nvn.nhanvien_hoten as nvn, nvg.nhanvien_hoten as nvg, dv.donvi_ten, trangthaigiao = CASE bsc.trangthaigiao when 1 then N'Đã ký' else N'Chưa ký' end,  trangthainhan = CASE bsc.trangthainhan when 1 then N'Đã ký' else N'Chưa ký' end, trangthaidongy_kqtd = CASE bsc.trangthaidongy_kqtd when 1 then N'Đã ký' else N'Chưa ký' end, trangthaiketthuc = CASE bsc.trangthaiketthuc when 1 then N'Đã ký' else N'Chưa ký' end ";
            sql += "from giaobscnhanvien bsc, nhanvien nvn, donvi dv, nhanvien nvg ";
            sql += "where bsc.nhanviennhan = nvn.nhanvien_id ";
            sql += "and bsc.nhanviengiao = nvg.nhanvien_id ";
            sql += "and nvg.nhanvien_donvi = dv.donvi_id ";
            sql += "and bsc.nhanviennhan = '" + id_nv + "' ";
            sql += "and bsc.thang = '" + thang + "' ";
            sql += "and bsc.nam = '" + nam + "' ";

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

        public static DataTable ChiTiet(int id_nv, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable dtResult = new DataTable();
            string sql = "select kpi.kpi_ten, nhom.ten_nhom , bsc.*, dvt.dvt_ten, isnull((bsc.kq_thuchien*100),0) as tlth, bsc.ghichu_thamdinh ";
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
            catch (Exception ex)
            {
                throw ex;
            }

            return dtResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id_nv = Convert.ToInt32(Request.QueryString["nhanviennhan"]);
                thang = Convert.ToInt32(Request.QueryString["thang"]);
                nam = Convert.ToInt32(Request.QueryString["nam"]);
                dtInfo = getNhanVien(id_nv, thang, nam);
                dtChiTiet = ChiTiet(id_nv, thang, nam);
            }
            catch
            {
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
        }
    }
}