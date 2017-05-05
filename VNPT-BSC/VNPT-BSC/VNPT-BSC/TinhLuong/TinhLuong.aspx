<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="TinhLuong.aspx.cs" Inherits="VNPT_BSC.TinhLuong.TinhLuong" %>
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
            <h3 class="panel-title">Tính Lương</h3>
          </div>
          <div class="panel-body">
              <div class="col-md-12 col-xs-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Thời điểm:</label>
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
                    <label class="control-label col-sm-6">Lương phân phối:</label>
                    <div class="col-sm-4 form-inline">
                        <input type='text' class='form-control' id="luongPhanPhoi" onkeypress='return onlyNumbers(event.charCode || event.keyCode);' value="1249194867.50236"/>
                    </div>
                </div>
                <div class="form-group">
                    <div class='table-responsive padding-top-10'>
                        <table id='dtNhanVien' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>
                            <caption>
                                <a id="btnBHXH" class="btn btn-warning btn-xs">BHXH</a>
                                <a id="btnTT" class="btn btn-warning btn-xs">Thực Tế</a>
                            </caption>
                            <thead>
                                <tr>
                                    <th class='text-center'><input type='checkbox' id='checkall'/></th>
                                    <th class='text-center'>MNV</th>
                                    <th class='text-center'>Họ và Tên</th>
                                    <th class='text-center'>Ngày công BHXH</th>
                                    <th class='text-center'>Ngày công thực tế</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% for(int nIndex = 0; nIndex < dtNhanVien.Rows.Count; nIndex++){ %>
                                    <tr data-id-nv = "<%= dtNhanVien.Rows[nIndex]["id"].ToString() %>">
                                        <td class='text-center'><input name='checkboxItem' type='checkbox' /></td>
                                        <td class='text-center'><strong><%= dtNhanVien.Rows[nIndex]["ma_nhanvien"].ToString() %></strong></td>
                                        <td><strong><%= dtNhanVien.Rows[nIndex]["ten_nhanvien"].ToString() %></strong></td>
                                        <td class='text-center'><input name='ngayBHXH' type='number' class='form-control' min="0" id="ngayBHXH_<%=dtNhanVien.Rows[nIndex]["id"].ToString() %>" value="0"/></td>
                                        <td class='text-center'><input name='ngayTT' type='number' class='form-control' min="0" id="ngayTT_<%=dtNhanVien.Rows[nIndex]["id"].ToString() %>" value="0"/></td>
                                    </tr>
                                <% } %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-6 col-sm-4 form-inline">
                        <a class="btn btn-success" id="tinhLuong">Tính Lương</a>
                    </div>
                </div>
              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    function tinhLuong(thang, nam, luongphanphoi, arrChamCong) {
        var requestData = {
            thang: thang,
            nam: nam,
            luongphanphoi: luongphanphoi,
            arrChamCong: arrChamCong
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "TinhLuong.aspx/tinhLuong",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var result = data.d;
                if (result) {
                    window.location.replace("ChiTietTapThe.aspx?thang=" + thang + "&nam=" + nam);
                }
            },
            error: function (msg) { alert(msg.d); }
        });
    }

    $(document).ready(function () {
        //$("#dtNhanVien").DataTable();
        $("#checkall").click(function () {
            if (this.checked) {
                // Iterate each checkbox
                $('input[name=checkboxItem]').each(function () {
                    this.checked = true;
                });
            }
            else {
                $('input[name=checkboxItem]').each(function () {
                    this.checked = false;
                });
            }
        });

        $("#tinhLuong").click(function () {
            var thang = $("#month").val();
            var nam = $("#year").val();
            var luongPhanPhoi = $("#luongPhanPhoi").val();
            if (luongPhanPhoi == "") {
                luongPhanPhoi = 0;
            }
            var arrChamCong = new Array();
            $("input[name='checkboxItem']").each(function () {
                var id_nv = $(this).closest("tr").attr("data-id-nv");
                var ngaycongBHXH = $("#ngayBHXH_" + id_nv).val();
                var ngaycongTT = $("#ngayTT_" + id_nv).val();
                if (isNaN(ngaycongBHXH)) {
                    ngaycongBHXH = 0;
                }
                if (isNaN(ngaycongTT)) {
                    ngaycongTT = 0;
                }
                arrChamCong.push({
                    id_nv: id_nv,
                    ngaycongBHXH: ngaycongBHXH,
                    ngaycongTT: ngaycongTT
                });
            });

            tinhLuong(thang, nam, luongPhanPhoi, arrChamCong);
        });

        $("#btnBHXH").click(function () {
            swal({
                title: "Nhập ngày công BHXH!",
                type: "input",
                showCancelButton: true,
                closeOnConfirm: false,
                animation: "slide-from-top",
                inputPlaceholder: "Nhập ngày công"
            },
            function (inputValue) {
                if (inputValue === false) return false;

                if (inputValue === "") {
                    swal.showInputError("Bạn cần nhập số ngày công BHXH!");
                    return false
                }

                if (isNaN(inputValue)) {
                    swal.showInputError("Vui lòng nhập số!");
                    return false
                }

                $("input[name='checkboxItem']:checked").each(function () {
                    var id = $(this).closest("tr").attr("data-id-nv");
                    $("#ngayBHXH_" + id).val(inputValue);
                });

                swal({
                    title: "Auto close alert!",
                    text: "I will close in 1 seconds.",
                    timer: 1,
                    showConfirmButton: false
                });
            });
        });

        $("#btnTT").click(function () {
            swal({
                title: "Nhập ngày công thực tế!",
                type: "input",
                showCancelButton: true,
                closeOnConfirm: false,
                animation: "slide-from-top",
                inputPlaceholder: "Nhập ngày công"
            },
            function (inputValue) {
                if (inputValue === false) return false;

                if (inputValue === "") {
                    swal.showInputError("Bạn cần nhập số ngày công thực tế!");
                    return false
                }

                if (isNaN(inputValue)) {
                    swal.showInputError("Vui lòng nhập số!");
                    return false
                }

                $("input[name='checkboxItem']:checked").each(function () {
                    var id = $(this).closest("tr").attr("data-id-nv");
                    $("#ngayTT_" + id).val(inputValue);
                });

                swal({
                    title: "Auto close alert!",
                    text: "I will close in 1 seconds.",
                    timer: 1,
                    showConfirmButton: false
                });
            });
        });
    });
</script>
</asp:Content>
