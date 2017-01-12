﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="ChiTietBSCNghiemThu.aspx.cs" Inherits="VNPT_BSC.BSC.ChiTietBSCNghiemThu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- Customize css -->
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>--%>

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
            Chi tiết BSC
            <div class="pull-right">
                <div class="btn-group">
                    <a class="btn btn-warning btn-xs" href="NghiemThuBSCDonVi.aspx">
                        Back
                    </a>
                </div>
            </div>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Ngày áp dụng:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span><strong id="ngayapdung"></strong></span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Trạng thái nộp:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span class="label label-default" id="chamLabel">Chưa nộp</span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Đồng ý kết quả thẩm định:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span id="dongyLabel" class="label label-default">Chưa đồng ý</span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Trạng thái kết thúc:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span id="ketthucLabel" class="label label-default">Chưa kết thúc</span>
                        <a class="btn btn-success btn-xs" id="updateKetThucStatus">Kết thúc</a>
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
              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    var donvigiao = "<%= donvigiao %>";
    var donvinhan = "<%= donvinhan %>";
    var thang = "<%= thang %>";
    var nam = "<%= nam %>";

    function loadDataToPage(donvigiao, donvinhan, thang, nam) {
        /*Hide button*/
        $("#updateKetThucStatus").hide();

        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNghiemThu.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var donvigiao = output.donvigiao;
                var donvinhan = output.donvinhan;
                var ten_dvn = output.ten_dvn;
                var thang = output.thang;
                var nam = output.nam;
                var trangthaigiao = output.trangthaigiao;
                var trangthainhan = output.trangthainhan;
                var trangthaicham = output.trangthaicham;
                var trangthaidongy_kqtd = output.trangthaidongy_kqtd;
                var trangthaiketthuc = output.trangthaiketthuc;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table
                // Fill ngày áp dụng
                $("#ngayapdung").text(thang + "/" + nam);

                // Cập nhật trạng thái chấm
                if (trangthaicham == "True") {
                    $("#chamLabel").removeClass("label-default");
                    $("#chamLabel").addClass("label-success");
                    $("#chamLabel").text("Đã nộp");
                }
                else {
                    $("#chamLabel").removeClass("label-success");
                    $("#chamLabel").addClass("label-default");
                    $("#chamLabel").text("Chưa nộp");
                }

                // Cập nhật trạng thái kiểm định
                if (trangthaidongy_kqtd == "True") {
                    $("#dongyLabel").removeClass("label-default");
                    $("#dongyLabel").addClass("label-success");
                    $("#dongyLabel").text("Đã đồng ý");
                }
                else {
                    $("#dongyLabel").removeClass("label-success");
                    $("#dongyLabel").addClass("label-default");
                    $("#dongyLabel").text("Chưa đồng ý");
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
                    if (trangthaidongy_kqtd == "True") {
                        $("#updateKetThucStatus").show();
                    }
                    else {
                        $("#updateKetThucStatus").hide();
                    }
                }

                $("#table-kpi").DataTable({
                    "order": [],
                    "pageLength": 50,
                    "dom": 'Bfrtip',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                            title: 'Nghiệm thu đơn vị ' + ten_dvn
                        },
                        {
                            extend: 'pdfHtml5',
                            title: 'Nghiệm thu đơn vị ' + ten_dvn
                        }
                    ],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateKetThucStatus(donvigiao, donvinhan, thang, nam) {
        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNghiemThu.aspx/updateKetThucStatus",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var isSuccess = result.d;
                if (isSuccess) {
                    swal({
                        title: "Kết thúc BSC thành công!!",
                        text: "",
                        type: "success"
                    },
                    function () {
                        loadDataToPage(donvigiao, donvinhan, thang, nam);
                    });
                }
                else {
                    swal("Error!!!", "Kết thúc BSC không thành công!!!", "error");
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {

        loadDataToPage(donvigiao, donvinhan, thang, nam);

        $("#updateKetThucStatus").click(function () {
            updateKetThucStatus(donvigiao, donvinhan, thang, nam);
        });
    });

</script>

</asp:Content>
