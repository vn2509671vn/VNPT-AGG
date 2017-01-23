using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace VNPT_BSC
{
    public class Message
    {
        private string username = "Ws_angiang_otp";
        private string password = "angiangotp";

        // Gửi SMS theo số điện thoai
        public void SendSMS_VTTP(string szPhone, string szMsgContent) {
            string szResult = "";
            SMSsv.AuthHeader authen = new SMSsv.AuthHeader();
            authen.Username = username;
            authen.Password = password;

            SMSsv.Service1 svSMS = new SMSsv.Service1();
            svSMS.AuthHeaderValue = authen;
            if (szPhone != "") {
                szResult = svSMS.sendsms(szPhone, szMsgContent);
            }
            //return szResult;
        }

        private DataTable dtSDTByMaDV(int donvi_id)
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select nv.nhanvien_didong from nhanvien nv, donvi dv, nhanvien_chucvu cv where nv.nhanvien_donvi = dv.donvi_id and dv.donvi_id = '" + donvi_id + "' and nv.nhanvien_id = cv.nhanvien_id and cv.chucvu_id in (3,5)";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtResult;
        }

        private DataTable dtSDTChuyenVienBSC()
        {
            DataTable dtResult = new DataTable();
            Connection cn = new Connection();
            string sql = "select * from nhanvien nv, nhanvien_chucvu cv where nv.nhanvien_id = cv.nhanvien_id and cv.chucvu_id in (10)";
            try
            {
                dtResult = cn.XemDL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtResult;
        }

        // Gửi sms cho PGĐ phòng bán hàng và trưởng phòng dựa theo mã đơn vị
        public void SendSMS_ByIDDV(int donvi_id, string szMsgContent)
        {
            Connection cn = new Connection();
            DataTable dtSDT = new DataTable();
            string szTmp = "";
            SMSsv.AuthHeader authen = new SMSsv.AuthHeader();
            authen.Username = username;
            authen.Password = password;

            SMSsv.Service1 svSMS = new SMSsv.Service1();
            svSMS.AuthHeaderValue = authen;

            dtSDT = dtSDTByMaDV(donvi_id);
            for (int i = 0; i < dtSDT.Rows.Count; i++)
            {
                string szSDT = dtSDT.Rows[i]["nhanvien_didong"].ToString().Trim();
                string szContent = szMsgContent;
                if (szSDT == "")
                {
                    continue;
                }
                szTmp = svSMS.sendsms(szSDT, szContent);
            }
        }

        // Gửi sms cho các chuyên viên bsc
        public void SendSMSToChuyenVienBSC(string szMsgContent)
        {
            Connection cn = new Connection();
            DataTable dtSDT = new DataTable();
            string szTmp = "";
            SMSsv.AuthHeader authen = new SMSsv.AuthHeader();
            authen.Username = username;
            authen.Password = password;

            SMSsv.Service1 svSMS = new SMSsv.Service1();
            svSMS.AuthHeaderValue = authen;

            dtSDT = dtSDTChuyenVienBSC();
            for (int i = 0; i < dtSDT.Rows.Count; i++)
            {
                string szSDT = dtSDT.Rows[i]["nhanvien_didong"].ToString().Trim();
                string szContent = szMsgContent;
                if (szSDT == "") {
                    continue;
                }
                szTmp = svSMS.sendsms(szSDT, szContent);
            }
        }
    }
}