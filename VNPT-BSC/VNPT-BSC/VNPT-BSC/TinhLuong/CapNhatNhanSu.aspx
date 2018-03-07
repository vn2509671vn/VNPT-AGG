<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="CapNhatNhanSu.aspx.cs" Inherits="VNPT_BSC.TinhLuong.CapNhatNhanSu" %>
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
                <h3 class="panel-title">CẬP NHẬT THÔNG TIN NHÂN SỰ</h3>
            </div>
            <div class="panel-body">
                <ul class="nav nav-tabs">
                  <li class="active"><a data-toggle="tab" href="#thongtinchung">Thông tin chung</a></li>
                </ul>

                <div class="tab-content">
                  <div id="thongtinchung" class="tab-pane fade in active margin-top-5">
                    <div class="col-sm-6 form-horizontal">
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Họ và Tên:</label>
                            <div class="col-sm-8 form-inline">
                                <input class="form-control" type="text" name="hoten" value="<%= userInfo.Rows[0]["ten_nhanvien"] %>"/>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Mã NV:</label>
                            <div class="col-sm-8 form-inline">
                                <input class="form-control" type="text" name="manv" value="<%= userInfo.Rows[0]["ma_nhanvien"] %>"/>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">CMND:</label>
                            <div class="col-sm-8 form-inline">
                                <input class="form-control" type="text" name="cmnd" value="<%= userInfo.Rows[0]["cmnd"] %>"/>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Ngày ký hđ:</label>
                            <div class="col-sm-8 form-inline">
                                <input type="date" class"form-control" name="ngaykyhd" value="<%= Convert.ToDateTime(userInfo.Rows[0]["ngayvaonganh"]).ToString("yyyy-dd-MM") %>"/>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 text-center">
                        <a class="btn btn-success" id="luuChung">Lưu</a>
                    </div>
                  </div>
                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    var id_nhansu = '<%=id_nhansu%>';
    $(document).ready(function () {
        $("#luuChung").click(function () {
            var cmnd = $("input[name=cmnd]").val();
            var manv = $("input[name=manv]").val();
            var hoten = $("input[name=hoten]").val();
            var ngaykyhd = $("input[name=ngaykyhd]").val();

            if (hoten == "") {
                alert("Vui lòng nhập tên nhân viên!");
                return false;
            }
            else if (ngaykyhd == "") {
                alert("Vui lòng chọn ngày ký hợp đồng!");
                return false;
            }

            var requestData = {
                cmnd: cmnd,
                manv: manv,
                hoten: hoten,
                ngaykyhd: ngaykyhd,
                id_nhansu: id_nhansu
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "CapNhatNhanSu.aspx/luuThongTin",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var data = result.d;
                    if (data.status == "ok") {
                        alert(data.message);
                        window.location.reload();
                    }
                    else {
                        alert(data.message);
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
