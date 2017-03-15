<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="XuatMauBSCNV.aspx.cs" Inherits="VNPT_BSC.BSC.XuatMauBSCNV" %>
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
            <h3 class="panel-title">Xuất mẫu BSC đơn vị</h3>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-md-6">Lọc theo tháng/năm:</label>
                    <div class="col-sm-6 form-inline">
                        <select class="form-control" id="month">
                            <% for(int i = 1; i <= 12; i++){ 
                                string selectOption = "";
                                int month =  Convert.ToInt32(DateTime.Now.ToString("MM"));
                                if(i == month){
                                    selectOption = "selected";
                                }
                            %>
                            <option value="<%=i %>" <%=selectOption %>><%=i %></option>
                            <% } %>
                        </select>
                        <select class="form-control" id="year">
                            <% for(int i = 2016; i <= 2100; i++){ 
                                string selectOption = "";
                                int year =  Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                                if(i == year){
                                    selectOption = "selected";
                                }
                            %>
                            <option value="<%=i %>" <%=selectOption %>><%=i %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Loại mẫu:</label>
                    <div class="col-sm-6 form-inline">
                        <select class='form-control' id='loaiMauBSC'>
                            <% for (int nMauBSC = 0; nMauBSC < dtMauBSC.Rows.Count; nMauBSC++){ %>
                            <option value="<% =dtMauBSC.Rows[nMauBSC]["loai_id"].ToString() %>"><% =dtMauBSC.Rows[nMauBSC]["loai_ten"].ToString() %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="form-group" id="chonNhanVien">
                    <label class="control-label col-sm-6">Chọn nhân viên:</label>
                    <div class="col-sm-6 form-inline">
                        <a class="btn btn-primary btn-xs" data-toggle="modal" data-target="#listNV" id="btnChonNV">Chọn</a>
                        <!-- Modal for BSC list -->
                        <div id="listNV" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Danh sách mẫu BSC</h4>
                                </div>
                                <div class="modal-body">
                                <% for (int nNhanVien = 0; nNhanVien < dtNhanVien.Rows.Count; nNhanVien++){ %>
                                    <%
                                        string nhanvien_taikhoan = dtNhanVien.Rows[nNhanVien]["nhanvien_taikhoan"].ToString();
                                        string nhanvien_hoten = dtNhanVien.Rows[nNhanVien]["nhanvien_hoten"].ToString();
                                        string nhanvien_chucdanh = dtNhanVien.Rows[nNhanVien]["chucdanh_ten"].ToString();
                                        string noidung = nhanvien_hoten + " - " + nhanvien_chucdanh;
                                    %>
                                    <div class="checkbox">
                                        <label><input type="checkbox" name="checkNV" value="<%=nhanvien_taikhoan %>" /> <%= noidung%></label>
                                    </div>
                                    <div class="clearfix"></div>
                                <% } %>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-success" data-dismiss="modal" id="chonDanhSachNV">Chọn</button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                                </div>
                            </div>

                            </div>
                        </div>
                    </div>
                </div>
              </div>
              <div class="col-md-12 col-xs-12" id="gridBSC">

              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    function loadBSCByTime(month, year, loaiMauBSC, nguoitao) {
        var tenMauBSC = $("#loaiMauBSC option:selected").text();
        var requestData = {
            thang: month,
            nam: year,
            loaiMauBSC: loaiMauBSC,
            nguoitao: nguoitao
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "XuatMauBSCNV.aspx/loadBSCByTime",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;

                $("#gridBSC").html(gridBSC);
                $("#chonNhanVien").show();
                $("#table-bsclist").DataTable({
                    "order": [],
                    "pageLength": 50,
                    "dom": 'Bfrtip',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                            text: 'Xuất file',
                            title: 'Giao BSC/KPI ' + tenMauBSC + " " + month + "-" + year
                        }
                    ],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
        var nguoitao = <%=nguoitao_id%>;
        // Load grid lần đầu
        loadBSCByTime($("#month").val(), $("#year").val(), $("#loaiMauBSC").val(), nguoitao);
        $("#chonNhanVien").hide();

        // Load grid khi năm thay đổi
        $("#year").change(function () {
            var thang = $("#month").val();
            var nam = $(this).val();
            var loaiMauBSC = $("#loaiMauBSC").val();
            loadBSCByTime(thang, nam, loaiMauBSC, nguoitao);
        });

        // Load grid khi tháng thay đổi
        $("#month").change(function () {
            var nam = $("#year").val();
            var thang = $(this).val();
            var loaiMauBSC = $("#loaiMauBSC").val();
            loadBSCByTime(thang, nam, loaiMauBSC, nguoitao);
        });

        // Load grid khi loại mẫu thay đổi
        $("#loaiMauBSC").change(function () {
            var nam = $("#year").val();
            var thang = $("#month").val();
            var loaiMauBSC = $(this).val();
            loadBSCByTime(thang, nam, loaiMauBSC, nguoitao);
        });

        $("#chonDanhSachNV").click(function(){
            var index = 0;
            var tenMauBSC = $("#loaiMauBSC option:selected").text();
            var nam = $("#year").val();
            var thang = $("#month").val();
            var totalColumns = $('#table-bsclist thead tr th').length;
            if(totalColumns > 5){
                var numCol = totalColumns - 5;
                for(var nIndexCol = 0; nIndexCol < numCol; nIndexCol++){
                    $('#table-bsclist thead').find("tr th:nth-child(6)").each(function(){$(this).remove()});
                    $('#table-bsclist tbody').find("tr td:nth-child(6)").each(function(){$(this).remove()});
                }
            }

            $("input[name='checkNV']:checked").each(function(){
                var taikhoan_nv = $(this).val();
                $('#table-bsclist tr').each(function(){
                    if(index == 0){
                        $(this).append('<th class="no-sort">'+taikhoan_nv+'</th>');
                    }
                    else {
                        $(this).append('<td></td>');
                    }
                    index++;
                });
                index = 0;
            });

            $("#table-bsclist").DataTable({
                "destroy": true,
                "order": [],
                "pageLength": 50,
                "dom": 'Bfrtip',
                "buttons": [
                    {
                        extend: 'excelHtml5',
                        text: 'Xuất file',
                        title: 'Giao BSC/KPI ' + tenMauBSC + " " + thang + "-" + nam
                    }
                ],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });
        });
    });
</script>
</asp:Content>
