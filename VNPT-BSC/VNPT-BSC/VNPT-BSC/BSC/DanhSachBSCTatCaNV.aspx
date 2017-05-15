<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="DanhSachBSCTatCaNV.aspx.cs" Inherits="VNPT_BSC.BSC.DanhSachBSCTatCaNV" %>
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
            <h3 class="panel-title">Danh Sách Nhân Viên Nhận BSC</h3>
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
                <div class="form-group">
                    <label class="control-label col-sm-6">Đơn vị:</label>
                    <div class="col-sm-6 form-inline">
                        <select class="form-control" id="donvi">
                            <% for (int i = 0; i < dtDonvi.Rows.Count; i++){ %>
                            <option value="<%=dtDonvi.Rows[i]["donvi_id"].ToString() %>"><%=dtDonvi.Rows[i]["donvi_ten"].ToString() %></option>
                            <% } %>
                        </select>
                    </div>
                </div>
              </div>
              <div class="col-md-12 col-xs-12" id="gridNV">

              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    function loadDanhSach(month, year, donvi) {
        var requestData = {
            thang: month,
            nam: year,
            donvi: donvi
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "DanhSachBSCTatCaNV.aspx/loadDanhSach",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;

                $("#gridNV").html(output);
                $("#table-nv").DataTable();
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {

        // Load grid lần đầu
        loadDanhSach($("#month").val(), $("#year").val(), $("#donvi").val());

        // Load grid khi năm thay đổi
        $("#year").change(function () {
            var thang = $("#month").val();
            var nam = $(this).val();
            var donvi = $("#donvi").val();
            loadDanhSach(thang, nam, donvi);
        });

        // Load grid khi tháng thay đổi
        $("#month").change(function () {
            var nam = $("#year").val();
            var thang = $(this).val();
            var donvi = $("#donvi").val();
            loadDanhSach(thang, nam, donvi);
        });

        // Load grid khi đơn vị thay đổi
        $("#donvi").change(function () {
            var nam = $("#year").val();
            var donvi = $(this).val();
            var thang = $("#month").val();
            loadDanhSach(thang, nam, donvi);
        });

        $(document).on('click', '.detail', function () {
            var id_nv = $(this).closest("tr").attr("data-id");
            var thang = $("#month").val();
            var nam = $("#year").val();
            var url = "ChiTietBSCNV.aspx?nhanviennhan=" + id_nv + "&thang=" + thang + "&nam=" + nam;
            window.open(url, '_blank', 'toolbar=0,location=0,menubar=0,height=600,width=800,scrollbars=yes,status=yes');
        });
    });
</script>
</asp:Content>
