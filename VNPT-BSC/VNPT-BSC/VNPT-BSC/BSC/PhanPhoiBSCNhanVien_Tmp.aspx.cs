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
    public partial class PhanPhoiBSCNhanVien_Tmp : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public static int nhanvienquanly;
        public static int donvi;
        public static DataTable dtNhanVien = new DataTable();
        public static DataTable dtKPI = new DataTable();
        public static DataTable dtDVT = new DataTable();
        public static DataTable dtNhomKPI = new DataTable();

        public class kpiDetail
        {
            public string ten_kpi { get; set; }
            public int tytrong { get; set; }
            public int dvt { get; set; }
            public float kehoach { get; set; }
            public int nhanvienthamdinh { get; set; }
            public int nhom_kpi_id { get; set; }
            public string ghichu { get; set; }
            public int stt { get; set; }
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

        /*List nhóm kpi*/
        private DataTable dsNhomKPI()
        {
            DataTable dsNhomKPI = new DataTable();
            string sqlNhomKPI = "select * from nhom_kpi where loaimaubsc_id = 16 and hienthi = 1 order by thutuhienthi asc";
            try
            {
                dsNhomKPI = cn.XemDL(sqlNhomKPI);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsNhomKPI;
        }

        /*List kpi*/
        private DataTable dsKPI(int nguoitao)
        {
            DataTable dsKPI = new DataTable();
            string sqlKPI = "select * from kpi where kpi_nguoitao = '" + nguoitao + "' and hienthi = 1";
            try
            {
                dsKPI = cn.XemDL(sqlKPI);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsKPI;
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
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpi.kpi_ma, kpo.kpo_id, kpo.kpo_ten, bsc.donvitinh, bsc.trongso, bsc.kehoach, bsc.nhom_kpi, nhom_kpi.ten_nhom, nhom_kpi.tytrong as tytrong_nhom, bsc.ghichu ";
            sqlBSC += "from bsc_nhanvien bsc, kpi, kpo, nhanvien nvgiao, nhanvien nvnhan, nhom_kpi ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and bsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and bsc.nhanviennhan = '" + nhanviennhan + "' ";
            sqlBSC += "and bsc.nhanviengiao = '" + id_nv_giao + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.nhom_kpi = nhom_kpi.id ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' ORDER BY nhom_kpi.thutuhienthi, bsc.stt ASC";
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

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th><button type='button' class='btn btn-primary btn-xs' id='btnAddRow'>+</button></th>";
            outputHTML += "<th class='text-center'>KPI</th>";
            outputHTML += "<th class='text-center'>Nhóm KPI</th>";
            outputHTML += "<th class='text-center'>Tỷ trọng (<span id='tongTyTrong' class='red-900'></span>%)</th>";
            outputHTML += "<th class='text-center'>ĐVT</th>";
            outputHTML += "<th class='text-center min-width-72'>Chỉ tiêu</th>";
            outputHTML += "<th class='text-center'>T/gian giao</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                //outputHTML += "<tr><td colspan='7' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    string nhom_kpi_id = gridData.Rows[nKPI]["nhom_kpi"].ToString();
                    string nhom_kpi_ten = gridData.Rows[nKPI]["ten_nhom"].ToString();
                    string nhom_kpi_tytrong = gridData.Rows[nKPI]["tytrong_nhom"].ToString();
                    int stt = nKPI + 1;
                    outputHTML += "<tr data-stt='" + stt + "'>";
                    outputHTML += "<td class='text-center'><button type='button' class='btn btn-danger btn-xs btnRemove'>-</button></td>";
                    //outputHTML += "<td><input type='text' class='form-control cls-kpi min-width-300' size='50' id='kpi_" + stt + "' value='" + gridData.Rows[nKPI]["kpi_ten"].ToString().Trim() + "' title='" + gridData.Rows[nKPI]["kpi_ten"].ToString().Trim() + "'/>";
                    outputHTML += "<td><textarea type='text' class='form-control cls-kpi min-width-300' size='50' id='kpi_" + stt + "' title='" + gridData.Rows[nKPI]["kpi_ten"].ToString().Trim() + "' >" + gridData.Rows[nKPI]["kpi_ten"].ToString().Trim() + "</textarea>";
                    
                    outputHTML += "<td class='text-center'>";
                    outputHTML += "<select class='form-control' id='nhom_kpi_" + stt + "'>";
                            for (int nIndex = 0; nIndex < dtNhomKPI.Rows.Count; nIndex++)
                               {
                                   string szSelected = "";
                                   int id_nhom = Convert.ToInt32(dtNhomKPI.Rows[nIndex]["id"].ToString());
                                   int tytrong_nhom = Convert.ToInt32(dtNhomKPI.Rows[nIndex]["tytrong"].ToString());
                                   if (id_nhom == Convert.ToInt32(nhom_kpi_id))
                                   {
                                       szSelected = "selected";
                                   }
                                   outputHTML += "<option data-tytrong-nhom='" + tytrong_nhom + "' value='" + id_nhom + "' " + szSelected + ">" + dtNhomKPI.Rows[nIndex]["ten_nhom"] + "</option>";
                            } 
                    outputHTML += "</select>";
                    outputHTML += "</td>";

                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='tytrong' id='tytrong_" + stt + "' size='2' maxlength='2' value='" + gridData.Rows[nKPI]["trongso"].ToString() + "'/></td>";
                    //outputHTML += "<td class='text-center'><input type='text' class='form-control' readonly name='dvt' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='5' value='" + gridData.Rows[nKPI]["donvitinh"].ToString() + "'/></td>";
                    outputHTML += "<td class='text-center'>";
                    outputHTML += "<select class='form-control' id='dvt_" + stt + "'>";
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

                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='kehoach' id='kehoach_" + stt + "' size='2' value='" + gridData.Rows[nKPI]["kehoach"].ToString() + "' onkeypress='return onlyNumbers(event.charCode || event.keyCode);'/></td>";
                    //outputHTML += "<td class='text-center'><input type='text' class='form-control min-width-300' name='ghichu' id='ghichu_" + stt + "' value='" + gridData.Rows[nKPI]["ghichu"].ToString() + "'/></td>";
                    outputHTML += "<td class='text-center'><textarea type='text' class='form-control min-width-300' name='ghichu' id='ghichu_" + stt + "'>" + gridData.Rows[nKPI]["ghichu"].ToString() + "</textarea></td>";
                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);
            dicOutput.Add("ten_nhanviennhan", ten_nhanviennhan);

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

                    string sqlInsertGiaoBSC = "insert into giaobscnhanvien(nhanviengiao, nhanviennhan, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaidongy_kqtd, trangthaiketthuc, loaimau) ";
                    sqlInsertGiaoBSC += "values('" + nhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0, 0, 16)";
                    cnData.ThucThiDL(sqlInsertGiaoBSC);
                    for (int i = 0; i < kpi_detail.Length; i++)
                    {
                        int kpi_id = getKPI_ID(kpi_detail[i].ten_kpi.Trim(), nhanviengiao, kpi_detail[i].nhom_kpi_id);
                        string sqlInsertBSCDV = "insert into bsc_nhanvien(nhanviengiao, nhanviennhan, thang, nam, kpi, nhanvienthamdinh, donvitinh, trongso, kehoach, thuchien, thamdinh, trangthaithamdinh, nhom_kpi, loaimau, ghichu, stt, donvi) ";
                        sqlInsertBSCDV += "values('" + nhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', '" + kpi_id + "', '" + kpi_detail[i].nhanvienthamdinh + "','" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + kpi_detail[i].kehoach + "', 0, 0, 0, '" + kpi_detail[i].nhom_kpi_id + "', 16, N'" + kpi_detail[i].ghichu + "', '" + kpi_detail[i].stt + "', '" + donvi + "')";
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
                string sqlInsertGiaoBSC = "insert into giaobscnhanvien(nhanviengiao, nhanviennhan, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaidongy_kqtd, trangthaiketthuc, loaimau) ";
                sqlInsertGiaoBSC += "values('" + nhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0, 0, 16)";

                // Người dùng nhập nhân viên nhận và giao không chính xác sẽ trả về false
                try
                {
                    cnData.ThucThiDL(sqlInsertGiaoBSC);
                }
                catch
                {
                    return false;
                }

                for (int i = 0; i < kpi_detail.Length; i++)
                {
                    int kpi_id = getKPI_ID(kpi_detail[i].ten_kpi.Trim(), nhanviengiao, kpi_detail[i].nhom_kpi_id);
                    string sqlInsertBSCDV = "insert into bsc_nhanvien(nhanviengiao, nhanviennhan, thang, nam, kpi, nhanvienthamdinh, donvitinh, trongso, kehoach, thuchien, thamdinh, trangthaithamdinh, nhom_kpi, loaimau, ghichu, stt, donvi) ";
                    sqlInsertBSCDV += "values('" + nhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', '" + kpi_id + "', '" + kpi_detail[i].nhanvienthamdinh + "', '" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + kpi_detail[i].kehoach + "', 0, 0, 0, '" + kpi_detail[i].nhom_kpi_id + "', 16, N'" + kpi_detail[i].ghichu + "', '" + kpi_detail[i].stt + "', '" + donvi + "')";
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
            Message msg = new Message();
            string szMsgContent = "Ban vua nhan duoc BSC/KPI " + thang + "-" + nam + ". Vui long vao kiem tra va xac nhan!!!";
            bool isSuccess = false;
            int id_nhanviennhan = 0;
            id_nhanviennhan = getNhanVienID(nhanviennhan);

            bool isExist = isExistGiaoBSC_NV(nhanviengiao, id_nhanviennhan, thang, nam);
            if (!isExist)
            {
                return false;
            }

            string sqlGiaoBSC = "update giaobscnhanvien set trangthaigiao = 1, ngaytao = GETDATE() where nhanviengiao = '" + nhanviengiao + "' and nhanviennhan = '" + id_nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cnGiaoBSC.ThucThiDL(sqlGiaoBSC);
                msg.SendSMS_ByIDNV(id_nhanviennhan, szMsgContent);
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

        private static int getNhanVienID(string taikhoan)
        {
            int id_nhanviennhan = 0;
            for (int nIndex = 0; nIndex < dtNhanVien.Rows.Count; nIndex++)
            {
                string taikhoanTmp = dtNhanVien.Rows[nIndex]["nhanvien_taikhoan"].ToString().Trim();
                if (taikhoanTmp == taikhoan.Trim())
                {
                    id_nhanviennhan = Convert.ToInt32(dtNhanVien.Rows[nIndex]["nhanvien_id"].ToString().Trim());
                    break;
                }
            }
            return id_nhanviennhan;
        }

        private static string getNhanVienName(string taikhoan)
        {
            string ten_nhanviennhan = "";
            for (int nIndex = 0; nIndex < dtNhanVien.Rows.Count; nIndex++)
            {
                string taikhoanTmp = dtNhanVien.Rows[nIndex]["nhanvien_taikhoan"].ToString().Trim();
                if (taikhoanTmp == taikhoan.Trim())
                {
                    ten_nhanviennhan = dtNhanVien.Rows[nIndex]["nhanvien_hoten"].ToString().Trim();
                    break;
                }
            }
            return ten_nhanviennhan;
        }

        private static int getKPI_ID(string ten_kpi, int nguoitao, int nhom_kpi) {
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            int kpi_id = 0;
            string sql = "select * from kpi where kpi_ten = N'" + ten_kpi + "' and kpi_nguoitao = '" + nguoitao + "'";
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
                kpi_id = Convert.ToInt32(tmp.Rows[0]["kpi_id"].ToString());
            }
            else {
                string sqlInsert = "insert into kpi(kpi_ten, kpi_nguoitao, kpi_thuoc_kpo, nhom_kpi, hienthi) ";
                sqlInsert += "values(N'" + ten_kpi + "', '" + nguoitao + "', 2023, '" + nhom_kpi + "', 1)";
                try
                {
                    cn.ThucThiDL(sqlInsert);
                }
                catch (Exception ex) {
                    throw ex;
                }
                kpi_id = getKPI_ID(ten_kpi, nguoitao, nhom_kpi);
            }
            return kpi_id;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Phân phối bsc nhân viên";
            //if (!IsPostBack)
            //{
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    //nhanvien = Session.GetCurrentUser();
                    nhanvien = (Nhanvien)Session["nhanvien"];

                    // Khai báo các biến cho việc kiểm tra quyền
                    List<int> quyenHeThong = new List<int>();
                    bool nFindResult = false;
                    quyenHeThong = (List<int>)Session["quyenhethong"];

                    /*Kiểm tra nếu không có quyền giao bsc nhân viên (id của quyền là 3) thì đẩy ra trang đăng nhập*/
                    nFindResult = quyenHeThong.Contains(3);

                    if (!nFindResult)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    nhanvienquanly = nhanvien.nhanvien_id;
                    donvi = nhanvien.nhanvien_donvi_id;

                    string sqlDanhSachNhanVien = "select * from nhanvien where nhanvien_donvi = '" + donvi + "'";
                    dtNhanVien = cn.XemDL(sqlDanhSachNhanVien);
                    dtKPI = dsKPI(nhanvienquanly);
                    dtDVT = dsDVT();
                    dtNhomKPI = dsNhomKPI();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            //}
        }
    }
}