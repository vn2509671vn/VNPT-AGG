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
    public partial class NhanBSCDonVi : System.Web.UI.Page
    {
        public static string donvinhan;
        /*Lấy danh sách BSC được giao theo năm*/
        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int nam, int donvinhan) {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select giaobsc.*, dvgiao.donvi_ten as tendvg ";
            sqlBSC += "from giaobscdonvi giaobsc, donvi dvgiao, donvi dvnhan ";
            sqlBSC += "where giaobsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and giaobsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and giaobsc.nam = '" + nam + "' ";
            sqlBSC += "and giaobsc.donvinhan = '" + donvinhan + "' ";
            sqlBSC += "and giaobsc.trangthaigiao = 1 ";

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
            outputHTML += "<th>Ngày áp dụng</th>";
            outputHTML += "<th>Trạng thái nhận</th>";
            outputHTML += "<th>Trạng thái chấm</th>";
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
                    string donvigiao = gridData.Rows[nIndex]["donvigiao"].ToString();
                    string szDonvinhan = gridData.Rows[nIndex]["donvinhan"].ToString();
                    string thang = gridData.Rows[nIndex]["thang"].ToString();
                    string nNam = gridData.Rows[nIndex]["nam"].ToString();
                    string trangthainhan = gridData.Rows[nIndex]["trangthainhan"].ToString();
                    string trangthaicham = gridData.Rows[nIndex]["trangthaicham"].ToString();
                    string trangthaidongythamdinh = gridData.Rows[nIndex]["trangthaidongy_kqtd"].ToString();
                    string trangthaiketthuc = gridData.Rows[nIndex]["trangthaiketthuc"].ToString();
                    string txtTrangThaiNhan = "Chưa nhận";
                    string txtTrangThaiCham = "Chưa nộp";
                    string txtTrangThaiDongYThamDinh = "Chưa đồng ý KQTĐ";
                    string txtTrangThaiKetThuc = "Chưa kết thúc";
                    string clsTrangThaiNhan = "label-default";
                    string clsTrangThaiCham = "label-default";
                    string clsTrangThaiDongYThamDinh = "label-default";
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

                    if (trangthaidongythamdinh == "True")
                    {
                        txtTrangThaiDongYThamDinh = "Đã đồng ý";
                        clsTrangThaiDongYThamDinh = "label-success";
                    }

                    if (trangthaiketthuc == "True")
                    {
                        txtTrangThaiKetThuc = "Đã kết thúc";
                        clsTrangThaiKetThuc = "label-success";
                    }

                    outputHTML += "<tr>";
                    outputHTML += "<td class='text-center'>" + (nIndex + 1) + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tendvg"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'><strong>" + thang + "/" + nNam + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiNhan + "'>" + txtTrangThaiNhan + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiCham + "'>" + txtTrangThaiCham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiDongYThamDinh + "'>" + txtTrangThaiDongYThamDinh + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiKetThuc + "'>" + txtTrangThaiKetThuc + "</span></td>";
                    outputHTML += "<td class='text-center'><a class='" + "btn btn-primary detail" + "' onclick='xemChiTiet(" + thang + ", " + nam + ", " + donvigiao + ", " + szDonvinhan + ")'>Chi tiết</a></td>";
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
            if (!IsPostBack) {
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();
                    /*Nếu không tồn tại session hoặc chức vụ của nhân viên không phải Trưởng phòng hoặc GĐ phòng bán hàng thì trở về trang login*/
                    if (nhanvien == null || nhanvien.nhanvien_chucvu_id != 3 && nhanvien.nhanvien_chucvu_id != 5)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    donvinhan = nhanvien.nhanvien_donvi_id.ToString();
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
            
        }
    }
}