<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="ChiTietBSCKiemDinh.aspx.cs" Inherits="VNPT_BSC.BSC.ChiTietBSCKiemDinh" %>
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
                    <a class="btn btn-warning btn-xs" href="KiemDinhBSCDonVi.aspx">
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
                            <i class="fa fa-bar-chart-o fa-fw"></i> Danh sách KPI <span class="red-color" id="ten_dvn"></span>
                            <div class="pull-right">
                                <a class="btn btn-primary btn-xs" id="copyKDtoTD">
                                        T.Hiện => T.Định
                                </a>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body" id="gridBSC">
                            
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
                <div class="col-md-12 col-xs-12 text-center">
                    <a class="btn btn-success" id="saveData">Save</a>
                </div>
              </div>
          </div>
        </div>
        <!----------------------------------------------------------Phản hồi--------------------------------------------------------------->
            <div id="guiPhanhoi" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content col-md-12">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" id="close_phanhoi">&times;</button>
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
                                <label class="control-label col-md-4">Kết quả đề xuất:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350" id="edit_thamdinh_dexuat" readonly/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4 ">Lý do đề xuất:</label>
                                <div class="col-md-8">
                                    <textarea id="edit_lydo_dexuat" rows="4" class="form-control">

                                    </textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4 ">Kết quả cuối cùng:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350 " id="edit_thamdinh_cuoicung" onkeypress="return onlyNumbers(event.charCode || event.keyCode);"/>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <a class="btn btn-success" id="edit_btnXuLy">Xử lý</a>
                        </div>
                    </div>
                </div>
            </div>
    <!-- End Phản hồi -->
    </div>

<script type="text/javascript">
    var donvigiao = "<%= donvigiao %>";
    var donvinhan = "<%= donvinhan %>";
    var donvithamdinh = "<%= donvithamdinh %>";
    var thang = "<%= thang %>";
    var nam = "<%= nam %>";

    function phanHoi(kpi_id) {
        var data = $('#idPhanHoi_' + kpi_id).val();
        var arrDate = data.split("-");
        var kpi_ten = arrDate[1];
        var kqtd = arrDate[2];
        var thamdinh_dexuat = arrDate[3];
        var lydo_dexuat = arrDate[4];
        var daxuly_dexuat = arrDate[5];
        $('#edit_kpi_id').val(kpi_id);
        $('#edit_kpi_ten').val(kpi_ten);
        $('#edit_thamdinh_dexuat').val(thamdinh_dexuat);
        $('#edit_lydo_dexuat').val(lydo_dexuat);
        $('#edit_thamdinh_cuoicung').val(kqtd);

        if (daxuly_dexuat == "True" || daxuly_dexuat == "") {
            $("#edit_btnXuLy").hide();
        }
        else if (daxuly_dexuat == "False") {
            $("#edit_btnXuLy").show();
        }
    }

    function loadDataToPage(donvigiao, donvinhan, thang, nam, donvithamdinh) {
        /*Hide button*/
        $("#updateThamDinhStatus").hide();
        $("#saveData").hide();

        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam,
            donvithamdinh: donvithamdinh
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCKiemDinh.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var donvigiao = output.donvigiao;
                var donvinhan = output.donvinhan;
                var donvinhan_ten = output.donvinhan_ten;
                var thang = output.thang;
                var nam = output.nam;
                var soluong_kpi_dathamdinh = output.soluong_kpi_dathamdinh;
                var soluong_dathamdinh = soluong_kpi_dathamdinh.split("/");
                var trangthaigiao = output.trangthaigiao;
                var trangthainhan = output.trangthainhan;
                var trangthaicham = output.trangthaicham;
                var trangthaiketthuc = output.trangthaiketthuc;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table
                $("#ten_dvn").html(donvinhan_ten);
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
                if (soluong_dathamdinh[0] == soluong_dathamdinh[1]) {
                    $("#kiemdinhLabel").removeClass("label-default");
                    $("#kiemdinhLabel").addClass("label-success");
                    $("#kiemdinhLabel").text("Đã kiểm định");
                    $("#saveData").hide();
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
                }
                else {
                    $("#ketthucLabel").removeClass("label-success");
                    $("#ketthucLabel").addClass("label-default");
                    $("#ketthucLabel").text("Chưa kết thúc");
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

    function updateKiemDinhStatus(donvigiao, donvinhan, thang, nam, donvithamdinh) {
        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam,
            donvithamdinh: donvithamdinh
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietBSCKiemDinh.aspx/updateKiemDinhStatus",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var isSuccess = result.d;
                if (isSuccess) {
                    swal({
                        title: "Kiểm định BSC thành công!!",
                        text: "",
                        type: "success"
                    },
                    function () {
                        loadDataToPage(donvigiao, donvinhan, thang, nam, donvithamdinh);
                    });
                }
                else {
                    swal("Error!!!", "Kiểm định BSC không thành công!!!", "error");
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

        loadDataToPage(donvigiao, donvinhan, thang, nam, donvithamdinh);

        $("#updateThamDinhStatus").click(function () {
            updateKiemDinhStatus(donvigiao, donvinhan, thang, nam, donvithamdinh);
        });

        $("#saveData").click(function () {
            var kpi_detail = [];
            $("#table-kpi > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                var thamdinh = $("#thamdinh_" + kpi_id).val();
                if (thamdinh == "") {
                    thamdinh = 0;
                }
                kpi_detail.push({
                    kpi_id: kpi_id,
                    thamdinh: thamdinh
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
                url: "ChiTietBSCKiemDinh.aspx/saveData",
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
                            loadDataToPage(donvigiao, donvinhan, thang, nam, donvithamdinh);
                        });
                    }
                    else {
                        swal("Error!!!", "Lưu không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#edit_btnXuLy").click(function () {
            var kpi_id = $('#edit_kpi_id').val();
            var thamdinh_cuoicung = "";
            thamdinh_cuoicung = $('#edit_thamdinh_cuoicung').val();

            if (thamdinh_cuoicung == "") {
                swal("Error!!!", "Vui lòng nhập giá trị kế hoạch cuối cùng!!!", "error");
                return;
            }

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_id: kpi_id,
                thamdinh_cuoicung: thamdinh_cuoicung,
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "ChiTietBSCKiemDinh.aspx/xulyPhanHoi",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Xử lý phản hồi thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            $("#close_phanhoi").click();
                            loadDataToPage(donvigiao, donvinhan, thang, nam, donvithamdinh);
                        });
                    }
                    else {
                        swal("Error!!!", "Xử lý phản hồi không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#copyKDtoTD").click(function () {
            if ($("#table-kpi").length > 0) {
                $("#table-kpi tbody tr").each(function () {
                    var tr_id = $(this).data("id");
                    var thuchien = $(this).children('td:eq(5)').text();
                    $("#thamdinh_" + tr_id).val(thuchien);
                })
            }
        });
    });

</script>
</asp:Content>
