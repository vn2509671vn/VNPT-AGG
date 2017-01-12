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

namespace VNPT_BSC.Admin
{
    public partial class loaiquyen : System.Web.UI.Page
    {

        Connection cn = new Connection();
        public DataTable dtquyen;
        public static DataTable dtnhomquyen = new DataTable();

        private DataTable getquyenList()
        {
          
            string sqlkpo = "select a.quyen_id,a.quyen_maquyen,a.quyen_ten,a.quyen_mota,b.loaiquyen_ten from quyen a,loaiquyen b where a.quyen_loaiquyen = b.loaiquyen_id";
            DataTable dtquyen = new DataTable();
            try
            {
                dtquyen = cn.XemDL(sqlkpo);
            }
            catch (Exception ex)
            {
                dtquyen = null;
            }
            return dtquyen;
        }

        [WebMethod]
        public static bool SaveData(string quyen_maA, string quyen_tenA, string quyen_motaA, int quyen_nhomA)
        {
            Connection quyen = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into quyen(quyen_maquyen,quyen_ten, quyen_mota,quyen_loaiquyen) values(N'" + quyen_maA + "',N'" + quyen_tenA + "', N'" + quyen_motaA + "','" + quyen_nhomA + "')";
                quyen.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }
        [WebMethod]
        public static bool EditData(string quyen_ma_suaA, string quyen_ten_suaA, string quyen_mota_suaA, string quyen_nhom_suaA, int quyen_id_suaA)
        {
            
            Connection quyen_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update quyen set quyen_maquyen = N'" + quyen_ma_suaA + "',quyen_ten = N'" + quyen_ten_suaA + "', quyen_mota = N'" + quyen_mota_suaA + "', quyen_loaiquyen = '" + quyen_nhom_suaA + "' where quyen_id = '" + quyen_id_suaA + "'";
                quyen_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool DeleteData(int quyen_idAprove)
        {
            Connection quyen_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete quyen where quyen_id = '" + quyen_idAprove + "'";
                quyen_delete.ThucThiDL(sqldeleteData);
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
            this.Title = "Quản lý loại quyền";

            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();
                dtquyen = new DataTable();
                dtquyen = getquyenList();
                string sqlnhomquyen = "select * from loaiquyen";
                dtnhomquyen = cn.XemDL(sqlnhomquyen);

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
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}