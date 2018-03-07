using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace VNPT_BSC
{
    public class Connection
    {
        public SqlConnection cn = new SqlConnection();
        public void KetNoi()
        {
            try
            {
                if (cn.State == 0)
                {
                    //cn.ConnectionString = @"Data Source=10.94.30.4;Initial Catalog=PortalVNPT;User ID=portal; Password = portal!123;Integrated Security=True";
                    cn.ConnectionString = @"Data Source=10.94.34.106;Initial Catalog=VNPT;Persist Security Info=True;User ID=sa; Password = 123;Connection Timeout=60";
                    cn.Open();
                }
            }
            catch (Exception ex){
                throw ex;
            }
        }

        public void NgatKetNoi() { 
            if(cn.State != 0){
                cn.Close();
            }
        }

        public DataTable XemDL(string sql) {
            KetNoi();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            NgatKetNoi();
            return dt;
        }

        public SqlCommand ThucThiDL(string sql) {
            KetNoi();
            SqlCommand cm = new SqlCommand(sql, cn);
            cm.ExecuteNonQuery();
            NgatKetNoi();
            return cm;
        }
    }
}