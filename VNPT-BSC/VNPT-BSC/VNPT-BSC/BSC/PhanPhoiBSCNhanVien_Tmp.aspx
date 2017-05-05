<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="PhanPhoiBSCNhanVien_Tmp.aspx.cs" Inherits="VNPT_BSC.BSC.PhanPhoiBSCNhanVien_Tmp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <h3 class="panel-title">GIAO BSC CHO NHÂN VIÊN</h3>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-3">Thời gian:</label>
                    <div class="col-sm-6 form-inline">
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
                    <label class="col-sm-8 col-sm-offset-3 margin-top-5"><strong id="ten_nhanviennhan"></strong></label>
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
                                    <a class="btn btn-primary btn-xs" id="btnLoadBSCThangTruoc">
                                        BSC Tháng Trước
                                    </a>
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body" id="gridBSC">
                            <div class="col-md-12 col-xs-12">
                                <div class='table-responsive'>
                                    <table id='table-kpi' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>
                                        <thead>
                                            <tr>
                                                <th class='text-center'><button type="button" class="btn btn-primary btn-xs" id="btnAddRow">+</button></th>
                                                <th class='text-center'>KPI</th>
                                                <th class='text-center'>Nhóm KPI</th>
                                                <th class='text-center'>Tỷ trọng</th>
                                                <th class='text-center'>ĐVT</th>
                                                <th class='text-center'>Chỉ tiêu</th>
                                                <th class='text-center'>T/gian giao</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                        </tbody>
                                    </table>
                                </div>
                            </div>
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
    </div>

