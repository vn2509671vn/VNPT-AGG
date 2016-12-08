<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PhanPhoiBSCNhanVien.aspx.cs" Inherits="VNPT_BSC.BSC.PhanPhoiBSCNhanVien" %>
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
            <h3 class="panel-title">GIAO BSC CHO NHÂN VIÊN</h3>
          </div>
          <div class="panel-body">
              <div class="col-sm-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-3">Nhân viên nhận:</label>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" list="danhsachnhanvien" size="50" id="nhanviennhan"/>
                        <datalist id="danhsachnhanvien">
                            <% for(int i = 0; i < dtNhanVien.Rows.Count; i++){ %>
                                <%
                                    string nhanvien_taikhoan =  dtNhanVien.Rows[i]["nhanvien_taikhoan"].ToString();
                                    string nhanvien_hoten =  dtNhanVien.Rows[i]["nhanvien_hoten"].ToString();
                                %>
                                <option value="<%= nhanvien_taikhoan%>"><%= nhanvien_hoten%></option>
                            <% } %>
                        </datalist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Nhân viên thẩm định:</label>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" list="danhsachnhanvienthamdinh" size="50" id="nhanvienthamdinh"/>
                        <datalist id="danhsachnhanvienthamdinh">
                        <% for(int i = 0; i < dtFullNV.Rows.Count; i++){ %>
                            <%
                                string nhanvien_taikhoan =  dtFullNV.Rows[i]["nhanvien_taikhoan"].ToString();
                                string nhanvien_hoten =  dtFullNV.Rows[i]["nhanvien_hoten"].ToString();
                            %>
                            <option value="<%= nhanvien_taikhoan%>"><%= nhanvien_hoten%></option>
                        <% } %>
                        </datalist>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Thời gian:</label>
                    <div class="col-sm-6 form-inline">
                        <select class="form-control" id="month" onchange="changeInputData()">
                            <% for(int nMonth = 1; nMonth <= 12; nMonth++){ %>
                                <option><%= nMonth %></option>
                            <% } %>
                        </select>
                        <select class="form-control" id="year" onchange="changeInputData()">
                            <% for(int nYear = 1900; nYear <= 2100; nYear++){ %>
                                <option><%= nYear %></option>
                            <% } %>
                        </select>
                        <a class="btn btn-warning" id="getCurrentDate">Hiện tại</a>
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
                <div class="form-group">
                    <label class="control-label col-sm-3">Trạng thái kiểm định:</label>
                    <div class="col-sm-8 form-inline">
                        <span id="kiemdinhLabel" class="label label-default">Chưa kiểm định</span>
                    </div>
                </div>
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
                                                %>
                                              <div class="radio">
                                                <label><input type="radio" name="optradioBSC" data-thang="<%=thang %>" data-nam="<%=nam %>"><%= noidung%></label>
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
                <div class="col-sm-12 text-center">
                    <a class="btn btn-success" id="saveData">Save</a>
                </div>
             </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    var nhanviengiao = "<%=nhanvienquanly%>";
    function getCurrentDate() {
        var curMonth = "<%= DateTime.Now.ToString("MM") %>";
        var curYear = "<%= DateTime.Now.ToString("yyyy") %>";
        $("#month").val(curMonth);
        $("#year").val(curYear);
        var nhanviennhan = $("#nhanviennhan").val();
        getBSCByCondition(nhanviengiao, nhanviennhan, curMonth, curYear);
    }

    function getBSCByCondition(id_nv_giao, nv_nhan, thang, nam) {
        /*Hide button*/
        $("#updateGiaoStatus").hide();
        $("#updateHuyGiaoStatus").hide();

        var requestData = {
            id_nv_giao: id_nv_giao,
            nv_nhan: nv_nhan,
            thang: thang,
            nam: nam
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "PhanPhoiBSCNhanVien.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var nhanviengiao = output.nhanviengiao;
                var nhanviennhan = output.nhanviennhan;
                var nhanvienthamdinh = output.nhanvienthamdinh;
                var trangthaigiao = output.trangthaigiao;
                var trangthainhan = output.trangthainhan;
                var trangthaithamdinh = output.trangthaithamdinh;
                var trangthaiketthuc = output.trangthaiketthuc;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table
                $("#nhanvienthamdinh").val(nhanvienthamdinh); // Fill to đơn vị thẩm định
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

                // Cập nhật trạng thái kiểm định
                if (trangthaithamdinh == "True") {
                    $("#kiemdinhLabel").removeClass("label-default");
                    $("#kiemdinhLabel").addClass("label-success");
                    $("#kiemdinhLabel").text("Đã kiểm định");
                }
                else {
                    $("#kiemdinhLabel").removeClass("label-success");
                    $("#kiemdinhLabel").addClass("label-default");
                    $("#kiemdinhLabel").text("Chưa kiểm định");
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

    function fillDataBSC() {
        var nhanviennhan = $("#nhanviennhan").val();
        getBSCByCondition(nhanviengiao, nhanviennhan, thang, nam);
    }

    function changeInputData() {
        var thang = $("#month").val();
        var nam = $("#year").val();
        var nhanviennhan = $("#nhanviennhan").val();
        getBSCByCondition(nhanviengiao, nhanviennhan, thang, nam);
    }

    function onlyNumbers(e) {
        if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
    }

    $(document).ready(function () {
        /*Hide button*/
        $("#updateGiaoStatus").hide();
        $("#updateHuyGiaoStatus").hide();

        /*Get current date when user click Now buttion*/
        $("#getCurrentDate").click(function () {
            getCurrentDate();
        });

        $("#nhanviennhan").focusout(function () {
            changeInputData();
        });

        $("#loadBSC").click(function () {
            var thang = $("input[name=optradioBSC]:checked").attr("data-thang");
            var nam = $("input[name=optradioBSC]:checked").attr("data-nam");
            var requestData = {
                thang: thang,
                nam: nam,
                nguoitao: nhanviengiao
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCNhanVien.aspx/loadBSC",
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
            var nhanviennhan = $("#nhanviennhan").val();
            var nhanvienthamdinh = $("#nhanvienthamdinh").val();
            var thang = $("#month").val();
            var nam = $("#year").val();
            if (nhanviennhan == null || nhanviennhan == "" || nhanvienthamdinh == null || nhanvienthamdinh == "") {
                swal("Error!", "Vui lòng nhập các trường bắt buộc!!!", "error");
                return false;
            }

            var kpi_detail = [];
            $("#table-kpi > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                var tytrong = $("#tytrong_" + kpi_id).val();
                var dvt = $("#dvt_" + kpi_id).val();
                var kehoach = $("#kehoach_" + kpi_id).val();
                kpi_detail.push({
                    kpi_id: kpi_id,
                    tytrong: tytrong,
                    dvt: dvt,
                    kehoach: kehoach
                });
            });

            var requestData = {
                nhanviengiao: nhanviengiao,
                nhanviennhan: nhanviennhan,
                nhanvienthamdinh: nhanvienthamdinh,
                thang: thang,
                nam: nam,
                kpi_detail: kpi_detail
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCNhanVien.aspx/saveData",
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
            var nhanviennhan = $("#nhanviennhan").val();
            var thang = $("#month").val();
            var nam = $("#year").val();

            var requestData = {
                nhanviengiao: nhanviengiao,
                nhanviennhan: nhanviennhan,
                thang: thang,
                nam: nam
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCNhanVien.aspx/giaoBSC",
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
                            getBSCByCondition(nhanviengiao, nhanviennhan, thang, nam);
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
            var nhanviennhan = $("#nhanviennhan").val();
            var thang = $("#month").val();
            var nam = $("#year").val();

            var requestData = {
                nhanviengiao: nhanviengiao,
                nhanviennhan: nhanviennhan,
                thang: thang,
                nam: nam
            };
            var szRequest = JSON.stringify(requestData);

            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCNhanVien.aspx/huygiaoBSC",
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
                            getBSCByCondition(nhanviengiao, nhanviennhan, thang, nam);
                        });
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
