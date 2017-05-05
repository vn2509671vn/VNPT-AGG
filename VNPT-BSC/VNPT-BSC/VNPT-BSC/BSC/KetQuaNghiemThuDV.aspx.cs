using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Globalization;

namespace VNPT_BSC.BSC
{
    public partial class KetQuanNghiemThuDV : System.Web.UI.Page
    {
        public static string donvinhan, ten_dvn;

        [WebMethod]
        public static string loadBSCByYear(int donvinhan, int thang, int nam)
        {
            Connection cnBSC = new Connection();
            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, dvt.dvt_ten as donvitinh, bsc.trongso, bsc.kehoach, bsc.thuchien, bsc.thamdinh, bsc.kq_thuchien, bsc.diem_kpi, danhsachbsc.stt ";
            sqlBSC += "from bsc_donvi bsc, kpi, kpo, donvi dvgiao, donvi dvnhan, donvitinh dvt, danhsachbsc ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.donvitinh = dvt.dvt_id ";
            sqlBSC += "and bsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and bsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and bsc.donvinhan = '" + donvinhan + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' ";
            sqlBSC += "and bsc.thang = danhsachbsc.thang ";
            sqlBSC += "and bsc.nam = danhsachbsc.nam ";
            sqlBSC += "and bsc.loaimau = danhsachbsc.maubsc ";
            sqlBSC += "and bsc.kpi = danhsachbsc.kpi_id  ";
            sqlBSC += "and danhsachbsc.bscduocgiao = '' ORDER BY danhsachbsc.stt ASC ";

            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            //outputHTML += "<caption>"+ten_dvn+"</caption>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='no-sort'>STT</th>";
            outputHTML += "<th class='no-sort'>Chỉ tiêu</th>";
            outputHTML += "<th class='no-sort'>Tỷ trọng (%)</th>";
            outputHTML += "<th class='no-sort'>ĐVT</th>";
            outputHTML += "<th class='no-sort'>Chỉ tiêu</th>";
            outputHTML += "<th class='no-sort'>Thực hiện</th>";
            outputHTML += "<th class='no-sort'>Thẩm định</th>";
            outputHTML += "<th class='no-sort'>Tỷ lệ thực hiện (%)</th>";
            outputHTML += "<th class='no-sort'>Mức độ hoàn thành</th>";
            outputHTML += "<th class='no-sort'>Điểm</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='10' class='text-center'>No item</td></tr>";
            }
            else
            {
                int nTongTrongSo = 0;
                double nTongSoDiem = 0;
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    double tylethuchien = 0;
                    double diem = 0;
                    int trongso = Convert.ToInt32(gridData.Rows[nKPI]["trongso"].ToString());
                    double kehoach = Convert.ToDouble(gridData.Rows[nKPI]["kehoach"].ToString());
                    double thamdinh = Convert.ToDouble(gridData.Rows[nKPI]["thamdinh"].ToString());
                    double mucdohoanthanh = Convert.ToDouble(gridData.Rows[nKPI]["kq_thuchien"].ToString());
                    if (gridData.Rows[nKPI]["diem_kpi"].ToString() != "")
                    {
                        diem = Convert.ToDouble(gridData.Rows[nKPI]["diem_kpi"].ToString());
                    }

                    tylethuchien = (thamdinh / kehoach) * 100;
                    nTongTrongSo += trongso;
                    nTongSoDiem += (mucdohoanthanh * trongso) / 100;

                    outputHTML += "<tr data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<td style='text-align: center'>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + "</strong></td>";
                    outputHTML += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["trongso"].ToString() + "</strong></td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nKPI]["donvitinh"].ToString() + "</strong></td>";
                    outputHTML += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["kehoach"].ToString() + "</strong></td>";
                    outputHTML += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["thuchien"].ToString() + "</strong></td>";
                    outputHTML += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["thamdinh"].ToString() + "</strong></td>";
                    outputHTML += "<td style='text-align: center'><strong>" + String.Format("{0:0.####}", tylethuchien) + "</strong></td>";
                    outputHTML += "<td style='text-align: center'><strong>" + String.Format("{0:0.####}", mucdohoanthanh) + "</strong></td>";
                    outputHTML += "<td style='text-align: center'><strong>" + String.Format("{0:0.####}", diem) + "</strong></td>";
                    outputHTML += "</tr>";
                }

                outputHTML += "<tr>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center'><strong>Tỷ lệ thực hiện</strong></td>";
                outputHTML += "<td style='text-align: center'><strong>" + nTongTrongSo + "%" + "</strong></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center'><strong>" + String.Format("{0:0.00}", (nTongSoDiem * 100)) + "%" + "</strong></td>";
                outputHTML += "</tr>";
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";

            return outputHTML;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Kết quả nghiệm thu BSC";
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

                    if (nhanvien == null)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    donvinhan = nhanvien.nhanvien_donvi_id.ToString();
                    ten_dvn = nhanvien.nhanvien_donvi.ToString();
                }
                catch
                {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}