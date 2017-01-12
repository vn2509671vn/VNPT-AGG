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
    public partial class nhanvienchucvu : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public DataTable dtnv_cv;
        public static DataTable dtchucvu = new DataTable();
        public static DataTable dtnhanvien = new DataTable();
   

        private DataTable getnv_cvList()
        {

            string sqlkpo = "select b.nhanvien_id,a.nhanvien_hoten,c.chucvu_ten,b.chucvu_id from nhanvien a,nhanvien_chucvu b, chucvu c where a.nhanvien_id = b.nhanvien_id and c.chucvu_id = b.chucvu_id";
            DataTable dtnv_cv = new DataTable();
            try
            {
                dtnv_cv = cn.XemDL(sqlkpo);
            }
            catch (Exception ex)
            {
                dtnv_cv = null;
            }
            return dtnv_cv;
        }
        [WebMethod]
        public static bool SaveData(string nv_idA, int cv_idA)
        {
            int nhanviennhan = 0;
            nhanviennhan = getNhanVienID(nv_idA);
            Connection gan_chucvu = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into nhanvien_chucvu(nhanvien_id,chucvu_id) values('" + nhanviennhan + "','" + cv_idA + "')";
                gan_chucvu.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(int nv_id_oldA, int cv_id_oldA, int nv_idA, int cv_idA)
        {
            Connection gan_chucvu_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update nhanvien_chucvu set nhanvien_id = '" + nv_idA + "',chucvu_id = '" + cv_idA + "' where nhanvien_id = '" + nv_id_oldA + "' and chucvu_id = '" + cv_id_oldA + "'";
                gan_chucvu_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }


        [WebMethod]
        public static bool DeleteData(int cv_idA, int nv_idA)
        {
            Connection nv_cv_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete nhanvien_chucvu where nhanvien_id = '" + nv_idA + "' and chucvu_id = '" + cv_idA + "'";
                nv_cv_delete.ThucThiDL(sqldeleteData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }
        private static int getNhanVienID(string taikhoan)
        {
            int id_nhanviennhan = 0;
            for (int nIndex = 0; nIndex < dtnhanvien.Rows.Count; nIndex++)
            {
                string taikhoanTmp = dtnhanvien.Rows[nIndex]["nhanvien_taikhoan"].ToString().Trim();
                if (taikhoanTmp == taikhoan.Trim())
                {
                    id_nhanviennhan = Convert.ToInt32(dtnhanvien.Rows[nIndex]["nhanvien_id"].ToString().Trim());
                    break;
                }
            }
            return id_nhanviennhan;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Quản lý chức vụ của nhân viên";
            if (!IsPostBack)
            {
                dtnv_cv = new DataTable();
                dtnv_cv = getnv_cvList();
                try
                {
                  
                    string sqlchucvu = "select * from chucvu";
                    string sqlnhanvien = "select * from nhanvien";
               
                    dtchucvu = cn.XemDL(sqlchucvu);
                    dtnhanvien = cn.XemDL(sqlnhanvien);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}