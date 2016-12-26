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
    public partial class ChiTietBSCNghiemThu : System.Web.UI.Page
    {
        public static string donvigiao, donvinhan, thang, nam;

        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int donvigiao, int donvinhan, int thang, int nam)
        {
            //string[] arrOutput = {};
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, dvt.dvt_ten as donvitinh, bsc.trongso, bsc.kehoach, bsc.thuchien, bsc.thamdinh, bsc.kq_thuchien, bsc.diem_kpi ";
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='no-sort'>STT</th>";
            outputHTML += "<th class='no-sort'>Chỉ tiêu</th>";
            outputHTML += "<th class='no-sort'>Tỷ trọng (%)</th>";
            outputHTML += "<th class='no-sort'>ĐVT</th>";
            outputHTML += "<th class='no-sort'>Kế hoạch</th>";
            outputHTML += "<th class='no-sort'>Thực hiện</th>";
            outputHTML += "<th class='no-sort'>Thẩm định</th>";
            outputHTML += "<th class='no-sort'>Tỷ lệ thực hiện (%)</th>";
            outputHTML += "<th class='no-sort'>Mức độ hoàn thành</th>";
            outputHTML += "<th class='no-sort'>Điểm</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='10' class='text-center'>No item</td></tr>";
            }
            else
            {
                int nTongTrongSo = 0;
                decimal nTongSoDiem = 0;
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    decimal tylethuchien = 0;
                    int trongso = Convert.ToInt32(gridData.Rows[nKPI]["trongso"].ToString());
                    decimal kehoach = Convert.ToDecimal(gridData.Rows[nKPI]["kehoach"].ToString());
                    decimal thamdinh = Convert.ToDecimal(gridData.Rows[nKPI]["thamdinh"].ToString());
                    decimal mucdohoanthanh = Convert.ToDecimal(gridData.Rows[nKPI]["kq_thuchien"].ToString());
                    decimal diem = Convert.ToDecimal(gridData.Rows[nKPI]["diem_kpi"].ToString());

                    tylethuchien = (thamdinh / kehoach) * 100;
                    nTongTrongSo += trongso;
                    nTongSoDiem += diem;

                    outputHTML += "<tr data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<td>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + " (" + gridData.Rows[nKPI]["kpo_ten"].ToString() + ")" + "</td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["trongso"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["donvitinh"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["kehoach"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["thuchien"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["thamdinh"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + String.Format("{0:0.000}",tylethuchien) + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + String.Format("{0:0.000}", mucdohoanthanh) + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + String.Format("{0:0.000}", diem) + "</strong></td>";
                    outputHTML += "</tr>";
                }

                outputHTML += "<tr>";
                outputHTML += "<td></td>";
                outputHTML += "<td class='text-center'><strong>Tỷ lệ thực hiện</strong></td>";
                outputHTML += "<td class='text-center'><strong>" + nTongTrongSo + "%" + "</strong></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td class='text-center'><strong>" + Convert.ToInt32((nTongSoDiem * 100)) + "%" + "</strong></td>";
                outputHTML += "</tr>";
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);

            /*Lấy danh sách các thông tin còn lại ở bảng giaobscdonvi*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select giaobscdonvi.*, donvinhan.donvi_ten as ten_dvn from giaobscdonvi, donvi as donvinhan ";
            sqlGiaoBSCDV += "where giaobscdonvi.donvigiao = '" + donvigiao + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.donvinhan = '" + donvinhan + "'";
            sqlGiaoBSCDV += "and giaobscdonvi.thang = '" + thang + "'";
            sqlGiaoBSCDV += "and giaobscdonvi.nam = '" + nam + "'";
            sqlGiaoBSCDV += "and giaobscdonvi.donvinhan = donvinhan.donvi_id";

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
                dicOutput.Add("ten_dvn", dtGiaoBSCDV.Rows[0]["ten_dvn"].ToString());
                dicOutput.Add("thang", dtGiaoBSCDV.Rows[0]["thang"].ToString());
                dicOutput.Add("nam", dtGiaoBSCDV.Rows[0]["nam"].ToString());
                dicOutput.Add("trangthaigiao", dtGiaoBSCDV.Rows[0]["trangthaigiao"].ToString());
                dicOutput.Add("trangthainhan", dtGiaoBSCDV.Rows[0]["trangthainhan"].ToString());
                dicOutput.Add("trangthaicham", dtGiaoBSCDV.Rows[0]["trangthaicham"].ToString());
                dicOutput.Add("trangthaidongy_kqtd", dtGiaoBSCDV.Rows[0]["trangthaidongy_kqtd"].ToString());
                dicOutput.Add("trangthaiketthuc", dtGiaoBSCDV.Rows[0]["trangthaiketthuc"].ToString());
            }
            else
            {
                dicOutput.Add("donvigiao", donvigiao.ToString());
                dicOutput.Add("donvinhan", donvinhan.ToString());
                dicOutput.Add("ten_dvn", "");
                dicOutput.Add("thang", "0");
                dicOutput.Add("nam", "0");
                dicOutput.Add("trangthaigiao", "0");
                dicOutput.Add("trangthainhan", "0");
                dicOutput.Add("trangthaicham", "0");
                dicOutput.Add("trangthaidongy_kqtd", "0");
                dicOutput.Add("trangthaiketthuc", "0");
            }

            return dicOutput;
        }

        [WebMethod]
        public static bool updateKetThucStatus(int donvigiao, int donvinhan, int thang, int nam)
        {
            Connection cnNhanBSC = new Connection();
            bool isSuccess = false;

            string sqlGiaoBSC = "update giaobscdonvi set trangthaiketthuc = 1 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
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
            if (!IsPostBack)
            {
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();

                    donvigiao = Request.QueryString["donvigiao"];
                    donvinhan = Request.QueryString["donvinhan"];
                    thang = Request.QueryString["thang"];
                    nam = Request.QueryString["nam"];

                    if (donvigiao == null || donvinhan == null || thang == null || nam == null || nhanvien.nhanvien_donvi_id != Convert.ToInt32(donvigiao))
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