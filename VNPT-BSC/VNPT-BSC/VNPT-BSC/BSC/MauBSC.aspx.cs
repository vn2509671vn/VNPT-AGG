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

        private DataTable getBSCList() {
            string sqlBSC = "select thang,nam from danhsachbsc group by thang, nam";
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
        public static string[] BindingCheckBox(int monthAprove, int yearAprove)
        {
            //return monthAprove + "@@" + yearAprove;
            //return DateTime.Now.ToString();
            DataTable dtKPI = new DataTable();
            Connection cnDanhSachBSC = new Connection();
            string[] arrKPI = {};
            string sql = "select * from danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "'";
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
        public static string[] SaveData(int monthAprove, int yearAprove, string[] arrKPI_ID)
        {

            return arrKPI_ID;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack) {
                /*Get list BSC*/
                dtBSC = new DataTable();
                dtBSC = getBSCList();

                /*Get list KPI*/
                dtKPI = new DataTable();
                dtKPI = getKPIList();
            }
        }
    }
}