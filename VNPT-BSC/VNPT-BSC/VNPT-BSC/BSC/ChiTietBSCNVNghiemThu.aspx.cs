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
    public partial class ChiTietBSCNVNghiemThu : System.Web.UI.Page
    {
        public static string nhanviengiao, nhanviennhan, nhanvienthamdinh, thang, nam;

        public static DataTable getKPIByTimeAndNhom(int thang, int nam, int nhom_kpi, int nhanviennhan)
        {
            Connection cnBSC = new Connection();
            DataTable dsKPIByTimeAndNhom = new DataTable();
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpi.kpi_ma, dvt.dvt_ten as donvitinh, bsc.trongso, bsc.kehoach, bsc.thuchien, bsc.thamdinh, bsc.kq_thuchien, bsc.diem_kpi ";
            sqlBSC += "from bsc_nhanvien bsc, kpi, nhanvien nvgiao, nhanvien nvnhan, donvitinh dvt, nhom_kpi ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.donvitinh = dvt.dvt_id ";
            sqlBSC += "and bsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and bsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and bsc.nhanviennhan = '" + nhanviennhan + "' ";
            sqlBSC += "and bsc.nhom_kpi = nhom_kpi.id ";
            sqlBSC += "and bsc.nhom_kpi = '" + nhom_kpi + "' ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' ORDER BY bsc.stt ASC";

            try
            {
                dsKPIByTimeAndNhom = cnBSC.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsKPIByTimeAndNhom;
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int nhanviengiao, int nhanviennhan, int thang, int nam)
        {
            //string[] arrOutput = {};
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable dsNhomKPIByTime = new DataTable();
            DataTable dsKPIByTimeAndNhom = new DataTable();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            //string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, dvt.dvt_ten as donvitinh, bsc.trongso, bsc.kehoach, bsc.thuchien, bsc.thamdinh ";
            //sqlBSC += "from bsc_nhanvien bsc, kpi, kpo, nhanvien nvgiao, nhanvien nvnhan, donvitinh dvt, nhom_kpi ";
            //sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            //sqlBSC += "and bsc.donvitinh = dvt.dvt_id ";
            //sqlBSC += "and bsc.nhanviengiao = nvgiao.nhanvien_id ";
            //sqlBSC += "and bsc.nhanviennhan = nvnhan.nhanvien_id ";
            //sqlBSC += "and bsc.nhanviennhan = '" + nhanviennhan + "' ";
            //sqlBSC += "and bsc.nhanviengiao = '" + nhanviengiao + "' ";
            //sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            //sqlBSC += "and kpi.nhom_kpi = nhom_kpi.id ";
            //sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' ORDER BY nhom_kpi.id ASC";

            string sqlNhomKPIByTime = "select nhom_kpi.id, nhom_kpi.ten_nhom, nhom_kpi.tytrong, nhom_kpi.thutuhienthi ";
            sqlNhomKPIByTime += "from bsc_nhanvien, nhom_kpi, kpi ";
            sqlNhomKPIByTime += "where bsc_nhanvien.kpi = kpi.kpi_id ";
            //sqlNhomKPIByTime += "and kpi.nhom_kpi = nhom_kpi.id ";
            sqlNhomKPIByTime += "and bsc_nhanvien.nhom_kpi = nhom_kpi.id ";
            sqlNhomKPIByTime += "and bsc_nhanvien.thang = '" + thang + "' ";
            sqlNhomKPIByTime += "and bsc_nhanvien.nam = '" + nam + "' ";
            sqlNhomKPIByTime += "and bsc_nhanvien.nhanviennhan = '" + nhanviennhan + "' ";
            sqlNhomKPIByTime += "group by nhom_kpi.id, nhom_kpi.ten_nhom, nhom_kpi.tytrong, nhom_kpi.thutuhienthi ";
            sqlNhomKPIByTime += "ORDER BY nhom_kpi.thutuhienthi ASC ";

            try
            {
                //gridData = cnBSC.XemDL(sqlBSC);
                dsNhomKPIByTime = cnBSC.XemDL(sqlNhomKPIByTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='text-center'>STT</th>";
            outputHTML += "<th class='text-center'>Chỉ tiêu</th>";
            outputHTML += "<th class='text-center'>Tỷ trọng (%)</th>";
            outputHTML += "<th class='text-center'>ĐVT</th>";
            outputHTML += "<th class='text-center'>Chỉ tiêu</th>";
            outputHTML += "<th class='text-center'>Thực hiện</th>";
            outputHTML += "<th class='text-center'>Thẩm định</th>";
            outputHTML += "<th class='text-center'>Tỷ lệ thực hiện (%)</th>";
            outputHTML += "<th class='text-center'>Điểm KPI</th>";
            outputHTML += "<th class='text-center'>Hệ số quy đổi tính lương</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (dsNhomKPIByTime.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='10' class='text-center'>No item</td></tr>";
            }
            else { 
                // Hiển thị các nhóm KPI
                double tongdiem_kq_thuchien = 0;
                double tongdiem_diem_kpi = 0;
                for (int nIndexNhom = 0; nIndexNhom < dsNhomKPIByTime.Rows.Count; nIndexNhom++)
                {
                    int nhom_id = Convert.ToInt32(dsNhomKPIByTime.Rows[nIndexNhom]["id"].ToString());
                    string nhom_ten = dsNhomKPIByTime.Rows[nIndexNhom]["ten_nhom"].ToString();
                    string nhom_tytrong = dsNhomKPIByTime.Rows[nIndexNhom]["tytrong"].ToString();
                    outputHTML += "<tr style = 'background-color: burlywood;'>";
                    outputHTML += "<td style='text-align: center;'><strong>" + (nIndexNhom + 1) + "</strong></td>";
                    outputHTML += "<td><strong>" + nhom_ten + "</strong></td>";
                    outputHTML += "<td style='text-align: center;'><strong>" + nhom_tytrong + "</strong></td>";
                    outputHTML += "<td></td>";
                    outputHTML += "<td></td>";
                    outputHTML += "<td></td>";
                    outputHTML += "<td></td>";
                    outputHTML += "<td></td>";
                    outputHTML += "<td></td>";
                    outputHTML += "<td></td>";
                    outputHTML += "</tr>";

                    // Hiển thị kết quả của các KPI số theo nhóm kpi
                    dsKPIByTimeAndNhom = getKPIByTimeAndNhom(thang, nam, nhom_id, nhanviennhan);
                    if (dsKPIByTimeAndNhom.Rows.Count <= 0)
                    {
                        outputHTML += "<tr><td colspan='10' class='text-center'>No item</td></tr>";
                    }
                    else
                    {
                        for (int nKPI = 0; nKPI < dsKPIByTimeAndNhom.Rows.Count; nKPI++)
                        {
                            double tylethuchien = 0;
                            double kehoach = Convert.ToDouble(dsKPIByTimeAndNhom.Rows[nKPI]["kehoach"].ToString());
                            double thamdinh = Convert.ToDouble(dsKPIByTimeAndNhom.Rows[nKPI]["thamdinh"].ToString());
                            double kq_thuchien = 0;
                            if(dsKPIByTimeAndNhom.Rows[nKPI]["kq_thuchien"].ToString() != ""){
                                kq_thuchien = Convert.ToDouble(dsKPIByTimeAndNhom.Rows[nKPI]["kq_thuchien"].ToString());
                            }

                            double diem_kpi = 0;
                            if (dsKPIByTimeAndNhom.Rows[nKPI]["diem_kpi"].ToString() != "") {
                                diem_kpi = Convert.ToDouble(dsKPIByTimeAndNhom.Rows[nKPI]["diem_kpi"].ToString());
                            }
                               
                            if (kehoach == 0)
                            {
                                tylethuchien = 0;
                            }
                            else
                            {
                                tylethuchien = (thamdinh / kehoach) * 100;
                            }

                            tongdiem_kq_thuchien += kq_thuchien;
                            tongdiem_diem_kpi += diem_kpi;

                            outputHTML += "<tr data-id='" + dsKPIByTimeAndNhom.Rows[nKPI]["kpi_id"].ToString() + "'>";
                            outputHTML += "<td class='text-center'></td>";
                            outputHTML += "<td><strong>" + "- " + dsKPIByTimeAndNhom.Rows[nKPI]["kpi_ten"].ToString() + "</strong></td>";
                            outputHTML += "<td style='text-align: center;'><strong>" + dsKPIByTimeAndNhom.Rows[nKPI]["trongso"].ToString() + "</strong></td>";
                            outputHTML += "<td><strong>" + dsKPIByTimeAndNhom.Rows[nKPI]["donvitinh"].ToString() + "</strong></td>";
                            outputHTML += "<td style='text-align: center;'><strong>" + dsKPIByTimeAndNhom.Rows[nKPI]["kehoach"].ToString() + "</strong></td>";
                            outputHTML += "<td style='text-align: center;'><strong>" + dsKPIByTimeAndNhom.Rows[nKPI]["thuchien"].ToString() + "</strong></td>";
                            outputHTML += "<td style='text-align: center;'><strong>" + dsKPIByTimeAndNhom.Rows[nKPI]["thamdinh"].ToString() + "</strong></td>";
                            outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0.##}", tylethuchien) + "</strong></td>";
                            outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0.####}", kq_thuchien) + "</strong></td>";
                            outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0.####}", diem_kpi) + "</strong></td>";
                            outputHTML += "</tr>";
                        }
                    }
                }
                outputHTML += "<tr style = 'background-color: burlywood;'>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center;'><strong>Tổng:</strong></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0.####}", tongdiem_kq_thuchien) + "</strong></td>";
                outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0.####}", tongdiem_diem_kpi) + "</strong></td>";
                outputHTML += "</tr>";
            }
            
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);

            /*Lấy danh sách các thông tin còn lại ở bảng giaobscdonvi*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select giaobscnhanvien.*, nhanvien.nhanvien_hoten as ten_nvn, loaimaubsc.loai_ten, donvi.donvi_ten, nhanvien.nhanvien_manv, nvgiao.nhanvien_hoten as ten_nvg from giaobscnhanvien, nhanvien, loaimaubsc, donvi, nhanvien nvgiao ";
            sqlGiaoBSCDV += "where giaobscnhanvien.nhanviengiao = '" + nhanviengiao + "' ";
            sqlGiaoBSCDV += "and giaobscnhanvien.nhanviennhan = '" + nhanviennhan + "'";
            sqlGiaoBSCDV += "and giaobscnhanvien.thang = '" + thang + "'";
            sqlGiaoBSCDV += "and giaobscnhanvien.nam = '" + nam + "'";
            sqlGiaoBSCDV += "and giaobscnhanvien.loaimau = loaimaubsc.loai_id ";
            sqlGiaoBSCDV += "and giaobscnhanvien.nhanviennhan = nhanvien.nhanvien_id ";
            sqlGiaoBSCDV += "and giaobscnhanvien.nhanviengiao = nvgiao.nhanvien_id ";
            sqlGiaoBSCDV += "and nhanvien.nhanvien_donvi = donvi.donvi_id";

            try
            {
                dtGiaoBSCDV = cnBSC.XemDL(sqlGiaoBSCDV);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtGiaoBSCDV.Rows.Count > 0)
            {
                dicOutput.Add("tendonvigiao", dtGiaoBSCDV.Rows[0]["donvi_ten"].ToString());
                dicOutput.Add("nhanviengiao", dtGiaoBSCDV.Rows[0]["nhanviengiao"].ToString());
                dicOutput.Add("tennhanviengiao", dtGiaoBSCDV.Rows[0]["ten_nvg"].ToString());
                dicOutput.Add("nhanviennhan", dtGiaoBSCDV.Rows[0]["nhanviennhan"].ToString());
                dicOutput.Add("tennhanviennhan", dtGiaoBSCDV.Rows[0]["ten_nvn"].ToString());
                dicOutput.Add("thang", dtGiaoBSCDV.Rows[0]["thang"].ToString());
                dicOutput.Add("nam", dtGiaoBSCDV.Rows[0]["nam"].ToString());
                dicOutput.Add("trangthaigiao", dtGiaoBSCDV.Rows[0]["trangthaigiao"].ToString());
                dicOutput.Add("trangthainhan", dtGiaoBSCDV.Rows[0]["trangthainhan"].ToString());
                dicOutput.Add("trangthaicham", dtGiaoBSCDV.Rows[0]["trangthaicham"].ToString());
                dicOutput.Add("trangthaidongy_kqtd", dtGiaoBSCDV.Rows[0]["trangthaidongy_kqtd"].ToString());
                dicOutput.Add("trangthaiketthuc", dtGiaoBSCDV.Rows[0]["trangthaiketthuc"].ToString());
                dicOutput.Add("loaimaubsc", dtGiaoBSCDV.Rows[0]["loai_ten"].ToString());
                dicOutput.Add("donvi_ten", dtGiaoBSCDV.Rows[0]["donvi_ten"].ToString());
                dicOutput.Add("ma_nv", dtGiaoBSCDV.Rows[0]["nhanvien_manv"].ToString());
            }
            else
            {
                dicOutput.Add("tendonvigiao", "");
                dicOutput.Add("nhanviengiao", nhanviengiao.ToString());
                dicOutput.Add("tennhanviengiao", "");
                dicOutput.Add("nhanviennhan", nhanviennhan.ToString());
                dicOutput.Add("tennhanviennhan", "");
                dicOutput.Add("thang", "0");
                dicOutput.Add("nam", "0");
                dicOutput.Add("trangthaigiao", "0");
                dicOutput.Add("trangthainhan", "0");
                dicOutput.Add("trangthaicham", "0");
                dicOutput.Add("trangthaidongy_kqtd", "0");
                dicOutput.Add("trangthaiketthuc", "0");
                dicOutput.Add("loaimaubsc", "");
                dicOutput.Add("donvi_ten", "");
                dicOutput.Add("ma_nv", "");
            }

            return dicOutput;
        }

        [WebMethod]
        public static bool updateKetThucStatus(int nhanviengiao, int nhanviennhan, int thang, int nam)
        {
            Connection cnNhanBSC = new Connection();
            bool isSuccess = false;

            string sqlGiaoBSC = "update giaobscnhanvien set trangthaiketthuc = 1 where nhanviengiao = '" + nhanviengiao + "' and nhanviennhan = '" + nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cnNhanBSC.ThucThiDL(sqlGiaoBSC);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Chi tiết nghiệm thu";
            //if (!IsPostBack)
            //{
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    //nhanvien = Session.GetCurrentUser();
                    nhanvien = (Nhanvien)Session["nhanvien"];

                    nhanviengiao = Request.QueryString["nhanviengiao"];
                    nhanviennhan = Request.QueryString["nhanviennhan"];
                    thang = Request.QueryString["thang"];
                    nam = Request.QueryString["nam"];

                    if (nhanviengiao == null || nhanviennhan == null || thang == null || nam == null || nhanvien.nhanvien_id != Convert.ToInt32(nhanviengiao))
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            //}
        }
    }
}