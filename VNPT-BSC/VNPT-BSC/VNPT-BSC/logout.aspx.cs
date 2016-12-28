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
    public partial class logout : System.Web.UI.Page
    {

        [WebMethod]
        public static bool thoat()
        {
            Page objp = new Page();
            try
            {
                objp.Session.Abandon();
                objp.Session.Clear();
                objp.Session.RemoveAll();
            }
            catch (Exception ex){
                throw ex;
            }
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}