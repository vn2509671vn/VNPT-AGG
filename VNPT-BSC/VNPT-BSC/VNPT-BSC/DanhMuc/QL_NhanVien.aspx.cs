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
            + "a.nhanvien_email,c.chucvu_ten,d.chucdanh_ten,a.nhanvien_taikhoan,a.nhanvien_matkhau"
            + " FROM nhanvien a, donvi b, chucvu c, chucdanh d "
            + "WHERE a.nhanvien_donvi = b.donvi_id and c.chucvu_id = a.nhanvien_chucvu and a.nhanvien_chucdanh = d.chucdanh_id ";
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
        public static bool SaveData(string nv_tenA, string nv_ngaysinhA, int nv_donviA, string nv_dantocA, string nv_tongiaoA, string nv_trinhdoA, string nv_gioitinhA, string nv_noisinhA, string nv_quequanA, string nv_diachiA, string nv_cmndA, string nv_ngaycapA, string nv_noicapA, string nv_doanvienA, string nv_dangvienA, string nv_ngayvaodangA, string nv_ngayvaonganhA, string nv_didongA, string nv_emailA, int nv_chucvuA, int nv_chucdanhA, string nv_taikhoanA, string nv_matkhauA)
        {
            Connection nv = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {


                sqlInsertNewData = "insert into nhanvien(nhanvien_hoten,nhanvien_ngaysinh, nhanvien_donvi,nhanvien_dantoc,nhanvien_tongiao,nhanvien_trinhdo,nhanvien_gioitinh,nhanvien_noisinh,nhanvien_quequan,nhanvien_diachi,nhanvien_cmnd,nhanvien_ngaycapcmnd,nhanvien_noicapcmnd,nhanvien_doanvien,nhanvien_dangvien,nhanvien_ngayvaodang,nhanvien_ngayvaonganh,nhanvien_didong,nhanvien_email,nhanvien_chucvu,nhanvien_chucdanh,nhanvien_taikhoan,nhanvien_matkhau) values(N'" + nv_tenA + "',N'" + nv_ngaysinhA + "', '" + nv_donviA + "',N'" + nv_dantocA + "',N'" + nv_tongiaoA + "',N'" + nv_trinhdoA + "',N'" + nv_gioitinhA + "',N'" + nv_noisinhA + "',N'" + nv_quequanA + "',N'" + nv_diachiA + "',N'" + nv_cmndA + "',N'" + nv_ngaycapA + "',N'" + nv_noicapA + "',N'" + nv_doanvienA + "',N'" + nv_dangvienA + "',N'" + nv_ngayvaodangA + "',N'" + nv_ngayvaonganhA + "',N'" + nv_didongA + "',N'" + nv_emailA + "',N'" + nv_chucvuA + "',N'" + nv_chucdanhA + "',N'" + nv_taikhoanA + "',N'" + nv_matkhauA + "')";
                try
                {
                    nv.ThucThiDL(sqlInsertNewData);
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