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

namespace VNPT_BSC.Admin
{
    public partial class QLGiaoBSCDonVi : System.Web.UI.Page
    {
        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam)
        {
            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select giaobsc.*, dvnhan.donvi_ten as tendvn, dvgiao.donvi_ten as tendvg ";
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
            outputHTML += "<th>STT</th>";
            outputHTML += "<th>Đơn vị giao</th>";
            outputHTML += "<th>Đơn vị nhận</th>";
            outputHTML += "<th>Ngày áp dụng</th>";
            outputHTML += "<th></th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";

            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='5' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nIndex = 0; nIndex < gridData.Rows.Count; nIndex++)
                {
                    string szDonvigiao = gridData.Rows[nIndex]["donvigiao"].ToString();
                    string szDonvinhan = gridData.Rows[nIndex]["donvinhan"].ToString();
                    string szThang = gridData.Rows[nIndex]["thang"].ToString();
                    string szNam = gridData.Rows[nIndex]["nam"].ToString();

                    outputHTML += "<tr>";
                    outputHTML += "<td class='text-center'>" + (nIndex + 1) + "</td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nIndex]["tendvg"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nIndex]["tendvn"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + szThang + "/" + szNam + "</strong></td>";
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
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();

                // Khai báo các biến cho việc kiểm tra quyền
                int[] quyenHeThong = { };
                int nFindResult = -1;
                quyenHeThong = Session.GetRole();

                /*Kiểm tra nếu không có quyền admin (id của quyền là 1) thì đẩy ra trang đăng nhập*/
                nFindResult = Array.IndexOf(quyenHeThong, 1);

                if (nhanvien == null || nFindResult == -1)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
            catch
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}