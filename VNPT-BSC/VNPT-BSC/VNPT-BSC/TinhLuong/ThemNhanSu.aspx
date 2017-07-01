<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="ThemNhanSu.aspx.cs" Inherits="VNPT_BSC.TinhLuong.ThemNhanSu" %>
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
                <h3 class="panel-title">THÊM NHÂN SỰ MỚI</h3>
            </div>
            <div class="panel-body">
                <ul class="nav nav-tabs">
                  <li class="active"><a data-toggle="tab" href="#thongtinchung">Thông tin chung</a></li>
                </ul>

                <div class="tab-content">
                  <div id="thongtinchung" class="tab-pane fade in active margin-top-5">
                    <div class="col-sm-6 form-horizontal">
                        <div class="form-group ">
                            <label class="control-label col-sm-4">CMND:</label>
                            <div class="col-sm-8 form-inline">
                                <input class="form-control" type="text" name="cmnd" />
                            </div>
                        </div>
                        <div class="form-group hide">
                            <label class="control-label col-sm-4">ID:</label>
                            <div class="col-sm-8 form-inline">
                                <input class="form-control" type="text" name="id" readonly="true"/>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Mã NV:</label>
                            <div class="col-sm-8 form-inline">
                                <input class="form-control" type="text" name="manv"/>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Họ và Tên:</label>
                            <div class="col-sm-8 form-inline">
                                <input class="form-control" type="text" name="hoten" />
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">TK Ngân hàng:</label>
                            <div class="col-sm-8 form-inline">
                                <input class="form-control" type="text" name="bank" />
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="col-sm-8 col-sm-offset-4 form-inline checkbox">
                                <label><input class="form-control" type="checkbox" name="chinhthuc" value=""/>NV Chính Thức</label>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="col-sm-8 col-sm-offset-4 form-inline checkbox">
                                <label><input class="form-control" type="checkbox" name="dangvien" value=""/>Đảng viên</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 form-horizontal">
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Đơn vị:</label>
                            <div class="col-sm-8 form-inline">
                                <select class="form-control" name="donvi">
                                    <% for(int nDonvi = 0; nDonvi < dtDonvi.Rows.Count; nDonvi++){ %>
                                    <option value="<%=dtDonvi.Rows[nDonvi]["id"].ToString().Trim() %>"><%=dtDonvi.Rows[nDonvi]["ten_donvi"].ToString().Trim() %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Chức danh:</label>
                            <div class="col-sm-8 form-inline">
                                <select class="form-control" name="chucdanh">
                                    <% for (int nChucdanh = 0; nChucdanh < dtChucdanh.Rows.Count; nChucdanh++){ %>
                                    <option value="<%=dtChucdanh.Rows[nChucdanh]["id"].ToString().Trim() %>"><%=dtChucdanh.Rows[nChucdanh]["ten_chucdanh"].ToString().Trim() %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Bậc lương:</label>
                            <div class="col-sm-8 form-inline">
                                <select class="form-control" name="bacluong">
                                    
                                </select>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Nhóm đơn vị:</label>
                            <div class="col-sm-8 form-inline">
                                <select class="form-control" name="nhomdonvi">
                                    <% for (int nNhom = 0; nNhom < dtNhomDonvi.Rows.Count; nNhom++){ %>
                                    <option value="<%=dtNhomDonvi.Rows[nNhom]["id"].ToString().Trim() %>"><%=dtNhomDonvi.Rows[nNhom]["ten_nhom"].ToString().Trim() %></option>
                                    <% } %>
                                </select>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label class="control-label col-sm-4">Ngày ký hđ:</label>
                            <div class="col-sm-8 form-inline">
                                <input type="date" class"form-control" name="ngaykyhd" />
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
    function searchNVByCMND() {
        var cmnd = $("input[name=cmnd]").val();
        var requestData = {
            cmnd: cmnd
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ThemNhanSu.aspx/loadNhanSu",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var data = result.d;
                $("input[name=id]").val(data.id);
                $("input[name=manv]").val(data.manv);
                $("input[name=hoten]").val(data.hoten);
                $("select[name=donvi]").val(data.donvi);
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    function onchangeChucdanh() {
        var chucdanh = $("select[name=chucdanh]").val();
        var requestData = {
            chucdanh: chucdanh
        };

        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "ThemNhanSu.aspx/loadBacLuongTheoChucDanh",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var data = result.d;
                $("select[name=bacluong]").html(data);
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
        onchangeChucdanh();

        $("input[name=cmnd]").focusout(function () {
            searchNVByCMND();
        });

        $("input[name=cmnd]").keypress(function (e) {
            if (e.which == 13) {
                searchNVByCMND();
            }
        });

        $("select[name=chucdanh]").change(function () {
            onchangeChucdanh();
        });

        $("#luuChung").click(function () {
            var id = $("input[name=id]").val();
            var cmnd = $("input[name=cmnd]").val();
            var manv = $("input[name=manv]").val();
            var hoten = $("input[name=hoten]").val();
            var bank = $("input[name=bank]").val();
            var chinhthuc = $("input[name=chinhthuc]").is(":checked");
            var dangvien = $("input[name=dangvien]").is(":checked");
            var donvi = $("select[name=donvi]").val();
            var chucdanh = $("select[name=chucdanh]").val();
            var bacluong = $("select[name=bacluong]").val();
            var nhomdonvi = $("select[name=nhomdonvi]").val(); 
            var ngaykyhd = $("input[name=ngaykyhd]").val();

            if (id == "") {
                alert("Nhân viên không tồn tại trên hệ thống! Vui lòng liên hệ phòng ĐHNV để được hỗ trợ.");
                return false;
            }
            else if (hoten == "") {
                alert("Vui lòng nhập tên nhân viên!");
                return false;
            }
            else if (ngaykyhd == "") {
                alert("Vui lòng chọn ngày ký hợp đồng!");
                return false;
            }

            var requestData = {
                id: id,
                cmnd: cmnd,
                manv: manv,
                hoten: hoten,
                bank: bank,
                chinhthuc: chinhthuc,
                dangvien: dangvien,
                donvi: donvi,
                chucdanh: chucdanh,
                bacluong: bacluong,
                nhomdonvi: nhomdonvi,
                ngaykyhd: ngaykyhd
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "ThemNhanSu.aspx/luuThongTinChung",
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
