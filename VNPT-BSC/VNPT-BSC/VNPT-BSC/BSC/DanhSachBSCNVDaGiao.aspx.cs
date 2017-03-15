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

namespace VNPT_BSC.BSC
{
    public partial class DanhSachBSCNVDaGiao : System.Web.UI.Page
    {
        public static DataTable dtMauBSC = new DataTable();
        public static int nhanviengiao;
        /*List loại mẫu bsc*/
        private DataTable dsMauBSC()
        {
            DataTable dsMauBSC = new DataTable();
            Connection cn = new Connection();
            string sqlMauBSC = "select * from loaimaubsc where loai_id not in (1,2,3)";
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
        public static DataTable getKPIByTimeAndKPO(int thang, int nam, int kpo_id, int nhanviengiao, int loaimau)
        {
            Connection cnBSC = new Connection();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            string sqlKPIByTimeAndKPO = "select kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, bsc_nhanvien.trongso ";
            sqlKPIByTimeAndKPO += "from kpo, kpi, bsc_nhanvien, donvitinh dvt ";
            sqlKPIByTimeAndKPO += "where bsc_nhanvien.kpi = kpi.kpi_id ";
            sqlKPIByTimeAndKPO += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPIByTimeAndKPO += "and bsc_nhanvien.donvitinh = dvt.dvt_id ";
            sqlKPIByTimeAndKPO += "and bsc_nhanvien.thang = '" + thang + "' ";
            sqlKPIByTimeAndKPO += "and bsc_nhanvien.nam = '" + nam + "' ";
            sqlKPIByTimeAndKPO += "and bsc_nhanvien.nhanviengiao = '" + nhanviengiao + "' ";
            sqlKPIByTimeAndKPO += "and bsc_nhanvien.loaimau = '" + loaimau + "' ";
            sqlKPIByTimeAndKPO += "and kpo.kpo_id = '" + kpo_id + "' ";
            sqlKPIByTimeAndKPO += "group by kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, bsc_nhanvien.trongso";
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
        public static DataTable getDetailByTimeAndKPI(int thang, int nam, int kpi_id, int nhanviengiao, int loaimau)
        {
            Connection cnBSC = new Connection();
            DataTable dsDetailByTimeAndKPI = new DataTable();
            string sqlDetailByTimeAndKPI = "select nhanviennhan, kehoach ";
            sqlDetailByTimeAndKPI += "from bsc_nhanvien ";
            sqlDetailByTimeAndKPI += "where thang = '" + thang + "' ";
            sqlDetailByTimeAndKPI += "and nam = '" + nam + "' ";
            sqlDetailByTimeAndKPI += "and kpi = '" + kpi_id + "'";
            sqlDetailByTimeAndKPI += "and nhanviengiao = '" + nhanviengiao + "'";
            sqlDetailByTimeAndKPI += "and loaimau = '" + loaimau + "'";

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
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam, int loaimaubsc)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable dsNhanVienByTime = new DataTable();
            DataTable dsKPOByTime = new DataTable();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            DataTable dsDetailByTimeAndKPI = new DataTable();

            string outputHTML = "";
            string sqlNhanVienByTime = "select bsc_nhanvien.nhanviennhan, nhanvien.nhanvien_taikhoan ";
            sqlNhanVienByTime += "from bsc_nhanvien, nhanvien ";
            sqlNhanVienByTime += "where bsc_nhanvien.thang = '" + thang + "' ";
            sqlNhanVienByTime += "and bsc_nhanvien.nam = '" + nam + "' ";
            sqlNhanVienByTime += "and bsc_nhanvien.nhanviengiao = '" + nhanviengiao + "' ";
            sqlNhanVienByTime += "and bsc_nhanvien.loaimau = '" + loaimaubsc + "' ";
            sqlNhanVienByTime += "and bsc_nhanvien.nhanviennhan = nhanvien.nhanvien_id ";
            sqlNhanVienByTime += "group by bsc_nhanvien.nhanviennhan, nhanvien.nhanvien_taikhoan ";

            string sqlKPOByTime = "select kpo.kpo_id, kpo.kpo_ten ";
            sqlKPOByTime += "from kpo, kpi, bsc_nhanvien ";
            sqlKPOByTime += "where bsc_nhanvien.kpi = kpi.kpi_id ";
            sqlKPOByTime += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPOByTime += "and bsc_nhanvien.thang = '" + thang + "' ";
            sqlKPOByTime += "and bsc_nhanvien.nam = '" + nam + "' ";
            sqlKPOByTime += "and bsc_nhanvien.nhanviengiao = '" + nhanviengiao + "' ";
            sqlKPOByTime += "and bsc_nhanvien.loaimau = '" + loaimaubsc + "' ";
            sqlKPOByTime += "group by kpo.kpo_id, kpo.kpo_ten ";

            try
            {
                dsNhanVienByTime = cnBSC.XemDL(sqlNhanVienByTime);
                dsKPOByTime = cnBSC.XemDL(sqlKPOByTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            int slNhanvien = dsNhanVienByTime.Rows.Count; // Tổng số các đơn vị được giao

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-bsclist' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='no-sort'>STT</th>";
            outputHTML += "<th class='no-sort'>Chỉ tiêu BSC/KPI</th>";
            outputHTML += "<th class='no-sort'>ĐVT</th>";
            //outputHTML += "<th class='no-sort'>Trọng số (%)</th>";
            //outputHTML += "<th class='text-center' colspan='" + slDonvi + "'>Chỉ tiêu giao</th>";
            //outputHTML += "</tr>";

            // Hiển thị danh sách các nhân viên ở header
            //outputHTML += "<tr>";
            if (dsNhanVienByTime.Rows.Count <= 0)
            {
                outputHTML += "<th class='no-sort' colspan='" + slNhanvien + "'>No item</th>";
            }
            else
            {
                for (int nIndexNV = 0; nIndexNV < dsNhanVienByTime.Rows.Count; nIndexNV++)
                {
                    outputHTML += "<th class='no-sort' class='text-center'>" + dsNhanVienByTime.Rows[nIndexNV]["nhanvien_taikhoan"].ToString() + "</th>";
                }
            }
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (dsKPOByTime.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='" + (slNhanvien + 3) + "' class='text-center'>No item</td></tr>";
            }
            else
            {
                // Hiển thị các KPO
                for (int nIndexKPO = 0; nIndexKPO < dsKPOByTime.Rows.Count; nIndexKPO++)
                {
                    int kpo_id = Convert.ToInt32(dsKPOByTime.Rows[nIndexKPO]["kpo_id"].ToString());
                    string kpo_ten = dsKPOByTime.Rows[nIndexKPO]["kpo_ten"].ToString();
                    outputHTML += "<tr>";
                    outputHTML += "<td colspan='" + (slNhanvien + 3) + "'><strong>" + kpo_ten + "</strong></td>";
                    outputHTML += "<td style='display: none;'></td>";
                    outputHTML += "<td style='display: none;'></td>";
                    //outputHTML += "<td style='display: none;'></td>";
                    for (int nIndexSLDV = 0; nIndexSLDV < slNhanvien; nIndexSLDV++)
                    {
                        outputHTML += "<td style='display: none;'></td>";
                    }
                    outputHTML += "</tr>";

                    // Hiển thị các KPI, DVT, Trọng số theo KPO
                    dsKPIByTimeAndKPO = getKPIByTimeAndKPO(thang, nam, kpo_id, nhanviengiao, loaimaubsc);
                    if (dsKPIByTimeAndKPO.Rows.Count <= 0)
                    {
                        outputHTML += "<tr><td colspan='" + (slNhanvien + 3) + "' class='text-center'>No item</td></tr>";
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
                            outputHTML += "<td><strong>" + dvt_ten + "</strong></td>";
                            //outputHTML += "<td class='text-center'><strong>" + trongso + "</strong></td>";

                            // Hiển thị danh sách kế hoạch bsc theo tháng năm và kpi của các đơn vị
                            dsDetailByTimeAndKPI = getDetailByTimeAndKPI(thang, nam, kpi_id, nhanviengiao, loaimaubsc);
                            if (dsDetailByTimeAndKPI.Rows.Count <= 0)
                            {
                                outputHTML += "<td colspan='" + slNhanvien + "' class='text-center'>No item</td>";
                            }
                            else
                            {
                                for (int nIndexDetail = 0; nIndexDetail < dsDetailByTimeAndKPI.Rows.Count; nIndexDetail++)
                                {
                                    string kehoach = dsDetailByTimeAndKPI.Rows[nIndexDetail]["kehoach"].ToString();
                                    outputHTML += "<td class='text-center'>" + kehoach + "</td>";
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
            this.Title = "Danh sách bsc đã giao";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();

                // Khai báo các biến cho việc kiểm tra quyền
                List<int> quyenHeThong = new List<int>();
                bool nFindResult = false;
                quyenHeThong = Session.GetRole();

                /*Kiểm tra nếu không có quyền giao bsc nhân viên (id của quyền là 3) thì đẩy ra trang đăng nhập*/
                nFindResult = quyenHeThong.Contains(3);

                if (nhanvien == null)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

                nhanviengiao = nhanvien.nhanvien_id;
                dtMauBSC = dsMauBSC();
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}