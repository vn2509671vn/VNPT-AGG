<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="NghiemThuBSCNhanVien.aspx.cs" Inherits="VNPT_BSC.BSC.NghiemThuBSCNhanVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- Customize css -->
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>--%>

    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/function.js"></script>
    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css"/>
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>

    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
          <div class="panel-heading">
            <h3 class="panel-title">Nghiệm thu BSC</h3>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Lọc theo tháng/năm:</label>
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
              </div>
              <div class="col-md-12 col-xs-12" id="gridBSC">

              </div>
          </div>
        </div>
    </div>
<script type="text/javascript">
    var nhanviengiao = '<%=nhanviengiao %>';

    function xemChiTiet(thang, nam, nhanviengiao, nhanviennhan) {
        window.location.replace("ChiTietBSCNVNghiemThu.aspx?nhanviengiao=" + nhanviengiao + "&nhanviennhan=" + nhanviennhan + "&thang=" + thang + "&nam=" + nam);
    }

    function loadBSCByYear(month, year, nhanviengiao) {
        var requestData = {
            nhanviengiao: nhanviengiao,
            thang: month,
            nam: year
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "NghiemThuBSCNhanVien.aspx/loadBSCByYear",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;

                $("#gridBSC").html(gridBSC);
                $("#table-bsclist").DataTable({
                    "searching": true,
                    "info": true,
                    "pageLength": 50
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {

        // Load grid lần đầu
        loadBSCByYear($("#month").val(), $("#year").val(), nhanviengiao);

        // Load grid khi năm thay đổi
        $("#year").change(function () {
            var thang = $("#month").val();
            var nam = $(this).val();
            loadBSCByYear(thang, nam, nhanviengiao);
        });

        // Load grid khi tháng thay đổi
        $("#month").change(function () {
            var nam = $("#year").val();
            var thang = $(this).val();
            loadBSCByYear(thang, nam, nhanviengiao);
        });
    });
</script>
</asp:Content>
