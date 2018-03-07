<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="Tao_DonHang.aspx.cs" Inherits="VNPT_BSC.Donhang.Tao_DonHang" %>
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
    <script type="text/javascript" src="https://cdn.datatables.net/fixedcolumns/3.2.2/js/dataTables.fixedColumns.min.js"></script>
    
    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-xs-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">TẠO ĐƠN HÀNG</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12 form-horizontal">
                        <div class="form-group ">
                            <label class="control-label col-md-4">Tên khách hàng:</label>
                            <div class="col-md-5">
                                <input class="form-control" type="text" name="hoten" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-4">Số điện thoại:</label>
                            <div class="col-md-5">
                                <input class="form-control" type="text" name="sdt"/>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Địa chỉ:</label>
                            <div class="col-sm-5">
                                <textarea class="form-control" name="diachi" rows="2"></textarea>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Ghi chú:</label>
                            <div class="col-sm-5">
                                <textarea class="form-control" name="ghichu" rows="2"></textarea>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Nơi tiếp nhận:</label>
                            <div class="col-sm-8 form-inline">
                                <select class="form-control" name="pbh_tiepnhan">
                                    <% for(int nDonvi = 0; nDonvi < dtDonvi.Rows.Count; nDonvi++){ %>
                                    <option value="<%=dtDonvi.Rows[nDonvi]["id"].ToString().Trim() %>"><%=dtDonvi.Rows[nDonvi]["ten_donvi"].ToString().Trim() %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 text-center">
                        <a class="btn btn-success" id="luuChung">Giao xuống PBH</a>
                    </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    var nguoitao = '<%=nguoigiao.nhanvien_id %>';
    $(document).ready(function () {
        $("#luuChung").click(function () {
            var hoten = $("input[name=hoten]").val();
            var sdt = $("input[name=sdt]").val();
            var diachi = $("textarea[name=diachi]").val();
            var ghichu = $("textarea[name=ghichu]").val();
            var pbh_tiepnhan = $("select[name=pbh_tiepnhan]").val();

            
            if (hoten == "") {
                alert("Vui lòng nhập tên khách hàng!");
                return false;
            }
            else if (sdt == "") {
                alert("Vui lòng nhập số điện thoại!");
                return false;
            }
            else if (diachi == "") {
                alert("Vui lòng nhập địa chỉ!");
                return false;
            }

            var requestData = {
                hoten: hoten,
                sdt: sdt,
                diachi: diachi,
                ghichu: ghichu,
                pbh_tiepnhan: pbh_tiepnhan,
                nguoitao: nguoitao
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "Tao_DonHang.aspx/luuThongTinChung",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var data = result.d;
                    if (data.status == "ok") {
                        swal({
                            title: data.message,
                            text: "",
                            type: "success"
                        },
                        function () {
                            window.location.reload();
                        });
                    }
                    else {
                        swal("Error!!!", data.message, "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
