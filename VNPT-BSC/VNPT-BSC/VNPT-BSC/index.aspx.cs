using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

namespace VNPT_BSC
{
    public partial class index : System.Web.UI.Page
    {

        Connection cn = new Connection();
        public DataTable dtkpiindex;
        public DataTable dtthamdinhindex;
        public DataTable dtbscdonviindex;
        public static int donvi_id;
        public static int nhanvien_id;
        public static DataTable dtnhanvien_kpi = new DataTable();
        public static DataTable dtkpi = new DataTable();

        

        [WebMethod]
        public static Dictionary<String, String> loadBSCByYear(int thang, int nam, int nhanvien_donvi_id)
        {

            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string donvinhan = "";
            string outputHTML = "";
            string sqlBSC = "SELECT dvg.donvi_id as donvigiao,dvg.donvi_ten as dvg,dvn.donvi_id as donvinhan,dvn.donvi_ten as dvn,bsc_donvi.thang,bsc_donvi.nam,kpi.kpi_id,kpi.kpi_ten,dvg.donvi_id as donvithamdinh,dvtd.donvi_ten as dvtd,dvt.dvt_id,dvt.dvt_ten,bsc_donvi.trongso,bsc_donvi.kehoach,bsc_donvi.thuchien,bsc_donvi.thamdinh,bsc_donvi.trangthaithamdinh,bsc_donvi.kq_thuchien,bsc_donvi.diem_kpi,bsc_donvi.hethong_thuchien, danhsachbsc.stt ";
            sqlBSC += "  FROM bsc_donvi, donvi dvg, donvi dvn, kpi,donvi dvtd,donvitinh dvt, danhsachbsc ";
            sqlBSC += "  where bsc_donvi.donvigiao = dvg.donvi_id and bsc_donvi.donvinhan = dvn.donvi_id and bsc_donvi.donvithamdinh = dvtd.donvi_id and bsc_donvi.kpi = kpi.kpi_id and bsc_donvi.donvitinh = dvt.dvt_id and dvn.donvi_id = '" + nhanvien_donvi_id + "'";
            sqlBSC += "and bsc_donvi.nam = '" + nam + "' ";
            sqlBSC += "and bsc_donvi.thang = '" + thang + "' ";
            sqlBSC += "and bsc_donvi.thang = danhsachbsc.thang ";
            sqlBSC += "and bsc_donvi.nam = danhsachbsc.nam ";
            sqlBSC += "and bsc_donvi.loaimau = danhsachbsc.maubsc ";
            sqlBSC += "and bsc_donvi.kpi = danhsachbsc.kpi_id  ";
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
            outputHTML += "<table id='table-bsclist' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th class='no-sort'>STT</th>";
            outputHTML += "<th class='no-sort'>Đơn vị giao</th>";
            outputHTML += "<th class='no-sort'>Tên KPI</th>";
            outputHTML += "<th class='no-sort'>Đơn vị thẩm định</th>";
            outputHTML += "<th class='no-sort'>Đơn vị tính</th>";
            outputHTML += "<th class='no-sort'>Trọng số</th>";
            outputHTML += "<th class='no-sort'>Kế hoạch</th>";
            outputHTML += "<th class='no-sort'>Thực hiện</th>";
            outputHTML += "<th class='no-sort'>Thẩm định</th>";
            outputHTML += "<th class='no-sort'>Trạng thái thẩm định</th>";
            outputHTML += "<th class='no-sort'>Kết quả thực hiện</th>";
            outputHTML += "<th class='no-sort'>Điểm KPI</th>";
            //outputHTML += "<th>Kết quả HT</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='14' class='text-center'>No item</td></tr>";
            }
            else
            {
                double nTongSoDiem = 0;
                int nTongTrongSo = 0;
                for (int nIndex = 0; nIndex < gridData.Rows.Count; nIndex++)
                {
                    string dv_giao = gridData.Rows[nIndex]["donvigiao"].ToString();
                    donvinhan = gridData.Rows[nIndex]["dvn"].ToString();
                    string dv_kpi = gridData.Rows[nIndex]["kpi_id"].ToString();
                    string dv_thamdinh = gridData.Rows[nIndex]["donvithamdinh"].ToString();
                    string dv_dvt = gridData.Rows[nIndex]["dvt_id"].ToString();
                    string dv_trongso = gridData.Rows[nIndex]["trongso"].ToString();
                    string dv_kehoach = gridData.Rows[nIndex]["kehoach"].ToString();
                    string dv_thuchien = gridData.Rows[nIndex]["thuchien"].ToString();
                    string dv_kqthamdinh = gridData.Rows[nIndex]["thamdinh"].ToString();
                    string dv_trangthaithamdinh = gridData.Rows[nIndex]["trangthaithamdinh"].ToString();
                    string dv_ketqua = "0";
                    string dv_diem = "0";
                    if (Convert.ToDouble(dv_kqthamdinh) != 0)
                    {
                        dv_ketqua = gridData.Rows[nIndex]["kq_thuchien"].ToString();
                        dv_diem = gridData.Rows[nIndex]["diem_kpi"].ToString();
                    }

                    nTongTrongSo += Convert.ToInt32(dv_trongso);
                    nTongSoDiem += Convert.ToDouble(dv_diem);
                    string dv_kqht = gridData.Rows[nIndex]["hethong_thuchien"].ToString();
                    string txtTrangThaiThamDinh = "Chưa thẩm định";
                    string clsTrangThaiThamDinh = "label-default";



                    if (dv_trangthaithamdinh == "True")
                    {
                        txtTrangThaiThamDinh = "Đã thẩm định";
                        clsTrangThaiThamDinh = "label-success";
                    }

                 

                    outputHTML += "<tr>";
                    outputHTML += "<td class='text-center'>" + (nIndex + 1) + "</td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nIndex]["dvg"].ToString() + "</strong></td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nIndex]["kpi_ten"].ToString() + "</strong></td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nIndex]["dvtd"].ToString() + "</strong></td>";
                    outputHTML += "<td><strong>" + gridData.Rows[nIndex]["dvt_ten"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nIndex]["trongso"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nIndex]["kehoach"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nIndex]["thuchien"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + gridData.Rows[nIndex]["thamdinh"].ToString() + "</strong></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + txtTrangThaiThamDinh + "</span></td>";
                    outputHTML += "<td class='text-center'><strong>" + dv_ketqua + "</strong></td>";
                    outputHTML += "<td class='text-center'><strong>" + dv_diem + "</strong></td>";
                    //outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["hethong_thuchien"].ToString() + "</td>";
                    outputHTML += "</tr>";
                }

                outputHTML += "<tr>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center'><strong>Tỷ lệ thực hiện</strong></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center'><strong>" + nTongTrongSo + "%" + "</strong></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td></td>";
                outputHTML += "<td style='text-align: center'><strong>" + String.Format("{0:0.00}", (nTongSoDiem * 100)) + "%" + "</strong></td>";
                outputHTML += "</tr>";

            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSC", outputHTML);
            dicOutput.Add("donvinhan", donvinhan);
            return dicOutput;
        }


