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
    public partial class ChiTietQLGiaoBSCDonVi : System.Web.UI.Page
    {
        public static string donvigiao, donvinhan, donvithamdinh, thang, nam;
        public static DataTable dtDVT = new DataTable();
        public class kpiDetail
        {
            public int kpi_id { get; set; }
            public decimal trongso { get; set; }
            public int donvitinh { get; set; }
            public int donvithamdinh { get; set; }
            public decimal kehoach { get; set; }
            public decimal thuchien { get; set; }
            public decimal thamdinh { get; set; }
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int donvigiao, int donvinhan, int thang, int nam)
        {
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            DataTable dsDonvitinh = new DataTable();
            DataTable dsDonvithamdinh = new DataTable();
            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            string sqlDVT = "select * from donvitinh";
            string sqlDVTD = "select * from donvi";
            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, dvt.dvt_id as donvitinh, bsc.trongso, bsc.kehoach, bsc.thuchien, bsc.thamdinh, bsc.trangthaithamdinh, bsc.donvithamdinh ";
            sqlBSC += "from bsc_donvi bsc, kpi, kpo, donvi dvgiao, donvi dvnhan, donvitinh dvt ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.donvitinh = dvt.dvt_id ";
            sqlBSC += "and bsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and bsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and bsc.donvinhan = '" + donvinhan + "' ";
            sqlBSC += "and bsc.donvigiao = '" + donvigiao + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "'";
            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
                dsDonvitinh = cnBSC.XemDL(sqlDVT);
                dsDonvithamdinh = cnBSC.XemDL(sqlDVTD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th>STT</th>";
            outputHTML += "<th>Chỉ tiêu</th>";
            outputHTML += "<th>Tỷ trọng (%)</th>";
            outputHTML += "<th>ĐVT</th>";
            outputHTML += "<th>Kế hoạch</th>";
            outputHTML += "<th>Thực hiện</th>";
            outputHTML += "<th>Thẩm định</th>";
            outputHTML += "<th>Đơn vị thẩm định</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='8' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    outputHTML += "<tr data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<td>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td class='min-width-130'>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + " (" + gridData.Rows[nKPI]["kpo_ten"].ToString() + ")" + "</td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='trongso' id='trongso_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' value='" + gridData.Rows[nKPI]["trongso"].ToString() + "' onkeypress='return onlyNumbers(event)'/></td>";
                    //outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["donvitinh"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'>";
                    outputHTML += "<select class='form-control' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    for (int nDVT = 0; nDVT < dsDonvitinh.Rows.Count; nDVT++)
                    {
                        if (dsDonvitinh.Rows[nDVT]["dvt_id"].ToString() == gridData.Rows[nKPI]["donvitinh"].ToString())
                        {
                            outputHTML += "<option value='" + dsDonvitinh.Rows[nDVT]["dvt_id"] + "' selected>" + dsDonvitinh.Rows[nDVT]["dvt_ten"] + "</option>";
                        }
                        else
                        {
                            outputHTML += "<option value='" + dsDonvitinh.Rows[nDVT]["dvt_id"] + "'>" + dsDonvitinh.Rows[nDVT]["dvt_ten"] + "</option>";
                        }
                    }
                    outputHTML += "</select>";
                    outputHTML += "</td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='kehoach' id='kehoach_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' value='" + gridData.Rows[nKPI]["kehoach"].ToString() + "' onkeypress='return onlyNumbers(event)'/></td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='thuchien' id='thuchien_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' value='" + gridData.Rows[nKPI]["thuchien"].ToString() + "' onkeypress='return onlyNumbers(event)'/></td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='thamdinh' id='thamdinh_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' value='" + gridData.Rows[nKPI]["thamdinh"].ToString() + "' onkeypress='return onlyNumbers(event)'/></td>";
                    // Đơn vị thẩm định
                    outputHTML += "<td class='text-center'>";
                    outputHTML += "<select class='form-control' id='dvtd_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    for (int nDVTD = 0; nDVTD < dsDonvithamdinh.Rows.Count; nDVTD++)
                    {
                        if (dsDonvithamdinh.Rows[nDVTD]["donvi_id"].ToString() == gridData.Rows[nKPI]["donvithamdinh"].ToString())
                        {
                            outputHTML += "<option value='" + dsDonvithamdinh.Rows[nDVTD]["donvi_id"] + "' selected>" + dsDonvithamdinh.Rows[nDVTD]["donvi_ten"] + "</option>";
                        }
                        else
                        {
                            outputHTML += "<option value='" + dsDonvithamdinh.Rows[nDVTD]["donvi_id"] + "'>" + dsDonvithamdinh.Rows[nDVTD]["donvi_ten"] + "</option>";
                        }
                    }
                    outputHTML += "</select>";
                    outputHTML += "</td>";

                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);

            /*Lấy danh sách các thông tin còn lại ở bảng giaobscdonvi*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select giaobscdonvi.*, dvgiao.donvi_ten as ten_dvg, dvnhan.donvi_ten as ten_dvn from giaobscdonvi, donvi dvgiao, donvi dvnhan ";
            sqlGiaoBSCDV += "where giaobscdonvi.donvigiao = '" + donvigiao + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.donvinhan = '" + donvinhan + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.thang = '" + thang + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.nam = '" + nam + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.donvigiao = dvgiao.donvi_id ";
            sqlGiaoBSCDV += "and giaobscdonvi.donvinhan = dvnhan.donvi_id";
            
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
                dicOutput.Add("donvigiao", dtGiaoBSCDV.Rows[0]["donvigiao"].ToString());
                dicOutput.Add("donvinhan", dtGiaoBSCDV.Rows[0]["donvinhan"].ToString());
                dicOutput.Add("thang", dtGiaoBSCDV.Rows[0]["thang"].ToString());
                dicOutput.Add("nam", dtGiaoBSCDV.Rows[0]["nam"].ToString());
                dicOutput.Add("ten_dvg", dtGiaoBSCDV.Rows[0]["ten_dvg"].ToString());
                dicOutput.Add("ten_dvn", dtGiaoBSCDV.Rows[0]["ten_dvn"].ToString());
            }
            else
            {
                dicOutput.Add("donvigiao", donvigiao.ToString());
                dicOutput.Add("donvinhan", donvinhan.ToString());
                dicOutput.Add("thang", "0");
                dicOutput.Add("nam", "0");
                dicOutput.Add("ten_dvg", "");
                dicOutput.Add("ten_dvn", "");
            }

            return dicOutput;
        }

        [WebMethod]
        public static bool saveData(int donvigiao, int donvinhan, int thang, int nam, kpiDetail[] kpi_detail)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            try
            {
                for (int i = 0; i < kpi_detail.Length; i++)
                {
                    string sqlInsertBSCDV = "update bsc_donvi set thamdinh = '" + kpi_detail[i].thamdinh + "', ";
                    sqlInsertBSCDV += "thuchien = '" + kpi_detail[i].thuchien + "', ";
                    sqlInsertBSCDV += "kehoach = '" + kpi_detail[i].kehoach + "', ";
                    sqlInsertBSCDV += "trongso = '" + kpi_detail[i].trongso + "', ";
                    sqlInsertBSCDV += "donvitinh = '" + kpi_detail[i].donvitinh + "', ";
                    sqlInsertBSCDV += "donvithamdinh = '" + kpi_detail[i].donvithamdinh + "' ";
                    sqlInsertBSCDV += "where donvigiao = '" + donvigiao + "' ";
                    sqlInsertBSCDV += "and donvinhan = '" + donvinhan + "' ";
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
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();

                donvigiao = Request.QueryString["donvigiao"];
                donvinhan = Request.QueryString["donvinhan"];
                thang = Request.QueryString["thang"];
                nam = Request.QueryString["nam"];

                if (donvigiao == null || donvinhan == null || thang == null || nam == null || nhanvien.nhanvien_chucvu_id != 30)
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