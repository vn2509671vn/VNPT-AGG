﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="ThongKeTrungBinhDiemBSCNV.aspx.cs" Inherits="VNPT_BSC.BSC.ThongKeTrungBinhDiemBSCNV" %>
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
            <h3 class="panel-title">Trung bình điểm BSC nhân viên</h3>
          </div>
          <div class="panel-body">
              <h4 class="text-center red-color">Dữ liệu được cập nhật tới <%=thoigiancapnhat %> --- [ <a id="btnDongBo" class="btn btn-xs btn-success">Đồng bộ tới hiện tại</a>] ---</h4>
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-4">Đơn vị:</label>
                    <div class="col-sm-6">
                        <select class="form-control" id="donvi">
                            <option value="0">Tất cả</option>
                            <% for (int i = 0; i < dtDonVi.Rows.Count; i++)
                               { 
                            %>
                            <option value="<%=dtDonVi.Rows[i]["donvi_id"].ToString() %>"><%=dtDonVi.Rows[i]["donvi_ten"].ToString() %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Năm:</label>
                    <div class="col-sm-6">
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
                    <label class="control-label col-sm-4">Quý:</label>
                    <div class="col-sm-6">
                        <select class="form-control" id="quy">
                            <option value="0" selected>Tất cả</option>
                            <option value="1">I</option>
                            <option value="2">II</option>
                            <option value="3">III</option>
                            <option value="4">IV</option>
                            <option value="6">6 tháng đầu năm</option>
                            <option value="9">9 tháng đầu năm</option>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Tháng:</label>
                    <div class="col-sm-6">
                        <select class="form-control" id="month">
                            <option value="0" selected>Tất cả</option>
                            <% for(int i = 1; i <= 12; i++)
                               {
                            %>
                            <option value="<%=i %>"><%=i %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Loại nhân viên:</label>
                    <div class="col-sm-6">
                        <select class="form-control" id="loainhanvien">
                            <option value="0">Tất cả</option>
                            <option value="1">Chính thức</option>
                            <option value="2">Cộng tác viên</option>
                        </select>
                    </div>
                </div>
              </div>
              <div class="col-md-12 col-xs-12" id="gridBSC">

              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    function loadBSC() {
        var donvi = $("#donvi").val();
        var nam = $("#year").val();
        var quy = $("#quy").val();
        var thang = $("#month").val();
        var loainv = $("#loainhanvien").val();

        var requestData = {
            donvi: donvi,
            nam: nam,
            quy: quy,
            thang: thang,
            loainv: loainv
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ThongKeTrungBinhDiemBSCNV.aspx/loadBSC",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                $("#gridBSC").html(output);
                $("#table-kpi").DataTable({
                    "pageLength": 100,
                    "dom": 'Bfrtip',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                            title: 'Điểm TB BSC/KPI'
                        },
                        {
                            extend: 'pdfHtml5',
                            title: 'Điểm TB BSC/KPI',
                            orientation: 'landscape',
                            pageSize: 'LEGAL'
                        }
                    ]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
        // Load grid lần đầu
        loadBSC();

        // Load grid khi năm thay đổi
        $("#year").change(function () {
            loadBSC();
        });

        // Load grid khi tháng thay đổi
        $(document).on('change', '#month', function () {
            loadBSC();
        });

        $("#quy").change(function () {
            $("#month").find("option").remove();
            var szOption = "<option value='0'>Tất cả</option>";
            var quy = $(this).val();
            if (quy == 0) {
                for (var i = 1; i <= 12; i++) {
                    szOption += "<option value='" + i + "'>" + i + "</option>";
                }
            }
            else if (quy == 1) {
                for (var i = 1; i <= 3; i++) {
                    szOption += "<option value='" + i + "'>" + i + "</option>";
                }
            }
            else if (quy == 2) {
                for (var i = 4; i <= 6; i++) {
                    szOption += "<option value='" + i + "'>" + i + "</option>";
                }
            }
            else if (quy == 3) {
                for (var i = 7; i <= 9; i++) {
                    szOption += "<option value='" + i + "'>" + i + "</option>";
                }
            }
            else if (quy == 4) {
                for (var i = 10; i <= 12; i++) {
                    szOption += "<option value='" + i + "'>" + i + "</option>";
                }
            }
            else if (quy == 6) {
                for (var i = 1; i <= 6; i++) {
                    szOption += "<option value='" + i + "'>" + i + "</option>";
                }
            }
            else if (quy == 9) {
                for (var i = 1; i <= 9; i++) {
                    szOption += "<option value='" + i + "'>" + i + "</option>";
                }
            }

            $("#month").append(szOption);

            loadBSC();
        });

        $("#donvi").change(function () {
            loadBSC();
        });

        $("#loainhanvien").change(function () {
            loadBSC();
        });

        $("#btnDongBo").click(function () {
            $.ajax({
                type: "POST",
                url: "ThongKeTrungBinhDiemBSCNV.aspx/dongboDiem",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var output = result.d;
                    if (output == true) {
                        alert("Đồng bộ thành công!");
                        window.location.reload();
                    }
                    else {
                        alert("Đồng bộ thất bại!");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
