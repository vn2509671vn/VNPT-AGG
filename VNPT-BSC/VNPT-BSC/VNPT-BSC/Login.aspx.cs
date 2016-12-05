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
    public partial class Login : System.Web.UI.Page
    {
        [WebMethod]
        public static bool dangnhap(string idApprove, string passApprove)
        {
            Page objp = new Page();
            Connection cn = new Connection();
            Nhanvien nv = new Nhanvien();
            DataTable dt = new DataTable();
            bool output = false;
            string sqlllogin = "";
            sqlllogin = "select * from nhanvien a, donvi b,chucvu c where a.nhanvien_taikhoan = '" + idApprove + "' and a.nhanvien_matkhau = '" + passApprove + "' and a.nhanvien_donvi = b.donvi_id and a.nhanvien_chucvu = c.chucvu_id";
            try
            {
                dt = cn.XemDL(sqlllogin);
                if (dt.Rows.Count > 0) {
                    output = true;
                    nv.nhanvien_id = Convert.ToInt32(dt.Rows[0][0].ToString());
                    nv.nhanvien_hoten = dt.Rows[0][1].ToString();
                    nv.nhanvien_donvi = dt.Rows[0]["donvi_ten"].ToString();
                    nv.nhanvien_chucvu = dt.Rows[0]["chucvu_ten"].ToString();
                    objp.Session.SetCurrentUser(nv);
                    
                }
                else{
                    output = false;
                    nv = null;
                }
            }
            catch
            {
                output = false;
            }
            return output;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}