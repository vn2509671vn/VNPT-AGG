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
    public partial class XuLy_DonHang : System.Web.UI.Page
    {
        public DataTable dtDonHang = new DataTable();

        public static DataTable getListDonHang(int id_nhanvien)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from dai_108_donhang where nhanvien_tiepnhan = '" + id_nhanvien + "' and trangthai_donhang = 0 order by ma_donhang desc";
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
        public static bool SaveData(string noidung_xuly, int[] arrDonHang_ID)
        {
            Connection cn = new Connection();
            Message msg = new Message();
            bool output = false;

            string sqlInsertNewData = "";
            try
            {
                for (int i = 0; i < arrDonHang_ID.Length; i++)
                {
                    sqlInsertNewData = "update dai_108_donhang set ghichu_giaohang = '" + noidung_xuly + "', trangthai_donhang = 1 where ma_donhang = '" + arrDonHang_ID[i] + "'";
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

            return output;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Xử lý đơn hàng";

            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = (Nhanvien)Session["nhanvien"];

                if (nhanvien == null)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này.!!!')</script>");
                    Response.Write("<script>window.location.href='../index.aspx';</script>");
                }
                dtDonHang = getListDonHang(nhanvien.nhanvien_id);
            }
            catch
            {
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
        }
    }
}