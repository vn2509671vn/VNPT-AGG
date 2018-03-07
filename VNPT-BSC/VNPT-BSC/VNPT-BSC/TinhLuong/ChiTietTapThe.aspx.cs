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
    public partial class ChiTietTapThe : System.Web.UI.Page
    {
        public string thang, nam;

        public static DataTable listNhom() {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select * from qlns_nhom_donvi order by id asc";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return dtResult;
        }

        public static DataTable chitiet(int nhom, int thang, int nam) {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string timekey = nam.ToString() + thang.ToString("00");
            string sql = "select nv.ma_nhanvien, chitiet.ten_nhanvien, chitiet.heso_lcb, isnull(chitiet.luong_coban,0) as luong_coban, isnull(chitiet.diem_p1,0) as diem_p1, isnull(chitiet.heso_bsc,0) as heso_bsc, isnull(chitiet.luong_3ps,0) as luong_3ps, isnull(chitiet.luong_pttb,0) as luong_pttb, isnull(chitiet.luong_congthem,0) as luong_congthem, (isnull(chitiet.luong_3ps,0) + isnull(chitiet.luong_pttb,0) + isnull(chitiet.luong_congthem,0)) as luongphanphoi ";
            sql += "from qlns_luong_tonghop_3ps chitiet, qlns_nhanvien nv ";
            sql += "where chitiet.timekey = '" + timekey + "' ";
            sql += "and chitiet.id_nhanvien = nv.id ";
            sql += "and chitiet.id_nhom = '" + nhom + "' ";
            sql += "order by nv.ma_nhanvien asc";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return dtResult;
        }

        [WebMethod]
        public static string loadLuong(int thang, int nam) {
            string outputHTML = "";
            DataTable dtNhom = new DataTable();
            DataTable dtChiTiet = new DataTable();
            dtNhom = listNhom();
            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-nv' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='no-sort text-center'>STT</th>";
            outputHTML += "<th class='no-sort text-center'>MNV</th>";
            outputHTML += "<th class='no-sort text-center'>Họ và Tên</th>";
            outputHTML += "<th class='no-sort text-center'>Hệ số</th>";
            outputHTML += "<th class='no-sort text-center'>Tiền lương</th>";
            outputHTML += "<th class='no-sort text-center'>Điểm P1</th>";
            outputHTML += "<th class='no-sort text-center'>Hệ số BSC</th>";
            outputHTML += "<th class='no-sort text-center'>Lương P3</th>";
            outputHTML += "<th class='no-sort text-center'>Lương PTTB/Lương Thu cước</th>";
            outputHTML += "<th class='no-sort text-center'>Lương cộng thêm</th>";
            outputHTML += "<th class='no-sort text-center'>Tổng lương phân phối</th>";
            outputHTML += "<th class='no-sort text-center'>BHTN</th>";
            outputHTML += "<th class='no-sort text-center'>BHXH</th>";
            outputHTML += "<th class='no-sort text-center'>BHYT</th>";
            outputHTML += "<th class='no-sort text-center'>Lương đã trừ BH</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            for (int nNhom = 0; nNhom < dtNhom.Rows.Count; nNhom++) {
                int id_nhom = Convert.ToInt32(dtNhom.Rows[nNhom]["id"].ToString());
                string ten_nhom = dtNhom.Rows[nNhom]["ten_nhom"].ToString();
                dtChiTiet = chitiet(id_nhom, thang, nam);
                outputHTML += "<tr style = 'background-color: burlywood;'>";
                outputHTML += "<td style='text-align: center;'><strong>" + (nNhom + 1) + "</strong></td>";
                outputHTML += "<td colspan='3'><strong>" + ten_nhom + "</strong></td>";
                outputHTML += "<td style='display:none;'></td>";
                outputHTML += "<td style='display:none;'></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "</tr>";

                double tongluongp3 = 0;
                double tongluongpttb = 0;
                double tongluongcongthem = 0;
                double tongluongphanphoi = 0;
                double tongluongdatrubh = 0;
                for (int nChiTiet = 0; nChiTiet < dtChiTiet.Rows.Count; nChiTiet++) {
                    tongluongp3 += Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_3ps"].ToString());
                    tongluongpttb += Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_pttb"].ToString());
                    tongluongcongthem += Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_congthem"].ToString());
                    tongluongphanphoi = tongluongp3 + tongluongpttb + tongluongcongthem;
                    double bhtn = Math.Round((Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_coban"].ToString()) * 0.01), 0);
                    double bhxh = Math.Round((Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_coban"].ToString()) * 0.08), 0);
                    double bhyt = Math.Round((Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_coban"].ToString()) * 0.015), 0);
                    double luongdatru = Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luongphanphoi"].ToString()) - bhtn - bhxh - bhyt;
                    tongluongdatrubh += luongdatru;

                    outputHTML += "<tr>";
                    outputHTML += "<td style='text-align: center;'><strong>" + (nNhom + 1) + "." + (nChiTiet + 1) + "</strong></td>";
                    outputHTML += "<td style='text-align: center;'><strong>" + dtChiTiet.Rows[nChiTiet]["ma_nhanvien"].ToString() + "</strong></td>";
                    outputHTML += "<td class='min-width-130'><strong>" + dtChiTiet.Rows[nChiTiet]["ten_nhanvien"].ToString() + "</strong></td>";
                    outputHTML += "<td style='text-align: center;'>" + dtChiTiet.Rows[nChiTiet]["heso_lcb"].ToString() + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_coban"].ToString())) + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + dtChiTiet.Rows[nChiTiet]["diem_p1"].ToString() + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + dtChiTiet.Rows[nChiTiet]["heso_bsc"].ToString() + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_3ps"].ToString())) + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_pttb"].ToString())) + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luong_congthem"].ToString())) + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", Convert.ToDouble(dtChiTiet.Rows[nChiTiet]["luongphanphoi"].ToString())) + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", bhtn) + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", bhxh) + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", bhyt) + "</td>";
                    outputHTML += "<td style='text-align: center;'>" + String.Format("{0:0,0}", luongdatru) + "</td>";
                    outputHTML += "</tr>";
                }
                outputHTML += "<tr>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td class='min-width-130' style='color: red;'><strong>" + "Cộng: " + dtChiTiet.Rows.Count + " nhân sự" + "</strong></td>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0,0}", tongluongp3) + "</strong></td>";
                outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0,0}", tongluongpttb) + "</strong></td>";
                outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0,0}", tongluongcongthem) + "</strong></td>";
                outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0,0}", tongluongphanphoi) + "</strong></td>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td style='text-align: center;'></td>";
                outputHTML += "<td style='text-align: center;'><strong>" + String.Format("{0:0,0}", tongluongdatrubh) + "</strong></td>";
                outputHTML += "</tr>";
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "</div>";
            
            return outputHTML;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Chi tiết lương tổng hợp (3PS) của nhân viên";
            //if (!IsPostBack)
            //{
                try
                {
                    thang = DateTime.Now.Month.ToString();
                    nam = DateTime.Now.ToString("yyyy");
                    if (Request.QueryString["thang"] != null && Request.QueryString["nam"] != null)
                    {
                        thang = Request.QueryString["thang"];
                        nam = Request.QueryString["nam"];
                    }
                }
                catch
                {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            //}
        }
    }
}