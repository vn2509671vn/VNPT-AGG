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
    public partial class ChiTietBSCKiemDinh : System.Web.UI.Page
    {
        public static string donvigiao, donvinhan, donvithamdinh, thang, nam;
        public class kpiDetail
        {
            public int kpi_id { get; set; }
            public decimal thamdinh { get; set; }
        }

        [WebMethod]
        public static Dictionary<String, String> loadBSCByCondition(int donvigiao, int donvinhan, int thang, int nam, int donvithamdinh)
        {
            //string[] arrOutput = {};
            Dictionary<String, String> dicOutput = new Dictionary<String, String>(); // Lưu bảng BSC (gridBSC), đơn vị thẩm định, trạng thái giao, trạng thái nhận, trạng thái thẩm định
            Connection cnBSC = new Connection();
            int soluong_kpi_dathamdinh = 0;
            string szSL_kpi_dathamdinh = "";

            /*Lấy danh sách các thông tin còn lại ở bảng giaobscdonvi*/
            DataTable dtGiaoBSCDV = new DataTable();
            string sqlGiaoBSCDV = "select giaobscdonvi.*, donvi.donvi_ten  from giaobscdonvi, donvi ";
            sqlGiaoBSCDV += "where giaobscdonvi.donvigiao = '" + donvigiao + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.donvinhan = '" + donvinhan + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.thang = '" + thang + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.nam = '" + nam + "' ";
            sqlGiaoBSCDV += "and giaobscdonvi.donvinhan = donvi.donvi_id ";

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
                dicOutput.Add("donvinhan_ten", dtGiaoBSCDV.Rows[0]["donvi_ten"].ToString());
                dicOutput.Add("thang", dtGiaoBSCDV.Rows[0]["thang"].ToString());
                dicOutput.Add("nam", dtGiaoBSCDV.Rows[0]["nam"].ToString());
                dicOutput.Add("trangthaigiao", dtGiaoBSCDV.Rows[0]["trangthaigiao"].ToString());
                dicOutput.Add("trangthainhan", dtGiaoBSCDV.Rows[0]["trangthainhan"].ToString());
                dicOutput.Add("trangthaicham", dtGiaoBSCDV.Rows[0]["trangthaicham"].ToString());
                dicOutput.Add("trangthaidongy_kqtd", dtGiaoBSCDV.Rows[0]["trangthaidongy_kqtd"].ToString());
                dicOutput.Add("trangthaiketthuc", dtGiaoBSCDV.Rows[0]["trangthaiketthuc"].ToString());
            }
            else
            {
                dicOutput.Add("donvigiao", donvigiao.ToString());
                dicOutput.Add("donvinhan", donvinhan.ToString());
                dicOutput.Add("donvinhan_ten", "");
                dicOutput.Add("thang", "0");
                dicOutput.Add("nam", "0");
                dicOutput.Add("trangthaigiao", "0");
                dicOutput.Add("trangthainhan", "0");
                dicOutput.Add("trangthaicham", "0");
                dicOutput.Add("trangthaidongy_kqtd", "0");
                dicOutput.Add("trangthaiketthuc", "0");
            }

            /*Lấy danh sách BSC từ bảng bsc_donvi*/
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select bsc.thang, bsc.nam, kpi.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten, dvt.dvt_ten as donvitinh, bsc.trongso, bsc.kehoach, bsc.thuchien, bsc.thamdinh, bsc.trangthaithamdinh, bsc.phanhoi_thamdinh_dexuat, bsc.phanhoi_thamdinh_lydo, bsc.phanhoi_thamdinh_daxuly, danhsachbsc.stt ";
            sqlBSC += "from bsc_donvi bsc, kpi, kpo, donvi dvgiao, donvi dvnhan, donvitinh dvt, danhsachbsc ";
            sqlBSC += "where bsc.kpi = kpi.kpi_id ";
            sqlBSC += "and bsc.donvitinh = dvt.dvt_id ";
            sqlBSC += "and bsc.donvigiao = dvgiao.donvi_id ";
            sqlBSC += "and bsc.donvinhan = dvnhan.donvi_id ";
            sqlBSC += "and bsc.donvinhan = '" + donvinhan + "' ";
            sqlBSC += "and bsc.donvigiao = '" + donvigiao + "' ";
            sqlBSC += "and bsc.donvithamdinh = '" + donvithamdinh + "' ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = '" + thang + "' and bsc.nam = '" + nam + "' ";
            sqlBSC += "and bsc.thang = danhsachbsc.thang ";
            sqlBSC += "and bsc.nam = danhsachbsc.nam ";
            sqlBSC += "and bsc.loaimau = danhsachbsc.maubsc ";
            sqlBSC += "and bsc.kpi = danhsachbsc.kpi_id  ";
            sqlBSC += "and danhsachbsc.bscduocgiao = '' ORDER BY danhsachbsc.stt ASC ";

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
            outputHTML += "<th class='text-center'>STT</th>";
            outputHTML += "<th class='text-center'>Chỉ tiêu</th>";
            outputHTML += "<th class='text-center'>Tỷ trọng (%)</th>";
            outputHTML += "<th class='text-center'>ĐVT</th>";
            outputHTML += "<th class='text-center'>Chỉ tiêu</th>";
            outputHTML += "<th class='text-center'>Thực hiện</th>";
            outputHTML += "<th class='text-center'>Thẩm định</th>";
            if (dicOutput["trangthaidongy_kqtd"] == "False")
            {
                outputHTML += "<th>Chức năng</th>";
            }
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                if (dicOutput["trangthaidongy_kqtd"] == "False")
                {
                    outputHTML += "<tr><td colspan='8' class='text-center'>No item</td></tr>";
                }
                else
                {
                    outputHTML += "<tr><td colspan='7' class='text-center'>No item</td></tr>";
                }
            }
            else
            {
                for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++)
                {
                    string trangthai_xuly_phanhoi = "";
                    string kpi_id = gridData.Rows[nKPI]["kpi_id"].ToString();
                    string kpi_ten = gridData.Rows[nKPI]["kpi_ten"].ToString();
                    string thamdinh_duocgiao = gridData.Rows[nKPI]["thamdinh"].ToString();
                    string thamdinh_dexuat = gridData.Rows[nKPI]["phanhoi_thamdinh_dexuat"].ToString();
                    string lydo_dexuat = gridData.Rows[nKPI]["phanhoi_thamdinh_lydo"].ToString();
                    string daxuly_dexuat = gridData.Rows[nKPI]["phanhoi_thamdinh_daxuly"].ToString();
                    string dataTmp = kpi_id + "-" + kpi_ten + "-" + thamdinh_duocgiao + "-" + thamdinh_dexuat + "-" + lydo_dexuat + "-" + daxuly_dexuat;

                    if (gridData.Rows[nKPI]["phanhoi_thamdinh_daxuly"].ToString() == "True")
                    {
                        trangthai_xuly_phanhoi = "cls_daxuly";
                    }
                    else if (gridData.Rows[nKPI]["phanhoi_thamdinh_daxuly"].ToString() == "False")
                    {
                        trangthai_xuly_phanhoi = "cls_chuaxuly";
                    }

                    if (gridData.Rows[nKPI]["trangthaithamdinh"].ToString() == "True")
                    {
                        soluong_kpi_dathamdinh += 1;
                    }

                    outputHTML += "<tr class='" + trangthai_xuly_phanhoi + "' data-id='" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'>";
                    outputHTML += "<input type='hidden' value='" + dataTmp + "' id='idPhanHoi_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "'/>";
                    outputHTML += "<td class='text-center'>" + (nKPI + 1) + "</td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["trongso"].ToString() + "</strong></td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nKPI]["donvitinh"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["kehoach"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nKPI]["thuchien"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><input type='text' class='form-control' name='thamdinh' id='thamdinh_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' value='" + gridData.Rows[nKPI]["thamdinh"].ToString() + "' onkeypress='return onlyNumbers(event.charCode || event.keyCode);'/></td>";

                    // Chức năng
                    outputHTML += "<td class='text-center'>";
                    if (daxuly_dexuat != "")
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

            szSL_kpi_dathamdinh = soluong_kpi_dathamdinh.ToString() + "/" + gridData.Rows.Count.ToString();
            dicOutput.Add("soluong_kpi_dathamdinh", szSL_kpi_dathamdinh);

            return dicOutput;
        }

        [WebMethod]
        public static bool updateKiemDinhStatus(int donvigiao, int donvinhan, int thang, int nam, int donvithamdinh)
        {
            Connection cnNhanBSC = new Connection();
            Message msg = new Message();
            string szMsgContent = "BSC cua don vi da duoc tham dinh!!! Ban vui long vao kiem tra lai.";
            bool isSuccess = false;

            string sqlGiaoBSC = "update bsc_donvi set trangthaithamdinh = 1 where donvigiao = '" + donvigiao + "' and donvinhan = '" + donvinhan + "' and thang = '" + thang + "' and nam = '" + nam + "' and donvithamdinh = '" + donvithamdinh + "'";
            try
            {
                cnNhanBSC.ThucThiDL(sqlGiaoBSC);
                msg.SendSMS_ByIDDV(donvinhan, szMsgContent);
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
                    string sqlInsertBSCDV = "update bsc_donvi set thamdinh = '" + kpi_detail[i].thamdinh + "' ";
                    sqlInsertBSCDV += "where donvigiao = '" + donvigiao + "' ";
                    sqlInsertBSCDV += "and donvinhan = '" + donvinhan + "' ";
                    sqlInsertBSCDV += "and thang = '" + thang + "' ";
                    sqlInsertBSCDV += "and nam = '" + nam + "' ";
                    sqlInsertBSCDV += "and kpi = '" + kpi_detail[i].kpi_id + "'";
                    //sqlInsertBSCDV += "EXEC sp_ketquathuchien @thang = '" + thang + "',@nam = '" + nam + "', @donvigiao = '" + donvigiao + "', @donvinhan = '" + donvinhan + "', @kpi_id = '" + kpi_detail[i].kpi_id + "'";
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
        public static bool xulyPhanHoi(int donvigiao, int donvinhan, int thang, int nam, int kpi_id, int thamdinh_cuoicung)
        {
            bool isSuccess = false;
            Connection cnData = new Connection();
            Message msg = new Message();
            string szMsgContent = "Phan hoi ve viec tham dinh BSC da duoc xu ly!!! Ban vui long vao kiem tra lai.";
            try
            {
                string sqlUpdatePhanHoi = "update bsc_donvi set thamdinh = '" + thamdinh_cuoicung + "', phanhoi_thamdinh_daxuly = 1 ";
                sqlUpdatePhanHoi += "where donvigiao = '" + donvigiao + "' ";
                sqlUpdatePhanHoi += "and donvinhan = '" + donvinhan + "' ";
                sqlUpdatePhanHoi += "and thang = '" + thang + "' ";
                sqlUpdatePhanHoi += "and nam = '" + nam + "' ";
                sqlUpdatePhanHoi += "and kpi = '" + kpi_id + "' ";
                sqlUpdatePhanHoi += "and trangthaithamdinh = 1";
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
            this.Title = "Chi tiết kiểm định";
            if (!IsPostBack)
            {
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();

                    donvigiao = Request.QueryString["donvigiao"];
                    donvinhan = Request.QueryString["donvinhan"];
                    donvithamdinh = Request.QueryString["donvithamdinh"];
                    thang = Request.QueryString["thang"];
                    nam = Request.QueryString["nam"];

                    if (donvigiao == null || donvinhan == null || thang == null || nam == null || donvithamdinh == null || nhanvien.nhanvien_donvi_id != Convert.ToInt32(donvithamdinh))
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