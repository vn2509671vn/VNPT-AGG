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

namespace VNPT_BSC.TinhLuong
{
    public partial class TinhLuongTheoBuoc : System.Web.UI.Page
    {
        [WebMethod]
        public static Dictionary<String, String> kiemtraDongBo(int thang, int nam)
        {
            Dictionary<String, String> dicOutput = new Dictionary<String, String>();
            Connection cn = new Connection();
            string timekey = nam.ToString() + thang.ToString("00");
            DateTime ngay = new DateTime(nam, 1, thang);
            DataTable tmp = new DataTable();
            tmp = cn.XemDL("select * from tmp_tongbsc_donvi where thang = '" + thang + "' and nam = '" + nam + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_bsc_donvi", "1");
            }
            else { 
                dicOutput.Add("trangthai_dongbo_bsc_donvi", "0"); 
            }

            tmp = cn.XemDL("select * from tmp_tongbsc_nhanvien where thang = '" + thang + "' and nam = '" + nam + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_bsc_nhanvien", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_bsc_nhanvien", "0");
            }

            tmp = cn.XemDL("select * from pttb_nhanvien_khoitructiep where timekey = '" + timekey + "' and loainhanvien='NV'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_luong_nhanvien_truoctiep", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_luong_nhanvien_truoctiep", "0");
            }

            tmp = cn.XemDL("select * from pttb_nhanvien_khoitructiep where timekey = '" + timekey + "' and loainhanvien='LD'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_luong_lanhdao_truoctiep", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_luong_lanhdao_truoctiep", "0");
            }

            tmp = cn.XemDL("select * from pttb_tongluong where timekey = '" + timekey + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_luong_pttb_trungtam_lxn", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_luong_pttb_trungtam_lxn", "0");
            }

            tmp = cn.XemDL("select * from pttb_quyluong_khoigiantiep where timekey = '" + timekey + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_quyluong_pttb_khoigiantiep", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_quyluong_pttb_khoigiantiep", "0");
            }

            tmp = cn.XemDL("select * from pttb_chitiet_nhanvien_khoigiantiep where timekey = '" + timekey + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_pttb_khoigiantiep", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_pttb_khoigiantiep", "0");
            }

            tmp = cn.XemDL("select * from qlns_quyluong_bsc_khoitructiep where timekey = '" + timekey + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_quyluong_bsc_khoitructiep", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_quyluong_bsc_khoitructiep", "0");
            }

            tmp = cn.XemDL("select * from qlns_quyluong_bsc_khoigiantiep where timekey = '" + timekey + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_quyluong_bsc_khoigiantiep", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_quyluong_bsc_khoigiantiep", "0");
            }

            tmp = cn.XemDL("select * from qlns_luong_bsc_khoitructiep where timekey = '" + timekey + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_luong_bsc_khoitructiep", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_luong_bsc_khoitructiep", "0");
            }

            tmp = cn.XemDL("select * from qlns_luong_bsc_khoigiantiep where timekey = '" + timekey + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_luong_bsc_khoigiantiep", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_luong_bsc_khoigiantiep", "0");
            }

            tmp = cn.XemDL("select * from qlns_luong_tonghop_3ps where timekey = '" + timekey + "'");
            if (tmp.Rows.Count > 0)
            {
                dicOutput.Add("trangthai_dongbo_luong_bsc_tonghop_3ps", "1");
            }
            else
            {
                dicOutput.Add("trangthai_dongbo_luong_bsc_tonghop_3ps", "0");
            }
            return dicOutput;
        }

        [WebMethod]
        public static int tienhanhDongBo(int thang, int nam, string[] lstStore) {
            int bResult = 1;
            Connection cn = new Connection();
            string timekey = nam.ToString() + thang.ToString("00");
            for (int i = 0; i < lstStore.Length; i++) {
                try
                {
                    string sql = "EXEC " + lstStore[i].ToString().Trim() + " '" + timekey + "'";
                    cn.ThucThiDL(sql);
                }
                catch (Exception ex) {
                    bResult = 0;
                    throw ex;
                }
            }
            
            return bResult;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Các bước tính lương";
        }
    }
}