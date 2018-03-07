﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="CTV_PTTB.aspx.cs" Inherits="VNPT_BSC.BSC.CTV_PTTB" %>
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
            <h3 class="panel-title">Cộng tác viên phát triển thuê bao</h3>
          </div>
          <div class="panel-body">
              <div id="divLoading" style="margin: 0px; padding: 0px; position: fixed; left: 0px; top: 0px; width: 100%; height: 100%; background-color: rgb(102, 102, 102); z-index: 30001; opacity: 0.8;">
                <p style="position: absolute; color: White; top: 50%; left: 30%;">
                    Vui lòng chờ trong giây lát do dữ liệu tải lên khá lớn ........
                    <img src="../Images/Loading/ajax-loader.gif" />
                </p>
              </div>
              
              <div class="col-md-12 col-xs-12 form-horizontal">
                <input type="hidden" id="socongvan" class="form-control" value="......" />
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
                    <label class="control-label col-sm-4">Nhóm CTV:</label>
                    <div class="col-sm-4">
                        <select class="form-control" id="nhomctv">
                            <option value="0" selected="selected">Tất cả</option>
                            <option value="2">Cộng tác viên</option>
                            <option value="3">Giao dịch viên</option>
                            <option value="7">Thu cước</option>
                            <option value="11">CTV KD</option>
                            <option value="12">CTV PTTB</option>
                            <option value="13">CTV Tay Trong</option>
                            <%--<option value="4">Điểm ủy quyền</option>--%>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4">Kiểu hiển thị:</label>
                    <div class="col-sm-4">
                        <select class="form-control" id="kieuhienthi">
                            <option value="1">Chi tiết</option>
                            <option value="2" selected="selected">Tổng hợp</option>
                        </select>
                    </div>
                </div>
                <h5 class="text-center red-color">Dữ liệu được cập nhật tới ngày <%=ngaycapnhat %></h5>
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
        var kieuhienthi = $("#kieuhienthi").val();
        var nhomctv = $("#nhomctv").val();
        var socongvan = $("#socongvan").val();

        var requestData = {
            donvi: donvi,
            thang: thang,
            nam: nam,
            kieuhienthi: kieuhienthi,
            nhomctv: nhomctv
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "CTV_PTTB.aspx/loadTB",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                $("#gridBSC").html(output);
                $("#table-kpi").DataTable({
                    "bPaginate": true,
                    "lengthMenu": [1500],
                    "dom": 'LBfrtip',
                    "buttons": [
                        {
                            text: 'Excel',
                            action: function (e, dt, node, config) {
                                if (kieuhienthi == 2) {
                                    ExportToExcel_CTV_PTTB_TongHop('table-kpi', $("#donvi option:selected").text(), thang, nam, socongvan);
                                }
                                else {
                                    ExportToExcel_CTV_PTTB_ChiTiet('table-kpi', $("#donvi option:selected").text(), thang, nam, socongvan);
                                }
                            }
                        }
                    ]
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
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
        $("#kieuhienthi").change(function () {
            loadTB();
        });

        $("#nhomctv").change(function () {
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
