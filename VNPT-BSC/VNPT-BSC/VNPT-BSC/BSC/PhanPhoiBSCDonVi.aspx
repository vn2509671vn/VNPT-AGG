﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="PhanPhoiBSCDonVi.aspx.cs" Inherits="VNPT_BSC.BSC.PhanPhoiBSCDonVi" %>
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
            <h3 class="panel-title">GIAO BSC CHO ĐƠN VỊ</h3>
          </div>
          <div class="panel-body">
              <%--<div class="col-sm-3">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Danh Sách Đơn Vị</h3>
                        </div>
                        <ul class="list-group">
                            <% for(int i = 0; i < dtDonvi.Rows.Count; i++){ %>
                                <%
                                    string donvi_id =  dtDonvi.Rows[i]["donvi_id"].ToString();
                                    string donvi_ten =  dtDonvi.Rows[i]["donvi_ten"].ToString();
                                %>
                                <a href="#" class="list-group-item list-group-item-info text-left" onclick="fillDataBSC('<%= donvi_ten%>','<%= donvi_id%>')"><%= donvi_ten%></a>
                            <% } %>
                        </ul>
                    </div>
              </div>--%>
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-md-3 col-xs-3">Thời gian:</label>
                    <div class="col-md-6 col-xs-6 form-inline">
                        <select class="form-control" id="month" onchange="changeInputData()">
                            <% for(int nMonth = 1; nMonth <= 12; nMonth++){ %>
                                <option><%= nMonth %></option>
                            <% } %>
                        </select>
                        <select class="form-control" id="year" onchange="changeInputData()">
                            <% for(int nYear = 2016; nYear <= 2100; nYear++){ %>
                                <option><%= nYear %></option>
                            <% } %>
                        </select>
                        <a class="btn btn-warning" id="getCurrentDate">Hiện tại</a>
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-3 col-xs-3">Đơn vị nhận:</label>
                    <div class="col-md-4 col-xs-12">
                        <select class="form-control" id="donvi" onchange="changeInputData()">
                        <% for(int i = 0; i < dtDonvi.Rows.Count; i++){ %>
                            <%
                                string donvi_id =  dtDonvi.Rows[i]["donvi_id"].ToString();
                                string donvi_ten =  dtDonvi.Rows[i]["donvi_ten"].ToString();
                            %>
                            <option value="<%= donvi_id%>"><%= donvi_ten%></option>
                        <% } %>
                        </select>
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="control-label col-sm-3">Trạng thái giao:</label>
                    <div class="col-sm-8 form-inline">
                        <span class="label label-default" id="giaoLabel">Chưa giao</span>
                        <a class="btn btn-success btn-xs" id="updateGiaoStatus">Giao</a>
                        <a class="btn btn-danger btn-xs" id="updateHuyGiaoStatus">Hủy</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Trạng thái nhận:</label>
                    <div class="col-sm-8 form-inline">
                        <span id="nhanLabel" class="label label-default">Chưa nhận</span>
                    </div>
                </div>
                <%--<div class="form-group">
                    <label class="control-label col-sm-3">Trạng thái kiểm định:</label>
                    <div class="col-sm-8 form-inline">
                        <span id="kiemdinhLabel" class="label label-default">Chưa kiểm định</span>
                    </div>
                </div>--%>
                <div class="form-group">
                    <label class="control-label col-sm-3">Trạng thái kết thúc:</label>
                    <div class="col-sm-8 form-inline">
                        <span id="ketthucLabel" class="label label-default">Chưa kết thúc</span>
                    </div>
                </div>
                <div class="row">
                      <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Danh sách KPI
                            <div class="pull-right">
                                <div class="btn-group">
                                    <a class="btn btn-primary btn-xs" data-toggle="modal" data-target="#listBSC" id="btnMauBSC">
                                        Mẫu BSC
                                    </a>
                                    <!-- Modal for BSC list -->
                                    <div id="listBSC" class="modal fade" role="dialog">
                                      <div class="modal-dialog">

                                        <!-- Modal content-->
                                        <div class="modal-content">
                                          <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Danh sách mẫu BSC</h4>
                                          </div>
                                          <div class="modal-body list-BSC">
                                            <% for(int iBSC = 0; iBSC < dtBSC.Rows.Count; iBSC++){ %>
                                                <%
                                                    string thang =  dtBSC.Rows[iBSC]["thang"].ToString();
                                                    string nam =  dtBSC.Rows[iBSC]["nam"].ToString();
                                                    string noidung =  dtBSC.Rows[iBSC]["content"].ToString();
                                                    string szNguoitao = dtBSC.Rows[iBSC]["nhanvien_id"].ToString();
                                                    string loaimau = dtBSC.Rows[iBSC]["loai_id"].ToString();
                                                %>
                                              <div class="radio">
                                                <label><input type="radio" name="optradioBSC" data-thang="<%=thang %>" data-nam="<%=nam %>" data-nguoitao="<%=szNguoitao %>" data-loaimau="<%=loaimau %>"><%= noidung%></label>
                                              </div>
                                          <% } %>
                                          </div>
                                          <div class="modal-footer">
                                            <button type="button" class="btn btn-success" data-dismiss="modal" id="loadBSC">Load</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                          </div>
                                        </div>

                                      </div>
                                    </div>
                                </div>
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
                                <label class="control-label col-md-4">Chỉ tiêu đề xuất:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350" id="edit_kehoach_dexuat" readonly/>
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
                                <label class="control-label col-md-4 ">Chỉ tiêu cuối cùng:</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control fix-width-350 " id="edit_kehoach_cuoicung" onkeypress="return onlyNumbers(event.charCode || event.keyCode);"/>
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
    var donvigiao = "<%= donvichuquan%>";
    var valDonvinhan = "<%=valDonvinhan%>";
    var valThang = "<%=valThang%>";
    var valNam = "<%=valNam%>";

    function setValueFromURL(valDonvinhan, valThang, valNam) {
        if (valDonvinhan != "" && valThang != "" && valNam != "") {
            $("#month").val(valThang);
            $("#year").val(valNam);
            $("#donvi").val(valDonvinhan);
        }
        changeInputData();
    }

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
        $('#edit_kehoach_dexuat').val(kehoach_dexuat);
        $('#edit_lydo_dexuat').val(lydo_dexuat);

        if (daxuly_dexuat == "True" || daxuly_dexuat == "") {
            $("#edit_btnXuLy").hide();
        }
        else if (daxuly_dexuat == "False") {
            $("#edit_btnXuLy").show();
        }
    }

    function getCurrentDate() {
        var curMonth = "<%= DateTime.Now.ToString("%M") %>";
        var curYear = "<%= DateTime.Now.ToString("yyyy") %>";
        $("#month").val(curMonth);
        $("#year").val(curYear);
        var donvinhan = $("#donvi").val();
        getBSCByCondition(donvigiao, donvinhan, curMonth, curYear);
    }

    function getBSCByCondition(id_dv_giao, id_dv_nhan, thang, nam) {
        /*Hide button*/
        $("#updateGiaoStatus").hide();
        $("#updateHuyGiaoStatus").hide();
        $("#updateKTStatus").hide();

        var requestData = {
            id_dv_giao: id_dv_giao,
            id_dv_nhan: id_dv_nhan,
            thang: thang,
            nam: nam
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "PhanPhoiBSCDonVi.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var donvigiao = output.donvigiao;
                var donvinhan = output.donvinhan;
                var donvithamdinh = output.donvithamdinh;
                var trangthaigiao = output.trangthaigiao;
                var trangthainhan = output.trangthainhan;
                var trangthaithamdinh = output.trangthaithamdinh;
                var trangthaiketthuc = output.trangthaiketthuc;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table
                $("#donvithamdinh").val(donvithamdinh); // Fill to đơn vị thẩm định
                // Cập nhật trạng thái giao
                if (trangthaigiao == "True") {
                    $("#giaoLabel").removeClass("label-default");
                    $("#giaoLabel").addClass("label-success");
                    $("#giaoLabel").text("Đã giao");
                    $("#updateGiaoStatus").hide();
                    $("#btnMauBSC").hide();
                    $("#saveData").hide();
                    if (trangthainhan != "True") {
                        $("#updateHuyGiaoStatus").show();
                    }
                    else {
                        $("#updateHuyGiaoStatus").hide();
                    }
                }
                else {
                    $("#giaoLabel").removeClass("label-success");
                    $("#giaoLabel").addClass("label-default");
                    $("#giaoLabel").text("Chưa giao");
                    $("#updateGiaoStatus").show();
                    $("#btnMauBSC").show();
                    $("#saveData").show();
                    $("#updateHuyGiaoStatus").hide();
                }

                // Cập nhật trạng thái nhận
                if (trangthainhan == "True") {
                    $("#nhanLabel").removeClass("label-default");
                    $("#nhanLabel").addClass("label-success");
                    $("#nhanLabel").text("Đã nhận");
                }
                else {
                    $("#nhanLabel").removeClass("label-success");
                    $("#nhanLabel").addClass("label-default");
                    $("#nhanLabel").text("Chưa nhận");
                }

                // Cập nhật trạng thái kết thúc
                if (trangthaiketthuc == "True") {
                    $("#ketthucLabel").removeClass("label-default");
                    $("#ketthucLabel").addClass("label-success");
                    $("#ketthucLabel").text("Đã kết thúc");
                    $("#updateKTStatus").hide();
                }
                else {
                    $("#ketthucLabel").removeClass("label-success");
                    $("#ketthucLabel").addClass("label-default");
                    $("#ketthucLabel").text("Chưa kết thúc");
                    if (trangthaithamdinh == "True") {
                        $("#updateKTStatus").show();
                    }
                    else {
                        $("#updateKTStatus").hide();
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

    function changeInputData() {
        var thang = $("#month").val();
        var nam = $("#year").val();
        var donvinhan = $("#donvi").val();
        getBSCByCondition(donvigiao, donvinhan, thang, nam);
    }

    function onlyNumbers(e) {
        //if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
        return !(e > 31 && (e < 48 || e > 57) && e != 46);
    }

    $(document).ready(function () {
        // Load data lần đầu tiên
        setValueFromURL(valDonvinhan, valThang, valNam);

        /*Hide button*/
        $("#updateGiaoStatus").hide();
        $("#updateHuyGiaoStatus").hide();
        $("#updateKTStatus").hide();
        /*Get current date when user click Now buttion*/
        $("#getCurrentDate").click(function () {
            getCurrentDate();
        });

        $("#loadBSC").click(function () {
            var thang = $("input[name=optradioBSC]:checked").attr("data-thang");
            var nam = $("input[name=optradioBSC]:checked").attr("data-nam");
            var szNguoitao = $("input[name=optradioBSC]:checked").attr("data-nguoitao");
            var loaimau = $("input[name=optradioBSC]:checked").attr("data-loaimau");
            var requestData = {
                thang: thang,
                nam: nam,
                nguoitao: szNguoitao,
                loaiMauBSC: loaimau
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCDonVi.aspx/loadBSC",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var kpiHTML = result.d;
                    $("#gridBSC").html(kpiHTML);
                    $("#table-kpi").DataTable({
                        "searching": true,
                        "info": true,
                        "pageLength": 50
                    });
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#saveData").click(function () {
            var donvinhan = $("#donvi").val();
            var thang = $("#month").val();
            var nam = $("#year").val();
            var loaimau = $("#table-kpi").attr("data-loaimau");
            if (donvinhan == "") {
                swal("Error!", "Vui lòng nhập các trường bắt buộc!!!", "error");
                return false;
            }

            var kpi_detail = [];
            $("#table-kpi > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                var tytrong = $("#tytrong_" + kpi_id).val();
                var dvt = $("#dvt_" + kpi_id).val();
                var kehoach = $("#kehoach_" + kpi_id).val();
                if (isNaN(kehoach)) {
                    swal("Error!", "Sai định dạng kiểu chữ số!!!", "error");
                    return false;
                }

                if (kehoach == "") {
                    kehoach = 0;
                }
                var donvithamdinh = $("#dvtd_" + kpi_id).val();
                kpi_detail.push({
                    kpi_id: kpi_id,
                    tytrong: tytrong,
                    dvt: dvt,
                    kehoach: kehoach,
                    donvithamdinh: donvithamdinh
                });
            });

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_detail: kpi_detail,
                loaimau: loaimau
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCDonVi.aspx/saveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal("Lưu thành công!!", "", "success");
                    }
                    else {
                        swal("Error!!!", "Lưu không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#updateGiaoStatus").click(function () {
            var donvinhan = $("#donvi").val();
            var thang = $("#month").val();
            var nam = $("#year").val();

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCDonVi.aspx/giaoBSC",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Giao BSC thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            getBSCByCondition(donvigiao, donvinhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Giao BSC không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#updateHuyGiaoStatus").click(function () {
            var donvinhan = $("#donvi").val();
            var thang = $("#month").val();
            var nam = $("#year").val();

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCDonVi.aspx/huygiaoBSC",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Hủy BSC thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            getBSCByCondition(donvigiao, donvinhan, thang, nam);
                        });
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#edit_btnXuLy").click(function () {
            var donvinhan = $("#donvi").val();
            var thang = $("#month").val();
            var nam = $("#year").val();
            var kpi_id = $('#edit_kpi_id').val();
            var kehoach_cuoicung = "";
            kehoach_cuoicung = $('#edit_kehoach_cuoicung').val();

            if (isNaN(kehoach_cuoicung)) {
                swal("Error!!!", "Sai định dạng kiểu chữ số!!!", "error");
                return;
            }

            if (kehoach_cuoicung == "") {
                swal("Error!!!", "Vui lòng nhập giá trị chỉ tiêu cuối cùng!!!", "error");
                return;
            }

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_id: kpi_id,
                kehoach_cuoicung: kehoach_cuoicung,
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCDonVi.aspx/xulyPhanHoi",
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
                            getBSCByCondition(donvigiao, donvinhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Xử lý phản hồi không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
