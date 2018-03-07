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
    public partial class XuatMauBSC_PBH : System.Web.UI.Page
    {
        public int nguoitao_id;

        [WebMethod]
        public static Dictionary<String, String> loadData()
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cn = new Connection();
            DataTable dtKPI = new DataTable();

            string sql = "select kpi.*, nhom_kpi.*  from kpi, nhom_kpi where kpi.kpi_thuoc_kpo = 2025 and nhom_kpi.id = kpi.nhom_kpi and kpi.nhom_kpi in (68,69,70,71,72,73,74) order by nhom_kpi.thutuhienthi asc";
            string outputHTML = "";
            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-data' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='no-sort'>Mã KPI</th>";
            outputHTML += "<th class='no-sort'>KPI</th>";
            outputHTML += "<th class='no-sort'>Trọng số (%)</th>";
            // 11 đơn vị huyện thị
            outputHTML += "<th class='no-sort'>APU</th>";
            outputHTML += "<th class='no-sort'>CDC</th>";
            outputHTML += "<th class='no-sort'>CPU</th>";
            outputHTML += "<th class='no-sort'>CTH</th>";
            outputHTML += "<th class='no-sort'>CMI</th>";
            outputHTML += "<th class='no-sort'>LXN</th>";
            outputHTML += "<th class='no-sort'>PTN</th>";
            outputHTML += "<th class='no-sort'>TCU</th>";
            outputHTML += "<th class='no-sort'>TSN</th>";
            outputHTML += "<th class='no-sort'>TBN</th>";
            outputHTML += "<th class='no-sort'>TTN</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            try
            {
                dtKPI = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            for (int nIndexKPI = 0; nIndexKPI < dtKPI.Rows.Count; nIndexKPI++)
            {
                string kpi_id = dtKPI.Rows[nIndexKPI]["kpi_id"].ToString();
                string kpi_ten = dtKPI.Rows[nIndexKPI]["kpi_ten"].ToString();
                string trongso = dtKPI.Rows[nIndexKPI]["tytrong"].ToString();

                outputHTML += "<tr>";
                outputHTML += "<td class='text-center'>" + kpi_id + "</td>";
                outputHTML += "<td><strong>" + kpi_ten + "</strong></td>";
                outputHTML += "<td class='text-center'><strong>" + trongso + "</strong></td>";

                // 11 đơn vị huyện thị
                for (int nTmp = 0; nTmp < 11; nTmp++)
                {
                    outputHTML += "<td class='text-center'></td>";
                }
                outputHTML += "</tr>";
            }

            // Nhiệm vụ khác với tỉ trọng 10%
            outputHTML += "<tr>";
            outputHTML += "<td class='text-center'></td>";
            outputHTML += "<td><strong>Nhiệm vụ khác</strong></td>";
            outputHTML += "<td class='text-center'><strong>10</strong></td>";
            // 11 đơn vị huyện thị
            for (int nTmp = 0; nTmp < 11; nTmp++)
            {
                outputHTML += "<td class='text-center'></td>";
            }
            outputHTML += "</tr>";

            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridData", outputHTML);

            return dicOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Xuất mẫu BSC LĐ PBH";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                //nhanvien = Session.GetCurrentUser();
                nhanvien = (Nhanvien)Session["nhanvien"];
                if (nhanvien == null)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../index.aspx';</script>");
                }

            }
            catch
            {
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
        }
    }
}