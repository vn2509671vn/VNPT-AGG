<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="TinhLuongTheoBuoc.aspx.cs" Inherits="VNPT_BSC.TinhLuong.TinhLuongTheoBuoc" %>
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
                    <div class='table-responsive padding-top-10'>
                        <table id='dtNhanVien' class='table table-striped table-bordered table-full-width' cellspacing='0' width='100%'>
                            <thead>
                                <tr>
                                    <th class='text-center'><input type='checkbox' id='checkall'/></th>
                                    <th class='text-center'>Chức năng</th>
                                    <th class='text-center'>Trạng thái</th>
                                    <th class='text-center'>Ghi chú</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_dongbo_tongdiem_bsc_donvi_hangthang" /></td>
                                    <td><strong>Đồng bộ điểm BSC/KPI đơn vị</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_bsc_donvi" class="label label-default">Chưa đồng bộ</span></td>
                                    <td>Nếu dữ liệu không được đồng bộ sẽ lấy mặc định là 100%</td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_dongbo_tongdiem_bsc_nhanvien_hangthang" /></td>
                                    <td><strong>Đồng bộ điểm BSC/KPI nhân viên</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_bsc_nhanvien" class="label label-default">Chưa đồng bộ</span></td>
                                    <td>Nếu dữ liệu không được đồng bộ sẽ lấy mặc định là 100%</td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_dongbo_luong_pttb_nhanvien_tructiep" /></td>
                                    <td><strong>Đồng bộ lương PTTB cho nhân viên khối trực tiếp</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_luong_nhanvien_truoctiep" class="label label-default">Chưa đồng bộ</span></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_dongbo_luong_pttb_lanhdao_tructiep" /></td>
                                    <td><strong>Đồng bộ lương PTTB cho lãnh đạo khối trực tiếp</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_luong_lanhdao_truoctiep" class="label label-default">Chưa đồng bộ</span></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_pttb_tongluong_pttb_khoitructiep" /></td>
                                    <td><strong>Đồng bộ lương PTTB cho Trung tâm và LXN</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_luong_pttb_trungtam_lxn" class="label label-default">Chưa đồng bộ</span></td>
                                    <td>Dữ liệu này dùng để tính quỹ lương cho các phòng ban chức năng và LXNGT</td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_pttb_quyluong_khoigiantiep" /></td>
                                    <td><strong>Đồng bộ quỹ lương PTTB cho các phòng ban chức năng và LXNGT</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_quyluong_pttb_khoigiantiep" class="label label-default">Chưa đồng bộ</span></td>
                                    <td>Dữ liệu này dùng để tính quỹ lương cho nhân viên khối gián tiếp</td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_pttb_chitietluong_khoigiantiep" /></td>
                                    <td><strong>Đồng bộ lương PTTB cho nhân viên phòng ban chức năng và LXNGT</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_pttb_khoigiantiep" class="label label-default">Chưa đồng bộ</span></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_dongbo_quyluong_bsc_khoitructiep" /></td>
                                    <td><strong>Đồng bộ quỹ lương 3PS cho khối trực tiếp</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_quyluong_bsc_khoitructiep" class="label label-default">Chưa đồng bộ</span></td>
                                    <td>Dữ liệu này dùng để tính lương 3PS cho nhân viên khối trực tiếp</td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_dongbo_quyluong_bsc_khoigiantiep" /></td>
                                    <td><strong>Đồng bộ quỹ lương 3PS cho khối gián tiếp</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_quyluong_bsc_khoigiantiep" class="label label-default">Chưa đồng bộ</span></td>
                                    <td>Dữ liệu này dùng để tính lương 3PS cho nhân viên khối gián tiếp</td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_chitiet_luong_bsc_khoitructiep" /></td>
                                    <td><strong>Đồng bộ lương 3PS cho khối trực tiếp</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_luong_bsc_khoitructiep" class="label label-default">Chưa đồng bộ</span></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_chitiet_luong_bsc_khoigiantiep" /></td>
                                    <td><strong>Đồng bộ lương 3PS cho khối gián tiếp</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_luong_bsc_khoigiantiep" class="label label-default">Chưa đồng bộ</span></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class='text-center'><input name='checkboxItem' type='checkbox' data-name="sp_chitiet_luong_tonghop_3ps" /></td>
                                    <td><strong>Tạo bảng lương tổng hợp 3PS cho tất cả nhân viên</strong></td>
                                    <td class='text-center'><span id="trangthai_dongbo_luong_bsc_tonghop_3ps" class="label label-default">Chưa tạo</span></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-6 col-sm-4 form-inline">
                        <a class="btn btn-success" id="btnThucThi">Thực hiện</a>
                    </div>
                </div>
              </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    function checkDongBo() {
        var thang = $("#month").val();
        var nam = $("#year").val();
        var requestData = {
            thang: thang,
            nam: nam
        };
        var szRequest = JSON.stringify(requestData);
        $.ajax({
            type: "POST",
            url: "TinhLuongTheoBuoc.aspx/kiemtraDongBo",
            data: szRequest,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var output = data.d;
                if (output.trangthai_dongbo_bsc_donvi == "1") {
                    $("#trangthai_dongbo_bsc_donvi").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_bsc_donvi").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_bsc_nhanvien == "1") {
                    $("#trangthai_dongbo_bsc_nhanvien").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_bsc_nhanvien").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_luong_nhanvien_truoctiep == "1") {
                    $("#trangthai_dongbo_luong_nhanvien_truoctiep").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_luong_nhanvien_truoctiep").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_luong_lanhdao_truoctiep == "1") {
                    $("#trangthai_dongbo_luong_lanhdao_truoctiep").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_luong_lanhdao_truoctiep").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_luong_pttb_trungtam_lxn == "1") {
                    $("#trangthai_dongbo_luong_pttb_trungtam_lxn").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_luong_pttb_trungtam_lxn").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_quyluong_pttb_khoigiantiep == "1") {
                    $("#trangthai_dongbo_quyluong_pttb_khoigiantiep").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_quyluong_pttb_khoigiantiep").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_pttb_khoigiantiep == "1") {
                    $("#trangthai_dongbo_pttb_khoigiantiep").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_pttb_khoigiantiep").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_quyluong_bsc_khoitructiep == "1") {
                    $("#trangthai_dongbo_quyluong_bsc_khoitructiep").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_quyluong_bsc_khoitructiep").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_quyluong_bsc_khoigiantiep == "1") {
                    $("#trangthai_dongbo_quyluong_bsc_khoigiantiep").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_quyluong_bsc_khoigiantiep").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_luong_bsc_khoitructiep == "1") {
                    $("#trangthai_dongbo_luong_bsc_khoitructiep").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_luong_bsc_khoitructiep").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_luong_bsc_khoigiantiep == "1") {
                    $("#trangthai_dongbo_luong_bsc_khoigiantiep").removeClass("label-default").addClass("label-success").text("Đã đồng bộ");
                }
                else {
                    $("#trangthai_dongbo_luong_bsc_khoigiantiep").addClass("label-default").removeClass("label-success").text("Chưa đồng bộ");
                }

                if (output.trangthai_dongbo_luong_bsc_tonghop_3ps == "1") {
                    $("#trangthai_dongbo_luong_bsc_tonghop_3ps").removeClass("label-default").addClass("label-success").text("Đã tạo");
                }
                else {
                    $("#trangthai_dongbo_luong_bsc_tonghop_3ps").addClass("label-default").removeClass("label-success").text("Chưa tạo");
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

        $("#btnThucThi").click(function () {
            var lstStore = new Array();
            var thang = $("#month").val();
            var nam = $("#year").val();

            $('input[name=checkboxItem]:checked').each(function () {
                lstStore.push($(this).data("name"));
            });

            var requestData = {
                thang: thang,
                nam: nam,
                lstStore: lstStore
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "TinhLuongTheoBuoc.aspx/tienhanhDongBo",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var output = data.d;
                    if (output == 1) {
                        swal({
                            title: "Thực hiện các store thành công!!!",
                            text: "",
                            type: "success"
                        },
                    function () {
                            window.location.reload();
                        });
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#month").change(function () {
            checkDongBo();
        });

        $("#year").change(function () {
            checkDongBo();
        });
    });
</script>
</asp:Content>
