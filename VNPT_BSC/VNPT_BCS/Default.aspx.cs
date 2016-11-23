using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VNPT_BCS
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            DataTable dt =  cn.XemDL("select * from chucdanh");
            int count = dt.Rows.Count;
            string tmp = dt.Rows[0][0].ToString().Trim();
        }
    }
}