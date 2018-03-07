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
    public partial class PhanPhoi_DonHang : System.Web.UI.Page
    {
        public DataTable dtDonHang = new DataTable();
        public DataTable dtNhanSu = new DataTable();

        public static DataTable getListDonHang(int donvi)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from dai_108_donhang where pbh_tiepnhan = '" + donvi + "' and nhanvien_tiepnhan is null order by ma_donhang desc";
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

        public static DataTable getListNhanSu(int donvi)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from nhanvien where nhanvien_chucdanh not in (1,2) and nhanvien_donvi = '" + donvi + "' order by nhanvien_manv desc";
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

        [WebMethod]
        public static bool SaveData(int nhansu, int[] arrDonHang_ID)
        {
            Connection cn = new Connection();
            Message msg = new Message();
            bool output = false;

            string sqlInsertNewData = "";
            try
            {
                for (int i = 0; i < arrDonHang_ID.Length; i++)
                {
                    sqlInsertNewData = "update dai_108_donhang set nhanvien_tiepnhan = '" + nhansu + "', ngay_phan_donhang = GETDATE(), trangthai_donhang = 0 where ma_donhang = '" + arrDonHang_ID[i] + "'";
                    try
                    {
                        cn.ThucThiDL(sqlInsertNewData);
                        output = true;
                    }
                    catch (Exception ex)
                    {
                        output = false;
                    }
                }
            }
            catch
            {
                output = false;
            }

            if (output) {
                msg.SendSMS_ByIDNV(nhansu, "Ban vua nhan duoc don hang tu GD PBH, vui long lien he voi khach hang!!!");
            }
            return output;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Phân phối đơn hàng";
            
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = (Nhanvien)Session["nhanvien"];

                // Khai báo các biến cho việc kiểm tra quyền
                List<int> quyenHeThong = new List<int>();
                bool nFindResult = false;
                //quyenHeThong = Session.GetRole();
                quyenHeThong = (List<int>)Session["quyenhethong"];

                /*Kiểm tra nếu không có quyền giao bsc đơn vị (id của quyền là 2) thì đẩy ra trang đăng nhập*/
                nFindResult = quyenHeThong.Contains(3);

                if (nhanvien == null || !nFindResult)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này.!!!')</script>");
                    Response.Write("<script>window.location.href='../index.aspx';</script>");
                }
                dtDonHang = getListDonHang(nhanvien.nhanvien_donvi_id);
                dtNhanSu = getListNhanSu(nhanvien.nhanvien_donvi_id);
            }
            catch
            {
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
        }
    }
}