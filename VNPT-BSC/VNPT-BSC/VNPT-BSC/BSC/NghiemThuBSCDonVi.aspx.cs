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
    public partial class NghiemThuBSCDonVi : System.Web.UI.Page
    {
        public static string donvigiao;
        /*Lấy danh sách BSC được giao theo năm*/
        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam, int donvigiao)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select giaobsc.*, dvnhan.donvi_ten as tendvn, dvthamdinh.donvi_ten as tendvtd ";
            sqlBSC += "from giaobscdonvi giaobsc, donvi dvgiao, donvi dvnhan, donvi dvthamdinh ";
            sqlBSC += "where giaobsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and giaobsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and giaobsc.donvithamdinh = dvthamdinh.donvi_id ";
            sqlBSC += "and giaobsc.thang = '" + thang + "' ";
            sqlBSC += "and giaobsc.nam = '" + nam + "' ";
            sqlBSC += "and giaobsc.donvigiao = '" + donvigiao + "' ";

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
            outputHTML += "<th>Đơn vị nhận</th>";
            outputHTML += "<th>Đơn vị thẩm định</th>";
            outputHTML += "<th>Ngày áp dụng</th>";
            outputHTML += "<th>Trạng thái nhận</th>";
            outputHTML += "<th>Trạng thái nộp</th>";
            outputHTML += "<th>Trạng thái thẩm định</th>";
            outputHTML += "<th>Trạng thái kết thúc</th>";
            outputHTML += "<th></th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='9' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nIndex = 0; nIndex < gridData.Rows.Count; nIndex++)
                {
                    string szDonvigiao = gridData.Rows[nIndex]["donvigiao"].ToString();
                    string szDonvinhan = gridData.Rows[nIndex]["donvinhan"].ToString();
                    string szThang = gridData.Rows[nIndex]["thang"].ToString();
                    string szNam = gridData.Rows[nIndex]["nam"].ToString();
                    string trangthainhan = gridData.Rows[nIndex]["trangthainhan"].ToString();
                    string trangthaicham = gridData.Rows[nIndex]["trangthaicham"].ToString();
                    string trangthaithamdinh = gridData.Rows[nIndex]["trangthaithamdinh"].ToString();
                    string trangthaiketthuc = gridData.Rows[nIndex]["trangthaiketthuc"].ToString();
                    string txtTrangThaiNhan = "Chưa nhận";
                    string txtTrangThaiCham = "Chưa nộp";
                    string txtTrangThaiThamDinh = "Chưa thẩm định";
                    string txtTrangThaiKetThuc = "Chưa kết thúc";
                    string clsTrangThaiNhan = "label-default";
                    string clsTrangThaiCham = "label-default";
                    string clsTrangThaiThamDinh = "label-default";
                    string clsTrangThaiKetThuc = "label-default";

                    if (trangthainhan == "True")
                    {
                        txtTrangThaiNhan = "Đã nhận";
                        clsTrangThaiNhan = "label-success";
                    }

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
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tendvn"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tendvtd"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'><strong>" + szThang + "/" + szNam + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiNhan + "'>" + txtTrangThaiNhan + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiCham + "'>" + txtTrangThaiCham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + txtTrangThaiThamDinh + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiKetThuc + "'>" + txtTrangThaiKetThuc + "</span></td>";
                    outputHTML += "<td class='text-center'><a class='" + "btn btn-primary detail" + "' onclick='xemChiTiet(" + szThang + ", " + szNam + ", " + szDonvigiao + ", " + szDonvinhan + ")'>Chi tiết</a></td>";
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
                Session["donvigiao"] = 1;
                donvigiao = Session["donvigiao"].ToString();
            }
        }
    }
}