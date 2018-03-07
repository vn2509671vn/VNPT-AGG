<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="MauBSCNhanVien.aspx.cs" Inherits="VNPT_BSC.BSC.MauBSCNhanVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
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
            <h3 class="panel-title">MẪU CHỈ TIÊU BSC/KPI CHO NHÂN VIÊN</h3>
          </div>
          <div class="panel-body">
            <div class="col-md-2 col-xs-12 padding-right-10">
                <div class="panel panel-primary">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Danh Sách Mẫu BSC</h3>
                    </div>--%>
                    <div class="panel list-group">
                        <% for(int nIndex = 0; nIndex < dtBSCNam.Rows.Count; nIndex++){ %>
                            <%
                               string BSCyear = dtBSCNam.Rows[nIndex]["nam"].ToString();
                            %>
                            <strong><a href="#" class="list-group-item list-group-item-success" data-toggle="collapse" data-target="#<%=BSCyear %>"><%=BSCyear%></a></strong>
                            <div id="<%=BSCyear %>" class="sublinks collapse">
                                <ul class="list-group">
                                    <% for(int i = 0; i < dtBSC.Rows.Count; i++){ %>
                                        <%
                                            string month =  dtBSC.Rows[i][0].ToString();
                                            string year =  dtBSC.Rows[i][1].ToString();
                                            string bscduocgiao =  dtBSC.Rows[i][2].ToString();
                                            if(year != BSCyear){
                                                continue;
                                            }
                                        %>
                                        <%--<a href="#" onclick="fillData(<%=month %>, <%=year %>, <%=nguoitao %>)" class="list-group-item list-group-item-info text-center"><%= month +"/"+ year%></a>--%>
                                        <a href="#" class="list-group-item list-group-item-info text-center" onclick="fillData(<%=month %>, <%=year %>, <%=nguoitao %>, '<%=bscduocgiao %>')"><%= month +"/"+ year%></a>
                                    <% } %>
                                </ul>
                            </div>
                        <% } %>
                    </div>
                    
                </div>
            </div>
            <div class="col-md-10 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-3">Thời gian áp dụng:</label>
                    <div class="col-sm-4 form-inline">
                        <input type="text" class="form-control number" id="month" name="month" maxlength="2" size="2" placeholder="Tháng" onkeypress="return onlyNumbers(event.charCode || event.keyCode);"/>
                        <input type="text" class="form-control number" id="year" name="year" maxlength="4" size="4" placeholder="Năm" onkeypress="return onlyNumbers(event.charCode || event.keyCode);"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Loại mẫu:</label>
                    <div class="col-sm-4 form-inline">
                        <select class='form-control' id='loaiMauBSC'>
                            <% for (int nMauBSC = 0; nMauBSC < dtMauBSC.Rows.Count; nMauBSC++){ %>
                            <option value="<% =dtMauBSC.Rows[nMauBSC]["loai_id"].ToString() %>"><% =dtMauBSC.Rows[nMauBSC]["loai_ten"].ToString() %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Xem quy định về tỷ trọng:</label>
                    <div class="col-sm-4 form-inline" style="padding-top: 5px">
                        <a href="#" data-target='#quydinhNhomKPI' data-toggle='modal' class="control-label">Xem chi tiết</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Chỉ xem KPI được chọn:</label>
                    <div class="col-sm-4 form-inline" style="padding-top: 5px">
                        <input type='checkbox' id='checkGomGon'/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">KPI được giao:</label>
                    <div class="col-sm-4">
                        <!-- Load 10 BSC được giao gần nhất -->
                        <select class="form-control" id="bscduocgiao">
                            <% for(int i = 0; i < dsBSCDV.Rows.Count; i++){ %>
                            <option value="<%= dsBSCDV.Rows[i]["thang"] + "-" + dsBSCDV.Rows[i]["nam"] + "-" + donvinhan%>"><%= dsBSCDV.Rows[i]["thang"] + "/" + dsBSCDV.Rows[i]["nam"] %></option>
                            <% } %>
                        </select>
                    </div>
                    <div class="col-md-12 col-xs-12 margin-top-5">
                        <div class='table-responsive' id="kpiduocgiao">
                        </div>
                    </div>
                </div>
                <!-- Mod start ThangTGM 02092017 - Các đơn vị huyện thị không được phép thêm kpi cá nhân -->
                <%--<div class="form-group">
                    <label class="control-label col-sm-3">Danh sách KPI:</label>
                    <div class="col-md-12 col-xs-12">
                        <div class='table-responsive'>
                            <table class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%' id="danhsachKPI">
                                <thead>
                                  <tr>
                                    <th class='text-center'><input type="checkbox" id="checkall-kpi"/></th>
                                    <th>KPI</th>
                                    <th>ĐVT</th>
                                    <th>Tỷ trọng (%)</th>
                                    <th>Nhân viên thẩm định</th>
                                  </tr>
                                </thead>
                                <tbody>
                                    <% for(int i = 0; i < dtKPI.Rows.Count; i++){ %>
                                        <tr data-id="<%=dtKPI.Rows[i]["kpi_id"].ToString() %>">
                                          <td class='text-center'><input name="checkbox-kpi" id='kpi_id_<%=dtKPI.Rows[i]["kpi_id"].ToString() %>' type="checkbox" value="<%=dtKPI.Rows[i]["kpi_id"].ToString() %>" /></td>
                                          <td><%=dtKPI.Rows[i]["name"].ToString() %></td>
                                          <td class='text-center'>
                                              <select class='form-control' id='dvt_<%=dtKPI.Rows[i]["kpi_id"].ToString() %>'>
                                                  <% for (int nDVT = 0; nDVT < dtDVT.Rows.Count; nDVT++){ %>
                                                  <option value="<% =dtDVT.Rows[nDVT]["dvt_id"].ToString() %>"><% =dtDVT.Rows[nDVT]["dvt_ten"].ToString() %></option>
                                                  <% } %>
                                              </select>
                                          </td>
                                          <td class='text-center'><input type="text" class='form-control' onkeypress='return onlyNumbers(event.charCode || event.keyCode);' id='tytrong_<%=dtKPI.Rows[i]["kpi_id"].ToString() %>' size="2"/></td>
                                          <!-- Dropdown Nhân viên thẩm định -->
                                          <td class='text-center'>
                                              <select class='form-control' id='nvtd_<%=dtKPI.Rows[i]["kpi_id"].ToString() %>'>
                                                  <% for (int nNVTD = 0; nNVTD < dtNVTD.Rows.Count; nNVTD++)
                                                     { %>
                                                  <option value="<% =dtNVTD.Rows[nNVTD]["nhanvien_id"].ToString() %>"><% =dtNVTD.Rows[nNVTD]["nhanvien_hoten"].ToString() %></option>
                                                  <% } %>
                                              </select>
                                          </td>
                                        </tr>
                                    <% } %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>--%>
                <div class="form-group">
                    <div class="col-md-12 col-xs-12 margin-top-5">
                        <div class='table-responsive' id="kho_kpi">
                            <table class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%' id='danhsachKhoKPI'>
                                <caption><strong>Thư Viện KPI</strong></caption>
                                <thead>
                                    <tr>
                                        <th class='text-center'><input type='checkbox' id='checkall-kpitrongkho' onclick='check_kpi_trongkho()'/></th>
                                        <th class='text-center'>KPI</th>
                                        <th class='text-center'>KPO</th>
                                        <th class='text-center'>ĐVT</th>
                                        <th class='text-center'>Tỷ trọng (%)</th>
                                        <th class='text-center'>Nhóm KPI</th>
                                    </tr>
                                    <tr id='filterSection_KhoKPI'>
                                        <th></th>
                                        <th data-filter='filter_kpi_trongkho' class='max-width-100'>KPI</th>
                                        <th data-filter='filter_kpi_trongkho' class='max-width-100'>KPO</th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%for (int i = 0; i < dtKhoKPI.Rows.Count; i++){
                                          string kpi_id = dtKhoKPI.Rows[i]["kpi_id"].ToString();
                                          string kpi_name = dtKhoKPI.Rows[i]["name"].ToString();
                                          string kpo_name = dtKhoKPI.Rows[i]["kpo_ten"].ToString();
                                          string nhom_kpi = dtKhoKPI.Rows[i]["nhom_kpi"].ToString();
                                    %>
                                    <tr data-id='<%=kpi_id %>'>
                                        <td class='text-center'><input name='checkbox-kpitrongkho' id='kpi_id_<%=kpi_id %>' type='checkbox' value='<%=kpi_id %>' /></td>
                                        <td class='min-width-300'><strong><%=kpi_name %></strong></td>
                                        <td><strong><%=kpo_name %></strong></td>
                                        <td class='text-center'>
                                            <select class='form-control' id='dvt_<%=kpi_id %>'>
                                                <%for (int nDVT = 0; nDVT < dtDVT.Rows.Count; nDVT++){
                                                      string dvt_id = dtDVT.Rows[nDVT]["dvt_id"].ToString();
                                                      string dvt_name = dtDVT.Rows[nDVT]["dvt_ten"].ToString();
                                                %>
                                                    <option value='<%=dvt_id %>'><%=dvt_name %></option>"
                                                <%} %>
                                            </select>
                                        </td>
                                        <td class='text-center'><input type='text' class='form-control cls_tytrong' id='tytrong_<%=kpi_id %>' size='2' onkeypress='return onlyNumbers(event.charCode || event.keyCode);'/></td>
                                        <td class='text-center min-width-200'>
                                            <select style="width: 100% !important;" class='form-control' id='nhom_kpi_<%=kpi_id %>' name="cboNhomKPI" data-nhom-id='<%=nhom_kpi %>'>
                                                <%for (int nNhom = 0; nNhom < dtNhomKPI.Rows.Count; nNhom++)
                                                  {
                                                      string nhom_id = dtNhomKPI.Rows[nNhom]["id"].ToString();
                                                      string nhom_name = dtNhomKPI.Rows[nNhom]["ten_nhom"].ToString();
                                                      string nhom_tytrong = dtNhomKPI.Rows[nNhom]["tytrong"].ToString();
                                                      string tenmau = dtNhomKPI.Rows[nNhom]["loai_ten"].ToString();
                                                %>
                                                    <option data-nhom-tytrong ="<%=nhom_tytrong %>" value='<%=nhom_id %>'><%= nhom_name %></option>
                                                <%} %>
                                            </select>
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <!-- Mod end ThangTGM 02092017 -->
                <div class="form-group">
                    <div class="col-md-12 col-xs-12">
                        <div class="col-sm-8 col-sm-offset-3">
                            <button type="button" class="btn btn-success" id="btnSave">Lưu</button>
                            <button type="button" class="btn btn-default" id="btnClean" onclick="clearInputs()">Reset</button>
                        </div>
                    </div>
                </div>
            </div>
          </div>
        </div>

        <!----------------------------------------------------------Quy định nhóm KPI--------------------------------------------------------------->
            <div id="quydinhNhomKPI" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content col-md-12">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" id="close_phanhoi">&times;</button>
                            <h4 class="modal-title">Quy định tỷ trọng của nhóm KPI</h4>
                        </div>                                    
                        <div class="modal-body">
                            <div class='table-responsive'>
                            <table class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%' id="tbQuyDinhTyTrong">
                                <thead>
                                    <tr>
                                        <th class='text-center'>Nhóm KPI</th>
                                        <th class='text-center'>Loại Mẫu</th>
                                        <th class='text-center'>Tỷ Trọng (%)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%for (int nNhom = 0; nNhom < dtNhomKPI.Rows.Count; nNhom++){
                                        string nhom_id = dtNhomKPI.Rows[nNhom]["id"].ToString();
                                        string nhom_name = dtNhomKPI.Rows[nNhom]["ten_nhom"].ToString();
                                        string nhom_tytrong = dtNhomKPI.Rows[nNhom]["tytrong"].ToString();
                                        string tenmau = dtNhomKPI.Rows[nNhom]["loai_ten"].ToString();
                                     %>
                                        <tr>
                                            <td><strong><%=nhom_name %></strong></td>
                                            <td><strong><%=tenmau %></strong></td>
                                            <td class="text-center"><strong><%=nhom_tytrong %></strong></td>
                                        </tr>
                                    <% } %>
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
    <!-- End Phản hồi -->
    </div>
    <a id='checkTyTrong' class="btn btn-danger btn-md" style="position:fixed; bottom: 0px; right:0px; border-radius: 40%;">Kiểm tra tỷ trọng</a>
<script type="text/javascript">
    var donvinhan = "<%=donvinhan%>";
    var nguoitao = "<%= nguoitao%>";
    var arrKPITmp = new Array();

    function onlyNumbers(e) {
        //if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
        return !(e > 31 && (e < 48 || e > 57));
    }

    function check_kpi_duocgiao() {
        $(document).on("click", "#checkall-kpiduocgiao", function () {
            if (this.checked) {
                // Iterate each checkbox
                $('input[name=checkbox-kpiduocgiao]').each(function () {
                    this.checked = true;
                });
            }
            else {
                $('input[name=checkbox-kpiduocgiao]').each(function () {
                    this.checked = false;
                });
            }
        });
    }

    function check_kpi_trongkho() {
        $(document).on("click", "#checkall-kpitrongkho", function () {
            if (this.checked) {
                // Iterate each checkbox
                $('input[name=checkbox-kpitrongkho]').each(function () {
                    this.checked = true;
                });
            }
            else {
                $('input[name=checkbox-kpitrongkho]').each(function () {
                    this.checked = false;
                });
            }
        });
    }

    function gomGonKPIDaChon(){
        var gomgon = $("#checkGomGon").is(":checked");
        if(gomgon){
            $("input[name=checkbox-kpiduocgiao]").each(function(){
                var checked = $(this).is(":checked");
                if(!checked){
                    $(this).closest("tr").addClass("cus_hide");
                }
            });

            $("input[name=checkbox-kpitrongkho]").each(function(){
                var checked = $(this).is(":checked");
                if(!checked){
                    $(this).closest("tr").addClass("cus_hide");
                }
            });
        }
        else {
            $("input[name=checkbox-kpiduocgiao]").each(function(){
                $(this).closest("tr").removeClass("cus_hide");
            });

            $("input[name=checkbox-kpitrongkho]").each(function(){
                $(this).closest("tr").removeClass("cus_hide");
            });
        }
    }

    // Fill data khi click danh sách mẫu bsc
    function fillData(month, year, nguoitao, bscduocgiao) {
        arrKPITmp = [];
        $("input[type=text]").each(function () {
            $(this).val("");
        });
        $("#month").val(month);
        $("#year").val(year);
        var loaiMauBSC = $("#loaiMauBSC").val();

        /*Remove red border*/
        $("#month").css("border-color", "#ccc");
        $("#year").css("border-color", "#ccc");

        // BSC được giao
        var dsKPIDuocGiao = bscduocgiao + "-" + donvinhan;
        $("#bscduocgiao").val(dsKPIDuocGiao);
        var data = $('#bscduocgiao').val();
        var arrDate = data.split("-");
        var thang = arrDate[0];
        var nam = arrDate[1];
        loadKPIDuocGiao(thang, nam, donvinhan);

        var requestData = {
            monthAprove: month,
            yearAprove: year,
            nguoitao: nguoitao,
            maubsc: loaiMauBSC
        };

        var szRequest = JSON.stringify(requestData);

        swal({
            title: "Tải dữ liệu!",
            text: "Vui lòng chờ trong giây lát.",
            timer: 2000,
            showConfirmButton: false
        });
        loadNhomKPI(loaiMauBSC);
        setTimeout(function () {
            $.ajax({
                type: "POST",
                url: "MauBSCNhanVien.aspx/BindingCheckBox",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $("input[type=checkbox]").attr("checked", false);
                    var arrKPI = new Array();
                    arrKPI = result.d;
                    for (var i = 0; i < arrKPI.length; i++) {
                        var KPI_ID = arrKPI[i].kpi_id;
                        var tytrong = arrKPI[i].tytrong;
                        var donvitinh = arrKPI[i].donvitinh;
                        //var nhanvienthamdinh = arrKPI[i].nhanvienthamdinh;
                        var nhom_kpi = arrKPI[i].nhom_kpi;
                        $(":checkbox[value='" + KPI_ID + "']").prop("checked", "true");
                        $("#dvt_" + KPI_ID).val(donvitinh);
                        $("#tytrong_" + KPI_ID).val(tytrong);
                        //$("#nvtd_" + KPI_ID).val(nhanvienthamdinh);
                        $("#nhom_kpi_" + KPI_ID).val(nhom_kpi);
                        var nhom_kpi_tytrong = $("#nhom_kpi_" + KPI_ID + " option:selected").attr("data-nhom-tytrong");
                        var nhom_kpi_ten = $("#nhom_kpi_" + KPI_ID + " option:selected").text();

                        arrKPITmp.push({
                            kpi_id: KPI_ID,
                            tytrong: tytrong,
                            dvt: donvitinh,
                            nhom_kpi: nhom_kpi,
                            nhom_kpi_tytrong: nhom_kpi_tytrong,
                            nhom_kpi_ten: nhom_kpi_ten
                        });
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        }, 2000);
    }

    // Load KPI dua theo BSC duoc giao
    function loadKPIDuocGiao(thang, nam, donvinhan) {
        var requestData = {
            thang: thang,
            nam: nam,
            donvinhan: donvinhan
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "MauBSCNhanVien.aspx/loadKPIDuocGiao",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                $("#kpiduocgiao").html(output);

                // Setup - add a text input to each footer cell
                $('#danhsachKPIDuocGiao thead tr#filterSection_BSCDuocGiao th').each(function () {
                    if ($(this).attr("data-filter") == "filter_kpi_duocgiao") {
                        var title = $(this).text();
                        $(this).html('<input class="max-width-100" type="text" placeholder="Search ' + title + '"/>');
                    }
                });

                // DataTable
                var table = $('#danhsachKPIDuocGiao').DataTable({
                    "bLengthChange": false,
                    "bPaginate": false,
                    "bSort": false
                });

                // Apply the search
                table.columns().every(function () {
                    var that = this;

                    $('input', this.header()).on('keyup change', function () {
                        if (that.search() !== this.value) {
                            that
                                .search(this.value)
                                .draw();
                        }
                    });
                });


                $("#danhsachKhoKPI tbody tr").each(function(){
                    $(this).removeClass("hide");
                });

                $("#danhsachKPIDuocGiao tbody tr").each(function () {
                    var kpi_id = $(this).attr("data-id");
                    $("#danhsachKhoKPI tbody tr[data-id='" + kpi_id + "']").addClass("hide");
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function loadNhomKPI(loaimaubsc) {
        var requestData = {
            loaimaubsc: loaimaubsc
        };

        var szRequest = JSON.stringify(requestData);
        setTimeout(function () {
            $.ajax({
                type: "POST",
                url: "MauBSCNhanVien.aspx/getNhomKPIByLoaiMauID",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var output = result.d;
                    $("select[name='cboNhomKPI']").each(function () {
                        $(this).html(output);
                        var id = $(this).attr("data-nhom-id");
                        $(this).val(id)
                    })
                },
                error: function (msg) { alert(msg.d); }
            });
        }, 2000);
    }

    $(document).ready(function () {
        
        // Set thời gian hiện tại
        //var curTime = new Date();
        //var month = curTime.getMonth() + 1;
        //var year = curTime.getFullYear();
        var dateTime = $("#bscduocgiao").val();
        if (dateTime != null) {
            var arrDate = dateTime.split("-");
            var month = arrDate[0];
            var year = arrDate[1];
            var nDonvinhan = arrDate[2];
            var maubsc = $("#loaiMauBSC").val();
            loadKPIDuocGiao(month, year, donvinhan);
            loadNhomKPI(maubsc);
            gomGonKPIDaChon();
        }
        else {
            $("#btnSave").hide();
            $("#btnClean").hide();
        }


        // Khi thay đổi bsc được giao
        $("#bscduocgiao").change(function () {
            $("#checkGomGon").prop("checked",false);
            arrKPITmp = [];
            var data = $(this).val();
            var arrDate = data.split("-");
            var thang = arrDate[0];
            var nam = arrDate[1];
            var nDonvinhan = arrDate[2];
            var maubsc = $("#loaiMauBSC").val();
            loadKPIDuocGiao(thang, nam, nDonvinhan);
            gomGonKPIDaChon();
        });

        $("#loaiMauBSC").change(function () {
            $("#checkGomGon").prop("checked",false);
            arrKPITmp = [];
            $("input[type=checkbox]").each(function(){
                $(this).prop("checked",false);
            });
            var month = $("#month").val();
            var year = $("#year").val();
            var data = $('#bscduocgiao').val();
            var maubsc = $("#loaiMauBSC").val();
            var arrDate = data.split("-");
            var thang = arrDate[0];
            var nam = arrDate[1];
            var bscduocgiao = thang + "-" + nam;
            if (month != "" && year != "" && bscduocgiao != "") {
                fillData(month, year, nguoitao, bscduocgiao);
            }
            else {
                loadNhomKPI(maubsc);
            }

            gomGonKPIDaChon();
        });

        // Khi click save
        $("#btnSave").click(function () {
            var month = $("#month").val();
            var year = $("#year").val();
            var data = $("#bscduocgiao").val();
            if (data == null) {
                swal("Error", "Không thể lưu mẫu bsc vì đơn vị của bạn chưa nhận bsc nào!!!", "error");
                return false;
            }

            var arrDate = data.split("-");
            var bscduocgiao = arrDate[0] + "-" + arrDate[1];
            var loaiMauBSC = $("#loaiMauBSC").val();

            var isMonth = validateMonth("month");
            var isYear = validateYear("year");

            if (!isMonth || !isYear) {
                swal("Error", "Vui lòng nhập đúng vào trường bất buộc!!!", "error");
                return false;
            }

            var arrKPI = new Array();
            var totalTyTrong = 0;

            // Clear search
            var cleardanhsachKPIDuocGiao = $('#danhsachKPIDuocGiao').DataTable();
            cleardanhsachKPIDuocGiao
             .search('')
             .columns().search('')
             .draw();
            $('#danhsachKPIDuocGiao thead tr#filterSection_BSCDuocGiao th input').each(function () {
                $(this).val('');
            });

            var cleardanhsachKPIDuocGiao = $('#danhsachKhoKPI').DataTable();
            cleardanhsachKPIDuocGiao
             .search('')
             .columns().search('')
             .draw();
            $('#danhsachKhoKPI thead tr#filterSection_KhoKPI th input').each(function () {
                $(this).val('');
            });


            $("#danhsachKPIDuocGiao > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                //var tytrong = $("#tytrong_" + kpi_id).val();
                //var dvt = $("#dvt_" + kpi_id).val();
                var tytrong = $("#tytrong_" + kpi_id).val();
                if (tytrong == "") {
                    tytrong = 0;
                }

                
                var dvt = $("#dvt_" + kpi_id).val();
                //var nvtd = $("#nvtd_" + kpi_id).val();
                var nhom_kpi = $("#nhom_kpi_" + kpi_id).val();
                var nhom_kpi_tytrong = $("#nhom_kpi_" + kpi_id + " option:selected").attr("data-nhom-tytrong");
                var nhom_kpi_ten = $("#nhom_kpi_" + kpi_id + " option:selected").text();
                var isChecked = $("#kpi_id_" + kpi_id).is(":checked");
                if (isChecked == true) {
                    totalTyTrong += parseInt(tytrong);
                    arrKPI.push({
                        kpi_id: kpi_id,
                        tytrong: tytrong,
                        dvt: dvt,
                        nhom_kpi: nhom_kpi,
                        nhom_kpi_tytrong: nhom_kpi_tytrong,
                        nhom_kpi_ten: nhom_kpi_ten,
                        nvtd: nguoitao
                    });
                }
            });

            $("#danhsachKhoKPI > tbody > tr").each(function () {
                var existHide = $(this).hasClass("hide");
                if (existHide) {
                    return;
                }
                var kpi_id = $(this).attr("data-id");
                //var tytrong = $("#tytrong_" + kpi_id).val();
                //var dvt = $("#dvt_" + kpi_id).val();
                var tytrong = $("#tytrong_" + kpi_id).val();
                if (tytrong == "") {
                    tytrong = 0;
                }

                
                var dvt = $("#dvt_" + kpi_id).val();
                //var nvtd = $("#nvtd_" + kpi_id).val();
                var nhom_kpi = $("#nhom_kpi_" + kpi_id).val();
                var nhom_kpi_tytrong = $("#nhom_kpi_" + kpi_id + " option:selected").attr("data-nhom-tytrong");
                var nhom_kpi_ten = $("#nhom_kpi_" + kpi_id + " option:selected").text();
                var isChecked = $("#kpi_id_" + kpi_id).is(":checked");
                if (isChecked == true) {
                    totalTyTrong += parseInt(tytrong);
                    arrKPI.push({
                        kpi_id: kpi_id,
                        tytrong: tytrong,
                        dvt: dvt,
                        nhom_kpi: nhom_kpi,
                        nhom_kpi_tytrong: nhom_kpi_tytrong,
                        nhom_kpi_ten: nhom_kpi_ten,
                        nvtd: nguoitao
                    });
                }
            });

            var arrNhomKPI = groupBy(arrKPI, 'nhom_kpi_ten', 'nhom_kpi_tytrong', 'tytrong');
            var message = "";
            for (var nIndex = 0; nIndex < arrNhomKPI.length; nIndex++) {
                var tongtytrong = parseInt(arrNhomKPI[nIndex].tytrong);
                var tytrongnhom = parseInt(arrNhomKPI[nIndex].nhom_kpi_tytrong);
                if (tongtytrong != tytrongnhom) {
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

            if (arrKPI.length == 0) {
                swal("Error", "Vui lòng chọn KPI!!!", "error");
                return false;
            }

            if (totalTyTrong != 100) {
                swal("Error", "Tổng tỷ trọng của các KPI phải bằng 100%. Vui lòng kiểm tra lại tỷ trọng của các KPI", "error");
                return false;
            }

            var requestData = {
                monthAprove: month,
                yearAprove: year,
                arrKPI_ID: arrKPI,
                nguoitao: nguoitao,
                bscduocgiao: bscduocgiao,
                maubsc: loaiMauBSC
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "MauBSCNhanVien.aspx/SaveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d) {
                        swal({
                            title: "Lưu dữ liệu thành công!!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            window.location.reload();
                        });
                    }
                    else {
                        swal("Error", "Vui lòng check lại!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        // Check all kpi của bản thân
        $("#checkall-kpi").click(function () {
            if (this.checked) {
                // Iterate each checkbox
                $('input[name=checkbox-kpi]').each(function () {
                    this.checked = true;
                });
            }
            else {
                $('input[name=checkbox-kpi]').each(function () {
                    this.checked = false;
                });
            }
        });

        // Setup - add a text input to each footer cell
        $('#danhsachKhoKPI thead tr#filterSection_KhoKPI th').each(function () {
            if ($(this).attr("data-filter") == "filter_kpi_trongkho") {
                var title = $(this).text();
                $(this).html('<input class="max-width-100" type="text" placeholder="Search ' + title + '"/>');
            }
        });

        // DataTable
        var tableKhoKPI = $('#danhsachKhoKPI').DataTable({
            "bLengthChange": false,
            "bPaginate": false,
            "bSort": false
        });

        // Apply the search
        tableKhoKPI.columns().every(function () {
            var that = this;

            $('input', this.header()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that
                        .search(this.value)
                        .draw();
                }
            });
        });

        $("#checkTyTrong").click(function () {
            var arrNhomKPI = groupByThreeCol(arrKPITmp, 'nhom_kpi_ten', 'nhom_kpi_tytrong', 'nhom_kpi', 'tytrong');
            var message = "";
            var tonghienco = 0;
            arrNhomKPI.sort(function (a, b) {
                var a1 = parseInt(a.nhom_kpi), b1 = parseInt(b.nhom_kpi);
                if (a1 == b1) return 0;
                return a1 > b1 ? 1 : -1;
            });

            for (var nIndex = 0; nIndex < arrNhomKPI.length; nIndex++) {
                var tongtytrong = parseInt(arrNhomKPI[nIndex].tytrong);
                var tytrongnhom = parseInt(arrNhomKPI[nIndex].nhom_kpi_tytrong);
                tonghienco += tongtytrong;
                message += "<tr>";
                message += "<td><strong>" + arrNhomKPI[nIndex].nhom_kpi_ten + "</strong></td>";
                message += "<td class='text-center'><strong>" + arrNhomKPI[nIndex].nhom_kpi_tytrong + "</strong></td>";
                message += "<td class='text-center'><strong>" + arrNhomKPI[nIndex].tytrong + "</strong></td>";
                message += "</tr>";
            }

            swal({
                title: "Kiểm tra tỷ trọng KPI",
                text: "<table table class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'><caption>Tổng tỷ trọng hiện có: " + tonghienco + "</caption><thead><tr><th>Nhóm KPI</th><th>Tỷ trọng yêu cầu</th><th>Tỷ trọng hiện có</th></tr></thead><tbody>" + message + "</tbody></table>",
                html: true
            });
            return false;
        });

        $("#tbQuyDinhTyTrong").DataTable();

        $(document).on("click", "input[type='checkbox']", function () {
            var kpi_id = $(this).val();
            var isChecked = $(this).is(":checked");
            if (isChecked) {
                if (kpi_id !== "on") {
                    $("table > tbody > tr[data-id='" + kpi_id + "']").each(function () {
                        var existHide = $(this).hasClass("hide");
                        if (existHide) {
                            return;
                        }
                        var tytrong = $("#tytrong_" + kpi_id).val();
                        if (tytrong == "") {
                            tytrong = 0;
                        }


                        var dvt = $("#dvt_" + kpi_id).val();
                        var nhom_kpi = $("#nhom_kpi_" + kpi_id).val();
                        var nhom_kpi_tytrong = $("#nhom_kpi_" + kpi_id + " option:selected").attr("data-nhom-tytrong");
                        var nhom_kpi_ten = $("#nhom_kpi_" + kpi_id + " option:selected").text();

                        arrKPITmp.push({
                            kpi_id: kpi_id,
                            tytrong: tytrong,
                            dvt: dvt,
                            nhom_kpi: nhom_kpi,
                            nhom_kpi_tytrong: nhom_kpi_tytrong,
                            nhom_kpi_ten: nhom_kpi_ten
                        });
                    });                    
                }
            }
            else {
                arrKPITmp = $.grep(arrKPITmp, function (e,i) {
                    return +e.kpi_id != kpi_id;
                });
            }
        });

        $(document).on("change", "select", function () {
            var kpi_id = $(this).closest('tr').attr("data-id");
            var isChecked = $("#kpi_id_" + kpi_id).is(":checked");
            if (isChecked) {
                if (kpi_id !== "on") {
                    $("table > tbody > tr[data-id='" + kpi_id + "']").each(function () {
                        var existHide = $(this).hasClass("hide");
                        if (existHide) {
                            return;
                        }
                        var tytrong = $("#tytrong_" + kpi_id).val();
                        if (tytrong == "") {
                            tytrong = 0;
                        }


                        var dvt = $("#dvt_" + kpi_id).val();
                        var nhom_kpi = $("#nhom_kpi_" + kpi_id).val();
                        var nhom_kpi_tytrong = $("#nhom_kpi_" + kpi_id + " option:selected").attr("data-nhom-tytrong");
                        var nhom_kpi_ten = $("#nhom_kpi_" + kpi_id + " option:selected").text();

                        arrKPITmp = $.grep(arrKPITmp, function (e, i) {
                            return +e.kpi_id != kpi_id;
                        });

                        arrKPITmp.push({
                            kpi_id: kpi_id,
                            tytrong: tytrong,
                            dvt: dvt,
                            nhom_kpi: nhom_kpi,
                            nhom_kpi_tytrong: nhom_kpi_tytrong,
                            nhom_kpi_ten: nhom_kpi_ten
                        });
                    });
                }
            }
            else {
                arrKPITmp = $.grep(arrKPITmp, function (e, i) {
                    return +e.kpi_id != kpi_id;
                });
            }
        });

        $(document).on("keyup", ".cls_tytrong", function () {
            var kpi_id = $(this).closest('tr').attr("data-id");
            var isChecked = $("#kpi_id_" + kpi_id).is(":checked");
            if (isChecked) {
                var objIndex = arrKPITmp.findIndex((obj => obj.kpi_id == kpi_id));
                arrKPITmp[objIndex].tytrong = $(this).val();
            }
        });

        $("#checkGomGon").change(function(){
            gomGonKPIDaChon();
        });
    });
</script>
</asp:Content>
