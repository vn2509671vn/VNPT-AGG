<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="nhanvienchucvu.aspx.cs" Inherits="VNPT_BSC.DanhMuc.nhanvienchucvu" %>

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
                <h3 class="panel-title">Danh mục nhân vien - chức vụ</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#chucvu">Gán chức vụ cho nhân viên</a>
                    </div>
                    <table id="table-nv_cv" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th>Nhân viên</th>
                                <th>Chức vụ</th>
                                <th>Edit</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% if (dtnv_cv.Rows.Count == 0)
                               { %>
                            <tr>
                                <td colspan="2" class="text-center">No item</td>
                            </tr>
                            <% }
                               else
                               { %>
                            <% for (int i = 0; i < dtnv_cv.Rows.Count; i++)
                               { %>
                            <%
                                   string nv_ten = dtnv_cv.Rows[i]["nhanvien_hoten"].ToString();
                                   string cv_ten = dtnv_cv.Rows[i]["chucvu_ten"].ToString();
                                   string nv_id = dtnv_cv.Rows[i]["nhanvien_id"].ToString();
                                   string cv_id = dtnv_cv.Rows[i]["chucvu_id"].ToString();
                                       
                                     
                            %>
                            <tr>
                                <td><%= nv_ten %></td>
                                <td><%=  cv_ten%></td>

                                <td>
                                    <a class="btn btn-primary btn-xs" type="button" data-target="#Editnv_cv" data-toggle="modal" onclick="editdata('<%=cv_id%>','<%=nv_id %>')">Chỉnh sửa</a>
                                    <a class="btn btn-danger btn-xs" type="button" onclick="deletedata('<%=cv_id %>','<%=nv_id %>')">Xóa</a>

                                </td>
                            </tr>
                            <% } %>
                            <% } %>
                        </tbody>
                    </table>
                    <!--------------------------------------------------------Add---->
                    <div id="chucvu" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">Gán chức vụ</h4>
                                </div>
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Nhân viên:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control col-sm-8" list="danhsachnhanvien" size="50" id="nhanvien_add" />
                                            <datalist id="danhsachnhanvien">
                                                <% for (int i = 0; i < dtnhanvien.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string nhanvien_taikhoan = dtnhanvien.Rows[i]["nhanvien_taikhoan"].ToString();
                                                       string nhanvien_hoten = dtnhanvien.Rows[i]["nhanvien_hoten"].ToString();
                                                %>
                                                <option value="<%= nhanvien_taikhoan%>"><%= nhanvien_hoten%></option>
                                                <% } %>
                                            </datalist>
                                        </div>
                                    </div>
                                   <%-- <div class="form-group">
                                        <label class="control-label col-sm-4">Nhân viên:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="nhanvien_add">
                                                <% for (int i = 0; i < dtnhanvien.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string nhanvien_id = dtnhanvien.Rows[i]["nhanvien_id"].ToString();
                                                       string nhanvien_ten = dtnhanvien.Rows[i]["nhanvien_hoten"].ToString();
                                                %>
                                                <option value="<%= nhanvien_id%>"><%= nhanvien_ten%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Chức vụ:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="chucvu_add">
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


                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnSave">Thêm</a>
                                    <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--------------------------------------------------------EDIT---->
                    <div id="Editnv_cv" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">CHỈNH SỬA NHÂN VIÊN - CHỨC VỤ</h4>
                                </div>
                                <input type="hidden" id="txtnv_id_old" />
                                <input type="hidden" id="txtcv_id_old" />
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Nhân viên:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="txtnhanvien_sua">
                                                <% for (int i = 0; i < dtnhanvien.Rows.Count; i++)
                                                   { %>
                                                <%
                                                       string nhanvien_id = dtnhanvien.Rows[i]["nhanvien_id"].ToString();
                                                       string nhanvien_ten = dtnhanvien.Rows[i]["nhanvien_hoten"].ToString();
                                                %>
                                                <option value="<%= nhanvien_id%>"><%= nhanvien_ten%></option>
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
        function editdata(cv_id, nv_id) {
            $('#txtnv_id_old').val(nv_id);
            $('#txtnhanvien_sua').val(nv_id);
            $('#txtcv_id_old').val(cv_id);
            $('#txtchucvu_sua').val(cv_id);
        }
        function deletedata(cv_id, nv_id) {
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
                               cv_idA: cv_id,
                               nv_idA: nv_id
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "nhanvienchucvu.aspx/DeleteData",
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
                var nv_id = $("#nhanvien_add").val();
                var cv_id = $('#chucvu_add').val();

                var requestData = {
                    nv_idA: nv_id,
                    cv_idA: cv_id
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "nhanvienchucvu.aspx/SaveData",
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
                var nv_id_old = $("#txtnv_id_old").val();
                var cv_id_old = $("#txtcv_id_old").val();
                var nv_id = $("#txtnhanvien_sua").val();
                var cv_id = $("#txtchucvu_sua").val();

                var requestData = {
                    nv_id_oldA: nv_id_old,
                    cv_id_oldA: cv_id_old,
                    nv_idA: nv_id,
                    cv_idA: cv_id
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "nhanvienchucvu.aspx/EditData",
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


            $("#table-nv_cv").DataTable({
                "searching": true,
                "info": true,
            });
        });
    </script>
</asp:Content>
