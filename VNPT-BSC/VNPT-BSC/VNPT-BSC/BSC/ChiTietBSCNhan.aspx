<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="ChiTietBSCNhan.aspx.cs" Inherits="VNPT_BSC.BSC.ChiTietBSCNhan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- Customize css -->
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>--%>

    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />
    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />
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
                    <a class="btn btn-warning btn-xs" href="NhanBSCDonVi.aspx">
                        <i class="fa fa-reply" aria-hidden="true"></i>
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
                    <label class="control-label col-sm-6">Trạng thái nhận:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span class="label label-default" id="nhanLabel">Chưa nhận</span>
                        <a class="btn btn-success btn-xs" id="updateNhanStatus">Nhận</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Trạng thái nộp:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span class="label label-default" id="chamLabel">Chưa nộp</span>
                        <a class="btn btn-success btn-xs" id="updateChamStatus">Nộp BSC</a>
                        <%--<a class="btn btn-danger btn-xs" id="updateHuyChamStatus">Hủy</a>--%>
                    </div>
                </div>
                <div class="form-group">
                    <%--<label class="control-label col-sm-6">Trạng thái thẩm định:</label>
                    <div class="col-sm-6 form-inline">
                        <span id="kiemdinhLabel" class="label label-default">Chưa thẩm định</span>
                    </div>--%>
                    <label class="control-label col-sm-6">Số lượng KPI đã thẩm định: </label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span><strong id="soluong_kpi_dathamdinh"></strong></span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Đồng ý kết quả thẩm định:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span class="label label-default" id="dongyLabel">Chưa đồng ý</span>
                        <a class="btn btn-success btn-xs" id="updateDongYStatus">Đồng ý</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6 ">Trạng thái kết thúc:</label>
                    <div class="col-sm-6 form-inline padding-top-7">
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
                <div class="col-md-12 col-xs-12 text-center">
                    <!-- Mod start ThangTGM 02092017 - Không cho đơn vị tự chấm bsc -->
                    <%--<a class="btn btn-success" id="saveData">Save</a>--%>
                    <!-- Mod end -->
                </div>
              </div>
          </div>
        </div>
        <!----------------------------------------------------------Phần hồi BSC Giao--------------------------------------------------------------->
            <div id="guiPhanhoi" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content col-md-12">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Gửi Phản Hồi</h4>
                        </div>
                        <input type="hidden" id="edit_kpi_id" />
                                    
                        <div class="modal-body form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-md-4">Tên KPI:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350" id="edit_kpi_ten" readonly/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4">Chỉ tiêu được giao:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350" id="edit_kehoach_duocgiao" readonly/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4 ">Chỉ tiêu đề xuất:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350 " id="edit_kehoach_dexuat" onkeypress="return onlyNumbers(event.charCode || event.keyCode);"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4 ">Lý do đề xuất:</label>
                                <div class="col-md-8">
                                    <textarea id="edit_lydo_dexuat" rows="4" class="form-control">

                                    </textarea>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <a class="btn btn-success" id="edit_btnGui">Gửi</a>
                            <a class="btn btn-default" id="edit_btnHuy">Hủy</a>
                        </div>
                    </div>
                </div>
            </div>
    <!-- End -->
    <!----------------------------------------------------------Phần hồi BSC Giao--------------------------------------------------------------->
            <div id="guiPhanhoiTD" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content col-md-12">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Gửi Phản Hồi Kết Quả Thẩm Định</h4>
                        </div>
                        <input type="hidden" id="td_edit_kpi_id" />
                                    
                        <div class="modal-body form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-md-4">Tên KPI:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350" id="td_edit_kpi_ten" readonly/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4">Kết quả thẩm định:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350" id="td_edit_kqtd" readonly/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4 ">Kết quả thâm định đề xuất:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350 " id="td_edit_kqtd_dexuat" onkeypress="return onlyNumbers(event.charCode || event.keyCode);"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4 ">Lý do đề xuất:</label>
                                <div class="col-md-8">
                                    <textarea id="td_edit_kqtd_lydo" rows="4" class="form-control">

                                    </textarea>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <a class="btn btn-success" id="td_edit_btnGui">Gửi</a>
                            <a class="btn btn-default" id="td_edit_btnHuy">Hủy</a>
                        </div>
                    </div>
                </div>
            </div>
    <!-- End -->
    </div>
