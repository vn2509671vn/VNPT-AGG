<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietBSCNV.aspx.cs" Inherits="VNPT_BSC.BSC.ChiTietBSCNV1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chi tiết bsc của nhân viên</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- Bootstrap -->
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />

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
</head>
<body>
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
          <div class="panel-heading">
            Chi tiết BSC <span><%=thang %>/<%=nam %></span>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <%--<label class="control-label col-sm-6">Ngày áp dụng:</label>
                    <div class="col-sm-6 form-inline padding-top-7">
                        <span><strong id="ngayapdung"></strong></span>
                    </div>--%>
                    <h4 class="text-center">BẢNG GIAO VÀ KẾT QUẢ THỰC HIỆN BSC/KPI THÁNG <span class="red-color"><strong id="ngayapdung"> <%=thang + "/" + nam %></strong></span></h4>
                    <h4 class="text-center">ĐƠN VỊ: <span class="red-color"><strong><%=dtInfo.Rows[0]["donvi_ten"] %></strong></span></h4>
                </div>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> <span class="red-color"><%=dtInfo.Rows[0]["nvn"] %></span>
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
                                            <th class="text-center">ĐVT</th>
                                            <th class="text-center">Chỉ tiêu</th>
                                            <th class="text-center">Thẩm định</th>
                                            <th class="text-center">Tỷ lệ thực hiện(%)</th>
                                            <th class="text-center">Hệ số quy đổi</th>
                                            <th class="text-center">Lý do thẩm định</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <% decimal tongdiem = 0; %>
                                        <% for(int nIndex = 0; nIndex < dtChiTiet.Rows.Count; nIndex++){
                                               if (dtChiTiet.Rows[nIndex]["diem_kpi"].ToString() != "") {
                                                   tongdiem += Convert.ToDecimal(dtChiTiet.Rows[nIndex]["diem_kpi"].ToString());
                                               }
                                        %>
                                        <tr>
                                            <td><strong><%= dtChiTiet.Rows[nIndex]["kpi_ten"] %></strong></td>
                                            <td><%= dtChiTiet.Rows[nIndex]["ten_nhom"] %></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["trongso"] %></strong></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["dvt_ten"] %></strong></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["kehoach"] %></strong></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["thamdinh"] %></strong></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["tlth"] %></strong></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["diem_kpi"] %></strong></td>
                                            <td class="text-center"><strong><%= dtChiTiet.Rows[nIndex]["ghichu_thamdinh"] %></strong></td>
                                        </tr>
                                        <% } %>
                                        <tr>
                                            <td class="text-center"><strong> Tổng: </strong></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td class="text-center"><strong> <%=tongdiem %></strong></td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                        <div class="panel-footer">
                            <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <label class="text-center col-sm-12">KÝ XÁC NHẬN CỦA CÁ NHÂN NHẬN VIỆC</label>
                                    <div class="col-sm-12 form-inline padding-top-7 text-center">
                                        <h5><span class="red-color"><%= dtInfo.Rows[0]["trangthainhan"] %></span></h5>
                                        <h5><strong><%=dtInfo.Rows[0]["nvn"] %></strong></h5>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <label class="text-center col-sm-12">LÃNH ĐẠO ĐƠN VỊ KÝ GIAO VIỆC</label>
                                    <div class="col-sm-12 form-inline padding-top-7 text-center">
                                        <h5><span class="red-color"><%= dtInfo.Rows[0]["trangthaigiao"] %></span></h5>
                                        <h5><strong><%=dtInfo.Rows[0]["nvg"] %></strong></h5>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <label class="text-center col-sm-12">KÝ XÁC NHẬN KẾT QUẢ ĐẠT ĐƯỢC CỦA CÁ NHÂN</label>
                                    <div class="col-sm-12 form-inline padding-top-7 text-center">
                                        <h5><span class="red-color"><%= dtInfo.Rows[0]["trangthaidongy_kqtd"] %></span></h5>
                                        <h5><strong><%=dtInfo.Rows[0]["nvn"] %></strong></h5>
                                    </div>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <label class="text-center col-sm-12">LÃNH ĐẠO ĐƠN VỊ KÝ XÁC NHẬN KẾT QUẢ</label>
                                    <div class="col-sm-12 form-inline padding-top-7 text-center">
                                        <h5><span class="red-color"><%= dtInfo.Rows[0]["trangthaiketthuc"] %></span></h5>
                                        <h5><strong><%=dtInfo.Rows[0]["nvg"] %></strong></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
              </div>
          </div>
        </div>
    </div>
</body>
<script type="text/javascript">
        $(document).ready(function () {
            $("#table-chitiet").DataTable({
                "bLengthChange": false,
                "bPaginate": false,
                "bSort": false,
                "dom": 'Bfrtip',
                "buttons": [
                    {
                        extend: 'excelHtml5',
                        title: 'Chi tiết BSC <%=dtInfo.Rows[0]["nvn"] %>'
                },
                        {
                            extend: 'pdfHtml5',
                            title: 'Chi tiết BSC <%=dtInfo.Rows[0]["nvn"] %>',
                            orientation: 'landscape',
                            pageSize: 'LEGAL'
                        }
            ]
        });
    });
</script>
</html>
