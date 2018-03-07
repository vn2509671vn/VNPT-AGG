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
    public partial class TyLeThuCuoc : System.Web.UI.Page
    {
        public DataTable dtDonVi = new DataTable();

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

        private static string getHeSoThuCuoc(int donvi, string timekey, decimal tyle)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select heso from dongia_tyle_thucuoc ";
            sql += "where tyle_batdau <= '" + tyle + "' ";
            sql += "and tyle_ketthuc > '" + tyle + "' ";
            sql += "and CHARINDEX('" + donvi + "',donvi_apdung) > 0 ";
            sql += "and timekey =  (select top 1 timekey from dongia_tyle_thucuoc where timekey <= '" + timekey + "' order by timekey desc) ";

            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmp.Rows[0][0].ToString();
        }

        [WebMethod]
        public static string loadData(int donvi, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable gridData = new DataTable();
            string timekey = nam.ToString() + thang.ToString("00") + "01";
            string timekey_tyle = nam.ToString() + thang.ToString("00");
            string sql = "";
            sql += "select d.donvi_id, nv.tendonvi, a.nhanvien_id, d.ten_nv, nv.nvql, count(a.ma_tb) as Tong_TB, ";
            sql += "(select count(b.ma_tb) from [CCBS]..[HIENHM_AGG].[TYLE_THUCUOC_" + timekey + "] b where b.nhanvien_id = a.nhanvien_id and b.thucuoc = 1) as SL_Thu, ";
            sql += "(select count(c.ma_tb) from [CCBS]..[HIENHM_AGG].[TYLE_THUCUOC_" + timekey + "] c where c.nhanvien_id = a.nhanvien_id and c.thucuoc = 0) as SL_ChuaThu ";
            sql += "from [CCBS]..[HIENHM_AGG].[TYLE_THUCUOC_" + timekey + "] a, ";
            sql += "[CCBS]..[ADMIN_AGG].[NHANVIEN] d, ";
            sql += "[CCBS]..[ADMIN_AGG].[DONVI] e, ";
            sql += "[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] nv ";
            sql += "where a.nhanvien_id = d.nhanvien_id ";
            sql += "and e.donvi_id = d.donvi_id ";
            sql += "and a.nhanvien_id = nv.ma_nv ";
            if (donvi != 0)
            {
                sql += "and e.donvi_id = '" + donvi + "' ";
            }
            sql += "group by d.donvi_id, nv.tendonvi, a.nhanvien_id, d.ten_nv, nv.nvql ";
            sql += "order by nv.tendonvi asc";

            try
            {
                gridData = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            string arrOutput = "";

            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            arrOutput += "<thead>";
            arrOutput += "<tr>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>STT</th>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>Đơn vị</th>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>Mã NV</th>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>Tên NV</th>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>Tổng TB</th>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>Đã Thu</th>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>Chưa Thu</th>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>Tỷ lệ thu</th>";
            arrOutput += "<th class='text-center' style='border:1px solid !important'>Hệ số</th>";
            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                //
            }
            else
            {
                for (int i = 0; i < gridData.Rows.Count; i++)
                {
                    decimal tyle = 0;
                    string heso = "";
                    decimal tong_tb = Convert.ToDecimal(gridData.Rows[i]["Tong_TB"].ToString());
                    decimal sl_dathu = Convert.ToDecimal(gridData.Rows[i]["SL_Thu"].ToString());
                    tyle = Math.Round(((sl_dathu / tong_tb) * 100),4);
                    
                    heso = getHeSoThuCuoc(donvi, timekey_tyle, tyle);

                    arrOutput += "<tr>";
                    //arrOutput += "<td style='display:none;border: none;'></td>";
                    arrOutput += "<td style='text-align: center; border:1px solid !important;'><strong>" + (i + 1) + "</strong></td>";
                    arrOutput += "<td style='border:1px solid !important;'><strong>" + gridData.Rows[i]["TenDonVi"].ToString() + "</strong></td>";
                    arrOutput += "<td style='border:1px solid !important;'><strong>" + gridData.Rows[i]["nvql"].ToString() + "</strong></td>";
                    arrOutput += "<td style='border:1px solid !important;'><strong>" + gridData.Rows[i]["ten_nv"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center; border:1px solid !important;'><strong>" + gridData.Rows[i]["Tong_TB"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center; border:1px solid !important;'><strong>" + gridData.Rows[i]["SL_Thu"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center; border:1px solid !important;'><strong>" + gridData.Rows[i]["SL_ChuaThu"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center; border:1px solid !important;'><strong>" + tyle + "</strong></td>";
                    arrOutput += "<td style='text-align: center; border:1px solid !important;'><strong>" + heso + "</strong></td>";
                    arrOutput += "</tr>";
                }
            }

            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Tỷ lệ thu cước của NVKD/CTV";
            dtDonVi = getDonVi();
        }
    }
}