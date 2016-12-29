﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ChiTietBSCNVNhan.aspx.cs" Inherits="VNPT_BSC.BSC.ChiTietBSCNVNhan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- Customize css -->
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>

    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>

    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
          <div class="panel-heading">
            Chi tiết BSC
            <div class="pull-right">
                <div class="btn-group">
                    <a class="btn btn-warning btn-xs" href="NhanBSCNhanVien.aspx">
                        Back
                    </a>
                </div>
            </div>
          </div>
          <div class="panel-body">
              <div class="col-sm-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Ngày áp dụng:</label>
                    <div class="col-sm-6 form-inline">
                        <span><strong id="ngayapdung"></strong></span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Trạng thái nhận:</label>
                    <div class="col-sm-6 form-inline">
                        <span class="label label-default" id="nhanLabel">Chưa nhận</span>
                        <a class="btn btn-success btn-xs" id="updateNhanStatus">Nhận</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Trạng thái nộp:</label>
                    <div class="col-sm-6 form-inline">
                        <span class="label label-default" id="chamLabel">Chưa nộp</span>
                        <a class="btn btn-success btn-xs" id="updateChamStatus">Nộp BSC</a>
                    </div>
                </div>
                <div class="form-group">
                    <%--<label class="control-label col-sm-6">Trạng thái thẩm định:</label>
                    <div class="col-sm-6 form-inline">
                        <span id="kiemdinhLabel" class="label label-default">Chưa thẩm định</span>
                    </div>--%>
                    <label class="control-label col-sm-6">Số lượng KPI đã thẩm định: </label>
                    <div class="col-sm-6 form-inline">
                        <span><strong id="soluong_kpi_dathamdinh"></strong></span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Đồng ý KQTĐ:</label>
                    <div class="col-sm-6 form-inline">
                        <span class="label label-default" id="dongyLabel">Chưa đồng ý</span>
                        <a class="btn btn-success btn-xs" id="updateDongYStatus">Đồng ý</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Trạng thái kết thúc:</label>
                    <div class="col-sm-6 form-inline">
                        <span id="ketthucLabel" class="label label-default">Chưa kết thúc</span>
                    </div>
                </div>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Danh sách KPI
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body" id="gridBSC">
                            
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
                <div class="col-sm-12 text-center">
                    <a class="btn btn-success" id="saveData">Save</a>
                </div>
              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    var nhanviengiao = "<%= nhanviengiao %>";
    var nhanviennhan = "<%= nhanviennhan %>";
    var thang = "<%= thang %>";
    var nam = "<%= nam %>";

    function loadDataToPage(nhanviengiao, nhanviennhan, thang, nam) {
        /*Hide button*/
        $("#updateNhanStatus").hide();
        $("#updateChamStatus").hide();
        $("#updateDongYStatus").hide();
        $("#saveData").hide();

        var requestData = {
            nhanviengiao: nhanviengiao,
            nhanviennhan: nhanviennhan,
            thang: thang,
            nam: nam
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNVNhan.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var nhanviengiao = output.nhanviengiao;
                var nhanviennhan = output.nhanviennhan;
                var thang = output.thang;
                var nam = output.nam;
                var soluong_kpi_dathamdinh = output.soluong_kpi_dathamdinh;
                var soluong_dathamdinh = soluong_kpi_dathamdinh.split("/");
                var trangthaigiao = output.trangthaigiao;
                var trangthainhan = output.trangthainhan;
                var trangthaicham = output.trangthaicham;
                var trangthaithamdinh = output.trangthaithamdinh;
                var trangthaiketthuc = output.trangthaiketthuc;
                var trangthaidongy_kqtd = output.trangthaidongy_kqtd;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table
                // Fill ngày áp dụng
                $("#ngayapdung").text(thang + "/" + nam);
                $("#soluong_kpi_dathamdinh").text(soluong_kpi_dathamdinh);

                // Cập nhật trạng thái nhận
                if (trangthainhan == "True") {
                    $("#nhanLabel").removeClass("label-default");
                    $("#nhanLabel").addClass("label-success");
                    $("#nhanLabel").text("Đã nhận");
                    $("#updateNhanStatus").hide();
                    if (trangthaicham != "True") {
                        $("#saveData").show();
                    }
                    else {
                        $("#saveData").hide();
                    }
                }
                else {
                    $("#nhanLabel").removeClass("label-success");
                    $("#nhanLabel").addClass("label-default");
                    $("#nhanLabel").text("Chưa nhận");
                    $("#updateNhanStatus").show();
                    $("#saveData").hide();
                }

                // Cập nhật trạng thái chấm
                if (trangthaicham == "True") {
                    $("#chamLabel").removeClass("label-default");
                    $("#chamLabel").addClass("label-success");
                    $("#chamLabel").text("Đã nộp");
                    $("#updateChamStatus").hide();
                }
                else {
                    $("#chamLabel").removeClass("label-success");
                    $("#chamLabel").addClass("label-default");
                    $("#chamLabel").text("Chưa nộp");
                    if (trangthainhan == "True") {
                        $("#updateChamStatus").show();
                    }
                    else {
                        $("#updateChamStatus").hide();
                    }
                }

                // Cập nhật trạng thái kết thúc
                if (trangthaiketthuc == "True") {
                    $("#ketthucLabel").removeClass("label-default");
                    $("#ketthucLabel").addClass("label-success");
                    $("#ketthucLabel").text("Đã kết thúc");
                }
                else {
                    $("#ketthucLabel").removeClass("label-success");
                    $("#ketthucLabel").addClass("label-default");
                    $("#ketthucLabel").text("Chưa kết thúc");
                }

                // Cập nhật trạng thái đồng ý thẩm định
                if (trangthaidongy_kqtd == "True") {
                    $("#dongyLabel").removeClass("label-default");
                    $("#dongyLabel").addClass("label-success");
                    $("#dongyLabel").text("Đồng ý");
                }
                else {
                    $("#dongyLabel").removeClass("label-success");
                    $("#dongyLabel").addClass("label-default");
                    $("#dongyLabel").text("Chưa đồng ý");
                    if (soluong_dathamdinh[0] == soluong_dathamdinh[1]) {
                        $("#updateDongYStatus").show();
                    }
                }

                $("#table-kpi").DataTable({
                    "searching": true,
                    "info": true,
                    "pageLength": 50
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateNhanStatus(nhanviengiao, nhanviennhan, thang, nam) {
        var requestData = {
            nhanviengiao: nhanviengiao,
            nhanviennhan: nhanviennhan,
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNVNhan.aspx/updateNhanStatus",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var isSuccess = result.d;
                if (isSuccess) {
                    swal({
                        title: "Nhận BSC thành công!!",
                        text: "",
                        type: "success"
                    },
                    function () {
                        loadDataToPage(nhanviengiao, nhanviennhan, thang, nam);
                    });
                }
                else {
                    swal("Error!!!", "Nhận BSC không thành công!!!", "error");
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateChamStatus(nhanviengiao, nhanviennhan, thang, nam) {
        var requestData = {
            nhanviengiao: nhanviengiao,
            nhanviennhan: nhanviennhan,
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNVNhan.aspx/updateChamStatus",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var isSuccess = result.d;
                if (isSuccess) {
                    swal({
                        title: "Nộp BSC thành công!!",
                        text: "",
                        type: "success"
                    },
                    function () {
                        loadDataToPage(nhanviengiao, nhanviennhan, thang, nam);
                    });
                }
                else {
                    swal("Error!!!", "Nộp BSC không thành công!!!", "error");
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateDongYStatus(nhanviengiao, nhanviennhan, thang, nam) {
        var requestData = {
            nhanviengiao: nhanviengiao,
            nhanviennhan: nhanviennhan,
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNVNhan.aspx/updateDongYStatus",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var isSuccess = result.d;
                if (isSuccess) {
                    swal({
                        title: "Đồng ý với kết quả thẩm định thành công!!",
                        text: "",
                        type: "success"
                    },
                    function () {
                        loadDataToPage(nhanviengiao, nhanviennhan, thang, nam);
                    });
                }
                else {
                    swal("Error!!!", "Cập nhật trạng thái không thành công!!!", "error");
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function onlyNumbers(e) {
        //if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
        return !(e > 31 && (e < 48 || e > 57));
    }

    $(document).ready(function () {
        // Hiển thị danh sách các chức năng của ở BSC
        $(".qlybsc_nv a").click();

        loadDataToPage(nhanviengiao, nhanviennhan, thang, nam);

        $("#updateNhanStatus").click(function () {
            updateNhanStatus(nhanviengiao, nhanviennhan, thang, nam);
        });

        $("#updateChamStatus").click(function () {
            updateChamStatus(nhanviengiao, nhanviennhan, thang, nam);
        });

        $("#updateDongYStatus").click(function () {
            updateDongYStatus(nhanviengiao, nhanviennhan, thang, nam);
        });

        $("#saveData").click(function () {
            var kpi_detail = [];
            $("#table-kpi > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                var thuchien = $("#thuchien_" + kpi_id).val();
                if (thuchien == "") {
                    thuchien = 0;
                }
                kpi_detail.push({
                    kpi_id: kpi_id,
                    thuchien: thuchien
                });
            });

            var requestData = {
                nhanviengiao: nhanviengiao,
                nhanviennhan: nhanviennhan,
                thang: thang,
                nam: nam,
                kpi_detail: kpi_detail
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "ChiTietBSCNVNhan.aspx/saveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Lưu thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            loadDataToPage(nhanviengiao, nhanviennhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Lưu không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });

</script>
</asp:Content>
