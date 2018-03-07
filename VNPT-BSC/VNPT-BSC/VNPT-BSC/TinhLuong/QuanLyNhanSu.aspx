<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="QuanLyNhanSu.aspx.cs" Inherits="VNPT_BSC.TinhLuong.QuanLyNhanSu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/function.js"></script>
    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css"/>
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>
    <!-- Add for export data of datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.4/css/buttons.dataTables.min.css"/>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.flash.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.print.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/fixedcolumns/3.2.2/js/dataTables.fixedColumns.min.js"></script>
    
    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Danh mục nhân sự</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12 table-responsive">
                    <a class="btn btn-success btn-xl fix-label-margin-top" href="ThemNhanSu.aspx">Thêm nhân viên</a>
                    <table id="table-nhansu" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th class="text-center">MNV</th>
                                <th class="text-center">Họ và Tên</th>
                                <th class="text-center min-width-200">Đơn vị</th>
                                <th class="text-center min-width-200">Chức danh</th>
                                <th class="text-center min-width-200">Nhóm</th>
                                <th class="text-center min-width-72">Bậc lương</th>
                                <th class="text-center">Tác vụ</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% for (int i = 0; i < dtNhanVien.Rows.Count; i++){
                                   string id = dtNhanVien.Rows[i]["id"].ToString();
                                   string ma_nhanvien = dtNhanVien.Rows[i]["ma_nhanvien"].ToString();
                                   string hoten = dtNhanVien.Rows[i]["ten_nhanvien"].ToString();
                                   string donvi = dtNhanVien.Rows[i]["ten_donvi"].ToString();
                                   string nhomdonvi = dtNhanVien.Rows[i]["ten_nhom"].ToString();
                                   string chucdanh = dtNhanVien.Rows[i]["ten_chucdanh"].ToString();
                                   string bacluong = dtNhanVien.Rows[i]["ten_bacluong"].ToString();
                            %>
                            <tr data-id="<%=id %>">
                                <td class="text-center"><strong><%=ma_nhanvien %></strong></td>
                                <td class="min-width-150"><strong><%=hoten %></strong></td>
                                <!-- Đơn vị -->
                                <td class="text-center"><%= donvi %></td>
                                <!-- Chức danh -->
                                <td class="text-center"><%= chucdanh %></td>
                                <!-- Nhóm Đơn vị -->
                                <td class="text-center"><%= nhomdonvi %></td>
                                <!-- Bậc lương -->
                                <td class="text-center">Bậc <%= bacluong %></td>
                                <!-- Tác vụ -->
                                <td class="text-center"><a href="#" class="btn btn-xs btn-primary">Chi tiết</a></td>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    $("#table-nhansu").DataTable({
        bSort: false
    });
</script>
</asp:Content>
