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
            Connection cnBSC = new Connection();
            DataTable gridData = new DataTable();
            string sqlBSC = "select bsc.thang, bsc.nam, bsc.kpi_id, kpi.kpi_ten, kpo.kpo_id, kpo.kpo_ten ";
            sqlBSC += "from danhsachbsc bsc, kpi, kpo ";
            sqlBSC += "where bsc.kpi_id = kpi.kpi_id ";
            sqlBSC += "and kpi.kpi_thuoc_kpo = kpo.kpo_id ";
            sqlBSC += "and bsc.thang = '"+thang+"' and bsc.nam = '"+nam+"'";
            try{
                gridData = cnBSC.XemDL(sqlBSC);
            }
            catch (Exception ex){
                throw ex;
            }

            string arrOutput = "";
            arrOutput += "<div class='table-responsive padding-top-10'>";
            arrOutput += "<table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>";
                arrOutput += "<thead>";
                    arrOutput += "<tr>";
                        arrOutput += "<th>STT</th>";
                        arrOutput += "<th>Chỉ tiêu</th>";
                        arrOutput += "<th>Tỷ trọng (%)</th>";
                        arrOutput += "<th>ĐVT</th>";
                        arrOutput += "<th>Kế hoạch</th>";
                    arrOutput += "</tr>";
                arrOutput += "</thead>";
                arrOutput += "<tbody>";
                if (gridData.Rows.Count <= 0)
                {
                    arrOutput += "<tr><td colspan='5' class='text-center'>No item</td></tr>";
                }
                else {
                    for (int nKPI = 0; nKPI < gridData.Rows.Count; nKPI++) {
                        arrOutput += "<tr>";
                        arrOutput += "<td>" + (nKPI+1) + "</td>";
                        arrOutput += "<td>" + gridData.Rows[nKPI]["kpi_ten"].ToString() + " (" + gridData.Rows[nKPI]["kpo_ten"].ToString()  + ")" + "</td>";
                        arrOutput += "<td class='text-center'><input type='text' class='form-control' name='tytrong' id='tytrong_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2' maxlength='2'/></td>";
                        arrOutput += "<td class='text-center'><input type='text' class='form-control' name='dvt' id='dvt_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='5'/></td>";
                        arrOutput += "<td class='text-center'><input type='text' class='form-control' name='kehoach' id='kehoach_" + gridData.Rows[nKPI]["kpi_id"].ToString() + "' size='2'/></td>";
                        arrOutput += "</tr>";
                    }
                }
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