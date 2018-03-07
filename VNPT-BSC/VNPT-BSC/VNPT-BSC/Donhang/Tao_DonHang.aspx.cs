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

namespace VNPT_BSC.Donhang
{
    public partial class Tao_DonHang : System.Web.UI.Page
    {
        public DataTable dtDonvi = new DataTable();
        public Nhanvien nguoigiao = new Nhanvien();

        private DataTable getDonVi()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_donvi where id not in (1,2,13,14,15,16,18) order by id asc";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmp;
        }

        public static DataTable dtSDTByMaDV(int szMaDV)
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select nv.nhanvien_didong from nhanvien nv, qlns_donvi dv, nhanvien_chucvu cv where nv.nhanvien_donvi = dv.id and dv.id = '" + szMaDV + "' and nv.nhanvien_id = cv.nhanvien_id and cv.chucvu_id in (3,5)";
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

        [WebMethod]
        public static Dictionary<string, string> luuThongTinChung(string hoten, string sdt, string diachi, string ghichu, int pbh_tiepnhan, int nguoitao)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            Connection cn = new Connection();
            DataTable tmp = new DataTable();

            string sql = "insert into dai_108_donhang(ten_khachhang, sodienthoai, diachi, ghichu, ngaytao, pbh_tiepnhan, nguoitao) ";
            sql += "values(N'" + hoten + "', N'" + sdt + "', N'" + diachi + "', N'" + ghichu + "', GETDATE(), '" + pbh_tiepnhan + "', '" + nguoitao + "')";
            try
            {
                cn.ThucThiDL(sql);
                dicResult.Add("status", "ok");
                dicResult.Add("message", "Tạo đơn hàng thành công!");

                // Send sms thông báo tới các đơn vị
                Message msg = new Message();
                DataTable dtSDT = dtSDTByMaDV(pbh_tiepnhan);
                for (int i = 0; i < dtSDT.Rows.Count; i++)
                {
                    string szSDT = dtSDT.Rows[i]["nhanvien_didong"].ToString().Trim();
                    string szContent = "Ban vua nhan duoc mot don hang tu Dai 108. Vui long vao kiem tra!!!";
                    msg.SendSMS_VTTP(szSDT, szContent);
                }
            }
            catch
            {
                dicResult.Add("status", "error");
                dicResult.Add("message", "Tạo đơn hàng thất bại!");
            }
            return dicResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Tạo đơn hàng";
            try
            {
                nguoigiao = (Nhanvien)Session["nhanvien"];

                if (nguoigiao == null || nguoigiao.nhanvien_donvi_id != 16)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../index.aspx';</script>");
                }

                dtDonvi = getDonVi();
            }
            catch
            {
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
            
        }
    }
}