<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="ChiTietBSCNVKiemDinh.aspx.cs" Inherits="VNPT_BSC.BSC.ChiTietBSCNVKiemDinh" %>
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
                    <a class="btn btn-warning btn-xs" href="KiemDinhBSCNhanVien.aspx">
                        Back
                    </a>
                </div>
            </div>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Ngày áp dụng:</label>
                    <div class="col-sm-6 form-inline padding-top-7">
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
                    <label class="control-label col-sm-6">Trạng thái thẩm định:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span id="kiemdinhLabel" class="label label-default">Chưa thẩm định</span>
                        <a class="btn btn-success btn-xs" id="updateThamDinhStatus">Duyệt</a>
                        <a class="btn btn-danger btn-xs" id="updateHuyTDStatus">Hủy</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Trạng thái kết thúc:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span id="ketthucLabel" class="label label-default">Chưa kết thúc</span>
                    </div>
                </div>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Danh sách KPI <span class="red-color" id="ten_nv"></span>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body" id="gridBSC">
                            
                        </div>
                        <!-- /.panel-body -->
                        <div class="panel-footer">
                            <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <label class="text-center col-sm-12">KÝ XÁC NHẬN CỦA CÁ NHÂN NHẬN VIỆC</label>
                                    <div class="col-sm-12 form-inline padding-top-7 text-center">
                                        <span id="nhanvienky" class="red-color">Chưa ký</span>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <label class="text-center col-sm-12">LÃNH ĐẠO ĐƠN VỊ KÝ GIAO VIỆC</label>
                                    <div class="col-sm-12 form-inline padding-top-7 text-center">
                                        <span id="lanhdaoky" class="red-color">Chưa ký</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <label class="text-center col-sm-12">KÝ XÁC NHẬN KẾT QUẢ ĐẠT ĐƯỢC CỦA CÁ NHÂN</label>
                                    <div class="col-sm-12 form-inline padding-top-7 text-center">
                                        <span id="nhanvienkyxacnhan" class="red-color">Chưa ký</span>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <label class="text-center col-sm-12">LÃNH ĐẠO ĐƠN VỊ KÝ XÁC NHẬN KẾT QUẢ</label>
                                    <div class="col-sm-12 form-inline padding-top-7 text-center">
                                        <span id="lanhdaokyxacnhan" class="red-color">Chưa ký</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-xs-12 text-center">
                    <a class="btn btn-success" id="saveData">Save</a>
                </div>
              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    var nhanviengiao = "<%= nhanviengiao %>";
    var nhanviennhan = "<%= nhanviennhan %>";
    var nhanvienthamdinh = "<%= nhanvienthamdinh %>";
    var thang = "<%= thang %>";
    var nam = "<%= nam %>";

    function loadDataToPage(nhanviengiao, nhanviennhan, thang, nam, nhanvienthamdinh) {
        /*Hide button*/
        $("#updateThamDinhStatus").hide();
        $("#updateHuyTDStatus").hide();
        $("#saveData").hide();

        var requestData = {
            nhanviengiao: nhanviengiao,
            nhanviennhan: nhanviennhan,
            thang: thang,
            nam: nam,
            nhanvienthamdinh: nhanvienthamdinh
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNVKiemDinh.aspx/loadBSCByCondition",
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
                var trangthaiketthuc = output.trangthaiketthuc;
                var trangthaidongy_kqtd = output.trangthaidongy_kqtd;
                var nv_nhan = output.ten_nv_nhan;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table
                $("#ten_nv").html(nv_nhan);
                // Fill ngày áp dụng
                $("#ngayapdung").text(thang + "/" + nam);
                
                if (trangthaigiao == "True") {
                    $("#lanhdaoky").text("Đã ký");
                }

                if (trangthainhan == "True") {
                    $("#nhanvienky").text("Đã ký");
                }

                if (trangthaidongy_kqtd == "True") {
                    $("#nhanvienkyxacnhan").text("Đã ký");
                }

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
                if (soluong_dathamdinh[0] == soluong_dathamdinh[1]) {
                    $("#kiemdinhLabel").removeClass("label-default");
                    $("#kiemdinhLabel").addClass("label-success");
                    $("#kiemdinhLabel").text("Đã kiểm định");
                    $("#saveData").hide();
                    // Cập nhật trạng thái kiểm định
                    if (trangthaidongy_kqtd == "True") {
                        $("#updateHuyTDStatus").hide();
                    }
                    else {
                        $("#updateHuyTDStatus").show();
                    }
                }
                else {
                    $("#kiemdinhLabel").removeClass("label-success");
                    $("#kiemdinhLabel").addClass("label-default");
                    $("#kiemdinhLabel").text("Chưa kiểm định");
                    if (trangthaicham == "True") {
                        $("#updateThamDinhStatus").show();
                        $("#saveData").show();
                    }
                    else {
                        $("#updateThamDinhStatus").hide();
                        $("#saveData").hide();
                    }
                }

                // Cập nhật trạng thái kết thúc
                if (trangthaiketthuc == "True") {
                    $("#ketthucLabel").removeClass("label-default");
                    $("#ketthucLabel").addClass("label-success");
                    $("#ketthucLabel").text("Đã kết thúc");
                    $("#lanhdaokyxacnhan").text("Đã ký");
                }
                else {
                    $("#ketthucLabel").removeClass("label-success");
                    $("#ketthucLabel").addClass("label-default");
                    $("#ketthucLabel").text("Chưa kết thúc");
                }

                $("#table-kpi").DataTable({
                    "bSort": false,
                    "pageLength": 50
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateKiemDinhStatus(nhanviengiao, nhanviennhan, thang, nam, nhanvienthamdinh, trangthaithamdinh) {
        var requestData = {
            nhanviengiao: nhanviengiao,
            nhanviennhan: nhanviennhan,
            thang: thang,
            nam: nam,
            nhanvienthamdinh: nhanvienthamdinh,
            trangthaithamdinh: trangthaithamdinh
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNVKiemDinh.aspx/updateKiemDinhStatus",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var isSuccess = result.d;
                if (isSuccess) {
                    if (trangthaithamdinh == 1) {
                        swal({
                            title: "Kiểm định BSC thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            loadDataToPage(nhanviengiao, nhanviennhan, thang, nam, nhanvienthamdinh);
                        });
                    }
                    else if (trangthaithamdinh == 0) {
                        swal({
                            title: "Hủy kiểm định BSC thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            loadDataToPage(nhanviengiao, nhanviennhan, thang, nam, nhanvienthamdinh);
                        });
                    }
                }
                else {
                    if (trangthaithamdinh) {
                        swal("Error!!!", "Kiểm định BSC không thành công!!!", "error");
                    }
                    else {
                        swal("Error!!!", "Hủy kiểm định BSC không thành công!!!", "error");
                    }
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function onlyNumbers(e) {
        //if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
        return !(e > 31 && (e < 48 || e > 57) && e != 46);
    }

    $(document).ready(function () {

        loadDataToPage(nhanviengiao, nhanviennhan, thang, nam, nhanvienthamdinh);

        $("#updateThamDinhStatus").click(function () {
            updateKiemDinhStatus(nhanviengiao, nhanviennhan, thang, nam, nhanvienthamdinh, 1);
        });

        $("#updateHuyTDStatus").click(function () {
            updateKiemDinhStatus(nhanviengiao, nhanviennhan, thang, nam, nhanvienthamdinh, 0);
        });

        $("#saveData").click(function () {
            var kpi_detail = [];
            var errFormatNumber = false;
            $("#table-kpi > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                if (kpi_id == 999999) {
                    return;
                }
                var thamdinh = $("#thamdinh_" + kpi_id).val();
                if (isNaN(thamdinh)) {
                    swal("Error!", "Sai định dạng kiểu chữ số!!!", "error");
                    errFormatNumber = true;
                    return false;
                }
                if (thamdinh == "") {
                    thamdinh = 0;
                }
                kpi_detail.push({
                    kpi_id: kpi_id,
                    thamdinh: thamdinh
                });
            });

            if (errFormatNumber) {
                return false;
            }

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
                url: "ChiTietBSCNVKiemDinh.aspx/saveData",
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
                            loadDataToPage(nhanviengiao, nhanviennhan, thang, nam, nhanvienthamdinh);
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
