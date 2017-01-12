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

namespace VNPT_BSC.DanhMuc
{
    public partial class QL_NhanVien : System.Web.UI.Page
    {
        Connection nv = new Connection();
        public static DataTable dtnhanvien;
        public static DataTable dtdonvi_nv = new DataTable();
        public static DataTable dtchucvu_nv = new DataTable();
        public static DataTable dtchucdanh_nv = new DataTable();
        private DataTable getnhanvienList()
        {
            string sqlBSC =
            "SELECT a.nhanvien_id,a.nhanvien_hoten,a.nhanvien_ngaysinh,b.donvi_ten,a.nhanvien_dantoc,a.nhanvien_tongiao,a.nhanvien_trinhdo,a.nhanvien_gioitinh,a.nhanvien_noisinh, "
            + "a.nhanvien_quequan,a.nhanvien_diachi,a.nhanvien_cmnd,a.nhanvien_ngaycapcmnd,a.nhanvien_noicapcmnd,a.nhanvien_doanvien,a.nhanvien_dangvien,a.nhanvien_ngayvaodang,a.nhanvien_ngayvaonganh,a.nhanvien_didong,"
            + "a.nhanvien_email,c.chucvu_ten,a.nhanvien_chucdanh,a.nhanvien_taikhoan,a.nhanvien_matkhau"
            + " FROM nhanvien a, donvi b, chucvu c "
            + "WHERE a.nhanvien_donvi = b.donvi_id and c.chucvu_id = a.nhanvien_chucvu ";
            dtnhanvien = new DataTable();
            try
            {
                dtnhanvien = nv.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtnhanvien;
        }

        [WebMethod]
        public static bool SaveData(string nv_tenA, string cd_motaAprove, string cd_maAprove)
        {
            Connection chucdanh = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {


                sqlInsertNewData = "insert into chucdanh(chucdanh_ten,chucdanh_mota, chucdanh_ma) values(N'" + nv_tenA + "',N'" + cd_motaAprove + "', N'" + cd_maAprove + "')";
                try
                {
                    chucdanh.ThucThiDL(sqlInsertNewData);
                }
                catch
                {
                    output = false;
                }
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*Get list BSC*/
                dtnhanvien = new DataTable();
                dtnhanvien = getnhanvienList();
                try
                {
                    string sqldonvi = "select * from donvi";
                    string sqlchucvu = "select * from chucvu";
                    string sqlchucdanh = "select * from chucdanh";

                    dtdonvi_nv = nv.XemDL(sqldonvi);
                    dtchucvu_nv = nv.XemDL(sqlchucvu);
                    dtchucdanh_nv = nv.XemDL(sqlchucdanh);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}