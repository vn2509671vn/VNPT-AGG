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
    public partial class MauThueBao_DHNV : System.Web.UI.Page
    {
        [WebMethod]
        public static string loadData(int thang, int nam, int trangthai)
        {
            Connection cn = new Connection();
            DataTable gridData = new DataTable();
            DataTable gridDV = new DataTable();
            string timekey = nam.ToString() + thang.ToString("00");

            string sql = "select * from thuebao_tratruoc where donvi_id_pt = 12 ";
            if (trangthai == 1) {
                sql += "and donvi_id = 0 ";
            }
            else if (trangthai == 2)
            {
                sql += "and donvi_id <> 0 ";
            }
            
            sql += "and left(timekey,6) = '" + timekey + "' ";
            sql += "order by timekey_pt, ma_nv_pt";

            string sqlDV = "select * from qlns_donvi where id not in (1,2,13,14,15,16,18) order by ma_donvi";

            try
            {
                gridData = cn.XemDL(sql);
                gridDV = cn.XemDL(sqlDV);

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
            for (int i = 0; i < gridDV.Rows.Count; i++)
            {
                arrOutput += "<li>" + gridDV.Rows[i]["map_id_donvi"] + " - " + gridDV.Rows[i]["ten_donvi"] + "</li>";
            }
            arrOutput += "</ul>";
            arrOutput += "</caption>";

            arrOutput += "<thead>";
            arrOutput += "<tr>";
            arrOutput += "<th class='text-center'>STT</th>";
            arrOutput += "<th class='text-center'>Mã đơn vị nhận</th>";
            arrOutput += "<th class='text-center'>Thuê bao</th>";
            arrOutput += "<th class='text-center'>Tên KH</th>";
            arrOutput += "<th class='text-center'>Ngày phát triển</th>";
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
                    
                    arrOutput += "<tr>";
                    arrOutput += "<td style='text-align: center'>" + (n + 1) + "</td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[n]["donvi_id"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[n]["so_dt"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[n]["ten_kh"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[n]["timekey_pt"].ToString() + "</strong></td>";
                    arrOutput += "</tr>";
                }
            }
            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Danh sách thuê bao ĐHNV chưa phân";
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
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
        }
    }
}