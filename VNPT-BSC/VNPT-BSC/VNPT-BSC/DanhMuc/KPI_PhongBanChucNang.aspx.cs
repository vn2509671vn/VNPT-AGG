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

namespace VNPT_BSC.DanhMuc
{
    public partial class KPI_PhongBanChucNang : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public DataTable dtkpi;
        public static DataTable dtnhanvien_kpi = new DataTable();
        public static DataTable dtkpi_kpo = new DataTable();
        public static DataTable dtnhom_kpi = new DataTable();

        private DataTable getkpiList()
        {
            Nhanvien nhanvien = Session.GetCurrentUser();
            string sqlkpi = "select a.kpi_id,a.kpi_ten,a.kpi_mota,a.kpi_ngaytao,c.nhanvien_hoten,b.kpo_ten,b.kpo_id,c.nhanvien_id, a.kpi_ma from kpi a,kpo b,nhanvien c where a.kpi_nguoitao = c.nhanvien_id and a.kpi_thuoc_kpo = b.kpo_id and a.kpi_nguoitao = '" + nhanvien.nhanvien_id + "' and a.hienthi = 1";
            DataTable dtkpi = new DataTable();
            try
            {
                dtkpi = cn.XemDL(sqlkpi);
            }
            catch (Exception ex)
            {
                dtkpi = null;
            }
            return dtkpi;
        }

        [WebMethod]
        public static bool SaveData(string kpi_tenAprove, string kpi_tenMa, string kpi_motaAprove, string kpi_ngayAprove, int kpi_kpoAprove)
        {
            Page objp = new Page();
            Nhanvien nhanvien = objp.Session.GetCurrentUser();
            Connection kpi = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into kpi(kpi_ten, kpi_ma, kpi_mota, kpi_ngaytao,kpi_nguoitao,kpi_thuoc_kpo, hienthi) values(N'" + kpi_tenAprove + "', '" + kpi_tenMa + "', N'" + kpi_motaAprove + "', '" + kpi_ngayAprove + "','" + nhanvien.nhanvien_id + "','" + kpi_kpoAprove + "', 1)";
                kpi.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(string kpi_ten_suaAprove, string kpi_ma_suaAprove, string kpi_mota_suaAprove, string kpi_kpo_suaAprove, int kpi_id_suaAprove)
        {
            Page objp = new Page();
            Nhanvien nhanvien = objp.Session.GetCurrentUser();
            Connection kpi_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update kpi set kpi_ten = N'" + kpi_ten_suaAprove + "', kpi_ma = '" + kpi_ma_suaAprove + "', kpi_mota = N'" + kpi_mota_suaAprove + "', kpi_nguoitao = '" + nhanvien.nhanvien_id + "', kpi_thuoc_kpo = '" + kpi_kpo_suaAprove + "' where kpi_id = '" + kpi_id_suaAprove + "'";
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
        public static bool DeleteData(int kpi_id_xoaAprove)
        {
            Connection kpi_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "update kpi set hienthi = 0 where kpi_id = '" + kpi_id_xoaAprove + "'";
                kpi_delete.ThucThiDL(sqldeleteData);
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
            this.Title = "Quản lý KPI";
            if (!IsPostBack)
            {
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();

                    // Khai báo các biến cho việc kiểm tra quyền
                    List<int> quyenHeThong = new List<int>();
                    bool nFindResultAdmin = false;
                    bool nFindResultBSCDonVi = false;
                    bool nFindResultBSCNhanVien = false;
                    quyenHeThong = Session.GetRole();

                    /*Kiểm tra nếu không có quyền admin, bsc đơn vị, bsc nhân viên (id của quyền là 1) thì đẩy ra trang đăng nhập*/
                    nFindResultAdmin = quyenHeThong.Contains(1);
                    nFindResultBSCDonVi = quyenHeThong.Contains(2);
                    nFindResultBSCNhanVien = quyenHeThong.Contains(3);

                    if (nhanvien == null || !nFindResultAdmin && !nFindResultBSCDonVi && !nFindResultBSCNhanVien)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    string sqlnhanvien = "select * from nhanvien";
                    string sqlkpo = "select * from kpo where kpo_id in (2015, 2016, 2017)";

                    dtkpi = new DataTable();
                    dtkpi = getkpiList();
                    dtnhanvien_kpi = cn.XemDL(sqlnhanvien);
                    dtkpi_kpo = cn.XemDL(sqlkpo);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

            }
        }
    }
}