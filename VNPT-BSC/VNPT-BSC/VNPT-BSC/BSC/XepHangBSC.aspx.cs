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
    public partial class XepHangBSC : System.Web.UI.Page
    {
        [WebMethod]
        public static string loadBSC(int thang, int nam)
        {
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            DataTable gridDataTTKD = new DataTable();

            string sqlBSC = "select donvi.donvi_id, donvi.donvi_ten, (select SUM(((kq_thuchien*trongso)/100)) from bsc_donvi where donvinhan = donvi.donvi_id and thang = '" + thang + "' and nam = '" + nam + "') as diem ";
            sqlBSC += "from donvi, bsc_donvi ";
            sqlBSC += "where bsc_donvi.donvinhan = donvi.donvi_id ";
            sqlBSC += "and donvi.donvi_loai = 1 ";
            sqlBSC += "and bsc_donvi.thang = '" + thang + "' and bsc_donvi.nam = '" + nam + "' ";
            sqlBSC += "group by donvi.donvi_id, donvi.donvi_ten ";
            sqlBSC += "ORDER BY diem DESC";

            string sqlTTKD = "select donvi.donvi_id, donvi.donvi_ten, (select SUM(((kq_thuchien*trongso)/100)) from bsc_donvi where donvinhan = donvi.donvi_id and thang = '" + thang + "' and nam = '" + nam + "') as diem ";
            sqlTTKD += "from donvi, bsc_donvi ";
            sqlTTKD += "where bsc_donvi.donvinhan = donvi.donvi_id ";
            sqlTTKD += "and bsc_donvi.thang = '" + thang + "' and bsc_donvi.nam = '" + nam + "' ";
            sqlTTKD += "and donvi.donvi_id = 1 ";
            sqlTTKD += "group by donvi.donvi_id, donvi.donvi_ten ";
            sqlTTKD += "ORDER BY diem DESC";

            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
                gridDataTTKD = cnBSC.XemDL(sqlTTKD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string arrOutput = "";
            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            arrOutput += "<thead>";
            arrOutput += "<tr>";
            arrOutput += "<th class='text-center'>STT</th>";
            arrOutput += "<th class='text-center'>Đơn vị</th>";
            arrOutput += "<th class='text-center'>Điểm xếp hạng</th>";
            arrOutput += "<th class='text-center'>Xếp hạng</th>";
            arrOutput += "<th class='hide'>Chú thích</th>";
            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                arrOutput += "<tr><td colspan='4' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    decimal diem = 0;
                    if (gridData.Rows[nKPI]["diem"].ToString() != "")
                    {
                        diem = Convert.ToDecimal(gridData.Rows[nKPI]["diem"].ToString()) * 100;
                    }
                    arrOutput += "<tr>";
                    arrOutput += "<td style='text-align: center'>" + (nKPI + 1) + "</td>";
                    arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[nKPI]["donvi_ten"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0.00}", diem) + "%" + "</strong></td>";
                    arrOutput += "<td style='text-align: center'>" + (nKPI + 1) + "</td>";
                    arrOutput += "<td class='hide'></td>";
                    arrOutput += "</tr>";
                }
            }

            if (gridDataTTKD.Rows.Count > 0) {
                for (int nKPITTKD = 0; nKPITTKD < gridDataTTKD.Rows.Count; nKPITTKD++)
                {
                    decimal diem = 0;
                    if (gridDataTTKD.Rows[nKPITTKD]["diem"].ToString() != "")
                    {
                        diem = Convert.ToDecimal(gridDataTTKD.Rows[nKPITTKD]["diem"].ToString()) * 100;
                    }
                    arrOutput += "<tr>";
                    arrOutput += "<td class='text-center'></td>";
                    arrOutput += "<td class='min-width-130'><strong>" + gridDataTTKD.Rows[nKPITTKD]["donvi_ten"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0.00}", diem) + "%" + "</strong></td>";
                    arrOutput += "<td class='text-center'></td>";
                    arrOutput += "<td></td>";
                    arrOutput += "</tr>";
                }
            }
            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Danh sách xếp hạng";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();

                //// Khai báo các biến cho việc kiểm tra quyền
                //List<int> quyenHeThong = new List<int>();
                //bool nFindResult = false;
                //quyenHeThong = Session.GetRole();

                ///*Kiểm tra nếu không có quyền giao bsc đơn vị (id của quyền là 2) thì đẩy ra trang đăng nhập*/
                //nFindResult = quyenHeThong.Contains(2);
                if (nhanvien == null)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}