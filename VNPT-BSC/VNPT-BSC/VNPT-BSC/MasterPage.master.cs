using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VNPT_BSC
{
    public partial class MasterPageEdit : System.Web.UI.MasterPage
    {
        public static int chucvu;
        protected void Page_Load(object sender, EventArgs e)
        {
            Nhanvien nhanvien = Session.GetCurrentUser();
            lblUser.Text = "Chào: <b>" + nhanvien.nhanvien_hoten + "</b>";
            lblDonvi.Text = nhanvien.nhanvien_donvi;
            lblChucvu.Text = nhanvien.nhanvien_chucvu;
            chucvu = nhanvien.nhanvien_chucvu_id;
        }
    }
}