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
            "SELECT a.nhanvien_id,a.nhanvien_hoten,a.nhanvien_ngaysinh,b.donvi_ten,b.donvi_id,a.nhanvien_dantoc,a.nhanvien_tongiao,a.nhanvien_trinhdo,a.nhanvien_gioitinh,a.nhanvien_noisinh, "
            + "a.nhanvien_quequan,a.nhanvien_diachi,a.nhanvien_cmnd,a.nhanvien_ngaycapcmnd,a.nhanvien_noicapcmnd,a.nhanvien_doanvien,a.nhanvien_dangvien,a.nhanvien_ngayvaodang,a.nhanvien_ngayvaonganh,a.nhanvien_didong,"
            + "a.nhanvien_email,d.chucdanh_ten,d.chucdanh_id,a.nhanvien_taikhoan,a.nhanvien_matkhau"
            + " FROM nhanvien a, donvi b, chucdanh d "
            + "WHERE a.nhanvien_donvi = b.donvi_id and  a.nhanvien_chucdanh = d.chucdanh_id ";
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
        public static bool SaveData(string nv_tenA, string nv_ngaysinhA, int nv_donviA, string nv_dantocA, string nv_tongiaoA, string nv_trinhdoA, string nv_gioitinhA, string nv_noisinhA, string nv_quequanA, string nv_diachiA, string nv_cmndA, string nv_ngaycapA, string nv_noicapA, string nv_doanvienA, string nv_dangvienA, string nv_ngayvaodangA, string nv_ngayvaonganhA, string nv_didongA, string nv_emailA, int nv_chucdanhA, string nv_taikhoanA, string nv_matkhauA)
        {
            Connection nv = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {

                sqlInsertNewData = "insert into nhanvien(nhanvien_hoten,nhanvien_ngaysinh, nhanvien_donvi,nhanvien_dantoc,nhanvien_tongiao,nhanvien_trinhdo,nhanvien_gioitinh,nhanvien_noisinh,nhanvien_quequan,nhanvien_diachi,nhanvien_cmnd,nhanvien_ngaycapcmnd,nhanvien_noicapcmnd,nhanvien_doanvien,nhanvien_dangvien,nhanvien_ngayvaodang,nhanvien_ngayvaonganh,nhanvien_didong,nhanvien_email,nhanvien_chucdanh,nhanvien_taikhoan,nhanvien_matkhau) values(N'" + nv_tenA + "',N'" + nv_ngaysinhA + "', '" + nv_donviA + "',N'" + nv_dantocA + "',N'" + nv_tongiaoA + "',N'" + nv_trinhdoA + "',N'" + nv_gioitinhA + "',N'" + nv_noisinhA + "',N'" + nv_quequanA + "',N'" + nv_diachiA + "',N'" + nv_cmndA + "',N'" + nv_ngaycapA + "',N'" + nv_noicapA + "',N'" + nv_doanvienA + "',N'" + nv_dangvienA + "',N'" + nv_ngayvaodangA + "',N'" + nv_ngayvaonganhA + "',N'" + nv_didongA + "',N'" + nv_emailA + "',N'" + nv_chucdanhA + "',N'" + nv_taikhoanA + "',N'" + nv_matkhauA + "')";
                nv.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(string nv_ten_suaA,  string nv_chucdanh_suaA, string nv_donvi_suaA, string nv_datengaysinh_suaA, string nv_dang_suaA, string nv_ngaydang_suaA, string nv_didong_suaA,
            string nv_email_suaA, string nv_diachi_suaA, string nv_dantoc_suaA, string nv_tongiao_suaA, string nv_trinhdo_suaA, string nv_gioitinh_suaA, string nv_datenganh_suaA, string nv_doan_suaA, string nv_cmnd_suaA,
            string nv_ngaycmnd_suaA, string nv_noicmnd_suaA, string nv_noisinh_suaA, string nv_quequan_suaA, int nv_id_suaA)
        {
            Page objp = new Page();
            Nhanvien nhanvien = objp.Session.GetCurrentUser();
            Connection kpi_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update nhanvien set nhanvien_hoten = N'" + nv_ten_suaA + "', nhanvien_chucdanh = '" + nv_chucdanh_suaA + "', " +
                                " nhanvien_donvi = '" + nv_donvi_suaA + "',nhanvien_ngaysinh = '" + nv_datengaysinh_suaA + "',nhanvien_dangvien = '" + nv_dang_suaA + "',nhanvien_ngayvaodang = '" + nv_ngaydang_suaA + "', " +
                                " nhanvien_didong = '" + nv_didong_suaA + "', nhanvien_email = '" + nv_email_suaA + "', nhanvien_diachi = '" + nv_diachi_suaA + "', nhanvien_dantoc = '" + nv_dantoc_suaA + "', " +
                                " nhanvien_tongiao = '" + nv_tongiao_suaA + "', nhanvien_trinhdo = '" + nv_trinhdo_suaA + "', nhanvien_gioitinh = '" + nv_gioitinh_suaA + "', nhanvien_ngayvaonganh = '" + nv_datenganh_suaA + "', " +
                                " nhanvien_doanvien = '" + nv_doan_suaA + "', nhanvien_cmnd = '" + nv_cmnd_suaA + "', nhanvien_ngaycapcmnd = '" + nv_ngaycmnd_suaA + "', nhanvien_noicapcmnd = '" + nv_noicmnd_suaA + "', " +
                                " nhanvien_noisinh = '" + nv_noisinh_suaA + "', nhanvien_quequan = '" + nv_quequan_suaA + "'  " +
                                " where nhanvien_id = '" + nv_id_suaA + "'";
                kpi_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool DeleteData(int nv_id_xoaAprove)
        {
            Connection nv_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete nhanvien where nhanvien_id = '" + nv_id_xoaAprove + "'";
                nv_delete.ThucThiDL(sqldeleteData);
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
            this.Title = "Quản lý nhân viên";
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
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();

                    // Khai báo các biến cho việc kiểm tra quyền
                    List<int> quyenHeThong = new List<int>();
                    bool nFindResult = false;
                    quyenHeThong = Session.GetRole();

                    /*Kiểm tra nếu không có quyền admin (id của quyền là 1) thì đẩy ra trang đăng nhập*/
                    nFindResult = quyenHeThong.Contains(1);

                    if (nhanvien == null || !nFindResult)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    /*Get list BSC*/
                    dtnhanvien = new DataTable();
                    dtnhanvien = getnhanvienList();

                    dtdonvi_nv = nv.XemDL(sqldonvi);
                    dtchucvu_nv = nv.XemDL(sqlchucvu);
                    dtchucdanh_nv = nv.XemDL(sqlchucdanh);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

            }
        }
    }
}