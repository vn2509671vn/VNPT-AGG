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
    public partial class KiemDinhBSCDonVi : System.Web.UI.Page
    {
        public static string donvikiemdinh;

        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam, int donvikiemdinh)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";

            string sqlBSC = "select dvgiao.donvi_id as donvigiao, dvgiao.donvi_ten as tendvg, dvnhan.donvi_id as donvinhan, dvnhan.donvi_ten as tendvn, giaobsc.nam, giaobsc.thang, giaobsc.trangthaicham, giaobsc.trangthaidongy_kqtd, giaobsc.trangthaiketthuc,  ";
            sqlBSC += "(select COUNT(*) from bsc_donvi where bsc_donvi.donvithamdinh = '" + donvikiemdinh + "' and bsc_donvi.trangthaithamdinh = 0 and bsc_donvi.nam = '" + nam + "' and bsc_donvi.thang = '" + thang + "' and bsc_donvi.donvinhan = dvnhan.donvi_id) as sl_chuatd ";
            sqlBSC += "from giaobscdonvi giaobsc, bsc_donvi giaobsc_dv, donvi dvgiao, donvi dvnhan ";
            sqlBSC += "where giaobsc.thang = giaobsc_dv.thang ";
            sqlBSC += "and giaobsc.nam = giaobsc_dv.nam ";
            sqlBSC += "and giaobsc.donvigiao = giaobsc_dv.donvigiao ";
            sqlBSC += "and giaobsc.donvinhan = giaobsc_dv.donvinhan ";
            sqlBSC += "and giaobsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and giaobsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and giaobsc.nam = '" + nam + "' ";
            sqlBSC += "and giaobsc.thang = '" + thang + "' ";
            sqlBSC += "and giaobsc_dv.donvithamdinh = '" + donvikiemdinh + "' ";
            sqlBSC += "group by dvgiao.donvi_id, dvgiao.donvi_ten, dvnhan.donvi_id, dvnhan.donvi_ten, giaobsc.nam, giaobsc.thang, giaobsc.trangthaicham, giaobsc.trangthaidongy_kqtd, giaobsc.trangthaiketthuc";

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
            outputHTML += "<th class='text-center'>Đơn vị giao</th>";
            outputHTML += "<th class='text-center'>Đơn vị nhận</th>";
            outputHTML += "<th class='text-center'>Ngày áp dụng</th>";
            outputHTML += "<th class='text-center'>Trạng thái nộp</th>";
            outputHTML += "<th class='text-center'>Trạng thái thẩm định</th>";
            outputHTML += "<th class='text-center'>Trạng thái kết thúc</th>";
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
                    string szDonvithamdinh = donvikiemdinh.ToString();
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
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tendvg"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tendvn"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'><strong>" + nThang + "/" + nNam + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiCham + "'>" + txtTrangThaiCham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + txtTrangThaiThamDinh + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiKetThuc + "'>" + txtTrangThaiKetThuc + "</span></td>";
                    outputHTML += "<td class='text-center'><a class='" + "btn btn-primary detail btn-xs" + "' onclick='xemChiTiet(" + nThang + ", " + nam + ", " + donvigiao + ", " + szDonvinhan + ", " + szDonvithamdinh + ")'>Chi tiết</a></td>";
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
            this.Title = "Kiểm định bsc";
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
                    donvikiemdinh = nhanvien.nhanvien_donvi_id.ToString();
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}