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
    public partial class CTV_PTTB : System.Web.UI.Page
    {
        public DataTable dtDonVi = new DataTable();
        public string ngaycapnhat = "";

        private DataTable getDonVi()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "SELECT * FROM [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi]";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmp;
        }

        private string maxNgayCapNhat() {
            string szResult = "";
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select format(cast(max(a.ngaycapnhat) as date), 'dd/MM/yyyy') as thoigian from tmp_ctv_pttb a";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                szResult = tmp.Rows[0][0].ToString();
            }
            return szResult;
        }

        [WebMethod]
        public static string loadTB(int donvi, int thang, int nam, int kieuhienthi, int nhomctv)
        {
            Connection cn = new Connection();
            DataTable gridData = new DataTable();
            string timekey = nam.ToString() + thang.ToString("00");
            string sql = "";
            if (kieuhienthi == 1)
            {
                // Hiển thị chi tiết
                sql += "select * ";
                sql += "from tmp_ctv_pttb ";
                sql += "where timekey = '" + timekey + "' ";
                if (nhomctv == 0)
                {
                    sql += "and nhom_id in (2,3,7,11,12,13) ";
                }
                else {
                    sql += "and nhom_id in (" + nhomctv + ") ";
                }

                if (donvi != 0) {
                    sql += "and donvi_id = '" + donvi + "' ";
                }

                sql += "order by tendonvi,TenNhom asc";
            }
            else
            {
                // Hiển thị gon gọn
                //sql += "select TenNV,TenNhom, TenDonVi, COUNT(So_DT) as sl ";
                sql += "select TenNV,TenNhom, TenDonVi, ";
                sql += "(select COUNT(So_DT) from tmp_ctv_pttb where TimeKey = '" + timekey + "' and loaisim = N'Sim Thường' and TenNV = a.TenNV and TenDonVi = a.TenDonVi) as N'simthuong',";
                sql += "(select COUNT(So_DT) from tmp_ctv_pttb where TimeKey = '" + timekey + "' and loaisim = N'Sim 12T' and TenNV = a.TenNV and TenDonVi = a.TenDonVi) as N'sim12t' ";

                sql += "from tmp_ctv_pttb a ";
                sql += "where TimeKey = '" + timekey + "' ";
                if (nhomctv == 0)
                {
                    sql += "and Nhom_ID in (2,3,7,11,12,13) ";
                }
                else
                {
                    sql += "and Nhom_ID in (" + nhomctv + ") ";
                }

                if (donvi != 0)
                {
                    sql += "and donvi_id = '" + donvi + "' ";
                }

                sql += "group by TenNV,TenNhom, TenDonVi ";
                sql += "order by TenDonVi,TenNhom asc";
            }

            try
            {
                gridData = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string arrOutput = "";
            int count = 0;
            int count_12t = 0;

            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            arrOutput += "<thead>";
            arrOutput += "<tr>";
            if (kieuhienthi == 1)
            {
                arrOutput += "<th class='text-center'>STT</th>";
                arrOutput += "<th class='text-center'>Đơn vị</th>";
                arrOutput += "<th class='text-center'>Nhóm CTV</th>";
                arrOutput += "<th class='text-center'>Số ĐT</th>";
                arrOutput += "<th class='text-center'>Tên KH</th>";
                arrOutput += "<th class='text-center'>Ngày đăng ký</th>";
                arrOutput += "<th class='text-center'>CMND</th>";
                arrOutput += "<th class='text-center'>CTV đăng ký</th>";
                arrOutput += "<th class='text-center'>Loại sim</th>";
            }
            else {
                arrOutput += "<th style='display:none;border: none;'></th>";
                arrOutput += "<th class='text-center' style='border:1px solid !important'>STT</th>";
                arrOutput += "<th class='text-center' style='border:1px solid !important'>Đơn vị</th>";
                arrOutput += "<th class='text-center' style='border:1px solid !important'>Nhóm CTV</th>";
                arrOutput += "<th class='text-center' style='border:1px solid !important'>CTV đăng ký</th>";
                //arrOutput += "<th class='text-center' style='border:1px solid !important'>Số lượng thuê bao ĐK</th>";
                arrOutput += "<th class='text-center' style='border:1px solid !important'>Số lượng sim thường</th>";
                arrOutput += "<th class='text-center' style='border:1px solid !important'>Số lượng sim 12T</th>";
            }

            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                //
            }
            else
            {
                if (kieuhienthi == 1)
                {
                    for (int i = 0; i < gridData.Rows.Count; i++)
                    {
                        count++;
                        arrOutput += "<tr>";
                        arrOutput += "<td style='text-align: center'><strong>" + (i + 1) + "</strong></td>";
                        arrOutput += "<td><strong>" + gridData.Rows[i]["TenDonVi"].ToString() + "</strong></td>";
                        arrOutput += "<td><strong>" + gridData.Rows[i]["TenNhom"].ToString() + "</strong></td>";
                        arrOutput += "<td><strong>" + gridData.Rows[i]["So_DT"].ToString() + "</strong></td>";
                        arrOutput += "<td><strong>" + gridData.Rows[i]["Ten_KH"].ToString() + "</strong></td>";
                        arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[i]["Ngay_DK"].ToString() + "</strong></td>";
                        arrOutput += "<td style='text-align: center;'><strong>" + gridData.Rows[i]["cmnd"].ToString() + "</strong></td>";
                        arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[i]["TenNV"].ToString() + "</strong></td>";
                        arrOutput += "<td><strong>" + gridData.Rows[i]["loaisim"].ToString() + "</strong></td>";
                        arrOutput += "</tr>";
                    }
                }
                else {
                    for (int i = 0; i < gridData.Rows.Count; i++)
                    {
                        count += Convert.ToInt32(gridData.Rows[i]["simthuong"].ToString());
                        count_12t += Convert.ToInt32(gridData.Rows[i]["sim12t"].ToString());
                        arrOutput += "<tr>";
                        arrOutput += "<td style='display:none;border: none;'></td>";
                        arrOutput += "<td style='text-align: center; border:1px solid !important;'><strong>" + (i + 1) + "</strong></td>";
                        arrOutput += "<td style='border:1px solid !important;'><strong>" + gridData.Rows[i]["TenDonVi"].ToString() + "</strong></td>";
                        arrOutput += "<td style='border:1px solid !important;'><strong>" + gridData.Rows[i]["TenNhom"].ToString() + "</strong></td>";
                        arrOutput += "<td style='border:1px solid !important;' class='min-width-130'><strong>" + gridData.Rows[i]["TenNV"].ToString() + "</strong></td>";
                        arrOutput += "<td style='text-align: center;border:1px solid !important;'><strong>" + gridData.Rows[i]["simthuong"].ToString() + "</strong></td>";
                        arrOutput += "<td style='text-align: center;border:1px solid !important;'><strong>" + gridData.Rows[i]["sim12t"].ToString() + "</strong></td>";
                        arrOutput += "</tr>";
                    }
                }
            }

            arrOutput += "<tfoot>";
            arrOutput += "<tr style='background:orange;'>";
            if (kieuhienthi == 1)
            {
                arrOutput += "<td></td>";
                arrOutput += "<td></td>";
                arrOutput += "<td></td>";
                arrOutput += "<td></td>";
                arrOutput += "<td></td>";
                arrOutput += "<td></td>";
                arrOutput += "<td></td>";
                arrOutput += "<td></td>";
                arrOutput += "<td style='text-align: center'><strong>Tổng : " + count + "</strong></td>";
            }
            else
            {
                arrOutput += "<td style='display:none;border: none;background: white !important;'></td>";
                arrOutput += "<td style='border: 1px solid !important'></td>";
                arrOutput += "<td style='border: 1px solid !important'></td>";
                arrOutput += "<td style='border: 1px solid !important'></td>";
                arrOutput += "<td style='border: 1px solid !important'>Tổng : </td>";
                arrOutput += "<td style='text-align: center;border: 1px solid !important;'><strong>" + count + "</strong></td>";
                arrOutput += "<td style='text-align: center;border: 1px solid !important;'><strong>" + count_12t + "</strong></td>";
            }
            arrOutput += "</tr>";
            arrOutput += "</tfoot>";

            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Cộng tác viên phát triển thuê bao";
            dtDonVi = getDonVi();
            ngaycapnhat = maxNgayCapNhat();
        }
    }
}