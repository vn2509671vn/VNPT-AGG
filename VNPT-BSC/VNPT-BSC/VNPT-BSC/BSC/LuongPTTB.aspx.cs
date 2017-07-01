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
    public partial class LuongPTTB_Tang : System.Web.UI.Page
    {
        public static DataTable dtDonVi = new DataTable();

        private DataTable getDonVi() {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "SELECT * FROM [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi]";
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
        public static string loadTB(int donvi, int thang, int nam, int loai, int nhom_nv)
        {
            Connection cn = new Connection();
            DataTable gridData = new DataTable();
            DataTable gridDataLanhDao = new DataTable();
            string timekey = nam.ToString() + thang.ToString("00");
            string sql = "";
            string sqlLanhDao = "";
            if (loai == 1)
            {
                // Tăng tiền
                sql += "select pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv, sum(pttb.tienluong) as tongtien ";
                sql += "from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] pttb, ";
                sql += "[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] nvkd, ";
                sql += "[OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                sql += "where pttb.donvi_id = dv.donvi_id ";
                if (donvi != 0) {
                    sql += "and dv.donvi_id = '" + donvi + "' ";
                }
                sql += "and pttb.ma_nv = nvkd.ma_nv ";
                sql += "and LEFT(pttb.timekey,6) = '" + timekey + "'";
                sql += " group by pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv ";
                sql += "order by dv.ten_donvi asc";

                sqlLanhDao += "select nv.ma_nhanvien, nv.ten_nhanvien, pttb.*, dv.ten_donvi, ";
                sqlLanhDao += "((( ";
                sqlLanhDao += "select sum(a.tienluong) ";
                sqlLanhDao += "from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";
                sqlLanhDao += "where a.donvi_id = b.donvi_id ";
                sqlLanhDao += "and LEFT(a.timekey,6) = '" + timekey + "' ";
                sqlLanhDao += "and b.donvi_id = map.donvi_id_vmos ";
                sqlLanhDao += ") * pttb.dongia)/1000) as tongtien ";
                sqlLanhDao += "from qlns_dongia_pttbm_gdoc_pgdoc_pbh pttb, qlns_nhanvien nv, donvi_map map, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                sqlLanhDao += "where nv.chucdanh = pttb.chucdanh ";
                sqlLanhDao += "and nv.donvi = pttb.donvi ";
                sqlLanhDao += "and nv.donvi = map.donvi_id ";
                sqlLanhDao += "and map.donvi_id_vmos = dv.donvi_id ";
                if (donvi != 0)
                {
                    sqlLanhDao += "and dv.donvi_id = '" + donvi + "' ";
                }
                sqlLanhDao += "and pttb.timekey = (";
                sqlLanhDao += "select top 1 timekey from qlns_dongia_pttbm_gdoc_pgdoc_pbh where timekey <= '" + timekey + "' order by timekey desc";
                sqlLanhDao += ")";
            }
            else if(loai == 2) { 
                // Giảm tiền
                sql += "select pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv, sum(pttb.tienluong) as tongtien ";
                sql += "from [OLAP].[OLAP].[dbo].[Hien_Tru_Luong_PhatTrienTB] pttb, ";
                sql += "[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] nvkd, ";
                sql += "[OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                sql += "where pttb.donvi_id = dv.donvi_id ";
                if (donvi != 0)
                {
                    sql += "and dv.donvi_id = '" + donvi + "' ";
                }
                sql += "and pttb.ma_nv = nvkd.ma_nv ";
                sql += "and LEFT(pttb.timekey,6) = '" + timekey + "'";
                sql += " group by pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv ";
                sql += "order by dv.ten_donvi asc";

                sqlLanhDao += "select nv.ma_nhanvien, nv.ten_nhanvien, pttb.*, dv.ten_donvi, ";
                sqlLanhDao += "((( ";
                sqlLanhDao += "select ISNULL(sum(a.tienluong),0) ";
                sqlLanhDao += "from [OLAP].[OLAP].[dbo].[Hien_Tru_Luong_PhatTrienTB] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";
                sqlLanhDao += "where a.donvi_id = b.donvi_id ";
                sqlLanhDao += "and LEFT(a.timekey,6) = '" + timekey + "' ";
                sqlLanhDao += "and b.donvi_id = map.donvi_id_vmos ";
                sqlLanhDao += "and a.truluong_gd_id != 0 ";
                sqlLanhDao += ") * pttb.dongia)/1000) as tongtien ";
                sqlLanhDao += "from qlns_dongia_pttbm_gdoc_pgdoc_pbh pttb, qlns_nhanvien nv, donvi_map map, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                sqlLanhDao += "where nv.chucdanh = pttb.chucdanh ";
                sqlLanhDao += "and nv.donvi = pttb.donvi ";
                sqlLanhDao += "and nv.donvi = map.donvi_id ";
                sqlLanhDao += "and map.donvi_id_vmos = dv.donvi_id ";
                if (donvi != 0)
                {
                    sqlLanhDao += "and dv.donvi_id = '" + donvi + "' ";
                }
                sqlLanhDao += "and pttb.timekey = (";
                sqlLanhDao += "select top 1 timekey from qlns_dongia_pttbm_gdoc_pgdoc_pbh where timekey <= '" + timekey + "' order by timekey desc";
                sqlLanhDao += ")";
            }
            else if (loai == 3) {
                sql += "select tmp.donvi_id,tmp.ten_donvi,tmp.nvql,tmp.tennv,tmp.tangluong, ";
                sql += "(select isnull(sum(tienluong),0) ";
                sql += "from [OLAP].[OLAP].[dbo].[Hien_Tru_Luong_PhatTrienTB] a,[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] b ";
                sql += "where  LEFT(a.timekey,6) = '" + timekey + "' ";
                sql += "and a.ma_nv = b.Ma_NV ";
                sql += "and b.NVQL = tmp.NVQL ";
                sql += ") as truluong ";
                sql += "from ( ";
                sql += "select pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv, isnull(sum(pttb.tienluong),0) as tangluong ";
                sql += "from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] pttb, ";
                sql += "[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] nvkd, ";
                sql += "[OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                sql += "where pttb.donvi_id = dv.donvi_id ";
                if (donvi != 0)
                {
                    sql += "and dv.donvi_id = '" + donvi + "' ";
                }
                sql += "and pttb.ma_nv = nvkd.ma_nv ";
                sql += "and LEFT(pttb.timekey,6) = '" + timekey + "' ";
                sql += "group by pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv) tmp ";
                sql += "union all ";
                sql += "select b.Donvi_id,b.TenDonVi,b.NVQL,b.TenNV,0,isnull(sum(tienluong),0) truluong ";
                sql += "from [OLAP].[OLAP].[dbo].[Hien_Tru_Luong_PhatTrienTB] a,[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] b ";
                sql += "where  LEFT(a.timekey,6) = '" + timekey + "' ";
                sql += "and a.ma_nv = b.Ma_NV ";
                if (donvi != 0)
                {
                    sql += "and b.Donvi_id = '" + donvi + "' ";
                }
                sql += "and b.NVQL not in (select b.NVQL from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] a,[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] b ";
                sql += "where  LEFT(a.timekey,6) = '" + timekey + "' ";
                sql += "and a.ma_nv = b.Ma_NV) ";
                sql += "group by b.Donvi_id,b.TenDonVi,b.NVQL,b.TenNV ";
                sql += "order by tmp.ten_donvi ";

                sqlLanhDao += "select nv.ma_nhanvien, nv.ten_nhanvien, pttb.*, dv.ten_donvi, ";
                sqlLanhDao += "((( ";
                sqlLanhDao += "select ISNULL(sum(a.tienluong),0) ";
                sqlLanhDao += "from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";
                sqlLanhDao += "where a.donvi_id = b.donvi_id ";
                sqlLanhDao += "and LEFT(a.timekey,6) = '" + timekey + "' ";
                sqlLanhDao += "and b.donvi_id = map.donvi_id_vmos ";
                sqlLanhDao += ") * pttb.dongia)/1000) as tangluong, ";
                sqlLanhDao += "((( ";
                sqlLanhDao += "select ISNULL(sum(a.tienluong),0) ";
                sqlLanhDao += "from [OLAP].[OLAP].[dbo].[Hien_Tru_Luong_PhatTrienTB] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";
                sqlLanhDao += "where a.donvi_id = b.donvi_id ";
                sqlLanhDao += "and LEFT(a.timekey,6) = '" + timekey + "' ";
                sqlLanhDao += "and b.donvi_id = map.donvi_id_vmos ";
                sqlLanhDao += "and a.truluong_gd_id != 0 ";
                sqlLanhDao += ") * pttb.dongia)/1000) as truluong ";
                sqlLanhDao += "from qlns_dongia_pttbm_gdoc_pgdoc_pbh pttb, qlns_nhanvien nv, donvi_map map, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                sqlLanhDao += "where nv.chucdanh = pttb.chucdanh ";
                sqlLanhDao += "and nv.donvi = pttb.donvi ";
                sqlLanhDao += "and nv.donvi = map.donvi_id ";
                sqlLanhDao += "and map.donvi_id_vmos = dv.donvi_id ";
                if (donvi != 0)
                {
                    sqlLanhDao += "and dv.donvi_id = '" + donvi + "' ";
                }
                sqlLanhDao += "and pttb.timekey = (";
                sqlLanhDao += "select top 1 timekey from qlns_dongia_pttbm_gdoc_pgdoc_pbh where timekey <= '" + timekey + "' order by timekey desc";
                sqlLanhDao += ")";
            }

            try
            {
                gridData = cn.XemDL(sql);
                gridDataLanhDao = cn.XemDL(sqlLanhDao);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string arrOutput = "";
            double tong = 0;
            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            arrOutput += "<thead>";
            arrOutput += "<tr>";
            arrOutput += "<th class='text-center'>STT</th>";
            arrOutput += "<th class='text-center'>Đơn vị</th>";
            arrOutput += "<th class='text-center'>Mã NV</th>";
            arrOutput += "<th class='text-center'>Nhân viên</th>";
            if (loai == 3) {
                arrOutput += "<th class='text-center'>Tăng</th>";
                arrOutput += "<th class='text-center'>Giảm</th>";
            }
            arrOutput += "<th class='text-center'>Tổng tiền</th>";
            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                arrOutput += "<tr><td colspan='5' class='text-center'>No item</td></tr>";
            }
            else
            {
                int n = 0, m = 0;
                if (nhom_nv == 1)
                {
                    for (n = 0; n < gridData.Rows.Count; n++)
                    {
                        double dTongtien = 0;
                        if (loai == 3)
                        {
                            dTongtien = Convert.ToDouble(gridData.Rows[n]["tangluong"].ToString()) - Convert.ToDouble(gridData.Rows[n]["truluong"].ToString());
                        }
                        else {
                            dTongtien = Convert.ToDouble(gridData.Rows[n]["tongtien"].ToString());
                        }
                        

                        tong += dTongtien;
                        arrOutput += "<tr>";
                        arrOutput += "<td style='text-align: center'><strong>" + (n + 1) + "</strong></td>";
                        arrOutput += "<td><strong>" + gridData.Rows[n]["ten_donvi"].ToString() + "</strong></td>";
                        arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[n]["nvql"].ToString() + "</strong></td>";
                        arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[n]["tennv"].ToString() + "</strong></td>";
                        if (loai == 3) {
                            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", Convert.ToDouble(gridData.Rows[n]["tangluong"].ToString())) + "</strong></td>";
                            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", Convert.ToDouble(gridData.Rows[n]["truluong"].ToString())) + "</strong></td>";
                        }
                        arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", dTongtien) + "</strong></td>";
                        arrOutput += "</tr>";
                    }

                    for (m = 0; m < gridDataLanhDao.Rows.Count; m++)
                    {
                        double dTongtien = 0;
                        if (loai == 3)
                        {
                            dTongtien = Convert.ToDouble(gridDataLanhDao.Rows[m]["tangluong"].ToString()) - Convert.ToDouble(gridDataLanhDao.Rows[m]["truluong"].ToString());
                        }
                        else {
                            dTongtien = Convert.ToDouble(gridDataLanhDao.Rows[m]["tongtien"].ToString());
                        }
                        

                        tong += dTongtien;
                        arrOutput += "<tr style='background:darkgray;'>";
                        arrOutput += "<td style='text-align: center'><strong>" + (m + n + 1) + "</strong></td>";
                        arrOutput += "<td><strong>" + gridDataLanhDao.Rows[m]["ten_donvi"].ToString() + "</strong></td>";
                        arrOutput += "<td style='text-align: center'><strong>" + gridDataLanhDao.Rows[m]["ma_nhanvien"].ToString() + "</strong></td>";
                        arrOutput += "<td class='min-width-130'><strong>" + gridDataLanhDao.Rows[m]["ten_nhanvien"].ToString() + "</strong></td>";
                        if (loai == 3)
                        {
                            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDao.Rows[m]["tangluong"].ToString())) + "</strong></td>";
                            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDao.Rows[m]["truluong"].ToString())) + "</strong></td>";
                        }
                        arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", dTongtien) + "</strong></td>";
                        arrOutput += "</tr>";
                    }
                }
                else if (nhom_nv == 2) {
                    for (n = 0; n < gridData.Rows.Count; n++)
                    {
                        double dTongtien = 0;
                        if (loai == 3)
                        {
                            dTongtien = Convert.ToDouble(gridData.Rows[n]["tangluong"].ToString()) - Convert.ToDouble(gridData.Rows[n]["truluong"].ToString());
                        }
                        else {
                            dTongtien = Convert.ToDouble(gridData.Rows[n]["tongtien"].ToString());
                        }

                        tong += dTongtien;
                        arrOutput += "<tr>";
                        arrOutput += "<td style='text-align: center'><strong>" + (n + 1) + "</strong></td>";
                        arrOutput += "<td><strong>" + gridData.Rows[n]["ten_donvi"].ToString() + "</strong></td>";
                        arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[n]["nvql"].ToString() + "</strong></td>";
                        arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[n]["tennv"].ToString() + "</strong></td>";
                        if (loai == 3)
                        {
                            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", Convert.ToDouble(gridData.Rows[n]["tangluong"].ToString())) + "</strong></td>";
                            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", Convert.ToDouble(gridData.Rows[n]["truluong"].ToString())) + "</strong></td>";
                        }
                        arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", dTongtien) + "</strong></td>";
                        arrOutput += "</tr>";
                    }
                }
                else if (nhom_nv == 3) {
                    for (m = 0; m < gridDataLanhDao.Rows.Count; m++)
                    {
                        double dTongtien = 0;
                        if (loai == 3)
                        {
                            dTongtien = Convert.ToDouble(gridDataLanhDao.Rows[m]["tangluong"].ToString()) - Convert.ToDouble(gridDataLanhDao.Rows[m]["truluong"].ToString());
                        }
                        else
                        {
                            dTongtien = Convert.ToDouble(gridDataLanhDao.Rows[m]["tongtien"].ToString());
                        }

                        tong += dTongtien;
                        arrOutput += "<tr style='background:darkgray;'>";
                        arrOutput += "<td style='text-align: center'><strong>" + (m + n + 1) + "</strong></td>";
                        arrOutput += "<td><strong>" + gridDataLanhDao.Rows[m]["ten_donvi"].ToString() + "</strong></td>";
                        arrOutput += "<td style='text-align: center'><strong>" + gridDataLanhDao.Rows[m]["ma_nhanvien"].ToString() + "</strong></td>";
                        arrOutput += "<td class='min-width-130'><strong>" + gridDataLanhDao.Rows[m]["ten_nhanvien"].ToString() + "</strong></td>";
                        if (loai == 3)
                        {
                            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDao.Rows[m]["tangluong"].ToString())) + "</strong></td>";
                            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDao.Rows[m]["truluong"].ToString())) + "</strong></td>";
                        }
                        arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", dTongtien) + "</strong></td>";
                        arrOutput += "</tr>";
                    }
                }
            }
            
            arrOutput += "<tfoot>";
            arrOutput += "<tr style='background:orange;'>";
            arrOutput += "<td></td>";
            arrOutput += "<td></td>";
            arrOutput += "<td></td>";
            arrOutput += "<td></td>";
            if (loai == 3) {
                arrOutput += "<td></td>";
                arrOutput += "<td></td>";
            }
            arrOutput += "<td style='text-align: center'><strong>" + String.Format("{0:0,0}", tong) + "</strong></td>";
            arrOutput += "</tr>";
            arrOutput += "</tfoot>";
            
            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Tổng tiền lương phát triển thuê bao";
            dtDonVi = getDonVi();
        }
    }
}