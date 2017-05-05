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
    public partial class DanhSachBSCTatCaNV : System.Web.UI.Page
    {
        public static DataTable dsNhanVienNhanBSC(int thang, int nam) {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select nv.nhanvien_id, nv.nhanvien_manv, nv.nhanvien_hoten, dv.donvi_ten, bsc.trangthaigiao, bsc.trangthainhan ";
            sql += "from giaobscnhanvien bsc, nhanvien nv, donvi dv ";
            sql += "where bsc.nhanviennhan = nv.nhanvien_id ";
            sql += "and nv.nhanvien_donvi = dv.donvi_id ";
            sql += "and bsc.thang = '" + thang + "' ";
            sql += "and bsc.nam = '" + nam + "' ";
            sql += "order by dv.donvi_id asc";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return dtResult;
        }

        [WebMethod]
        public static string loadDanhSach(int thang, int nam) {
            string outputHTML = "";
            DataTable dtDanhSach = new DataTable();
            dtDanhSach = dsNhanVienNhanBSC(thang, nam);
            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-nv' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='text-center'>MNV</th>";
            outputHTML += "<th class='text-center'>Họ và Tên</th>";
            outputHTML += "<th class='text-center'>Đơn vị</th>";
            outputHTML += "<th class='text-center'>Trạng thái giao</th>";
            outputHTML += "<th class='text-center'>Trạng thái nhận</th>";
            outputHTML += "<th class='text-center'></th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            for (int nIndex = 0; nIndex < dtDanhSach.Rows.Count; nIndex++) {
                string szTrangThaiGiao = "Chưa giao";
                string szTrangThaiNhan = "Chưa nhận";
                string clsTrangThaiGiao = "label-default";
                string clsTrangThaiNhan = "label-default";
                bool bSttGiao = Convert.ToBoolean(dtDanhSach.Rows[nIndex]["trangthaigiao"].ToString());
                bool bSttNhan = Convert.ToBoolean(dtDanhSach.Rows[nIndex]["trangthainhan"].ToString());
                if (bSttGiao) {
                    szTrangThaiGiao = "Đã giao";
                    clsTrangThaiGiao = "label-success";
                }

                if (bSttNhan)
                {
                    szTrangThaiNhan = "Đã nhận";
                    clsTrangThaiNhan = "label-success";
                }

                outputHTML += "<tr data-id='" + dtDanhSach.Rows[nIndex]["nhanvien_id"].ToString() + "'>";
                outputHTML += "<td class='text-center'><strong>" + dtDanhSach.Rows[nIndex]["nhanvien_manv"].ToString() + "</strong></td>";
                outputHTML += "<td><strong>" + dtDanhSach.Rows[nIndex]["nhanvien_hoten"].ToString() + "</strong></td>";
                outputHTML += "<td><strong>" + dtDanhSach.Rows[nIndex]["donvi_ten"].ToString() + "</strong></td>";
                outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiGiao + "'>" + szTrangThaiGiao + "</span></td>";
                outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiNhan + "'>" + szTrangThaiNhan + "</span></td>";
                outputHTML += "<td class='text-center'><a class='btn btn-primary detail btn-xs'>Chi tiết</a></td>";
                outputHTML += "</tr>";
            }

            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "</div>";
            return outputHTML;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Danh sách nhân viên đã nhận bsc";
            try
            {

            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}