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
    public partial class XuatMauBSCNV : System.Web.UI.Page
    {
        public static int nguoitao_id;
        public static DataTable dtMauBSC = new DataTable();
        public static DataTable dtNhanVien = new DataTable();
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

        /*List danh sách nhân viên theo đơn vị*/
        private DataTable dsNhanVien(int donvi)
        {
            DataTable dsNhanVien = new DataTable();
            Connection cn = new Connection();
            string sqlNhanVien = "select nhanvien.*, chucdanh.chucdanh_ten from nhanvien, chucdanh where nhanvien_donvi = '" + donvi + "' and chucdanh.chucdanh_id = nhanvien_chucdanh ORDER BY nhanvien.nhanvien_chucdanh";
            try
            {
                dsNhanVien = cn.XemDL(sqlNhanVien);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsNhanVien;
        }

        // Lấy ra danh sách các KPI, DVT, Trọng số theo tháng, năm và KPO
        public static DataTable getKPIByTimeAndKPO(int thang, int nam, int kpo_id, int loaiMauBSC, int nguoitao)
        {
            Connection cnBSC = new Connection();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            string sqlKPIByTimeAndKPO = "select kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, danhsachbsc.tytrong, danhsachbsc.nhom_kpi ";
            sqlKPIByTimeAndKPO += "from kpo, kpi, danhsachbsc, donvitinh dvt ";
            sqlKPIByTimeAndKPO += "where danhsachbsc.kpi_id = kpi.kpi_id ";
            sqlKPIByTimeAndKPO += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.donvitinh = dvt.dvt_id ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.thang = '" + thang + "' ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.nam = '" + nam + "' ";
            sqlKPIByTimeAndKPO += "and kpo.kpo_id = '" + kpo_id + "' ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.nguoitao = '" + nguoitao + "' ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.bscduocgiao != '' ";
            sqlKPIByTimeAndKPO += "and danhsachbsc.maubsc = '" + loaiMauBSC + "'";
            sqlKPIByTimeAndKPO += "group by kpi.kpi_id, kpi.kpi_ten, dvt.dvt_ten, danhsachbsc.tytrong, danhsachbsc.nhom_kpi ";
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
        public static Dictionary<String, String> loadBSCByTime(int thang, int nam, int loaiMauBSC, int nguoitao)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable dsDonViByTime = new DataTable();
            DataTable dsKPOByTime = new DataTable();
            DataTable dsKPIByTimeAndKPO = new DataTable();
            DataTable dsDetailByTimeAndKPI = new DataTable();

            string outputHTML = "";

            string sqlKPOByTime = "select kpo.kpo_id, kpo.kpo_ten ";
            sqlKPOByTime += "from kpo, kpi, danhsachbsc ";
            sqlKPOByTime += "where danhsachbsc.kpi_id = kpi.kpi_id ";
            sqlKPOByTime += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlKPOByTime += "and danhsachbsc.thang = '" + thang + "' ";
            sqlKPOByTime += "and danhsachbsc.nam = '" + nam + "' ";
            sqlKPOByTime += "and danhsachbsc.nguoitao = '" + nguoitao + "' ";
            sqlKPOByTime += "and danhsachbsc.bscduocgiao != '' ";
            sqlKPOByTime += "and danhsachbsc.maubsc = '" + loaiMauBSC + "' ";
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
            outputHTML += "<th class='no-sort'>Nhóm KPI</th>";
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
                    dsKPIByTimeAndKPO = getKPIByTimeAndKPO(thang, nam, kpo_id, loaiMauBSC, nguoitao);
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
                            string nhom_kpi = dsKPIByTimeAndKPO.Rows[nIndexKPI]["nhom_kpi"].ToString();

                            outputHTML += "<tr>";
                            outputHTML += "<td class='text-center'>" + kpi_id + "</td>";
                            outputHTML += "<td><strong>" + kpi_ten + "</strong></td>";
                            outputHTML += "<td><strong>" + dvt_ten + "</strong></td>";
                            outputHTML += "<td class='text-center'><strong>" + trongso + "</strong></td>";
                            outputHTML += "<td class='text-center'><strong>" + nhom_kpi + "</strong></td>";

                            // 11 đơn vị huyện thị và KHTCDN
                            //for (int nTmp = 0; nTmp < 12; nTmp++)
                            //{
                            //    outputHTML += "<td class='text-center'></td>";
                            //}
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
            this.Title = "Xuất mẫu bsc nhân viên";
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

                /*Nếu không tồn tại session hoặc chức vụ của nhân viên không đúng*/
                if (nhanvien == null || !nFindResult)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

                nguoitao_id = nhanvien.nhanvien_id;
                /*Get list MauBSC*/
                dtMauBSC = dsMauBSC();
                dtNhanVien = dsNhanVien(nhanvien.nhanvien_donvi_id);
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}