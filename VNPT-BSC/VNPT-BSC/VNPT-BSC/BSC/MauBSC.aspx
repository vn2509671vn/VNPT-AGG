<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MauBSC.aspx.cs" Inherits="VNPT_BSC.BSC.MauBSC" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
          <div class="panel-heading">
            <h3 class="panel-title">MẪU CHỈ TIÊU BSC/KPI</h3>
          </div>
          <div class="panel-body">
            <div class="col-sm-2">
                <div class="list-group">
                <% for(int i = 0; i < dtBSC.Rows.Count; i++){ %>
                    <%
                        string month =  dtBSC.Rows[i][0].ToString();
                        string year =  dtBSC.Rows[i][1].ToString();
                    %>
                    <a href="#" onclick="fillData(<%=month %>, <%=year %>)" class="list-group-item list-group-item-info text-center"><%= month +"/"+ year%></a>
                <% } %>
                </div>
            </div>
            <div class="col-sm-10 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-3">Thời gian áp dụng:</label>
                    <div class="col-sm-4 form-inline">
                        <input type="text" class="form-control number" id="month" name="month" maxlength="2" size="2"/>
                        <input type="text" class="form-control number" id="year" name="year" maxlength="4" size="4"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Danh sách KPI:</label>
                    <div class="col-sm-8">
                        <% for(int i = 0; i < dtKPI.Rows.Count; i++){ %>
                            <div class="checkbox">
                              <label><input type="checkbox" value="<%=dtKPI.Rows[i]["kpi_id"].ToString() %>" /><%=dtKPI.Rows[i]["name"].ToString() %></label>
                            </div>
                        <% } %>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-8 col-sm-offset-3">
                            <button type="button" class="btn btn-success" id="btnSave">Lưu</button>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        
                    </div>
                </div>
            </div>
          </div>
        </div>
    </div>
<script type="text/javascript">
    function fillData(month, year) {
        $("#month").val(month);
        $("#year").val(year);
        $.ajax({
            method: "post",
            url: "",
            data: {
                month: month,
                year: year
            },
            dataType:"text",  
            success: function (data) {
                
            }
        });
    }

    $(document).ready(function () {
        $("#month").keydown(function () {
            $(this).keypress(function (e) {
                if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
            });
        });

        $("#year").keydown(function () {
            $(this).keypress(function (e) {
                if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
            });
        });

        $("#btnSave").click(function () {
            alert("Test");
        });
    });
</script>

</asp:Content>
