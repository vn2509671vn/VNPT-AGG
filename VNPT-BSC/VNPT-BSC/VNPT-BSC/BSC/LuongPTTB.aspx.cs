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
        public DataTable dtDonVi = new DataTable();

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
            DataTable gridDataGianTiep = new DataTable();
            DataTable gridDataLanhDaoGianTiep = new DataTable();
            string timekey = nam.ToString() + thang.ToString("00");
            string sql = "";
            string sqlLanhDao = "";
            string sqlNVGianTiep = "";
            string sqlLDGianTiep = "";

            string dongbo_truluong_lanhdao_pbh = "EXEC sp_pttb_luu_truluong_lanhdao_pbh "  + "'" + timekey + "'";
            cn.ThucThiDL(dongbo_truluong_lanhdao_pbh);

            if (loai == 1)
            {
                // Tăng tiền
                //sql += "select donvi_id, ten_donvi, nvql, ten_nv as tennv, sum(tienluong) as tongtien ";
                ////sql += "from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] pttb, ";
                ////sql += "[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] nvkd, ";
                ////sql += "[OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                ////sql += "where pttb.donvi_id = dv.donvi_id ";
                ////if (donvi != 0) {
                ////    sql += "and dv.donvi_id = '" + donvi + "' ";
                ////}
                ////sql += "and pttb.ma_nv = nvkd.ma_nv ";
                ////sql += "and LEFT(pttb.timekey,6) = '" + timekey + "'";
                ////sql += " group by pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv ";
                ////sql += "order by dv.ten_donvi asc";
                //sql += "from [OLAP].[OLAP].[dbo].[ThangTMG_Luong]";
                //sql += "where LEFT(timekey,6) = '" + timekey + "'";
                //if (donvi != 0)
                //{
                //    sql += "and donvi_id = '" + donvi + "' ";
                //}
                //sql += " group by donvi_id, ten_donvi, nvql, ten_nv ";
                //sql += "order by ten_donvi asc";

                sql += "select a.donvi_id, dv.ten_donvi, a.nvql, nv.ten_nhanvien, sum(a.tongtien) as tongtien ";
                sql += "from ";
                sql += "(select pttb.donvi_id, pttb.nvql, sum(pttb.tienluong) as tongtien ";
                sql += "from [OLAP].[OLAP].[dbo].[ThangTMG_Luong] pttb ";
                sql += "where LEFT(pttb.timekey,6) = '" + timekey + "' ";
                sql += "group by pttb.donvi_id, pttb.ten_donvi, pttb.nvql, pttb.ten_nv ";
                sql += "union all ";
                sql += "select id_diaban_banthe, ma_nhanvien, (doanhthu_banthe + doanhthu_napthe_dungdiaban) ";
                sql += "from luong_banthe_napthe ";
                sql += "where timekey = '" + timekey + "' ";
                sql += "and ma_nhanvien is not null) a, qlns_nhanvien nv, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                sql += "where a.nvql = nv.ma_nhanvien ";
                sql += "and a.donvi_id = dv.donvi_id ";
                if (donvi != 0)
                {
                    sql += "and a.donvi_id = '" + donvi + "' ";
                }
                sql += "group by a.donvi_id, dv.ten_donvi, a.nvql, nv.ten_nhanvien ";
                sql += "order by dv.ten_donvi asc";

                //sqlLanhDao += "select nv.ma_nhanvien, nv.ten_nhanvien, pttb.*, dv.ten_donvi, ";
                //sqlLanhDao += "((( ";
                //sqlLanhDao += "select sum(a.tienluong) ";
                ////sqlLanhDao += "from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";
                //sqlLanhDao += "from [OLAP].[OLAP].[dbo].[ThangTMG_Luong] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";
                //sqlLanhDao += "where a.donvi_id = b.donvi_id ";
                //sqlLanhDao += "and LEFT(a.timekey,6) = '" + timekey + "' ";
                //sqlLanhDao += "and b.donvi_id = map.donvi_id_vmos ";
                //sqlLanhDao += ") * pttb.dongia)/1000) as tongtien ";
                //sqlLanhDao += "from qlns_dongia_pttbm_gdoc_pgdoc_pbh pttb, qlns_nhanvien nv, donvi_map map, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                //sqlLanhDao += "where nv.chucdanh = pttb.chucdanh ";
                //sqlLanhDao += "and nv.donvi = pttb.donvi ";
                //sqlLanhDao += "and nv.donvi = map.donvi_id ";
                //sqlLanhDao += "and map.donvi_id_vmos = dv.donvi_id ";
                //if (donvi != 0)
                //{
                //    sqlLanhDao += "and dv.donvi_id = '" + donvi + "' ";
                //}
                //sqlLanhDao += "and pttb.timekey = (";
                //sqlLanhDao += "select top 1 timekey from qlns_dongia_pttbm_gdoc_pgdoc_pbh where timekey <= '" + timekey + "' order by timekey desc";
                //sqlLanhDao += ")";

                //if (donvi == 1 || donvi == 0) {
                //    sqlNVGianTiep = "select pttb.*, dv.ten_donvi ";
                //    sqlNVGianTiep += "from pttb_chitiet_nhanvien_khoigiantiep pttb, qlns_donvi dv ";
                //    sqlNVGianTiep += "where pttb.donvi = dv.id ";
                //    sqlNVGianTiep += "and pttb.timekey = '" + timekey + "' ";
                //    if (donvi == 1)
                //    {
                //        sqlNVGianTiep += "and dv.id = 17 ";
                //    }
                //    sqlNVGianTiep += "and pttb.id_nhom_quyluong_pttb not in (5,6,7) order by dv.ten_donvi asc ";

                //    sqlLDGianTiep = "select pttb.*, dv.ten_donvi ";
                //    sqlLDGianTiep += "from pttb_chitiet_nhanvien_khoigiantiep pttb, qlns_donvi dv ";
                //    sqlLDGianTiep += "where pttb.donvi = dv.id ";
                //    sqlLDGianTiep += "and pttb.timekey = '" + timekey + "' ";
                //    if (donvi == 1)
                //    {
                //        sqlLDGianTiep += "and dv.id = 17 ";
                //    }
                //    sqlLDGianTiep += "and pttb.id_nhom_quyluong_pttb in (5) order by dv.ten_donvi asc ";
                //}
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

                sqlLanhDao += "SELECT ma_nhanvien, ten_nhanvien, ten_donvi, sum(truluong) as tongtien ";
                sqlLanhDao += "FROM [VNPT].[dbo].[tmp_truluong_lanhdao_pbh] ";
                sqlLanhDao += "where left(thoigian_tinhluong,6) = '" + timekey + "' ";
                if (donvi != 0)
                {
                    sqlLanhDao += "and donvi = '" + donvi + "' ";
                }
                sqlLanhDao += "group by ma_nhanvien, ten_nhanvien, ten_donvi ";

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
                ////sql += "select pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv, isnull(sum(pttb.tienluong),0) as tangluong ";
                ////sql += "from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] pttb, ";
                ////sql += "[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] nvkd, ";
                //sql += "select pttb.donvi_id, pttb.ten_donvi, pttb.nvql, pttb.ten_nv as tennv, isnull(sum(pttb.tienluong),0) as tangluong ";
                //sql += "from [OLAP].[OLAP].[dbo].[ThangTMG_Luong] pttb, ";

                //sql += "[OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                //sql += "where pttb.donvi_id = dv.donvi_id ";
                //if (donvi != 0)
                //{
                //    sql += "and dv.donvi_id = '" + donvi + "' ";
                //}

                ////sql += "and pttb.ma_nv = nvkd.ma_nv ";

                //sql += "and LEFT(pttb.timekey,6) = '" + timekey + "' ";
                ////sql += "group by pttb.donvi_id, dv.ten_donvi, nvkd.nvql, nvkd.tennv) tmp ";
                //sql += "group by pttb.donvi_id, pttb.ten_donvi, pttb.nvql, pttb.ten_nv) tmp ";
                sql += "select a.donvi_id, dv.ten_donvi, a.nvql, nv.ten_nhanvien as tennv, isnull(sum(a.tongtien),0) as tangluong ";
                sql += "from ";
                sql += "(select pttb.donvi_id, pttb.nvql, sum(pttb.tienluong) as tongtien ";
                sql += "from [OLAP].[OLAP].[dbo].[ThangTMG_Luong] pttb ";
                sql += "where LEFT(pttb.timekey,6) = '" + timekey + "' ";
                sql += "group by pttb.donvi_id, pttb.ten_donvi, pttb.nvql, pttb.ten_nv ";
                sql += "union all ";
                sql += "select id_diaban_banthe, ma_nhanvien, (doanhthu_banthe + doanhthu_napthe_dungdiaban) ";
                sql += "from luong_banthe_napthe ";
                sql += "where timekey = '" + timekey + "' ";
                sql += "and ma_nhanvien is not null) a, qlns_nhanvien nv, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] dv ";
                sql += "where a.nvql = nv.ma_nhanvien ";
                sql += "and a.donvi_id = dv.donvi_id ";
                if (donvi != 0)
                {
                    sql += "and a.donvi_id = '" + donvi + "' ";
                }
                sql += "group by a.donvi_id, dv.ten_donvi, a.nvql, nv.ten_nhanvien) tmp ";

                sql += "union all ";
                sql += "select b.Donvi_id,b.TenDonVi,b.NVQL,b.TenNV,0,isnull(sum(tienluong),0) truluong ";
                sql += "from [OLAP].[OLAP].[dbo].[Hien_Tru_Luong_PhatTrienTB] a,[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] b ";
                sql += "where  LEFT(a.timekey,6) = '" + timekey + "' ";
                sql += "and a.ma_nv = b.Ma_NV ";
                if (donvi != 0)
                {
                    sql += "and b.Donvi_id = '" + donvi + "' ";
                }

                //sql += "and b.NVQL not in (select b.NVQL from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] a,[OLAP].[OLAP].[dbo].[Dim_VNP_NhanVien_KD] b ";
                //sql += "where  LEFT(a.timekey,6) = '" + timekey + "' ";
                //sql += "and a.ma_nv = b.Ma_NV) ";
                sql += "and b.NVQL not in (select NVQL from [OLAP].[OLAP].[dbo].[ThangTMG_Luong] ";
                sql += "where  LEFT(timekey,6) = '" + timekey + "' )";

                sql += "group by b.Donvi_id,b.TenDonVi,b.NVQL,b.TenNV ";
                sql += "order by tmp.ten_donvi ";

                if (nam >= 2018)
                {
                    sqlLanhDao += "select nv.ma_nhanvien, nv.ten_nhanvien, pttb.*, dv.ten_donvi, 0 as tangluong, ";
                }
                else {
                    sqlLanhDao += "select nv.ma_nhanvien, nv.ten_nhanvien, pttb.*, dv.ten_donvi, ";
                    sqlLanhDao += "((( ";
                    sqlLanhDao += "select ISNULL(sum(a.tienluong),0) ";
                    //sqlLanhDao += "from [OLAP].[OLAP].[dbo].[Hien_Luong_PhatTrienTB] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";
                    sqlLanhDao += "from [OLAP].[OLAP].[dbo].[ThangTMG_Luong] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";

                    sqlLanhDao += "where a.donvi_id = b.donvi_id ";
                    sqlLanhDao += "and LEFT(a.timekey,6) = '" + timekey + "' ";
                    sqlLanhDao += "and b.donvi_id = map.donvi_id_vmos ";
                    sqlLanhDao += ") * pttb.dongia)/1000) as tangluong, ";
                }

                ////sqlLanhDao += "((( ";
                ////sqlLanhDao += "select ISNULL(sum(a.tienluong),0) ";
                ////sqlLanhDao += "from [OLAP].[OLAP].[dbo].[Hien_Tru_Luong_PhatTrienTB] a, [OLAP].[OLAP].[dbo].[Dim_PTTB_DonVi] b ";
                ////sqlLanhDao += "where a.donvi_id = b.donvi_id ";
                ////sqlLanhDao += "and LEFT(a.timekey,6) = '" + timekey + "' ";
                ////sqlLanhDao += "and b.donvi_id = map.donvi_id_vmos ";
                ////sqlLanhDao += "and a.truluong_gd_id != 0 ";
                ////sqlLanhDao += ") * pttb.dongia)/1000) as truluong ";
                sqlLanhDao += "(select ISNULL(sum(truluong),0) ";
                sqlLanhDao += "from [VNPT].[dbo].[tmp_truluong_lanhdao_pbh] ";
                sqlLanhDao += "where left(thoigian_tinhluong,6) = '" + timekey + "' ";
                sqlLanhDao += "and ma_nhanvien = nv.ma_nhanvien) as truluong ";

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

                //if (donvi == 1 || donvi == 0)
                //{
                //    sqlNVGianTiep = "select pttb.*, dv.ten_donvi ";
                //    sqlNVGianTiep += "from pttb_chitiet_nhanvien_khoigiantiep pttb, qlns_donvi dv ";
                //    sqlNVGianTiep += "where pttb.donvi = dv.id ";
                //    sqlNVGianTiep += "and pttb.timekey = '" + timekey + "' ";
                //    if (donvi == 1)
                //    {
                //        sqlNVGianTiep += "and dv.id = 17 ";
                //    }
                //    sqlNVGianTiep += "and pttb.id_nhom_quyluong_pttb not in (5,6,7) order by dv.ten_donvi asc";

                //    sqlLDGianTiep = "select pttb.*, dv.ten_donvi ";
                //    sqlLDGianTiep += "from pttb_chitiet_nhanvien_khoigiantiep pttb, qlns_donvi dv ";
                //    sqlLDGianTiep += "where pttb.donvi = dv.id ";
                //    sqlLDGianTiep += "and pttb.timekey = '" + timekey + "' ";
                //    if (donvi == 1)
                //    {
                //        sqlLDGianTiep += "and dv.id = 17 ";
                //    }
                //    sqlLDGianTiep += "and pttb.id_nhom_quyluong_pttb in (5) order by dv.ten_donvi asc";
                //}
            }

            try
            {
                gridData = cn.XemDL(sql);
                gridDataLanhDao = cn.XemDL(sqlLanhDao);
                if (sqlNVGianTiep != "" && sqlLDGianTiep != "")
                {
                    gridDataGianTiep = cn.XemDL(sqlNVGianTiep);
                    gridDataLanhDaoGianTiep = cn.XemDL(sqlLDGianTiep);
                }
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
                arrOutput += "<th class='text-center'>Tiền lương</th>";
                arrOutput += "<th class='text-center'>Trừ lương</th>";
            }
            arrOutput += "<th class='text-center'>Tổng tiền</th>";
            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                //arrOutput += "<tr><td colspan='5' class='text-center'>No item</td></tr>";
            }
            else
            {
                int n = 0, m = 0, y = 0, j = 0;
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
                        arrOutput += "<td style='text-align: center; font-weight: bold;'>" + (n + 1) + "</td>";
                        arrOutput += "<td style='font-weight: bold;'>" + gridData.Rows[n]["ten_donvi"].ToString() + "</td>";
                        arrOutput += "<td style='text-align: center; font-weight: bold;'>" + gridData.Rows[n]["nvql"].ToString() + "</td>";
                        arrOutput += "<td class='min-width-130' style='font-weight: bold;'>" + gridData.Rows[n]["tennv"].ToString() + "</td>";
                        if (loai == 3) {
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridData.Rows[n]["tangluong"].ToString())) + "</td>";
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridData.Rows[n]["truluong"].ToString())) + "</td>";
                        }
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", dTongtien) + "</td>";
                        arrOutput += "</tr>";
                    }

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
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + (m + n + 1) + "</td>";
                        arrOutput += "<td style='font-weight: bold'>" + gridDataLanhDao.Rows[m]["ten_donvi"].ToString() + "</td>";
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + gridDataLanhDao.Rows[m]["ma_nhanvien"].ToString() + "</td>";
                        arrOutput += "<td class='min-width-130' style='font-weight: bold'>" + gridDataLanhDao.Rows[m]["ten_nhanvien"].ToString() + "</td>";
                        if (loai == 3)
                        {
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDao.Rows[m]["tangluong"].ToString())) + "</td>";
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDao.Rows[m]["truluong"].ToString())) + "</td>";
                        }
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", dTongtien) + "</td>";
                        arrOutput += "</tr>";
                    }

                    for (y = 0; y < gridDataGianTiep.Rows.Count; y++)
                    {
                        double dTongtien = 0;
                        dTongtien = Convert.ToDouble(gridDataGianTiep.Rows[y]["luong_pttb"].ToString());


                        tong += dTongtien;
                        arrOutput += "<tr style='background:burlywood;'>";
                        arrOutput += "<td style='text-align: center; font-weight: bold;'>" + (y + m + n + 1) + "</td>";
                        arrOutput += "<td style='font-weight: bold;'>" + gridDataGianTiep.Rows[y]["ten_donvi"].ToString() + "</td>";
                        arrOutput += "<td style='text-align: center; font-weight: bold;'>" + gridDataGianTiep.Rows[y]["ma_nhanvien"].ToString() + "</td>";
                        arrOutput += "<td class='min-width-130' style='font-weight: bold;'>" + gridDataGianTiep.Rows[y]["ten_nhanvien"].ToString() + "</td>";
                        if (loai == 3)
                        {
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataGianTiep.Rows[y]["luong_pttb"].ToString())) + "</td>";
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", 0) + "</td>";
                        }
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", dTongtien) + "</td>";
                        arrOutput += "</tr>";
                    }

                    //for (j = 0; j < gridDataLanhDaoGianTiep.Rows.Count; j++)
                    //{
                    //    double dTongtien = 0;
                    //    dTongtien = Convert.ToDouble(gridDataLanhDaoGianTiep.Rows[j]["luong_pttb"].ToString());


                    //    tong += dTongtien;
                    //    arrOutput += "<tr style='background:paleturquoise;'>";
                    //    arrOutput += "<td style='text-align: center; font-weight: bold;'>" + (j + y + m + n + 1) + "</td>";
                    //    arrOutput += "<td style='font-weight: bold;'>" + gridDataLanhDaoGianTiep.Rows[j]["ten_donvi"].ToString() + "</td>";
                    //    arrOutput += "<td style='text-align: center; font-weight: bold;'>" + gridDataLanhDaoGianTiep.Rows[j]["ma_nhanvien"].ToString() + "</td>";
                    //    arrOutput += "<td class='min-width-130' style='font-weight: bold;'>" + gridDataLanhDaoGianTiep.Rows[j]["ten_nhanvien"].ToString() + "</td>";
                    //    if (loai == 3)
                    //    {
                    //        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDaoGianTiep.Rows[j]["luong_pttb"].ToString())) + "</td>";
                    //        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", 0) + "</td>";
                    //    }
                    //    arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", dTongtien) + "</td>";
                    //    arrOutput += "</tr>";
                    //}
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
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + (n + 1) + "</td>";
                        arrOutput += "<td style='font-weight: bold'>" + gridData.Rows[n]["ten_donvi"].ToString() + "</td>";
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + gridData.Rows[n]["nvql"].ToString() + "</td>";
                        arrOutput += "<td class='min-width-130' style='font-weight: bold'>" + gridData.Rows[n]["tennv"].ToString() + "</td>";
                        if (loai == 3)
                        {
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridData.Rows[n]["tangluong"].ToString())) + "</td>";
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridData.Rows[n]["truluong"].ToString())) + "</td>";
                        }
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", dTongtien) + "</td>";
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
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + (m + n + 1) + "</td>";
                        arrOutput += "<td style='font-weight: bold'>" + gridDataLanhDao.Rows[m]["ten_donvi"].ToString() + "</td>";
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + gridDataLanhDao.Rows[m]["ma_nhanvien"].ToString() + "</td>";
                        arrOutput += "<td class='min-width-130' style='font-weight: bold'>" + gridDataLanhDao.Rows[m]["ten_nhanvien"].ToString() + "</td>";
                        if (loai == 3)
                        {
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDao.Rows[m]["tangluong"].ToString())) + "</td>";
                            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDao.Rows[m]["truluong"].ToString())) + "</td>";
                        }
                        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", dTongtien) + "</td>";
                        arrOutput += "</tr>";
                    }
                }
                else if (nhom_nv == 4) {
                    //for (y = 0; y < gridDataGianTiep.Rows.Count; y++)
                    //{
                    //    double dTongtien = 0;
                    //    dTongtien = Convert.ToDouble(gridDataGianTiep.Rows[y]["luong_pttb"].ToString());


                    //    tong += dTongtien;
                    //    arrOutput += "<tr style='background:burlywood;'>";
                    //    arrOutput += "<td style='text-align: center; font-weight: bold;'>" + (y + m + n + 1) + "</td>";
                    //    arrOutput += "<td style='font-weight: bold;'>" + gridDataGianTiep.Rows[y]["ten_donvi"].ToString() + "</td>";
                    //    arrOutput += "<td style='text-align: center; font-weight: bold;'>" + gridDataGianTiep.Rows[y]["ma_nhanvien"].ToString() + "</td>";
                    //    arrOutput += "<td class='min-width-130' style='font-weight: bold;'>" + gridDataGianTiep.Rows[y]["ten_nhanvien"].ToString() + "</td>";
                    //    if (loai == 3)
                    //    {
                    //        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataGianTiep.Rows[y]["luong_pttb"].ToString())) + "</td>";
                    //        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", 0) + "</td>";
                    //    }
                    //    arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", dTongtien) + "</td>";
                    //    arrOutput += "</tr>";
                    //}
                }
                else if (nhom_nv == 5)
                {
                    //for (j = 0; j < gridDataLanhDaoGianTiep.Rows.Count; j++)
                    //{
                    //    double dTongtien = 0;
                    //    dTongtien = Convert.ToDouble(gridDataLanhDaoGianTiep.Rows[j]["luong_pttb"].ToString());


                    //    tong += dTongtien;
                    //    arrOutput += "<tr style='background:paleturquoise;'>";
                    //    arrOutput += "<td style='text-align: center; font-weight: bold;'>" + (j + y + m + n + 1) + "</td>";
                    //    arrOutput += "<td style='font-weight: bold;'>" + gridDataLanhDaoGianTiep.Rows[j]["ten_donvi"].ToString() + "</td>";
                    //    arrOutput += "<td style='text-align: center; font-weight: bold;'>" + gridDataLanhDaoGianTiep.Rows[j]["ma_nhanvien"].ToString() + "</td>";
                    //    arrOutput += "<td class='min-width-130' style='font-weight: bold;'>" + gridDataLanhDaoGianTiep.Rows[j]["ten_nhanvien"].ToString() + "</td>";
                    //    if (loai == 3)
                    //    {
                    //        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", Convert.ToDouble(gridDataLanhDaoGianTiep.Rows[j]["luong_pttb"].ToString())) + "</td>";
                    //        arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", 0) + "</td>";
                    //    }
                    //    arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", dTongtien) + "</td>";
                    //    arrOutput += "</tr>";
                    //}
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
            arrOutput += "<td style='text-align: center; font-weight: bold'>" + String.Format("{0:0,0}", tong) + "</td>";
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