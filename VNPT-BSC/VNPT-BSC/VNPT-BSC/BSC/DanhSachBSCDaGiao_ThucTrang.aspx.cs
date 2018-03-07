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
    public partial class DanhSachBSCDaGiao_ThucTrang : System.Web.UI.Page
    {
        public DataTable dtMauBSC = new DataTable();

        /*List loại mẫu bsc*/
        private DataTable dsMauBSC()
        {
            DataTable dsMauBSC = new DataTable();
            Connection cn = new Connection();
            string sqlMauBSC = "select * from loaimaubsc where loai_id = 1";
            try
            {
                dsMauBSC = cn.XemDL(sqlMauBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsMauBSC;
        }

        // Lấy ra danh sách các KPI, DVT, Trọng số theo tháng, năm và KPO
        public static DataTable getKPIByTimeAndKPO(int thang, int nam, int kpo_id, int loaibsc)
        {
            Connection cnBSC = new Connection();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            string sqlKPIByTimeAndKPO = "select kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, bsc_donvi.trongso, danhsachbsc.stt ";
            sqlKPIByTimeAndKPO += "from kpo, kpi, bsc_donvi, donvitinh dvt, danhsachbsc ";
            sqlKPIByTimeAndKPO += "where bsc_donvi.kpi = kpi.kpi_id ";
            sqlKPIByTimeAndKPO += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.donvitinh = dvt.dvt_id ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.thang = '" + thang + "' ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.nam = '" + nam + "' ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.loaimau = '" + loaibsc + "' ";
            sqlKPIByTimeAndKPO += "and kpo.kpo_id = '" + kpo_id + "' ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.thang = danhsachbsc.thang ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.nam = danhsachbsc.nam ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.loaimau = danhsachbsc.maubsc ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.kpi = danhsachbsc.kpi_id  ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.bscduocgiao = '' ";
            sqlKPIByTimeAndKPO += "group by kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, bsc_donvi.trongso, danhsachbsc.stt ORDER BY danhsachbsc.stt ASC";
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

        // Lấy danh sách kế hoạch bsc theo tháng năm và kpi của các đơn vị
        public static DataTable getDetailByTimeAndKPI(int thang, int nam, int kpi_id, int loaibsc)
        {
            Connection cnBSC = new Connection();
            DataTable dsDetailByTimeAndKPI = new DataTable();
            string sqlDetailByTimeAndKPI = "select donvinhan, kehoach, thuchien ";
            sqlDetailByTimeAndKPI += "from bsc_donvi ";
            sqlDetailByTimeAndKPI += "where thang = '" + thang + "' ";
            sqlDetailByTimeAndKPI += "and nam = '" + nam + "' ";
            sqlDetailByTimeAndKPI += "and kpi = '" + kpi_id + "'";
            sqlDetailByTimeAndKPI += "and loaimau = '" + loaibsc + "'";

            try
            {
                dsDetailByTimeAndKPI = cnBSC.XemDL(sqlDetailByTimeAndKPI);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDetailByTimeAndKPI;
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam, int loaibsc)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable dsDonViByTime = new DataTable();
            DataTable dsKPOByTime = new DataTable();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            DataTable dsDetailByTimeAndKPI = new DataTable();

            string outputHTML = "";
            string sqlDonViByTime = "select bsc_donvi.donvinhan, donvi.donvi_ma, donvi.thutu_hienthi ";
            sqlDonViByTime += "from bsc_donvi, donvi ";
            sqlDonViByTime += "where bsc_donvi.thang = '" + thang + "' ";
            sqlDonViByTime += "and bsc_donvi.nam = '" + nam + "' ";
            sqlDonViByTime += "and bsc_donvi.loaimau = '" + loaibsc + "' ";
            sqlDonViByTime += "and bsc_donvi.donvinhan = donvi.donvi_id ";
            sqlDonViByTime += "group by bsc_donvi.donvinhan, donvi.donvi_ma, donvi.thutu_hienthi ";
            sqlDonViByTime += "order by donvi.thutu_hienthi asc ";

            string sqlKPOByTime = "select kpo.kpo_id, kpo.kpo_ten ";
            sqlKPOByTime += "from kpo, kpi, bsc_donvi ";
            sqlKPOByTime += "where bsc_donvi.kpi = kpi.kpi_id ";
            sqlKPOByTime += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPOByTime += "and bsc_donvi.thang = '" + thang + "' ";
            sqlKPOByTime += "and bsc_donvi.nam = '" + nam + "' ";
            sqlKPOByTime += "and bsc_donvi.loaimau = '" + loaibsc + "' ";
            sqlKPOByTime += "group by kpo.kpo_id, kpo.kpo_ten ";

            try
            {
                dsDonViByTime = cnBSC.XemDL(sqlDonViByTime);
                dsKPOByTime = cnBSC.XemDL(sqlKPOByTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            int slDonvi = dsDonViByTime.Rows.Count; // Tổng số các đơn vị được giao

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-bsclist' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='no-sort' rowspan='2'>STT</th>";
            outputHTML += "<th class='no-sort' rowspan='2'>Chỉ tiêu BSC/KPI</th>";
            outputHTML += "<th class='no-sort' rowspan='2'>ĐVT</th>";
            outputHTML += "<th class='no-sort' rowspan='2'>Trọng số (%)</th>";
            //outputHTML += "<th class='text-center' colspan='" + slDonvi + "'>Chỉ tiêu giao</th>";
            //outputHTML += "</tr>";

            // Hiển thị danh sách các đơn vị ở header
            //outputHTML += "<tr>";
            if (dsDonViByTime.Rows.Count <= 0)
            {
                outputHTML += "<th class='no-sort' colspan='" + (slDonvi*2) + "'>No item</th>";
            }
            else
            {
                for (int nIndexDV = 0; nIndexDV < dsDonViByTime.Rows.Count; nIndexDV++)
                {
                    outputHTML += "<th class='no-sort text-center' colspan='2'>" + dsDonViByTime.Rows[nIndexDV]["donvi_ma"].ToString() + "</th>";
                }
            }
            outputHTML += "</tr>";

            outputHTML += "<tr>";
            for (int nIndexDV = 0; nIndexDV < dsDonViByTime.Rows.Count; nIndexDV++)
            {
                outputHTML += "<th class='no-sort text-center'>K.Hoạch</th>";
                outputHTML += "<th class='no-sort text-center'>T.Hiện</th>";
            }
            outputHTML += "</tr>";

            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (dsKPOByTime.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='" + (slDonvi*2 + 4) + "' class='text-center'>No item</td></tr>";
            }
            else
            {
                // Hiển thị các KPO
                for (int nIndexKPO = 0; nIndexKPO < dsKPOByTime.Rows.Count; nIndexKPO++)
                {
                    int kpo_id = Convert.ToInt32(dsKPOByTime.Rows[nIndexKPO]["kpo_id"].ToString());
                    string kpo_ten = dsKPOByTime.Rows[nIndexKPO]["kpo_ten"].ToString();
                    outputHTML += "<tr style = 'background-color: burlywood;'>";
                    outputHTML += "<td colspan='" + (slDonvi*2 + 4) + "'><strong>" + kpo_ten + "</strong></td>";
                    outputHTML += "<td style='display: none;'></td>";
                    outputHTML += "<td style='display: none;'></td>";
                    outputHTML += "<td style='display: none;'></td>";
                    for (int nIndexSLDV = 0; nIndexSLDV < slDonvi*2; nIndexSLDV++)
                    {
                        outputHTML += "<td style='display: none;'></td>";
                    }
                    outputHTML += "</tr>";

                    // Hiển thị các KPI, DVT, Trọng số theo KPO
                    dsKPIByTimeAndKPO = getKPIByTimeAndKPO(thang, nam, kpo_id, loaibsc);
                    if (dsKPIByTimeAndKPO.Rows.Count <= 0)
                    {
                        outputHTML += "<tr><td colspan='" + (slDonvi*2 + 4) + "' class='text-center'>No item</td></tr>";
                    }
                    else
                    {
                        for (int nIndexKPI = 0; nIndexKPI < dsKPIByTimeAndKPO.Rows.Count; nIndexKPI++)
                        {
                            int kpi_id = Convert.ToInt32(dsKPIByTimeAndKPO.Rows[nIndexKPI]["kpi_id"].ToString());
                            string kpi_ten = dsKPIByTimeAndKPO.Rows[nIndexKPI]["kpi_ten"].ToString();
                            string dvt_ten = dsKPIByTimeAndKPO.Rows[nIndexKPI]["dvt_ten"].ToString();
                            string trongso = dsKPIByTimeAndKPO.Rows[nIndexKPI]["trongso"].ToString();

                            outputHTML += "<tr>";
                            outputHTML += "<td class='text-center'>" + (nIndexKPI + 1) + "</td>";
                            outputHTML += "<td class='min-width-200'><strong>" + kpi_ten + "</strong></td>";
                            outputHTML += "<td><strong>" + dvt_ten + "</strong></td>";
                            outputHTML += "<td class='text-center'><strong>" + trongso + "</strong></td>";

                            // Hiển thị danh sách kế hoạch bsc theo tháng năm và kpi của các đơn vị
                            dsDetailByTimeAndKPI = getDetailByTimeAndKPI(thang, nam, kpi_id, loaibsc);
                            if (dsDetailByTimeAndKPI.Rows.Count <= 0)
                            {
                                outputHTML += "<td colspan='" + slDonvi*2 + "' class='text-center'>No item</td>";
                            }
                            else
                            {
                                for (int nIndexDetail = 0; nIndexDetail < dsDetailByTimeAndKPI.Rows.Count; nIndexDetail++)
                                {
                                    string kehoach = dsDetailByTimeAndKPI.Rows[nIndexDetail]["kehoach"].ToString();
                                    string thuchien = dsDetailByTimeAndKPI.Rows[nIndexDetail]["thuchien"].ToString();
                                    outputHTML += "<td class='text-center min-width-72'>" + kehoach + "</td>";
                                    outputHTML += "<td class='text-center min-width-72' style='background-color: #92d050'>" + thuchien + "</td>";
                                }
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
            this.Title = "Dữ liệu BSC đơn vị trên Vmos";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                //nhanvien = Session.GetCurrentUser();
                nhanvien = (Nhanvien)Session["nhanvien"];

                //// Khai báo các biến cho việc kiểm tra quyền
                //List<int> quyenHeThong = new List<int>();
                //bool nFindResult = false;
                //quyenHeThong = Session.GetRole();

                ///*Kiểm tra nếu không có quyền giao bsc đơn vị (id của quyền là 2) thì đẩy ra trang đăng nhập*/
                //nFindResult = quyenHeThong.Contains(2);

                //if (nhanvien == null)
                //{
                //    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                //    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                //}

                dtMauBSC = dsMauBSC();
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}