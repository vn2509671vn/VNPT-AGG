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
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam, int nhanvienkiemdinh)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";

            string sqlBSC = "select nvgiao.nhanvien_id as nhanviengiao, nvgiao.nhanvien_hoten as tennvg, nvnhan.nhanvien_id as nhanviennhan, nvnhan.nhanvien_hoten as tennvn, giaobsc.nam, giaobsc.thang, giaobsc.trangthaicham, giaobsc.trangthaidongy_kqtd, giaobsc.trangthaiketthuc,  ";
            sqlBSC += "(select COUNT(*) from bsc_nhanvien where bsc_nhanvien.nhanvienthamdinh = '" + nhanvienkiemdinh + "' and bsc_nhanvien.trangthaithamdinh = 0 and bsc_nhanvien.nam = '" + nam + "' and bsc_nhanvien.thang = '" + thang + "' and bsc_nhanvien.nhanviennhan = nvnhan.nhanvien_id) as sl_chuatd ";
            sqlBSC += "from giaobscnhanvien giaobsc, bsc_nhanvien giaobsc_nv, nhanvien nvgiao, nhanvien nvnhan ";
            sqlBSC += "where giaobsc.thang = giaobsc_nv.thang ";
            sqlBSC += "and giaobsc.nam = giaobsc_nv.nam ";
            sqlBSC += "and giaobsc.nhanviengiao = giaobsc_nv.nhanviengiao ";
            sqlBSC += "and giaobsc.nhanviennhan = giaobsc_nv.nhanviennhan ";
            sqlBSC += "and giaobsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and giaobsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and giaobsc.nam = '" + nam + "' ";
            sqlBSC += "and giaobsc.thang = '" + thang + "' ";
            sqlBSC += "and giaobsc_nv.nhanvienthamdinh = '" + nhanvienkiemdinh + "' ";
            sqlBSC += "group by nvgiao.nhanvien_id, nvgiao.nhanvien_hoten, nvnhan.nhanvien_id, nvnhan.nhanvien_hoten, giaobsc.nam, giaobsc.thang, giaobsc.trangthaicham, giaobsc.trangthaidongy_kqtd, giaobsc.trangthaiketthuc";

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
                    string szNhanvienthamdinh = nhanvienkiemdinh.ToString();
                    string nThang = gridData.Rows[nIndex]["thang"].ToString();
                    string nNam = gridData.Rows[nIndex]["nam"].ToString();
                    string sl_chuathamdinh = gridData.Rows[nIndex]["sl_chuatd"].ToString();
                    string trangthaicham = gridData.Rows[nIndex]["trangthaicham"].ToString();
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

                    if (Convert.ToInt32(sl_chuathamdinh) == 0)
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
                    outputHTML += "<td class='text-center'><strong>" + nThang + "/" + nNam + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiCham + "'>" + txtTrangThaiCham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + txtTrangThaiThamDinh + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiKetThuc + "'>" + txtTrangThaiKetThuc + "</span></td>";
                    outputHTML += "<td class='text-center'><a class='" + "btn btn-primary detail" + "' onclick='xemChiTiet(" + nThang + ", " + nam + ", " + nhanviengiao + ", " + szNhanviennhan + ", " + szNhanvienthamdinh + ")'>Chi tiết</a></td>";
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
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();

                    // Khai báo các biến cho việc kiểm tra quyền
                    int[] quyenHeThong = { };
                    int nFindResult = -1;
                    quyenHeThong = Session.GetRole();

                    /*Kiểm tra nếu không có quyền giao bsc nhân viên (id của quyền là 3) thì đẩy ra trang đăng nhập*/
                    nFindResult = Array.IndexOf(quyenHeThong, 3);

                    if (nhanvien == null || nFindResult == -1)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    nhanvienkiemdinh = nhanvien.nhanvien_id.ToString();
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}