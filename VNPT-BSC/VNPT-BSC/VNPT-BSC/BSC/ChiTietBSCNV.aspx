<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="ChiTietBSCNV.aspx.cs" Inherits="VNPT_BSC.BSC.ChiTietBSCNV" %>
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

    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
          <div class="panel-heading">
            Chi tiết BSC
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> <span class="red-color"><%=ten_nhanvien %></span>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class='table-responsive padding-top-10'>
                                <table id='table-chitiet' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>
                                    <thead>
                                        <tr>
                                            <th class="text-center">KPI</th>
                                            <th class="text-center">Nhóm</th>
                                            <th class="text-center">Tỷ trọng</th>
                                            <th class="text-center">Chỉ tiêu</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <% for(int nIndex = 0; nIndex < dtChiTiet.Rows.Count; nIndex++){ %>
                                        <tr>
                                            <td><strong><%= dtChiTiet.Rows[nIndex]["kpi_ten"] %></strong></td>
                                            <td><%= dtChiTiet.Rows[nIndex]["ten_nhom"] %></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["trongso"] %></strong></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["kehoach"] %></strong></td>
                                        </tr>
                                        <% } %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#table-chitiet").DataTable();
    });
</script>
</asp:Content>
