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
    public partial class PhanPhoiBSCDonVi : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public static DataTable dtDonvi = new DataTable();
        public static DataTable dtFullDV = new DataTable();
        public static DataTable dtBSC = new DataTable();
        public class kpiDetail {
            public int kpi_id { get; set; }
            public int tytrong { get; set; }
            public string dvt { get; set; }
            public decimal kehoach { get; set; }
        }
        [WebMethod]
        public static string loadBSC(int thang, int nam) {
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string sqlBSC = "select bsc.thang, bsc.nam, bsc.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten ";
            sqlBSC += "from danhsachbsc bsc, kpi, kpo ";
            sqlBSC += "where bsc.kpi_id = kpi.kpi_id ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = '"+thang+"' and bsc.nam = '"+nam+"'";
            try{
                gridData = cnBSC.XemDL(sqlBSC);
            }
            catch (Exception ex){
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
                    arrOutput += "</tr>";
                arrOutput += "</thead>";
                arrOutput += "<tbody>";
                if (gridData.Rows.Count <= 0)
                {
                    arrOutput += "<tr><td colspan='5' class='text-center'>No item</td></tr>";
                }
                else {
                    for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++) {
                        arrOutput += "<tr data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                        arrOutput += "<td>" + (nKPI+1) + "</td>";
                        arrOutput += "<td>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + " (" + gridData.Rows[nKPI]["kpo_ten"].ToString()  + ")" + "</td>";
                        arrOutput += "<td class='text-center'><input type='text' class='form-control' name='tytrong' id='tytrong_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' maxlength='2'/></td>";
                        arrOutput += "<td class='text-center'><input type='text' class='form-control' name='dvt' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='5'/></td>";
                        arrOutput += "<td class='text-center'><input type='text' class='form-control' name='kehoach' id='kehoach_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' onkeypress='return onlyNumbers(event)'/></td>";
                        arrOutput += "</tr>";
                    }
                }
                arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }


        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int id_dv_giao, int id_dv_nhan, int thang, int nam)
        {
            //string[] arrOutput = {};
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, bsc.donvitinh, bsc.trongso, bsc.kehoach ";
            sqlBSC += "from bsc_donvi bsc, kpi, kpo, donvi dvgiao, donvi dvnhan ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and bsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and bsc.donvinhan = '" + id_dv_nhan + "' ";
            sqlBSC += "and bsc.donvigiao = '" + id_dv_giao + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "'";
            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
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
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='5' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    outputHTML += "<tr data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<td>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + " (" + gridData.Rows[nKPI]["kpo_ten"].ToString() + ")" + "</td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='tytrong' id='tytrong_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' maxlength='2' value='" + gridData.Rows[nKPI]["trongso"].ToString() + "'/></td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='dvt' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='5' value='" + gridData.Rows[nKPI]["donvitinh"].ToString() + "'/></td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='kehoach' id='kehoach_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' value='" + gridData.Rows[nKPI]["kehoach"].ToString() + "' onkeypress='return onlyNumbers(event)'/></td>";
                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);

            /*Lấy danh sách các thông tin còn lại ở bảng giaobscdonvi*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select * from giaobscdonvi ";
            sqlGiaoBSCDV += "where donvigiao = '"+id_dv_giao+"' ";
            sqlGiaoBSCDV += "and donvinhan = '" + id_dv_nhan + "'";
            sqlGiaoBSCDV += "and thang = '" + thang + "'";
            sqlGiaoBSCDV += "and nam = '" + nam + "'";
            try
            {
                dtGiaoBSCDV = cnBSC.XemDL(sqlGiaoBSCDV);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (dtGiaoBSCDV.Rows.Count > 0)
            {
                dicOutput.Add("donvigiao", dtGiaoBSCDV.Rows[0]["donvigiao"].ToString());
                dicOutput.Add("donvinhan", dtGiaoBSCDV.Rows[0]["donvinhan"].ToString());
                dicOutput.Add("donvithamdinh", dtGiaoBSCDV.Rows[0]["donvithamdinh"].ToString());
                dicOutput.Add("trangthaigiao", dtGiaoBSCDV.Rows[0]["trangthaigiao"].ToString());
                dicOutput.Add("trangthainhan", dtGiaoBSCDV.Rows[0]["trangthainhan"].ToString());
                dicOutput.Add("trangthaithamdinh", dtGiaoBSCDV.Rows[0]["trangthaithamdinh"].ToString());
                dicOutput.Add("trangthaiketthuc", dtGiaoBSCDV.Rows[0]["trangthaiketthuc"].ToString());
            }
            else {
                dicOutput.Add("donvigiao", id_dv_giao.ToString());
                dicOutput.Add("donvinhan", id_dv_nhan.ToString());
                dicOutput.Add("donvithamdinh", "");
                dicOutput.Add("trangthaigiao", "0");
                dicOutput.Add("trangthainhan", "0");
                dicOutput.Add("trangthaithamdinh", "0");
                dicOutput.Add("trangthaiketthuc", "0");
            }
            
            return dicOutput;
        }

        [WebMethod]
        public static bool saveData(int donvigiao, int donvinhan, int donvithamdinh, int thang, int nam, kpiDetail[] kpi_detail)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            bool isExist = false;
            isExist = isExistGiaoBSC_DV(donvigiao, donvinhan, thang, nam);
            if (isExist)
            {
                string sqlDeleteBSCDV = "delete bsc_donvi where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
                string sqlDeleteGiaoBSCDV = "delete giaobscdonvi where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
                try
                {
                    cnData.ThucThiDL(sqlDeleteBSCDV);
                    cnData.ThucThiDL(sqlDeleteGiaoBSCDV);

                    string sqlInsertGiaoBSC = "insert into giaobscdonvi(donvigiao, donvinhan, donvithamdinh, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaithamdinh, trangthaiketthuc) ";
                    sqlInsertGiaoBSC += "values('" + donvigiao + "', '" + donvinhan + "', '" + donvithamdinh + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0)";
                    cnData.ThucThiDL(sqlInsertGiaoBSC);
                    for (int i = 0; i < kpi_detail.Length; i++)
                    {
                        string sqlInsertBSCDV = "insert into bsc_donvi(donvigiao, donvinhan, thang, nam, kpi, donvitinh, trongso, kehoach) ";
                        sqlInsertBSCDV += "values('" + donvigiao + "', '" + donvinhan + "', '" + thang + "', '" + nam + "', '" + Convert.ToInt32(kpi_detail[i].kpi_id) + "', N'" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + Convert.ToDecimal(kpi_detail[i].kehoach) + "')";
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
                catch {
                    isSuccess = false;
                }
            }
            else {
                string sqlInsertGiaoBSC = "insert into giaobscdonvi(donvigiao, donvinhan, donvithamdinh, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaithamdinh, trangthaiketthuc) ";
                sqlInsertGiaoBSC += "values('" + donvigiao + "', '" + donvinhan + "', '" + donvithamdinh + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0, 0)";
                cnData.ThucThiDL(sqlInsertGiaoBSC);
                for (int i = 0; i < kpi_detail.Length; i++)
                {
                    string sqlInsertBSCDV = "insert into bsc_donvi(donvigiao, donvinhan, thang, nam, kpi, donvitinh, trongso, kehoach) ";
                    sqlInsertBSCDV += "values('" + donvigiao + "', '" + donvinhan + "', '" + thang + "', '" + nam + "', '" + Convert.ToInt32(kpi_detail[i].kpi_id) + "', N'" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + Convert.ToDecimal(kpi_detail[i].kehoach) + "')";
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
        public static bool giaoBSC(int donvigiao, int donvinhan, int thang, int nam) {
            Connection cnGiaoBSC = new Connection();
            bool isSuccess = false;
            bool isExist = isExistGiaoBSC_DV(donvigiao, donvinhan, thang, nam);
            if (!isExist) {
                return false;
            }

            string sqlGiaoBSC = "update giaobscdonvi set trangthaigiao = 1 where donvigiao = '"+donvigiao+"' and donvinhan = '"+donvinhan+"' and thang = '"+thang+"' and nam = '"+nam+"'";
            try
            {
                cnGiaoBSC.ThucThiDL(sqlGiaoBSC);
                isSuccess = true;
            }
            catch {
                isSuccess = false;
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool huygiaoBSC(int donvigiao, int donvinhan, int thang, int nam)
        {
            Connection cnGiaoBSC = new Connection();
            bool isSuccess = false;
            string sqlGiaoBSC = "update giaobscdonvi set trangthaigiao = 0 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
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
        public static bool ketthucBSC(int donvigiao, int donvinhan, int thang, int nam)
        {
            Connection cnGiaoBSC = new Connection();
            bool isSuccess = false;
            bool isExist = isExistGiaoBSC_DV(donvigiao, donvinhan, thang, nam);
            if (!isExist)
            {
                return false;
            }

            string sqlGiaoBSC = "update giaobscdonvi set trangthaiketthuc = 1 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
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

        private static bool isExistGiaoBSC_DV(int id_dv_giao, int id_dv_nhan, int thang, int nam)
        {
            Connection cnData = new Connection();
            bool result = false;
            string sql = "select * ";
            sql += "from giaobscdonvi ";
            sql += "where donvigiao = '" + id_dv_giao + "' ";
            sql += "and donvinhan = '" + id_dv_nhan + "' ";
            sql += "and thang = '" + thang + "' ";
            sql += "and nam = '" + nam + "' ";
            DataTable dtQuery = new DataTable();
            try {
                dtQuery = cnData.XemDL(sql);
            }
            catch (Exception ex){
                throw ex;
            }

            if (dtQuery.Rows.Count > 0) {
                result = true;
            }

            return result;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                try
                {
                    int donvichuquan = 1;
                    string sqlDanhSachDonVi = "select * from donvi where donvi_id not in ('" + donvichuquan + "')";
                    string sqlDanhSachFullDV = "select * from donvi";
                    string sqlDanhSachKPI = "select thang, nam, CONVERT(varchar(4), thang) + '/' + CONVERT(varchar(4), nam) AS content from DANHSACHBSC group by nam, thang order by nam, thang ASC";
                    dtDonvi = cn.XemDL(sqlDanhSachDonVi);
                    dtBSC = cn.XemDL(sqlDanhSachKPI);
                    dtFullDV = cn.XemDL(sqlDanhSachFullDV);
                }
                catch (Exception ex){
                    throw ex;
                }
            }
        }
    }
}