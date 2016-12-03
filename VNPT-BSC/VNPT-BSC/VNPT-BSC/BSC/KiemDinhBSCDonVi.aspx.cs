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
    public partial class KiemDinhBSCDonVi : System.Web.UI.Page
    {
        public static string donvikiemdinh;

        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int nam, int donvikiemdinh)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select giaobsc.*, dvgiao.donvi_ten as tendvg, dvnhan.donvi_ten as tendvn ";
            sqlBSC += "from giaobscdonvi giaobsc, donvi dvgiao, donvi dvnhan, donvi dvthamdinh ";
            sqlBSC += "where giaobsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and giaobsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and giaobsc.donvithamdinh = dvthamdinh.donvi_id ";
            sqlBSC += "and giaobsc.nam = '" + nam + "' ";
            sqlBSC += "and giaobsc.donvithamdinh = '" + donvikiemdinh + "' ";

            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-bsclist' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th>STT</th>";
            outputHTML += "<th>Đơn vị giao</th>";
            outputHTML += "<th>Đơn vị nhận</th>";
            outputHTML += "<th>Ngày áp dụng</th>";
            outputHTML += "<th>Trạng thái nộp</th>";
            outputHTML += "<th>Trạng thái thẩm định</th>";
            outputHTML += "<th>Trạng thái kết thúc</th>";
            outputHTML += "<th></th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='8' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nIndex = 0; nIndex < gridData.Rows.Count; nIndex++)
                {
                    string donvigiao = gridData.Rows[nIndex]["donvigiao"].ToString();
                    string szDonvinhan = gridData.Rows[nIndex]["donvinhan"].ToString();
                    string szDonvithamdinh = gridData.Rows[nIndex]["donvithamdinh"].ToString();
                    string thang = gridData.Rows[nIndex]["thang"].ToString();
                    string nNam = gridData.Rows[nIndex]["nam"].ToString();
                    string trangthainhan = gridData.Rows[nIndex]["trangthainhan"].ToString();
                    string trangthaicham = gridData.Rows[nIndex]["trangthaicham"].ToString();
                    string trangthaithamdinh = gridData.Rows[nIndex]["trangthaithamdinh"].ToString();
                    string trangthaiketthuc = gridData.Rows[nIndex]["trangthaiketthuc"].ToString();
                    string txtTrangThaiCham = "Chưa nộp";
                    string txtTrangThaiThamDinh = "Chưa thẩm định";
                    string txtTrangThaiKetThuc = "Chưa kết thúc";
                    string clsTrangThaiCham = "label-default";
                    string clsTrangThaiThamDinh = "label-default";
                    string clsTrangThaiKetThuc = "label-default";

                    if (trangthaicham == "True")
                    {
                        txtTrangThaiCham = "Đã nộp";
                        clsTrangThaiCham = "label-success";
                    }

                    if (trangthaithamdinh == "True")
                    {
                        txtTrangThaiThamDinh = "Đã thẩm định";
                        clsTrangThaiThamDinh = "label-success";
                    }

                    if (trangthaiketthuc == "True")
                    {
                        txtTrangThaiKetThuc = "Đã kết thúc";
                        clsTrangThaiKetThuc = "label-success";
                    }

                    outputHTML += "<tr>";
                    outputHTML += "<td class='text-center'>" + (nIndex + 1) + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tendvg"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tendvn"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'><strong>" + thang + "/" + nNam + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiCham + "'>" + txtTrangThaiCham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + txtTrangThaiThamDinh + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiKetThuc + "'>" + txtTrangThaiKetThuc + "</span></td>";
                    outputHTML += "<td class='text-center'><a class='" + "btn btn-primary detail" + "' onclick='xemChiTiet(" + thang + ", " + nam + ", " + donvigiao + ", " + szDonvinhan + ", " + szDonvithamdinh + ")'>Chi tiết</a></td>";
                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);

            return dicOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["donvikiemdinh"] = 1;
                donvikiemdinh = Session["donvikiemdinh"].ToString();
            }
        }
    }
}