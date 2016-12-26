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

namespace VNPT_BSC.Admin
{
    public partial class XuatMauBSC : System.Web.UI.Page
    {
        public static int nguoitao_id;
        // Lấy ra danh sách các KPI, DVT, Trọng số theo tháng, năm và KPO
        public static DataTable getKPIByTimeAndKPO(int thang, int nam, int kpo_id)
        {
            Connection cnBSC = new Connection();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            string sqlKPIByTimeAndKPO = "select kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, danhsachbsc.tytrong ";
            sqlKPIByTimeAndKPO += "from kpo, kpi, danhsachbsc, donvitinh dvt ";
            sqlKPIByTimeAndKPO += "where danhsachbsc.kpi_id = kpi.kpi_id ";
            sqlKPIByTimeAndKPO += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.donvitinh = dvt.dvt_id ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.thang = '" + thang + "' ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.nam = '" + nam + "' ";
            sqlKPIByTimeAndKPO += "and kpo.kpo_id = '" + kpo_id + "' ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.nguoitao = (select top 1 nhanvien_id from nhanvien where nhanvien_chucvu = 10) ";
            sqlKPIByTimeAndKPO += "group by kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, danhsachbsc.tytrong";
            try
            {
                dsKPIByTimeAndKPO = cnBSC.XemDL(sqlKPIByTimeAndKPO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsKPIByTimeAndKPO;
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam, int nguoitao)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable dsDonViByTime = new DataTable();
            DataTable dsKPOByTime = new DataTable();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            DataTable dsDetailByTimeAndKPI = new DataTable();

            string outputHTML = "";

            string sqlKPOByTime = "select kpo.kpo_id, kpo.kpo_ten ";
            sqlKPOByTime += "from kpo, kpi, bsc_donvi ";
            sqlKPOByTime += "where bsc_donvi.kpi = kpi.kpi_id ";
            sqlKPOByTime += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPOByTime += "and bsc_donvi.thang = '" + thang + "' ";
            sqlKPOByTime += "and bsc_donvi.nam = '" + nam + "' ";
            sqlKPOByTime += "group by kpo.kpo_id, kpo.kpo_ten ";

            try
            {
                dsKPOByTime = cnBSC.XemDL(sqlKPOByTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-bsclist' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='no-sort'>Mã KPI</th>";
            outputHTML += "<th class='no-sort'>Chỉ tiêu BSC/KPI</th>";
            outputHTML += "<th class='no-sort'>ĐVT</th>";
            outputHTML += "<th class='no-sort'>Trọng số (%)</th>";
            // 11 đơn vị huyện thị và KHTCDN
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
            outputHTML += "<th class='no-sort'>KHTCDN</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (dsKPOByTime.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='" + (12 + 4) + "' class='text-center'>No item</td></tr>";
            }
            else
            {
                // Hiển thị các KPO
                for (int nIndexKPO = 0; nIndexKPO < dsKPOByTime.Rows.Count; nIndexKPO++)
                {
                    int kpo_id = Convert.ToInt32(dsKPOByTime.Rows[nIndexKPO]["kpo_id"].ToString());

                    // Hiển thị các KPI, DVT, Trọng số theo KPO
                    dsKPIByTimeAndKPO = getKPIByTimeAndKPO(thang, nam, kpo_id);
                    if (dsKPIByTimeAndKPO.Rows.Count <= 0)
                    {
                        outputHTML += "<tr><td colspan='" + (12 + 4) + "' class='text-center'>No item</td></tr>";
                    }
                    else
                    {
                        for (int nIndexKPI = 0; nIndexKPI < dsKPIByTimeAndKPO.Rows.Count; nIndexKPI++)
                        {
                            int kpi_id = Convert.ToInt32(dsKPIByTimeAndKPO.Rows[nIndexKPI]["kpi_id"].ToString());
                            string kpi_ten = dsKPIByTimeAndKPO.Rows[nIndexKPI]["kpi_ten"].ToString();
                            string dvt_ten = dsKPIByTimeAndKPO.Rows[nIndexKPI]["dvt_ten"].ToString();
                            string trongso = dsKPIByTimeAndKPO.Rows[nIndexKPI]["tytrong"].ToString();

                            outputHTML += "<tr>";
                            outputHTML += "<td class='text-center'>" + kpi_id + "</td>";
                            outputHTML += "<td><strong>" + kpi_ten + "</strong></td>";
                            outputHTML += "<td class='text-center'><strong>" + dvt_ten + "</strong></td>";
                            outputHTML += "<td class='text-center'><strong>" + trongso + "</strong></td>";

                            // 11 đơn vị huyện thị và KHTCDN
                            for (int nTmp = 0; nTmp < 12; nTmp++) {
                                outputHTML += "<td class='text-center'></td>";
                            }
                            outputHTML += "</tr>";
                        }
                    }
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);

            return dicOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();
                /*Nếu không tồn tại session hoặc chức vụ của nhân viên không phải admin (id = 30)*/
                if (nhanvien == null || nhanvien.nhanvien_chucvu_id != 30)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

                nguoitao_id = nhanvien.nhanvien_id;
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}