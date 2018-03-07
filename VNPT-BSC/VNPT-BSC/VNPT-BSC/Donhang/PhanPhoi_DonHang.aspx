<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="PhanPhoi_DonHang.aspx.cs" Inherits="VNPT_BSC.Donhang.PhanPhoi_DonHang" %>
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
                <h3 class="panel-title">DANH SÁCH ĐƠN HÀNG CHƯA GIAO</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#giaonhanvien">CHỌN NHÂN VIÊN</a>
                    </div>
                    <table id="table-donhang" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th class="text-center">#</th>
                                <th class="text-center">Mã HĐ</th>
                                <th class="text-center">Tên KH</th>
                                <th class="text-center">Số điện thoại</th>
                                <th class="text-center">Địa chỉ</th>
                                <th class="text-center">108 Ghi chú</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% for (int i = 0; i < dtDonHang.Rows.Count; i++){%>
                            <tr data-id="<%=dtDonHang.Rows[i]["ma_donhang"].ToString() %>">
                                <td class="text-center"><input name="checkbox-donhang" type="checkbox" value="<%=dtDonHang.Rows[i]["ma_donhang"].ToString() %>" /></td>
                                <td class="text-center"><strong><%=dtDonHang.Rows[i]["ma_donhang"].ToString() %></strong></td>
                                <td><strong><%=dtDonHang.Rows[i]["ten_khachhang"].ToString() %></strong></td>
                                <td><strong><%=dtDonHang.Rows[i]["sodienthoai"].ToString() %></strong></td>
                                <td><strong><%=dtDonHang.Rows[i]["diachi"].ToString() %></strong></td>
                                <td><strong><%=dtDonHang.Rows[i]["ghichu"].ToString() %></strong></td>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                    <!-- Add -->
                    <div id="giaonhanvien" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">CHỌN NHÂN VIÊN CẦN GIAO</h4>
                                </div>
                                <div class="modal-body form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Nhân viên tiếp nhận:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="nhansu">
                                                <% for (int i = 0; i < dtNhanSu.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string id = dtNhanSu.Rows[i]["nhanvien_id"].ToString();
                                                       string ten_nhanvien = dtNhanSu.Rows[i]["nhanvien_hoten"].ToString();
                                                %>
                                                <option value="<%= id%>"><%= ten_nhanvien%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnSave">Phân phối</a>
                                    <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#table-donhang").dataTable();

        $("#btnSave").click(function () {
            var nhansu = $("#nhansu").val();
            var arrDonHang = new Array();
            $('input:checkbox:checked').each(function () {
                arrDonHang.push($(this).val());
            });

            if (arrDonHang.length == 0) {
                swal("Error", "Vui lòng chọn đơn hàng!!!", "error");
                return false;
            }

            var requestData = {
                nhansu: nhansu,
                arrDonHang_ID: arrDonHang
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "PhanPhoi_DonHang.aspx/SaveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d) {
                        swal({
                            title: "Thành Công",
                            text: "Phân phối đơn hàng thành công",
                            type: "success",
                            timer: 1000,
                            showConfirmButton: false
                        },
                        function () {
                            window.location.reload();
                        });
                    }
                    else {
                        swal("Oops...!", "Phân phối đơn hàng không thành công!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
