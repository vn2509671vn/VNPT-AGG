﻿using System;
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
    public partial class Home : System.Web.UI.Page
    {
        Connection cn = new Connection();
        public DataTable dtchucdanh;

        private DataTable getchucdanhList()
        {
            string sqlBSC = "select chucdanh_id,chucdanh_ten,chucdanh_mota,chucdanh_ma from chucdanh";
            DataTable dtchucdanh = new DataTable();
            try
            {
                dtchucdanh = cn.XemDL(sqlBSC);
            }
            catch (Exception ex)
            {
                dtchucdanh = null;
            }
            return dtchucdanh;
        }

        [WebMethod]
        public static bool SaveData(string cd_tenAprove, string cd_motaAprove, string cd_maAprove)
        {
            Connection chucdanh = new Connection();
            bool output = false;
            string sqlInsertNewData = "";
            try
            {


                sqlInsertNewData = "insert into chucdanh(chucdanh_ten,chucdanh_mota, chucdanh_ma) values('" + cd_tenAprove + "','" + cd_motaAprove + "', '" + cd_maAprove + "')";
                try
                {
                    chucdanh.ThucThiDL(sqlInsertNewData);
                }
                catch
                {
                    output = false;
                }
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool EditData(string cd_ten_suaAprove, string cd_mota_suaAprove, string cd_ma_suaAprove, int cd_id_suaAprove)
        {
            Connection chucdanh_edit = new Connection();
            bool output = false;
            string sqlUpdateData = "";
            try
            {
                sqlUpdateData = "Update chucdanh set chucdanh_ten = '" + cd_ten_suaAprove + "',chucdanh_mota = '" + cd_mota_suaAprove + "', chucdanh_ma = '" + cd_ma_suaAprove + "' where chucdanh_id = '" + cd_id_suaAprove + "'";
                try
                {
                    chucdanh_edit.ThucThiDL(sqlUpdateData);
                }
                catch
                {
                    output = false;
                }
                output = true;
            }
            catch
            {
                output = false;
            }
            return output;
        }

        [WebMethod]
        public static bool DeleteData(int cd_id_xoaAprove)
        {
            Connection chucdanh_delete = new Connection();
            bool output = false;
            string sqldeleteData = "";
            try
            {


                sqldeleteData = "delete chucdanh where chucdanh_id = '" + cd_id_xoaAprove + "'";
                try
                {
                    chucdanh_delete.ThucThiDL(sqldeleteData);
                }
                catch
                {
                    output = false;
                }
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
                dtchucdanh = new DataTable();
                dtchucdanh = getchucdanhList();


            }
        }
    }
}