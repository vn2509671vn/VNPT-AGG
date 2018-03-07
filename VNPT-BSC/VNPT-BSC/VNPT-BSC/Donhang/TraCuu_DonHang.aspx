<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="TraCuu_DonHang.aspx.cs" Inherits="VNPT_BSC.Donhang.TraCuu_DonHang" %>
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
            <h3 class="panel-title">TRA CỨU ĐƠN HÀNG</h3>
          </div>
          <div class="panel-body">              
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-4">Từ ngày:</label>
                    <div class="col-sm-5">
                        <input type="date" class"form-control" name="ngay_bd" value="<%=DateTime.Today.ToString("yyyy-MM-dd") %>"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Đến ngày:</label>
                    <div class="col-sm-5">
                        <input type="date" class"form-control" name="ngay_kt" value="<%=DateTime.Today.ToString("yyyy-MM-dd") %>"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Đơn vị:</label>
                    <div class="col-sm-5">
                        <select class="form-control" id="donvi">
                            <option value="0" selected="selected">Tất cả</option>
                            <% for(int i = 0; i < dtDonVi.Rows.Count; i++){ %>
                            <option value="<%=dtDonVi.Rows[i]["id"].ToString().Trim() %>"><%=dtDonVi.Rows[i]["ten_donvi"].ToString().Trim() %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Nhân viên:</label>
                    <div class="col-sm-5">
                        <select class="form-control" id="nhanvien">
                            <option value="0" selected="selected">Tất cả</option>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Trạng thái:</label>
                    <div class="col-sm-4">
                        <select class="form-control" id="trangthai">
                            <option value="0" selected="selected">Tất cả</option>
                            <option value="1">Chưa giao</option>
                            <option value="2">Chưa xử lý</option>
                            <option value="3">Đã xử lý</option>
                        </select>
                    </div>
                </div>
                <div class="col-sm-12 text-center">
                    <a class="btn btn-success" id="traCuu">Tra cứu</a>
                </div>
              </div>
              <div class="col-md-12 col-xs-12" id="gridTraCuu">

              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#donvi").change(function () {
            var donvi = $(this).val();
            var requestData = {
                donvi: donvi
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "TraCuu_DonHang.aspx/loadNhanVien",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var data = result.d;
                    $("#nhanvien").html(data);
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#traCuu").click(function () {
            var ngay_bd = $("input[name=ngay_bd]").val();
            var ngay_kt = $("input[name=ngay_kt]").val();
            var donvi = $("#donvi").val();
            var nhanvien = $("#nhanvien").val();
            var trangthai = $("#trangthai").val();

            if (ngay_bd == "" || ngay_kt == "") {
                alert("Vui lòng nhập ngày tra cứu!!!");
                return false;
            }

            var requestData = {
                ngay_bd: ngay_bd,
                ngay_kt: ngay_kt,
                donvi: donvi,
                nhanvien: nhanvien,
                trangthai: trangthai
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "TraCuu_DonHang.aspx/loadTB",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var output = result.d;
                    $("#gridTraCuu").html(output);
                    $("#table-tracuu").DataTable({
                        "bLengthChange": false,
                        "bPaginate": false,
                        "bSort": true,
                        "dom": 'Bfrtip',
                        "buttons": [
                            {
                                extend: 'excelHtml5',
                            },
                            {
                                extend: 'pdfHtml5',
                                orientation: 'landscape',
                                pageSize: 'LEGAL'
                            }
                        ]
                    });
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
