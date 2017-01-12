﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="Chucvu.aspx.cs" Inherits="VNPT_BSC.Chucvu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Bootstrap/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Bootstrap/sweetalert.css"/>
    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />
    <%--<link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" />--%>
    <script src="../Bootstrap/jquery.js"></script>
    <%--<script src="../Bootstrap/bootstrap.js"></script>--%>
    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css"/>
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>

    <script src="../Bootstrap/Alert.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">DANH MỤC CHỨC VỤ</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themchucvu">Thêm chức vụ</a>
                        <table id="table-chucvu" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tên chức vụ</th>
                                    <th>Mô tả chức vụ</th>
                                    <th>Mã chức vụ</th>
                                    <th>Edit</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (dtchucvu.Rows.Count == 0)
                                   { %>
                                <tr>
                                    <td colspan="2" class="text-center">No item</td>
                                </tr>
                                <% }
                                   else
                                   { %>
                                <% for (int i = 0; i < dtchucvu.Rows.Count; i++)
                                   { %>
                                <%
                                       string cv_id = dtchucvu.Rows[i][0].ToString();
                                       string cv_ten = dtchucvu.Rows[i][1].ToString();
                                       string cv_mota = dtchucvu.Rows[i][2].ToString();
                                       string cv_ma = dtchucvu.Rows[i][3].ToString();
                                     
                                %>
                                <tr>
                                    <td><%= cv_id %></td>
                                    <td><%=  cv_ten%></td>
                                    <td><%=  cv_mota%></td>
                                    <td><%=  cv_ma%></td>
                                    <td>
                                        <a class="btn btn-primary btn-xs" type="button" data-target="#Editchucvu" data-toggle="modal" onclick="editdata('<%=cv_id %>','<%=cv_ten%>','<%=cv_mota%>','<%=cv_ma%>')">Chỉnh sửa</a>
                                        <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=cv_id %>')">Xóa</a>

                                    </td>
                                </tr>
                                <% } %>
                                <% } %>
                            </tbody>
                        </table>
                         <!------------------------------------------------------------------ Thêm Chức vụ-->
                        <div id="themchucvu" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Thêm chức vụ</h4>
                                    </div>
                                    
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên chức vụ:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txttenchucvu" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả chức vụ:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmotachucvu" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Mã chức vụ:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmachucvu" />
                                        </div>
                                    </div>
                                </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-success" id="btnSave">Thêm chức vụ</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!------------------------------------------------------------------ Sửa Chức vụ-->
                        <div id="Editchucvu" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Chỉnh sửa chức vụ</h4>
                                    </div>
                                    <input type="hidden" id="txtidchucvu_sua" />
                                    
                                    <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên chức vụ:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txttenchucvu_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả chức vụ:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmotachucvu_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Mã chức vụ:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmachucvu_sua" />
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
                        <!------------------------------------------------------------------ Sửa Chức vụ-->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function checkItemThem() {
            var cd_ten = $('#txttenchucvu').val();
            var cd_ma = $('#txtmachucvu').val();
            if (cd_ten == "" || cd_ma == "") {
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
            var cd_ten = $('#txttenchucvu_sua').val();
            var cd_ma = $('#txtmachucvu_sua').val();
            if (cd_ten == "" || cd_ma == "") {
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

        function editdata(cv_id, cv_ten, cv_mota, cv_ma) {
            $('#txtidchucvu_sua').val(cv_id);
            $('#txttenchucvu_sua').val(cv_ten);
            $('#txtmotachucvu_sua').val(cv_mota);
            $('#txtmachucvu_sua').val(cv_ma);
        }
        function deletedata(cv_id) {
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
                               cv_id_xoaAprove: cv_id,
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "Chucvu.aspx/DeleteData",
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

            $("#table-chucvu").DataTable({
                "searching": true,
                "info": true,
            });


            $("#btnSave").click(function () {
                var cv_ten = $("#txttenchucvu").val();
                var cv_mota = $('#txtmotachucvu').val();
                var cv_ma = $("#txtmachucvu").val();
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    cv_tenAprove: cv_ten,
                    cv_motaAprove: cv_mota,
                    cv_maAprove: cv_ma
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "Chucvu.aspx/SaveData",
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
                var cv_id_sua = $("#txtidchucvu_sua").val();
                var cv_ten_sua = $("#txttenchucvu_sua").val();
                var cv_mota_sua = $("#txtmotachucvu_sua").val();
                var cv_ma_sua = $("#txtmachucvu_sua").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    cv_id_suaAprove: cv_id_sua,
                    cv_ten_suaAprove: cv_ten_sua,
                    cv_mota_suaAprove: cv_mota_sua,
                    cv_ma_suaAprove: cv_ma_sua
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "Chucvu.aspx/EditData",
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

            $('#btnXoa').click(function () {
                var cd_id_xoa = $("#txtidchucvu_xoa").val();

            });
        });
    </script>
</asp:Content>
