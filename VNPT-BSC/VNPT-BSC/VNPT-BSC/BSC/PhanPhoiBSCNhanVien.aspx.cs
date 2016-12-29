﻿using System;
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
    public partial class PhanPhoiBSCNhanVien : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public static int nhanvienquanly;
        public static int donvi;
        public static DataTable dtNhanVien = new DataTable();
        public static DataTable dtFullNV = new DataTable();
        public static DataTable dtBSC = new DataTable();
        public static DataTable dtDVT = new DataTable();
        public class kpiDetail
        {
            public int kpi_id { get; set; }
            public int tytrong { get; set; }
            public int dvt { get; set; }
            public decimal kehoach { get; set; }
            public int nhanvienthamdinh { get; set; }
        }

        /*List đơn vị tính*/
        private DataTable dsDVT()
        {
            DataTable dsDonvitinh = new DataTable();
            string sqlDVT = "select * from donvitinh";
            try
            {
                dsDonvitinh = cn.XemDL(sqlDVT);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDonvitinh;
        }

        [WebMethod]
        public static string loadBSC(int thang, int nam, int nguoitao, int donvi)
        {
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            DataTable dsDonvitinh = new DataTable();
            DataTable dsNhanvienthamdinh = new DataTable();
            string sqlDVT = "select * from donvitinh";
            string sqlNVTD = "select nhanvien.* from nhanvien, nhanvien_chucvu where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id and nhanvien_chucvu.chucvu_id in (3,5) and nhanvien.nhanvien_donvi = '" + donvi + "'";
            string sqlBSC = "select bsc.thang, bsc.nam, bsc.kpi_id, bsc.tytrong, bsc.donvitinh, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, bsc.nhanvienthamdinh ";
            sqlBSC += "from danhsachbsc bsc, kpi, kpo ";
            sqlBSC += "where bsc.kpi_id = kpi.kpi_id ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.bscduocgiao != '' ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' and bsc.nguoitao = '"+nguoitao+"'";
            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
                dsDonvitinh = cnBSC.XemDL(sqlDVT);
                dsNhanvienthamdinh = cnBSC.XemDL(sqlNVTD);
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
            arrOutput += "<th>STT</th>";
            arrOutput += "<th>Chỉ tiêu</th>";
            arrOutput += "<th>Tỷ trọng (%)</th>";
            arrOutput += "<th>ĐVT</th>";
            arrOutput += "<th>Kế hoạch</th>";
            arrOutput += "<th>Nhân viên thẩm định</th>";
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
                    arrOutput += "<tr data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    arrOutput += "<td>" + (nKPI + 1) + "</td>";
                    arrOutput += "<td>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + " (" + gridData.Rows[nKPI]["kpo_ten"].ToString() + ")" + "</td>";
                    arrOutput += "<td class='text-center'><input type='text' class='form-control' name='tytrong' id='tytrong_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' maxlength='2' value='" + gridData.Rows[nKPI]["tytrong"].ToString() + "'/></td>";
                    //arrOutput += "<td class='text-center'><input type='text' class='form-control' name='dvt' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='5' value='" + gridData.Rows[nKPI]["donvitinh"].ToString() + "'/></td>";
                    arrOutput += "<td class='text-center'>";
                    arrOutput += "<select class='form-control' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    for (int nDVT = 0; nDVT < dsDonvitinh.Rows.Count; nDVT++)
                    {
                        if (dsDonvitinh.Rows[nDVT]["dvt_id"].ToString() == gridData.Rows[nKPI]["donvitinh"].ToString())
                        {
                            arrOutput += "<option value='" + dsDonvitinh.Rows[nDVT]["dvt_id"] + "' selected>" + dsDonvitinh.Rows[nDVT]["dvt_ten"] + "</option>";
                        }
                        else
                        {
                            arrOutput += "<option value='" + dsDonvitinh.Rows[nDVT]["dvt_id"] + "'>" + dsDonvitinh.Rows[nDVT]["dvt_ten"] + "</option>";
                        }
                    }
                    arrOutput += "</select>";
                    arrOutput += "</td>";

                    arrOutput += "<td class='text-center'><input type='text' class='form-control' name='kehoach' id='kehoach_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' onkeypress='return onlyNumbers(event.charCode || event.keyCode);'/></td>";

                    // Nhân viên thẩm định
                    arrOutput += "<td class='text-center'>";
                    arrOutput += "<select class='form-control' id='nvtd_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    for (int nNVTD = 0; nNVTD < dsNhanvienthamdinh.Rows.Count; nNVTD++)
                    {
                        //arrOutput += "<option value='" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_id"] + "'>" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_hoten"] + "</option>";
                        if (dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_id"].ToString() == gridData.Rows[nKPI]["nhanvienthamdinh"].ToString())
                        {
                            arrOutput += "<option value='" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_id"] + "' selected>" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_hoten"] + "</option>";
                        }
                        else
                        {
                            arrOutput += "<option value='" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_id"] + "'>" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_hoten"] + "</option>";
                        }
                    }
                    arrOutput += "</select>";
                    arrOutput += "</td>";
                    arrOutput += "</tr>";
                }
            }
            arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int id_nv_giao, string nv_nhan, int thang, int nam, int donvi)
        {
            //string[] arrOutput = {};
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            DataTable dsDonvitinh = new DataTable();
            DataTable dsNhanvienthamdinh = new DataTable();
            string sqlDVT = "select * from donvitinh";
            string sqlNVTD = "select nhanvien.* from nhanvien, nhanvien_chucvu where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id and nhanvien_chucvu.chucvu_id in (3,5) and nhanvien.nhanvien_donvi = '" + donvi + "'";

            // Lấy thông tin của nhân viên nhận thông qua tài khoản
            int nhanviennhan = 0;
            string ten_nhanviennhan = "";
            nhanviennhan = getNhanVienID(nv_nhan);
            ten_nhanviennhan = getNhanVienName(nv_nhan);

            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, bsc.donvitinh, bsc.trongso, bsc.kehoach ";
            sqlBSC += "from bsc_nhanvien bsc, kpi, kpo, nhanvien nvgiao, nhanvien nvnhan ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and bsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and bsc.nhanviennhan = '" + nhanviennhan + "' ";
            sqlBSC += "and bsc.nhanviengiao = '" + id_nv_giao + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "'";
            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
                dsDonvitinh = cnBSC.XemDL(sqlDVT);
                dsNhanvienthamdinh = cnBSC.XemDL(sqlNVTD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th>STT</th>";
            outputHTML += "<th>Chỉ tiêu</th>";
            outputHTML += "<th>Tỷ trọng (%)</th>";
            outputHTML += "<th>ĐVT</th>";
            outputHTML += "<th>Kế hoạch</th>";
            outputHTML += "<th>Nhân viên thẩm định</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='6' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    outputHTML += "<tr data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<td>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + " (" + gridData.Rows[nKPI]["kpo_ten"].ToString() + ")" + "</td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' readonly name='tytrong' id='tytrong_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' maxlength='2' value='" + gridData.Rows[nKPI]["trongso"].ToString() + "'/></td>";
                    //outputHTML += "<td class='text-center'><input type='text' class='form-control' readonly name='dvt' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='5' value='" + gridData.Rows[nKPI]["donvitinh"].ToString() + "'/></td>";
                    outputHTML += "<td class='text-center'>";
                    outputHTML += "<select class='form-control' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    for (int nDVT = 0; nDVT < dsDonvitinh.Rows.Count; nDVT++)
                    {
                        if (dsDonvitinh.Rows[nDVT]["dvt_id"].ToString() == gridData.Rows[nKPI]["donvitinh"].ToString())
                        {
                            outputHTML += "<option value='" + dsDonvitinh.Rows[nDVT]["dvt_id"] + "' selected>" + dsDonvitinh.Rows[nDVT]["dvt_ten"] + "</option>";
                        }
                        else
                        {
                            outputHTML += "<option value='" + dsDonvitinh.Rows[nDVT]["dvt_id"] + "'>" + dsDonvitinh.Rows[nDVT]["dvt_ten"] + "</option>";
                        }
                    }
                    outputHTML += "</select>";
                    outputHTML += "</td>";

                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='kehoach' id='kehoach_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' value='" + gridData.Rows[nKPI]["kehoach"].ToString() + "' onkeypress='return onlyNumbers(event.charCode || event.keyCode);'/></td>";

                    // Nhân viên thẩm định
                    outputHTML += "<td class='text-center'>";
                    outputHTML += "<select class='form-control' id='nvtd_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    for (int nNVTD = 0; nNVTD < dsNhanvienthamdinh.Rows.Count; nNVTD++)
                    {
                        outputHTML += "<option value='" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_id"] + "'>" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_hoten"] + "</option>";
                    }
                    outputHTML += "</select>";
                    outputHTML += "</td>";
                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);
            dicOutput.Add("ten_nhanviennhan", ten_nhanviennhan);
            
            /*Lấy danh sách các thông tin còn lại ở bảng giaobscnhanvien*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select bscnhanvien.* from giaobscnhanvien bscnhanvien ";
            sqlGiaoBSCDV += "where bscnhanvien.nhanviengiao = '" + id_nv_giao + "' ";
            sqlGiaoBSCDV += "and bscnhanvien.nhanviennhan = '" + nhanviennhan + "' ";
            sqlGiaoBSCDV += "and bscnhanvien.thang = '" + thang + "' ";
            sqlGiaoBSCDV += "and bscnhanvien.nam = '" + nam + "' ";
            try
            {
                dtGiaoBSCDV = cnBSC.XemDL(sqlGiaoBSCDV);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtGiaoBSCDV.Rows.Count > 0)
            {
                dicOutput.Add("nhanviengiao", dtGiaoBSCDV.Rows[0]["nhanviengiao"].ToString());
                dicOutput.Add("nhanviennhan", dtGiaoBSCDV.Rows[0]["nhanviennhan"].ToString());
                dicOutput.Add("trangthaigiao", dtGiaoBSCDV.Rows[0]["trangthaigiao"].ToString());
                dicOutput.Add("trangthainhan", dtGiaoBSCDV.Rows[0]["trangthainhan"].ToString());
                dicOutput.Add("trangthaidongy_kqtd", dtGiaoBSCDV.Rows[0]["trangthaidongy_kqtd"].ToString());
                dicOutput.Add("trangthaiketthuc", dtGiaoBSCDV.Rows[0]["trangthaiketthuc"].ToString());
            }
            else
            {
                dicOutput.Add("nhanviengiao", id_nv_giao.ToString());
                dicOutput.Add("nhanviennhan", nv_nhan.ToString());
                dicOutput.Add("trangthaigiao", "0");
                dicOutput.Add("trangthainhan", "0");
                dicOutput.Add("trangthaidongy_kqtd", "0");
                dicOutput.Add("trangthaiketthuc", "0");
            }

            return dicOutput;
        }

        [WebMethod]
        public static bool saveData(int nhanviengiao, string nhanviennhan, int thang, int nam, kpiDetail[] kpi_detail)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            bool isExist = false;
            int id_nhanviennhan = 0;

            id_nhanviennhan = getNhanVienID(nhanviennhan);

            isExist = isExistGiaoBSC_NV(nhanviengiao, id_nhanviennhan, thang, nam);
            if (isExist)
            {
                string sqlDeleteBSCDV = "delete bsc_nhanvien where nhanviengiao = '" + nhanviengiao + "' and nhanviennhan = '" + id_nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
                string sqlDeleteGiaoBSCDV = "delete giaobscnhanvien where nhanviengiao = '" + nhanviengiao + "' and nhanviennhan = '" + id_nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
                try
                {
                    cnData.ThucThiDL(sqlDeleteBSCDV);
                    cnData.ThucThiDL(sqlDeleteGiaoBSCDV);

                    string sqlInsertGiaoBSC = "insert into giaobscnhanvien(nhanviengiao, nhanviennhan, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaidongy_kqtd, trangthaiketthuc) ";
                    sqlInsertGiaoBSC += "values('" + nhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0, 0)";
                    cnData.ThucThiDL(sqlInsertGiaoBSC);
                    for (int i = 0; i < kpi_detail.Length; i++)
                    {
                        string sqlInsertBSCDV = "insert into bsc_nhanvien(nhanviengiao, nhanviennhan, thang, nam, kpi, nhanvienthamdinh, donvitinh, trongso, kehoach, trangthaithamdinh) ";
                        sqlInsertBSCDV += "values('" + nhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', '" + Convert.ToInt32(kpi_detail[i].kpi_id) + "', '" + kpi_detail[i].nhanvienthamdinh + "','" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + Convert.ToDecimal(kpi_detail[i].kehoach) + "', 0)";
                        try
                        {
                            cnData.ThucThiDL(sqlInsertBSCDV);
                            isSuccess = true;
                        }
                        catch
                        {
                            isSuccess = false;
                            break;
                        }
                    }
                }
                catch
                {
                    isSuccess = false;
                }
            }
            else
            {
                string sqlInsertGiaoBSC = "insert into giaobscnhanvien(nhanviengiao, nhanviennhan, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaidongy_kqtd, trangthaiketthuc) ";
                sqlInsertGiaoBSC += "values('" + nhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0, 0)";
                
                // Người dùng nhập nhân viên nhận và giao không chính xác sẽ trả về false
                try
                {
                    cnData.ThucThiDL(sqlInsertGiaoBSC);
                }
                catch {
                    return false;
                }

                for (int i = 0; i < kpi_detail.Length; i++)
                {
                    string sqlInsertBSCDV = "insert into bsc_nhanvien(nhanviengiao, nhanviennhan, thang, nam, kpi, nhanvienthamdinh, donvitinh, trongso, kehoach, trangthaithamdinh) ";
                    sqlInsertBSCDV += "values('" + nhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', '" + Convert.ToInt32(kpi_detail[i].kpi_id) + "', '"+kpi_detail[i].nhanvienthamdinh+"', '" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + Convert.ToDecimal(kpi_detail[i].kehoach) + "', 0)";
                    try
                    {
                        cnData.ThucThiDL(sqlInsertBSCDV);
                        isSuccess = true;
                    }
                    catch
                    {
                        isSuccess = false;
                        break;
                    }
                }
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool giaoBSC(int nhanviengiao, string nhanviennhan, int thang, int nam)
        {
            Connection cnGiaoBSC = new Connection();
            bool isSuccess = false;
            int id_nhanviennhan = 0;
            id_nhanviennhan = getNhanVienID(nhanviennhan);

            bool isExist = isExistGiaoBSC_NV(nhanviengiao, id_nhanviennhan, thang, nam);
            if (!isExist)
            {
                return false;
            }

            string sqlGiaoBSC = "update giaobscnhanvien set trangthaigiao = 1 where nhanviengiao = '" + nhanviengiao + "' and nhanviennhan = '" + id_nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cnGiaoBSC.ThucThiDL(sqlGiaoBSC);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool huygiaoBSC(int nhanviengiao, string nhanviennhan, int thang, int nam)
        {
            Connection cnGiaoBSC = new Connection();
            bool isSuccess = false;
            int id_nhanviennhan = 0;
            id_nhanviennhan = getNhanVienID(nhanviennhan);
            string sqlGiaoBSC = "update giaobscnhanvien set trangthaigiao = 0 where nhanviengiao = '" + nhanviengiao + "' and nhanviennhan = '" + id_nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cnGiaoBSC.ThucThiDL(sqlGiaoBSC);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        private static bool isExistGiaoBSC_NV(int id_nv_giao, int id_nv_nhan, int thang, int nam)
        {
            Connection cnData = new Connection();
            bool result = false;
            string sql = "select * ";
            sql += "from giaobscnhanvien ";
            sql += "where nhanviengiao = '" + id_nv_giao + "' ";
            sql += "and nhanviennhan = '" + id_nv_nhan + "' ";
            sql += "and thang = '" + thang + "' ";
            sql += "and nam = '" + nam + "' ";
            DataTable dtQuery = new DataTable();
            try
            {
                dtQuery = cnData.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (dtQuery.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        private static int getNhanVienID(string taikhoan) {
            int id_nhanviennhan = 0;
            for (int nIndex = 0; nIndex < dtFullNV.Rows.Count; nIndex++)
            {
                string taikhoanTmp = dtFullNV.Rows[nIndex]["nhanvien_taikhoan"].ToString().Trim();
                if (taikhoanTmp == taikhoan.Trim())
                {
                    id_nhanviennhan = Convert.ToInt32(dtFullNV.Rows[nIndex]["nhanvien_id"].ToString().Trim());
                    break;
                }
            }
            return id_nhanviennhan;
        }

        private static string getNhanVienName(string taikhoan)
        {
            string ten_nhanviennhan = "";
            for (int nIndex = 0; nIndex < dtFullNV.Rows.Count; nIndex++)
            {
                string taikhoanTmp = dtFullNV.Rows[nIndex]["nhanvien_taikhoan"].ToString().Trim();
                if (taikhoanTmp == taikhoan.Trim())
                {
                    ten_nhanviennhan = dtFullNV.Rows[nIndex]["nhanvien_hoten"].ToString().Trim();
                    break;
                }
            }
            return ten_nhanviennhan;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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

                    if (nhanvien == null || !nFindResult)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    nhanvienquanly = nhanvien.nhanvien_id;
                    donvi = nhanvien.nhanvien_donvi_id;

                    string sqlDanhSachNhanVien = "select * from nhanvien where nhanvien_donvi = '" + donvi + "'";
                    string sqlDanhSachFullNV = sqlDanhSachNhanVien;
                    string sqlDanhSachKPI = "select thang, nam, CONVERT(varchar(4), thang) + '/' + CONVERT(varchar(4), nam) AS content from DANHSACHBSC where nguoitao = '" + nhanvienquanly + "' and bscduocgiao != '' group by nam, thang order by nam, thang ASC";
                    dtNhanVien = cn.XemDL(sqlDanhSachNhanVien);
                    dtBSC = cn.XemDL(sqlDanhSachKPI);
                    dtFullNV = cn.XemDL(sqlDanhSachFullNV);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}