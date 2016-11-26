using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace VNPT_BSC.Ajax
{
    public class ajaxBSC
    {
        Connection cn = new Connection();
        public string getKPIByDate(int month, int year) {
            string szOutput = "";
            string sql = "select * from danhsachbsc where thang = '" + month + "' and nam = '" + year + "'";
            DataTable query = new DataTable();
            query = cn.XemDL(sql);
            return szOutput;
        }
    }
}