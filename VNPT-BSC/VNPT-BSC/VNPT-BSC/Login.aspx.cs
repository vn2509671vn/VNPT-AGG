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
    public partial class Login : System.Web.UI.Page
    {
        public static int[] quyenHeThong;
        
        private static DataTable getQuyenByChucVuID(int chucvu_id)
        {
            Connection cn = new Connection();
            DataTable result = new DataTable();
            string sql = "select * from quyen_cv where chucvu_id = '" + chucvu_id + "'";
            try
            {
                result = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        [WebMethod]
        public static bool dangnhap(string idApprove, string passApprove)
        {
            Page objp = new Page();
            Connection cn = new Connection();
            Nhanvien nv = new Nhanvien();
            DataTable dt = new DataTable();
            DataTable dtQuyen = new DataTable();

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
                    nv.nhanvien_donvi_id = Convert.ToInt32(dt.Rows[0]["nhanvien_donvi"].ToString());
                    nv.nhanvien_chucvu_id = Convert.ToInt32(dt.Rows[0]["nhanvien_chucvu"].ToString());
                    objp.Session.SetCurrentUser(nv);

                    dtQuyen = getQuyenByChucVuID(nv.nhanvien_chucvu_id);
                    quyenHeThong = new int[dtQuyen.Rows.Count];
                    for (int nIndex = 0; nIndex < dtQuyen.Rows.Count; nIndex++)
                    {
                        quyenHeThong[nIndex] = Convert.ToInt32(dtQuyen.Rows[nIndex]["quyen_id"].ToString());
                    }
                    objp.Session.SetRole(quyenHeThong);
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