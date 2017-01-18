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
    public partial class NghiemThuBSCNhanVien : System.Web.UI.Page
    {
        public static string nhanviengiao;

        /*Lấy danh sách BSC được giao theo năm*/
        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam, int nhanviengiao)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select giaobsc.*, nvnhan.nhanvien_hoten as tendvn ";
            sqlBSC += "from giaobscnhanvien giaobsc, nhanvien nvgiao, nhanvien nvnhan ";
            sqlBSC += "where giaobsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and giaobsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and giaobsc.thang = '" + thang + "' ";
            sqlBSC += "and giaobsc.nam = '" + nam + "' ";
            sqlBSC += "and giaobsc.nhanviengiao = '" + nhanviengiao + "' ";

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
            outputHTML += "<th>Nhân viên nhận</th>";
            outputHTML += "<th>Ngày áp dụng</th>";
            outputHTML += "<th>Trạng thái nhận</th>";
            outputHTML += "<th>Trạng thái nộp</th>";
            outputHTML += "<th>Trạng thái đồng ý KQTĐ</th>";
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
                    string szNhanviengiao = gridData.Rows[nIndex]["nhanviengiao"].ToString();
                    string szNhanviennhan = gridData.Rows[nIndex]["nhanviennhan"].ToString();
                    string szThang = gridData.Rows[nIndex]["thang"].ToString();
                    string szNam = gridData.Rows[nIndex]["nam"].ToString();
                    string trangthainhan = gridData.Rows[nIndex]["trangthainhan"].ToString();
                    string trangthaicham = gridData.Rows[nIndex]["trangthaicham"].ToString();
                    string trangthaidongy_kqtd = gridData.Rows[nIndex]["trangthaidongy_kqtd"].ToString();
                    string trangthaiketthuc = gridData.Rows[nIndex]["trangthaiketthuc"].ToString();
                    string txtTrangThaiNhan = "Chưa nhận";
                    string txtTrangThaiCham = "Chưa nộp";
                    string txtTrangThaiDongY = "Chưa đồng ý";
                    string txtTrangThaiKetThuc = "Chưa kết thúc";
                    string clsTrangThaiNhan = "label-default";
                    string clsTrangThaiCham = "label-default";
                    string clsTrangThaiDongY = "label-default";
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

                    if (trangthaidongy_kqtd == "True")
                    {
                        txtTrangThaiDongY = "Đã đồng ý";
                        clsTrangThaiDongY = "label-success";
                    }

                    if (trangthaiketthuc == "True")
                    {
                        txtTrangThaiKetThuc = "Đã kết thúc";
                        clsTrangThaiKetThuc = "label-success";
                    }

                    outputHTML += "<tr>";
                    outputHTML += "<td class='text-center'>" + (nIndex + 1) + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tendvn"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'><strong>" + szThang + "/" + szNam + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiNhan + "'>" + txtTrangThaiNhan + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiCham + "'>" + txtTrangThaiCham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiDongY + "'>" + txtTrangThaiDongY + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiKetThuc + "'>" + txtTrangThaiKetThuc + "</span></td>";
                    outputHTML += "<td class='text-center'><a class='" + "btn btn-primary detail btn-xs" + "' onclick='xemChiTiet(" + szThang + ", " + szNam + ", " + szNhanviengiao + ", " + szNhanviennhan + ")'>Chi tiết</a></td>";
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
            this.Title = "Nghiệm thu bsc";
            if (!IsPostBack)
            {
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

                    if (nhanvien == null || !nFindResult)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    nhanviengiao = nhanvien.nhanvien_id.ToString();
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}