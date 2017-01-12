<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="NhanBSCDonVi.aspx.cs" Inherits="VNPT_BSC.BSC.NhanBSCDonVi" %>
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
            <h3 class="panel-title">NHẬN BSC</h3>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-md-6">Lọc theo năm:</label>
                    <div class="col-md-3">
                        <select class="form-control" id="year">
                            <% for(int i = 2016; i <= 2100; i++){ 
                                string selectOption = "";
                                int date =  Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                                if(i == date){
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
    function xemChiTiet(thang, nam, donvigiao, donvinhan) {
        window.location.replace("ChiTietBSCNhan.aspx?donvigiao=" + donvigiao + "&donvinhan=" + donvinhan + "&thang=" + thang + "&nam=" + nam);
    }

    function loadBSCByYear(year, donvinhan) {
        var requestData = {
            donvinhan: donvinhan,
            nam: year
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "NhanBSCDonVi.aspx/loadBSCByYear",
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
        var donvinhan = '<%=donvinhan %>';

        // Load grid lần đầu
        loadBSCByYear($("#year").val(), donvinhan);

        // Load grid khi năm thay đổi
        $("#year").change(function () {
            var nam = $(this).val();
            loadBSCByYear(nam, donvinhan);
        });
    });
</script>
</asp:Content>
