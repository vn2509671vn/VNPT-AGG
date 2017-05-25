<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="TuDongCapNhatBacLuong.aspx.cs" Inherits="VNPT_BSC.TinhLuong.TuDongCapNhatBacLuong" %>
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

    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
          <div class="panel-heading">
            <h3 class="panel-title">Cập nhật hệ số lương mới theo quý</h3>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-4">Năm:</label>
                    <div class="col-sm-6">
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
                <div class="form-group">
                    <label class="control-label col-sm-4">Quý:</label>
                    <div class="col-sm-6">
                        <select class="form-control" id="quy">
                            <option value="1">I</option>
                            <option value="2">II</option>
                            <option value="3">III</option>
                            <option value="4">IV</option>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-6 col-sm-4 form-inline">
                        <a class="btn btn-success" id="capnhatBL">Cập nhật</a>
                    </div>
                </div>
              </div>
              <div class="col-md-12 col-xs-12" id="gridBL">

              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    function loadHeSoLuongMoi() {
        var nam = $("#year").val();
        var quy = $("#quy").val();

        var requestData = {
            nam: nam,
            quy: quy
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "TuDongCapNhatBacLuong.aspx/loadHeSoLuongMoi",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                $("#gridBL").html(output);
                $("#table-bacluong").DataTable({
                    "dom": 'Bfrtip',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                            title: 'Hệ số lương mới quý ' + quy + ' năm ' + nam
                        },
                        {
                            extend: 'pdfHtml5',
                            title: 'Hệ số lương mới quý ' + quy + ' năm ' + nam,
                            orientation: 'landscape',
                            pageSize: 'LEGAL'
                        }
                    ]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
        $("#capnhatBL").click(function () {
            loadHeSoLuongMoi();
        });
    });
</script>
</asp:Content>
