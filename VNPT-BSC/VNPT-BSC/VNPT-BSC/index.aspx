<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VNPT_BSC.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Bootstrap/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Bootstrap/sweetalert.css">
    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>

    <script src="../Bootstrap/Alert.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Lọc dữ liệu</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12 form-horizontal">
                    <div class="form-group">
                        <label class="control-label col-sm-6">Lọc theo tháng/năm:</label>
                        <div class="col-sm-6 form-inline">
                            <select class="form-control" id="month">
                                <% for (int i = 1; i <= 12; i++)
                                   {
                                       string selectOption = "";
                                       int month = Convert.ToInt32(DateTime.Now.ToString("MM"));
                                       if (i == month)
                                       {
                                           selectOption = "selected";
                                       }
                                %>
                                <option value="<%=i %>" <%=selectOption %>><%=i %></option>
                                <% } %>
                            </select>
                            <select class="form-control" id="year">
                                <% for (int i = 2016; i <= 2100; i++)
                                   {
                                       string selectOption = "";
                                       int date = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                                       if (i == date)
                                       {
                                           selectOption = "selected";
                                       }
                                %>
                                <option value="<%=i %>" <%=selectOption %>><%=i %></option>
                                <% } %>
                            </select>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="col-md-12 margin-top-30" id="Div1">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Danh sách BSC đơn vi</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12" id="gridBSC">
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12 margin-top-30" id="Div2">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Danh sách KPI nhân viên</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12" id="gridBSCnhanvien">
                </div>
            </div>
        </div>
    </div>
     <div class="col-md-12 margin-top-30" id="Div3">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Danh sách KPI thẩm định</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12" id="gridBSCnhanvienthamdinh">
                </div>
            </div>
        </div>
    </div>





    <script>
        function loadBSCByYear(month, year, donvi_id) {
            var requestData = {
                nam: year,
                thang: month,
                nhanvien_donvi_id: donvi_id
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "index.aspx/loadBSCByYear",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var output = result.d;
                    var gridBSC = output.gridBSC;

                    $("#gridBSC").html(gridBSC);
                    $("#table-bsclist").DataTable({
                        "searching": true,
                        "info": true,
                        "lengthMenu": [5, 10, 20]
                    });
                },
                error: function (msg) { alert(msg.d); }
            });
        }


        function loadBSCnhanvienByYear(month, year, nhanvien_id) {
            var requestData = {
                nam: year,
                thang: month,
                nhanvien_id: nhanvien_id
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "index.aspx/loadBSCnhanvienByYear",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var output = result.d;
                    var gridBSCnhanvien = output.gridBSCnhanvien;

                    $("#gridBSCnhanvien").html(gridBSCnhanvien);
                    $("#table-bscnhanvienlist").DataTable({
                        "searching": true,
                        "info": true,
                        "lengthMenu": [5, 10, 20]
                    });
                },
                error: function (msg) { alert(msg.d); }
            });
        }

        function loadBSCnhanvien_thamdinhByYear(month, year, nhanvien_id) {
            var requestData = {
                nam: year,
                thang: month,
                nhanvien_id: nhanvien_id
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "index.aspx/loadBSCnhanvien_thamdinhByYear",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var output = result.d;
                    var gridBSCnhanvienthamdinh = output.gridBSCnhanvienthamdinh;

                    $("#gridBSCnhanvienthamdinh").html(gridBSCnhanvienthamdinh);
                    $("#table-bscnhanvienthamdinh").DataTable({
                        "searching": true,
                        "info": true,
                        "lengthMenu": [5, 10, 20]
                    });
                },
                error: function (msg) { alert(msg.d); }
            });
        }
        $(document).ready(function () {
            var donvi_id = "<%=  donvi_id%>";
            var nhanvien_id = "<%=  nhanvien_id%>";
            // Load grid lần đầu
            loadBSCByYear($("#month").val(), $("#year").val(), donvi_id);
            loadBSCnhanvienByYear($("#month").val(), $("#year").val(), nhanvien_id);
            loadBSCnhanvien_thamdinhByYear($("#month").val(), $("#year").val(), nhanvien_id);

            // Load grid khi năm thay đổi
            $("#year").change(function () {
                var thang = $("#month").val();
                var nam = $(this).val();
                loadBSCByYear(thang, nam, donvi_id);
                loadBSCnhanvienByYear(thang, nam, nhanvien_id);
                loadBSCnhanvien_thamdinhByYear(thang, nam, nhanvien_id);
            });

            // Load grid khi tháng thay đổi
            $("#month").change(function () {
                var nam = $("#year").val();
                var thang = $(this).val();
                loadBSCByYear(thang, nam, donvi_id);
                loadBSCnhanvienByYear(thang, nam, nhanvien_id);
                loadBSCnhanvien_thamdinhByYear(thang, nam, nhanvien_id);
            });
        });
    </script>
</asp:Content>
