using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
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

        private string getTenChucVu(int chucvuID) {
            string szResult = "";
            Connection cn = new Connection();
            DataTable dtResult = new DataTable();
            string sql = "select chucvu_ten from chucvu where chucvu_id = '" + chucvuID + "'";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (dtResult.Rows.Count > 0) {
                szResult = dtResult.Rows[0][0].ToString();
            }

            return szResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Nhanvien nhanvien = new Nhanvien();
            DataTable dtQuyen = new DataTable();
            int nNumRole = 0;
            string szChucVu = "";
            //nhanvien = Session.GetCurrentUser();
            nhanvien = (Nhanvien)Session["nhanvien"];

            quyenHeThong = (List<int>)Session["quyenhethong"];
            if(nhanvien == null){
                Response.Redirect("~/Login.aspx");
            }

            nNumRole = nhanvien.nhanvien_chucvu_id.Length;
            lblUser.Text = "Chào: <b>" + nhanvien.nhanvien_hoten + "</b>";
            lblDonvi.Text = nhanvien.nhanvien_donvi;

            for (int nIndex = 0; nIndex < nNumRole; nIndex++) {
                int chucvu_id = nhanvien.nhanvien_chucvu_id[nIndex];
                szChucVu += getTenChucVu(chucvu_id);
                if (nIndex < nNumRole - 1) {
                    szChucVu += ",";
                }
            }

            lblChucvu.Text = szChucVu;
        }
    }
}