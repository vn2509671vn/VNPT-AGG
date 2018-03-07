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
    public partial class PhanPhoiBSCNhanVien_PGD : System.Web.UI.Page
    {
        public int nhanvienthamdinh;
        public static string szNhanviengiao;
        public int donvi;
        public DataTable dtNhanVien = new DataTable();
        public DataTable dtKPI = new DataTable();
        public DataTable dtDVT = new DataTable();
        public DataTable dtNhomKPI = new DataTable();

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
        private static DataTable dsDVT()
        {
            DataTable dsDonvitinh = new DataTable();
            Connection cn = new Connection();
            string sqlDVT = "select * from donvitinh where dvt_id = 17";
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
        private static DataTable dsNhomKPI()
        {
            DataTable dsNhomKPI = new DataTable();
            Connection cn = new Connection();
            string sqlNhomKPI = "select * from nhom_kpi where id = 75";
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
        private static DataTable dsKPI(int nguoitao)
        {
            DataTable dsKPI = new DataTable();
            Connection cn = new Connection();
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

        private static string getNhanVienGiao(string nhanviennhan, int thang, int nam) {
            DataTable dtTmp = new DataTable();
            Connection cn = new Connection();
            string sql = "select nhanviengiao from giaobscnhanvien where thang = '" + thang + "' and nam = '" + nam + "' and nhanviennhan = '" + nhanviennhan + "'";
            try
            {
                dtTmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (dtTmp.Rows.Count > 0) {
                return dtTmp.Rows[0][0].ToString();
            }

            return "";
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int id_nv_thamdinh, string nv_nhan, int thang, int nam, int donvi)
        {
            //string[] arrOutput = {};
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            DataTable dsDonvitinh = new DataTable();
            DataTable dsNhanvienthamdinh = new DataTable();
            DataTable dataNhomKPI = new DataTable();
            dataNhomKPI = dsNhomKPI();
            string sqlDVT = "select * from donvitinh where dvt_id = 17";
            string sqlNVTD = "select nhanvien.* from nhanvien, nhanvien_chucvu where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id and nhanvien_chucvu.chucvu_id in (3,5) and nhanvien.nhanvien_donvi = '" + donvi + "'";

            // Lấy thông tin của nhân viên nhận thông qua tài khoản
            string nhanviennhan = nv_nhan;
            szNhanviengiao = getNhanVienGiao(nhanviennhan, thang, nam);

            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpi.kpi_ma, kpo.kpo_id, kpo.kpo_ten, bsc.donvitinh, bsc.trongso, bsc.kehoach, bsc.nhom_kpi, nhom_kpi.ten_nhom, nhom_kpi.tytrong as tytrong_nhom, bsc.ghichu ";
            sqlBSC += "from bsc_nhanvien bsc, kpi, kpo, nhom_kpi ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.nhanviennhan = '" + nhanviennhan + "' ";
            sqlBSC += "and bsc.nhanvienthamdinh = '" + id_nv_thamdinh + "' ";
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
            sqlGiaoBSCDV += "where bscnhanvien.nhanviengiao = '" + szNhanviengiao + "' ";
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
                dicOutput.Add("nhanviengiao", szNhanviengiao.ToString());
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
                    for (int nIndex = 0; nIndex < dataNhomKPI.Rows.Count; nIndex++)
                    {
                        string szSelected = "";
                        int id_nhom = Convert.ToInt32(dataNhomKPI.Rows[nIndex]["id"].ToString());
                        int tytrong_nhom = Convert.ToInt32(dataNhomKPI.Rows[nIndex]["tytrong"].ToString());
                        if (id_nhom == Convert.ToInt32(nhom_kpi_id))
                        {
                            szSelected = "selected";
                        }
                        outputHTML += "<option data-tytrong-nhom='" + tytrong_nhom + "' value='" + id_nhom + "' " + szSelected + ">" + dataNhomKPI.Rows[nIndex]["ten_nhom"] + "</option>";
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

            return dicOutput;
        }

        [WebMethod]
        public static bool saveData(int nhanvienthamdinh, string nhanviennhan, int thang, int nam, kpiDetail[] kpi_detail, int donvi)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            bool isExist = false;
            string id_nhanviennhan = nhanviennhan;

            if (szNhanviengiao == "") {
                return false;
            }

            isExist = isExistGiaoBSC_NV(id_nhanviennhan, thang, nam);
            if (isExist)
            {
                string sqlDeleteBSCDV = "delete bsc_nhanvien where nhanvienthamdinh = '" + nhanvienthamdinh + "' and nhanviennhan = '" + id_nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
                
                try
                {
                    cnData.ThucThiDL(sqlDeleteBSCDV);
                    for (int i = 0; i < kpi_detail.Length; i++)
                    {
                        int kpi_id = getKPI_ID(kpi_detail[i].ten_kpi.Trim(), nhanvienthamdinh, kpi_detail[i].nhom_kpi_id);
                        string sqlInsertBSCDV = "insert into bsc_nhanvien(nhanviengiao, nhanviennhan, thang, nam, kpi, nhanvienthamdinh, donvitinh, trongso, kehoach, thuchien, thamdinh, trangthaithamdinh, nhom_kpi, loaimau, ghichu, stt, donvi) ";
                        sqlInsertBSCDV += "values('" + szNhanviengiao + "', '" + id_nhanviennhan + "', '" + thang + "', '" + nam + "', '" + kpi_id + "', '" + kpi_detail[i].nhanvienthamdinh + "','" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + kpi_detail[i].kehoach + "', 0, 0, 0, '" + kpi_detail[i].nhom_kpi_id + "', 16, N'" + kpi_detail[i].ghichu + "', '" + kpi_detail[i].stt + "', '" + donvi + "')";
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
                //
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool giaoBSC(string nhanviennhan, int thang, int nam)
        {
            if (szNhanviengiao == "") {
                return false;
            }

            Connection cnGiaoBSC = new Connection();
            Message msg = new Message();
            string szMsgContent = "Ban vua nhan duoc BSC/KPI " + thang + "-" + nam + ". Vui long vao kiem tra va xac nhan!!!";
            bool isSuccess = false;
            string id_nhanviennhan = nhanviennhan;

            string sqlGiaoBSC = "update giaobscnhanvien set trangthaigiao = 1, ngaytao = GETDATE() where nhanviengiao = '" + szNhanviengiao + "' and nhanviennhan = '" + id_nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cnGiaoBSC.ThucThiDL(sqlGiaoBSC);
                msg.SendSMS_ByIDNV( Convert.ToInt32(id_nhanviennhan), szMsgContent);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool huygiaoBSC(string nhanviennhan, int thang, int nam)
        {
            Connection cnGiaoBSC = new Connection();
            bool isSuccess = false;
            string id_nhanviennhan = nhanviennhan;
            string sqlGiaoBSC = "update giaobscnhanvien set trangthaigiao = 0 where nhanviengiao = '" + szNhanviengiao + "' and nhanviennhan = '" + id_nhanviennhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
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

        private static bool isExistGiaoBSC_NV(string id_nv_nhan, int thang, int nam)
        {
            if (szNhanviengiao == "") {
                return false;
            }

            Connection cnData = new Connection();
            bool result = false;
            string sql = "select * ";
            sql += "from giaobscnhanvien ";
            sql += "where nhanviengiao = '" + szNhanviengiao + "' ";
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

        private static int getKPI_ID(string ten_kpi, int nguoitao, int nhom_kpi)
        {
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
            else
            {
                string sqlInsert = "insert into kpi(kpi_ten, kpi_nguoitao, kpi_thuoc_kpo, nhom_kpi, hienthi) ";
                sqlInsert += "values(N'" + ten_kpi + "', '" + nguoitao + "', 2025, '" + nhom_kpi + "', 1)";
                try
                {
                    cn.ThucThiDL(sqlInsert);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                kpi_id = getKPI_ID(ten_kpi, nguoitao, nhom_kpi);
            }
            return kpi_id;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Phân phối bsc cho PGĐ";
            //if (!IsPostBack)
            //{
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                Connection cn = new Connection();
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
                    Response.Write("<script>window.location.href='../index.aspx';</script>");
                }

                nhanvienthamdinh = nhanvien.nhanvien_id;
                donvi = nhanvien.nhanvien_donvi_id;

                string sqlDanhSachNhanVien = "select * from nhanvien where nhanvien_donvi = '" + donvi + "' and nhanvien_chucdanh = 2";
                dtNhanVien = cn.XemDL(sqlDanhSachNhanVien);
                dtKPI = dsKPI(nhanvienthamdinh);
                dtDVT = dsDVT();
                dtNhomKPI = dsNhomKPI();
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.location.href='../index.aspx';</script>");
            }
        }
    }
}