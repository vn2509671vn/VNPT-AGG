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
    public partial class TinhLuong : System.Web.UI.Page
    {
        public static DataTable dtNhanVien = new DataTable();
        public class xepHang
        {
            public int donvi_id { get; set; }
            public decimal diem_kpi { get; set; }
            public decimal heso_vtcntt { get; set; }
            public decimal heso_tkc { get; set; }
            public decimal heso_ns { get; set; }
        }

        public class BaoHiem
        {
            public decimal bhtn { get; set; }
            public decimal bhxh { get; set; }
            public decimal bhyt { get; set; }
        }

        public class ngaycongNhanVien
        {
            public int id_nv { get; set; }
            public int ngaycongBHXH { get; set; }
            public int ngaycongTT { get; set; }
        }

        public static xepHang[] getListXH(int thang, int nam) {
            xepHang[] lstXepHang = new xepHang[17];
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            decimal diem_kpi_ttkd = 0;
            int tmpHang = 1;
            string sql = "select donvinhan, sum(diem_kpi) as diem_kpi from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' and donvinhan in (3,4,5,6,7,8,9,10,11,12,17,1) group by donvinhan order by diem_kpi DESC";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            for (int i = 0; i < tmp.Rows.Count; i++) {
                int donvinhan = Convert.ToInt32(tmp.Rows[i]["donvinhan"].ToString());
                string szTmp = tmp.Rows[i]["diem_kpi"].ToString();
                if (donvinhan == 1) {
                    diem_kpi_ttkd = Convert.ToDecimal(tmp.Rows[i]["diem_kpi"].ToString());
                    continue;
                }
                lstXepHang[tmpHang - 1] = new xepHang();
                lstXepHang[tmpHang - 1].donvi_id = donvinhan;
                lstXepHang[tmpHang - 1].diem_kpi = Convert.ToDecimal(tmp.Rows[i]["diem_kpi"].ToString());
                tmpHang++;
            }
            /*  
                2	Ban Giám Đốc
                13	Phòng Điều hành - Nghiệp vụ
                14	Phòng Kế hoạch - Kế toán
                15	Phòng Khách hàng Tổ chức - DN
                16	Đài Hỗ trợ Khách hàng
                18	Phòng Tổng hợp - Nhân sự
            */
            // 2	Ban Giám Đốc
            lstXepHang[11] = new xepHang();
            lstXepHang[11].donvi_id = 2;
            lstXepHang[11].diem_kpi = diem_kpi_ttkd;
            lstXepHang[11].heso_vtcntt = Convert.ToDecimal(1.1);
            lstXepHang[11].heso_tkc = Convert.ToDecimal(1.1);
            lstXepHang[11].heso_ns = Convert.ToDecimal(1.1);
            // 13	Phòng Điều hành - Nghiệp vụ
            lstXepHang[12] = new xepHang();
            lstXepHang[12].donvi_id = 13;
            lstXepHang[12].diem_kpi = Convert.ToDecimal(0.4)*1 + (Convert.ToDecimal(0.6) * diem_kpi_ttkd);
            lstXepHang[12].heso_vtcntt = Convert.ToDecimal(1.1);
            lstXepHang[12].heso_tkc = Convert.ToDecimal(1.1);
            lstXepHang[12].heso_ns = Convert.ToDecimal(1.1);
            // 14	Phòng Kế hoạch - Kế toán
            lstXepHang[13] = new xepHang();
            lstXepHang[13].donvi_id = 14;
            lstXepHang[13].diem_kpi = Convert.ToDecimal(0.4) * 1 + (Convert.ToDecimal(0.6) * diem_kpi_ttkd);
            lstXepHang[13].heso_vtcntt = Convert.ToDecimal(1.1);
            lstXepHang[13].heso_tkc = Convert.ToDecimal(1.1);
            lstXepHang[13].heso_ns = Convert.ToDecimal(1.1);
            // 15	Phòng Khách hàng Tổ chức - DN
            lstXepHang[14] = new xepHang();
            lstXepHang[14].donvi_id = 15;
            lstXepHang[14].diem_kpi = Convert.ToDecimal(0.4) * 1 + (Convert.ToDecimal(0.6) * diem_kpi_ttkd);
            lstXepHang[14].heso_vtcntt = Convert.ToDecimal(1.1);
            lstXepHang[14].heso_tkc = Convert.ToDecimal(1.1);
            lstXepHang[14].heso_ns = Convert.ToDecimal(1.1);
            // 16	Đài Hỗ trợ Khách hàng
            lstXepHang[15] = new xepHang();
            lstXepHang[15].donvi_id = 16;
            lstXepHang[15].diem_kpi = Convert.ToDecimal(0.4) * 1 + (Convert.ToDecimal(0.6) * diem_kpi_ttkd);
            lstXepHang[15].heso_vtcntt = Convert.ToDecimal(1.1);
            lstXepHang[15].heso_tkc = Convert.ToDecimal(1.1);
            lstXepHang[15].heso_ns = Convert.ToDecimal(1.1);
            // 18	Phòng Tổng hợp - Nhân sự
            lstXepHang[16] = new xepHang();
            lstXepHang[16].donvi_id = 18;
            lstXepHang[16].diem_kpi = Convert.ToDecimal(0.4) * 1 + (Convert.ToDecimal(0.6) * diem_kpi_ttkd);
            lstXepHang[16].heso_vtcntt = Convert.ToDecimal(1.1);
            lstXepHang[16].heso_tkc = Convert.ToDecimal(1.1);
            lstXepHang[16].heso_ns = Convert.ToDecimal(1.1);

            lstXepHang = exeLuongVTCNTT(lstXepHang, thang, nam);
            lstXepHang = exeLuongTKC(lstXepHang, thang, nam);
            lstXepHang = exeLuongNS(lstXepHang, thang, nam);
            return lstXepHang;
        }

        public static BaoHiem getBaoHiem() {
            Connection cn = new Connection();
            DataTable dtBaoHiem = new DataTable();
            BaoHiem bhResult = new BaoHiem();
            string sql = "select * from qlns_baohiem";
            try
            {
                dtBaoHiem = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (dtBaoHiem.Rows.Count > 0) {
                bhResult.bhtn = Convert.ToDecimal(dtBaoHiem.Rows[0]["bhtn_canhan"].ToString());
                bhResult.bhxh = Convert.ToDecimal(dtBaoHiem.Rows[0]["bhxh_canhan"].ToString());
                bhResult.bhyt = Convert.ToDecimal(dtBaoHiem.Rows[0]["bhyt_canhan"].ToString());
            }

            return bhResult;
        }

        public static xepHang[] exeLuongVTCNTT(xepHang[] listXepHang, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string szTime = "";
            szTime = nam.ToString() + thang.ToString("00");
            string sql = "select qlns_donvi.id, qlns_donvi.ten_donvi, tmp_doanhthu_vtcntt.thoigian, tmp_doanhthu_vtcntt.tong ";
            sql += "from tmp_doanhthu_vtcntt, qlns_donvi ";
            sql += "where tmp_doanhthu_vtcntt.thoigian = '" + szTime + "' ";
            sql += "and tmp_doanhthu_vtcntt.id_donvi = qlns_donvi.map_id_donvi ";
            sql += "order by tmp_doanhthu_vtcntt.tong DESC ";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                for (int nIndex = 0; nIndex < tmp.Rows.Count; nIndex++) {
                    int donvi_id = Convert.ToInt32(tmp.Rows[nIndex]["id"].ToString());
                    for (int nTmp = 0; nTmp < 11; nTmp++) {
                        if (listXepHang[nTmp].donvi_id == donvi_id) {
                            listXepHang[nTmp].heso_vtcntt = Convert.ToDecimal(1.1 - nIndex * 0.01);
                        }
                    }
                }
            }

            return listXepHang;
        }

        public static xepHang[] exeLuongTKC(xepHang[] listXepHang, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string szTime = "";
            szTime = nam.ToString() + thang.ToString("00");
            string sql = "select qlns_donvi.id, qlns_donvi.ten_donvi, tmp_doanhthu_tkc.thoigian, tmp_doanhthu_tkc.tong ";
            sql += "from tmp_doanhthu_tkc, qlns_donvi ";
            sql += "where tmp_doanhthu_tkc.thoigian = '" + szTime + "' ";
            sql += "and tmp_doanhthu_tkc.id_donvi = qlns_donvi.map_id_donvi ";
            sql += "and qlns_donvi.id not in (15) ";
            sql += "order by tmp_doanhthu_tkc.tong DESC ";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (tmp.Rows.Count > 0)
            {
                for (int nIndex = 0; nIndex < tmp.Rows.Count; nIndex++)
                {
                    int donvi_id = Convert.ToInt32(tmp.Rows[nIndex]["id"].ToString());
                    for (int nTmp = 0; nTmp < 11; nTmp++)
                    {
                        if (listXepHang[nTmp].donvi_id == donvi_id)
                        {
                            listXepHang[nTmp].heso_tkc = Convert.ToDecimal(1.1 - nIndex * 0.01);
                        }
                    }
                }
            }
            return listXepHang;
        }

        public static xepHang[] exeLuongNS(xepHang[] listXepHang, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string szTime = "";
            szTime = nam.ToString() + thang.ToString("00");
            string sql = "select *, ((tkc+vtcntt)/sl_nhanvien) as nangsuat from ";
            sql += "(select qlns_donvi.id, qlns_donvi.ten_donvi, tkc.thoigian, tkc.tong as tkc, ";
            sql += "( select tong from tmp_doanhthu_vtcntt vtcntt ";
            sql += "where vtcntt.id_donvi = tkc.id_donvi ";
            sql += "and vtcntt.thoigian = '" + szTime + "' ";
            sql += ") as vtcntt, ";
            sql += "( select count(*) from qlns_nhanvien where donvi =  qlns_donvi.id and chinhthuc = 1) as sl_nhanvien ";
            sql += "from tmp_doanhthu_tkc tkc, qlns_donvi ";
            sql += "where tkc.thoigian = '" + szTime + "' ";
            sql += "and tkc.id_donvi = qlns_donvi.map_id_donvi ";
            sql += "and qlns_donvi.id not in (15) ) as tonghop ORDER BY nangsuat DESC";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (tmp.Rows.Count > 0)
            {
                for (int nIndex = 0; nIndex < tmp.Rows.Count; nIndex++)
                {
                    int donvi_id = Convert.ToInt32(tmp.Rows[nIndex]["id"].ToString());
                    for (int nTmp = 0; nTmp < 11; nTmp++)
                    {
                        if (listXepHang[nTmp].donvi_id == donvi_id)
                        {
                            listXepHang[nTmp].heso_ns = Convert.ToDecimal(1.1 - nIndex * 0.01);
                        }
                    }
                }
            }
            return listXepHang;
        }

        public static void exeLuongPhanPhoi(int thang, int nam, decimal luongphanphoi, decimal fLuongDuyTri, decimal fLuongP3, decimal fLuongDuyTri_VTCNTT, decimal fLuongDuyTri_TKC, decimal fLuongDuyTri_NS) {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sqlExist = "select * from qlns_luongphanphoi where thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                tmp = cn.XemDL(sqlExist);
            }
            catch (Exception ex) {
                throw ex;
            }
            
            if (tmp.Rows.Count > 0)
            {
                // Update
                string szUpdate = "update qlns_luongphanphoi set luongphanphoi = '" + luongphanphoi + "', ";
                szUpdate += "luongp3 = '" + fLuongP3 + "', ";
                szUpdate += "luongduytri = '" + fLuongDuyTri + "', ";
                szUpdate += "luongduytri_VTCNTT = '" + fLuongDuyTri_VTCNTT + "', ";
                szUpdate += "luongduytri_TKC = '" + fLuongDuyTri_TKC + "', ";
                szUpdate += "luongduytri_NS = '" + fLuongDuyTri_NS + "' ";
                szUpdate += "where thang = '" + thang + "' ";
                szUpdate += "and nam = '" + nam + "' ";

                cn.ThucThiDL(szUpdate);
            }
            else { 
                // Insert
                string szInsert = "insert into qlns_luongphanphoi(thang, nam, luongphanphoi, luongp3, luongduytri, luongduytri_VTCNTT, luongduytri_TKC, luongduytri_NS) ";
                szInsert += "values ('" + thang + "', '" + nam + "', '" + luongphanphoi + "', '" + fLuongP3 + "', '" + fLuongDuyTri + "', '" + fLuongDuyTri_VTCNTT + "', '" + fLuongDuyTri_TKC + "', '" + fLuongDuyTri_NS + "')";
                cn.ThucThiDL(szInsert);
            }
        }

        public static DataTable getListNhanVien() {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select qlns_nhanvien.*, qlns_bacluong.heso_bacluong from qlns_nhanvien, qlns_bacluong where qlns_nhanvien.id_bacluong = qlns_bacluong.id and qlns_nhanvien.chinhthuc = 1";
            try{
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex){
                throw ex;
            }
            
            return dtResult;
        }

        public static DataTable getNhanVienDetail(int nhanvien, int thang, int nam)
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select qlns_nhanvien.*, qlns_bacluong.heso_bacluong, qlns_chamcong.ngaycong_bhxh, qlns_chamcong.ngaycong_thucte ";
            sql += "from qlns_nhanvien, qlns_bacluong, qlns_chamcong ";
            sql += "where qlns_nhanvien.id_bacluong = qlns_bacluong.id ";
            sql += "and qlns_nhanvien.id = qlns_chamcong.nhanvien ";
            sql += "and qlns_chamcong.thang = '" + thang + "' ";
            sql += "and qlns_chamcong.nam = '" + nam + "' ";
            sql += "and qlns_nhanvien.id = '" + nhanvien + "'";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtResult;
        }

        public static decimal getDiemBSCNhanVien(int thang, int nam, int id_nhanvien, int id_nhom, int id_donvi) {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            decimal dResult = 0;
            string sql = "";
            if (id_nhom != 17 && id_nhom != 18 && id_nhom != 19) {
                //sql = "select sum(diem_kpi) as diem from bsc_nhanvien where thang = '" + thang + "' and nam = '" + nam + "' and nhanviennhan = '" + id_nhanvien + "'";
                sql = "select tongdiem_bsc as diem from tmp_tongbsc_nhanvien where thang = '" + thang + "' and nam = '" + nam + "' and id_nhanvien = '" + id_nhanvien + "'";
            }
            else if (id_nhom == 18) {
                sql = "select sum(diem_kpi) as diem from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' and donvinhan = '" + id_donvi + "'";
            }
            else if (id_nhom == 17)
            {
                sql = "select (sum(diem_kpi)*0.6 + 1*0.4) as diem from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' and donvinhan = 1";
            }
            else {
                sql = "select sum(diem_kpi) as diem from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' and donvinhan = 1";
            }

            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            if (tmp.Rows.Count > 0) {
                if (tmp.Rows[0]["diem"].ToString() != "") {
                    dResult = Convert.ToDecimal(tmp.Rows[0]["diem"].ToString());
                }
            }
            return dResult;
        }

        public static decimal getHeSoPhuCapKiemNhiemNhanVien(int id_nv) {
            decimal dResult = 0;
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select max(qlns_phucapkiemnhiem.heso_phucap) from qlns_kiemnhiem_nhanvien, qlns_phucapkiemnhiem where qlns_kiemnhiem_nhanvien.id_kiemnhiem = qlns_phucapkiemnhiem.id and qlns_kiemnhiem_nhanvien.id_nhanvien = '" + id_nv + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0 && tmp.Rows[0][0].ToString() != "")
            {
                dResult = Convert.ToDecimal(tmp.Rows[0][0].ToString());
            }
            return dResult;
        }

        public static DataTable getListNhomDonVi()
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string slq = "select * from qlns_nhom_donvi";
            try
            {
                dtResult = cn.XemDL(slq);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtResult;
        }

        public static xepHang findXepHang(xepHang[] arrXepHang, int donvi) {
            xepHang result = new xepHang();
            for (int i = 0; i < arrXepHang.Length; i++)
            {
                if (donvi == arrXepHang[i].donvi_id) {
                    result = arrXepHang[i];
                    break;
                }
            }
            return result;
        }

        public static bool clearLuong(int thang, int nam) {
            Connection cn = new Connection();
            bool bResult = false;
            string sqlChamCong = "delete qlns_chamcong where thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlLuong = "delete qlns_luongphanphoi where thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlVTCNTT = "delete qlns_bangluong_duytri_VTCNTT where thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlTKC = "delete qlns_bangluong_duytri_TKC where thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlNS = "delete qlns_bangluong_nangsuat where thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlP3 = "delete qlns_bangluong_p3 where thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlQuyLuongTTKD = "delete qlns_bangluong_quyTTKD where thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlLuongChiTiet = "delete qlns_bangluong_chitiet where thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cn.ThucThiDL(sqlChamCong);
                cn.ThucThiDL(sqlLuong);
                cn.ThucThiDL(sqlVTCNTT);
                cn.ThucThiDL(sqlTKC);
                cn.ThucThiDL(sqlNS);
                cn.ThucThiDL(sqlP3);
                cn.ThucThiDL(sqlQuyLuongTTKD);
                cn.ThucThiDL(sqlLuongChiTiet);
                bResult = true;
            }
            catch (Exception ex) {
                throw ex;
            }

            return bResult;
        }

        public static void insertVTCNTT(int thang, int nam, int nhanvien, decimal p1, decimal heso)
        {
            Connection cn = new Connection();
            decimal p1_saudieuchinh = 0;
            p1_saudieuchinh = p1 * heso;
            string sql = "insert into qlns_bangluong_duytri_VTCNTT(thang, nam, nhanvien, p1_nhanvien, heso, p1_saudieuchinh, luong_VTCNTT) ";
            sql += "values('" + thang + "', '" + nam + "', '" + nhanvien + "', '" + p1 + "', '" + heso + "', '" + p1_saudieuchinh + "', 0)";
            try
            {
                cn.ThucThiDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public static void insertTKC(int thang, int nam, int nhanvien, decimal p1, decimal heso)
        {
            Connection cn = new Connection();
            decimal p1_saudieuchinh = 0;
            p1_saudieuchinh = p1 * heso;
            string sql = "insert into qlns_bangluong_duytri_TKC(thang, nam, nhanvien, p1_nhanvien, heso, p1_saudieuchinh, luong_TKC) ";
            sql += "values('" + thang + "', '" + nam + "', '" + nhanvien + "', '" + p1 + "', '" + heso + "', '" + p1_saudieuchinh + "', 0)";
            try
            {
                cn.ThucThiDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insertNS(int thang, int nam, int nhanvien, decimal p1, decimal heso)
        {
            Connection cn = new Connection();
            decimal p1_saudieuchinh = 0;
            p1_saudieuchinh = p1 * heso;
            string sql = "insert into qlns_bangluong_nangsuat(thang, nam, nhanvien, p1_nhanvien, heso, p1_saudieuchinh, luong_nangsuat) ";
            sql += "values('" + thang + "', '" + nam + "', '" + nhanvien + "', '" + p1 + "', '" + heso + "', '" + p1_saudieuchinh + "', 0)";
            try
            {
                cn.ThucThiDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insertP3(int thang, int nam, int nhanvien, decimal p1, decimal diem_bsc_donvi)
        {
            Connection cn = new Connection();
            decimal p1_saudieuchinh = 0;
            p1_saudieuchinh = p1 * diem_bsc_donvi;
            string sql = "insert into qlns_bangluong_p3(thang, nam, nhanvien, p1_nhanvien, diem_bsc_donvi, p1_saudieuchinh, luong_p3) ";
            sql += "values('" + thang + "', '" + nam + "', '" + nhanvien + "', '" + p1 + "', '" + diem_bsc_donvi + "', '" + p1_saudieuchinh + "', 0)";
            try
            {
                cn.ThucThiDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insertQuyLuongTTKD(int thang, int nam, int id_nhom_donvi, decimal luong_duytri, decimal luong_p3, decimal luong_tong)
        {
            Connection cn = new Connection();
            string sql = "insert into qlns_bangluong_quyTTKD(thang, nam, id_nhom_donvi, luong_duytri, luong_p3, luong_tonghop) ";
            sql += "values('" + thang + "', '" + nam + "', '" + id_nhom_donvi + "', '" + luong_duytri + "', '" + luong_p3 + "', '" + luong_tong + "')";
            try
            {
                cn.ThucThiDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insertLuongChiTiet(int thang, int nam, BaoHiem baohiem)
        {
            Connection cn = new Connection();
            DataTable dtNhanVien = new DataTable();
            DataTable dtNhanVienChiTiet = new DataTable();
            dtNhanVien = getListNhanVien();
            if (dtNhanVien.Rows.Count > 0) {
                for (int nIndex = 0; nIndex < dtNhanVien.Rows.Count; nIndex++) {
                    int id_nhanvien = Convert.ToInt32(dtNhanVien.Rows[nIndex]["id"].ToString());
                    dtNhanVienChiTiet = getNhanVienDetail(id_nhanvien, thang, nam);
                    if (dtNhanVienChiTiet.Rows.Count > 0) {
                        string id_pttb = dtNhanVienChiTiet.Rows[0]["id_pttb"].ToString();
                        bool dangvien = Convert.ToBoolean(dtNhanVienChiTiet.Rows[0]["dangvien"].ToString());
                        int nhom_donvi = Convert.ToInt32(dtNhanVienChiTiet.Rows[0]["id_nhom_donvi"].ToString());
                        int id_donvi = Convert.ToInt32(dtNhanVienChiTiet.Rows[0]["donvi"].ToString());
                        int chucdanh = Convert.ToInt32(dtNhanVienChiTiet.Rows[0]["chucdanh"].ToString());
                        int ngaycong_bhxh = Convert.ToInt32(dtNhanVienChiTiet.Rows[0]["ngaycong_bhxh"].ToString());
                        int ngaycong_thucte = Convert.ToInt32(dtNhanVienChiTiet.Rows[0]["ngaycong_thucte"].ToString());
                        decimal tienluong = Convert.ToDecimal(dtNhanVienChiTiet.Rows[0]["luong_duytri"].ToString());
                        decimal diem_bsc = getDiemBSCNhanVien(thang, nam, id_nhanvien, nhom_donvi, id_donvi);
                        decimal heso_phucap_kiemnhiem = getHeSoPhuCapKiemNhiemNhanVien(id_nhanvien);
                        decimal heso_bacluong_duytri = 0;
                        decimal heso_bacluong_p3 = 0;
                        if (ngaycong_bhxh != 0)
                        {
                            heso_bacluong_duytri = Convert.ToDecimal(dtNhanVienChiTiet.Rows[0]["heso_bacluong"].ToString()) * ngaycong_bhxh / ngaycong_bhxh;
                            heso_bacluong_p3 = Convert.ToDecimal(dtNhanVienChiTiet.Rows[0]["heso_bacluong"].ToString()) * ngaycong_bhxh / ngaycong_bhxh * diem_bsc;
                        }

                        decimal tong_heso_luongduytri = tongHSL_DuyTri_TheoNhom(nhom_donvi, thang, nam);
                        decimal tong_luongduytri = tongLuong_DuyTri_TheoNhom(nhom_donvi, thang, nam);
                        
                        decimal tong_heso_p3 = tongHSL_P3_TheoNhom(nhom_donvi, thang, nam);
                        decimal tong_luongp3 = tongLuong_P3_TheoNhom(nhom_donvi, thang, nam);

                        decimal luong_duytri_canhan = 0;
                        decimal luong_p3_canhan = 0;
                        decimal luong_phattrien_tb_canhan = tongtien_pttb(id_pttb, thang, nam);
                        
                        if (tong_heso_luongduytri != 0) {
                            luong_duytri_canhan = tong_luongduytri / tong_heso_luongduytri * heso_bacluong_duytri;
                        }

                        if (tong_heso_p3 != 0) {
                            luong_p3_canhan = tong_luongp3 / tong_heso_p3 * heso_bacluong_p3;
                        }

                        if (chucdanh == 7 || chucdanh == 8) {
                            luong_phattrien_tb_canhan = luong_phattrien_tb_canhan * Convert.ToDecimal(0.9);
                        }

                        decimal phi_bhtn = 0;
                        decimal phi_bhxh = 0;
                        decimal phi_bhyt = 0;
                        decimal phi_congdoan = 0;
                        decimal phi_dangvien = 0;

                        phi_bhtn = tienluong * baohiem.bhtn;
                        phi_bhxh = tienluong * baohiem.bhxh;
                        phi_bhyt = tienluong * baohiem.bhyt;
                        if ((luong_duytri_canhan + luong_p3_canhan + luong_phattrien_tb_canhan - phi_bhtn - phi_bhxh - phi_bhyt) > 12100000)
                        {
                            phi_congdoan = 121000;
                        }
                        else {
                            phi_congdoan = (luong_duytri_canhan + luong_p3_canhan + luong_phattrien_tb_canhan - phi_bhtn - phi_bhxh - phi_bhyt) * Convert.ToDecimal(0.01);
                        }

                        if (dangvien) {
                            phi_dangvien = (luong_duytri_canhan + luong_p3_canhan + luong_phattrien_tb_canhan) * Convert.ToDecimal(0.01);
                        }

                        decimal tongtien_kiemnhiem = 0;
                        tongtien_kiemnhiem = heso_phucap_kiemnhiem * 1210000;

                        string sqlInsert = "insert into qlns_bangluong_chitiet(thang, nam, nhanvien, heso_bsc, luong_duytri, luong_p3, luong_phattrien_tb, bhtn, bhxh, bhyt, congdoan_phi, dang_phi, phucap_kiemnhiem) ";
                        sqlInsert += "values('" + thang + "', '" + nam + "', '" + id_nhanvien + "', '" + diem_bsc + "', '" + luong_duytri_canhan + "', '" + luong_p3_canhan + "', '" + luong_phattrien_tb_canhan + "', '" + phi_bhtn + "', '" + phi_bhxh + "', '" + phi_bhyt + "', '" + phi_congdoan + "', '" + phi_dangvien + "', '" + tongtien_kiemnhiem + "')";

                        try
                        {
                            cn.ThucThiDL(sqlInsert);
                        }
                        catch (Exception ex) { 
                            throw ex;
                        }
                    }
                }
            }
        }

        public static void updateVTCNTT(int thang, int nam, decimal luongVTCNTT) {
            Connection cn = new Connection();
            DataTable dtTmp = new DataTable();
            string sql = "select * from qlns_bangluong_duytri_VTCNTT where thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                dtTmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (dtTmp.Rows.Count > 0) {
                decimal tongP1 = 0;
                for (int nIndex1 = 0; nIndex1 < dtTmp.Rows.Count; nIndex1++)
                {
                    decimal p1 = Convert.ToDecimal(dtTmp.Rows[nIndex1]["p1_saudieuchinh"].ToString());
                    tongP1 += p1;
                }

                for (int nIndex2 = 0; nIndex2 < dtTmp.Rows.Count; nIndex2++)
                {
                    decimal p1_saudieuchinh = Convert.ToDecimal(dtTmp.Rows[nIndex2]["p1_saudieuchinh"].ToString());
                    int nhanvien = Convert.ToInt32(dtTmp.Rows[nIndex2]["nhanvien"].ToString());
                    decimal luongDuyTriVTCNTT = p1_saudieuchinh / tongP1 * luongVTCNTT;
                    string szUpdate = "update qlns_bangluong_duytri_VTCNTT set luong_VTCNTT = '" + luongDuyTriVTCNTT + "' where thang = '" + thang + "' and nam = '" + nam + "' and nhanvien = '" + nhanvien + "'";
                    try
                    {
                        cn.ThucThiDL(szUpdate);
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }
        }

        public static void updateTKC(int thang, int nam, decimal luongTKC)
        {
            Connection cn = new Connection();
            DataTable dtTmp = new DataTable();
            string sql = "select * from qlns_bangluong_duytri_TKC where thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                dtTmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtTmp.Rows.Count > 0)
            {
                decimal tongP1 = 0;
                for (int nIndex1 = 0; nIndex1 < dtTmp.Rows.Count; nIndex1++)
                {
                    decimal p1 = Convert.ToDecimal(dtTmp.Rows[nIndex1]["p1_saudieuchinh"].ToString());
                    tongP1 += p1;
                }

                for (int nIndex2 = 0; nIndex2 < dtTmp.Rows.Count; nIndex2++)
                {
                    decimal p1_saudieuchinh = Convert.ToDecimal(dtTmp.Rows[nIndex2]["p1_saudieuchinh"].ToString());
                    int nhanvien = Convert.ToInt32(dtTmp.Rows[nIndex2]["nhanvien"].ToString());
                    decimal luongDuyTriTKC = p1_saudieuchinh / tongP1 * luongTKC;
                    string szUpdate = "update qlns_bangluong_duytri_TKC set luong_TKC = '" + luongDuyTriTKC + "' where thang = '" + thang + "' and nam = '" + nam + "' and nhanvien = '" + nhanvien + "'";
                    try
                    {
                        cn.ThucThiDL(szUpdate);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public static void updateNS(int thang, int nam, decimal luongNS)
        {
            Connection cn = new Connection();
            DataTable dtTmp = new DataTable();
            string sql = "select * from qlns_bangluong_nangsuat where thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                dtTmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtTmp.Rows.Count > 0)
            {
                decimal tongP1 = 0;
                for (int nIndex1 = 0; nIndex1 < dtTmp.Rows.Count; nIndex1++)
                {
                    decimal p1 = Convert.ToDecimal(dtTmp.Rows[nIndex1]["p1_saudieuchinh"].ToString());
                    tongP1 += p1;
                }

                for (int nIndex2 = 0; nIndex2 < dtTmp.Rows.Count; nIndex2++)
                {
                    decimal p1_saudieuchinh = Convert.ToDecimal(dtTmp.Rows[nIndex2]["p1_saudieuchinh"].ToString());
                    int nhanvien = Convert.ToInt32(dtTmp.Rows[nIndex2]["nhanvien"].ToString());
                    decimal luongDuyTriNS = p1_saudieuchinh / tongP1 * luongNS;
                    string szUpdate = "update qlns_bangluong_nangsuat set luong_nangsuat = '" + luongDuyTriNS + "' where thang = '" + thang + "' and nam = '" + nam + "' and nhanvien = '" + nhanvien + "'";
                    try
                    {
                        cn.ThucThiDL(szUpdate);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public static void updateP3(int thang, int nam, decimal luongP3)
        {
            Connection cn = new Connection();
            DataTable dtTmp = new DataTable();
            string sql = "select * from qlns_bangluong_p3 where thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                dtTmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtTmp.Rows.Count > 0)
            {
                decimal tongP1 = 0;
                for (int nIndex1 = 0; nIndex1 < dtTmp.Rows.Count; nIndex1++)
                {
                    decimal p1 = Convert.ToDecimal(dtTmp.Rows[nIndex1]["p1_saudieuchinh"].ToString());
                    tongP1 += p1;
                }

                for (int nIndex2 = 0; nIndex2 < dtTmp.Rows.Count; nIndex2++)
                {
                    decimal p1_saudieuchinh = Convert.ToDecimal(dtTmp.Rows[nIndex2]["p1_saudieuchinh"].ToString());
                    int nhanvien = Convert.ToInt32(dtTmp.Rows[nIndex2]["nhanvien"].ToString());
                    decimal dLuongP3 = p1_saudieuchinh / tongP1 * luongP3;
                    string szUpdate = "update qlns_bangluong_p3 set luong_p3 = '" + dLuongP3 + "' where thang = '" + thang + "' and nam = '" + nam + "' and nhanvien = '" + nhanvien + "'";
                    try
                    {
                        cn.ThucThiDL(szUpdate);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public static void tinhLuongDuyTriVTCNTT(int thang, int nam, xepHang[] arrXepHang, decimal dLuongDuyTriVTCNTT) {
            DataTable dtNhanVien = new DataTable();
            dtNhanVien = getListNhanVien();
            for (int nIndex = 0; nIndex < dtNhanVien.Rows.Count; nIndex++)
            {
                xepHang xhNhanVien = new xepHang();
                int nhanvien = Convert.ToInt32(dtNhanVien.Rows[nIndex]["id"].ToString());
                int donvi = Convert.ToInt32(dtNhanVien.Rows[nIndex]["donvi"].ToString());
                decimal p1 = Convert.ToDecimal(dtNhanVien.Rows[nIndex]["heso_bacluong"].ToString());
                bool thaisan = Convert.ToBoolean(dtNhanVien.Rows[nIndex]["thaisan"].ToString());
                if (thaisan) {
                    p1 = p1 / 2;
                }
                xhNhanVien = findXepHang(arrXepHang, donvi);
                insertVTCNTT(thang, nam, nhanvien, p1, xhNhanVien.heso_vtcntt);
            }
            updateVTCNTT(thang, nam, dLuongDuyTriVTCNTT);
        }

        public static void tinhLuongDuyTriTKC(int thang, int nam, xepHang[] arrXepHang, decimal dLuongDuyTriTKC)
        {
            DataTable dtNhanVien = new DataTable();
            dtNhanVien = getListNhanVien();
            for (int nIndex = 0; nIndex < dtNhanVien.Rows.Count; nIndex++)
            {
                xepHang xhNhanVien = new xepHang();
                int nhanvien = Convert.ToInt32(dtNhanVien.Rows[nIndex]["id"].ToString());
                int donvi = Convert.ToInt32(dtNhanVien.Rows[nIndex]["donvi"].ToString());
                decimal p1 = Convert.ToDecimal(dtNhanVien.Rows[nIndex]["heso_bacluong"].ToString());
                bool thaisan = Convert.ToBoolean(dtNhanVien.Rows[nIndex]["thaisan"].ToString());
                if (thaisan)
                {
                    p1 = p1 / 2;
                }
                xhNhanVien = findXepHang(arrXepHang, donvi);
                insertTKC(thang, nam, nhanvien, p1, xhNhanVien.heso_tkc);
            }
            updateTKC(thang, nam, dLuongDuyTriTKC);
        }

        public static void tinhLuongNS(int thang, int nam, xepHang[] arrXepHang, decimal dLuongNS)
        {
            DataTable dtNhanVien = new DataTable();
            dtNhanVien = getListNhanVien();
            for (int nIndex = 0; nIndex < dtNhanVien.Rows.Count; nIndex++)
            {
                xepHang xhNhanVien = new xepHang();
                int nhanvien = Convert.ToInt32(dtNhanVien.Rows[nIndex]["id"].ToString());
                int donvi = Convert.ToInt32(dtNhanVien.Rows[nIndex]["donvi"].ToString());
                decimal p1 = Convert.ToDecimal(dtNhanVien.Rows[nIndex]["heso_bacluong"].ToString());
                bool thaisan = Convert.ToBoolean(dtNhanVien.Rows[nIndex]["thaisan"].ToString());
                if (thaisan)
                {
                    p1 = p1 / 2;
                }
                xhNhanVien = findXepHang(arrXepHang, donvi);
                insertNS(thang, nam, nhanvien, p1, xhNhanVien.heso_ns);
            }
            updateNS(thang, nam, dLuongNS);
        }

        public static void tinhLuongP3(int thang, int nam, xepHang[] arrXepHang, decimal dLuongP3)
        {
            DataTable dtNhanVien = new DataTable();
            dtNhanVien = getListNhanVien();
            for (int nIndex = 0; nIndex < dtNhanVien.Rows.Count; nIndex++)
            {
                xepHang xhNhanVien = new xepHang();
                int nhanvien = Convert.ToInt32(dtNhanVien.Rows[nIndex]["id"].ToString());
                int donvi = Convert.ToInt32(dtNhanVien.Rows[nIndex]["donvi"].ToString());
                decimal p1 = Convert.ToDecimal(dtNhanVien.Rows[nIndex]["heso_bacluong"].ToString());
                bool thaisan = Convert.ToBoolean(dtNhanVien.Rows[nIndex]["thaisan"].ToString());
                if (thaisan)
                {
                    p1 = p1 / 2;
                }
                xhNhanVien = findXepHang(arrXepHang, donvi);
                insertP3(thang, nam, nhanvien, p1, xhNhanVien.diem_kpi);
            }
            updateP3(thang, nam, dLuongP3);
        }

        public static decimal p3_nhom_donvi(int id_nhom, int thang, int nam) {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            decimal dResult = 0;
            string sql = "select qlns_nhanvien.id_nhom_donvi, sum(qlns_bangluong_p3.luong_p3) as p3 ";
            sql += "from qlns_bangluong_p3, qlns_nhanvien ";
            sql += "where qlns_nhanvien.id = qlns_bangluong_p3.nhanvien ";
            sql += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "' ";
            sql += "and qlns_bangluong_p3.thang = '" + thang + "' ";
            sql += "and qlns_bangluong_p3.nam = '" + nam + "' ";
            sql += "and qlns_nhanvien.chinhthuc = 1 ";
            sql += "group by qlns_nhanvien.id_nhom_donvi";

            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                dResult = Convert.ToDecimal(tmp.Rows[0]["p3"].ToString());
            }

            return dResult;
        }

        public static decimal duytri_nhom_donvi(int id_nhom, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable tmpNS = new DataTable();
            DataTable tmpTKC = new DataTable();
            DataTable tmpVTCNTT = new DataTable();
            decimal dResult = 0;
            string sqlNS = "select qlns_nhanvien.id_nhom_donvi, sum(qlns_bangluong_nangsuat.luong_nangsuat) as ns ";
            sqlNS += "from qlns_bangluong_nangsuat, qlns_nhanvien ";
            sqlNS += "where qlns_nhanvien.id = qlns_bangluong_nangsuat.nhanvien ";
            sqlNS += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "' ";
            sqlNS += "and qlns_bangluong_nangsuat.thang = '" + thang + "' ";
            sqlNS += "and qlns_bangluong_nangsuat.nam = '" + nam + "' ";
            sqlNS += "and qlns_nhanvien.chinhthuc = 1 ";
            sqlNS += "group by qlns_nhanvien.id_nhom_donvi";

            string sqlTKC = "select qlns_nhanvien.id_nhom_donvi, sum(qlns_bangluong_duytri_TKC.luong_TKC) as tkc ";
            sqlTKC += "from qlns_bangluong_duytri_TKC, qlns_nhanvien ";
            sqlTKC += "where qlns_nhanvien.id = qlns_bangluong_duytri_TKC.nhanvien ";
            sqlTKC += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "' ";
            sqlTKC += "and qlns_bangluong_duytri_TKC.thang = '" + thang + "' ";
            sqlTKC += "and qlns_bangluong_duytri_TKC.nam = '" + nam + "' ";
            sqlTKC += "and qlns_nhanvien.chinhthuc = 1 ";
            sqlTKC += "group by qlns_nhanvien.id_nhom_donvi";

            string sqlVTCNTT = "select qlns_nhanvien.id_nhom_donvi, sum(qlns_bangluong_duytri_VTCNTT.luong_VTCNTT) as vtcntt ";
            sqlVTCNTT += "from qlns_bangluong_duytri_VTCNTT, qlns_nhanvien ";
            sqlVTCNTT += "where qlns_nhanvien.id = qlns_bangluong_duytri_VTCNTT.nhanvien ";
            sqlVTCNTT += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "' ";
            sqlVTCNTT += "and qlns_bangluong_duytri_VTCNTT.thang = '" + thang + "' ";
            sqlVTCNTT += "and qlns_bangluong_duytri_VTCNTT.nam = '" + nam + "' ";
            sqlVTCNTT += "and qlns_nhanvien.chinhthuc = 1 ";
            sqlVTCNTT += "group by qlns_nhanvien.id_nhom_donvi";

            try
            {
                tmpNS = cn.XemDL(sqlNS);
                tmpTKC = cn.XemDL(sqlTKC);
                tmpVTCNTT = cn.XemDL(sqlVTCNTT);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (tmpNS.Rows.Count > 0 && tmpTKC.Rows.Count > 0 && tmpVTCNTT.Rows.Count > 0)
            {
                decimal ns = Convert.ToDecimal(tmpNS.Rows[0]["ns"].ToString());
                decimal tkc = Convert.ToDecimal(tmpTKC.Rows[0]["tkc"].ToString());
                decimal vtcntt = Convert.ToDecimal(tmpVTCNTT.Rows[0]["vtcntt"].ToString());
                dResult = ns + tkc + vtcntt;
            }

            return dResult;
        }

        public static void tinhQuyLuongTTKD(int thang, int nam)
        {
            DataTable dtNhomDonvi = new DataTable();
            dtNhomDonvi = getListNhomDonVi();
            if (dtNhomDonvi.Rows.Count > 0) {
                for (int nIndex = 0; nIndex < dtNhomDonvi.Rows.Count; nIndex++)
                {
                    decimal duytri = 0;
                    decimal p3 = 0;
                    decimal tong = 0;
                    int id_nhom = Convert.ToInt32(dtNhomDonvi.Rows[nIndex]["id"].ToString());
                    duytri = duytri_nhom_donvi(id_nhom, thang, nam);
                    p3 = p3_nhom_donvi(id_nhom, thang, nam);
                    tong = duytri + p3;
                    insertQuyLuongTTKD(thang, nam, id_nhom, duytri, p3, tong);
                }
            }
        }

        public static decimal tongHSL_DuyTri_TheoNhom(int id_nhom, int thang, int nam) {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            decimal dResult = 0;
            string sql = "select sum(qlns_bacluong.heso_bacluong*qlns_chamcong.ngaycong_bhxh/qlns_chamcong.ngaycong_bhxh) as tong ";
            sql += "from qlns_nhanvien, qlns_bacluong, qlns_chamcong ";
            sql += "where qlns_nhanvien.id_bacluong = qlns_bacluong.id ";
            sql += "and qlns_nhanvien.id = qlns_chamcong.nhanvien ";
            sql += "and qlns_chamcong.ngaycong_thucte != 0 ";
            sql += "and qlns_chamcong.ngaycong_bhxh != 0 ";
            sql += "and qlns_nhanvien.chinhthuc = 1 ";
            sql += "and qlns_chamcong.thang = '" + thang + "' ";
            sql += "and qlns_chamcong.nam = '" + nam + "' ";
            sql += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                dResult = Convert.ToDecimal(tmp.Rows[0]["tong"].ToString());
            }

            return dResult;
        }

        public static decimal tongHSL_P3_TheoNhom(int id_nhom, int thang, int nam)
        {
            /*
                1	Phòng bán hàng KV An Phú
                2	Phòng bán hàng KV Châu Đốc
                3	Phòng bán hàng KV Chợ Mới
                4	Phòng bán hàng KV Châu Phú
                5	Phòng bán hàng KV Châu Thành
                6	Phòng bán hàng KV Phú Tân
                7	Phòng bán hàng KV Tịnh Biên
                8	Phòng bán hàng KV Tân Châu
                9	Phòng bán hàng KV Thoại Sơn
                10	Phòng bán hàng KV Tri Tôn
                11	Phòng bán hàng KV Long Xuyên
                12	Đài Hỗ trợ Khách hàng
                13	Phòng Điều hành - Nghiệp Vụ
                14	Phòng Bán Hàng Doanh Nghiệp
                15	Phòng Kế hoạch - Kế toán
                16	Phòng Tổng hợp - Nhân sự
                17	Trưởng phòng chức năng
                18	GĐ phòng Bán hàng Khu vực
                19	Ban Giám đốc 
            */
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            xepHang xhTmp = new xepHang();
            decimal dResult = 0;
            string sql = "";
            if (id_nhom != 17 && id_nhom != 18 && id_nhom != 19)
            {
                sql = "select sum(qlns_bacluong.heso_bacluong*qlns_chamcong.ngaycong_bhxh/qlns_chamcong.ngaycong_bhxh*tmp.diem) as tong ";
                sql += "from qlns_nhanvien, qlns_bacluong, qlns_chamcong, ";
                //sql += "(select nhanviennhan, sum(diem_kpi) as diem from bsc_nhanvien where thang = '" + thang + "' and nam = '" + nam + "' group by nhanviennhan) tmp ";
                sql += "(select id_nhanvien, tongdiem_bsc as diem from tmp_tongbsc_nhanvien where thang = '" + thang + "' and nam = '" + nam + "') tmp ";
                sql += "where qlns_nhanvien.id_bacluong = qlns_bacluong.id ";
                sql += "and qlns_nhanvien.id = qlns_chamcong.nhanvien ";
                sql += "and qlns_chamcong.ngaycong_thucte != 0 ";
                sql += "and qlns_chamcong.ngaycong_bhxh != 0 ";
                sql += "and qlns_chamcong.thang = '" + thang + "' ";
                sql += "and qlns_chamcong.nam = '" + nam + "' ";
                sql += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "' ";
                //sql += "and qlns_nhanvien.id = tmp.nhanviennhan ";
                sql += "and qlns_nhanvien.id = tmp.id_nhanvien ";
                sql += "and qlns_nhanvien.chinhthuc = 1";
            }
            else if (id_nhom == 18)
            {
                sql = "select sum(qlns_bacluong.heso_bacluong*qlns_chamcong.ngaycong_bhxh/qlns_chamcong.ngaycong_bhxh*tmp.diem) as tong ";
                sql += "from qlns_nhanvien, qlns_bacluong, qlns_chamcong, ";
                sql += "(select donvinhan, sum(diem_kpi) as diem from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' group by donvinhan) tmp ";
                sql += "where qlns_nhanvien.id_bacluong = qlns_bacluong.id ";
                sql += "and qlns_nhanvien.id = qlns_chamcong.nhanvien ";
                sql += "and qlns_chamcong.ngaycong_thucte != 0 ";
                sql += "and qlns_chamcong.ngaycong_bhxh != 0 ";
                sql += "and qlns_chamcong.thang = '" + thang + "' ";
                sql += "and qlns_chamcong.nam = '" + nam + "' ";
                sql += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "' ";
                sql += "and qlns_nhanvien.donvi = tmp.donvinhan ";
                sql += "and qlns_nhanvien.chinhthuc = 1";
            }
            else if (id_nhom == 17)
            {
                sql = "select sum(qlns_bacluong.heso_bacluong*qlns_chamcong.ngaycong_bhxh/qlns_chamcong.ngaycong_bhxh*tmp.diem) as tong ";
                sql += "from qlns_nhanvien, qlns_bacluong, qlns_chamcong, ";
                sql += "(select donvinhan, (sum(diem_kpi)*0.6 + 1*0.4) as diem from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' and donvinhan = 1 group by donvinhan) tmp ";
                sql += "where qlns_nhanvien.id_bacluong = qlns_bacluong.id ";
                sql += "and qlns_nhanvien.id = qlns_chamcong.nhanvien ";
                sql += "and qlns_chamcong.ngaycong_thucte != 0 ";
                sql += "and qlns_chamcong.ngaycong_bhxh != 0 ";
                sql += "and qlns_chamcong.thang = '" + thang + "' ";
                sql += "and qlns_chamcong.nam = '" + nam + "' ";
                sql += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "' ";
                sql += "and qlns_nhanvien.chinhthuc = 1";
            }
            else { 
                // Dành cho ban giám đốc
                sql = "select sum(qlns_bacluong.heso_bacluong*qlns_chamcong.ngaycong_bhxh/qlns_chamcong.ngaycong_bhxh*tmp.diem) as tong ";
                sql += "from qlns_nhanvien, qlns_bacluong, qlns_chamcong, ";
                sql += "(select donvinhan, sum(diem_kpi) as diem from bsc_donvi where thang = '" + thang + "' and nam = '" + nam + "' and donvinhan = 1 group by donvinhan) tmp ";
                sql += "where qlns_nhanvien.id_bacluong = qlns_bacluong.id ";
                sql += "and qlns_nhanvien.id = qlns_chamcong.nhanvien ";
                sql += "and qlns_chamcong.ngaycong_thucte != 0 ";
                sql += "and qlns_chamcong.ngaycong_bhxh != 0 ";
                sql += "and qlns_chamcong.thang = '" + thang + "' ";
                sql += "and qlns_chamcong.nam = '" + nam + "' ";
                sql += "and qlns_nhanvien.id_nhom_donvi = '" + id_nhom + "' ";
                sql += "and qlns_nhanvien.chinhthuc = 1";
            }

            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (tmp.Rows.Count > 0)
            {
                if (tmp.Rows[0]["tong"].ToString() != "") {
                    dResult = Convert.ToDecimal(tmp.Rows[0]["tong"].ToString());
                }
            }

            return dResult;
        }

        public static decimal tongLuong_DuyTri_TheoNhom(int id_nhom, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            decimal dResult = 0;
            string sql = "select luong_duytri from qlns_bangluong_quyTTKD where thang = '" + thang + "' and nam = '" + nam + "' and id_nhom_donvi = '" + id_nhom + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                dResult = Convert.ToDecimal(tmp.Rows[0]["luong_duytri"].ToString());
            }
            return dResult;
        }

        public static decimal tongLuong_P3_TheoNhom(int id_nhom, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            decimal dResult = 0;
            string sql = "select luong_p3 from qlns_bangluong_quyTTKD where thang = '" + thang + "' and nam = '" + nam + "' and id_nhom_donvi = '" + id_nhom + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (tmp.Rows.Count > 0)
            {
                dResult = Convert.ToDecimal(tmp.Rows[0]["luong_p3"].ToString());
            }
            return dResult;
        }

        public static decimal tongtien_pttb(string nv_id_pttb, int thang, int nam)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            decimal dResult = 0;
            string timekey = "";
            timekey = nam.ToString() + thang.ToString("00");
            string sql = "select (tongtien_lt_220 + tongtien_gt_220 + tongtiendidong + tongtienmytv + tongtienezcom) as luong_pttb from tmp_nhanvien_pttb where nhanvien_id = '" + nv_id_pttb + "' and timekey = '" + timekey + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (tmp.Rows.Count > 0)
            {
                dResult = Convert.ToDecimal(tmp.Rows[0]["luong_pttb"].ToString());
            }
            return dResult;
        }

        public static void ChamCong(int thang, int nam, ngaycongNhanVien[] arrChamCong) {
            Connection cn = new Connection();
            int dem = 0;
            DateTime f = new DateTime(nam, thang, 01);
            int x = f.Month + 1;
            while (f.Month < x)
            {
                dem = dem + 1;
                if (f.DayOfWeek == DayOfWeek.Sunday)
                {
                    dem = dem - 1;
                }
                f = f.AddDays(1);
            }
            for (int nIndex = 0; nIndex < arrChamCong.Length; nIndex++) {
                decimal binhquan = Math.Round(Convert.ToDecimal(730000 / dem),2);
                decimal thuclinh = Math.Round(Convert.ToDecimal(binhquan * arrChamCong[nIndex].ngaycongTT), 1);
                string sql = "insert into qlns_chamcong(thang, nam, nhanvien, ngaycong_bhxh, ngaycong_thucte, giuaca_ngay, giuaca_thuclanh) values ('" + thang + "', '" + nam + "', '" + arrChamCong[nIndex].id_nv + "', '" + arrChamCong[nIndex].ngaycongBHXH + "', '" + arrChamCong[nIndex].ngaycongTT + "', '" + binhquan + "', '" + thuclinh + "')";
                try
                {
                    cn.ThucThiDL(sql);
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }

        [WebMethod]
        public static bool tinhLuong(int thang, int nam, decimal luongphanphoi, ngaycongNhanVien[] arrChamCong){
            bool bResult = false;
            bool bClearLuong = false;
            bClearLuong = clearLuong(thang, nam);
            if (!bClearLuong)
            {
                return false;
            }

            // Chấm công nhân viên
            ChamCong(thang, nam, arrChamCong);

            xepHang[] lstXepHang = new xepHang[17];
            BaoHiem baohiem = new BaoHiem();
            decimal fLuongDuyTri = 0;
            decimal fLuongP3 = 0;
            decimal fLuongDuyTri_VTCNTT = 0;
            decimal fLuongDuyTri_TKC = 0;
            decimal fLuongDuyTri_NS = 0;

            fLuongDuyTri = luongphanphoi*Convert.ToDecimal(0.3);
            fLuongDuyTri = Math.Round(fLuongDuyTri, 0);
            fLuongP3 = luongphanphoi - fLuongDuyTri;

            fLuongDuyTri_VTCNTT = fLuongDuyTri / 3;
            fLuongDuyTri_VTCNTT = Math.Round(fLuongDuyTri_VTCNTT, 0);
            fLuongDuyTri_TKC = fLuongDuyTri / 3;
            fLuongDuyTri_TKC = Math.Round(fLuongDuyTri_TKC, 0);
            fLuongDuyTri_NS = fLuongDuyTri - fLuongDuyTri_VTCNTT - fLuongDuyTri_TKC;

            // Insert or Update vào database phần tính lương phân phối
            exeLuongPhanPhoi(thang, nam, luongphanphoi, fLuongDuyTri, fLuongP3, fLuongDuyTri_VTCNTT, fLuongDuyTri_TKC, fLuongDuyTri_NS);

            // Get hệ số lương của các đơn vị
            lstXepHang = getListXH(thang, nam);

            // Get bảo hiểm
            baohiem = getBaoHiem();

            tinhLuongDuyTriVTCNTT(thang, nam, lstXepHang, fLuongDuyTri_VTCNTT);
            tinhLuongDuyTriTKC(thang, nam, lstXepHang, fLuongDuyTri_TKC);
            tinhLuongNS(thang, nam, lstXepHang, fLuongDuyTri_NS);
            tinhLuongP3(thang, nam, lstXepHang, fLuongP3);
            tinhQuyLuongTTKD(thang, nam);
            insertLuongChiTiet(thang, nam, baohiem);
            bResult = true;
            return bResult;
        }

        public static DataTable getDsNhanVien() {
            DataTable tmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select * from qlns_nhanvien where chinhthuc = 1 order by donvi ASC";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return tmp;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack) {
                this.Title = "Tính Lương Nhân Viên";
                dtNhanVien = getDsNhanVien();
            //}
        }
    }
}