<script type="text/javascript">
    var donvigiao = "<%= donvigiao %>";
    var donvinhan = "<%= donvinhan %>";
    var thang = "<%= thang %>";
    var nam = "<%= nam %>";

    function phanHoi(kpi_id) {
        var data = $('#idPhanHoi_' + kpi_id).val();
        var arrDate = data.split("-");
        var kpi_ten = arrDate[1];
        var kehoach_duocgiao = arrDate[2];
        var kehoach_dexuat = arrDate[3];
        var lydo_dexuat = arrDate[4];
        var daxuly_dexuat = arrDate[5];
        $('#edit_kpi_id').val(kpi_id);
        $('#edit_kpi_ten').val(kpi_ten);
        $('#edit_kehoach_duocgiao').val(kehoach_duocgiao);
        $('#edit_kehoach_dexuat').val(kehoach_dexuat);
        $('#edit_lydo_dexuat').val(lydo_dexuat);

        if (daxuly_dexuat == "True") {
            $("#edit_btnGui").hide();
            $("#edit_btnHuy").hide();
        }
        else {
            $("#edit_btnGui").show();
            $("#edit_btnHuy").show();
        }
    }

    function phanHoiTD(kpi_id) {
        var data = $('#idPhanHoiTD_' + kpi_id).val();
        var arrDate = data.split("-");
        var kpi_ten = arrDate[1];
        var ketqua_thamdinh = arrDate[2];
        var thamdinh_dexuat = arrDate[3];
        var thamdinh_lydo_dexuat = arrDate[4];
        var thamdinh_daxuly_dexuat = arrDate[5];
        $('#td_edit_kpi_id').val(kpi_id);
        $('#td_edit_kpi_ten').val(kpi_ten);
        $('#td_edit_kqtd').val(ketqua_thamdinh);
        $('#td_edit_kqtd_dexuat').val(thamdinh_dexuat);
        $('#td_edit_kqtd_lydo').val(thamdinh_lydo_dexuat);

        if (thamdinh_daxuly_dexuat == "True") {
            $("#td_edit_btnGui").hide();
            $("#td_edit_btnHuy").hide();
        }
        else {
            $("#td_edit_btnGui").show();
            $("#td_edit_btnHuy").show();
        }
    }

    function loadDataToPage(donvigiao, donvinhan, thang, nam) {
        /*Hide button*/
        $("#updateNhanStatus").hide();
        $("#updateChamStatus").hide();
        $("#updateHuyChamStatus").hide();
        $("#updateDongYStatus").hide();
        $("#saveData").hide();

        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNhan.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var donvigiao = output.donvigiao;
                var donvinhan = output.donvinhan;
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

                    if (trangthaithamdinh == "True") {
                        $("#updateHuyChamStatus").hide();
                    }
                    else {
                        $("#updateHuyChamStatus").show();
                    }
                }
                else {
                    $("#chamLabel").removeClass("label-success");
                    $("#chamLabel").addClass("label-default");
                    $("#chamLabel").text("Chưa nộp");
                    $("#updateHuyChamStatus").hide();
                    if (trangthainhan == "True") {
                        //$("#updateChamStatus").show();
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
                    "pageLength": 50,
                    "dom": 'Bfrtip',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                            title: 'Chi tiết BSC-KPI ' + thang + "-" + nam
                        },
                        {
                            extend: 'pdfHtml5',
                            title: 'Chi tiết BSC-KPI ' + thang + "-" + nam,
                            orientation: 'landscape',
                            pageSize: 'LEGAL'
                        }
                    ]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateNhanStatus(donvigiao, donvinhan, thang, nam) {
        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNhan.aspx/updateNhanStatus",
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
                        loadDataToPage(donvigiao, donvinhan, thang, nam);
                    });
                }
                else {
                    swal("Error!!!", "Nhận BSC không thành công!!!", "error");
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateChamStatus(donvigiao, donvinhan, thang, nam) {
        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNhan.aspx/updateChamStatus",
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
                        loadDataToPage(donvigiao, donvinhan, thang, nam);
                    });
                }
                else {
                    swal("Error!!!", "Nộp BSC không thành công!!!", "error");
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateDongYStatus(donvigiao, donvinhan, thang, nam) {
        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNhan.aspx/updateDongYStatus",
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
                        loadDataToPage(donvigiao, donvinhan, thang, nam);
                    });
                }
                else {
                    swal("Error!!!", "Cập nhật trạng thái không thành công!!!", "error");
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function updateHuyChamStatus(donvigiao, donvinhan, thang, nam) {
        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCNhan.aspx/updateHuyChamStatus",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var isSuccess = result.d;
                if (isSuccess) {
                    swal({
                        title: "Hủy Nộp BSC thành công!!",
                        text: "",
                        type: "success"
                    },
                    function () {
                        loadDataToPage(donvigiao, donvinhan, thang, nam);
                    });
                }
                else {
                    swal("Error!!!", "Nộp BSC không thành công!!!", "error");
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

        loadDataToPage(donvigiao, donvinhan, thang, nam);

        $("#updateNhanStatus").click(function () {
            updateNhanStatus(donvigiao, donvinhan, thang, nam);
        });

        //$("#updateChamStatus").click(function () {
        //    updateChamStatus(donvigiao, donvinhan, thang, nam);
        //});

        $("#updateHuyChamStatus").click(function () {
            updateHuyChamStatus(donvigiao, donvinhan, thang, nam);
        });

        $("#updateDongYStatus").click(function () {
            updateDongYStatus(donvigiao, donvinhan, thang, nam);
        });

        $("#saveData").click(function () {
            var kpi_detail = [];
            $("#table-kpi > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                var thuchien = $("#thuchien_" + kpi_id).val();
                if (isNaN(thuchien)) {
                    swal("Error!", "Sai định dạng kiểu chữ số!!!", "error");
                    return false;
                }

                if (thuchien == "") {
                    thuchien = 0;
                }
                kpi_detail.push({
                    kpi_id: kpi_id,
                    thuchien: thuchien
                });
            });

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_detail: kpi_detail
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "ChiTietBSCNhan.aspx/saveData",
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
                            loadDataToPage(donvigiao, donvinhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Lưu không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#edit_btnGui").click(function () {
            var kpi_id = $('#edit_kpi_id').val();
            var kehoach_dexuat = "";
            var lydo_dexuat = "";
            kehoach_dexuat = $('#edit_kehoach_dexuat').val();
            lydo_dexuat = $('#edit_lydo_dexuat').val();

            if (isNaN(kehoach_dexuat)) {
                swal("Error!", "Sai định dạng kiểu chữ số!!!", "error");
                return false;
            }

            if (kehoach_dexuat == "") {
                swal("Error!!!", "Vui lòng nhập giá trị chỉ tiêu đề xuất!!!", "error");
                return;
            }

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_id: kpi_id,
                kehoach_dexuat: kehoach_dexuat,
                lydo_dexuat: lydo_dexuat
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "ChiTietBSCNhan.aspx/savePhanHoi",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Phản hồi thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            $("button[data-dismiss=modal]").click();
                            loadDataToPage(donvigiao, donvinhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Phản hồi không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#td_edit_btnGui").click(function () {
            var kpi_id = $('#td_edit_kpi_id').val();
            var thamdinh_dexuat = "";
            var thamdinh_lydo_dexuat = "";
            thamdinh_dexuat = $('#td_edit_kqtd_dexuat').val();
            thamdinh_lydo_dexuat = $('#td_edit_kqtd_lydo').val();

            if (isNaN(thamdinh_dexuat)) {
                swal("Error!", "Sai định dạng kiểu chữ số!!!", "error");
                return false;
            }

            if (thamdinh_dexuat == "") {
                swal("Error!!!", "Vui lòng nhập giá trị chỉ tiêu đề xuất!!!", "error");
                return;
            }

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_id: kpi_id,
                thamdinh_dexuat: thamdinh_dexuat,
                thamdinh_lydo_dexuat: thamdinh_lydo_dexuat
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "ChiTietBSCNhan.aspx/savePhanHoiTD",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Phản hồi thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            $("button[data-dismiss=modal]").click();
                            loadDataToPage(donvigiao, donvinhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Phản hồi không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#edit_btnHuy").click(function () {
            var kpi_id = $('#edit_kpi_id').val();

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_id: kpi_id
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "ChiTietBSCNhan.aspx/huyPhanHoi",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Hủy phản hồi thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            $("button[data-dismiss=modal]").click();
                            loadDataToPage(donvigiao, donvinhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Hủy phản hồi không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#td_edit_btnHuy").click(function () {
            var kpi_id = $('#td_edit_kpi_id').val();

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_id: kpi_id
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "ChiTietBSCNhan.aspx/huyPhanHoiTD",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Hủy phản hồi thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            $("button[data-dismiss=modal]").click();
                            loadDataToPage(donvigiao, donvinhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Hủy phản hồi không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });

</script>
</asp:Content>
