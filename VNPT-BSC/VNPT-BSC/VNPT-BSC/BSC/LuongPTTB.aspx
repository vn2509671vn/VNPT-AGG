<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="LuongPTTB.aspx.cs" Inherits="VNPT_BSC.BSC.LuongPTTB_Tang" %>
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
            <h3 class="panel-title">Lương PTTB</h3>
          </div>
          <div class="panel-body">
              <div id="divLoading" style="margin: 0px; padding: 0px; position: fixed; left: 0px; top: 0px; width: 100%; height: 100%; background-color: rgb(102, 102, 102); z-index: 30001; opacity: 0.8;">
                <p style="position: absolute; color: White; top: 50%; left: 30%;">
                    Vui lòng chờ trong giây lát do dữ liệu tải lên khá lớn ........
                    <img src="../Images/Loading/ajax-loader.gif" />
                </p>
              </div>
              <div class="col-md-12 col-xs-12 form-horizontal">
                <%--<h4 class="text-center red-color">Lưu ý: Lương PTTB của khối gián tiếp đang tạm tính, tất cả dữ liệu sẽ chốt sau ngày 15 hằng tháng</h4>--%>
                <div class="form-group">
                    <label class="control-label col-sm-4">Lọc theo tháng/năm:</label>
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
                    <label class="control-label col-sm-4">Đơn vị:</label>
                    <div class="col-sm-4">
                        <select class="form-control" id="donvi">
                            <option value="0" selected="selected">Tất cả</option>
                            <% for(int i = 0; i < dtDonVi.Rows.Count; i++){ %>
                            <option value="<%=dtDonVi.Rows[i]["donvi_id"].ToString().Trim() %>"><%=dtDonVi.Rows[i]["ten_donvi"].ToString().Trim() %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Loại tiền lương:</label>
                    <div class="col-sm-4">
                        <select class="form-control" id="loai">
                            <option value="3" selected="selected">Tổng hợp</option>
                            <option value="1">Tiền lương PTTB</option>
                            <option value="2">Trừ lương PTTB</option>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Loại nhân sự:</label>
                    <div class="col-sm-4">
                        <select class="form-control" id="nhom_nv">
                            <option value="1" selected="selected">Tất cả</option>
                            <option value="2">[Khối trực tiếp] Nhân viên</option>
                            <option value="3">[Khối trực tiếp] Lãnh đạo</option>
                            <%--<option value="4">[Khối gián tiếp] Nhân viên</option>
                            <option value="5">[Khối gián tiếp] Lãnh đạo</option>--%>
                        </select>
                    </div>
                </div>
              </div>
              <div class="col-md-12 col-xs-12" id="gridBSC">

              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">

    function loadTB() {
        var thang = $("#month").val();
        var nam = $("#year").val();
        var donvi = $("#donvi").val();
        var loai = $("#loai").val();
        var nhom_nv = $("#nhom_nv").val();
        
        var requestData = {
            donvi: donvi,
            thang: thang,
            nam: nam,
            loai: loai,
            nhom_nv: nhom_nv
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "LuongPTTB.aspx/loadTB",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;

                $("#gridBSC").html(output);
                $("#table-kpi").DataTable({
                    "pageLength": 1000,
                    "dom": 'Bfrtip',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                            title: 'Lương phát triển TB ' + thang + "-" + nam,
                            exportOptions: {
                                format: {
                                    body: function (data, columnIndex) {
                                        if (columnIndex === 4 || columnIndex === 5 || columnIndex === 6) {
                                            return data.replace(/[,]/g, '');
                                        }
                                        else {
                                            return data;
                                        }
                                        //return columnIndex === 6 ?
                                        //    data.replace(/[,]/g, '') :
                                        //    data;
                                    }
                                }
                            }
                        },
                        {
                            extend: 'pdfHtml5',
                            title: 'Lương phát triển TB ' + thang + "-" + nam,
                            orientation: 'landscape',
                            pageSize: 'LEGAL'
                        }
                    ]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
        var buttonCommon = {
            exportOptions: {
                format: {
                    body: function (data, row, column, node) {
                        // Strip $ from salary column to make it numeric
                        return column === 6 ?
                            data.replace(/,/g, '') :
                            data;
                    }
                }
            }
        };

        // Load grid lần đầu
        loadTB();

        // Load grid khi năm thay đổi
        $("#year").change(function () {
            loadTB();
        });

        // Load grid khi tháng thay đổi
        $("#month").change(function () {
            loadTB();
        });

        // Load grid khi đơn vị thay đổi
        $("#donvi").change(function () {
            loadTB();
        });

        // Load grid khi Loại thay đổi
        $("#loai").change(function () {
            loadTB();
        });

        $("#nhom_nv").change(function () {
            loadTB();
        });

        $(document).ajaxStart(function () {
            //var left = $(".left_col").css("width");
            //$("#divLoading").css({
            //    "left": left + "px"
            //});
            $("#divLoading").show();
        }).ajaxStop(function () {
            $("#divLoading").hide();
        });
    });
</script>
</asp:Content>