<script type="text/javascript">
    var nhanviengiao = "<%=nhanvienquanly%>";
    var donvi = "<%=donvi%>";
    function getCurrentDate() {
        var curMonth = "<%= DateTime.Now.ToString("%M") %>";
        var curYear = "<%= DateTime.Now.ToString("yyyy") %>";
        $("#month").val(curMonth);
        $("#year").val(curYear);
        var nhanviennhan = $("#nhanviennhan").val();
        getBSCByCondition(nhanviengiao, nhanviennhan, curMonth, curYear, donvi);
    }

    function getBSCByCondition(id_nv_giao, nv_nhan, thang, nam, donvi) {
        /*Hide button*/
        $("#updateGiaoStatus").hide();
        $("#updateHuyGiaoStatus").hide();

        var requestData = {
            id_nv_giao: id_nv_giao,
            nv_nhan: nv_nhan,
            thang: thang,
            nam: nam,
            donvi: donvi
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "PhanPhoiBSCNhanVien_Tmp.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var nhanviengiao = output.nhanviengiao;
                var nhanviennhan = output.nhanviennhan;
                var ten_nhanviennhan = output.ten_nhanviennhan;
                var trangthaigiao = output.trangthaigiao;
                var trangthainhan = output.trangthainhan;
                var trangthaithamdinh = output.trangthaithamdinh;
                var trangthaiketthuc = output.trangthaiketthuc;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table
                $("#ten_nhanviennhan").text("Tên nhân viên: " + ten_nhanviennhan);

                // Cập nhật trạng thái giao
                if (trangthaigiao == "True") {
                    $("#giaoLabel").removeClass("label-default");
                    $("#giaoLabel").addClass("label-success");
                    $("#giaoLabel").text("Đã giao");
                    $("#updateGiaoStatus").hide();
                    $("#btnLoadBSCThangTruoc").hide();
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
                    $("#btnLoadBSCThangTruoc").show();
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

                var tong = 0;
                $('input[name="tytrong"]').each(function () {
                    var value = $(this).val();
                    if (!isNaN(value)) {
                        tong += parseInt(value);
                    }
                });
                $("#tongTyTrong").text(tong);
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function getBSCByOldMonth(id_nv_giao, nv_nhan, thang, nam, donvi) {
        var requestData = {
            id_nv_giao: id_nv_giao,
            nv_nhan: nv_nhan,
            thang: thang,
            nam: nam,
            donvi: donvi
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "PhanPhoiBSCNhanVien_Tmp.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var nhanviengiao = output.nhanviengiao;
                var nhanviennhan = output.nhanviennhan;
                var ten_nhanviennhan = output.ten_nhanviennhan;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table
                $("#ten_nhanviennhan").text("Tên nhân viên: " + ten_nhanviennhan);

                $("#table-kpi").DataTable({
                    "bSort": false,
                    "pageLength": 50
                });

                var tong = 0;
                $('input[name="tytrong"]').each(function () {
                    var value = $(this).val();
                    if (!isNaN(value)) {
                        tong += parseInt(value);
                    }
                });
                $("#tongTyTrong").text(tong);
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function fillDataBSC() {
        var nhanviennhan = $("#nhanviennhan").val();
        getBSCByCondition(nhanviengiao, nhanviennhan, thang, nam, donvi);
    }

    function changeInputData() {
        var thang = $("#month").val();
        var nam = $("#year").val();
        var nhanviennhan = $("#nhanviennhan").val();
        getBSCByCondition(nhanviengiao, nhanviennhan, thang, nam, donvi);
    }

    function onlyNumbers(e) {
        //if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
        return !(e > 31 && (e < 48 || e > 57) && e != 46);
    }

    function createDataForAuto() {
        var arrResult = new Array();
        <% for(int i = 0; i < dtKPI.Rows.Count; i++){ %>
        <% 
            string ten_kpi = dtKPI.Rows[i]["kpi_ten"].ToString();
            string id = dtKPI.Rows[i]["kpi_id"].ToString();
            string id_nhom = dtKPI.Rows[i]["nhom_kpi"].ToString();
        %>
        arrResult.push({
            data: '<%=id%>',
            value: '<%=ten_kpi%>',
            nhom_kpi: '<%=id_nhom%>'
        });
        <% } %>
        return arrResult;
    }

    $(document).ready(function () {
        $(document).on('focus', '.cls-kpi', function () {
            var dataTmp = createDataForAuto();
            // initialize autocomplete with custom appendTo
            $(this).autocomplete({
                lookup: dataTmp,
                onSelect: function (suggestion) {
                    var stt = $(this).closest('tr').attr("data-stt");
                    var id_nhom = suggestion.nhom_kpi;
                    $("#nhom_kpi_" + stt).val(id_nhom);
                    $(this).attr("title", suggestion.value);
                }
            });
        });

        $(document).on('click', '#btnAddRow', function () {
            var stt = 1;
            if ($(".dataTables_empty").length > 0) {
                //$(".dataTables_empty").remove();
                $(".dataTables_empty").closest('tr').addClass('hide');
                stt = 0;
            }

            var szHTML = "";
            stt += $('#table-kpi > tbody > tr').length;
            szHTML += "<tr data-stt='" + stt + "'>";
            szHTML += "<td class='text-center'><button type='button' class='btn btn-danger btn-xs btnRemove'>-</button></td>";
            szHTML += "<td>";
            szHTML += "<input type='text' class='form-control cls-kpi min-width-300' size='50' id='kpi_"+stt+"'/>";
            szHTML += "</td>";
            szHTML += "<td class='text-center'>";
            szHTML += "<select class='form-control' id='nhom_kpi_" + stt + "'>";
                    <% for (int nIndex = 0; nIndex < dtNhomKPI.Rows.Count; nIndex++)
                       {
                           string szSelected = "";
                           int id_nhom = Convert.ToInt32(dtNhomKPI.Rows[nIndex]["id"].ToString());
                           int tytrong_nhom = Convert.ToInt32(dtNhomKPI.Rows[nIndex]["tytrong"].ToString());
                           if (id_nhom == 2)
                           {
                               szSelected = "selected";
                           }
                    %>
            szHTML += "<option data-tytrong-nhom='<%= tytrong_nhom%>' value='<%=id_nhom%>' <%=szSelected%>><%=dtNhomKPI.Rows[nIndex]["ten_nhom"].ToString().Trim()%></option>";
                    <% } %>
            szHTML += "</select>";
            szHTML += "</td>";
            szHTML += "<td><input type='text' name='tytrong' id='tytrong_" + stt + "' maxlength='2' value='0' size='2' class='form-control'/></td>";
            szHTML += "<td class='text-center'>";
            szHTML += "<select class='form-control' id='dvt_" + stt + "'>";
                    <% for (int nDVT = 0; nDVT < dtDVT.Rows.Count; nDVT++){
                           string szSelected = "";
                           int dvt = Convert.ToInt32(dtDVT.Rows[nDVT]["dvt_id"].ToString());
                           if (dvt == 2)
                           {
                               szSelected = "selected";
                           }
                    %>
            szHTML += "<option value='<%=dvt%>' <%=szSelected%>> <%=dtDVT.Rows[nDVT]["dvt_ten"]%></option>";
                    <% } %>
            szHTML += "</select>";
            szHTML += "</td>";
            szHTML += "<td><input type='text' id='kehoach_" + stt + "' class='form-control' size='2' value='100' onkeypress='return onlyNumbers(event.charCode || event.keyCode);'/></td>";
            szHTML += "<td><input type='text' id='ghichu_" + stt + "' class='form-control min-width-300'/></td>";
            szHTML += "</tr>";
            $('#table-kpi > tbody:last-child').append(szHTML);
        });

        $(document).on('click', '.btnRemove', function () {
            $(this).closest('tr').addClass('hide');
        });

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

        $("#saveData").click(function () {
            var nhanviennhan = $("#nhanviennhan").val();
            var thang = $("#month").val();
            var nam = $("#year").val();
            var loaimau = '16';
            if (nhanviennhan == null || nhanviennhan == "") {
                swal("Error!", "Vui lòng nhập các trường bắt buộc!!!", "error");
                return false;
            }

            var kpi_detail = [];
            var tongtytrong = 0;
            $("#table-kpi > tbody > tr").each(function () {
                var isRemove = $(this).hasClass('hide');
                if (isRemove) {
                    return;
                }
                var stt = $(this).attr("data-stt");
                var ten_kpi = $("#kpi_" + stt).val();
                var tytrong = $("#tytrong_" + stt).val();
                var dvt = $("#dvt_" + stt).val();
                var kehoach = $("#kehoach_" + stt).val();
                var nhom_kpi_id = $("#nhom_kpi_" + stt).val();
                var nhom_kpi_ten = $("#nhom_kpi_" + stt + " option:selected").text();
                var nhom_kpi_tytrong = $("#nhom_kpi_" + stt + " option:selected").attr("data-tytrong-nhom");
                var ghichu = $("#ghichu_" + stt).val();

                if (kehoach == "") {
                    kehoach = 0;
                }

                tongtytrong += parseInt(tytrong);
                kpi_detail.push({
                    ten_kpi: ten_kpi,
                    tytrong: tytrong,
                    dvt: dvt,
                    kehoach: kehoach,
                    nhanvienthamdinh: nhanviengiao,
                    nhom_kpi_id: nhom_kpi_id,
                    nhom_kpi_ten: nhom_kpi_ten,
                    nhom_kpi_tytrong: nhom_kpi_tytrong,
                    ghichu: ghichu
                });
            });

            //var arrNhomKPI = groupBy(kpi_detail, 'nhom_kpi_ten', 'nhom_kpi_tytrong', 'tytrong');
            //var message = "";
            //for (var nIndex = 0; nIndex < arrNhomKPI.length; nIndex++) {
            //    var tongtytrong_nhom = parseInt(arrNhomKPI[nIndex].tytrong);
            //    var tytrongnhom = parseInt(arrNhomKPI[nIndex].nhom_kpi_tytrong);
            //    if (tongtytrong_nhom != tytrongnhom) {
            //        message += arrNhomKPI[nIndex].nhom_kpi_ten + "(yêu cầu: " + arrNhomKPI[nIndex].nhom_kpi_tytrong + "): " + arrNhomKPI[nIndex].tytrong + "<br>";
            //    }
            //}

            //if (message != "") {
            //    swal({
            //        title: "Tỷ trọng của nhóm KPI không thỏa!!!",
            //        text: message,
            //        html: true
            //    });
            //    return false;
            //}

            //if (tongtytrong != 100) {
            //    swal("Error", "Tổng tỷ trọng phải bằng 100!! Vui lòng kiểm tra lại", "error");
            //    return false;
            //}

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
                url: "PhanPhoiBSCNhanVien_Tmp.aspx/saveData",
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
            var loaimau = '16';
            if (nhanviennhan == null || nhanviennhan == "") {
                swal("Error!", "Vui lòng nhập các trường bắt buộc!!!", "error");
                return false;
            }

            var kpi_detail = [];
            var tongtytrong = 0;
            $("#table-kpi > tbody > tr").each(function () {
                var isRemove = $(this).hasClass('hide');
                if (isRemove) {
                    return;
                }
                var stt = $(this).attr("data-stt");
                var ten_kpi = $("#kpi_" + stt).val();
                var tytrong = $("#tytrong_" + stt).val();
                var dvt = $("#dvt_" + stt).val();
                var kehoach = $("#kehoach_" + stt).val();
                var nhom_kpi_id = $("#nhom_kpi_" + stt).val();
                var nhom_kpi_ten = $("#nhom_kpi_" + stt + " option:selected").text();
                var nhom_kpi_tytrong = $("#nhom_kpi_" + stt + " option:selected").attr("data-tytrong-nhom");
                var ghichu = $("#ghichu_" + stt).val();

                if (kehoach == "") {
                    kehoach = 0;
                }

                tongtytrong += parseInt(tytrong);
                kpi_detail.push({
                    ten_kpi: ten_kpi,
                    tytrong: tytrong,
                    dvt: dvt,
                    kehoach: kehoach,
                    nhanvienthamdinh: nhanviengiao,
                    nhom_kpi_id: nhom_kpi_id,
                    nhom_kpi_ten: nhom_kpi_ten,
                    nhom_kpi_tytrong: nhom_kpi_tytrong,
                    ghichu: ghichu
                });
            });

            var arrNhomKPI = groupBy(kpi_detail, 'nhom_kpi_ten', 'nhom_kpi_tytrong', 'tytrong');
            var message = "";
            for (var nIndex = 0; nIndex < arrNhomKPI.length; nIndex++) {
                var tongtytrong_nhom = parseInt(arrNhomKPI[nIndex].tytrong);
                var tytrongnhom = parseInt(arrNhomKPI[nIndex].nhom_kpi_tytrong);
                if (tongtytrong_nhom != tytrongnhom) {
                    message += arrNhomKPI[nIndex].nhom_kpi_ten + "(yêu cầu: " + arrNhomKPI[nIndex].nhom_kpi_tytrong + "): " + arrNhomKPI[nIndex].tytrong + "<br>";
                }
            }

            if (message != "") {
                swal({
                    title: "Tỷ trọng của nhóm KPI không thỏa!!!",
                    text: message,
                    html: true
                });
                return false;
            }

            if (tongtytrong != 100) {
                swal("Error", "Tổng tỷ trọng phải bằng 100!! Vui lòng kiểm tra lại", "error");
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
                url: "PhanPhoiBSCNhanVien_Tmp.aspx/saveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        var requestDataUpdate = {
                            nhanviengiao: nhanviengiao,
                            nhanviennhan: nhanviennhan,
                            thang: thang,
                            nam: nam
                        };
                        var szRequestUpdate = JSON.stringify(requestDataUpdate);

                        $.ajax({
                            type: "POST",
                            url: "PhanPhoiBSCNhanVien_Tmp.aspx/giaoBSC",
                            data: szRequestUpdate,
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
                                        //getBSCByCondition(nhanviengiao, nhanviennhan, thang, nam, donvi);
                                        window.location.reload();
                                    });
                                }
                                else {
                                    swal("Error!!!", "Giao BSC không thành công!!! Vui lòng save lại trước khi giao", "error");
                                }
                            },
                            error: function (msg) { alert(msg.d); }
                        });
                    }
                    else {
                        swal("Error!!!", "Giao không thành công!!!", "error");
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
                url: "PhanPhoiBSCNhanVien_Tmp.aspx/huygiaoBSC",
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
                            getBSCByCondition(nhanviengiao, nhanviennhan, thang, nam, donvi);
                        });
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#btnLoadBSCThangTruoc").click(function () {
            var month = $("#month").val();
            var year = $("#year").val();
            if (month == 1) {
                month = 12;
                year = year - 1;
            }
            else {
                month = month - 1;
            }
            var nhanviennhan = $("#nhanviennhan").val();
            getBSCByOldMonth(nhanviengiao, nhanviennhan, month, year, donvi);
        });

        $(document).on('change', 'input[name="tytrong"]', function () {
            var tong = 0;
            $('input[name="tytrong"]').each(function () {
                var value = $(this).val();
                if (!isNaN(value)) {
                    tong += parseInt(value);
                }
            });
            $("#tongTyTrong").text(tong);
        });
    });
</script>
</asp:Content>
