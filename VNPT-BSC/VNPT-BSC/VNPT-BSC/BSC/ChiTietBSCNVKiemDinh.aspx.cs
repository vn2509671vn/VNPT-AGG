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
    public partial class ChiTietBSCNVKiemDinh : System.Web.UI.Page
    {
        public static string nhanviengiao, nhanviennhan, nhanvienthamdinh, thang, nam;
        public class kpiDetail
        {
            public int kpi_id { get; set; }
            public float thamdinh { get; set; }
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int nhanviengiao, int nhanviennhan, int thang, int nam, int nhanvienthamdinh)
        {
            //string[] arrOutput = {};
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            int soluong_kpi_dathamdinh = 0;
            string szSL_kpi_dathamdinh = "";

            /*Lấy danh sách BSC từ bảng bsc_nhanvien*/
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, dvt.dvt_ten as donvitinh, bsc.trongso, bsc.kehoach, bsc.thuchien, bsc.thamdinh, bsc.trangthaithamdinh, bsc.ghichu, nhom_kpi.id as nhom, bsc.diem_kpi ";
            sqlBSC += "from bsc_nhanvien bsc, kpi, kpo, nhanvien nvgiao, nhanvien nvnhan, donvitinh dvt, nhom_kpi ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.donvitinh = dvt.dvt_id ";
            sqlBSC += "and bsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and bsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and bsc.nhanviengiao = '" + nhanviengiao + "' ";
            sqlBSC += "and bsc.nhanviennhan = '" + nhanviennhan + "' ";
            sqlBSC += "and bsc.nhanvienthamdinh = '" + nhanvienthamdinh + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and kpi.nhom_kpi = nhom_kpi.id ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' ORDER BY nhom_kpi.id ASC";
            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
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
            outputHTML += "<th class='text-center'>T/gian giao</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='8' class='text-center'>No item</td></tr>";
            }
            else
            {
                double tongdiem_diem_kpi = 0;
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    double diem_kpi = 0;
                    if (gridData.Rows[nKPI]["diem_kpi"].ToString() != "")
                    {
                        diem_kpi = Convert.ToDouble(gridData.Rows[nKPI]["diem_kpi"].ToString());
                    }
                    tongdiem_diem_kpi += diem_kpi;

                    if (gridData.Rows[nKPI]["trangthaithamdinh"].ToString() == "True")
                    {
                        soluong_kpi_dathamdinh += 1;
                    }

                    outputHTML += "<tr class='nhom-" + gridData.Rows[nKPI]["nhom"].ToString() + "' data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<td class='text-center'>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td><strong>" +  gridData.Rows[nKPI]["kpi_ten"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["trongso"].ToString() + "</strong></td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nKPI]["donvitinh"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["kehoach"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["thuchien"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='thamdinh' id='thamdinh_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' value='" + gridData.Rows[nKPI]["thamdinh"].ToString() + "' onkeypress='return onlyNumbers(event.charCode || event.keyCode);'/></td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nKPI]["ghichu"].ToString() + "</strong></td>";
                    outputHTML += "</tr>";
                }
                outputHTML += "<tr data-id='999999'>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center;'><strong class='red-900'>Tổng điểm:</strong></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center;'><strong class='red-900'>" + String.Format("{0:0.####}", tongdiem_diem_kpi) + "</strong></td>";
                outputHTML += "<td></td>";
                outputHTML += "</tr>";
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);

            szSL_kpi_dathamdinh = soluong_kpi_dathamdinh.ToString() + "/" + gridData.Rows.Count.ToString();
            dicOutput.Add("soluong_kpi_dathamdinh", szSL_kpi_dathamdinh);

            /*Lấy danh sách các thông tin còn lại ở bảng giaobscnhanvien*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select giaobscnhanvien.*, nv.nhanvien_hoten from giaobscnhanvien, nhanvien nv ";
            sqlGiaoBSCDV += "where giaobscnhanvien.nhanviengiao = '" + nhanviengiao + "' ";
            sqlGiaoBSCDV += "and giaobscnhanvien.nhanviennhan = '" + nhanviennhan + "' ";
            sqlGiaoBSCDV += "and giaobscnhanvien.thang = '" + thang + "' ";
            sqlGiaoBSCDV += "and giaobscnhanvien.nam = '" + nam + "' ";
            sqlGiaoBSCDV += "and giaobscnhanvien.nhanviennhan = nv.nhanvien_id";
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
                dicOutput.Add("nhanviengiao", dtGiaoBSCDV.Rows[0]["nhanviengiao"].ToString());
                dicOutput.Add("nhanviennhan", dtGiaoBSCDV.Rows[0]["nhanviennhan"].ToString());
                dicOutput.Add("thang", dtGiaoBSCDV.Rows[0]["thang"].ToString());
                dicOutput.Add("nam", dtGiaoBSCDV.Rows[0]["nam"].ToString());
                dicOutput.Add("trangthaigiao", dtGiaoBSCDV.Rows[0]["trangthaigiao"].ToString());
                dicOutput.Add("trangthainhan", dtGiaoBSCDV.Rows[0]["trangthainhan"].ToString());
                dicOutput.Add("trangthaicham", dtGiaoBSCDV.Rows[0]["trangthaicham"].ToString());
                dicOutput.Add("trangthaiketthuc", dtGiaoBSCDV.Rows[0]["trangthaiketthuc"].ToString());
                dicOutput.Add("trangthaidongy_kqtd", dtGiaoBSCDV.Rows[0]["trangthaidongy_kqtd"].ToString());
                dicOutput.Add("ten_nv_nhan", dtGiaoBSCDV.Rows[0]["nhanvien_hoten"].ToString());
            }
            else
            {
                dicOutput.Add("nhanviengiao", nhanviengiao.ToString());
                dicOutput.Add("nhanviennhan", nhanviennhan.ToString());
                dicOutput.Add("thang", "0");
                dicOutput.Add("nam", "0");
                dicOutput.Add("trangthaigiao", "0");
                dicOutput.Add("trangthainhan", "0");
                dicOutput.Add("trangthaicham", "0");
                dicOutput.Add("trangthaiketthuc", "0");
                dicOutput.Add("trangthaidongy_kqtd", "0");
                dicOutput.Add("ten_nv_nhan", "");
            }

            return dicOutput;
        }

        [WebMethod]
        public static bool updateKiemDinhStatus(int nhanviengiao, int nhanviennhan, int thang, int nam, int nhanvienthamdinh, int trangthaithamdinh)
        {
            Connection cnNhanBSC = new Connection();
            Message msg = new Message();
            string szMsgContent = "BSC cua ban da duoc tham dinh!!! Ban vui long vao kiem tra.";
            bool isSuccess = false;

            string sqlGiaoBSC = "update bsc_nhanvien set trangthaithamdinh = '" + trangthaithamdinh + "' where nhanviengiao = '" + nhanviengiao + "' and nhanviennhan = '" + nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "' and nhanvienthamdinh = '" + nhanvienthamdinh + "'";
            try
            {
                cnNhanBSC.ThucThiDL(sqlGiaoBSC);
                if (trangthaithamdinh == 1) {
                    msg.SendSMS_ByIDNV(nhanviennhan, szMsgContent);
                }
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool saveData(int nhanviengiao, int nhanviennhan, int thang, int nam, kpiDetail[] kpi_detail)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            try
            {
                for (int i = 0; i < kpi_detail.Length; i++)
                {
                    string sqlInsertBSCDV = "update bsc_nhanvien set thamdinh = '" + kpi_detail[i].thamdinh + "' ";
                    sqlInsertBSCDV += "where nhanviengiao = '" + nhanviengiao + "' ";
                    sqlInsertBSCDV += "and nhanviennhan = '" + nhanviennhan + "' ";
                    sqlInsertBSCDV += "and thang = '" + thang + "' ";
                    sqlInsertBSCDV += "and nam = '" + nam + "' ";
                    sqlInsertBSCDV += "and kpi = '" + kpi_detail[i].kpi_id + "'";
                    try
                    {
                        cnData.ThucThiDL(sqlInsertBSCDV);
                        isSuccess = true;
                    }
                    catch
                    {
                        isSuccess = false;
                        break;
                    }
                }
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Chi tiết kiểm định";
            if (!IsPostBack)
            {
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();

                    nhanviengiao = Request.QueryString["nhanviengiao"];
                    nhanviennhan = Request.QueryString["nhanviennhan"];
                    nhanvienthamdinh = Request.QueryString["nhanvienthamdinh"];
                    thang = Request.QueryString["thang"];
                    nam = Request.QueryString["nam"];

                    if (nhanviengiao == null || nhanviennhan == null || thang == null || nam == null || nhanvienthamdinh == null || nhanvien.nhanvien_id != Convert.ToInt32(nhanvienthamdinh))
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}