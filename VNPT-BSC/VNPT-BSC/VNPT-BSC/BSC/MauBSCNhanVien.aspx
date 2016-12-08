<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MauBSCNhanVien.aspx.cs" Inherits="VNPT_BSC.BSC.MauBSCNhanVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
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
                    <div class="panel-heading">
                        <h3 class="panel-title">Danh Sách Mẫu BSC</h3>
                    </div>
                    <ul class="list-group">
                        <% for(int i = 0; i < dtBSC.Rows.Count; i++){ %>
                            <%
                                string month =  dtBSC.Rows[i][0].ToString();
                                string year =  dtBSC.Rows[i][1].ToString();
                            %>
                            <a href="#" onclick="fillData(<%=month %>, <%=year %>, <%=nguoitao %>)" class="list-group-item list-group-item-info text-center"><%= month +"/"+ year%></a>
                        <% } %>
                    </ul>
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
                    <div class="col-sm-8 col-sm-offset-3" id="kpiduocgiao">
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Danh sách KPI:</label>
                    <div class="col-sm-8">
                        <% for(int i = 0; i < dtKPI.Rows.Count; i++){ %>
                            <div class="checkbox">
                              <label><input type="checkbox" value="<%=dtKPI.Rows[i]["kpi_id"].ToString() %>" /><%=dtKPI.Rows[i]["name"].ToString() %></label>
                            </div>
                        <% } %>
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

    // Fill data khi click danh sách mẫu bsc
    function fillData(month, year, nguoitao) {
        $("#month").val(month);
        $("#year").val(year);

        /*Remove red border*/
        $("#month").css("border-color", "#ccc");
        $("#year").css("border-color", "#ccc");

        // BSC được giao
        var dsKPIDuocGiao = month + "-" + year + "-" + donvinhan;
        $("#bscduocgiao").val(dsKPIDuocGiao);
        loadKPIDuocGiao(month, year, donvinhan);

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
                        var KPI_ID = arrKPI[i];
                        $(":checkbox[value='" + KPI_ID + "']").prop("checked", "true");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        },2000);
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
            var isMonth = validateMonth("month");
            var isYear = validateYear("year");
            if (!isMonth || !isYear) {
                swal("Error", "Vui lòng nhập đúng vào trường bất buộc!!!", "error");
                return false;
            }
            var arrKPI = new Array();
            $("input[type=checkbox]:checked").each(function () {
                var exist = $.inArray($(this).val(), arrKPI);
                if (exist == -1) {
                    arrKPI.push($(this).val());
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
                nguoitao: nguoitao
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
    });
</script>
</asp:Content>
