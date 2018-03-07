<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="Luong3PS.aspx.cs" Inherits="VNPT_BSC.BSC.Luong3PS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />
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
    <script type="text/javascript" src="https://cdn.datatables.net/fixedcolumns/3.2.2/js/dataTables.fixedColumns.min.js"></script>
    
    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Chi Tiết Lương 3PS Của Nhân Viên</h3>
        </div>
        <div class="panel-body">
            <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Thời điểm:</label>
                    <div class="col-sm-6 form-inline">
                        <select class="form-control" id="month">
                            <% for(int i = 1; i <= 12; i++){ 
                                string selectOption = "";
                                int month =  Convert.ToInt32(DateTime.Now.ToString("MM"));
                                if(i == month){
                                    selectOption = "selected";
                                }
                            %>
                            <option value="<%=i %>" <%=selectOption %>><%=i %></option>
                            <% } %>
                        </select>
                        <select class="form-control" id="year">
                            <% for(int i = 2016; i <= 2100; i++){ 
                                string selectOption = "";
                                int year =  Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                                if(i == year){
                                    selectOption = "selected";
                                }
                            %>
                            <option value="<%=i %>" <%=selectOption %>><%=i %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Danh sách Nhân viên
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body" id="gridNhanvien">
                            
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    var thang = "<%= thang %>";
    var nam = "<%= nam %>";
    function loadLuong(thang, nam) {
        var requestData = {
            thang: thang,
            nam: nam
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "Luong3PS.aspx/loadLuong",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#gridNhanvien").html(result.d);
                $("#table-nv").DataTable({
                    "order": [],
                    "pageLength": 200,
                    "dom": 'Bfrtip',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                            title: 'Lương 3Ps ' + thang + "-" + nam
                        },
                        {
                            extend: 'pdfHtml5',
                            title: 'Lương 3Ps ' + thang + "-" + nam,
                            orientation: 'landscape',
                            pageSize: 'LEGAL'
                        }
                    ],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
        $("#month").val(thang);
        $("#year").val(nam);
        loadLuong(thang, nam);

        $("#month").change(function () {
            var month = $(this).val();
            var year = $("#year").val();
            loadLuong(month, year);
        });

        $("#year").change(function () {
            var year = $(this).val();
            var month = $("#month").val();
            loadLuong(month, year);
        });
    });
</script>
</asp:Content>
