using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
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

        [WebMethod]
        public static bool changePass(int nhanvien_id, string old_pwd, string new_pwd) {
            Connection cn = new Connection();
            bool bResult = false;
            DataTable dtTmp = new DataTable();
            string isSuccess = "update nhanvien set nhanvien_matkhau = '" + new_pwd + "' where nhanvien_id = '" + nhanvien_id + "' and nhanvien_matkhau = '" + old_pwd + "'";
            string sql = "select * from nhanvien where nhanvien_id = '" + nhanvien_id + "' and nhanvien_matkhau = '" + old_pwd + "'";
            try
            {
                dtTmp = cn.XemDL(sql);
                if (dtTmp.Rows.Count > 0)
                {
                    cn.ThucThiDL(isSuccess);
                    bResult = true;
                }
                else {
                    bResult = false;
                }
            }
            catch {
                bResult = false;
            }
            return bResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}