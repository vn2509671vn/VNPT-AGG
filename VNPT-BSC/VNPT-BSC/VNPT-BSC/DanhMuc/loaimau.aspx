<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="loaimau.aspx.cs" Inherits="VNPT_BSC.DanhMuc.loaimau" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../Bootstrap/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Bootstrap/sweetalert.css"/>
    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />
    <%--<link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" />--%>
    <script src="../Bootstrap/jquery.js"></script>
   <%-- <script src="../Bootstrap/bootstrap.js"></script>--%>
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
                <h3 class="panel-title">DANH MỤC LOẠI MẪU BSC</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themdvt">Thêm loại mẫu</a>
                        <table id="table-dvt" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tên mẫu BSC</th>
                                    <th>Ghi chú</th>
                                    <th class="fix-table-edit-edit">Chỉnh sửa</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (dtdvt.Rows.Count == 0)
                                   { %>
                                <tr>
                                    <td colspan="6" class="text-center">No item</td>
                                </tr>
                                <% }
                                   else
                                   { %>
                                <% for (int i = 0; i < dtdvt.Rows.Count; i++)
                                   { %>
                                <%
                                       string dvt_id = dtdvt.Rows[i]["loai_id"].ToString();
                                       string dvt_ten = dtdvt.Rows[i]["loai_ten"].ToString();
                                       string dvt_mota = dtdvt.Rows[i]["loai_mota"].ToString();
                                

                                %>
                                <tr>
                                    <td><%= dvt_id %></td>
                                    <td><%= dvt_ten%></td>
                                    <td><%= dvt_mota%></td>

                                    <td>
                                        <a class="btn btn-primary btn-xs" type="button" data-target="#Editdonvitinh" data-toggle="modal" onclick="editdata('<%=dvt_id %>','<%=dvt_ten%>','<%=dvt_mota%>')">Chỉnh sửa</a>
                                        <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=dvt_id %>')">Xóa</a>

                                    </td>
                                </tr>
                                <% } %>
                                <% } %>
                            </tbody>
                        </table>
                        <div id="themdvt" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title" style="text-align: center">Thêm loại mẫu</h4>
                                    </div>
                                    <div class="modal-body list-BSC form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Tên mẫu BSC:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txtten" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Ghi chú:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txtghichu" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-success" id="btnSave">Thêm loại mẫu</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <!----------------------------------------------------------SỬA--------------------------------------------------------------->
                        <div id="Editdonvitinh" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content col-md-12">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Chỉnh sửa loại mẫu</h4>
                                    </div>
                                    <input type="hidden" id="txtiddvt" />
                                    <div class="modal-body list-BSC form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Tên mẫu BSC:</label>
                                            <div class="col-sm-8">
                                                <input type="text" class="form-control fix-width-350" id="txtten_sua" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Ghi chú:</label>
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
            var dvt_ten = $('#txtten').val();
       
            if (dvt_ten == "" ) {
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
            var dvt_ten = $('#txtten_sua').val();
      
            if (dvt_ten == "" ) {
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

        function editdata(dvt_id, dvt_ten, dvt_mota) {
            $('#txtiddvt').val(dvt_id);
            $('#txtten_sua').val(dvt_ten);
            $('#txtmota_sua').val(dvt_mota);
        }
        function deletedata(dvt_id) {
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
                               dvt_idAprove: dvt_id,
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "loaimau.aspx/DeleteData",
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
                var dvt_ten = $("#txtten").val();
                var dvt_mota = $('#txtghichu').val();
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    dvt_tenAprove: dvt_ten,
                    dvt_motaAprove: dvt_mota,
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "loaimau.aspx/SaveData",
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
                var dvt_id_sua = $("#txtiddvt").val();
                var dvt_ten_sua = $("#txtten_sua").val();
                var dvt_mota_sua = $("#txtmota_sua").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    dvt_id_suaAprove: dvt_id_sua,
                    dvt_ten_suaAprove: dvt_ten_sua,
                    dvt_mota_suaAprove: dvt_mota_sua,
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "loaimau.aspx/EditData",
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



            $("#table-dvt").DataTable({
                "searching": true,
                "info": true,
            });
        });
    </script>

</asp:Content>
