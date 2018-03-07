using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Globalization;

namespace VNPT_BSC.TinhLuong
{
    public partial class CapNhatNhanSu : System.Web.UI.Page
    {
        public string id_nhansu;
        public DataTable userInfo = new DataTable();

        public static DataTable getUser(string id_nhansu) {
            DataTable tmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select *, CONVERT(date,ngaykyhd,101) as ngayvaonganh from qlns_nhanvien where id = '" + id_nhansu + "' ";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return tmp;
        }

        [WebMethod]
        public static Dictionary<String, String> luuThongTin(string cmnd, string manv, string hoten, string ngaykyhd, string id_nhansu)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string newDate = "";
            DateTime dt = DateTime.Parse(ngaykyhd);
            newDate = dt.ToString("dd/MM/yyyy");
            string sql = "update qlns_nhanvien set cmnd = '" + cmnd + "', ma_nhanvien = '" + manv + "', ten_nhanvien = N'" + hoten + "', ngaykyhd = '" + newDate + "' where id = '"+id_nhansu+"'";
            try
            {
                cn.ThucThiDL(sql);
                dicResult.Add("status", "ok");
                dicResult.Add("message", "Cập nhật nhân sự thành công!");
            }
            catch {
                dicResult.Add("status", "error");
                dicResult.Add("message", "Cập nhật nhân sự không thành công! Vui lòng thử lại.");
            }
            return dicResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Cập nhật nhân sự";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                //nhanvien = Session.GetCurrentUser();
                nhanvien = (Nhanvien)Session["nhanvien"];

                id_nhansu = Request.QueryString["user"];

                if (nhanvien.nhanvien_id != 196)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

                userInfo = getUser(id_nhansu);
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}