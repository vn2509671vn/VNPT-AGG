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

namespace VNPT_BSC.BSC
{
    public partial class PhanPhoiBSCDonVi : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public static DataTable dtDonvi = new DataTable();
        public static DataTable dtBSC = new DataTable();

        [WebMethod]
        public static string loadBSC(int thang, int nam) {
            string arrOutput = "";
            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-user' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
                arrOutput += "<thead>";
                    arrOutput += "<tr>";
                        arrOutput += "<th>STT</th>";
                        arrOutput += "<th>Chỉ tiêu</th>";
                        arrOutput += "<th>Tỷ trọng</th>";
                        arrOutput += "<th>ĐVT</th>";
                        arrOutput += "<th>Kế hoạch</th>";
                    arrOutput += "</tr>";
                arrOutput += "</thead>";
                arrOutput += "<tbody>";
                    
                arrOutput += "</tbody>";
            arrOutput += "</table>";
            return arrOutput;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                try
                {
                    int donvichuquan = 1;
                    string sqlDanhSachDonVi = "select * from donvi where donvi_id not in ('" + donvichuquan + "')";
                    string sqlDanhSachKPI = "select thang, nam, CONVERT(varchar(4), thang) + '/' + CONVERT(varchar(4), nam) AS content from DANHSACHBSC group by nam, thang order by nam, thang ASC";
                    dtDonvi = cn.XemDL(sqlDanhSachDonVi);
                    dtBSC = cn.XemDL(sqlDanhSachKPI);
                }
                catch { 
                    dtDonvi = null;
                }
            }
        }
    }
}