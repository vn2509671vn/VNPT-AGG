using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using DevExpress.Web.ASPxEditors;

namespace VNPT_BSC.BSC
{
    public partial class MauBSC : System.Web.UI.Page
    {
        Connection cn = new Connection();

        /*Get KPO list*/
        private DataTable getKPOList()
        {
            string sqlKPO = "select * from kpo";
            DataTable dtKPO = new DataTable();
            try
            {
                dtKPO = cn.XemDL(sqlKPO);
            }
            catch (Exception ex){
                throw ex;
            }
            return dtKPO;
        }

        /*Get KPI list by KPO*/
        private DataTable getKPIListByKPO(int kpoID)
        {
            string sqlKPI = "select * from kpi where kpi_thuoc_kpo = '" + kpoID + "'";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                /*Fill year to dropdownlist*/
                int indexDropYear = 0;
                for (int nYear = 1900; nYear <= 2100; nYear++)
                {
                    ListItem lstItem = new ListItem(nYear.ToString(), nYear.ToString());
                    dropYear.Items.Insert(indexDropYear, lstItem);
                    indexDropYear++;
                }

                /*Fill KPO to dropdownlist*/
                DataTable dtKPO = new DataTable();
                dtKPO = getKPOList();
                dropKPO.DataSource = dtKPO;
                dropKPO.DataTextField = "kpo_ten";
                dropKPO.DataValueField = "kpo_id";
                dropKPO.DataBind();

                /*Fill KPI to CheckBoxList*/
                DataTable dtKPI = new DataTable();
                int kpoID = Convert.ToInt32(dropKPO.SelectedValue.ToString());
                dtKPI = getKPIListByKPO(kpoID);
                checkboxKPI.DataSource = dtKPI;
                checkboxKPI.DataTextField = "kpi_ten";
                checkboxKPI.DataValueField = "kpi_id";
                checkboxKPI.DataBind();

                /*Fill data to gridview*/
            }
        }

        protected void dropKPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Fill KPI to CheckBoxList*/
            DataTable dtKPI = new DataTable();
            int kpoID = Convert.ToInt32(dropKPO.SelectedValue.ToString());
            dtKPI = getKPIListByKPO(kpoID);
            checkboxKPI.DataSource = dtKPI;
            checkboxKPI.DataTextField = "kpi_ten";
            checkboxKPI.DataValueField = "kpi_id";
            checkboxKPI.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable tmp = (DataTable)ViewState["tmpTable"];
            string col_1 = null;
            string col_2 = null;
            col_1 = dropKPO.SelectedItem.ToString();
            col_2 = checkboxKPI.SelectedItem.ToString();
            tmp.Rows.Add(col_1, col_2);
            ViewState["tmpTable"] = tmp;
        }

        protected void gvBSC_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "KPI")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;

                combo.DataSource = getKPOList();
                combo.TextField = "kpi_ten";
                combo.ValueField = "kpi_id";
                combo.ValueType = typeof(System.Int32);
                combo.DataBindItems();
            }
        }
    }
}