<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="Chucdanh.aspx.cs" Inherits="VNPT_BSC.Home" %>

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
                <h3 class="panel-title">DANH MỤC CHỨC DANH</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#listBSC">Thêm chức danh</a>
                        <table id="table-chucdanh" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th class="fix-table-edit">Tên chức danh</th>
                                    <th class="fix-table-edit">Mô tả chức danh</th>
                                    <th class="fix-table">Mã chức danh</th>
                                    <th>Edit</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (dtchucdanh.Rows.Count == 0)
                                   { %>
                                <tr>
                                    <td colspan="2" class="text-center">No item</td>
                                </tr>
                                <% }
                                   else
                                   { %>
                                <% for (int i = 0; i < dtchucdanh.Rows.Count; i++)
                                   { %>
                                <%
                                       string cd_id = dtchucdanh.Rows[i][0].ToString();
                                       string cd_ten = dtchucdanh.Rows[i][1].ToString();
                                       string cd_mota = dtchucdanh.Rows[i][2].ToString();
                                       string cd_ma = dtchucdanh.Rows[i][3].ToString();
                                     
                                %>
                                <tr>
                                    <td><%= cd_id %></td>
                                    <td><%=  cd_ten%></td>
                                    <td><%=  cd_mota%></td>
                                    <td><%=  cd_ma%></td>
                                    <td>
                                        <a class="btn btn-primary btn-xs" type="button" data-target="#Editchucdanh" data-toggle="modal" onclick="editdata('<%=cd_id %>','<%=cd_ten%>','<%=cd_mota%>','<%=cd_ma%>')">Chỉnh sửa</a>
                                        <a class="btn btn-danger btn-xs" type="button" onclick="deletedata('<%=cd_id %>')">Xóa</a>

                                    </td>
                                </tr>
                                <% } %>
                                <% } %>
                            </tbody>
                        </table>

                        <!----------------------------------------------------------THÊM--------------------------------------------------------------->
                        <div id="listBSC" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Thêm chức danh</h4>
                                    </div>
                                 <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên chức danh:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txttenchucdanh" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả chức danh:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmotachucdanh" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Mã chức danh:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmachucdanh" />
                                        </div>
                                    </div>
                                </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-success" id="btnSave">Thêm chức danh</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        
                        <!----------------------------------------------------------SỬA--------------------------------------------------------------->
                        <div id="Editchucdanh" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Chỉnh sửa chức danh</h4>
                                    </div>
                                    <input type="hidden" id="txtidchucdanh_sua" />
                                    
                                    <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên chức danh:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txttenchucdanh_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả chức danh:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmotachucdanh_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Mã chức danh:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350 " id="txtmachucdanh_sua" />
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
                        
                        <!----------------------------------------------------------XÓA--------------------------------------------------------------->
                        
                        <%--<div id="Deletechucdanh" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <input type="hidden" id="txtidchucdanh_xoa" />
                                        <h4 class="modal-title">Xóa chức danh</h4>
                                    </div>
                                    <div class="modal-body list-BSC">
                                        <div class="input-group">
                                            <div class="input-group">
                                                <h3 style="font-family: Tahoma; font-size: 20px">
                                                    <label>Bạn có đồng ý xóa chức danh này không?</label>
                                                </h3>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-primary" id="btnXoa">Xóa</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <!----------------------------------------------------------END--------------------------------------------------------------->
                        
    </div>
    </div>
            </div>
        </div>
    </div>
    <script>
        function checkItemThem() {
            var cd_ten = $('#txttenchucdanh').val();
            var cd_ma = $('#txtmachucdanh').val();
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
            var cd_ten = $('#txttenchucdanh_sua').val();
            var cd_ma = $('#txtmachucdanh_sua').val();
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

        function editdata(cd_id, cd_ten, cd_mota, cd_ma) {
            $('#txtidchucdanh_sua').val(cd_id);
            $('#txttenchucdanh_sua').val(cd_ten);
            $('#txtmotachucdanh_sua').val(cd_mota);
            $('#txtmachucdanh_sua').val(cd_ma);
        }

        function deletedata(cd_id) {
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
                               cd_id_xoaAprove: cd_id,
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "Chucdanh.aspx/DeleteData",
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

            $("#table-chucdanh").DataTable({
                "searching": true,
                "info": true,
            });

            $("#btnSave").click(function () {
                var cd_ten = $("#txttenchucdanh").val();
                var cd_mota = $('#txtmotachucdanh').val();
                var cd_ma = $("#txtmachucdanh").val();
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    cd_tenAprove: cd_ten,
                    cd_motaAprove: cd_mota,
                    cd_maAprove: cd_ma
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "Chucdanh.aspx/SaveData",
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
                var cd_id_sua = $("#txtidchucdanh_sua").val();
                var cd_ten_sua = $("#txttenchucdanh_sua").val();
                var cd_mota_sua = $("#txtmotachucdanh_sua").val();
                var cd_ma_sua = $("#txtmachucdanh_sua").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    cd_id_suaAprove: cd_id_sua,
                    cd_ten_suaAprove: cd_ten_sua,
                    cd_mota_suaAprove: cd_mota_sua,
                    cd_ma_suaAprove: cd_ma_sua
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "Chucdanh.aspx/EditData",
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
