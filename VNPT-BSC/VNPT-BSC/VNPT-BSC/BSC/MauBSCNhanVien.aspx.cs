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

namespace VNPT_BSC.BSC
{
    public partial class MauBSCNhanVien : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public static DataTable dsBSCDV = new DataTable();
        public static DataTable dtKPI = new DataTable();
        public static DataTable dtBSC = new DataTable();
        public static DataTable dtBSCNam = new DataTable();
        public static DataTable dtDVT = new DataTable();
        public static DataTable dtNVTD = new DataTable();
        public static DataTable dtMauBSC = new DataTable();
        public static int nguoitao;
        public static int donvinhan;
        public class kpiDetail
        {
            public int kpi_id { get; set; }
            public int tytrong { get; set; }
            public string dvt { get; set; }
            public int nvtd { get; set; }
        }

        /*List loại mẫu bsc*/
        private DataTable dsMauBSC()
        {
            DataTable dsMauBSC = new DataTable();
            string sqlMauBSC = "select * from loaimaubsc";
            try
            {
                dsMauBSC = cn.XemDL(sqlMauBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsMauBSC;
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

        /*List nhân viên thẩm định*/
        private static DataTable dsNVTD(int donvi)
        {
            Connection cnNVTD = new Connection();
            DataTable dsNhanvienthamdinh = new DataTable();
            string sqlNVTD = "select * from nhanvien where nhanvien_id in (select nhanvien_id from nhanvien_chucvu where chucvu_id in (3,5)) and nhanvien_donvi = '" + donvi + "'";
            try
            {
                dsNhanvienthamdinh = cnNVTD.XemDL(sqlNVTD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsNhanvienthamdinh;
        }

        /*List BSC theo năm*/
        private DataTable dsBSCNam(int nguoitao)
        {
            DataTable dsBSC = new DataTable();
            string sqlBSCDuocGiao = "select nam from danhsachbsc where nguoitao = '" + nguoitao + "' and bscduocgiao != '' group by nam order by nam DESC";
            try
            {
                dsBSC = cn.XemDL(sqlBSCDuocGiao);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsBSC;
        }

        private DataTable dsBSCDuocGiao(int donvinhan)
        {
            DataTable dsBSC = new DataTable();
            string sqlBSCDuocGiao = "select top 24 thang,nam from giaobscdonvi where donvinhan = '" + donvinhan + "' and trangthaigiao = 1 group by thang, nam order by nam,thang DESC";
            try
            {
                dsBSC = cn.XemDL(sqlBSCDuocGiao);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsBSC;
        }

        /*Get KPI list*/
        private DataTable getKPIList(int nguoitao)
        {
            string sqlKPI = "select kpi.kpi_id, kpo.kpo_id, kpi.kpi_ten + ' (' + kpo.kpo_ten + ')' as name from kpi, kpo where kpi.kpi_thuoc_kpo = kpo.kpo_id and kpi.kpi_nguoitao = '" + nguoitao + "' order by kpo.kpo_id ASC";
            DataTable dtKPI = new DataTable();
            try
            {
                dtKPI = cn.XemDL(sqlKPI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtKPI;
        }

        private DataTable getBSCList(int nguoitao)
        {
            string sqlBSC = "select thang,nam,bscduocgiao from danhsachbsc where nguoitao = '" + nguoitao + "' and bscduocgiao != '' group by thang, nam,bscduocgiao order by nam,thang,bscduocgiao";
            DataTable dtBSC = new DataTable();
            try
            {
                dtBSC = cn.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtBSC;
        }

        [WebMethod]
        public static string loadKPIDuocGiao(int thang, int nam, int donvinhan)
        {
            Connection cnData = new Connection();
            DataTable dtKPIDuocGiao = new DataTable();
            DataTable dsDonvitinh = new DataTable();
            DataTable dsNhanvienthamdinh = new DataTable();
            string sqlDVT = "select * from donvitinh";
            string szOutput = "";
            string sql = "select kpi.kpi_id, kpo.kpo_id, kpi.kpi_ten + ' (' + kpo.kpo_ten + ')' as name, bscdv.donvitinh, bscdv.trongso ";
            sql += "from bsc_donvi bscdv, kpi, kpo ";
            sql += "where bscdv.thang = '" + thang + "' and bscdv.nam = '" + nam + "' and bscdv.donvinhan = '" + donvinhan + "' ";
            sql += "and bscdv.kpi = kpi.kpi_id ";
            sql += "and kpi.kpi_thuoc_kpo = kpo.kpo_id";
            try
            {
                dtKPIDuocGiao = cnData.XemDL(sql);
                dsDonvitinh = cnData.XemDL(sqlDVT);
                dsNhanvienthamdinh = dsNVTD(donvinhan);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            szOutput += "<table class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%' id='danhsachKPIDuocGiao'>";
            szOutput += "<thead>";
            szOutput += "<tr>";
            szOutput += "<th><input type='checkbox' id='checkall-kpiduocgiao' onclick='check_kpi_duocgiao()'/></th>";
            szOutput += "<th>KPI</th>";
            szOutput += "<th>ĐVT</th>";
            szOutput += "<th>Tỷ trọng (%)</th>";
            szOutput += "<th>Nhân viên thẩm định</th>";
            szOutput += "</tr>";
            szOutput += "</thead>";
            szOutput += "<tbody>";
            for (int i = 0; i < dtKPIDuocGiao.Rows.Count; i++)
            {
                szOutput += "<tr data-id='" + dtKPIDuocGiao.Rows[i]["kpi_id"].ToString() + "'>";
                szOutput += "<td><input name='checkbox-kpiduocgiao' id='kpi_id_" + dtKPIDuocGiao.Rows[i]["kpi_id"].ToString() + "' type='checkbox' checked value='" + dtKPIDuocGiao.Rows[i]["kpi_id"].ToString() + "' /></td>";
                szOutput += "<td>" + dtKPIDuocGiao.Rows[i]["name"].ToString() + "</td>";
                szOutput += "<td class='text-center'>";
                szOutput += "<select class='form-control' id='dvt_" + dtKPIDuocGiao.Rows[i]["kpi_id"].ToString() + "'>";
                for (int nDVT = 0; nDVT < dsDonvitinh.Rows.Count; nDVT++) {
                   szOutput += "<option value='" + dsDonvitinh.Rows[nDVT]["dvt_id"] + "'>" + dsDonvitinh.Rows[nDVT]["dvt_ten"] + "</option>";
                }
                szOutput += "</select>";
                szOutput += "</td>";
                szOutput += "<td class='text-center'><input type='text' class='form-control' id='tytrong_" + dtKPIDuocGiao.Rows[i]["kpi_id"].ToString() + "' size='2' value='" + dtKPIDuocGiao.Rows[i]["trongso"].ToString() + "'/></td>";
                szOutput += "<td class='text-center'>";
                szOutput += "<select class='form-control' id='nvtd_" + dtKPIDuocGiao.Rows[i]["kpi_id"].ToString() + "'>";
                for (int nNVTD = 0; nNVTD < dsNhanvienthamdinh.Rows.Count; nNVTD++)
                {
                   szOutput += "<option value='" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_id"] + "'>" + dsNhanvienthamdinh.Rows[nNVTD]["nhanvien_hoten"] + "</option>";
                }
                szOutput += "</select>";
                szOutput += "</td>";
                szOutput += "</tr>";
            }
            szOutput += "</tbody>";
            szOutput += "</table>";

            return szOutput;
        }

        [WebMethod]
        public static Dictionary<String, String>[] BindingCheckBox(int monthAprove, int yearAprove, int nguoitao, int maubsc)
        {
            DataTable dtKPI = new DataTable();
            Connection cnDanhSachBSC = new Connection();
            Dictionary<String, String>[] arrKPI = { };
            string sql = "select * from danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao = '" + nguoitao + "' and bscduocgiao != '' and maubsc = '" + maubsc + "'";
            dtKPI = cnDanhSachBSC.XemDL(sql);
            if (dtKPI.Rows.Count > 0)
            {
                arrKPI = new Dictionary<String, String>[dtKPI.Rows.Count];
                for (int i = 0; i < dtKPI.Rows.Count; i++)
                {
                    arrKPI[i] = new Dictionary<string, string>();
                    arrKPI[i].Add("kpi_id", dtKPI.Rows[i]["kpi_id"].ToString());
                    arrKPI[i].Add("tytrong", dtKPI.Rows[i]["tytrong"].ToString());
                    arrKPI[i].Add("donvitinh", dtKPI.Rows[i]["donvitinh"].ToString());
                    arrKPI[i].Add("nhanvienthamdinh", dtKPI.Rows[i]["nhanvienthamdinh"].ToString());
                }
            }
            return arrKPI;
        }

        private static DataTable dtDSBSC(int thang, int nam, int nguoitao) {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select bscduocgiao from danhsachbsc where thang = '" + thang + "' and nam = '" + nam + "' and nguoitao = '" + nguoitao + "' and bscduocgiao != '' group by bscduocgiao";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }
            return dtResult;
        }

        [WebMethod]
        public static bool SaveData(int monthAprove, int yearAprove, kpiDetail[] arrKPI_ID, int nguoitao, string bscduocgiao, int maubsc)
        {
            Connection cnDanhSachBSC = new Connection();
            DataTable dtKiemTraDSBSC = new DataTable();
            //Dictionary<String, String> dicOutput = new Dictionary<string, string>();
            bool bResult = false;
            string sqlDelOldData = "";
            string sqlInsertNewData = "";
            try
            {
                dtKiemTraDSBSC = dtDSBSC(monthAprove, yearAprove, nguoitao);
                if (dtKiemTraDSBSC.Rows.Count > 0) {
                    if (bscduocgiao == dtKiemTraDSBSC.Rows[0][0].ToString())
                    {
                        // Xóa chính nó và insert mới bản thân nó
                        sqlDelOldData = "delete danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao = '" + nguoitao + "' and bscduocgiao = '" + bscduocgiao + "' and maubsc = '" + maubsc + "'";
                    }
                    else { 
                        // Xóa hết mẫu
                        sqlDelOldData = "delete danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao = '" + nguoitao + "' and bscduocgiao != ''";
                    }
                    cnDanhSachBSC.ThucThiDL(sqlDelOldData);
                }

                for (int i = 0; i < arrKPI_ID.Length; i++)
                {
                    int kpi_id = arrKPI_ID[i].kpi_id;
                    int tytrong = arrKPI_ID[i].tytrong;
                    string dvt = arrKPI_ID[i].dvt;
                    int nhanvienthamdinh = arrKPI_ID[i].nvtd;
                    string curDate = DateTime.Now.ToString("yyyy-MM-dd");
                    sqlInsertNewData = "insert into danhsachbsc(thang, nam, kpi_id, nguoitao, bscduocgiao, ngaytao, donvitinh, tytrong, nhanvienthamdinh, maubsc) values('" + monthAprove + "', '" + yearAprove + "', '" + kpi_id + "', '" + nguoitao + "', '" + bscduocgiao + "', '" + curDate + "', N'" + dvt + "', '" + tytrong + "', '" + nhanvienthamdinh + "', '" + maubsc + "')";
                    try
                    {
                        cnDanhSachBSC.ThucThiDL(sqlInsertNewData);
                    }
                    catch (Exception ex)
                    {
                        cnDanhSachBSC.ThucThiDL(sqlDelOldData);
                        bResult = false;
                        break;
                    }
                }
                bResult = true;
            }
            catch
            {
                bResult = false;
            }
            return bResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Mẫu BSC";
            if (!IsPostBack) {
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
                    donvinhan = nhanvien.nhanvien_donvi_id;
                    nguoitao = nhanvien.nhanvien_id;

                    dsBSCDV = dsBSCDuocGiao(donvinhan);
                    dtKPI = getKPIList(nguoitao);
                    dtBSC = getBSCList(nguoitao);
                    dtBSCNam = dsBSCNam(nguoitao);
                    dtDVT = dsDVT();
                    dtNVTD = dsNVTD(donvinhan);
                    dtMauBSC = dsMauBSC();
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}