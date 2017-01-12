<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="loaiquyen.aspx.cs" Inherits="VNPT_BSC.Admin.loaiquyen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../Bootstrap/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Bootstrap/sweetalert.css">
    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>--%>

    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>
    <script src="../Bootstrap/Alert.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">DANH MỤC LOẠI QUYỀN</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themkpi">Thêm Loại Quyền</a>
                    </div>
                    <div class="table-responsive fix-border-table">
                        <table id="table-kpi" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Mã loại quyền</th>
                                    <th>Tên loại quyền</th>
                                    <th>Mô tả loại quyền</th>
                                    <th>Nhóm quyền</th>
                                    <th class="fix-table-edit-edit">Chỉnh sửa</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (dtquyen.Rows.Count == 0)
                                   { %>
                                <tr>
                                    <td colspan="6" class="text-center">No item</td>
                                </tr>
                                <% }
                                   else
                                   { %>
                                <% for (int i = 0; i < dtquyen.Rows.Count; i++)
                                   { %>
                                <%
                                        string quyen_id = dtquyen.Rows[i]["quyen_id"].ToString();
                                        string quyen_ma = dtquyen.Rows[i]["quyen_maquyen"].ToString();
                                        string quyen_ten = dtquyen.Rows[i]["quyen_ten"].ToString();
                                        string quyen_mota = dtquyen.Rows[i]["quyen_mota"].ToString();
                                        string quyen_nhom = dtquyen.Rows[i]["loaiquyen_ten"].ToString();

                                %>
                                <tr>
                                    <td><%= quyen_id %></td>
                                    <td><%= quyen_ma%></td>
                                    <td><%= quyen_ten%></td>
                                    <td><%= quyen_mota %></td>
                                    <td><%= quyen_nhom%></td>
                                    <td>
                                        <a class="btn btn-primary btn-xs" type="button" data-target="#Editquyen" data-toggle="modal" onclick="editdata('<%=quyen_id %>','<%=quyen_ma%>','<%=quyen_ten%>','<%=quyen_mota%>','<%=quyen_nhom%>')">Chỉnh sửa</a>
                                        <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=quyen_id %>')">Xóa</a>

                                    </td>
                                </tr>
                                <% } %>
                                <% } %>
                            </tbody>
                        </table>
                    </div>
                    <!-- ADD-->
                    <div id="themkpi" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">THÊM LOẠI QUYỀN</h4>
                                </div>
                                <div class="modal-body list-BSC form-horizontal">

                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mã loại quyền:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmaquyen" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Tên loại quyền:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350 fix-height-34" id="txttenquyen" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Mô tả loại quyền:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350 fix-height-34" id="txtmotaquyen" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Nhóm quyền:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="txtnhomquyen">
                                                <% for (int i = 0; i < dtnhomquyen.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string nhomquyen_id = dtnhomquyen.Rows[i]["loaiquyen_id"].ToString();
                                                       string nhomquyen_ten = dtnhomquyen.Rows[i]["loaiquyen_ten"].ToString();
                                                %>
                                                <option value="<%= nhomquyen_id%>"><%= nhomquyen_ten%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnSave">Thêm loại quyền</a>
                                    <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--------------------------------------------------------EDIT---->
                    <div id="Editquyen" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">CHỈNH SỬA LOẠI QUYỀN</h4>
                                </div>
                                <input type="hidden" id="txtidquyen_sua" />
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mã loại quyền:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmaquyen_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên loại quyền:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txttenquyen_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả loại quyền:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmotaquyen_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Nhóm quyền:</label>
                                        <div class="col-sm-8">
                                            <%--<input type="text" class="form-control fix-width-350 fix-height-34"  id="txtthuockpo_sua" />--%>
                                            <select class="form-control fix-day" id="txtnhomquyen_sua">
                                                <% for (int i = 0; i < dtnhomquyen.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string nhomquyen_id = dtnhomquyen.Rows[i]["loaiquyen_id"].ToString();
                                                       string nhomquyen_ten = dtnhomquyen.Rows[i]["loaiquyen_ten"].ToString();
                                                %>
                                                <option value="<%= nhomquyen_id%>"><%= nhomquyen_ten%></option>
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
    <script>
        function checkItemThem() {
            var quyen_ma = $("#txtmaquyen").val();
            var quyen_ten = $('#txttenquyen').val();
            var quyen_mota = $("#txtmotaquyen").val();
            if (quyen_ma == "" || quyen_ten == "" || quyen_mota == "") {
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
            var quyen_ma_sua = $('#txtmaquyen_sua').val();
            var quyen_ten_sua = $('#txttenquyen_sua').val();
            var quyen_mota_sua = $('#txtmotaquyen_sua').val();
            if (quyen_ma_sua == "" || quyen_ten_sua == "" || quyen_mota_sua == "") {
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

        function editdata(quyen_id, quyen_ma, quyen_ten, quyen_mota, quyen_nhom) {
            $('#txtidquyen_sua').val(quyen_id);
            $('#txtmaquyen_sua').val(quyen_ma);
            $('#txttenquyen_sua').val(quyen_ten);
            $('#txtmotaquyen_sua').val(quyen_mota);
            $('#txtnhomquyen').val(quyen_nhom);

        }

        function deletedata(quyen_id) {
            swal({
                title: "Bạn có chắc không?",
                text: "Bạn sẽ xóa dữ liệu này!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Xóa dữ liệu",
                cancelButtonText: "Đóng",
                closeOnConfirm: false,
                closeOnCancel: false
            },
                   function (isConfirm) {
                       if (isConfirm) {
                           var requestData = {
                               quyen_idAprove: quyen_id,
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "loaiquyen.aspx/DeleteData",
                               data: szRequest,
                               contentType: "application/json; charset=utf-8",
                               dataType: "json",
                               success: function (result) {
                                   if (result.d) {
                                       swal({
                                           title: "Thành Công",
                                           text: "Bạn đã Delete dữ liệu thành công",
                                           type: "success",
                                           timer: 1000,
                                           showConfirmButton: false
                                       },
                                       function () {
                                           window.location.reload();
                                       });
                                   }
                                   else {
                                       alert_loi();
                                   }
                               },
                           });
                       } else {
                           swal({
                               title: "Đóng",
                               text: "Hủy xóa dữ liệu",
                               type: "error",
                               timer: 1000,
                               showConfirmButton: false
                           })
                       }
                   });
        }

        $(document).ready(function () {

            $("#btnSave").click(function () {
                var quyen_ma = $("#txtmaquyen").val();
                var quyen_ten = $('#txttenquyen').val();
                var quyen_mota = $("#txtmotaquyen").val();
                var quyen_nhom = $("#txtnhomquyen").val();
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    quyen_maA: quyen_ma,
                    quyen_tenA: quyen_ten,
                    quyen_motaA: quyen_mota,
                    quyen_nhomA: quyen_nhom
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "loaiquyen.aspx/SaveData",
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
                            alert_loi();
                        }
                    },
                    error: function (msg) { alert(msg.d); }
                });
            });

            $('#btnEdit').click(function () {
                var quyen_id_sua = $("#txtidquyen_sua").val();
                var quyen_ma_sua = $("#txtmaquyen_sua").val();
                var quyen_ten_sua = $("#txttenquyen_sua").val();
                var quyen_mota_sua = $("#txtmotaquyen_sua").val();
                var quyen_nhom_sua = $("#txtnhomquyen_sua").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    quyen_id_suaA: quyen_id_sua,
                    quyen_ma_suaA: quyen_ma_sua,
                    quyen_ten_suaA: quyen_ten_sua,
                    quyen_mota_suaA: quyen_mota_sua,
                    quyen_nhom_suaA: quyen_nhom_sua
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "loaiquyen.aspx/EditData",
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
                            alert_loi();
                        }
                    },
                    error: function (msg) { alert(msg.d); }
                });
            });





        });
    </script>
</asp:Content>
