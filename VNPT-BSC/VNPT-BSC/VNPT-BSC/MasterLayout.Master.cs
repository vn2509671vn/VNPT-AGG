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
    public partial class MasterLayout : System.Web.UI.MasterPage
    {
        public int chucvu;
        public List<int> quyenToanHeThong = new List<int>();
        public JavaScriptSerializer javaSerial = new JavaScriptSerializer();
        public string userName = "";
        public int userID = 0;
        public int userDonVi = 0;
        public DataTable dtPhanHoiGiao = new DataTable();
        public DataTable dtPhanHoiThamDinh = new DataTable();

        private static DataTable getPhanHoiGiao() {
            Connection cn = new Connection();
            DataTable dtResult = new DataTable();
            string sql = "select bsc.thang, bsc.nam, bsc.donvinhan, dv.donvi_ten, bsc.donvigiao ";
            sql += "from bsc_donvi bsc, donvi dv, giaobscdonvi ";
            sql += "where bsc.phanhoi_giao_daxuly = 0 ";
            sql += "and bsc.donvinhan = dv.donvi_id ";

            sql += "and bsc.donvigiao = giaobscdonvi.donvigiao ";
            sql += "and bsc.donvinhan = giaobscdonvi.donvinhan ";
            sql += "and bsc.thang = giaobscdonvi.thang ";
            sql += "and bsc.nam = giaobscdonvi.nam ";
            sql += "and giaobscdonvi.trangthainhan = 0 ";

            sql += "group by bsc.thang, bsc.nam, bsc.donvinhan, dv.donvi_ten, bsc.donvigiao";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return dtResult;
        }

        private static DataTable getPhanHoiThamDinh(int donvithamdinh)
        {
            Connection cn = new Connection();
            DataTable dtResult = new DataTable();
            string sql = "select bsc.thang, bsc.nam, bsc.donvigiao, bsc.donvinhan, dv.donvi_ten, bsc.donvithamdinh ";
            sql += "from bsc_donvi bsc, donvi dv ";
            sql += "where bsc.phanhoi_thamdinh_daxuly = 0 ";
            sql += "and bsc.donvinhan = dv.donvi_id ";
            sql += "and bsc.donvithamdinh = '" + donvithamdinh + "'";
            sql += "group by bsc.thang, bsc.nam, bsc.donvigiao, bsc.donvinhan, dv.donvi_ten, bsc.donvithamdinh";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtResult;
        }

        private string getTenChucVu(int chucvuID)
        {
            string szResult = "";
            Connection cn = new Connection();
            DataTable dtResult = new DataTable();
            string sql = "select chucvu_ten from chucvu where chucvu_id = '" + chucvuID + "'";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtResult.Rows.Count > 0)
            {
                szResult = dtResult.Rows[0][0].ToString();
            }

            return szResult;
        }

        protected override void OnInit(EventArgs e)
        {
            Nhanvien tmpNhanvien = new Nhanvien();
            DataTable dtQuyen = new DataTable();
            int nNumRole = 0;
            //string szChucVu = "";
            //nhanvien = Session.GetCurrentUser();
            //quyenHeThong = Session.GetRole();

            tmpNhanvien = (Nhanvien)Session["nhanvien"];
            quyenToanHeThong = (List<int>)Session["quyenhethong"];

            if (tmpNhanvien == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            userName = tmpNhanvien.nhanvien_hoten;
            nNumRole = tmpNhanvien.nhanvien_chucvu_id.Length;
            userID = tmpNhanvien.nhanvien_id;
            userDonVi = tmpNhanvien.nhanvien_donvi_id;

            dtPhanHoiGiao = getPhanHoiGiao();
            dtPhanHoiThamDinh = getPhanHoiThamDinh(tmpNhanvien.nhanvien_donvi_id);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ////if (!IsPostBack) {
            //    Nhanvien nhanvien = new Nhanvien();
            //    DataTable dtQuyen = new DataTable();
            //    int nNumRole = 0;
            //    //string szChucVu = "";
            //    //nhanvien = Session.GetCurrentUser();
            //    //quyenHeThong = Session.GetRole();

            //    nhanvien = (Nhanvien)Session["nhanvien"];
            //    quyenHeThong = (List<int>)Session["quyenhethong"];

            //    if (nhanvien == null)
            //    {
            //        Response.Redirect("~/Login.aspx");
            //    }

            //    userName = nhanvien.nhanvien_hoten;
            //    nNumRole = nhanvien.nhanvien_chucvu_id.Length;
            //    userID = nhanvien.nhanvien_id;
            //    userDonVi = nhanvien.nhanvien_donvi_id;

            //    dtPhanHoiGiao = getPhanHoiGiao();
            //    dtPhanHoiThamDinh = getPhanHoiThamDinh(nhanvien.nhanvien_donvi_id);
            ////}
        }
    }
}