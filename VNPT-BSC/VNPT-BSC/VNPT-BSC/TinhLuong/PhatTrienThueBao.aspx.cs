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

namespace VNPT_BSC.TinhLuong
{
    public partial class PhatTrienThueBao : System.Web.UI.Page
    {
        [WebMethod]
        public static string loadTB(int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable gridData = new DataTable();
            string timekey = nam.ToString() + thang.ToString("00");
            string sqlBSC = "select nv.ma_nhanvien , pttb.* ";
            sqlBSC += "from tmp_nhanvien_pttb pttb, qlns_nhanvien nv ";
            sqlBSC += "where pttb.nhanvien_id = nv.id_pttb ";
            sqlBSC += "and pttb.timekey = '" + timekey + "' ";

            try
            {
                gridData = cn.XemDL(sqlBSC);
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
            arrOutput += "<th class='text-center'>Mã NV</th>";
            arrOutput += "<th class='text-center'>Nhân viên</th>";
            arrOutput += "<th class='text-center'>Fiber (< 200k)</th>";
            arrOutput += "<th class='text-center'>Fiber (>= 200k)</th>";
            arrOutput += "<th class='text-center'>Di động</th>";
            arrOutput += "<th class='text-center'>MyTV</th>";
            arrOutput += "<th class='text-center'>Ezcom</th>";
            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                arrOutput += "<tr><td colspan='7' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    arrOutput += "<tr>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["ma_nhanvien"].ToString() + "</strong></td>";
                    arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[nKPI]["ten_nv"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["fiber_lt_220"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["fiber_gt_220"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["didong"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["mytv"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["ezcom"].ToString() + "</strong></td>";
                    arrOutput += "</tr>";
                }
            }
            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Số lượng phát triển thuê bao";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                //nhanvien = Session.GetCurrentUser();
                nhanvien = (Nhanvien)Session["nhanvien"];

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