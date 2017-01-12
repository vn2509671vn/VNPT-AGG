<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="Phanquyen.aspx.cs" Inherits="VNPT_BSC.Admin.Phanquyen" %>

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
                <h3 class="panel-title">DANH MỤC PHÂN QUYỀN</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themquyen">Thêm phân quyền</a>
                    </div>
                    <table id="table-kpi" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th>Quyền</th>
                                <th>Chức vụ</th>
                                <th>Mô tả</th>
                                <th class="fix-table-edit-edit">Chỉnh sửa</th>
                            </tr>
                        </thead>
                         <tbody>
                            <% if (dtphanquyen.Rows.Count == 0)
                               { %>
                            <tr>
                                <td colspan="6" class="text-center">No item</td>
                            </tr>
                            <% }
                               else
                               { %>
                            <% for (int i = 0; i < dtphanquyen.Rows.Count; i++)
                               { %>
                            <%
                                   string quyen_ten = dtphanquyen.Rows[i]["quyen_ten"].ToString();
                                   string quyen_chucvu = dtphanquyen.Rows[i]["chucvu_ten"].ToString();
                                   string quyen_mota = dtphanquyen.Rows[i]["mota"].ToString();
                                   string quyen_chucvu_id = dtphanquyen.Rows[i]["chucvu_id"].ToString();
                                   string quyen_id = dtphanquyen.Rows[i]["quyen_id"].ToString();
                               
                            %>
                            <tr>
                                <td><%= quyen_ten %></td>
                                <td><%= quyen_chucvu%></td>
                                <td><%= quyen_mota%></td>
                                <td>
                                    <a class="btn btn-primary btn-xs" type="button" data-target="#Editquyen" data-toggle="modal" onclick="editdata('<%=quyen_mota%>','<%=quyen_chucvu_id%>','<%=quyen_id%>')">Chỉnh sửa</a>
                                    <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=quyen_id %>','<%=quyen_chucvu_id %>')">Xóa</a>

                                </td>
                            </tr>
                            <% } %>
                            <% } %>
                        </tbody>
                    </table>
                    <!-- ADD-->
                    <div id="themquyen" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">THÊM PHÂN QUYỀN</h4>
                                </div>
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên quyền:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="txttenquyen">
                                                <% for (int i = 0; i < dtquyen.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string quyen_id = dtquyen.Rows[i]["quyen_id"].ToString();
                                                       string quyen_ten = dtquyen.Rows[i]["quyen_ten"].ToString();
                                                %>
                                                <option value="<%= quyen_id%>"><%= quyen_ten%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Chức vụ:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="txtchucvu">
                                                <% for (int i = 0; i < dtchucvu.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string chucvu_id = dtchucvu.Rows[i]["chucvu_id"].ToString();
                                                       string chucvu_ten = dtchucvu.Rows[i]["chucvu_ten"].ToString();
                                                %>
                                                <option value="<%= chucvu_id%>"><%= chucvu_ten%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Mô tả:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350 fix-height-34" id="txtmota" />
                                        </div>
                                    </div>
                                   
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnSave">Thêm phân quyền</a>
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
                                    <h4 class="modal-title" style="text-align: center">CHỈNH SỬA PHÂN QUYỀN</h4>
                                </div>
                                <input type="hidden" id="txtquyen_sua_old" />
                                <input type="hidden" id="txtchucvu_sua_old" />
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên quyền:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="txtquyen_sua">
                                                <% for (int i = 0; i < dtquyen.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string quyen_id = dtquyen.Rows[i]["quyen_id"].ToString();
                                                       string quyen_ten = dtquyen.Rows[i]["quyen_ten"].ToString();
                                                %>
                                                <option value="<%= quyen_id%>"><%= quyen_ten%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Chức vụ:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="txtchucvu_sua">
                                                <% for (int i = 0; i < dtchucvu.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string chucvu_id = dtchucvu.Rows[i]["chucvu_id"].ToString();
                                                       string chucvu_ten = dtchucvu.Rows[i]["chucvu_ten"].ToString();
                                                %>
                                                <option value="<%= chucvu_id%>"><%= chucvu_ten%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Mô tả:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350 fix-height-34" id="txtmota_sua" />
                                        </div>
                                    </div>
                                   
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnEdit">Lưu thay đổi</a>
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
        
            var quyen_mota = $("#txtmota").val();
            if (quyen_mota == "") {
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
            var quyen_mota_sua = $('#txtmota_sua').val();
            
            if (quyen_mota_sua == "") {
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

        function editdata(quyen_mota, quyen_chucvu_id, quyen_id) {
            $('#txtquyen_sua_old').val(quyen_id);
            $('#txtchucvu_sua_old').val(quyen_chucvu_id);
            $('#txtquyen_sua').val(quyen_id);
            $('#txtchucvu_sua').val(quyen_chucvu_id);
            $('#txtmota_sua').val(quyen_mota);
        }

        function deletedata(quyen_id, quyen_chucvu_id) {
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
                               quyen_idA: quyen_id,
                               quyen_chucvu_idA: quyen_chucvu_id
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "Phanquyen.aspx/DeleteData",
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
                var quyen_ten = $("#txttenquyen").val();
                var quyen_chucvu = $('#txtchucvu').val();
                var quyen_mota = $("#txtmota").val();
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    quyen_tenA: quyen_ten,
                    quyen_chucvuA: quyen_chucvu,
                    quyen_motaA: quyen_mota
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "Phanquyen.aspx/SaveData",
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
                var quyen_ten_sua_old = $("#txtquyen_sua_old").val();
                var quyen_chucvu_sua_old = $("#txtchucvu_sua_old").val();
                var quyen_ten_sua = $("#txtquyen_sua").val();
                var quyen_chucvu_sua = $("#txtchucvu_sua").val();
                var quyen_mota_sua = $("#txtmota_sua").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    quyen_ten_sua_oldA: quyen_ten_sua_old,
                    quyen_chucvu_sua_oldA: quyen_chucvu_sua_old,
                    quyen_ten_suaA: quyen_ten_sua,
                    quyen_chucvu_suaA: quyen_chucvu_sua,
                    quyen_mota_suaA: quyen_mota_sua
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "Phanquyen.aspx/EditData",
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
