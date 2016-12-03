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
    public partial class MauBSC : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public DataTable dtBSC;
        public DataTable dtKPI;
        public static int nguoitao;
        /*Get KPO list*/
        private DataTable getKPIList()
        {
            string sqlKPI = "select kpi.kpi_id, kpo.kpo_id, kpi.kpi_ten + ' (' + kpo.kpo_ten + ')' as name from kpi, kpo where kpi.kpi_thuoc_kpo = kpo.kpo_id order by kpo.kpo_id ASC";
            DataTable dtKPI = new DataTable();
            try
            {
                dtKPI = cn.XemDL(sqlKPI);
            }
            catch (Exception ex){
                dtKPI = null;
            }
            return dtKPI;
        }

        private DataTable getBSCList(int nguoitao) {
            string sqlBSC = "select thang,nam from danhsachbsc where nguoitao = '" + nguoitao + "' group by thang, nam order by nam,thang";
            DataTable dtBSC = new DataTable();
            try {
                dtBSC = cn.XemDL(sqlBSC);
            }
            catch (Exception ex){
                dtBSC = null;
            }
            return dtBSC;
        }

        [WebMethod]
        public static string[] BindingCheckBox(int monthAprove, int yearAprove, int nguoitao)
        {
            DataTable dtKPI = new DataTable();
            Connection cnDanhSachBSC = new Connection();
            string[] arrKPI = {};
            string sql = "select * from danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao = '" + nguoitao + "'";
            dtKPI = cnDanhSachBSC.XemDL(sql);
            if (dtKPI.Rows.Count > 0) {
                arrKPI = new string[dtKPI.Rows.Count];
                for (int i = 0; i < dtKPI.Rows.Count; i++) {
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
                for (int i = 0; i < arrKPI_ID.Length; i++) {
                    int kpi_id = Convert.ToInt32(arrKPI_ID[i].ToString());
                    string curDate = DateTime.Now.ToString("yyyy-MM-dd");
                    sqlInsertNewData = "insert into danhsachbsc(thang, nam, kpi_id, nguoitao, ngaytao) values('" + monthAprove + "', '" + yearAprove + "', '" + kpi_id + "', '"+nguoitao+"','" + curDate + "')";
                    try {
                        cnDanhSachBSC.ThucThiDL(sqlInsertNewData);
                    }
                    catch {
                        output = false;
                    }
                }
                output = true;
            }
            catch {
                output = false;
            }
            return output;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            nguoitao = 1;
            if (!IsPostBack) {
                /*Get list BSC*/
                dtBSC = new DataTable();
                dtBSC = getBSCList(nguoitao);

                /*Get list KPI*/
                dtKPI = new DataTable();
                dtKPI = getKPIList();
            }
        }
    }
}