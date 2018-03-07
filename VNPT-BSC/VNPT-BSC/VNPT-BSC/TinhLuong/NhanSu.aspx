<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="NhanSu.aspx.cs" Inherits="VNPT_BSC.TinhLuong.NhanSu" %>
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
                <h3 class="panel-title">Danh mục nhân sự</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12 table-responsive">
                    <a class="btn btn-success btn-xl fix-label-margin-top" href="ThemNhanSu.aspx">Thêm nhân viên</a>
                    <table id="table-nhansu" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th class="text-center">MNV</th>
                                <th class="text-center">Họ và Tên</th>
                                <th class="text-center min-width-200">Đơn vị</th>
                                <th class="text-center min-width-200">Chức danh</th>
                                <th class="text-center min-width-200">Nhóm</th>
                                <th class="text-center min-width-72">Bậc lương</th>
                                <th class="text-center">Số TK</th>
                                <th class="text-center">Hệ số lương</th>
                                <th class="text-center">Lương cơ bản</th>
                                <th class="text-center">Ngày vào ngành</th>
                                <th class="text-center">Chính thức</th>
                                <th class="text-center">Đảng viên</th>
                                <th class="text-center">Tác vụ</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% for (int i = 0; i < dtNhanVien.Rows.Count; i++){
                                   string id = dtNhanVien.Rows[i]["id"].ToString();
                                   string ma_nhanvien = dtNhanVien.Rows[i]["ma_nhanvien"].ToString();
                                   string hoten = dtNhanVien.Rows[i]["ten_nhanvien"].ToString();
                                   string donvi = dtNhanVien.Rows[i]["donvi"].ToString();
                                   string nhomdonvi = dtNhanVien.Rows[i]["id_nhom_donvi"].ToString();
                                   string chucdanh = dtNhanVien.Rows[i]["chucdanh"].ToString();
                                   string bacluong = dtNhanVien.Rows[i]["id_bacluong"].ToString();
                                   string so_tk = dtNhanVien.Rows[i]["sotaikhoan"].ToString();
                                   string hesoluong = dtNhanVien.Rows[i]["hesoluong"].ToString();
                                   string luongcoban = dtNhanVien.Rows[i]["luongcoban"].ToString();
                                   string chinhthuc = dtNhanVien.Rows[i]["chinhthuc"].ToString();
                                   string ngaykyhd = dtNhanVien.Rows[i]["ngaykyhd"].ToString();
                                   string szChinhThuc = "";
                                   if (Convert.ToBoolean(chinhthuc)) {
                                       szChinhThuc = "checked";
                                   }

                                   if (bacluong == "") {
                                       bacluong = "0";
                                   }
                                   
                                   string dangvien = dtNhanVien.Rows[i]["dangvien"].ToString();
                                   string szDangVien = "";
                                   if (Convert.ToBoolean(dangvien))
                                   {
                                       szDangVien = "checked";
                                   }
                            %>
                            <tr data-id="<%=id %>">
                                <td class="text-center"><strong><%=ma_nhanvien %></strong></td>
                                <td class="min-width-150"><strong><%=hoten %></strong></td>
                                <!-- Đơn vị -->
                                <td>
                                    <select class="form-control" id="donvi_<%=id %>">
                                        <% for(int nDonvi = 0; nDonvi < dtDonvi.Rows.Count; nDonvi++){
                                               int dvValue = Convert.ToInt32(dtDonvi.Rows[nDonvi]["id"].ToString());
                                               string szSelected = "";
                                               if (dvValue == Convert.ToInt32(donvi)) {
                                                   szSelected = "selected";
                                               }
                                        %>
                                        <option <%=szSelected %> value="<%=dvValue %>"><%= dtDonvi.Rows[nDonvi]["ten_donvi"] %></option>
                                        <% } %>
                                    </select>
                                </td>
                                <!-- Chức danh -->
                                <td>
                                    <select class="form-control dropdownChucDanh" id="chucdanh_<%=id %>">
                                        <% for (int nChucdanh = 0; nChucdanh < dtChucDanh.Rows.Count; nChucdanh++)
                                           {
                                               int cdValue = Convert.ToInt32(dtChucDanh.Rows[nChucdanh]["id"].ToString());
                                               string szSelected = "";
                                               if (cdValue == Convert.ToInt32(chucdanh))
                                               {
                                                   szSelected = "selected";
                                               }
                                        %>
                                        <option <%=szSelected %> value="<%=cdValue %>"><%= dtChucDanh.Rows[nChucdanh]["ten_chucdanh"] %></option>
                                        <% } %>
                                    </select>
                                </td>
                                <!-- Nhóm Đơn vị -->
                                <td>
                                    <select class="form-control" id="nhom_donvi_<%=id %>">
                                        <% for (int nNhomDonvi = 0; nNhomDonvi < dtNhomDonvi.Rows.Count; nNhomDonvi++)
                                           {
                                               int ndvValue = Convert.ToInt32(dtNhomDonvi.Rows[nNhomDonvi]["id"].ToString());
                                               string szSelected = "";
                                               if (ndvValue == Convert.ToInt32(nhomdonvi))
                                               {
                                                   szSelected = "selected";
                                               }
                                        %>
                                        <option <%=szSelected %> value="<%=ndvValue %>"><%= dtNhomDonvi.Rows[nNhomDonvi]["ten_nhom"] %></option>
                                        <% } %>
                                    </select>
                                </td>
                                <!-- Bậc lương -->
                                <td class="text-center">
                                    <select class="form-control" id="bacluong_<%=id %>">
                                        <% for (int nBacluong = 0; nBacluong < dtBacLuong.Rows.Count; nBacluong++)
                                           {
                                               int blValue = Convert.ToInt32(dtBacLuong.Rows[nBacluong]["id"].ToString());
                                               int id_chucdanh = Convert.ToInt32(dtBacLuong.Rows[nBacluong]["id_chucdanh"].ToString());
                                               string szSelected = "";
                                               if (blValue == Convert.ToInt32(bacluong))
                                               {
                                                   szSelected = "selected";
                                               }
                                               if (id_chucdanh == Convert.ToInt32(chucdanh)){
                                        %>
                                        <option <%=szSelected %> value="<%=blValue %>">Bậc <%= dtBacLuong.Rows[nBacluong]["ten_bacluong"] %></option>
                                        <% }} %>
                                    </select>
                                </td>
                                <td><input class="form-control min-width-150" type="text" id="stk_<%=id %>" value="<%=so_tk %>"/></td>
                                <td><input class="form-control cls-hesoluong" type="number" min="0" id="hesoluong_<%=id %>" value="<%=hesoluong %>"/></td>
                                <td class="text-center" id="luongcoban_<%=id %>"><%=luongcoban %></td>
                                <td><input id="ngaykyhd_<%=id %>" class="date-picker form-control col-md-7 col-xs-12 ngaykyhd min-width-130" required="required" type="text" value="<%=ngaykyhd %>"/></td>
                                <td><input class="form-control" type="checkbox"  id="chinhthuc_<%=id %>" <%=szChinhThuc %>/></td>
                                <td><input class="form-control" type="checkbox"  id="dangvien_<%=id %>" <%=szDangVien %>/></td>
                                <td class="text-center">
                                    <div class="dropdown">
                                      <button class="btn btn-default dropdown-toggle btn-xs" type="button" id="dropdownMenu<%=id %>" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                        Chọn
                                        <span class="caret"></span>
                                      </button>
                                      <ul class="dropdown-menu" aria-labelledby="dropdownMenu<%=id %>">
                                        <li><a href="CapNhatNhanSu.aspx?user=<%=id %>">Thông tin khác</a></li>
                                        <li><a class="btn-kiemnhiem" data-target="#KiemNhiem" data-toggle="modal">Kiêm Nhiệm</a></li>
                                        <li><a class="btn-del">Nghỉ việc</a></li>
                                        <li><a class="btn-action">Lưu</a></li>
                                      </ul>
                                    </div>
                                </td>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>

                    <!-- EDIT ---->
                    <div id="KiemNhiem" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content col-md-12">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="text-align: center">CHỨC VỤ KIÊM NHIỆM</h4>
                                </div>
                                <input type="hidden" id="txt_id" />
                                <div class="modal-body form-horizontal">
                                    <% for (int nIndex = 0; nIndex < dtKiemNhiem.Rows.Count; nIndex++ ){ %>
                                        <div class="checkbox">
                                          <label><input type="checkbox" name="chucvu" value="<%=dtKiemNhiem.Rows[nIndex]["id"].ToString() %>" /><%=dtKiemNhiem.Rows[nIndex]["chucvu_kiemnhiem"].ToString() %></label>
                                        </div>
                                    <% } %>
                                </div>
                                <div class="modal-footer">
                                    <a class="btn btn-success" id="btnLuu">Lưu</a>
                                    <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    var arrBacLuong = new Array();
    $("#table-nhansu").DataTable({
        bSort: false
    });
    function getListBacLuong() {
        var arrTmp = new Array();
        <% for (int nBacluong = 0; nBacluong < dtBacLuong.Rows.Count; nBacluong++)
           { %>
        arrTmp.push({
            id: '<%=dtBacLuong.Rows[nBacluong]["id"].ToString()%>',
            ten_bacluong: '<%=dtBacLuong.Rows[nBacluong]["ten_bacluong"].ToString()%>',
            id_chucdanh: '<%=dtBacLuong.Rows[nBacluong]["id_chucdanh"].ToString()%>',
        });
        <%}%>
        return arrTmp;
    };

    $(document).ready(function () {
        arrBacLuong = getListBacLuong();

        $(document).on('change', '.dropdownChucDanh', function () {
            var chucdanh = $(this).val();
            var id_nv = $(this).closest("tr").attr("data-id");
            var szOption = "";
            for (var i = 0; i < arrBacLuong.length; i++) {
                if (chucdanh == arrBacLuong[i].id_chucdanh) {
                    szOption += "<option value='" + arrBacLuong[i].id + "'> Bậc " + arrBacLuong[i].ten_bacluong + "</option>";
                }
            }

            $("#bacluong_" + id_nv).html(szOption);
        });

        $(document).on('change', '.cls-hesoluong', function () {
            var id_nv = $(this).closest("tr").attr("data-id");
            var value = $(this).val();
            if (!isNaN(value)) {
                var coban = 3500000 * value;
                $("#luongcoban_" + id_nv).text(coban);
            }
        });

        $(document).on('click', '.btn-action', function () {
            var id_nv = $(this).closest("tr").attr("data-id");
            var donvi = $("#donvi_" + id_nv).val(); 
            var nhomdonvi = $("#nhom_donvi_" + id_nv).val();
            var chucdanh = $("#chucdanh_" + id_nv).val();
            var bacluong = $("#bacluong_" + id_nv).val();
            var stk = $("#stk_" + id_nv).val();
            var hesoluong = $("#hesoluong_" + id_nv).val();
            var chinhthuc = $("#chinhthuc_" + id_nv).is(":checked");
            var dangvien = $("#dangvien_" + id_nv).is(":checked");

            var requestData = {
                id_nv: id_nv,
                donvi: donvi,
                nhomdonvi: nhomdonvi,
                chucdanh: chucdanh,
                bacluong: bacluong,
                stk: stk,
                hesoluong: hesoluong,
                chinhthuc: chinhthuc,
                dangvien: dangvien
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "NhanSu.aspx/saveData",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var output = result.d;
                    if (output) {
                        swal({
                            title: "Lưu thành công!!",
                            text: "",
                            type: "success"
                        });
                    }
                    else {
                        swal("Error!!!", "Lưu không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $(document).on('click', '.btn-del', function () {
            var id_nv = $(this).closest("tr").attr("data-id");

            var requestData = {
                id_nv: id_nv
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "NhanSu.aspx/delNV",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var output = result.d;
                    if (output) {
                        swal({
                            title: "Duyệt nghỉ việc thành công!!",
                            text: "",
                            type: "success"
                        });
                    }
                    else {
                        swal("Error!!!", "Duyệt nghỉ việc không thành công!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $(document).on("click", '.btn-kiemnhiem', function () {
            var id_nv = $(this).closest("tr").attr("data-id");
            $("input[name=chucvu]").each(function () {
                this.checked = false;
            });

            $("#txt_id").val(id_nv);
            var requestData = {
                id_nv: id_nv
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "NhanSu.aspx/loadKiemNhiem",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var arrKPI = new Array();
                    arrKPI = result.d;
                    for (var i = 0; i < arrKPI.length; i++) {
                        var KPI_ID = arrKPI[i];
                        $(":checkbox[value='" + KPI_ID + "']").prop("checked", "true");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $("#btnLuu").click(function () {
            var id = $("#txt_id").val();
            var chucvu = new Array();
            $("input[name=chucvu]").each(function () {
                var isChecked = $(this).is(":checked");
                if (isChecked) {
                    chucvu.push($(this).val());
                }
            });

            var requestData = {
                id_nv: id,
                chucvu: chucvu
            };

            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "NhanSu.aspx/SaveKiemNhiem",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d) {
                        swal({
                            title: "Lưu dữ liệu thành công!!!",
                            text: "",
                            type: "success"
                        },
                        function () {
                            //window.location.reload();
                        });
                    }
                    else {
                        swal("Error", "Vui lòng check lại!!!", "error");
                    }
                },
                error: function (msg) { alert(msg.d); }
            });
        });

        $(".date-picker").daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                format: 'DD/MM/YYYY'
            },
            maxDate: new Date()
        });
    });
</script>
</asp:Content>
