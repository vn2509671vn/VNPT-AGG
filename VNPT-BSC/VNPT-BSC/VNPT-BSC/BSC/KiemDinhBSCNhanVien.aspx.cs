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
    public partial class KiemDinhBSCNhanVien : System.Web.UI.Page
    {
        public static string nhanvienkiemdinh;

        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int nam, int nhanvienkiemdinh)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select giaobsc.*, nvgiao.nhanvien_hoten as tennvg, nvnhan.nhanvien_hoten as tennvn ";
            sqlBSC += "from giaobscnhanvien giaobsc, nhanvien nvgiao, nhanvien nvnhan, nhanvien nvthamdinh ";
            sqlBSC += "where giaobsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and giaobsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and giaobsc.nhanvienthamdinh = nvthamdinh.nhanvien_id ";
            sqlBSC += "and giaobsc.nam = '" + nam + "' ";
            sqlBSC += "and giaobsc.nhanvienthamdinh = '" + nhanvienkiemdinh + "' ";

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
            outputHTML += "<th>Nhân viên giao</th>";
            outputHTML += "<th>Nhân viên nhận</th>";
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
                    string nhanviengiao = gridData.Rows[nIndex]["nhanviengiao"].ToString();
                    string szNhanviennhan = gridData.Rows[nIndex]["nhanviennhan"].ToString();
                    string szNhanvienthamdinh = gridData.Rows[nIndex]["nhanvienthamdinh"].ToString();
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
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tennvg"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tennvn"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'><strong>" + thang + "/" + nNam + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiCham + "'>" + txtTrangThaiCham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + txtTrangThaiThamDinh + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiKetThuc + "'>" + txtTrangThaiKetThuc + "</span></td>";
                    outputHTML += "<td class='text-center'><a class='" + "btn btn-primary detail" + "' onclick='xemChiTiet(" + thang + ", " + nam + ", " + nhanviengiao + ", " + szNhanviennhan + ", " + szNhanvienthamdinh + ")'>Chi tiết</a></td>";
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
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();

                if (nhanvien == null || nhanvien.nhanvien_id == 2 && nhanvien.nhanvien_id == 4)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

                nhanvienkiemdinh = nhanvien.nhanvien_id.ToString();
            }
        }
    }
}