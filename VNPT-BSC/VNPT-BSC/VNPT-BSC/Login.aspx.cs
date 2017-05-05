using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data;
using System.Data.Sql;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

namespace VNPT_BSC
{
    public partial class Login : System.Web.UI.Page
    {
        public static List<int> quyenHeThong = new List<int>();
        
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

        private static DataTable getListChucVu(int nhanvien_id)
        {
            Connection cn = new Connection();
            DataTable dtResult = new DataTable();
            string sql = "select * from nhanvien_chucvu where nhanvien_id = '" + nhanvien_id + "'";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return dtResult;
        }

        [WebMethod]
        public static bool dangnhap(string idApprove, string passApprove)
        {
            Page objp = new Page();
            Connection cn = new Connection();
            Nhanvien nv = new Nhanvien();
            DataTable dt = new DataTable();
            DataTable dtQuyen = new DataTable();
            DataTable dtChucVu = new DataTable();

            bool output = false;
            quyenHeThong.Clear();
            string sqlllogin = "";
            sqlllogin = "select * from nhanvien a, donvi b where a.nhanvien_taikhoan = '" + idApprove + "' and a.nhanvien_matkhau = '" + passApprove + "' and a.nhanvien_donvi = b.donvi_id";
            try
            {
                dt = cn.XemDL(sqlllogin);
                if (dt.Rows.Count > 0) {
                    output = true;
                    nv.nhanvien_id = Convert.ToInt32(dt.Rows[0]["nhanvien_id"].ToString());
                    nv.nhanvien_manv = dt.Rows[0]["nhanvien_manv"].ToString();
                    nv.nhanvien_hoten = dt.Rows[0]["nhanvien_hoten"].ToString();
                    nv.nhanvien_donvi = dt.Rows[0]["donvi_ten"].ToString();
                    nv.nhanvien_donvi_id = Convert.ToInt32(dt.Rows[0]["nhanvien_donvi"].ToString());
                    objp.Session.SetCurrentUser(nv);

                    dtChucVu = getListChucVu(nv.nhanvien_id);
                    int numChucVu = dtChucVu.Rows.Count;
                    nv.nhanvien_chucvu_id = new int[numChucVu];

                    for (int nCVIndex = 0; nCVIndex < dtChucVu.Rows.Count; nCVIndex++) {
                        int chuvu_id = Convert.ToInt32(dtChucVu.Rows[nCVIndex]["chucvu_id"].ToString());
                        nv.nhanvien_chucvu_id[nCVIndex] = chuvu_id;

                        dtQuyen = getQuyenByChucVuID(chuvu_id);
                        for (int nIndex = 0; nIndex < dtQuyen.Rows.Count; nIndex++)
                        {
                            int quyen_id = Convert.ToInt32(dtQuyen.Rows[nIndex]["quyen_id"].ToString());
                            bool existItem = false;
                            existItem = quyenHeThong.Contains(quyen_id);
                            if (!existItem) {
                                quyenHeThong.Add(quyen_id);
                            }
                        }
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
            this.Title = "Đăng nhập";
        }
    }
}