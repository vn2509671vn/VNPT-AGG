﻿using System;
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
    public partial class ExportBSC : System.Web.UI.Page
    {
        // Lấy ra danh sách các KPI, DVT, Trọng số theo tháng, năm và KPO
        public static DataTable getKPIByTimeAndKPO(int thang, int nam, int kpo_id) {
            Connection cnBSC = new Connection();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            string sqlKPIByTimeAndKPO = "select kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, bsc_donvi.trongso ";
            sqlKPIByTimeAndKPO += "from kpo, kpi, bsc_donvi, donvitinh dvt ";
            sqlKPIByTimeAndKPO += "where bsc_donvi.kpi = kpi.kpi_id ";
            sqlKPIByTimeAndKPO += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.donvitinh = dvt.dvt_id ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.thang = '" + thang + "' ";
            sqlKPIByTimeAndKPO += "and bsc_donvi.nam = '" + nam + "' ";
            sqlKPIByTimeAndKPO += "and kpo.kpo_id = '" + kpo_id + "' ";
            sqlKPIByTimeAndKPO += "group by kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, bsc_donvi.trongso";
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
        public static DataTable getDetailByTimeAndKPI(int thang, int nam, int kpi_id)
        {
            Connection cnBSC = new Connection();
            DataTable dsDetailByTimeAndKPI = new DataTable();
            string sqlDetailByTimeAndKPI = "select donvinhan, kehoach ";
            sqlDetailByTimeAndKPI += "from bsc_donvi ";
            sqlDetailByTimeAndKPI += "where thang = '" + thang + "' ";
            sqlDetailByTimeAndKPI += "and nam = '" + nam + "' ";
            sqlDetailByTimeAndKPI += "and kpi = '" + kpi_id + "'";

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
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable dsDonViByTime = new DataTable();
            DataTable dsKPOByTime = new DataTable();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            DataTable dsDetailByTimeAndKPI = new DataTable();

            string outputHTML = "";
            string sqlDonViByTime = "select bsc_donvi.donvinhan, donvi.donvi_ma ";
            sqlDonViByTime += "from bsc_donvi, donvi ";
            sqlDonViByTime += "where bsc_donvi.thang = '" + thang + "' ";
            sqlDonViByTime += "and bsc_donvi.nam = '" + nam + "' ";
            sqlDonViByTime += "and bsc_donvi.donvinhan = donvi.donvi_id ";
            sqlDonViByTime += "group by bsc_donvi.donvinhan, donvi.donvi_ma ";

            string sqlKPOByTime = "select kpo.kpo_id, kpo.kpo_ten ";
            sqlKPOByTime += "from kpo, kpi, bsc_donvi ";
            sqlKPOByTime += "where bsc_donvi.kpi = kpi.kpi_id ";
            sqlKPOByTime += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPOByTime += "and bsc_donvi.thang = '" + thang + "' ";
            sqlKPOByTime += "and bsc_donvi.nam = '" + nam + "' ";
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
            outputHTML += "<th class='no-sort'>STT</th>";
            outputHTML += "<th class='no-sort'>Chỉ tiêu BSC/KPI</th>";
            outputHTML += "<th class='no-sort'>ĐVT</th>";
            outputHTML += "<th class='no-sort'>Trọng số (%)</th>";
            //outputHTML += "<th class='text-center' colspan='" + slDonvi + "'>Chỉ tiêu giao</th>";
            //outputHTML += "</tr>";

            // Hiển thị danh sách các đơn vị ở header
            //outputHTML += "<tr>";
            if (dsDonViByTime.Rows.Count <= 0)
            {
                outputHTML += "<th class='no-sort' colspan='" + slDonvi + "'>No item</th>";
            }
            else
            {
                for (int nIndexDV = 0; nIndexDV < dsDonViByTime.Rows.Count; nIndexDV++)
                {
                    outputHTML += "<th class='no-sort' class='text-center'>" + dsDonViByTime.Rows[nIndexDV]["donvi_ma"].ToString() + "</th>";
                }
            }
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (dsKPOByTime.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='" + (slDonvi+4) + "' class='text-center'>No item</td></tr>";
            }
            else
            {
                // Hiển thị các KPO
                for (int nIndexKPO = 0; nIndexKPO < dsKPOByTime.Rows.Count; nIndexKPO++)
                {
                    int kpo_id = Convert.ToInt32(dsKPOByTime.Rows[nIndexKPO]["kpo_id"].ToString());
                    //string kpo_ten = dsKPOByTime.Rows[nIndexKPO]["kpo_ten"].ToString();
                    //outputHTML += "<tr>";
                    //outputHTML += "<td colspan='" + (slDonvi + 4) + "'><strong>" + kpo_ten + "</strong></td>";
                    //outputHTML += "<td style='display: none;'></td>";
                    //outputHTML += "<td style='display: none;'></td>";
                    //outputHTML += "<td style='display: none;'></td>";
                    //for (int nIndexSLDV = 0; nIndexSLDV < slDonvi; nIndexSLDV++) {
                    //    outputHTML += "<td style='display: none;'></td>";
                    //}
                    //outputHTML += "</tr>";

                    // Hiển thị các KPI, DVT, Trọng số theo KPO
                    dsKPIByTimeAndKPO = getKPIByTimeAndKPO(thang, nam, kpo_id);
                    if (dsKPIByTimeAndKPO.Rows.Count <= 0)
                    {
                        outputHTML += "<tr><td colspan='" + (slDonvi + 4) + "' class='text-center'>No item</td></tr>";
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
                            outputHTML += "<td><strong>" + kpi_ten + "</strong></td>";
                            outputHTML += "<td class='text-center'><strong>" + dvt_ten + "</strong></td>";
                            outputHTML += "<td class='text-center'><strong>" + trongso + "</strong></td>";

                            // Hiển thị danh sách kế hoạch bsc theo tháng năm và kpi của các đơn vị
                            dsDetailByTimeAndKPI = getDetailByTimeAndKPI(thang, nam, kpi_id);
                            if (dsDetailByTimeAndKPI.Rows.Count <= 0)
                            {
                                outputHTML += "<td colspan='" + slDonvi + "' class='text-center'>No item</td>";
                            }
                            else
                            {
                                for (int nIndexDetail = 0; nIndexDetail < dsDetailByTimeAndKPI.Rows.Count; nIndexDetail++)
                                {
                                    string kehoach = dsDetailByTimeAndKPI.Rows[nIndexDetail]["kehoach"].ToString();
                                    outputHTML += "<td>" + kehoach + "</td>";
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
            this.Title = "Thống kê bsc đã giao";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                //nhanvien = Session.GetCurrentUser();
                nhanvien = (Nhanvien)Session["nhanvien"];

                // Khai báo các biến cho việc kiểm tra quyền
                List<int> quyenHeThong = new List<int>();
                bool nFindResult = false;
                //quyenHeThong = Session.GetRole();
                quyenHeThong = (List<int>)Session["quyenhethong"];

                /*Kiểm tra nếu không có quyền admin (id của quyền là 1) thì đẩy ra trang đăng nhập*/
                nFindResult = quyenHeThong.Contains(1);

                if (nhanvien == null || !nFindResult)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}