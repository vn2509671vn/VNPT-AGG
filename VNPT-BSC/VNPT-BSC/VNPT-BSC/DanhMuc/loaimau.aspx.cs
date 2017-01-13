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
    public partial class loaimau : System.Web.UI.Page
    {
        Connection dvt = new Connection();
        public DataTable dtdvt;

        private DataTable getdvtList()
        {
            string sqlkpi = "select * from loaimaubsc";
            DataTable dtdvt = new DataTable();
            try
            {
                dtdvt = dvt.XemDL(sqlkpi);
            }
            catch (Exception ex)
            {
                dtdvt = null;
            }
            return dtdvt;
        }
        [WebMethod]
        public static bool SaveData(string dvt_tenAprove, string dvt_motaAprove)
        {
            Connection donvitinh = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into loaimaubsc(loai_ten,loai_mota) values(N'" + dvt_tenAprove + "',N'" + dvt_motaAprove + "')";
                donvitinh.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(string dvt_ten_suaAprove, string dvt_mota_suaAprove, int dvt_id_suaAprove)
        {
            Connection dvt_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update loaimaubsc set loai_ten = N'" + dvt_ten_suaAprove + "',loai_mota = N'" + dvt_mota_suaAprove + "' where loai_id = '" + dvt_id_suaAprove + "'";
                dvt_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool DeleteData(int dvt_idAprove)
        {
            Connection dvt_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete loaimaubsc where loai_id = '" + dvt_idAprove + "'";
                dvt_delete.ThucThiDL(sqldeleteData);
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
            this.Title = "Quản lý loại mẫu BSC";
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

                    dtdvt = new DataTable();
                    dtdvt = getdvtList();
                }
                catch
                {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

            }
        }
    }
}