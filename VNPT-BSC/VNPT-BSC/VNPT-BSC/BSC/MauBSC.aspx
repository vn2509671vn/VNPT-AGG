﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MauBSC.aspx.cs" Inherits="VNPT_BSC.BSC.MauBSC" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>

    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
          <div class="panel-heading">
            <h3 class="panel-title">MẪU CHỈ TIÊU BSC/KPI</h3>
          </div>
          <div class="panel-body">
            <div class="col-sm-3">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Danh Sách KPI</h3>
                    </div>
                    <ul class="list-group">
                        <% for(int i = 0; i < dtBSC.Rows.Count; i++){ %>
                            <%
                                string month =  dtBSC.Rows[i][0].ToString();
                                string year =  dtBSC.Rows[i][1].ToString();
                            %>
                            <a href="#" onclick="fillData(<%=month %>, <%=year %>)" class="list-group-item list-group-item-info text-center"><%= month +"/"+ year%></a>
                        <% } %>
                    </ul>
                </div>
            </div>
            <div class="col-sm-9 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-3">Thời gian áp dụng:</label>
                    <div class="col-sm-4 form-inline">
                        <input type="text" class="form-control number" id="month" name="month" maxlength="2" size="2" placeholder="Tháng"/>
                        <input type="text" class="form-control number" id="year" name="year" maxlength="4" size="4" placeholder="Năm"/>
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
                            <button type="button" class="btn btn-default" id="btnClean" onclick="clearInputs()">Reset</button>
                        </div>
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

        /*Remove red border*/
        $("#month").css("border-color", "#ccc");
        $("#year").css("border-color", "#ccc");

        var requestData = {
            monthAprove : month,
            yearAprove : year
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "MauBSC.aspx/BindingCheckBox",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("input[type=checkbox]").attr("checked", false);
                var arrKPI = new Array();
                arrKPI = result.d;
                for (var i = 0; i < arrKPI.length; i++) {
                    var KPI_ID = arrKPI[i];
                    $(":checkbox[value='" + KPI_ID + "']").prop("checked", "true");
                }
            },
            error: function (msg) { alert(msg.d);}
        });
    }

    $(document).ready(function () {
        validateNumber("month");
        validateNumber("year");

        $("#btnSave").click(function () {
            var month = $("#month").val();
            var year = $("#year").val();
            var isMonth = validateMonth("month");
            var isYear = validateYear("year");
            if (!isMonth || !isYear) {
                alert("Vui lòng nhập đúng vào trường bất buộc!!!");
                return false;
            }
            var arrKPI = new Array();
            $("input[type=checkbox]:checked").each(function () {
                arrKPI.push($(this).val());
            });

            var requestData = {
                monthAprove: month,
                yearAprove: year,
                arrKPI_ID: arrKPI
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "MauBSC.aspx/SaveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d) {
                        alert("Lưu dữ liệu thành công!!!");
                        window.location.reload();
                    }
                    else {
                        alert("Vui lòng check lại!!!");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>

</asp:Content>
