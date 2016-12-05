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
    public partial class KPO : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public DataTable dtkpo;
        public static DataTable dtnhanvien_kpo = new DataTable();

        private DataTable getkpoList()
        {
            string sqlkpo = "select a.kpo_id,a.kpo_ten,a.kpo_mota,a.kpo_ngaytao,b.nhanvien_hoten from kpo a,nhanvien b where a.kpo_nguoitao = b.nhanvien_id";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            dtkpo = new DataTable();
            dtkpo = getkpoList();
        }
    }
}