        [WebMethod]
        public static Dictionary<String, String> loadBSCnhanvienByYear(int thang, int nam, int nhanvien_id)
        {

            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select nvgiao.nhanvien_hoten as giao,nvgiao.nhanvien_id as giao_id,nvnhan.nhanvien_hoten as nhan,nvnhan.nhanvien_id as nhan_id,a.thang,a.nam,b.kpi_id,b.kpi_ten as tenkpi,a.nhanvienthamdinh,nvthamdinh.nhanvien_hoten as nvtd,a.donvitinh,c.dvt_ten,a.trongso,a.kehoach,a.thuchien,a.thamdinh,a.trangthaithamdinh " +
                            "from bsc_nhanvien a,nhanvien nvnhan, nhanvien nvgiao, kpi b,donvitinh c,nhanvien nvthamdinh " +
                            "where a.nhanviengiao = nvgiao.nhanvien_id and a.nhanviennhan = nvnhan.nhanvien_id and a.nhanvienthamdinh = nvthamdinh.nhanvien_id and b.kpi_id = a.kpi and a.donvitinh = c.dvt_id and a.nhanviennhan = '" + nhanvien_id + "' and a.nam = '" + nam + "' and a.thang = '" + thang + "'";
     
            
            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-bscnhanvienlist' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th>STT</th>";
            outputHTML += "<th>Nhân viên giao</th>";
            outputHTML += "<th>Tên KPI</th>";
            outputHTML += "<th>Nhân viên thẩm định</th>";
            outputHTML += "<th>Đơn vị tính</th>";
            outputHTML += "<th>Trọng số</th>";
            outputHTML += "<th>Kế hoạch</th>";
            outputHTML += "<th>Thực hiện</th>";
            outputHTML += "<th>Thẩm định</th>";
            outputHTML += "<th>Trạng thái thẩm định</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='10' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nIndex = 0; nIndex < gridData.Rows.Count; nIndex++)
                {
                    string dv_giao = gridData.Rows[nIndex]["giao_id"].ToString();
                    string dv_kpi = gridData.Rows[nIndex]["kpi_id"].ToString();
                    string dv_thamdinh = gridData.Rows[nIndex]["nhanvienthamdinh"].ToString();
                    string dv_dvt = gridData.Rows[nIndex]["donvitinh"].ToString();
                    string dv_trongso = gridData.Rows[nIndex]["trongso"].ToString();
                    string dv_kehoach = gridData.Rows[nIndex]["kehoach"].ToString();
                    string dv_thuchien = gridData.Rows[nIndex]["thuchien"].ToString();
                    string dv_kqthamdinh = gridData.Rows[nIndex]["thamdinh"].ToString();
                    string dv_trangthaithamdinh = gridData.Rows[nIndex]["trangthaithamdinh"].ToString();
                    string txtTrangThaiThamDinh = "Chưa thẩm định";
                    string clsTrangThaiThamDinh = "label-default";



                    if (dv_trangthaithamdinh == "True")
                    {
                        txtTrangThaiThamDinh = "Đã thẩm định";
                        clsTrangThaiThamDinh = "label-success";
                    }



                    outputHTML += "<tr>";
                    outputHTML += "<td class='text-center'>" + (nIndex + 1) + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["giao"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tenkpi"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["nvtd"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["dvt_ten"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["trongso"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["kehoach"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["thuchien"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["thamdinh"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiThamDinh + "'>" + txtTrangThaiThamDinh + "</span></td>";
                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSCnhanvien", outputHTML);
            return dicOutput;
        }


        [WebMethod]
        public static Dictionary<String, String> loadBSCnhanvien_thamdinhByYear(int thang, int nam, int nhanvien_id)
        {

            Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string outputHTML = "";
            string sqlBSC = "select nvgiao.nhanvien_id as nhanviengiao, nvgiao.nhanvien_hoten as tennvg, nvnhan.nhanvien_id as nhanviennhan, nvnhan.nhanvien_hoten as tennvn, giaobsc.nam, giaobsc.thang, giaobsc.trangthaicham, giaobsc.trangthaidongy_kqtd, giaobsc.trangthaiketthuc,  ";
            sqlBSC += "(select COUNT(*) from bsc_nhanvien where bsc_nhanvien.nhanvienthamdinh = '" + nhanvien_id + "' and bsc_nhanvien.trangthaithamdinh = 0 and bsc_nhanvien.nam = '"+nam+"' and bsc_nhanvien.thang = giaobsc.thang) as sl_chuatd ";
            sqlBSC += "from giaobscnhanvien giaobsc, bsc_nhanvien giaobsc_nv, nhanvien nvgiao, nhanvien nvnhan ";
            sqlBSC += "where giaobsc.thang = giaobsc_nv.thang ";
            sqlBSC += "and giaobsc.nam = giaobsc_nv.nam ";
            sqlBSC += "and giaobsc.nhanviengiao = giaobsc_nv.nhanviengiao ";
            sqlBSC += "and giaobsc.nhanviennhan = giaobsc_nv.nhanviennhan ";
            sqlBSC += "and giaobsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and giaobsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and giaobsc.nam = '"+nam+"'";
            sqlBSC += "and giaobsc.thang = '" + thang + "'";
            sqlBSC += "and giaobsc_nv.nhanvienthamdinh = '" + nhanvien_id + "' ";
            sqlBSC += "group by nvgiao.nhanvien_id, nvgiao.nhanvien_hoten, nvnhan.nhanvien_id, nvnhan.nhanvien_hoten, giaobsc.nam, giaobsc.thang, giaobsc.trangthaicham, giaobsc.trangthaidongy_kqtd, giaobsc.trangthaiketthuc";

            try
            {
                gridData = cnBSC.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            outputHTML += "<div class='table-responsive padding-top-10'>";
            outputHTML += "<table id='table-bscnhanvienthamdinh' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
            outputHTML += "<thead>";
            outputHTML += "<tr>";
            outputHTML += "<th>STT</th>";
            outputHTML += "<th>Nhân viên giao</th>";
            outputHTML += "<th>Nhân viên nhận</th>";
            outputHTML += "<th>Trạng thái chấm</th>";
            outputHTML += "<th>Trạng thái đồng ý KQKĐ</th>";
            outputHTML += "<th>Trạng thái kết thúc</th>";
            outputHTML += "<th>Số lượng KPI chưa thẩm định</th>";
            outputHTML += "</tr>";
            outputHTML += "</thead>";
            outputHTML += "<tbody>";
            if (gridData.Rows.Count <= 0)
            {
                outputHTML += "<tr><td colspan='7' class='text-center'>No item</td></tr>";
            }
            else
            {
                for (int nIndex = 0; nIndex < gridData.Rows.Count; nIndex++)
                {
                    string dv_giao = gridData.Rows[nIndex]["nhanviengiao"].ToString();
                    string dv_kpi = gridData.Rows[nIndex]["nhanviennhan"].ToString();
                    string dv_trangthaicham = gridData.Rows[nIndex]["trangthaicham"].ToString();
                    string dv_trangthaidongy = gridData.Rows[nIndex]["trangthaidongy_kqtd"].ToString();
                    string dv_trangthaiketthuc = gridData.Rows[nIndex]["trangthaiketthuc"].ToString();
                    string dv_sl = gridData.Rows[nIndex]["sl_chuatd"].ToString();
                    string txtTrangThaicham = "Chưa chấm";
                    string clsTrangThaicham = "label-default";
                    string txtTrangThaidongy = "Chưa đồng ý";
                    string clsTrangThaidongy = "label-default";
                    string txtTrangThaiketthuc = "Chưa kết thúc";
                    string clsTrangThaiketthuc = "label-default";


                    if (dv_trangthaicham == "True")
                    {
                        txtTrangThaicham = "Đã thẩm định";
                        clsTrangThaicham = "label-success";
                    }

                    if (dv_trangthaidongy == "True")
                    {
                        txtTrangThaidongy = "Đã đồng ý";
                        clsTrangThaidongy = "label-success";
                    }
                    if (dv_trangthaiketthuc == "True")
                    {
                        txtTrangThaiketthuc = "Đã kết thúc";
                        clsTrangThaiketthuc = "label-success";
                    }



                    outputHTML += "<tr>";
                    outputHTML += "<td class='text-center'>" + (nIndex + 1) + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tennvg"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["tennvn"].ToString() + "</td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaicham + "'>" + txtTrangThaicham + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaidongy + "'>" + txtTrangThaidongy + "</span></td>";
                    outputHTML += "<td class='text-center'><span class='label " + clsTrangThaiketthuc + "'>" + txtTrangThaiketthuc + "</span></td>";
                    outputHTML += "<td class='text-center'>" + gridData.Rows[nIndex]["sl_chuatd"].ToString() + "</td>";
                    outputHTML += "</tr>";
                }
            }
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            dicOutput.Add("gridBSCnhanvienthamdinh", outputHTML);
            return dicOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Trang chủ";
            if (!IsPostBack)
            {
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();

                    if (nhanvien == null)
                    {
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }

                    nhanvien_id = nhanvien.nhanvien_id;
                    donvi_id = nhanvien.nhanvien_donvi_id;

                    try
                    {
                        string sqlnhanvien = "select * from nhanvien";
                        string sqlkpi = "select * from kpi";

                        dtnhanvien_kpi = cn.XemDL(sqlnhanvien);
                        dtkpi = cn.XemDL(sqlkpi);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}