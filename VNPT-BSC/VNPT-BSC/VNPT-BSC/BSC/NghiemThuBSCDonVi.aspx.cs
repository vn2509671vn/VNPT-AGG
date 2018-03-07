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
        //public static string donvigiao;
        /*Lấy danh sách BSC được giao theo năm*/
        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            int soluongDongY = 0;
            int soluongKetthuc = 0;
            string outputHTML = "";
            string sqlBSC = "select giaobsc.*, dvnhan.donvi_ten as tendvn ";
            sqlBSC += "from giaobscdonvi giaobsc, donvi dvgiao, donvi dvnhan ";
            sqlBSC += "where giaobsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and giaobsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and giaobsc.thang = '" + thang + "' ";
            sqlBSC += "and giaobsc.nam = '" + nam + "' ";

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
            outputHTML += "<th class='text-center'>STT</th>";
            outputHTML += "<th class='text-center'>Đơn vị nhận</th>";
            outputHTML += "<th class='text-center'>Ngày áp dụng</th>";
            outputHTML += "<th class='text-center'>Trạng thái nhận</th>";
            outputHTML += "<th class='text-center'>Trạng thái nộp</th>";
            outputHTML += "<th class='text-center'>Đồng ý kết quả thẩm định</th>";
            outputHTML += "<th class='text-center'>Trạng thái kết thúc</th>";
            outputHTML += "<th></th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='8' class='text-center'>No item</td></tr>";
                dicOutput.Add("isKetThucTatCa", "False");
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
                        soluongDongY++;
                    }

                    if (trangthaiketthuc == "True")
                    {
                        txtTrangThaiKetThuc = "Đã kết thúc";
                        clsTrangThaiKetThuc = "label-success";
                        soluongKetthuc++;
                    }

                    outputHTML += "<tr>";
                    outputHTML += "<td class='text-center'>" + (nIndex + 1) + "</td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nIndex]["tendvn"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + szThang + "/" + szNam + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiNhan + "'>" + txtTrangThaiNhan + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiCham + "'>" + txtTrangThaiCham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiDongY + "'>" + txtTrangThaiDongY + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiKetThuc + "'>" + txtTrangThaiKetThuc + "</span></td>";
                    outputHTML += "<td class='text-center'><a class='" + "btn btn-primary detail btn-xs" + "' onclick='xemChiTiet(" + szThang + ", " + szNam + ", " + szDonvigiao + ", " + szDonvinhan + ")'>Chi tiết</a></td>";
                    outputHTML += "</tr>";
                }

                // Nếu tất cả các đơn vị đều đồng ý kết quả thẩm định thì cho phép kết thúc tất cả
                if (soluongDongY == gridData.Rows.Count && soluongKetthuc < gridData.Rows.Count)
                {
                    dicOutput.Add("isKetThucTatCa", "True");
                }
                else {
                    dicOutput.Add("isKetThucTatCa", "False");
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);

            return dicOutput;
        }

        [WebMethod]
        public static bool nghiemthuTatCa(int thang, int nam) {
            Connection cn = new Connection();
            bool bResult = false;
            string sql = "update giaobscdonvi set trangthaiketthuc = 1 where thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cn.ThucThiDL(sql);
                bResult = true;
            }
            catch {
                bResult = false;
            }
            return bResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Nghiệm thu bsc";
            //if (!IsPostBack)
            //{
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    //nhanvien = Session.GetCurrentUser();
                    nhanvien = (Nhanvien)Session["nhanvien"];

                    // Khai báo các biến cho việc kiểm tra quyền
                    List<int> quyenHeThong = new List<int>();
                    bool nFindResult = false;
                    quyenHeThong = (List<int>)Session["quyenhethong"];

                    /*Kiểm tra nếu không có quyền giao bsc đơn vị (id của quyền là 2) thì đẩy ra trang đăng nhập*/
                    nFindResult = quyenHeThong.Contains(2);

                    if (nhanvien == null || !nFindResult)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../index.aspx';</script>");
                    }
                    //donvigiao = nhanvien.nhanvien_donvi_id.ToString();
                }
                catch {
                    Response.Write("<script>window.location.href='../index.aspx';</script>");
                }
            //}
        }
    }
}