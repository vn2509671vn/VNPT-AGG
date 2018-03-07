﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="MauThueBao_DHNV.aspx.cs" Inherits="VNPT_BSC.BSC.MauThueBao_DHNV" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/function.js"></script>
    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css"/>
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>
    <!-- Add for export data of datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.4/css/buttons.dataTables.min.css"/>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.flash.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.print.min.js"></script>

    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
          <div class="panel-heading">
            <h3 class="panel-title">DANH SÁCH THUÊ BAO P.ĐHNV CHƯA PHÂN</h3>
          </div>
          <div class="panel-body">
              <div id="divLoading" style="margin: 0px; padding: 0px; position: fixed; left: 0px; top: 0px; width: 100%; height: 100%; background-color: rgb(102, 102, 102); z-index: 30001; opacity: 0.8;">
                <p style="position: absolute; color: White; top: 50%; left: 30%;">
                    Vui lòng chờ trong giây lát do dữ liệu tải lên khá lớn ........
                    <img src="../Images/Loading/ajax-loader.gif" />
                </p>
              </div>
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Lọc theo tháng/năm:</label>
                    <div class="col-sm-6 form-inline">
                        <select class="form-control" id="month">
                            <% for(int i = 1; i <= 12; i++){ 
                                string selectOption = "";
                                int month =  Convert.ToInt32(DateTime.Now.ToString("MM"));
                                if(i == month){
                                    selectOption = "selected";
                                }
                            %>
                            <option value="<%=i %>" <%=selectOption %>><%=i %></option>
                            <% } %>
                        </select>
                        <select class="form-control" id="year">
                            <% for(int i = 2016; i <= 2100; i++){ 
                                string selectOption = "";
                                int year =  Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                                if(i == year){
                                    selectOption = "selected";
                                }
                            %>
                            <option value="<%=i %>" <%=selectOption %>><%=i %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Trạng thái:</label>
                    <div class="col-sm-6 form-inline">
                        <select class="form-control" id="trangthai">
                            <option value="0">Tất cả</option>
                            <option value="1" selected="selected">Chưa giao</option>
                            <option value="2">Đã giao</option>
                        </select>
                    </div>
                </div>
              </div>
              <div class="col-md-12 col-xs-12" id="gridData">

              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    function loadData(month, year, trangthai) {
        var requestData = {
            thang: month,
            nam: year,
            trangthai: trangthai
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "MauThueBao_DHNV.aspx/loadData",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;

                $("#gridData").html(output);
                $("#table-data").DataTable({
                    "bLengthChange": false,
                    "bPaginate": false,
                    "bSort": true,
                    "dom": 'Bfrtip',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                        }
                    ]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {

        // Load grid lần đầu
        loadData($("#month").val(), $("#year").val(), $("#trangthai").val());

        // Load grid khi năm thay đổi
        $("#year").change(function () {
            var thang = $("#month").val();
            var nam = $(this).val();
            var trangthai = $("#trangthai").val();
            loadData(thang, nam, trangthai);
        });

        // Load grid khi tháng thay đổi
        $("#month").change(function () {
            var nam = $("#year").val();
            var thang = $(this).val();
            var trangthai = $("#trangthai").val();
            loadData(thang, nam, trangthai);
        });

        // Load grid khi trạng thái thay đổi
        $("#trangthai").change(function () {
            var nam = $("#year").val();
            var thang = $("#month").val();
            var trangthai = $(this).val();
            loadData(thang, nam, trangthai);
        });

        $(document).ajaxStart(function () {
            //var left = $(".left_col").css("width");
            //$("#divLoading").css({
            //    "left": left + "px"
            //});
            $("#divLoading").show();
        }).ajaxStop(function () {
            $("#divLoading").hide();
        });
    });
</script>
</asp:Content>
