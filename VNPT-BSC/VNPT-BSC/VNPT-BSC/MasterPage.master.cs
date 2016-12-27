using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using DevExpress.Web.ASPxEditors;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;



namespace VNPT_BSC
{
    public partial class MasterPageEdit : System.Web.UI.MasterPage
    {
        public static int chucvu;
        public static List<int> quyenHeThong = new List<int>();
        public static JavaScriptSerializer javaSerial = new JavaScriptSerializer();

        protected void Page_Load(object sender, EventArgs e)
        {
            Nhanvien nhanvien = new Nhanvien();
            DataTable dtQuyen = new DataTable();
            nhanvien = Session.GetCurrentUser();
            quyenHeThong = Session.GetRole();
            if(nhanvien == null){
                Response.Redirect("~/Login.aspx");
            }

            lblUser.Text = "Chào: <b>" + nhanvien.nhanvien_hoten + "</b>";
            lblDonvi.Text = nhanvien.nhanvien_donvi;
            lblChucvu.Text = "Đang cập nhật";
        }
    }
}