using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using DevExpress.Web.ASPxEditors;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

namespace VNPT_BSC.DanhMuc
{
    public partial class BSC : System.Web.UI.Page
    {
   
        Connection cn = new Connection();
        public DataTable dtkpi;
        public static DataTable dtnhanvien_kpi = new DataTable();
        public static DataTable dtkpi_kpo = new DataTable();
       

        private DataTable getkpiList()
        {
            Nhanvien nhanvien = Session.GetCurrentUser();
            string sqlkpi = "select a.kpi_id,a.kpi_ten,a.kpi_mota,a.kpi_ngaytao,c.nhanvien_hoten,b.kpo_ten,b.kpo_id,c.nhanvien_id from kpi a,kpo b,nhanvien c where a.kpi_nguoitao = c.nhanvien_id and a.kpi_thuoc_kpo = b.kpo_id and a.kpi_nguoitao = '"+nhanvien.nhanvien_id+"'";
            DataTable dtkpi = new DataTable();
            try
            {
                dtkpi = cn.XemDL(sqlkpi);
            }
            catch (Exception ex)
            {
                dtkpi = null;
            }
            return dtkpi;
        }

        [WebMethod]
        public static bool SaveData(string kpi_tenAprove, string kpi_motaAprove, string kpi_ngayAprove, int kpi_kpoAprove)
        {
            Page objp = new Page();
            Nhanvien nhanvien = objp.Session.GetCurrentUser();
            Connection kpi = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {


                sqlInsertNewData = "insert into kpi(kpi_ten,kpi_mota, kpi_ngaytao,kpi_nguoitao,kpi_thuoc_kpo) values('" + kpi_tenAprove + "','" + kpi_motaAprove + "', '" + kpi_ngayAprove + "','" + nhanvien.nhanvien_id + "','" + kpi_kpoAprove + "')";
                try
                {
                    kpi.ThucThiDL(sqlInsertNewData);
                }
                catch
                {
                    output = false;
                }
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(string kpi_ten_suaAprove, string kpi_mota_suaAprove,  string kpi_kpo_suaAprove, int kpi_id_suaAprove)
        {
            Page objp = new Page();
            Nhanvien nhanvien = objp.Session.GetCurrentUser();
            Connection kpi_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update kpi set kpi_ten = '" + kpi_ten_suaAprove + "',kpi_mota = '" + kpi_mota_suaAprove + "', kpi_nguoitao = '" + nhanvien.nhanvien_id + "', kpi_thuoc_kpo = '" + kpi_kpo_suaAprove + "' where kpi_id = '" + kpi_id_suaAprove + "'";
                try
                {
                    kpi_edit.ThucThiDL(sqlUpdateData);
                }
                catch
                {
                    output = false;
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

            
            if (!IsPostBack)
            {
                Nhanvien nhanvien = Session.GetCurrentUser();
                dtkpi = new DataTable();
                dtkpi = getkpiList();
                try
                {
                    string sqlnhanvien = "select * from nhanvien";
                    string sqlkpo = "select * from kpo";

                    dtnhanvien_kpi = cn.XemDL(sqlnhanvien);
                    dtkpi_kpo = cn.XemDL(sqlkpo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}