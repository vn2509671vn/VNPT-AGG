<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="BacLuong.aspx.cs" Inherits="VNPT_BSC.TinhLuong.ChamCong" %>
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
                <h3 class="panel-title">DANH MỤC BẬC LƯƠNG</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#thembl">Thêm Bậc Lương</a>
                    </div>
                    <table id="table-bacluong" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th class="text-center">Chức danh</th>
                                <th class="text-center">Bậc lương</th>
                                <th class="text-center">Hệ số lương</th>
                                <th class="text-center">Chỉnh sửa</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% for (int i = 0; i < dtBacLuong.Rows.Count; i++){%>
                            <tr data-id="<%=dtBacLuong.Rows[i]["id"].ToString() %>">
                                <td class="ten_chucdanh" data-id-chucdanh="<%=dtBacLuong.Rows[i]["id_chucdanh"].ToString() %>"><strong><%=dtBacLuong.Rows[i]["ten_chucdanh"].ToString() %></strong></td>
                                <td class="bacluong"><strong><%=dtBacLuong.Rows[i]["ten_bacluong"].ToString() %></strong></td>
                                <td class="text-center heso"><strong><%=dtBacLuong.Rows[i]["heso_bacluong"].ToString() %></strong></td>
                                <td class="text-center"><a class="btn btn-primary btn-xs btn-action" data-target="#Editbl" data-toggle="modal">Sửa</a></td>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                    <!-- Add -->
                    <div id="thembl" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">THÊM BẬC LƯƠNG</h4>
                                </div>
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên bậc lương:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtten" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Hệ số lương:</label>
                                        <div class="col-sm-8">
                                            <input type="number" min="0" class="form-control fix-width-350" id="txtheso" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Chức danh:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="chucdanh">
                                                <% for (int i = 0; i < dtChucDanh.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string id = dtChucDanh.Rows[i]["id"].ToString();
                                                       string ten_chucdanh = dtChucDanh.Rows[i]["ten_chucdanh"].ToString();
                                                %>
                                                <option value="<%= id%>"><%= ten_chucdanh%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnSave">Thêm</a>
                                    <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- EDIT ---->
                    <div id="Editbl" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">CHỈNH SỬA BẬC LƯƠNG</h4>
                                </div>
                                <input type="hidden" id="txt_id_bacluong" />
                                <div class="modal-body form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên bậc:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtten_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Hệ số lương:</label>
                                        <div class="col-sm-8">
                                            <input type="number" min="0" class="form-control fix-width-350" id="txtheso_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Chức danh:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="chucdanh_sua">
                                                <% for (int i = 0; i < dtChucDanh.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string id = dtChucDanh.Rows[i]["id"].ToString();
                                                       string ten_chucdanh = dtChucDanh.Rows[i]["ten_chucdanh"].ToString();
                                                %>
                                                <option value="<%= id%>"><%= ten_chucdanh%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnEdit">Lưu</a>
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
    function checkItemThem() {
        var ten_bac = $("#txtten").val();
        var heso = $("#txtheso").val();
        if (ten_bac == "" || heso == "" || isNaN(heso)) {
            swal({
                title: "Lỗi Dữ Liệu",
                text: "Nhập thiếu trường dữ liệu!!!!",
                type: "error",
                timer: 1000,
                showConfirmButton: false
            });
            return false;
        }
        else {
            return true;
        }
    }

    function checkItemSua() {
        var ten_bac = $("#txtten_sua").val();
        var heso = $("#txtheso_sua").val();
        if (ten_bac == "" || heso == "" || isNaN(heso)) {
            swal({
                title: "Dữ liệu không được bỏ trống!!",
                timer: 1000,
                showConfirmButton: false
            });
            return false;
        }
        else {
            return true;
        }
    }

    $(document).ready(function () {
        $("#table-bacluong").dataTable();

        $(document).on('click', '.btn-action', function () {
            var id_bacluong = $(this).closest("tr").attr("data-id");
            var ten_bacluong = $("tr[data-id=" + id_bacluong + "] > td.bacluong").text();
            var heso = $("tr[data-id=" + id_bacluong + "] > td.heso").text();
            var id_chucdanh = $("tr[data-id=" + id_bacluong + "] > td.ten_chucdanh").attr("data-id-chucdanh");

            $("#txt_id_bacluong").val(id_bacluong);
            $("#txtten_sua").val(ten_bacluong);
            $("#txtheso_sua").val(heso);
            $("#chucdanh_sua").val(id_chucdanh);
        });

        $("#btnSave").click(function () {
            var ten_bacluong = $("#txtten").val();
            var heso_bacluong = $("#txtheso").val();
            var id_chucdanh = $('#chucdanh').val();
            var isCheck = checkItemThem();
            if (!isCheck) {
                return false;
            }
            var requestData = {
                ten_bacluong: ten_bacluong,
                heso_bacluong: heso_bacluong,
                id_chucdanh: id_chucdanh
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "BacLuong.aspx/SaveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d) {
                        swal({
                            title: "Thành Công",
                            text: "Bạn đã thêm dữ liệu thành công",
                            type: "success",
                            timer: 1000,
                            showConfirmButton: false
                        },
                        function () {
                            window.location.reload();
                        });
                    }
                    else {
                        swal("Oops...!", "Thêm mới không thành công!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#btnEdit").click(function () {
            var id_bacluong = $("#txt_id_bacluong").val();
            var ten_bacluong = $("#txtten_sua").val();
            var heso_bacluong = $("#txtheso_sua").val();
            var id_chucdanh = $('#chucdanh_sua').val();
            var isCheck = checkItemSua();
            if (!isCheck) {
                return false;
            }
            var requestData = {
                id_bacluong: id_bacluong,
                ten_bacluong: ten_bacluong,
                heso_bacluong: heso_bacluong,
                id_chucdanh: id_chucdanh
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "BacLuong.aspx/EditData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d) {
                        swal({
                            title: "Thành Công",
                            text: "Bạn đã thay đổi dữ liệu thành công",
                            type: "success",
                            timer: 1000,
                            showConfirmButton: false
                        },
                        function () {
                            window.location.reload();
                        });
                    }
                    else {
                        swal("Oops...!", "Thay đổi không thành công!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
