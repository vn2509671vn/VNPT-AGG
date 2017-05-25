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
    public partial class ThongKeTrungBinhDiemBSCNV : System.Web.UI.Page
    {
        public static DataTable dtDonVi = new DataTable();

        public static DataTable dsDonVi() {
            DataTable tmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select * from donvi where donvi_id not in (1,2)";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            return tmp;
        }

        [WebMethod]
        public static string loadBSC(int donvi, int nam, int quy, int thang, int loainv)
        {
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            DataTable gridDataTTKD = new DataTable();
            string szThang = "";
            int tongsothang = 1;
            if (thang == 0)
            {
                if (quy == 0)
                {
                    szThang = "1,2,3,4,5,6,7,8,9,10,11,12";
                    tongsothang = 12;
                }
                else if (quy == 1)
                {
                    szThang = "1,2,3";
                    tongsothang = 3;
                }
                else if (quy == 2)
                {
                    szThang = "4,5,6";
                    tongsothang = 3;
                }
                else if (quy == 3)
                {
                    szThang = "7,8,9";
                    tongsothang = 3;
                }
                else if (quy == 4)
                {
                    szThang = "10,11,12";
                    tongsothang = 3;
                }
            }
            else {
                szThang = thang.ToString();
            }

            string sqlBSC = "select nv.nhanvien_manv, nv.nhanvien_hoten, dv.donvi_ten, (bsc.tongdiem_bsc/" + tongsothang + ") as diem ";
            //sqlBSC += "from bsc_nhanvien bsc, nhanvien nv, donvi dv ";
            sqlBSC += "from tmp_tongbsc_nhanvien bsc, nhanvien nv, donvi dv ";
            sqlBSC += "where bsc.id_nhanvien = nv.nhanvien_id ";
            sqlBSC += "and bsc.thang in (" + szThang + ") ";
            sqlBSC += "and bsc.nam = '" + nam + "' ";
            sqlBSC += "and nv.nhanvien_donvi = dv.donvi_id ";
            if (donvi != 0) {
                sqlBSC += "and dv.donvi_id = '" + donvi + "' ";
            }

            if (loainv == 1) {
                sqlBSC += "and nv.nhanvien_manv is not null ";
            }
            else if (loainv == 2) {
                sqlBSC += "and nv.nhanvien_manv is null ";
            }

            //sqlBSC += "group by nv.nhanvien_manv, nv.nhanvien_hoten, dv.donvi_ten ";
            sqlBSC += "order by diem DESC";

            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
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
            arrOutput += "<th class='text-center'>STT</th>";
            arrOutput += "<th class='text-center'>Mã NV</th>";
            arrOutput += "<th class='text-center'>Nhân viên</th>";
            arrOutput += "<th class='text-center'>Đơn vị</th>";
            arrOutput += "<th class='text-center'>Điểm TB</th>";
            arrOutput += "<th class='hide'>Chú thích</th>";
            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                arrOutput += "<tr><td colspan='5' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    decimal diem = 0;
                    if (gridData.Rows[nKPI]["diem"].ToString() != "")
                    {
                        diem = Convert.ToDecimal(gridData.Rows[nKPI]["diem"].ToString());
                    }
                    arrOutput += "<tr>";
                    arrOutput += "<td style='text-align: center'>" + (nKPI + 1) + "</td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["nhanvien_manv"].ToString() + "</strong></td>";
                    arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[nKPI]["nhanvien_hoten"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[nKPI]["donvi_ten"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0.0000}", diem) + "</strong></td>";
                    arrOutput += "<td class='hide'></td>";
                    arrOutput += "</tr>";
                }
            }
            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Trung bình điểm BSC Nhân Viên";
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

                dtDonVi = dsDonVi();
                string sql = "";
                sql.ToString();
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}