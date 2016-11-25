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