using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VNPT
{
    public partial class Default : System.Web.UI.Page
    {



        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
}      
}