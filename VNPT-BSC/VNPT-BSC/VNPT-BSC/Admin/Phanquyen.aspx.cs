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

namespace VNPT_BSC.Admin
{
    public partial class Phanquyen : System.Web.UI.Page
    {

        Connection cn = new Connection();
        public DataTable dtphanquyen;
        public static DataTable dtchucvu = new DataTable();
        public static DataTable dtquyen = new DataTable();

        private DataTable getphanquyenList()
        {
           
            string sqlquyen = "select a.chucvu_id,a.chucvu_ten,c.quyen_id,c.quyen_ten,b.mota from chucvu a,quyen_cv b, quyen c where a.chucvu_id = b.chucvu_id and b.quyen_id = c.quyen_id";
            DataTable dtphanquyen = new DataTable();
            try
            {
                dtphanquyen = cn.XemDL(sqlquyen);
            }
            catch (Exception ex)
            {
                dtphanquyen = null;
            }
            return dtphanquyen;
        }

        [WebMethod]
        public static bool SaveData(int quyen_tenA, int quyen_chucvuA, string quyen_motaA)
        {
           
            Connection phanquyen = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into quyen_cv(quyen_id,chucvu_id, mota) values(N'" + quyen_tenA + "',N'" + quyen_chucvuA + "', N'" + quyen_motaA + "')";
                phanquyen.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(int quyen_ten_sua_oldA,int quyen_chucvu_sua_oldA,int quyen_ten_suaA, int quyen_chucvu_suaA, string quyen_mota_suaA)
        {
            Connection phanquyen_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update quyen_cv set quyen_id = '" + quyen_ten_suaA + "',chucvu_id = '" + quyen_chucvu_suaA + "', mota = N'" + quyen_mota_suaA + "' where quyen_id = '" + quyen_ten_sua_oldA + "' and chucvu_id = '" + quyen_chucvu_sua_oldA + "'";
                phanquyen_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }
        [WebMethod]
        public static bool DeleteData(int quyen_idA, int quyen_chucvu_idA)
        {
            Connection phanquyen_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete quyen_cv where quyen_id = '" + quyen_idA + "' and chucvu_id = '" + quyen_chucvu_idA + "'";
                phanquyen_delete.ThucThiDL(sqldeleteData);
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
                dtphanquyen = new DataTable();
                dtphanquyen = getphanquyenList();
                try
                {
                    string sqlchucvu = "select * from chucvu";
                    string sqlquyen = "select * from quyen";

                    dtchucvu = cn.XemDL(sqlchucvu);
                    dtquyen = cn.XemDL(sqlquyen);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}