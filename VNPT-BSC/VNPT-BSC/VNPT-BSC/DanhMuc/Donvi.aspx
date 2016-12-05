<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Donvi.aspx.cs" Inherits="VNPT_BSC.DanhMuc.Donvi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
    <script src="../Bootstrap/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Bootstrap/sweetalert.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">DANH MỤC ĐƠN VỊ</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themdv">Thêm đơn vị</a>
                        <table id="table-donvi" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tên đơn vị</th>
                                    <th>Mô tả đơn vị</th>
                                    <th>Mã đơn vị</th>
                                    <th>Chỉnh sửa</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (dtdonvi.Rows.Count == 0)
                                   { %>
                                <tr>
                                    <td colspan="2" class="text-center">No item</td>
                                </tr>
                                <% }
                                   else
                                   { %>
                                <% for (int i = 0; i < dtdonvi.Rows.Count; i++)
                                   { %>
                                <%
                                       string dv_id = dtdonvi.Rows[i][0].ToString();
                                       string dv_ten = dtdonvi.Rows[i][1].ToString();
                                       string dv_mota = dtdonvi.Rows[i][2].ToString();
                                       string dv_ma = dtdonvi.Rows[i][3].ToString();
                                     
                                %>
                                <tr>
                                    <td><%= dv_id %></td>
                                    <td><%=  dv_ten%></td>
                                    <td><%=  dv_mota%></td>
                                    <td><%=  dv_ma%></td>
                                    <td>
                                        <a class="btn btn-primary btn-xs" type="button" data-target="#Editdv" data-toggle="modal" onclick="editdata('<%=dv_id %>','<%=dv_ten%>','<%=dv_mota%>','<%=dv_ma%>')">Chỉnh sửa</a>
                                        <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=dv_id %>')">Xóa</a>

                                    </td>
                                </tr>
                                <% } %>
                                <% } %>
                            </tbody>
                        </table>

                        <!----------------------------------------------------------THÊM--------------------------------------------------------------->
                        <div id="themdv" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Thêm đơn vị</h4>
                                    </div>
                                    <div class="modal-body list-BSC">
                                        <div class="input-group">
                                            <div class="input-group">
                                                <span class="input-group-addon" id="Span1" >Tên đơn vị:<label style="color:red">*</label>&nbsp;</span>
                                                <input type="text" class="form-control" style="width: 415px" aria-describedby="sizing-addon2" id="txttendv">
                                            </div>
                                            <br />
                                            <div class="input-group">
                                                <span class="input-group-addon " id="Span4">Mô tả đơn vị:</span>
                                                <input type="text" class="form-control" style="width: 412px" aria-describedby="sizing-addon2" id="txtmotadv">
                                            </div>
                                            <br />
                                            <div class="input-group">
                                                <span class="input-group-addon" id="sizing-addon2">Mã đơn vị:<label style="color:red">*</label>&nbsp;&nbsp; </span>
                                                <input type="text" class="form-control" style="width: 415px" aria-describedby="sizing-addon2"" id="txtmadv">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-success" id="btnSave">Thêm đơn vị</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        
                        <!----------------------------------------------------------SỬA--------------------------------------------------------------->
                        <div id="Editdv" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Chỉnh sửa đơn vị</h4>
                                    </div>
                                    <input type="hidden" id="txtiddv_sua" />
                                    <div class="modal-body list-BSC">
                                        <div class="input-group">
                                            <div class="input-group">
                                                <span class="input-group-addon" id="Span2">Tên đơn vị:<label style="color:red">*</label>&nbsp;</span>
                                                <input type="text" class="form-control" style="width: 415px" aria-describedby="sizing-addon2" id="txttendv_sua">
                                            </div>
                                            <br />
                                            <div class="input-group">
                                                <span class="input-group-addon " id="Span3">Mô tả đơn vị:</span>
                                                <input type="text" class="form-control" style="width: 412px" aria-describedby="sizing-addon2" id="txtmotadv_sua">
                                            </div>
                                            <br />
                                            <div class="input-group">
                                                <span class="input-group-addon" id="Span5" >Mã đơn vị:<label style="color:red">*</label>&nbsp;&nbsp;</span>
                                                <input type="text" class="form-control" style="width: 415px" aria-describedby="sizing-addon2"" id="txtmadv_sua">
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
            var dv_ten = $('#txttendv').val();
            var dv_ma = $('#txtmadv').val();
            if (dv_ten == "" || dv_ma == "") {
                swal({
                    title: "Nhập thiếu trường dữ liệu!!",
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
            var dv_ten = $('#txttendv_sua').val();
            var dv_ma = $('#txtmadv_sua').val();
            if (dv_ten == "" || dv_ma == "") {
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
        function editdata(dv_id, dv_ten, dv_mota, dv_ma) {
            $('#txtiddv_sua').val(dv_id);
            $('#txttendv_sua').val(dv_ten);
            $('#txtmotadv_sua').val(dv_mota);
            $('#txtmadv_sua').val(dv_ma);
        }

        function deletedata(dv_id) {
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
                               dv_id_xoaAprove: dv_id,
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "Donvi.aspx/DeleteData",
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
            $("#table-donvi").DataTable({
                "searching": true,
                "info": true,
            });

            $("#btnSave").click(function () {
                var dv_ten = $("#txttendv").val();
                var dv_mota = $('#txtmotadv').val();
                var dv_ma = $("#txtmadv").val();
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    dv_tenAprove: dv_ten,
                    dv_motaAprove: dv_mota,
                    dv_maAprove: dv_ma
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "Donvi.aspx/SaveData",
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
                var dv_id_sua = $("#txtiddv_sua").val();
                var dv_ten_sua = $("#txttendv_sua").val();
                var dv_mota_sua = $("#txtmotadv_sua").val();
                var dv_ma_sua = $("#txtmadv_sua").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    dv_id_suaAprove: dv_id_sua,
                    dv_ten_suaAprove: dv_ten_sua,
                    dv_mota_suaAprove: dv_mota_sua,
                    dv_ma_suaAprove: dv_ma_sua
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "Donvi.aspx/EditData",
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
                var cd_id_xoa = $("#txtiddv_xoa").val();

            });

        });

    </script>
</asp:Content>
