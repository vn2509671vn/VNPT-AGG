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
    public partial class DanhSachBSCTatCaDV : System.Web.UI.Page
    {
        private static string getNgayThamDinh(string donvinhan, int thang, int nam)
        {
            string szResult = "";
            DataTable tmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select format(max(thoigian_thamdinh),'dd/MM/yyyy HH:mm:ss') from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' and donvinhan = '" + donvinhan + "'";
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
                szResult = tmp.Rows[0][0].ToString();
            }
            return szResult;
        }

        private static bool getSttThamDinh(string donvinhan, int thang, int nam)
        {
            bool bResult = true;
            DataTable tmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select * from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' and donvinhan = '" + donvinhan + "' and trangthaithamdinh = 0";
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

        private static DataTable dsDonViNhanBSC(int thang, int nam)
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select dv.donvi_id, dv.donvi_ten, bsc.trangthaigiao, bsc.trangthainhan, bsc.trangthaiketthuc, format(bsc.ngaytao,'dd/MM/yyyy HH:mm:ss') as ngaytao ";
            sql += "from giaobscdonvi bsc, donvi dv ";
            sql += "where bsc.donvinhan = dv.donvi_id ";
            sql += "and bsc.thang = '" + thang + "' ";
            sql += "and bsc.nam = '" + nam + "' ";
            sql += "order by dv.donvi_ten asc";
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
        public static string loadDanhSach(int thang, int nam)
        {
            string outputHTML = "";
            DataTable dtDanhSach = new DataTable();
            dtDanhSach = dsDonViNhanBSC(thang, nam);
            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-nv' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='text-center'>Đơn vị</th>";
            outputHTML += "<th class='text-center'>Trạng thái giao</th>";
            outputHTML += "<th class='text-center'>Trạng thái nhận</th>";
            outputHTML += "<th class='text-center'>Trạng thẩm định</th>";
            outputHTML += "<th class='text-center'>Ngày giao</th>";
            outputHTML += "<th class='text-center'>Ngày thẩm định</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            for (int nIndex = 0; nIndex < dtDanhSach.Rows.Count; nIndex++)
            {
                string szTrangThaiGiao = "Chưa giao";
                string szTrangThaiNhan = "Chưa nhận";
                string szTrangThaiThamDinh = "Chưa thẩm định";
                string clsTrangThaiGiao = "label-default";
                string clsTrangThaiNhan = "label-default";
                string clsTrangThaiThamDinh = "label-default";
                bool bSttGiao = Convert.ToBoolean(dtDanhSach.Rows[nIndex]["trangthaigiao"].ToString());
                bool bSttNhan = Convert.ToBoolean(dtDanhSach.Rows[nIndex]["trangthainhan"].ToString());
                bool bSttThamDinh = getSttThamDinh(dtDanhSach.Rows[nIndex]["donvi_id"].ToString(), thang, nam);
                string ngaythamdinh = "";
                ngaythamdinh = getNgayThamDinh(dtDanhSach.Rows[nIndex]["donvi_id"].ToString(), thang, nam);

                if (bSttGiao)
                {
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

                outputHTML += "<tr data-id='" + dtDanhSach.Rows[nIndex]["donvi_id"].ToString() + "'>";
                outputHTML += "<td><strong>" + dtDanhSach.Rows[nIndex]["donvi_ten"].ToString() + "</strong></td>";
                outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiGiao + "'>" + szTrangThaiGiao + "</span></td>";
                outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiNhan + "'>" + szTrangThaiNhan + "</span></td>";
                outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + szTrangThaiThamDinh + "</span></td>";
                outputHTML += "<td class='text-center'>" + dtDanhSach.Rows[nIndex]["ngaytao"].ToString() + "</td>";
                outputHTML += "<td class='text-center'>" + ngaythamdinh + "</td>";
                outputHTML += "</tr>";
            }

            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "</div>";
            return outputHTML;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Danh sách đơn vị đã nhận bsc";
        }
    }
}