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

namespace VNPT_BSC.DanhMuc
{
    public partial class KPO : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public DataTable dtkpo;
        public static DataTable dtnhanvien_kpo = new DataTable();

        private DataTable getkpoList()
        {
            Nhanvien nhanvien = Session.GetCurrentUser();
            string sqlkpo = "select a.kpo_id,a.kpo_ten,a.kpo_mota,a.kpo_ngaytao,b.nhanvien_hoten from kpo a,nhanvien b where a.kpo_nguoitao = b.nhanvien_id and a.kpo_nguoitao = '" + nhanvien.nhanvien_id + "'";
            DataTable dtkpo = new DataTable();
            try
            {
                dtkpo = cn.XemDL(sqlkpo);
            }
            catch (Exception ex)
            {
                dtkpo = null;
            }
            return dtkpo;
        }

        [WebMethod]
        public static bool SaveData(string kpo_tenAprove, string kpo_motaAprove, string kpo_ngayAprove)
        {
            Page objp = new Page();
            Nhanvien nhanvien = objp.Session.GetCurrentUser();
            Connection kpo = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into kpo(kpo_ten,kpo_mota, kpo_ngaytao,kpo_nguoitao) values (N'" + kpo_tenAprove + "',N'" + kpo_motaAprove + "', '" + kpo_ngayAprove + "','" + nhanvien.nhanvien_id + "')";
                kpo.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(string kpo_ten_suaAprove, string kpo_mota_suaAprove, int kpo_id_suaAprove)
        {
            Page objp = new Page();
            Nhanvien nhanvien = objp.Session.GetCurrentUser();
            Connection kpo_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update kpo set kpo_ten = N'" + kpo_ten_suaAprove + "',kpo_mota = N'" + kpo_mota_suaAprove + "', kpo_nguoitao = '" + nhanvien.nhanvien_id + "' where kpo_id = '" + kpo_id_suaAprove + "'";
                kpo_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }
        [WebMethod]
        public static bool DeleteData(int kpo_id_xoaAprove)
        {
            Connection kpo_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete kpo where kpo_id = '" + kpo_id_xoaAprove + "'";
                kpo_delete.ThucThiDL(sqldeleteData);
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
            dtkpo = new DataTable();
            dtkpo = getkpoList();
        }
    }
}