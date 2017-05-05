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
                <h3 class="panel-title">DANH MỤC BẬC LƯƠNG</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12 table-responsive">
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
                                <th class="text-center">Lương duy trì</th>
                                <th class="text-center">Lương P3</th>
                                <th class="text-center">Chính thức</th>
                                <th class="text-center">Nghỉ thai sản</th>
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
                                   string luong_p3 = dtNhanVien.Rows[i]["luong_p3"].ToString();
                                   string luong_duytri = dtNhanVien.Rows[i]["luong_duytri"].ToString();
                                   string chinhthuc = dtNhanVien.Rows[i]["chinhthuc"].ToString();
                                   string szChinhThuc = "";
                                   if (Convert.ToBoolean(chinhthuc)) {
                                       szChinhThuc = "checked";
                                   }
                                   string thaisan = dtNhanVien.Rows[i]["thaisan"].ToString();
                                   string szThaiSan = "";
                                   if (Convert.ToBoolean(thaisan))
                                   {
                                       szThaiSan = "checked";
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
                                <td>
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
                                        <option <%=szSelected %> value="<%=blValue %>"><%= dtBacLuong.Rows[nBacluong]["ten_bacluong"] %></option>
                                        <% }} %>
                                    </select>
                                </td>
                                <td><input class="form-control min-width-150" type="text" id="stk_<%=id %>" value="<%=so_tk %>"/></td>
                                <td><input class="form-control cls-hesoluong" type="number" min="0" id="hesoluong_<%=id %>" value="<%=hesoluong %>"/></td>
                                <td class="text-center" id="luong_p3_<%=id %>"><%=luong_p3 %></td>
                                <td class="text-center" id="luong_duytri_<%=id %>"><%=luong_duytri %></td>
                                <td><input class="form-control" type="checkbox"  id="chinhthuc_<%=id %>" <%=szChinhThuc %>/></td>
                                <td><input class="form-control" type="checkbox"  id="thaisan_<%=id %>" <%=szThaiSan %>/></td>
                                <td class="text-center"><a class="btn btn-primary btn-xs btn-action">Lưu</a></td>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
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
        <% for (int nBacluong = 0; nBacluong < dtBacLuong.Rows.Count; nBacluong++) { %>
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
                    szOption += "<option value='" + arrBacLuong[i].id + "'>" + arrBacLuong[i].ten_bacluong + "</option>";
                }
            }

            $("#bacluong_" + id_nv).html(szOption);
        });

        $(document).on('change', '.cls-hesoluong', function () {
            var id_nv = $(this).closest("tr").attr("data-id");
            var value = $(this).val();
            if (!isNaN(value)) {
                var p3 = 1150000 * value;
                var duytri = 3500000 * value;
                $("#luong_p3_" + id_nv).text(p3);
                $("#luong_duytri_" + id_nv).text(duytri);
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
            var thaisan = $("#thaisan_" + id_nv).is(":checked");

            var requestData = {
                id_nv: id_nv,
                donvi: donvi,
                nhomdonvi: nhomdonvi,
                chucdanh: chucdanh,
                bacluong: bacluong,
                stk: stk,
                hesoluong: hesoluong,
                chinhthuc: chinhthuc,
                thaisan: thaisan
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
    });
</script>
</asp:Content>
