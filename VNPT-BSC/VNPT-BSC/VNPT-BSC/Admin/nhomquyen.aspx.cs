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
    public partial class nhomquyen : System.Web.UI.Page
    {

        Connection cn = new Connection();
        public DataTable dtnhomquyen;

        private DataTable getnhomquyenList()
        {
            string sqlBSC = "select loaiquyen_id,loaiquyen_maloai,loaiquyen_ten,loaiquyen_mota from loaiquyen";
            DataTable dtnhomquyen = new DataTable();
            try
            {
                dtnhomquyen = cn.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                dtnhomquyen = null;
            }
            return dtnhomquyen;
        }


        [WebMethod]
        public static bool SaveData(string quyen_maA, string quyen_tenA, string quyen_motaA)
        {
            Connection nhomquyen = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {
                sqlInsertNewData = "insert into loaiquyen(loaiquyen_maloai,loaiquyen_ten, loaiquyen_mota) values(N'" + quyen_maA + "',N'" + quyen_tenA + "', N'" + quyen_motaA + "')";
                nhomquyen.ThucThiDL(sqlInsertNewData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }
        [WebMethod]
        public static bool EditData(string quyen_ma_suaA, string quyen_ten_suaA, string quyen_mota_suaA, int quyen_id_suaA)
        {
            Connection nhomquyen_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update loaiquyen set loaiquyen_maloai = N'" + quyen_ma_suaA + "',loaiquyen_ten = N'" + quyen_ten_suaA + "', loaiquyen_mota = N'" + quyen_mota_suaA + "' where loaiquyen_id = '" + quyen_id_suaA + "'";
                nhomquyen_edit.ThucThiDL(sqlUpdateData);
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }
        [WebMethod]
        public static bool DeleteData(int nq_id_xoaAprove)
        {
            Connection loaiquyen_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {
                sqldeleteData = "delete loaiquyen where loaiquyen_id = '" + nq_id_xoaAprove + "'";
                loaiquyen_delete.ThucThiDL(sqldeleteData);
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
                /*Get list BSC*/
                dtnhomquyen = new DataTable();
                dtnhomquyen = getnhomquyenList();


            }
        }
    }
}