using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using DevExpress.Web.ASPxEditors;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

namespace VNPT_BSC.BSC
{
    public partial class MauBSCNhanVien : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public static DataTable dsBSCDV = new DataTable();
        public static DataTable dtKPI = new DataTable();
        public static DataTable dtBSC = new DataTable();
        public static int nguoitao;
        public static int donvinhan;

        private DataTable dsBSCDuocGiao(int donvinhan) {
            DataTable dsBSC = new DataTable();
            string sqlBSCDuocGiao = "select top 10 thang,nam from giaobscdonvi where donvinhan = '" + donvinhan + "' and trangthaigiao = 1 group by thang, nam order by nam,thang DESC";
            try
            {
                dsBSC = cn.XemDL(sqlBSCDuocGiao);
            }
            catch (Exception ex) {
                throw ex;
            }

            return dsBSC;
        }

        /*Get KPI list*/
        private DataTable getKPIList(int nguoitao)
        {
            string sqlKPI = "select kpi.kpi_id, kpo.kpo_id, kpi.kpi_ten + ' (' + kpo.kpo_ten + ')' as name from kpi, kpo where kpi.kpi_thuoc_kpo = kpo.kpo_id and kpi.kpi_nguoitao = '" + nguoitao + "' order by kpo.kpo_id ASC";
            DataTable dtKPI = new DataTable();
            try
            {
                dtKPI = cn.XemDL(sqlKPI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtKPI;
        }

        private DataTable getBSCList(int nguoitao)
        {
            string sqlBSC = "select thang,nam from danhsachbsc where nguoitao = '" + nguoitao + "' group by thang, nam order by nam,thang";
            DataTable dtBSC = new DataTable();
            try
            {
                dtBSC = cn.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtBSC;
        }

        [WebMethod]
        public static string loadKPIDuocGiao(int thang, int nam, int donvinhan) {
            Connection cnData = new Connection();
            DataTable dtKPIDuocGiao = new DataTable();
            string szOutput = "";
            string sql = "select kpi.kpi_id, kpo.kpo_id, kpi.kpi_ten + ' (' + kpo.kpo_ten + ')' as name ";
            sql += "from bsc_donvi bscdv, kpi, kpo ";
            sql += "where bscdv.thang = '" + thang + "' and bscdv.nam = '" + nam + "' and bscdv.donvinhan = '" + donvinhan + "' ";
            sql += "and bscdv.kpi = kpi.kpi_id ";
            sql += "and kpi.kpi_thuoc_kpo = kpo.kpo_id";
            try {
                dtKPIDuocGiao = cnData.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            for (int i = 0; i < dtKPIDuocGiao.Rows.Count; i++) {
                szOutput += "<div class='checkbox'>";
                szOutput += "<label><input type='checkbox' checked value='" + dtKPIDuocGiao.Rows[i]["kpi_id"].ToString() + "' />" + dtKPIDuocGiao.Rows[i]["name"].ToString() + "</label>";
                szOutput += "</div>";
            }
            
            return szOutput;
        }

        [WebMethod]
        public static string[] BindingCheckBox(int monthAprove, int yearAprove, int nguoitao)
        {
            DataTable dtKPI = new DataTable();
            Connection cnDanhSachBSC = new Connection();
            string[] arrKPI = { };
            string sql = "select * from danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao = '" + nguoitao + "'";
            dtKPI = cnDanhSachBSC.XemDL(sql);
            if (dtKPI.Rows.Count > 0)
            {
                arrKPI = new string[dtKPI.Rows.Count];
                for (int i = 0; i < dtKPI.Rows.Count; i++)
                {
                    arrKPI[i] = dtKPI.Rows[i]["kpi_id"].ToString();
                }
            }
            return arrKPI;
        }

        [WebMethod]
        public static bool SaveData(int monthAprove, int yearAprove, string[] arrKPI_ID, int nguoitao)
        {
            Connection cnDanhSachBSC = new Connection();
            bool output = false;
            string sqlDelOldData = "delete danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao = '" + nguoitao + "'";
            string sqlInsertNewData = "";
            try
            {
                cnDanhSachBSC.ThucThiDL(sqlDelOldData);
                for (int i = 0; i < arrKPI_ID.Length; i++)
                {
                    int kpi_id = Convert.ToInt32(arrKPI_ID[i].ToString());
                    string curDate = DateTime.Now.ToString("yyyy-MM-dd");
                    sqlInsertNewData = "insert into danhsachbsc(thang, nam, kpi_id, nguoitao, ngaytao) values('" + monthAprove + "', '" + yearAprove + "', '" + kpi_id + "', '" + nguoitao + "','" + curDate + "')";
                    try
                    {
                        cnDanhSachBSC.ThucThiDL(sqlInsertNewData);
                    }
                    catch (Exception ex)
                    {
                        output = false;
                    }
                }
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Nhanvien nhanvien = new Nhanvien();
            nhanvien = Session.GetCurrentUser();
            /*Nếu không tồn tại session hoặc chức vụ của nhân viên không phải Trưởng phòng hoặc GĐ phòng bán hàng thì trở về trang login*/
            if (nhanvien == null || nhanvien.nhanvien_chucvu_id != 2 && nhanvien.nhanvien_chucvu_id != 4)
            {
                Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
            donvinhan = nhanvien.nhanvien_donvi_id;
            nguoitao = nhanvien.nhanvien_id;

            if (!IsPostBack) {
                dsBSCDV = dsBSCDuocGiao(donvinhan);
                dtKPI = getKPIList(nguoitao);
                dtBSC = getBSCList(nguoitao);
            }
        }
    }
}