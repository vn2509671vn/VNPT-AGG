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
    public partial class TuDongCapNhatBacLuong : System.Web.UI.Page
    {
        public static void CapNhatBacNhanVien(int id_nhanvien, int id_chucdanh, int bacmoi) {
            Connection cn = new Connection();
            string sql = "update qlns_nhanvien set id_bacluong = (select id from qlns_bacluong where id_chucdanh = '" + id_chucdanh + "' and ten_bacluong = '" + bacmoi + "') where id = '" + id_nhanvien + "'";
            try
            {
                cn.ThucThiDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        public static DataTable dsDonVi() {
            Connection cn = new Connection();
            DataTable dtResult = new DataTable();
            string sql = "select * from qlns_donvi";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return dtResult;
        }

        public static double tbBSCDonVi(string thang, int nam, int id_donvi)
        {
            double dResult = 0;
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select sum(tongdiem_bsc)/3 as diem from tmp_tongbsc_donvi ";
            sql += "where thang in (" + thang + ") ";
            sql += "and nam = '" + nam + "'";
            sql += "and id_donvi = '" + id_donvi + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0) {
                if (tmp.Rows[0][0].ToString() != "") {
                    dResult = Convert.ToDouble(tmp.Rows[0][0].ToString());
                }
            }

            return dResult;
        }

        public static DataTable dsNhanVienTheoDonVi(int id_donvi, string thang, int nam) {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select nv.*, ";
            sql += "(select sum(tongdiem_bsc)/3 as diem from tmp_tongbsc_nhanvien where thang in (" + thang + ") and nam = '" + nam + "' and id_nhanvien = nv.id) as tb_bsc ";
            sql += "from qlns_nhanvien nv, qlns_nhom_donvi nhom ";
            sql += "where nv.id_nhom_donvi = nhom.id ";
            sql += "and nhom.id not in (17,18,19) ";
            sql += "and nv.donvi = '" + id_donvi + "' ";
            sql += "and nv.chinhthuc = 1 ";
            sql += "order by cast(nv.ngaykyhd as date), tb_bsc DESC";

            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            return dtResult;
        }

        public static DataTable dsNhanVienTheoNhom_17_18_19(string thang, int nam)
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select nv.*, ";
            sql += "(select sum(tongdiem_bsc)/3 as diem from tmp_tongbsc_nhanvien where thang in (" + thang + ") and nam = '" + nam + "' and id_nhanvien = nv.id) as tb_bsc ";
            sql += "from qlns_nhanvien nv, qlns_nhom_donvi nhom ";
            sql += "where nv.id_nhom_donvi = nhom.id ";
            sql += "and nhom.id in (17,18,19) ";
            sql += "and nv.chinhthuc = 1 ";
            sql += "order by tb_bsc DESC";

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

        [WebMethod]
        public static string loadHeSoLuongMoi(int nam, int quy)
        {
            Connection cnBSC = new Connection();
            DataTable dtDonvi = new DataTable();
            DataTable dtNhanVienThuocNhomQuanLy = new DataTable();
            DataTable gridData = new DataTable();

            string szThang = "";
            if (quy == 0)
            {
                szThang = "1,2,3,4,5,6,7,8,9,10,11,12";
            }
            else if (quy == 1)
            {
                szThang = "1,2,3";
            }
            else if (quy == 2)
            {
                szThang = "4,5,6";
            }
            else if (quy == 3)
            {
                szThang = "7,8,9";
            }
            else if (quy == 4)
            {
                szThang = "10,11,12";
            }

            dtDonvi = dsDonVi();
            dtNhanVienThuocNhomQuanLy = dsNhanVienTheoNhom_17_18_19(szThang, nam);

            // CẬp nhật bậc lương cho các nhân viên không thuộc nhóm trưởng phòng, giám đốc (not in 17, 18, 19)
            for (int nIndex = 0; nIndex < dtDonvi.Rows.Count; nIndex++) { 
                int id_donvi = Convert.ToInt32(dtDonvi.Rows[nIndex]["id"].ToString());
                double tbDiemBSCDonVi = tbBSCDonVi(szThang, nam, id_donvi);
                DataTable dtNhanVienTheoDonVi = new DataTable();
                DateTime ngayvaonganh = new DateTime();
                DateTime ngayxet = new DateTime();
                ngayxet = DateTime.Now;

                int tongso_nhanvien = 0;
                dtNhanVienTheoDonVi = dsNhanVienTheoDonVi(id_donvi, szThang, nam);
                tongso_nhanvien = dtNhanVienTheoDonVi.Rows.Count;
                

                if (tbDiemBSCDonVi < 0.98) { 
                    // 10% bậc 4, 60% bậc 3, 30% bậc 2
                    for (int nIndexNV = 0; nIndexNV < dtNhanVienTheoDonVi.Rows.Count; nIndexNV++) { 
                        int tmpSl = nIndexNV + 1;
                        int id_nhanvien = Convert.ToInt32(dtNhanVienTheoDonVi.Rows[nIndexNV]["id"].ToString());
                        int id_chucdanh = Convert.ToInt32(dtNhanVienTheoDonVi.Rows[nIndexNV]["chucdanh"].ToString());
                        int bac = 1;
                        int sothang_vaonganh = 0;
                        ngayvaonganh = Convert.ToDateTime(dtNhanVienTheoDonVi.Rows[nIndexNV]["ngaykyhd"].ToString());
                        sothang_vaonganh = GetMonthDifference(ngayvaonganh, ngayxet);
                        if (sothang_vaonganh >= 6)
                        {
                            if (tmpSl <= Convert.ToInt32(tongso_nhanvien * 0.1))
                            {
                                bac = 4;
                            }
                            else if (tmpSl <= (Convert.ToInt32(tongso_nhanvien * 0.6) + Convert.ToInt32(tongso_nhanvien * 0.1)))
                            {
                                bac = 3;
                            }
                            else if (tmpSl <= tongso_nhanvien)
                            {
                                bac = 2;
                            }
                        }
                        else {
                            bac = 1;
                        }
                        // Cập nhật bậc nhân viên
                        CapNhatBacNhanVien(id_nhanvien, id_chucdanh, bac);
                    }
                }
                else if (tbDiemBSCDonVi < 1) { 
                    // 20% bậc 4, 60% bậc 3, 20% bậc 2
                    for (int nIndexNV = 0; nIndexNV < dtNhanVienTheoDonVi.Rows.Count; nIndexNV++)
                    {
                        int tmpSl = nIndexNV + 1;
                        int id_nhanvien = Convert.ToInt32(dtNhanVienTheoDonVi.Rows[nIndexNV]["id"].ToString());
                        int id_chucdanh = Convert.ToInt32(dtNhanVienTheoDonVi.Rows[nIndexNV]["chucdanh"].ToString());
                        int bac = 1;
                        int sothang_vaonganh = 0;
                        ngayvaonganh = Convert.ToDateTime(dtNhanVienTheoDonVi.Rows[nIndexNV]["ngaykyhd"].ToString());
                        sothang_vaonganh = GetMonthDifference(ngayvaonganh, ngayxet);
                        if (sothang_vaonganh >= 6)
                        {
                            if (tmpSl <= Convert.ToInt32(tongso_nhanvien * 0.2))
                            {
                                bac = 4;
                            }
                            else if (tmpSl <= (Convert.ToInt32(tongso_nhanvien * 0.6) + Convert.ToInt32(tongso_nhanvien * 0.2)))
                            {
                                bac = 3;
                            }
                            else if (tmpSl <= tongso_nhanvien)
                            {
                                bac = 2;
                            }
                        }
                        else
                        {
                            bac = 1;
                        }
                        // Cập nhật bậc nhân viên
                        CapNhatBacNhanVien(id_nhanvien, id_chucdanh, bac);
                    }
                }
                else if (tbDiemBSCDonVi >= 1) { 
                    // 30% bậc 4, 70% bậc 3
                    for (int nIndexNV = 0; nIndexNV < dtNhanVienTheoDonVi.Rows.Count; nIndexNV++)
                    {
                        int tmpSl = nIndexNV + 1;
                        int id_nhanvien = Convert.ToInt32(dtNhanVienTheoDonVi.Rows[nIndexNV]["id"].ToString());
                        int id_chucdanh = Convert.ToInt32(dtNhanVienTheoDonVi.Rows[nIndexNV]["chucdanh"].ToString());
                        int bac = 1;
                        int sothang_vaonganh = 0;
                        ngayvaonganh = Convert.ToDateTime(dtNhanVienTheoDonVi.Rows[nIndexNV]["ngaykyhd"].ToString());
                        sothang_vaonganh = GetMonthDifference(ngayvaonganh, ngayxet);
                        if (sothang_vaonganh >= 6)
                        {
                            if (tmpSl <= Convert.ToInt32(tongso_nhanvien * 0.3))
                            {
                                bac = 4;
                            }
                            else if (tmpSl <= tongso_nhanvien)
                            {
                                bac = 3;
                            }
                        }
                        else
                        {
                            bac = 1;
                        }
                        // Cập nhật bậc nhân viên
                        CapNhatBacNhanVien(id_nhanvien, id_chucdanh, bac);
                    }
                }
            }

            for (int nIndexNV = 0; nIndexNV < dtNhanVienThuocNhomQuanLy.Rows.Count; nIndexNV++) {
                int id_nv = Convert.ToInt32(dtNhanVienThuocNhomQuanLy.Rows[nIndexNV]["id"].ToString());
                int id_chucdanh = Convert.ToInt32(dtNhanVienThuocNhomQuanLy.Rows[nIndexNV]["chucdanh"].ToString());
                double diembsc = Convert.ToDouble(dtNhanVienThuocNhomQuanLy.Rows[nIndexNV]["tb_bsc"].ToString());
                int bac = 0;
                if (diembsc < 0.98) {
                    bac = 2;
                }
                else if (diembsc < 1) {
                    bac = 3;
                }
                else if (diembsc >= 1) {
                    bac = 4;
                }

                if (id_chucdanh == 51) {
                    bac = 4;
                }
                else if (id_chucdanh == 50) {
                    bac = 5;
                }
                CapNhatBacNhanVien(id_nv, id_chucdanh, bac);
            }

            string sqlbsc = "select nhom.ten_nhom, nv.ma_nhanvien, nv.ten_nhanvien, cd.ten_chucdanh, (N'Bậc ' + CAST(bl.ten_bacluong as char)) as ten_bl, bl.heso_bacluong ";
            sqlbsc += "from qlns_nhanvien nv, qlns_nhom_donvi nhom, qlns_chucdanh cd, qlns_bacluong bl ";
            sqlbsc += "where nv.chucdanh = cd.id ";
            sqlbsc += "and nv.id_nhom_donvi = nhom.id ";
            sqlbsc += "and nv.id_bacluong = bl.id ";
            sqlbsc += "and nv.chinhthuc = 1 ";
            sqlbsc += "order by nhom.id ASC";

            try
            {
                gridData = cnBSC.XemDL(sqlbsc);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string arrOutput = "";
            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-bacluong' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            arrOutput += "<thead>";
            arrOutput += "<tr>";
            arrOutput += "<th class='text-center'>Nhóm</th>";
            arrOutput += "<th class='text-center'>MNV</th>";
            arrOutput += "<th class='text-center'>Nhân viên</th>";
            arrOutput += "<th class='text-center'>Chức danh</th>";
            arrOutput += "<th class='text-center'>Bậc lương</th>";
            arrOutput += "<th class='text-center'>Hệ số chức danh</th>";
            arrOutput += "</tr>";
            arrOutput += "</thead>";
            arrOutput += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                arrOutput += "<tr><td colspan='6' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    arrOutput += "<tr>";
                    arrOutput += "<td>" + gridData.Rows[nKPI]["ten_nhom"].ToString() + "</td>";
                    arrOutput += "<td>" + gridData.Rows[nKPI]["ma_nhanvien"].ToString() + "</td>";
                    arrOutput += "<td><strong>" + gridData.Rows[nKPI]["ten_nhanvien"].ToString() + "</strong></td>";
                    arrOutput += "<td>" + gridData.Rows[nKPI]["ten_chucdanh"].ToString() + "</td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["ten_bl"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[nKPI]["heso_bacluong"].ToString() + "</strong></td>";
                    arrOutput += "</tr>";
                }
            }
            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Tự động cập nhật bậc lương theo quý";
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