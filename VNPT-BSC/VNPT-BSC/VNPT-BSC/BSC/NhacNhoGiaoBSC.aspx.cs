using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Globalization;

namespace VNPT_BSC.BSC
{
    public partial class NhacNhoGiaoBSC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Message msg = new Message();
            //DateTime date = new DateTime();
            int month_giaobsc = DateTime.Today.Month;
            int month_thamdinh = DateTime.Today.Month == 1 ? 12 : (DateTime.Today.Month - 1);
            string szMsgContent1 = "Nhac nho cac anh/chi Giam doc PBH va cac Phong ban chuc nang giao BSC/KPI thang " + month_giaobsc + " cho NLD. Han chot giao BSC la ngay 6 moi thang.";
            string szMsgContent2 = "Nhac nho cac anh/chi Giam doc PBH va cac Phong ban chuc nang tham dinh BSC/KPI thang " + month_thamdinh + " cho NLD. Han chot tham dinh BSC la ngay 13 moi thang.";
            string szAction = Request.QueryString["act"];
            string sql = "select donvi_id from donvi where donvi_id not in (1,2)";
            Connection cn = new Connection();
            DataTable tmp = new DataTable();
            try
            {
                tmp = cn.XemDL(sql);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (szAction == "giaobsc") {
                for (int i = 0; i < tmp.Rows.Count; i++) {
                    msg.SendSMS_ByIDDV(Convert.ToInt32(tmp.Rows[i]["donvi_id"].ToString()), szMsgContent1);
                }
            }
            else if (szAction == "thamdinh")
            {
                for (int i = 0; i < tmp.Rows.Count; i++)
                {
                    msg.SendSMS_ByIDDV(Convert.ToInt32(tmp.Rows[i]["donvi_id"].ToString()), szMsgContent2);
                }
                //msg.SendSMS_ByIDNV(215, "Test Nhac Nho giao BSC");
            }
        }
    }
}