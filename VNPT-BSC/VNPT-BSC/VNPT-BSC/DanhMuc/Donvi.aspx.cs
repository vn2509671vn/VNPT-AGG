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
    public partial class Donvi : System.Web.UI.Page
    {
        Connection dv = new Connection();
        public DataTable dtdonvi;

        private DataTable getdonviList()
        {
            string sqlBSC = "select donvi_id,donvi_ten,donvi_mota,donvi_ma from donvi";
            DataTable dtdonvi = new DataTable();
            try
            {
                dtdonvi = dv.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                dtdonvi = null;
            }
            return dtdonvi;
        }

        [WebMethod]
        public static bool SaveData(string dv_tenAprove, string dv_motaAprove, string dv_maAprove)
        {
            Connection donvi = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into donvi(donvi_ten,donvi_mota, donvi_ma) values(N'" + dv_tenAprove + "',N'" + dv_motaAprove + "', '" + dv_maAprove + "')";
                donvi.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(string dv_ten_suaAprove, string dv_mota_suaAprove, string dv_ma_suaAprove, int dv_id_suaAprove)
        {
            Connection donvi_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update donvi set donvi_ten = N'" + dv_ten_suaAprove + "',donvi_mota = N'" + dv_mota_suaAprove + "', donvi_ma = N'" + dv_ma_suaAprove + "' where donvi_id = '" + dv_id_suaAprove + "'";
                donvi_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool DeleteData(int dv_id_xoaAprove)
        {
            Connection donvi_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete donvi where donvi_id = '" + dv_id_xoaAprove + "'";
                donvi_delete.ThucThiDL(sqldeleteData);
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
            this.Title = "Quản lý đơn vị";
            //if (!IsPostBack)
            //{
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    //nhanvien = Session.GetCurrentUser();
                    nhanvien = (Nhanvien)Session["nhanvien"];

                    // Khai báo các biến cho việc kiểm tra quyền
                    List<int> quyenHeThong = new List<int>();
                    bool nFindResult = false;
                    quyenHeThong = (List<int>)Session["quyenhethong"];

                    /*Kiểm tra nếu không có quyền admin (id của quyền là 1) thì đẩy ra trang đăng nhập*/
                    nFindResult = quyenHeThong.Contains(1);

                    if (nhanvien == null || !nFindResult)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    /*Get list BSC*/
                    dtdonvi = new DataTable();
                    dtdonvi = getdonviList();
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            //}

        }
    }
}