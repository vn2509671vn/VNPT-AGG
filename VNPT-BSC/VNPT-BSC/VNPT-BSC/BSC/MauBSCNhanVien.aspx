<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MauBSCNhanVien.aspx.cs" Inherits="VNPT_BSC.BSC.MauBSCNhanVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
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
            <h3 class="panel-title">MẪU CHỈ TIÊU BSC/KPI CHO NHÂN VIÊN</h3>
          </div>
          <div class="panel-body">
            <div class="col-sm-3">
                <div class="panel panel-primary">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title">Danh Sách Mẫu BSC</h3>
                    </div>--%>
                    <div class="panel list-group">
                        <% for(int nIndex = 0; nIndex < dtBSCNam.Rows.Count; nIndex++){ %>
                            <%
                                string BSCyear =  dtBSC.Rows[nIndex]["nam"].ToString();
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
            <div class="col-sm-9 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-3">Thời gian áp dụng:</label>
                    <div class="col-sm-4 form-inline">
                        <input type="text" class="form-control number" id="month" name="month" maxlength="2" size="2" placeholder="Tháng"/>
                        <input type="text" class="form-control number" id="year" name="year" maxlength="4" size="4" placeholder="Năm"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">KPI được giao:</label>
                    <div class="col-sm-8">
                        <!-- Load 10 BSC được giao gần nhất -->
                        <select class="form-control" id="bscduocgiao">
                            <% for(int i = 0; i < dsBSCDV.Rows.Count; i++){ %>
                            <option value="<%= dsBSCDV.Rows[i]["thang"] + "-" + dsBSCDV.Rows[i]["nam"] + "-" + donvinhan%>"><%= dsBSCDV.Rows[i]["thang"] + "/" + dsBSCDV.Rows[i]["nam"] %></option>
                            <% } %>
                        </select>
                    </div>
                    <div class="col-sm-12 margin-top-5" id="kpiduocgiao">
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Danh sách KPI:</label>
                    <div class="col-sm-12">
                        <div class='table-responsive'>
                            <table class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%' id="danhsachKPI">
                                <thead>
                                  <tr>
                                    <th><input type="checkbox" id="checkall-kpi"/></th>
                                    <th>KPI</th>
                                    <th>ĐVT</th>
                                    <th>Tỷ trọng (%)</th>
                                  </tr>
                                </thead>
                                <tbody>
                                    <% for(int i = 0; i < dtKPI.Rows.Count; i++){ %>
                                        <tr data-id="<%=dtKPI.Rows[i]["kpi_id"].ToString() %>">
                                          <td><input name="checkbox-kpi" id='kpi_id_<%=dtKPI.Rows[i]["kpi_id"].ToString() %>' type="checkbox" value="<%=dtKPI.Rows[i]["kpi_id"].ToString() %>" /></td>
                                          <td><%=dtKPI.Rows[i]["name"].ToString() %></td>
                                          <%--<td class='text-center'><input type="text" class='form-control' id='dvt_<%=dtKPI.Rows[i]["kpi_id"].ToString() %>' size="5"/></td>--%>
                                          <td class='text-center'>
                                              <select class='form-control' id='dvt_<%=dtKPI.Rows[i]["kpi_id"].ToString() %>'>
                                                  <% for (int nDVT = 0; nDVT < dtDVT.Rows.Count; nDVT++){ %>
                                                  <option value="<% =dtDVT.Rows[nDVT]["dvt_id"].ToString() %>"><% =dtDVT.Rows[nDVT]["dvt_ten"].ToString() %></option>
                                                  <% } %>
                                              </select>
                                          </td>
                                          <td class='text-center'><input type="text" class='form-control' onkeypress='return onlyNumbers(event.charCode || event.keyCode);' id='tytrong_<%=dtKPI.Rows[i]["kpi_id"].ToString() %>' size="2"/></td>
                                        </tr>
                                    <% } %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="col-sm-8 col-sm-offset-3">
                            <button type="button" class="btn btn-success" id="btnSave">Lưu</button>
                            <button type="button" class="btn btn-default" id="btnClean" onclick="clearInputs()">Reset</button>
                        </div>
                    </div>
                </div>
            </div>
          </div>
        </div>
    </div>
<script type="text/javascript">
    var donvinhan = "<%=donvinhan%>";
    var nguoitao = "<%= nguoitao%>";

    function onlyNumbers(e) {
        //if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
        return !(e > 31 && (e < 48 || e > 57));
    }

    function check_kpi_duocgiao() {
        $("#checkall-kpiduocgiao").click(function () {
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

    // Fill data khi click danh sách mẫu bsc
    function fillData(month, year, nguoitao, bscduocgiao) {
        $("input[type=text]").each(function () {
            $(this).val("");
        });
        $("#month").val(month);
        $("#year").val(year);

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
            nguoitao: nguoitao
        };

        var szRequest = JSON.stringify(requestData);

        swal({
            title: "Tải dữ liệu!",
            text: "Vui lòng chờ trong giây lát.",
            timer: 2000,
            showConfirmButton: false
        });

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
                        $(":checkbox[value='" + KPI_ID + "']").prop("checked", "true");
                        $("#dvt_" + KPI_ID).val(donvitinh);
                        $("#tytrong_" + KPI_ID).val(tytrong);
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
            donvinhan: donvinhan,
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "MauBSCNhanVien.aspx/loadKPIDuocGiao",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#kpiduocgiao").html(result.d);
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
        // Hiển thị danh sách các chức năng của ở BSC
        $(".qlybsc_nv a").click();

        validateNumber("month");
        validateNumber("year");

        // Set thời gian hiện tại
        var curTime = new Date();
        var month = curTime.getMonth() + 1;
        var year = curTime.getFullYear();
        loadKPIDuocGiao(month, year, donvinhan);


        // Khi thay đổi bsc được giao
        $("#bscduocgiao").change(function () {
            var data = $(this).val();
            var arrDate = data.split("-");
            var thang = arrDate[0];
            var nam = arrDate[1];
            var donvinhan = arrDate[2];
            loadKPIDuocGiao(thang, nam, donvinhan);
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

            var isMonth = validateMonth("month");
            var isYear = validateYear("year");
            if (!isMonth || !isYear) {
                swal("Error", "Vui lòng nhập đúng vào trường bất buộc!!!", "error");
                return false;
            }

            var arrKPI = new Array();
            $("#danhsachKPIDuocGiao > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                //var tytrong = $("#tytrong_" + kpi_id).val();
                //var dvt = $("#dvt_" + kpi_id).val();
                var tytrong = $("#tytrong_" + kpi_id).val();
                var dvt = $("#dvt_" + kpi_id).val();
                var isChecked = $("#kpi_id_" + kpi_id).is(":checked");
                if (isChecked == true) {
                    arrKPI.push({
                        kpi_id: kpi_id,
                        tytrong: tytrong,
                        dvt: dvt
                    });
                }
            });

            $("#danhsachKPI > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                //var tytrong = $("#tytrong_" + kpi_id).val();
                //var dvt = $("#dvt_" + kpi_id).val();
                var tytrong = $("#tytrong_" + kpi_id).val();
                var dvt = $("#dvt_" + kpi_id).val();
                var isChecked = $("#kpi_id_" + kpi_id).is(":checked");
                if (isChecked == true) {
                    arrKPI.push({
                        kpi_id: kpi_id,
                        tytrong: tytrong,
                        dvt: dvt
                    });
                }
            });

            if (arrKPI.length == 0) {
                swal("Error", "Vui lòng chọn KPI!!!", "error");
                return false;
            }

            var requestData = {
                monthAprove: month,
                yearAprove: year,
                arrKPI_ID: arrKPI,
                nguoitao: nguoitao,
                bscduocgiao: bscduocgiao
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
    });
</script>
</asp:Content>
