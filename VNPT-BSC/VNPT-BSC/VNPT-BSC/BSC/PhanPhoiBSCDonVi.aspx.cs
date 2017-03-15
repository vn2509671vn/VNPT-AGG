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
        public static int donvichuquan;
        public static DataTable dtDonvi = new DataTable();
        public static DataTable dtFullDV = new DataTable();
        public static DataTable dtBSC = new DataTable();
        public static DataTable dtDVT = new DataTable();
        public class kpiDetail {
            public int kpi_id { get; set; }
            public int tytrong { get; set; }
            public string dvt { get; set; }
            public float kehoach { get; set; }
            public int donvithamdinh { get; set; }
        }
        public static string valDonvigiao = "";
        public static string valDonvinhan = "";
        public static string valThang = "";
        public static string valNam = "";

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
        public static string loadBSC(int thang, int nam, int nguoitao, int loaiMauBSC) {
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            DataTable dsDonvitinh = new DataTable();
            DataTable dsDonvithamdinh = new DataTable();
            string sqlDVT = "select * from donvitinh";
            string sqlDVTD = "select * from donvi";
            string sqlBSC = "select bsc.thang, bsc.nam, bsc.kpi_id, bsc.tytrong, bsc.donvitinh, bsc.donvithamdinh, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, bsc.stt ";
            sqlBSC += "from danhsachbsc bsc, kpi, kpo ";
            sqlBSC += "where bsc.kpi_id = kpi.kpi_id ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' and bsc.nguoitao = '" + nguoitao + "' and bsc.maubsc = '" + loaiMauBSC + "' ORDER BY bsc.stt ASC";
            try{
                gridData = cnBSC.XemDL(sqlBSC);
                dsDonvitinh = cnBSC.XemDL(sqlDVT);
                dsDonvithamdinh = cnBSC.XemDL(sqlDVTD);
            }
            catch (Exception ex){
                throw ex;
            }

            string arrOutput = "";
            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%' data-loaimau = '" + loaiMauBSC + "'>";
                arrOutput += "<thead>";
                    arrOutput += "<tr>";
                        arrOutput += "<th>STT</th>";
                        arrOutput += "<th>Chỉ tiêu</th>";
                        arrOutput += "<th>Tỷ trọng (%)</th>";
                        arrOutput += "<th>ĐVT</th>";
                        arrOutput += "<th>Kế hoạch</th>";
                        arrOutput += "<th>Đơn vị thẩm định</th>";
                    arrOutput += "</tr>";
                arrOutput += "</thead>";
                arrOutput += "<tbody>";
                if (gridData.Rows.Count <= 0)
                {
                    arrOutput += "<tr><td colspan='6' class='text-center'>No item</td></tr>";
                }
                else {
                    for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++) {
                        arrOutput += "<tr data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                        arrOutput += "<td class='text-center'>" + (nKPI+1) + "</td>";
                        arrOutput += "<td class='min-width-130'><strong>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + "</strong></td>";
                        arrOutput += "<td class='text-center'><input type='text' class='form-control' name='tytrong' id='tytrong_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' readonly size='2' maxlength='2' value='" + gridData.Rows[nKPI]["tytrong"].ToString() + "'/></td>";
                        //arrOutput += "<td class='text-center'><input type='text' class='form-control' name='dvt' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='5' readonly value='" + gridData.Rows[nKPI]["donvitinh"].ToString() + "'/></td>";
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

                        // Đơn vị thẩm định
                        arrOutput += "<td class='text-center'>";
                        arrOutput += "<select class='form-control' id='dvtd_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                        for (int nDVTD = 0; nDVTD < dsDonvithamdinh.Rows.Count; nDVTD++)
                        {
                            //arrOutput += "<option value='" + dsDonvithamdinh.Rows[nDVTD]["donvi_id"] + "'>" + dsDonvithamdinh.Rows[nDVTD]["donvi_ten"] + "</option>";
                            if (dsDonvithamdinh.Rows[nDVTD]["donvi_id"].ToString() == gridData.Rows[nKPI]["donvithamdinh"].ToString())
                            {
                                arrOutput += "<option value='" + dsDonvithamdinh.Rows[nDVTD]["donvi_id"] + "' selected>" + dsDonvithamdinh.Rows[nDVTD]["donvi_ten"] + "</option>";
                            }
                            else
                            {
                                arrOutput += "<option value='" + dsDonvithamdinh.Rows[nDVTD]["donvi_id"] + "'>" + dsDonvithamdinh.Rows[nDVTD]["donvi_ten"] + "</option>";
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
        public static Dictionary<String, String> loadBSCByCondition(int id_dv_giao, int id_dv_nhan, int thang, int nam)
        {
            //string[] arrOutput = {};
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();

            /*Lấy danh sách các thông tin còn lại ở bảng giaobscdonvi*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select * from giaobscdonvi ";
            sqlGiaoBSCDV += "where donvigiao = '" + id_dv_giao + "' ";
            sqlGiaoBSCDV += "and donvinhan = '" + id_dv_nhan + "'";
            sqlGiaoBSCDV += "and thang = '" + thang + "'";
            sqlGiaoBSCDV += "and nam = '" + nam + "'";
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
                dicOutput.Add("donvigiao", dtGiaoBSCDV.Rows[0]["donvigiao"].ToString());
                dicOutput.Add("donvinhan", dtGiaoBSCDV.Rows[0]["donvinhan"].ToString());
                dicOutput.Add("trangthaigiao", dtGiaoBSCDV.Rows[0]["trangthaigiao"].ToString());
                dicOutput.Add("trangthainhan", dtGiaoBSCDV.Rows[0]["trangthainhan"].ToString());
                dicOutput.Add("trangthaiketthuc", dtGiaoBSCDV.Rows[0]["trangthaiketthuc"].ToString());
            }
            else
            {
                dicOutput.Add("donvigiao", id_dv_giao.ToString());
                dicOutput.Add("donvinhan", id_dv_nhan.ToString());
                dicOutput.Add("trangthaigiao", "0");
                dicOutput.Add("trangthainhan", "0");
                dicOutput.Add("trangthaiketthuc", "0");
            }

            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            DataTable dsDonvitinh = new DataTable();
            DataTable dsDonvithamdinh = new DataTable();
            string sqlDVT = "select * from donvitinh";
            string sqlDVTD = "select * from donvi";
            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, bsc.donvitinh, bsc.trongso, bsc.kehoach, bsc.donvithamdinh, bsc.phanhoi_giao_dexuat, bsc.phanhoi_giao_lydo, bsc.phanhoi_giao_daxuly, danhsachbsc.stt ";
            sqlBSC += "from bsc_donvi bsc, kpi, kpo, donvi dvgiao, donvi dvnhan, danhsachbsc ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and bsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and bsc.donvinhan = '" + id_dv_nhan + "' ";
            sqlBSC += "and bsc.donvigiao = '" + id_dv_giao + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = danhsachbsc.thang ";
            sqlBSC += "and bsc.nam = danhsachbsc.nam ";
            sqlBSC += "and bsc.loaimau = danhsachbsc.maubsc ";
            sqlBSC += "and bsc.kpi = danhsachbsc.kpi_id  ";
            sqlBSC += "and danhsachbsc.bscduocgiao = '' ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' ORDER BY danhsachbsc.stt ASC";
            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
                dsDonvitinh = cnBSC.XemDL(sqlDVT);
                dsDonvithamdinh = cnBSC.XemDL(sqlDVTD);
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
            outputHTML += "<th>Đơn vị thẩm định</th>";
            if (dicOutput["trangthainhan"] == "False")
            {
                outputHTML += "<th>Chức năng</th>";
            }
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                if (dicOutput["trangthainhan"] == "False")
                {
                    outputHTML += "<tr><td colspan='7' class='text-center'>No item</td></tr>";
                }
                else
                {
                    outputHTML += "<tr><td colspan='6' class='text-center'>No item</td></tr>";
                }
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    string trangthai_xuly_phanhoi = "";
                    string kpi_id = gridData.Rows[nKPI]["kpi_id"].ToString();
                    string kpi_ten = gridData.Rows[nKPI]["kpi_ten"].ToString();
                    string kehoach_duocgiao = gridData.Rows[nKPI]["kehoach"].ToString();
                    string kehoach_dexuat = gridData.Rows[nKPI]["phanhoi_giao_dexuat"].ToString();
                    string lydo_dexuat = gridData.Rows[nKPI]["phanhoi_giao_lydo"].ToString();
                    string daxuly_dexuat = gridData.Rows[nKPI]["phanhoi_giao_daxuly"].ToString();
                    string dataTmp = kpi_id + "-" + kpi_ten + "-" + kehoach_duocgiao + "-" + kehoach_dexuat + "-" + lydo_dexuat + "-" + daxuly_dexuat;
                    if (gridData.Rows[nKPI]["phanhoi_giao_daxuly"].ToString() == "True")
                    {
                        trangthai_xuly_phanhoi = "cls_daxuly";
                    }
                    else if (gridData.Rows[nKPI]["phanhoi_giao_daxuly"].ToString() == "False")
                    {
                        trangthai_xuly_phanhoi = "cls_chuaxuly";
                    }

                    outputHTML += "<tr class='" + trangthai_xuly_phanhoi + "' data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<input type='hidden' value='" + dataTmp + "' id='idPhanHoi_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'/>";
                    outputHTML += "<td class='text-center'>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td class='min-width-130'><strong>" +  gridData.Rows[nKPI]["kpi_ten"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='tytrong' id='tytrong_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' maxlength='2' value='" + gridData.Rows[nKPI]["trongso"].ToString() + "'/></td>";
                    //outputHTML += "<td class='text-center'><input type='text' class='form-control' name='dvt' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='5' value='" + gridData.Rows[nKPI]["donvitinh"].ToString() + "'/></td>";
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
                    
                    // Đơn vị thẩm định
                    outputHTML += "<td class='text-center'>";
                    outputHTML += "<select class='form-control' id='dvtd_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    for (int nDVTD = 0; nDVTD < dsDonvithamdinh.Rows.Count; nDVTD++)
                    {
                        if (dsDonvithamdinh.Rows[nDVTD]["donvi_id"].ToString() == gridData.Rows[nKPI]["donvithamdinh"].ToString())
                        {
                            outputHTML += "<option value='" + dsDonvithamdinh.Rows[nDVTD]["donvi_id"] + "' selected>" + dsDonvithamdinh.Rows[nDVTD]["donvi_ten"] + "</option>";
                        }
                        else
                        {
                            outputHTML += "<option value='" + dsDonvithamdinh.Rows[nDVTD]["donvi_id"] + "'>" + dsDonvithamdinh.Rows[nDVTD]["donvi_ten"] + "</option>";
                        }
                    }
                    outputHTML += "</select>";
                    outputHTML += "</td>";

                    // Chức năng
                    outputHTML += "<td class='text-center'>";
                    if (dicOutput["trangthainhan"] == "False")
                    {
                        outputHTML += "<a class='btn btn-primary btn-xs' type='button' data-target='#guiPhanhoi' data-toggle='modal' onclick='phanHoi(" + gridData.Rows[nKPI]["kpi_id"].ToString() + ")'>Phản Hồi</a>";
                    }
                    outputHTML += "</td>";
                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);
            
            return dicOutput;
        }

        [WebMethod]
        public static bool saveData(int donvigiao, int donvinhan, int thang, int nam, kpiDetail[] kpi_detail, int loaimau)
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

                    string sqlInsertGiaoBSC = "insert into giaobscdonvi(donvigiao, donvinhan, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaidongy_kqtd, trangthaiketthuc, loaimau) ";
                    sqlInsertGiaoBSC += "values('" + donvigiao + "', '" + donvinhan + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0, 0, '" + loaimau + "')";
                    cnData.ThucThiDL(sqlInsertGiaoBSC);
                    for (int i = 0; i < kpi_detail.Length; i++)
                    {
                        string sqlInsertBSCDV = "insert into bsc_donvi(donvigiao, donvinhan, thang, nam, kpi, donvithamdinh, donvitinh, trongso, kehoach, thuchien, thamdinh, trangthaithamdinh, kq_thuchien, hethong_thuchien, loaimau) ";
                        sqlInsertBSCDV += "values('" + donvigiao + "', '" + donvinhan + "', '" + thang + "', '" + nam + "', '" + kpi_detail[i].kpi_id + "', '" + kpi_detail[i].donvithamdinh + "','" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + kpi_detail[i].kehoach + "', 0, 0, 0, 0, 0, '" + loaimau + "')";
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
                string sqlInsertGiaoBSC = "insert into giaobscdonvi(donvigiao, donvinhan, thang, nam, trangthaigiao, trangthainhan, trangthaicham, trangthaidongy_kqtd, trangthaiketthuc, loaimau) ";
                sqlInsertGiaoBSC += "values('" + donvigiao + "', '" + donvinhan + "', '" + thang + "', '" + nam + "', 0, 0, 0, 0, 0, '" + loaimau + "')";
                cnData.ThucThiDL(sqlInsertGiaoBSC);
                for (int i = 0; i < kpi_detail.Length; i++)
                {
                    string sqlInsertBSCDV = "insert into bsc_donvi(donvigiao, donvinhan, thang, nam, kpi, donvithamdinh, donvitinh, trongso, kehoach, thuchien, thamdinh, trangthaithamdinh, kq_thuchien, hethong_thuchien, loaimau) ";
                    sqlInsertBSCDV += "values('" + donvigiao + "', '" + donvinhan + "', '" + thang + "', '" + nam + "', '" + kpi_detail[i].kpi_id + "', '" + kpi_detail[i].donvithamdinh + "', '" + kpi_detail[i].dvt + "', '" + Convert.ToInt32(kpi_detail[i].tytrong) + "', '" + kpi_detail[i].kehoach + "', 0, 0, 0, 0, 0, '" + loaimau + "')";
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
            Message msg = new Message();
            string szMsgContent = "Ban vua nhan duoc BSC/KPI " + thang + "-" + nam + ". Vui long vao kiem tra va xac nhan!!!";
            bool isSuccess = false;
            bool isExist = isExistGiaoBSC_DV(donvigiao, donvinhan, thang, nam);
            if (!isExist) {
                return false;
            }

            string sqlGiaoBSC = "update giaobscdonvi set trangthaigiao = 1, ngaytao = GETDATE() where donvigiao = '"+donvigiao+"' and donvinhan = '"+donvinhan+"' and thang = '"+thang+"' and nam = '"+nam+"'";
            try
            {
                cnGiaoBSC.ThucThiDL(sqlGiaoBSC);
                msg.SendSMS_ByIDDV(donvinhan, szMsgContent);
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
            Message msg = new Message();
            string szMsgContent = "BSC/KPI " + thang + "-" + nam + " cua ban da duoc nghiem thu xong!!!";
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
                msg.SendSMS_ByIDDV(donvinhan, szMsgContent);
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

        [WebMethod]
        public static bool xulyPhanHoi(int donvigiao, int donvinhan, int thang, int nam, int kpi_id, float kehoach_cuoicung)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            Message msg = new Message();
            string szMsgContent = "Phan hoi ve viec giao BSC da duoc xu ly!!! Ban vui long vao kiem tra lai.";
            try
            {
                string sqlUpdatePhanHoi = "update bsc_donvi set kehoach = '" + kehoach_cuoicung + "', phanhoi_giao_daxuly = 1 ";
                sqlUpdatePhanHoi += "where donvigiao = '" + donvigiao + "' ";
                sqlUpdatePhanHoi += "and donvinhan = '" + donvinhan + "' ";
                sqlUpdatePhanHoi += "and thang = '" + thang + "' ";
                sqlUpdatePhanHoi += "and nam = '" + nam + "' ";
                sqlUpdatePhanHoi += "and kpi = '" + kpi_id + "' ";
                sqlUpdatePhanHoi += "and trangthaithamdinh = 0";
                //sqlUpdatePhanHoi += "EXEC sp_ketquathuchien @thang = '" + thang + "',@nam = '" + nam + "', @donvigiao = '" + donvigiao + "', @donvinhan = '" + donvinhan + "', @kpi_id = '" + kpi_id + "'";

                try
                {
                    cnData.ThucThiDL(sqlUpdatePhanHoi);
                    msg.SendSMS_ByIDDV(donvinhan, szMsgContent);
                    isSuccess = true;
                }
                catch
                {
                    isSuccess = false;
                }
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Phân phối bsc";
            try
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();

                // Khai báo các biến cho việc kiểm tra quyền
                List<int> quyenHeThong = new List<int>();
                bool nFindResult = false;
                quyenHeThong = Session.GetRole();
                
                /*Kiểm tra nếu không có quyền giao bsc đơn vị (id của quyền là 2) thì đẩy ra trang đăng nhập*/
                nFindResult = quyenHeThong.Contains(2);

                if (nhanvien == null || !nFindResult)
                {
                    Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }

                donvichuquan = nhanvien.nhanvien_donvi_id;

                string sqlDanhSachDonVi = "select * from donvi";
                string sqlDanhSachKPI = "select TOP 20 DANHSACHBSC.thang, DANHSACHBSC.nam, CONVERT(varchar(4), DANHSACHBSC.thang) + '/' + CONVERT(varchar(4), DANHSACHBSC.nam) + ' ' + loaimaubsc.loai_ten + N' - Người tạo:' + nhanvien.nhanvien_hoten AS content, nhanvien.nhanvien_id, loaimaubsc.loai_id ";
                //sqlDanhSachKPI += "from DANHSACHBSC, nhanvien where DANHSACHBSC.nguoitao in (select nhanvien_id from nhanvien_chucvu where chucvu_id = 10) ";
                sqlDanhSachKPI += "from DANHSACHBSC, nhanvien, loaimaubsc where DANHSACHBSC.nguoitao ";
                sqlDanhSachKPI += "in (select nhanvien_chucvu.nhanvien_id ";
                sqlDanhSachKPI += "from chucvu, nhanvien_chucvu, quyen_cv ";
                sqlDanhSachKPI += "where chucvu.chucvu_id = nhanvien_chucvu.chucvu_id ";
                sqlDanhSachKPI += "and chucvu.chucvu_id = quyen_cv.chucvu_id ";
                sqlDanhSachKPI += "and quyen_cv.quyen_id = 2) ";
                sqlDanhSachKPI += "and DANHSACHBSC.bscduocgiao = '' ";
                sqlDanhSachKPI += "and DANHSACHBSC.nguoitao = nhanvien.nhanvien_id ";
                sqlDanhSachKPI += "and DANHSACHBSC.maubsc = loaimaubsc.loai_id ";
                sqlDanhSachKPI += "group by DANHSACHBSC.nam, DANHSACHBSC.thang, nhanvien.nhanvien_hoten, nhanvien.nhanvien_id, loaimaubsc.loai_id, loaimaubsc.loai_ten ";
                sqlDanhSachKPI += "order by DANHSACHBSC.nam, DANHSACHBSC.thang DESC";

                dtDonvi = cn.XemDL(sqlDanhSachDonVi);
                dtBSC = cn.XemDL(sqlDanhSachKPI);
                dtDVT = dsDVT();

                // Get các tham số từ url
                valDonvinhan = Request.QueryString["donvinhan"];
                valThang = Request.QueryString["thang"];
                valNam = Request.QueryString["nam"];

            }
            catch (Exception ex)
            {
                Response.Write("<script>window.location.href='../Login.aspx';</script>");
            }
        }
    }
}