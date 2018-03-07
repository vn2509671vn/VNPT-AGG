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
        public DataTable dtDonvi = new DataTable();

        private static string getNgayThamDinh(string nhanviennhan, int thang, int nam) {
            string szResult = "";
            DataTable tmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select format(max(thoigian_thamdinh),'dd/MM/yyyy HH:mm:ss') from bsc_nhanvien where thang = '" + thang + "' and nam = '" + nam + "' and nhanviennhan = '" + nhanviennhan + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                szResult = tmp.Rows[0][0].ToString();
            }
            return szResult;
        }

        private static bool getSttThamDinh(string nhanviennhan, int thang, int nam)
        {
            bool bResult = true;
            DataTable tmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select * from bsc_nhanvien where thang = '" + thang + "' and nam = '" + nam + "' and nhanviennhan = '" + nhanviennhan + "' and trangthaithamdinh = 0";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (tmp.Rows.Count > 0)
            {
                bResult = false;
            }
            return bResult;
        }

        private static DataTable dsDonvi() {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select * from donvi where donvi_id not in (1,2)";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return dtResult;
        }

        private static DataTable dsNhanVienNhanBSC(int thang, int nam, int donvi)
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select nv.nhanvien_id, nv.nhanvien_manv, nv.nhanvien_hoten, dv.donvi_ten, bsc.trangthaigiao, bsc.trangthainhan, format(bsc.ngaytao,'dd/MM/yyyy HH:mm:ss') as ngaytao ";
            sql += "from giaobscnhanvien bsc, nhanvien nv, donvi dv ";
            sql += "where bsc.nhanviennhan = nv.nhanvien_id ";
            sql += "and nv.nhanvien_donvi = dv.donvi_id ";
            sql += "and bsc.thang = '" + thang + "' ";
            sql += "and bsc.nam = '" + nam + "' ";
            sql += "and nv.nhanvien_donvi = '" + donvi + "' ";
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
        public static string loadDanhSach(int thang, int nam, int donvi) {
            string outputHTML = "";
            DataTable dtDanhSach = new DataTable();
            dtDanhSach = dsNhanVienNhanBSC(thang, nam, donvi);
            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-nv' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='text-center'>MNV</th>";
            outputHTML += "<th class='text-center'>Họ và Tên</th>";
            outputHTML += "<th class='text-center'>Đơn vị</th>";
            outputHTML += "<th class='text-center'>Trạng thái giao</th>";
            outputHTML += "<th class='text-center'>Trạng thái nhận</th>";
            outputHTML += "<th class='text-center'>Trạng thẩm định</th>";
            outputHTML += "<th class='text-center'>Ngày giao</th>";
            outputHTML += "<th class='text-center'>Ngày thẩm định</th>";
            outputHTML += "<th class='text-center'></th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            for (int nIndex = 0; nIndex < dtDanhSach.Rows.Count; nIndex++) {
                string szTrangThaiGiao = "Chưa giao";
                string szTrangThaiNhan = "Chưa nhận";
                string szTrangThaiThamDinh = "Chưa thẩm định";
                string clsTrangThaiGiao = "label-default";
                string clsTrangThaiNhan = "label-default";
                string clsTrangThaiThamDinh = "label-default";
                bool bSttGiao = Convert.ToBoolean(dtDanhSach.Rows[nIndex]["trangthaigiao"].ToString());
                bool bSttNhan = Convert.ToBoolean(dtDanhSach.Rows[nIndex]["trangthainhan"].ToString());
                bool bSttThamDinh = getSttThamDinh(dtDanhSach.Rows[nIndex]["nhanvien_id"].ToString(), thang, nam);
                string ngaythamdinh = "";
                ngaythamdinh = getNgayThamDinh(dtDanhSach.Rows[nIndex]["nhanvien_id"].ToString(), thang, nam);

                if (bSttGiao) {
                    szTrangThaiGiao = "Đã giao";
                    clsTrangThaiGiao = "label-success";
                }

                if (bSttNhan)
                {
                    szTrangThaiNhan = "Đã nhận";
                    clsTrangThaiNhan = "label-success";
                }

                if (bSttThamDinh)
                {
                    szTrangThaiThamDinh = "Đã thẩm định";
                    clsTrangThaiThamDinh = "label-success";
                }

                outputHTML += "<tr data-id='" + dtDanhSach.Rows[nIndex]["nhanvien_id"].ToString() + "'>";
                outputHTML += "<td class='text-center'><strong>" + dtDanhSach.Rows[nIndex]["nhanvien_manv"].ToString() + "</strong></td>";
                outputHTML += "<td><strong>" + dtDanhSach.Rows[nIndex]["nhanvien_hoten"].ToString() + "</strong></td>";
                outputHTML += "<td><strong>" + dtDanhSach.Rows[nIndex]["donvi_ten"].ToString() + "</strong></td>";
                outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiGiao + "'>" + szTrangThaiGiao + "</span></td>";
                outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiNhan + "'>" + szTrangThaiNhan + "</span></td>";
                outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + szTrangThaiThamDinh + "</span></td>";
                outputHTML += "<td class='text-center'>" + dtDanhSach.Rows[nIndex]["ngaytao"].ToString() + "</td>";
                outputHTML += "<td class='text-center'>" + ngaythamdinh + "</td>";
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
                dtDonvi = dsDonvi();
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}