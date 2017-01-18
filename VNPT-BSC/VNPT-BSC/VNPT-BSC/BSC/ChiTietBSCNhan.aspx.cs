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
    public partial class ChiTietBSCNhan : System.Web.UI.Page
    {
        public static string donvigiao, donvinhan, thang, nam;
        public class kpiDetail
        {
            public int kpi_id { get; set; }
            public decimal thuchien { get; set; }
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int donvigiao, int donvinhan, int thang, int nam)
        {
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            int soluong_kpi_dathamdinh = 0;
            string szSL_kpi_dathamdinh = "";

            /*Lấy danh sách các thông tin còn lại ở bảng giaobscdonvi*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select * from giaobscdonvi ";
            sqlGiaoBSCDV += "where donvigiao = '" + donvigiao + "' ";
            sqlGiaoBSCDV += "and donvinhan = '" + donvinhan + "'";
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
                dicOutput.Add("thang", dtGiaoBSCDV.Rows[0]["thang"].ToString());
                dicOutput.Add("nam", dtGiaoBSCDV.Rows[0]["nam"].ToString());
                dicOutput.Add("trangthaigiao", dtGiaoBSCDV.Rows[0]["trangthaigiao"].ToString());
                dicOutput.Add("trangthainhan", dtGiaoBSCDV.Rows[0]["trangthainhan"].ToString());
                dicOutput.Add("trangthaicham", dtGiaoBSCDV.Rows[0]["trangthaicham"].ToString());
                dicOutput.Add("trangthaiketthuc", dtGiaoBSCDV.Rows[0]["trangthaiketthuc"].ToString());
                dicOutput.Add("trangthaidongy_kqtd", dtGiaoBSCDV.Rows[0]["trangthaidongy_kqtd"].ToString());
            }
            else
            {
                dicOutput.Add("donvigiao", donvigiao.ToString());
                dicOutput.Add("donvinhan", donvinhan.ToString());
                dicOutput.Add("thang", "0");
                dicOutput.Add("nam", "0");
                dicOutput.Add("trangthaigiao", "0");
                dicOutput.Add("trangthainhan", "0");
                dicOutput.Add("trangthaicham", "0");
                dicOutput.Add("trangthaiketthuc", "0");
                dicOutput.Add("trangthaidongy_kqtd", "0");
            }

            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, bsc.trangthaithamdinh, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, dvt.dvt_ten as donvitinh, bsc.trongso, bsc.kehoach, bsc.thuchien, bsc.thamdinh, dvthamdinh.donvi_ten as donvithamdinh, bsc.phanhoi_giao_dexuat, bsc.phanhoi_giao_lydo, bsc.phanhoi_giao_daxuly, bsc.phanhoi_thamdinh_dexuat, bsc.phanhoi_thamdinh_lydo, bsc.phanhoi_thamdinh_daxuly ";
            sqlBSC += "from bsc_donvi bsc, kpi, kpo, donvi dvgiao, donvi dvnhan, donvi dvthamdinh, donvitinh dvt ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and bsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and bsc.donvithamdinh = dvthamdinh.donvi_id ";
            sqlBSC += "and bsc.donvinhan = '" + donvinhan + "' ";
            sqlBSC += "and bsc.donvigiao = '" + donvigiao + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.donvitinh = dvt.dvt_id ";
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
            outputHTML += "<th>Thực hiện</th>";
            outputHTML += "<th>Thẩm định</th>";
            outputHTML += "<th>Trạng thái thẩm định</th>";
            if (dicOutput["trangthainhan"] == "False") {
                outputHTML += "<th>Phản hồi</th>";
            }
            if (dicOutput["trangthaidongy_kqtd"] == "False" && dicOutput["trangthaicham"] == "True")
            {
                outputHTML += "<th>Phản hồi</th>";
            }
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                if (dicOutput["trangthainhan"] == "False")
                {
                    outputHTML += "<tr><td colspan='9' class='text-center'>No item</td></tr>";
                }
                else {
                    outputHTML += "<tr><td colspan='8' class='text-center'>No item</td></tr>";
                }
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    string txtTrangThaiThamDinh = "Chưa thẩm định";
                    string clsTrangThaiThamDinh = "label-default";
                    string trangthai_xuly_phanhoi = "";
                    string kpi_id = gridData.Rows[nKPI]["kpi_id"].ToString();
                    string kpi_ten = gridData.Rows[nKPI]["kpi_ten"].ToString();
                    string kehoach_duocgiao = gridData.Rows[nKPI]["kehoach"].ToString();
                    string ketqua_thamdinh = gridData.Rows[nKPI]["thamdinh"].ToString();

                    // Giao BSC
                    string kehoach_dexuat = gridData.Rows[nKPI]["phanhoi_giao_dexuat"].ToString();
                    string lydo_dexuat = gridData.Rows[nKPI]["phanhoi_giao_lydo"].ToString();
                    string daxuly_dexuat = gridData.Rows[nKPI]["phanhoi_giao_daxuly"].ToString();

                    // Thẩm định BSC
                    string thamdinh_dexuat = gridData.Rows[nKPI]["phanhoi_thamdinh_dexuat"].ToString();
                    string thamdinh_lydo_dexuat = gridData.Rows[nKPI]["phanhoi_thamdinh_lydo"].ToString();
                    string thamdinh_daxuly_dexuat = gridData.Rows[nKPI]["phanhoi_thamdinh_daxuly"].ToString();

                    string dataTmp = kpi_id + "-" + kpi_ten + "-" + kehoach_duocgiao + "-" + kehoach_dexuat + "-" + lydo_dexuat + "-" + daxuly_dexuat;
                    string dataTmpTD = kpi_id + "-" + kpi_ten + "-" + ketqua_thamdinh + "-" + thamdinh_dexuat + "-" + thamdinh_lydo_dexuat + "-" + thamdinh_daxuly_dexuat;

                    if (gridData.Rows[nKPI]["trangthaithamdinh"].ToString() == "True") {
                        soluong_kpi_dathamdinh += 1;
                        txtTrangThaiThamDinh = "Đã thẩm định";
                        clsTrangThaiThamDinh = "label-success";
                    }

                    if (gridData.Rows[nKPI]["phanhoi_giao_daxuly"].ToString() == "True")
                    {
                        trangthai_xuly_phanhoi = "cls_daxuly";
                    }
                    else if (gridData.Rows[nKPI]["phanhoi_giao_daxuly"].ToString() == "False")
                    {
                        trangthai_xuly_phanhoi = "cls_chuaxuly";
                    }

                    if (gridData.Rows[nKPI]["phanhoi_thamdinh_daxuly"].ToString() == "True")
                    {
                        trangthai_xuly_phanhoi = "cls_daxuly";
                    }
                    else if (gridData.Rows[nKPI]["phanhoi_thamdinh_daxuly"].ToString() == "False")
                    {
                        trangthai_xuly_phanhoi = "cls_chuaxuly";
                    }

                    outputHTML += "<tr class='" + trangthai_xuly_phanhoi + "' data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<input type='hidden' value='" + dataTmp + "' id='idPhanHoi_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'/>";
                    outputHTML += "<input type='hidden' value='" + dataTmpTD + "' id='idPhanHoiTD_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'/>";
                    outputHTML += "<td class='text-center'>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + " (" + gridData.Rows[nKPI]["kpo_ten"].ToString() + ")" + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["trongso"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center fix-table-edit-100'><strong>" + gridData.Rows[nKPI]["donvitinh"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["kehoach"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='thuchien' id='thuchien_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='3' value='" + gridData.Rows[nKPI]["thuchien"].ToString() + "' onkeypress='return onlyNumbers(event.charCode || event.keyCode);'/></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["thamdinh"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + txtTrangThaiThamDinh + "</span></td>";
                    outputHTML += "<td class='text-center'>";
                    if (dicOutput["trangthainhan"] == "False")
                    {
                        outputHTML += "<a class='btn btn-primary btn-xs' type='button' data-target='#guiPhanhoi' data-toggle='modal' onclick='phanHoi(" + gridData.Rows[nKPI]["kpi_id"].ToString() + ")'>Đơn vị giao</a>";
                    }

                    if (dicOutput["trangthaidongy_kqtd"] == "False" && gridData.Rows[nKPI]["trangthaithamdinh"].ToString() == "True")
                    {
                        outputHTML += "<a class='btn btn-primary btn-xs' type='button' data-target='#guiPhanhoiTD' data-toggle='modal' onclick='phanHoiTD(" + gridData.Rows[nKPI]["kpi_id"].ToString() + ")'>Đơn vị TĐ</a>";
                    }
                    outputHTML += "</td>";
                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";

            dicOutput.Add("gridBSC", outputHTML);

            szSL_kpi_dathamdinh = soluong_kpi_dathamdinh.ToString() + "/" + gridData.Rows.Count.ToString();
            dicOutput.Add("soluong_kpi_dathamdinh", szSL_kpi_dathamdinh);

            return dicOutput;
        }

        [WebMethod]
        public static bool updateNhanStatus(int donvigiao, int donvinhan, int thang, int nam)
        {
            Connection cnNhanBSC = new Connection();
            bool isSuccess = false;

            string sqlGiaoBSC = "update giaobscdonvi set trangthainhan = 1 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlUpdatePhanHoi = "update bsc_donvi set phanhoi_giao_daxuly = 1 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "' and phanhoi_giao_daxuly = 0";
            try
            {
                cnNhanBSC.ThucThiDL(sqlGiaoBSC);
                cnNhanBSC.ThucThiDL(sqlUpdatePhanHoi);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool updateChamStatus(int donvigiao, int donvinhan, int thang, int nam)
        {
            Connection cnNhanBSC = new Connection();
            bool isSuccess = false;

            string sqlGiaoBSC = "update giaobscdonvi set trangthaicham = 1 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cnNhanBSC.ThucThiDL(sqlGiaoBSC);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool updateDongYStatus(int donvigiao, int donvinhan, int thang, int nam)
        {
            Connection cnNhanBSC = new Connection();
            bool isSuccess = false;

            string sqlGiaoBSC = "update giaobscdonvi set trangthaidongy_kqtd = 1 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            string sqlUpdatePhanHoi = "update bsc_donvi set phanhoi_thamdinh_daxuly = 1 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "' and phanhoi_thamdinh_daxuly = 0";
            try
            {
                cnNhanBSC.ThucThiDL(sqlGiaoBSC);
                cnNhanBSC.ThucThiDL(sqlUpdatePhanHoi);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool updateHuyChamStatus(int donvigiao, int donvinhan, int thang, int nam)
        {
            Connection cnNhanBSC = new Connection();
            bool isSuccess = false;

            string sqlGiaoBSC = "update giaobscdonvi set trangthaicham = 0 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "'";
            try
            {
                cnNhanBSC.ThucThiDL(sqlGiaoBSC);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        [WebMethod]
        public static bool saveData(int donvigiao, int donvinhan, int thang, int nam, kpiDetail[] kpi_detail)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            try
            {
                for (int i = 0; i < kpi_detail.Length; i++)
                {
                    string sqlInsertBSCDV = "update bsc_donvi set thuchien = '" + kpi_detail[i].thuchien + "', thamdinh = '" + kpi_detail[i].thuchien + "' ";
                    sqlInsertBSCDV += "where donvigiao = '"+donvigiao+"' ";
                    sqlInsertBSCDV += "and donvinhan = '"+donvinhan+"' ";
                    sqlInsertBSCDV += "and thang = '" + thang + "' ";
                    sqlInsertBSCDV += "and nam = '" + nam + "' ";
                    sqlInsertBSCDV += "and kpi = '" + kpi_detail[i].kpi_id + "' ";
                    sqlInsertBSCDV += "and trangthaithamdinh = 0;";
                    sqlInsertBSCDV += "EXEC sp_ketquathuchien @thang = '" + thang + "',@nam = '" + nam + "', @donvigiao = '" + donvigiao + "', @donvinhan = '" + donvinhan + "', @kpi_id = '" + kpi_detail[i].kpi_id + "'";

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
            return isSuccess;
        }

        [WebMethod]
        public static bool savePhanHoi(int donvigiao, int donvinhan, int thang, int nam, int kpi_id, int kehoach_dexuat, string lydo_dexuat)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            try
            {
                string sqlUpdatePhanHoi = "update bsc_donvi set phanhoi_giao_dexuat = '" + kehoach_dexuat + "', phanhoi_giao_lydo = N'" + lydo_dexuat + "', phanhoi_giao_daxuly = 0 ";
                sqlUpdatePhanHoi += "where donvigiao = '" + donvigiao + "' ";
                sqlUpdatePhanHoi += "and donvinhan = '" + donvinhan + "' ";
                sqlUpdatePhanHoi += "and thang = '" + thang + "' ";
                sqlUpdatePhanHoi += "and nam = '" + nam + "' ";
                sqlUpdatePhanHoi += "and kpi = '" + kpi_id + "' ";
                sqlUpdatePhanHoi += "and trangthaithamdinh = 0;";
                sqlUpdatePhanHoi += "EXEC sp_ketquathuchien @thang = '" + thang + "',@nam = '" + nam + "', @donvigiao = '" + donvigiao + "', @donvinhan = '" + donvinhan + "', @kpi_id = '" + kpi_id + "'";

                try
                {
                    cnData.ThucThiDL(sqlUpdatePhanHoi);
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

        [WebMethod]
        public static bool savePhanHoiTD(int donvigiao, int donvinhan, int thang, int nam, int kpi_id, int thamdinh_dexuat, string thamdinh_lydo_dexuat)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            try
            {
                string sqlUpdatePhanHoi = "update bsc_donvi set phanhoi_thamdinh_dexuat = '" + thamdinh_dexuat + "', phanhoi_thamdinh_lydo = N'" + thamdinh_lydo_dexuat + "', phanhoi_thamdinh_daxuly = 0 ";
                sqlUpdatePhanHoi += "where donvigiao = '" + donvigiao + "' ";
                sqlUpdatePhanHoi += "and donvinhan = '" + donvinhan + "' ";
                sqlUpdatePhanHoi += "and thang = '" + thang + "' ";
                sqlUpdatePhanHoi += "and nam = '" + nam + "' ";
                sqlUpdatePhanHoi += "and kpi = '" + kpi_id + "' ";
                sqlUpdatePhanHoi += "and trangthaithamdinh = 1;";
                sqlUpdatePhanHoi += "EXEC sp_ketquathuchien @thang = '" + thang + "',@nam = '" + nam + "', @donvigiao = '" + donvigiao + "', @donvinhan = '" + donvinhan + "', @kpi_id = '" + kpi_id + "'";

                try
                {
                    cnData.ThucThiDL(sqlUpdatePhanHoi);
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

        [WebMethod]
        public static bool huyPhanHoi(int donvigiao, int donvinhan, int thang, int nam, int kpi_id)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            try
            {
                string sqlUpdatePhanHoi = "update bsc_donvi set phanhoi_giao_dexuat = null, phanhoi_giao_lydo = null, phanhoi_giao_daxuly = null ";
                sqlUpdatePhanHoi += "where donvigiao = '" + donvigiao + "' ";
                sqlUpdatePhanHoi += "and donvinhan = '" + donvinhan + "' ";
                sqlUpdatePhanHoi += "and thang = '" + thang + "' ";
                sqlUpdatePhanHoi += "and nam = '" + nam + "' ";
                sqlUpdatePhanHoi += "and kpi = '" + kpi_id + "' ";
                sqlUpdatePhanHoi += "and trangthaithamdinh = 0;";
                sqlUpdatePhanHoi += "EXEC sp_ketquathuchien @thang = '" + thang + "',@nam = '" + nam + "', @donvigiao = '" + donvigiao + "', @donvinhan = '" + donvinhan + "', @kpi_id = '" + kpi_id + "'";

                try
                {
                    cnData.ThucThiDL(sqlUpdatePhanHoi);
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

        [WebMethod]
        public static bool huyPhanHoiTD(int donvigiao, int donvinhan, int thang, int nam, int kpi_id)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            try
            {
                string sqlUpdatePhanHoi = "update bsc_donvi set phanhoi_thamdinh_dexuat = null, phanhoi_thamdinh_lydo = null, phanhoi_thamdinh_daxuly = null ";
                sqlUpdatePhanHoi += "where donvigiao = '" + donvigiao + "' ";
                sqlUpdatePhanHoi += "and donvinhan = '" + donvinhan + "' ";
                sqlUpdatePhanHoi += "and thang = '" + thang + "' ";
                sqlUpdatePhanHoi += "and nam = '" + nam + "' ";
                sqlUpdatePhanHoi += "and kpi = '" + kpi_id + "' ";
                sqlUpdatePhanHoi += "and trangthaithamdinh = 1;";
                sqlUpdatePhanHoi += "EXEC sp_ketquathuchien @thang = '" + thang + "',@nam = '" + nam + "', @donvigiao = '" + donvigiao + "', @donvinhan = '" + donvinhan + "', @kpi_id = '" + kpi_id + "'";

                try
                {
                    cnData.ThucThiDL(sqlUpdatePhanHoi);
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
            this.Title = "Chi tiết nhận bsc";
            if (!IsPostBack) {
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();

                    donvigiao = Request.QueryString["donvigiao"];
                    donvinhan = Request.QueryString["donvinhan"];
                    thang = Request.QueryString["thang"];
                    nam = Request.QueryString["nam"];

                    if (donvigiao == null || donvinhan == null || thang == null || nam == null || nhanvien.nhanvien_donvi_id != Convert.ToInt32(donvinhan))
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}