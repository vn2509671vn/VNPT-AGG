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

namespace VNPT_BSC
{
    public partial class index : System.Web.UI.Page
    {

        Connection cn = new Connection();
        public DataTable dtkpiindex;
        public DataTable dtthamdinhindex;
        public static DataTable dtnhanvien_kpi = new DataTable();
        public static DataTable dtkpi = new DataTable();

        private DataTable getkpiindexList(int nhanvien_id)
        {
            
            string sqlBSC = "select nvgiao.nhanvien_hoten as giao,nvnhan.nhanvien_hoten as nhan,a.thang,a.nam,b.kpi_ten as tenkpi,a.donvitinh,a.trongso,a.kehoach,a.thuchien,a.thamdinh "+
                            "from bsc_nhanvien a,nhanvien nvnhan, nhanvien nvgiao, kpi b "+
                            "where a.nhanviengiao = nvgiao.nhanvien_id and a.nhanviennhan = nvnhan.nhanvien_id and b.kpi_id = a.kpi and a.nhanviennhan = '" + nhanvien_id + "' and a.thang = datepart(MM ,GETDATE())";
            DataTable dtkpiindex = new DataTable();
            try
            {
                dtkpiindex = cn.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                dtkpiindex = null;
            }
            return dtkpiindex;
        }

        private DataTable getthamdinhindexList(int nhanvien_id)
        {

            string sqlBSC = "select nvgiao.nhanvien_id as nhanviengiao, nvgiao.nhanvien_hoten as tennvg, nvnhan.nhanvien_id as nhanviennhan, nvnhan.nhanvien_hoten as tennvn, giaobsc.nam, giaobsc.thang, giaobsc.trangthaicham, giaobsc.trangthaidongy_kqtd, giaobsc.trangthaiketthuc,  ";
            sqlBSC += "(select COUNT(*) from bsc_nhanvien where bsc_nhanvien.nhanvienthamdinh = '" + nhanvien_id + "' and bsc_nhanvien.trangthaithamdinh = 0 and bsc_nhanvien.nam = datepart(YYYY ,GETDATE()) and bsc_nhanvien.thang = giaobsc.thang) as sl_chuatd ";
            sqlBSC += "from giaobscnhanvien giaobsc, bsc_nhanvien giaobsc_nv, nhanvien nvgiao, nhanvien nvnhan ";
            sqlBSC += "where giaobsc.thang = giaobsc_nv.thang ";
            sqlBSC += "and giaobsc.nam = giaobsc_nv.nam ";
            sqlBSC += "and giaobsc.nhanviengiao = giaobsc_nv.nhanviengiao ";
            sqlBSC += "and giaobsc.nhanviennhan = giaobsc_nv.nhanviennhan ";
            sqlBSC += "and giaobsc.nhanviengiao = nvgiao.nhanvien_id ";
            sqlBSC += "and giaobsc.nhanviennhan = nvnhan.nhanvien_id ";
            sqlBSC += "and giaobsc.nam = datepart(YYYY ,GETDATE()) ";
            sqlBSC += "and giaobsc_nv.nhanvienthamdinh = '" + nhanvien_id + "' ";
            sqlBSC += "group by nvgiao.nhanvien_id, nvgiao.nhanvien_hoten, nvnhan.nhanvien_id, nvnhan.nhanvien_hoten, giaobsc.nam, giaobsc.thang, giaobsc.trangthaicham, giaobsc.trangthaidongy_kqtd, giaobsc.trangthaiketthuc";
            DataTable dtthamdinhindex = new DataTable();
            try
            {
                dtthamdinhindex = cn.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                dtthamdinhindex = null;
            }
            return dtthamdinhindex;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Nhanvien nhanvien = new Nhanvien();
                nhanvien = Session.GetCurrentUser();
                /*Get list BSC*/
                dtkpiindex = new DataTable();
                dtkpiindex = getkpiindexList(nhanvien.nhanvien_id);

                dtthamdinhindex = new DataTable();
                dtthamdinhindex = getthamdinhindexList(nhanvien.nhanvien_id);
                try
                {
                    string sqlnhanvien = "select * from nhanvien";
                    string sqlkpi = "select * from kpi";

                    dtnhanvien_kpi = cn.XemDL(sqlnhanvien);
                    dtkpi = cn.XemDL(sqlkpi);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
    }
}