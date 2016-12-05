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

namespace VNPT_BSC
{
    public partial class Chucvu : System.Web.UI.Page
    {

        Connection cv = new Connection();
        public DataTable dtchucvu;

        private DataTable getchucvuList()
        {
            string sqlBSC = "select chucvu_id,chucvu_ten,chucvu_mota,chucvu_ma from chucvu";
            DataTable dtchucvu = new DataTable();
            try
            {
                dtchucvu = cv.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                dtchucvu = null;
            }
            return dtchucvu;
        }

        [WebMethod]
        public static bool SaveData(string cv_tenAprove, string cv_motaAprove, string cv_maAprove)
        {
            Connection them_chucvu = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {


                sqlInsertNewData = "insert into chucvu(chucvu_ten,chucvu_mota, chucvu_ma) values('" + cv_tenAprove + "','" + cv_motaAprove + "', '" + cv_maAprove + "')";
                try
                {
                    them_chucvu.ThucThiDL(sqlInsertNewData);
                }
                catch
                {
                    output = false;
                }
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }
        [WebMethod]
        public static bool EditData(string cv_ten_suaAprove, string cv_mota_suaAprove, string cv_ma_suaAprove, int cv_id_suaAprove)
        {
            Connection chucvu_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update chucvu set chucvu_ten = '" + cv_ten_suaAprove + "',chucvu_mota = '" + cv_mota_suaAprove + "', chucvu_ma = '" + cv_ma_suaAprove + "' where chucvu_id = '" + cv_id_suaAprove + "'";
                try
                {
                    chucvu_edit.ThucThiDL(sqlUpdateData);
                }
                catch
                {
                    output = false;
                }
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool DeleteData(int cv_id_xoaAprove)
        {
            Connection chucvu_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {


                sqldeleteData = "delete chucvu where chucvu_id = '" + cv_id_xoaAprove + "'";
                try
                {
                    chucvu_delete.ThucThiDL(sqldeleteData);
                }
                catch
                {
                    output = false;
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
            if (!IsPostBack)
            {
                /*Get list BSC*/
                dtchucvu = new DataTable();
                dtchucvu = getchucvuList();


            }
        }
    }
}