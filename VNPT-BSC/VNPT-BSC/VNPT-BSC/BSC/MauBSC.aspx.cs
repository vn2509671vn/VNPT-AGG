﻿using System;
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
        public DataTable dtDVTD;
        public static int nguoitao;
        public class kpiDetail
        {
            public int kpi_id { get; set; }
            public int tytrong { get; set; }
            public int dvt { get; set; }
            public int dvtd { get; set; }
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

        /*List đơn vị thẩm định*/
        private DataTable dsDVTD()
        {
            DataTable dsDonvithamdinh = new DataTable();
            string sqlDVTD = "select * from donvi";
            try
            {
                dsDonvithamdinh = cn.XemDL(sqlDVTD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDonvithamdinh;
        }

        /*List BSC theo năm*/
        private DataTable dsBSCNam()
        {
            DataTable dsBSC = new DataTable();
            string sqlBSCDuocGiao = "select nam from danhsachbsc where nguoitao ";
            sqlBSCDuocGiao += "in (select nhanvien.nhanvien_id from nhanvien, chucvu, nhanvien_chucvu, quyen_cv ";
            sqlBSCDuocGiao += "where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id ";
            sqlBSCDuocGiao += "and chucvu.chucvu_id = nhanvien_chucvu.chucvu_id ";
            sqlBSCDuocGiao += "and chucvu.chucvu_id = quyen_cv.chucvu_id ";
            sqlBSCDuocGiao += "and quyen_cv.quyen_id = 2) group by nam order by nam DESC";

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
        private DataTable getKPIList()
        {
            string sqlKPI = "select kpi.kpi_id, kpo.kpo_id, kpi.kpi_ten + ' (' + kpo.kpo_ten + ')' as name from kpi, kpo where kpi.kpi_thuoc_kpo = kpo.kpo_id and kpi.kpi_nguoitao ";
            sqlKPI += "in (select nhanvien.nhanvien_id from nhanvien, chucvu, nhanvien_chucvu, quyen_cv ";
            sqlKPI += "where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id ";
            sqlKPI += "and chucvu.chucvu_id = nhanvien_chucvu.chucvu_id ";
            sqlKPI += "and chucvu.chucvu_id = quyen_cv.chucvu_id ";
            sqlKPI += "and quyen_cv.quyen_id = 2) order by kpo.kpo_id ASC";

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

        private DataTable getBSCList() {
            string sqlBSC = "select thang,nam from danhsachbsc where nguoitao ";
            sqlBSC += "in (select nhanvien.nhanvien_id from nhanvien, chucvu, nhanvien_chucvu, quyen_cv ";
            sqlBSC += "where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id ";
            sqlBSC += "and chucvu.chucvu_id = nhanvien_chucvu.chucvu_id ";
            sqlBSC += "and chucvu.chucvu_id = quyen_cv.chucvu_id ";
            sqlBSC += "and quyen_cv.quyen_id = 2) group by thang, nam order by nam,thang DESC";
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
        public static Dictionary<String, String>[] BindingCheckBox(int monthAprove, int yearAprove)
        {
            DataTable dtKPI = new DataTable();
            Connection cnDanhSachBSC = new Connection();
            Dictionary<String, String>[] arrKPI = { };
            string sql = "select * from danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao ";
            sql += "in (select nhanvien.nhanvien_id from nhanvien, chucvu, nhanvien_chucvu, quyen_cv ";
            sql += "where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id ";
            sql += "and chucvu.chucvu_id = nhanvien_chucvu.chucvu_id ";
            sql += "and chucvu.chucvu_id = quyen_cv.chucvu_id ";
            sql += "and quyen_cv.quyen_id = 2)";

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
                    arrKPI[i].Add("donvithamdinh", dtKPI.Rows[i]["donvithamdinh"].ToString());
                }
            }
            return arrKPI;
        }

        [WebMethod]
        public static bool SaveData(int monthAprove, int yearAprove, kpiDetail[] arrKPI_ID, int nguoitao)
        {
            Connection cnDanhSachBSC = new Connection();
            bool output = false;
            string sqlDelOldData = "delete danhsachbsc where thang = '" + monthAprove + "' and nam = '" + yearAprove + "' and nguoitao ";
            sqlDelOldData += "in (select nhanvien.nhanvien_id from nhanvien, chucvu, nhanvien_chucvu, quyen_cv ";
            sqlDelOldData += "where nhanvien.nhanvien_id = nhanvien_chucvu.nhanvien_id ";
            sqlDelOldData += "and chucvu.chucvu_id = nhanvien_chucvu.chucvu_id ";
            sqlDelOldData += "and chucvu.chucvu_id = quyen_cv.chucvu_id ";
            sqlDelOldData += "and quyen_cv.quyen_id = 2) ";
            sqlDelOldData += "and bscduocgiao = '' ";

            string sqlInsertNewData = "";
            try
            {
                cnDanhSachBSC.ThucThiDL(sqlDelOldData);
                for (int i = 0; i < arrKPI_ID.Length; i++)
                {
                    int kpi_id = arrKPI_ID[i].kpi_id;
                    int tytrong = arrKPI_ID[i].tytrong;
                    int dvt = arrKPI_ID[i].dvt;
                    int dvtd = arrKPI_ID[i].dvtd;
                    string curDate = DateTime.Now.ToString("yyyy-MM-dd");
                    sqlInsertNewData = "insert into danhsachbsc(thang, nam, kpi_id, nguoitao, bscduocgiao, ngaytao, donvitinh, tytrong, donvithamdinh) values('" + monthAprove + "', '" + yearAprove + "', '" + kpi_id + "', '" + nguoitao + "', '" + "" + "', '" + curDate + "', '" + dvt + "', '" + tytrong + "', '"+dvtd+"')";
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
                    nguoitao = nhanvien.nhanvien_id;

                    /*Get list BSC*/
                    dtBSC = new DataTable();
                    dtBSC = getBSCList();

                    /*Get list KPI*/
                    dtKPI = new DataTable();
                    dtKPI = getKPIList();

                    /*Get list các năm của BSC*/
                    dtBSCNam = new DataTable();
                    dtBSCNam = dsBSCNam();

                    /*Get list DVT*/
                    dtDVT = new DataTable();
                    dtDVT = dsDVT();

                    /*Get list DVTD*/
                    dtDVTD = new DataTable();
                    dtDVTD = dsDVTD();
                }
                catch {
                    Response.Write("<script>window.location.href='../Login.aspx';</script>");
                }
            }
        }
    }
}