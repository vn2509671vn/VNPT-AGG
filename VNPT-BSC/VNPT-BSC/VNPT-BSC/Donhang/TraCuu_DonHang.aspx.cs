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

namespace VNPT_BSC.Donhang
{
    public partial class TraCuu_DonHang : System.Web.UI.Page
    {
        public DataTable dtDonVi = new DataTable();

        private DataTable getDonVi()
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string sql = "select * from qlns_donvi where id not in (1,2,13,14,15,16,18) order by id asc";
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

        private static string getDonVi(int id_donvi){
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string szResult = "";
            string sql = "select * from qlns_donvi where id = '" + id_donvi + "'";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (tmp.Rows.Count > 0)
            {
                szResult = tmp.Rows[0]["ten_donvi"].ToString();
            }
            return szResult;
        }

        private static string getNhanvien(int id_nhanvien)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string szResult = "";
            string sql = "select * from nhanvien where nhanvien_id = '" + id_nhanvien + "'";
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
                szResult = tmp.Rows[0]["nhanvien_hoten"].ToString();
            }
            return szResult;
        }

        [WebMethod]
        public static string loadNhanVien(int donvi)
        {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            string szResult = "";
            string sql = "select * from nhanvien where nhanvien_donvi = '" + donvi + "' and nhanvien_chucdanh not in (1,2) order by nhanvien_hoten asc";
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            szResult += "<option value='0' selected='selected'>Tất cả</option>";
            if (tmp.Rows.Count > 0)
            {
                for (int n = 0; n < tmp.Rows.Count; n++)
                {
                    szResult += "<option value='" + tmp.Rows[n]["nhanvien_id"].ToString().Trim() + "'>" + tmp.Rows[n]["nhanvien_hoten"].ToString().Trim() + "</option>";
                }
            }
            return szResult;
        }

        [WebMethod]
        public static string loadTB(string ngay_bd, string ngay_kt, int donvi, int nhanvien, int trangthai)
        {
            Connection cn = new Connection();
            DataTable gridData = new DataTable();
            string tmpNgayBD = "";
            string tmpNgayKT = "";
            DateTime dt = DateTime.Parse(ngay_bd);
            tmpNgayBD = dt.ToString("yyyyMMdd");
            dt = DateTime.Parse(ngay_kt);
            tmpNgayKT = dt.ToString("yyyyMMdd");

            string sql = "select dh.*, format(dh.ngaytao,'dd/MM/yyyy') as _ngaytao, format(dh.ngay_phan_donhang,'dd/MM/yyyy') as _ngay_phan_donhang ";
            sql += "from dai_108_donhang dh ";
            sql += "where format(dh.ngaytao,'yyyyMMdd') >= '" + tmpNgayBD + "' ";
            sql += "and format(dh.ngaytao,'yyyyMMdd') <= '" + tmpNgayKT + "' ";
            if (donvi != 0) {
                sql += "and dh.pbh_tiepnhan = '" + donvi + "' ";
            }

            if (trangthai == 1) {
                // Chưa giao nhân viên
                sql += "and dh.nhanvien_tiepnhan is null ";
            }
            else if (trangthai == 2) { 
                // Đã giao nhưng nhân viên chưa xử lý
                sql += "and dh.nhanvien_tiepnhan is not null ";
                sql += "and dh.trangthai_donhang = 0 ";

                if (nhanvien != 0)
                {
                    sql += "and dh.nhanvien_tiepnhan = '" + nhanvien + "' ";
                }
            }
            else if (trangthai == 3)
            {
                // Đã giao và nhân viên đã xử lý
                sql += "and dh.nhanvien_tiepnhan is not null ";
                sql += "and dh.trangthai_donhang = 1 ";

                if (nhanvien != 0)
                {
                    sql += "and dh.nhanvien_tiepnhan = '" + nhanvien + "' ";
                }
            }

            sql += "order by dh.ngaytao desc ";

            try
            {
                gridData = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string arrOutput = "";
            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-tracuu' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            arrOutput += "<thead>";
            arrOutput += "<tr>";
            arrOutput += "<th class='text-center'>Mã HĐ</th>";
            arrOutput += "<th class='text-center'>Tên KH</th>";
            arrOutput += "<th class='text-center'>Số điện thoại</th>";
            arrOutput += "<th class='text-center'>Địa chỉ</th>";
            arrOutput += "<th class='text-center'>108 Ghi chú</th>";
            arrOutput += "<th class='text-center'>Ngày tạo</th>";
            arrOutput += "<th class='text-center'>PBH</th>";
            arrOutput += "<th class='text-center'>Nhân viên nhận</th>";
            arrOutput += "<th class='text-center'>Ngày nhận</th>";
            arrOutput += "<th class='text-center'>Trạng thái</th>";
            arrOutput += "<th class='text-center'>Ghi chú xử lý</th>";
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
                    
                    int nDonVi = Convert.ToInt32(gridData.Rows[i]["pbh_tiepnhan"].ToString());
                    string szTrangThai = "";
                    string szDonVi = "";
                    string szNhanvien = "";

                    if (gridData.Rows[i]["nhanvien_tiepnhan"].ToString() != "") {
                        int id_nhanvien = Convert.ToInt32(gridData.Rows[i]["nhanvien_tiepnhan"].ToString());
                        szNhanvien = getNhanvien(id_nhanvien);
                    }

                    if (gridData.Rows[i]["trangthai_donhang"].ToString() != "") {
                        int nStt = Convert.ToInt32(gridData.Rows[i]["trangthai_donhang"].ToString());
                        if (nStt == 0)
                        {
                            szTrangThai = "Chưa xử lý";
                        }
                        else
                        {
                            szTrangThai = "Đã xử lý";
                        }
                    }
                    
                    szDonVi = getDonVi(nDonVi);
                    

                    arrOutput += "<tr>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[i]["ma_donhang"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[i]["ten_khachhang"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + gridData.Rows[i]["sodienthoai"].ToString() + "</strong></td>";
                    arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[i]["diachi"].ToString() + "</strong></td>";
                    arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[i]["ghichu"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[i]["_ngaytao"].ToString() + "</strong></td>";
                    arrOutput += "<td><strong>" + szDonVi + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + szNhanvien + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + gridData.Rows[i]["_ngay_phan_donhang"].ToString() + "</strong></td>";
                    arrOutput += "<td style='text-align: center'><strong>" + szTrangThai + "</strong></td>";
                    arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[i]["ghichu_giaohang"].ToString() + "</strong></td>";
                    arrOutput += "</tr>";
                }
            }

            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Tra cứu đơn hàng";
            dtDonVi = getDonVi();
        }
    }
}