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
    public partial class MauBSC : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public DataTable dtBSC;
        public DataTable dtKPI;
        public DataTable dtBSCNam;
        public DataTable dtDVT;
        public static int nguoitao;
        public class kpiDetail
        {
            public int kpi_id { get; set; }
            public int tytrong { get; set; }
            public string dvt { get; set; }
        }

        /*List đơn vị tính*/
        private DataTable dsDVT() {
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

        /*List BSC theo năm*/
        private DataTable dsBSCNam(int nguoitao)
        {
            DataTable dsBSC = new DataTable();
            string sqlBSCDuocGiao = "select nam from danhsachbsc where nguoitao = '" + nguoitao + "' group by nam order by nam DESC";
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
            catch (Exception ex){
                throw ex;
            }
            return dtKPI;
        }

        private DataTable getBSCList(int nguoitao) {
            string sqlBSC = "select thang,nam from danhsachbsc where nguoitao = '" + nguoitao + "' group by thang, nam order by nam,thang DESC";
            DataTable dtBSC = new DataTable();
            try {
                dtBSC = cn.XemDL(sqlBSC);
            }
            catch (Exception ex){
                throw ex;
            }
            return dtBSC;
        }

        [WebMethod]
        public static Dictionary<String, String>[] BindingCheckBox(int monthAprove, int yearAprove, int nguoitao)
        {
            DataTable dtKPI = new DataTable();
            Connection cnDanhSachBSC = new Connection();
            Dictionary<String, String>[] arrKPI = { };
            string sql = "select * from danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao = '" + nguoitao + "'";
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
                }
            }
            return arrKPI;
        }

        [WebMethod]
        public static bool SaveData(int monthAprove, int yearAprove, kpiDetail[] arrKPI_ID, int nguoitao)
        {
            Connection cnDanhSachBSC = new Connection();
            bool output = false;
            string sqlDelOldData = "delete danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao = '" + nguoitao + "'";
            string sqlInsertNewData = "";
            try
            {
                cnDanhSachBSC.ThucThiDL(sqlDelOldData);
                for (int i = 0; i < arrKPI_ID.Length; i++)
                {
                    int kpi_id = arrKPI_ID[i].kpi_id;
                    int tytrong = arrKPI_ID[i].tytrong;
                    string dvt = arrKPI_ID[i].dvt;
                    string curDate = DateTime.Now.ToString("yyyy-MM-dd");
                    sqlInsertNewData = "insert into danhsachbsc(thang, nam, kpi_id, nguoitao, bscduocgiao, ngaytao, donvitinh, tytrong) values('" + monthAprove + "', '" + yearAprove + "', '" + kpi_id + "', '" + nguoitao + "', '" + "" + "', '" + curDate + "', N'" + dvt + "', '" + tytrong + "')";
                    try
                    {
                        cnDanhSachBSC.ThucThiDL(sqlInsertNewData);
                    }
                    catch (Exception ex)
                    {
                        output = false;
                    }
                }
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack) {
                try
                {
                    Nhanvien nhanvien = new Nhanvien();
                    nhanvien = Session.GetCurrentUser();
                    /*Kiểm tra nếu không phải là chuyên viên BSC (id của chuyên viên BSC là 10) thì đẩy ra trang đăng nhập*/
                    if (nhanvien == null || nhanvien.nhanvien_chucvu_id != 10)
                    {
                        Response.Write("<script>alert('Bạn không được quyền truy cập vào trang này. Vui lòng đăng nhập lại!!!')</script>");
                        Response.Write("<script>window.location.href='../Login.aspx';</script>");
                    }
                    nguoitao = nhanvien.nhanvien_id;

                    /*Get list BSC*/
                    dtBSC = new DataTable();
                    dtBSC = getBSCList(nguoitao);

                    /*Get list KPI*/
                    dtKPI = new DataTable();
                    dtKPI = getKPIList(nguoitao);

                    /*Get list các năm của BSC*/
                    dtBSCNam = new DataTable();
                    dtBSCNam = dsBSCNam(nguoitao);

                    /*Get list DVT*/
                    dtDVT = new DataTable();
                    dtDVT = dsDVT();
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}