<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="KPO.aspx.cs" Inherits="VNPT_BSC.DanhMuc.KPO" %>

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
                <h3 class="panel-title">DANH MỤC KPO</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themKPO">Thêm KPO</a>
                        <table id="table-kpi" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tên KPO</th>
                                    <th>Mô tả KPO</th>
                                    <th>Ngày tạo KPO</th>
                                    <th class="fix-table-edit-edit">Người tạo KPO</th>
                                    <th class="fix-table-edit-edit">Chỉnh sửa</th>
                                </tr>
                            </thead>
                            <tbody>
                            <% if (dtkpo.Rows.Count == 0)
                                { %>
                            <tr>
                                <td colspan="6" class="text-center">No item</td>
                            </tr>
                            <% }
                                else
                                { %>
                            <% for (int i = 0; i < dtkpo.Rows.Count; i++)
                                { %>
                            <%
                                string kpo_id = dtkpo.Rows[i][0].ToString();
                                string kpo_ten = dtkpo.Rows[i][1].ToString();
                                string kpo_mota = dtkpo.Rows[i][2].ToString();
                                string kpo_ngaytao = dtkpo.Rows[i][3].ToString();
                                string kpo_nguoitao = dtkpo.Rows[i]["nhanvien_hoten"].ToString();

                            %>
                            <tr>
                                <td><%= kpo_id %></td>
                                <td><%= kpo_ten%></td>
                                <td><%= kpo_mota%></td>
                                <td><%= kpo_ngaytao %></td>
                                <td><%= kpo_nguoitao%></td>
                                <td>
                                    <a class="btn btn-primary btn-xs" type="button" data-target="#Editkpo" data-toggle="modal" onclick="editdata('<%=kpo_id %>','<%=kpo_ten%>','<%=kpo_mota%>','<%=kpo_ngaytao%>')">Chỉnh sửa</a>
                                    <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=kpo_id %>')">Xóa</a>

                                </td>
                            </tr>
                            <% } %>
                            <% } %>
                        </tbody>
                        </table>
                         <!-- ADD-->
                    <div id="themKPO" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title"  style="text-align:center">THÊM KPO</h4>
                                </div>
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên KPO:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350"  id="txtten" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả KPO:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350"  id="txtmota" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Ngày tạo KPO:</label>
                                        <div class="col-sm-8">
                                            <input type="date" class="form-control fix-width-350 fix-height-34"  id="txtngay" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnSave">Thêm KPO</a>
                                    <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                </div>
                            </div>
                        </div>
                    </div>
                        <!-- EDIT-->
                        <div id="Editkpo" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align:center">CHỈNH SỬA KPO</h4>
                                </div>
                                <input type="hidden" id="txtidkpo_sua" />
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" >Tên KPO:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtten_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả KPO:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmota_sua" />
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
    </div>

    <script>

        function checkItemThem() {
            var kpi_ten = $("#txtten").val();
            var kpi_mota = $('#txtmota').val();
            var kpi_ngay = $("#txtngay").val();
            if (kpi_ten == "" || kpi_mota == "" || kpi_ngay == "") {
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
            var kpo_ten_sua = $('#txtten_sua').val();
            var kpo_mota_sua = $('#txtmota_sua').val();
            if (kpo_ten_sua == "" || kpo_mota_sua == "") {
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


        function editdata(kpo_id, kpo_ten, kpo_mota) {
            $('#txtidkpo_sua').val(kpo_id);
            $('#txtten_sua').val(kpo_ten);
            $('#txtmota_sua').val(kpo_mota);
        }

        function deletedata(kpo_id) {
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
                               kpo_id_xoaAprove: kpo_id,
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "KPO.aspx/DeleteData",
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

            var now = new Date();
            var month = (now.getMonth() + 1);
            var day = now.getDate();
            if (month < 10)
                month = "0" + month;
            if (day < 10)
                day = "0" + day;
            var today = now.getFullYear() + '-' + month + '-' + day;
            $('#txtngay').val(today);

            $("#btnSave").click(function () {
                var kpo_ten = $("#txtten").val();
                var kpo_mota = $('#txtmota').val();
                var kpo_ngay = $("#txtngay").val();
     
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    kpo_tenAprove: kpo_ten,
                    kpo_motaAprove: kpo_mota,
                    kpo_ngayAprove: kpo_ngay,

                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "KPO.aspx/SaveData",
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
                var kpo_id_sua = $("#txtidkpo_sua").val();
                var kpo_ten_sua = $("#txtten_sua").val();
                var kpo_mota_sua = $("#txtmota_sua").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    kpo_id_suaAprove: kpo_id_sua,
                    kpo_ten_suaAprove: kpo_ten_sua,
                    kpo_mota_suaAprove: kpo_mota_sua,
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "KPO.aspx/EditData",
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


            $("#table-kpo").DataTable({
                "searching": true,
                "info": true,
            });
        });
    </script>
</asp:Content>
