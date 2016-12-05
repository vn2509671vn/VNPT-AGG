<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="KPI.aspx.cs" Inherits="VNPT_BSC.DanhMuc.BSC" %>

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
                <h3 class="panel-title">DANH MỤC KPI</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themkpi">Thêm KPI</a>
                    </div>
                    <table id="table-kpi" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Tên KPI</th>
                                <th>Mô tả KPI</th>
                                <th>Ngày tạo KPI</th>
                                <th class="fix-table-edit-edit1">Người tạo KPI</th>
                                <th class="fix-table-edit-edit1">Thuộc nhóm KPO</th>
                                <th class="fix-table-edit-edit1">Chỉnh sửa</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% if (dtkpi.Rows.Count == 0)
                                { %>
                            <tr>
                                <td colspan="7" class="text-center">No item</td>
                            </tr>
                            <% }
                                else
                                { %>
                            <% for (int i = 0; i < dtkpi.Rows.Count; i++)
                                { %>
                            <%
                                string kpi_id = dtkpi.Rows[i][0].ToString();
                                string kpi_ten = dtkpi.Rows[i][1].ToString();
                                string kpi_mota = dtkpi.Rows[i][2].ToString();
                                string kpi_ngaytao = String.Format("{0:MM/dd/yyyy}",dtkpi.Rows[i][3].ToString());
                                string kpi_nguoitao = dtkpi.Rows[i]["nhanvien_hoten"].ToString();
                                string kpi_thuockpo = dtkpi.Rows[i]["kpo_ten"].ToString();
                                string kpo_id = dtkpi.Rows[i]["kpo_id"].ToString();
                                string nhanvien_id = dtkpi.Rows[i]["nhanvien_id"].ToString();

                            %>
                            <tr>
                                <td><%= kpi_id %></td>
                                <td><%= kpi_ten%></td>
                                <td><%= kpi_mota%></td>
                                <td><%= kpi_ngaytao %></td>
                                <td><%= kpi_nguoitao%></td>
                                <td><%= kpi_thuockpo%></td>
                                <td>
                                    <a class="btn btn-primary btn-xs" type="button" data-target="#Editkpi" data-toggle="modal" onclick="editdata('<%=kpi_id %>','<%=kpi_ten%>','<%=kpi_mota%>','<%=kpi_ngaytao%>','<%=kpo_id%>')">Chỉnh sửa</a>
                                    <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=kpi_id %>')">Xóa</a>

                                </td>
                            </tr>
                            <% } %>
                            <% } %>
                        </tbody>
                    </table>
                    <!-- EDIT-->
                    <div id="themkpi" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title"  style="text-align:center">THÊM KPI</h4>
                                </div>
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Tên KPI:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350"  id="txtten" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả KPI:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350"  id="txtmota" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4 ">Ngày tạo KPI:</label>
                                        <div class="col-sm-8">
                                            <input type="date" class="form-control fix-width-350 fix-height-34"  id="txtngay" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Thuộc nhóm KPO:</label>
                                        <div class="col-sm-8">
                                            <select class="form-control fix-day" id="kpo">
                                                <% for (int i = 0; i < dtkpi_kpo.Rows.Count; i++)
                                                    { %>
                                                <%
                                                    string kpo_id = dtkpi_kpo.Rows[i]["kpo_id"].ToString();
                                                    string kpo_ten = dtkpi_kpo.Rows[i]["kpo_ten"].ToString();
                                                %>
                                                <option value="<%= kpo_id%>"><%= kpo_ten%></option>
                                                <% } %>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnSave">Thêm KPI</a>
                                    <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--------------------------------------------------------EDIT---->
                    <div id="Editkpi" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align:center">CHỈNH SỬA KPI</h4>
                                </div>
                                <input type="hidden" id="txtidkpi_sua" />
                                <div class="modal-body list-BSC form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" >Tên KPI:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtten_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Mô tả KPI:</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control fix-width-350" id="txtmota_sua" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Thuộc nhóm KPO:</label>
                                        <div class="col-sm-8">
                                            <%--<input type="text" class="form-control fix-width-350 fix-height-34"  id="txtthuockpo_sua" />--%>
                                            <select class="form-control fix-day" id="kpo_edit">
                                                <% for (int i = 0; i < dtkpi_kpo.Rows.Count; i++)
                                                    { %>
                                                <%
                                                    string kpo_id = dtkpi_kpo.Rows[i]["kpo_id"].ToString();
                                                    string kpo_ten = dtkpi_kpo.Rows[i]["kpo_ten"].ToString();
                                                %>
                                                <option value="<%= kpo_id%>"><%= kpo_ten%></option>
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
        function checkItemThem() {
            var kpi_ten = $("#txtten").val();
            var kpi_mota = $('#txtmota').val();
            var kpi_ngay = $("#txtngay").val();
            if (kpi_ten == "" || kpi_mota == "" || kpi_ngay == "") {
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
            var kpi_ten_sua = $('#txtten_sua').val();
            var kpi_mota_sua = $('#txtmota_sua').val();
            if (kpi_ten_sua == "" || kpi_mota_sua == "") {
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

        function editdata(kpi_id, kpi_ten, kpi_mota, kpi_ngaytao,  kpi_thuockpo) {
            $('#txtidkpi_sua').val(kpi_id);
            $('#txtten_sua').val(kpi_ten);
            $('#txtmota_sua').val(kpi_mota);
            $('#txtngay_sua').val(kpi_ngaytao);
            $('#kpo_edit').val(kpi_thuockpo);
        }

        function deletedata(kpi_id) {
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
                               url: "KPI.aspx/DeleteData",
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
                var kpi_ten = $("#txtten").val();
                var kpi_mota = $('#txtmota').val();
                var kpi_ngay = $("#txtngay").val();
                var kpi_kpo = $("#kpo").val();
                var isCheck = checkItemThem();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    kpi_tenAprove: kpi_ten,
                    kpi_motaAprove: kpi_mota,
                    kpi_ngayAprove: kpi_ngay,
                    kpi_kpoAprove: kpi_kpo
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "KPI.aspx/SaveData",
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
                var kpi_id_sua = $("#txtidkpi_sua").val();
                var kpi_ten_sua = $("#txtten_sua").val();
                var kpi_mota_sua = $("#txtmota_sua").val();
                var kpi_kpo_sua = $("#kpo_edit").val();
                var isCheck = checkItemSua();
                if (!isCheck) {
                    return false;
                }
                var requestData = {
                    kpi_id_suaAprove: kpi_id_sua,
                    kpi_ten_suaAprove: kpi_ten_sua,
                    kpi_mota_suaAprove: kpi_mota_sua,
                    kpi_kpo_suaAprove: kpi_kpo_sua
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "KPI.aspx/EditData",
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

            $("#table-kpi").DataTable({
                "searching": true,
                "info": true,
            });
        });
    </script>
</asp:Content>
