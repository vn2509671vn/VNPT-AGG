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

namespace VNPT_BSC.DanhMuc
{
    public partial class Donvitinh : System.Web.UI.Page
    {
        Connection dvt = new Connection();
        public DataTable dtdvt;

        private DataTable getdvtList()
        {
            string sqlkpi = "select * from donvitinh";
            DataTable dtdvt = new DataTable();
            try
            {
                dtdvt = dvt.XemDL(sqlkpi);
            }
            catch (Exception ex)
            {
                dtdvt = null;
            }
            return dtdvt;
        }
        [WebMethod]
        public static bool SaveData(string dvt_tenAprove, string dvt_motaAprove)
        {
            Connection donvitinh = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into donvitinh(dvt_ten,dvt_ghichu) values(N'" + dvt_tenAprove + "',N'" + dvt_motaAprove + "')";
                donvitinh.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(string dvt_ten_suaAprove, string dvt_mota_suaAprove, int dvt_id_suaAprove)
        {
            Connection dvt_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update donvitinh set dvt_ten = N'" + dvt_ten_suaAprove + "',dvt_ghichu = N'" + dvt_mota_suaAprove + "' where dvt_id = '" + dvt_id_suaAprove + "'";
                dvt_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool DeleteData(int dvt_idAprove)
        {
            Connection dvt_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete donvitinh where dvt_id = '" + dvt_idAprove + "'";
                dvt_delete.ThucThiDL(sqldeleteData);
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
                dtdvt = new DataTable();
                dtdvt = getdvtList();
            }
        }
    }
}