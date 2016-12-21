<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="nhomquyen.aspx.cs" Inherits="VNPT_BSC.Admin.nhomquyen" %>

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
                <h3 class="panel-title">DANH MỤC CHỨC DANH</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#addquyen">Thêm nhóm quyền</a>
                        <table id="table-nhomquyen" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Mã quyền</th>
                                    <th>Tên quyền</th>
                                    <th>Mô tả quyền</th>
                                    <th>Edit</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (dtnhomquyen.Rows.Count == 0)
                                   { %>
                                <tr>
                                    <td colspan="2" class="text-center">No item</td>
                                </tr>
                                <% }
                                   else
                                   { %>
                                <% for (int i = 0; i < dtnhomquyen.Rows.Count; i++)
                                   { %>
                                <%
                                       string nq_id = dtnhomquyen.Rows[i][0].ToString();
                                       string nq_ma = dtnhomquyen.Rows[i][1].ToString();
                                       string nq_ten = dtnhomquyen.Rows[i][2].ToString();
                                       string nq_mota = dtnhomquyen.Rows[i][3].ToString();
                                     
                                %>
                                <tr>
                                    <td><%= nq_id %></td>
                                    <td><%=  nq_ma%></td>
                                    <td><%=  nq_ten%></td>
                                    <td><%=  nq_mota%></td>
                                    <td>
                                        <a class="btn btn-primary btn-xs" type="button" data-target="#Editnhomquyen" data-toggle="modal" onclick="editdata('<%=nq_id %>','<%=nq_ma%>','<%=nq_ten%>','<%=nq_mota%>')">Chỉnh sửa</a>
                                        <a class="btn btn-danger btn-xs" type="button" onclick="deletedata('<%=nq_id %>')">Xóa</a>

                                    </td>
                                </tr>
                                <% } %>
                                <% } %>
                            </tbody>
                        </table>
                        <!----------------------------------------------------------THÊM--------------------------------------------------------------->
                        <div id="addquyen" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Thêm nhóm quyền</h4>
                                    </div>
                                    <div class="modal-body list-BSC form-horizontal">

                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Mã quyền:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txtmaquyen" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Tên quyền:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txttenquyen" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Mô tả quyền:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txtmotaquyen" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-success" id="btnSave">Thêm quyền</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!----------------------------------------------------------SỬA--------------------------------------------------------------->
                        <div id="Editnhomquyen" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Chỉnh sửa loại quyền</h4>
                                    </div>
                                    <input type="hidden" id="txtidquyen_sua" />
                                    <div class="modal-body list-BSC form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Mã quyền:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txttmaquyen_sua" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Tên quyền:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txttenquyen_sua" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Mô tả quyền:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txtmota_sua" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-primary" id="btnGhi">Ghi lại</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
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
            var quyen_ma = $('#txtmaquyen').val();
            var quyen_ten = $('#txttenquyen').val();
            var quyen_mota = $('#txtmotaquyen').val();
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
            var quyen_ma_sua = $('#txttmaquyen_sua').val();
            var quyen_ten_sua = $('#txttenquyen_sua').val();
            var quyen_mota_sua = $('#txtmota_sua').val();
            if (quyen_ma_sua == "" || quyen_ten_sua == "" || quyen_mota_sua == "") {
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

        function editdata(nq_id, nq_ma, nq_ten, nq_mota) {
            $('#txtidquyen_sua').val(nq_id);
            $('#txttmaquyen_sua').val(nq_ma);
            $('#txttenquyen_sua').val(nq_ten);
            $('#txtmota_sua').val(nq_mota);
        }

        function deletedata(nq_id) {
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
                               nq_id_xoaAprove: nq_id,
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "nhomquyen.aspx/DeleteData",
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
            $("#table-nhomquyen").DataTable({
                "searching": true,
                "info": true,
            });


            $("#btnSave").click(function () {
                var quyen_ma = $("#txtmaquyen").val();
                var quyen_ten = $('#txttenquyen').val();
                var quyen_mota = $("#txtmotaquyen").val();
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    quyen_maA: quyen_ma,
                    quyen_tenA: quyen_ten,
                    quyen_motaA: quyen_mota
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "nhomquyen.aspx/SaveData",
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

            $('#btnGhi').click(function () {
                var quyen_id_sua = $("#txtidquyen_sua").val();
                var quyen_ma_sua = $("#txttmaquyen_sua").val();
                var quyen_ten_sua = $("#txttenquyen_sua").val();
                var quyen_mota_sua = $("#txtmota_sua").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    quyen_id_suaA: quyen_id_sua,
                    quyen_ma_suaA: quyen_ma_sua,
                    quyen_ten_suaA: quyen_ten_sua,
                    quyen_mota_suaA: quyen_mota_sua
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "nhomquyen.aspx/EditData",
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
