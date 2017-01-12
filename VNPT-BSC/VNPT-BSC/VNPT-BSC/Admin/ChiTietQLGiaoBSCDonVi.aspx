<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="ChiTietQLGiaoBSCDonVi.aspx.cs" Inherits="VNPT_BSC.Admin.ChiTietQLGiaoBSCDonVi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>--%>

    <!-- Customize css -->
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />

    <%--<script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>

    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>--%>

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/function.js"></script>
    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css" />
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
            <h3 class="panel-title">Chi tiết BSC</h3>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Ngày áp dụng:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span><strong id="ngayapdung"></strong></span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Đơn vị giao:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span><strong id="donvigiao"></strong></span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Đơn vị nhận:</label>
                    <div class="col-sm-6 form-inline padding-top-7 ">
                        <span><strong id="donvinhan"></strong></span>
                    </div>
                </div>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Danh sách KPI
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body" id="gridBSC">
                            
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
                <div class="col-md-12 col-xs-12 text-center">
                    <a class="btn btn-success" id="saveData">Save</a>
                </div>
              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    var donvigiao = "<%= donvigiao %>";
    var donvinhan = "<%= donvinhan %>";
    var thang = "<%= thang %>";
    var nam = "<%= nam %>";

    function loadDataToPage(donvigiao, donvinhan, thang, nam, donvithamdinh) {

        var requestData = {
            donvigiao: donvigiao,
            donvinhan: donvinhan,
            thang: thang,
            nam: nam
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ChiTietQLGiaoBSCDonVi.aspx/loadBSCByCondition",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var output = result.d;
                var gridBSC = output.gridBSC;
                var donvigiao = output.donvigiao;
                var donvinhan = output.donvinhan;
                var thang = output.thang;
                var nam = output.nam;
                var ten_dvg = output.ten_dvg;
                var ten_dvn = output.ten_dvn;

                /*Fill data*/
                $("#gridBSC").html(gridBSC);    // Fill to table

                // Fill ngày áp dụng
                $("#ngayapdung").text(thang + "/" + nam);

                // Fill data
                $("#donvigiao").text(ten_dvg);
                $("#donvinhan").text(ten_dvn);

                $("#table-kpi").DataTable({
                    "searching": true,
                    "info": true,
                    "pageLength": 50
                });
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function onlyNumbers(e) {
        //if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
        return !(e > 31 && (e < 48 || e > 57));
    }

    $(document).ready(function () {
        loadDataToPage(donvigiao, donvinhan, thang, nam);

        $("#saveData").click(function () {
            var kpi_detail = [];
            $("#table-kpi > tbody > tr").each(function () {
                var kpi_id = $(this).attr("data-id");
                var thamdinh = $("#thamdinh_" + kpi_id).val();
                var thuchien = $("#thuchien_" + kpi_id).val();
                var kehoach = $("#kehoach_" + kpi_id).val();
                var trongso = $("#trongso_" + kpi_id).val();
                var donvitinh = $("#dvt_" + kpi_id).val();
                var donvithamdinh = $("#dvtd_" + kpi_id).val();
                if (thamdinh == "") {
                    thamdinh = 0;
                }
                if (thuchien == "") {
                    thuchien = 0;
                }
                if (kehoach == "") {
                    kehoach = 0;
                }
                if (trongso == "") {
                    trongso = 0;
                }

                kpi_detail.push({
                    kpi_id: kpi_id,
                    thamdinh: thamdinh,
                    thuchien: thuchien,
                    kehoach: kehoach,
                    trongso: trongso,
                    donvitinh: donvitinh,
                    donvithamdinh: donvithamdinh
                });
            });

            var requestData = {
                donvigiao: donvigiao,
                donvinhan: donvinhan,
                thang: thang,
                nam: nam,
                kpi_detail: kpi_detail
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "ChiTietQLGiaoBSCDonVi.aspx/saveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var isSuccess = result.d;
                    if (isSuccess) {
                        swal({
                            title: "Lưu thành công!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            loadDataToPage(donvigiao, donvinhan, thang, nam);
                        });
                    }
                    else {
                        swal("Error!!!", "Lưu không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });

</script>
</asp:Content>
