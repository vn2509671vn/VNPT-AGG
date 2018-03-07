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

namespace VNPT_BSC.BSC
{
    public partial class MauThueBao_PBH : System.Web.UI.Page
    {
        public static string donvi;

        public static DataTable getListDonHang(int donvi, string timekey, int trangthai)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select a.*, c.tennv from thuebao_tratruoc a, qlns_donvi b, [OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien] c where a.donvi_id = b.map_id_donvi and c.ma_nv = a.ma_nv_pt ";
            if (trangthai == 1)
            {
                sql += "and a.ma_nv = '1' ";
            }
            else if (trangthai == 2) {
                sql += "and a.ma_nv <> '1' ";
            }
            sql += "and left(a.timekey,6) = '" + timekey + "' and b.map_id_donvi = '" + donvi + "' order by a.timekey_pt";
            

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
            string sql = "select * from [OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] where nhom_id = 2 and donvi_id = '" + donvi + "' order by ma_nv";
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
        public static string loadData(int thang, int nam, int donvi, int trangthai)
        {
            Connection cn = new Connection();
            DataTable gridData = new DataTable();
            DataTable gridNV = new DataTable();
            string timekey = nam.ToString() + thang.ToString("00");

            try
            {
                gridData = getListDonHang(donvi, timekey, trangthai);
                gridNV = getListNhanSu(donvi);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            string arrOutput = "";
            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-data' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";

            // Danh mục đơn vị
            arrOutput += "<caption>";
            arrOutput += "<ul class='text-left'>";
            for (int i = 0; i < gridNV.Rows.Count; i++)
            {
                arrOutput += "<li class = 'col-md-6 col-sm-6'>" + gridNV.Rows[i]["ma_nv"] + " - " + gridNV.Rows[i]["tennv"] + "</li>";
            }
            arrOutput += "</ul>";
            arrOutput += "</caption>";

            arrOutput += "<thead>";
            arrOutput += "<tr>";
            arrOutput += "<th class='text-center'>STT</th>";
            arrOutput += "<th class='text-center'>Mã nhân viên nhận</th>";
            arrOutput += "<th class='text-center'>Thuê bao</th>";
            arrOutput += "<th class='text-center'>Tên KH</th>";
            arrOutput += "<th class='text-center'>Nạp Thẻ</th>";
            arrOutput += "<th class='text-center'>Gói</th>";
            arrOutput += "<th class='text-center'>NV PTriển</th>";
            arrOutput += "<th class='text-center'>Mã NV PTriển</th>";
            arrOutput += "<th class='text-center'>Ngày PTriển</th>";
            arrOutput += "<th class='text-center'>Nơi PTriển</th>";
            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                //
            }
            else
            {
                for (int n = 0; n < gridData.Rows.Count; n++)
                {
                    string szDVPT = "ĐHNV";
                    if(gridData.Rows[n]["donvi_id_pt"].ToString() != "12"){
                        szDVPT = "PBH";
                    }
                    arrOutput += "<tr>";
                    arrOutput += "<td style='text-align: center'>" + (n + 1) + "</td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[n]["ma_nv"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[n]["so_dt"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[n]["ten_kh"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[n]["dt_napthe"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[n]["ten_goi_vas"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[n]["tennv"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[n]["ma_nv_pt"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[n]["timekey_pt"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + szDVPT + "</strong></td>";
                    arrOutput += "</tr>";
                }
            }
            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        private string getDonViVmos(int id_donvi)
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select map_id_donvi from qlns_donvi where id = '" + id_donvi + "'";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtResult.Rows.Count > 0)
            {
                return dtResult.Rows[0][0].ToString();
            }

            return "0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Danh sách thuê bao PBH chưa phân";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                //nhanvien = Session.GetCurrentUser();
                nhanvien = (Nhanvien)Session["nhanvien"];

                //if (nhanvien == null || nhanvien.nhanvien_id != 164)
                if (nhanvien == null)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../index.aspx';</script>");
                }

                donvi = getDonViVmos(nhanvien.nhanvien_donvi_id);
                if (donvi == "") {
                    donvi = "0";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
        }
    }
}