<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="QL_NhanVien.aspx.cs" Inherits="VNPT_BSC.DanhMuc.QL_NhanVien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <!-- Customize css -->
    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>

    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>

    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">DANH MỤC NHÂN VIÊN</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12 ">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themNV">Thêm nhân viên</a>
                        <table id="table-nhanvien" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Họ tên</th>
                                    <th>Đơn vị</th>
                                    <th>Di động</th>
                                    <th>Chức vụ</th>
                                    <th>Chức danh</th>
                                    <th>Tài khoản</th>
                                    <th>Chỉnh sửa</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (dtnhanvien.Rows.Count == 0)
                                   { %>
                                <tr>
                                    <td colspan="2" class="text-center">No item</td>
                                </tr>
                                <% }
                                   else
                                   { %>
                                <% for (int i = 0; i < dtnhanvien.Rows.Count; i++)
                                   { %>
                                <%
                                       string nv_id = dtnhanvien.Rows[i][0].ToString();
                                       string nv_ten = dtnhanvien.Rows[i][1].ToString();
                                       string nv_ngaysinh = dtnhanvien.Rows[i][2].ToString();
                                       string nv_donvi = dtnhanvien.Rows[i]["donvi_ten"].ToString();
                                       string nv_dantoc = dtnhanvien.Rows[i][4].ToString();
                                       string nv_tongiao = dtnhanvien.Rows[i][5].ToString();
                                       string nv_trinhdo = dtnhanvien.Rows[i][6].ToString();
                                       string nv_gioitinh = dtnhanvien.Rows[i][7].ToString();
                                       string nv_noisinh = dtnhanvien.Rows[i][8].ToString();
                                       string nv_quequan = dtnhanvien.Rows[i][9].ToString();
                                       string nv_diachi = dtnhanvien.Rows[i][10].ToString();
                                       string nv_cmnd = dtnhanvien.Rows[i][11].ToString();
                                       string nv_ngaycap = dtnhanvien.Rows[i][12].ToString();
                                       string nv_noicap = dtnhanvien.Rows[i][13].ToString();
                                       string nv_doanvien = dtnhanvien.Rows[i][14].ToString();
                                       string nv_dangvien = dtnhanvien.Rows[i][15].ToString();
                                       string nv_ngayvaodang = dtnhanvien.Rows[i][16].ToString();
                                       string nv_ngayvaonganh = dtnhanvien.Rows[i][17].ToString();
                                       string nv_didong = dtnhanvien.Rows[i][18].ToString();
                                       string nv_email = dtnhanvien.Rows[i][19].ToString();
                                       string nv_chucvu = dtnhanvien.Rows[i]["chucvu_ten"].ToString();
                                       string nv_chucdanh = dtnhanvien.Rows[i]["chucdanh_ten"].ToString();
                                       string nv_taikhoan = dtnhanvien.Rows[i][22].ToString();
                                       string nv_matkhau = dtnhanvien.Rows[i][23].ToString();
                                     
                                %>
                                <tr>
                                    <td><%= nv_id %></td>
                                    <td><%=  nv_ten%></td>
                                    <td><%=  nv_donvi%></td>
                                    <td><%=  nv_didong%></td>
                                    <td><%= nv_chucvu %></td>
                                    <td><%=  nv_chucdanh%></td>
                                    <td><%=  nv_taikhoan%></td>
                                    <td>
                                        <a class="btn btn-primary btn-xs" type="button" data-target="#chitiet" data-toggle="modal" onclick="chitietdata('<%=nv_id %>','<%=nv_ten %>','<%=nv_ngaysinh %>','<%=nv_donvi %>'
                                            ,'<%=nv_dantoc %>','<%=nv_tongiao %>','<%=nv_trinhdo %>','<%=nv_gioitinh %>','<%=nv_quequan %>','<%=nv_noisinh %>','<%=nv_diachi %>','<%=nv_cmnd %>','<%=nv_ngaycap %>','<%=nv_noicap %>','<%=nv_doanvien %>','<%=nv_dangvien %>'
                                            ,'<%=nv_ngayvaodang %>','<%=nv_ngayvaonganh %>','<%=nv_didong %>','<%=nv_email %>','<%=nv_chucvu %>','<%=nv_chucdanh %>','<%=nv_taikhoan %>')">Thông tin chi tiết</a>

                                    </td>
                                </tr>
                                <% } %>
                                <% } %>
                            </tbody>
                        </table>
                        <div id="themNV" class="modal fade " role="dialog">
                            <div class="modal-dialog fix-modal">
                                <div class="modal-content row">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title" style="text-align: center">Thêm Nhân Viên</h4>
                                    </div>
                                    <!--left-->
                                    <div class="modal-body list-nhanvien">
                                        <div class="col-sm-6 form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Họ và tên:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="ten" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Chức vụ:</label>
                                                <div class="col-sm-8">
                                                    <select  class="form-control fix-day col-sm-8" id="chucvu">
                                                        <% for (int i = 0; i < dtchucvu_nv.Rows.Count; i++)
                                                           { %>
                                                        <%
                                                               string chucvu_id = dtchucvu_nv.Rows[i]["chucvu_id"].ToString();
                                                               string chucvu_ten = dtchucvu_nv.Rows[i]["chucvu_ten"].ToString();
                                                        %>
                                                        <option value="<%= chucvu_id%>"><%= chucvu_ten%></option>

                                                        <% } %>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Chức danh:</label>
                                                <div class="col-sm-8">
                                                    <select class="form-control fix-day col-sm-8" id="chucdanh">
                                                        <% for (int i = 0; i < dtchucdanh_nv.Rows.Count; i++)
                                                           { %>
                                                        <%
                                                               string chucdanh_id = dtchucdanh_nv.Rows[i]["chucdanh_id"].ToString();
                                                               string chucdanh_ten = dtchucdanh_nv.Rows[i]["chucdanh_ten"].ToString();
                                                        %>
                                                        <option value="<%= chucdanh_id%>"><%= chucdanh_ten%></option>

                                                        <% } %>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Đơn vị:</label>
                                                <div class="col-sm-8">
                                                    <select class="form-control fix-day col-sm-8" id="donvi">
                                                        <% for (int i = 0; i < dtdonvi_nv.Rows.Count; i++)
                                                           { %>
                                                        <%
                                                               string donvi_id = dtdonvi_nv.Rows[i]["donvi_id"].ToString();
                                                               string donvi_ten = dtdonvi_nv.Rows[i]["donvi_ten"].ToString();
                                                        %>
                                                        <option value="<%= donvi_id%>"><%= donvi_ten%></option>

                                                        <% } %>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Ngày sinh:</label>
                                                <div class="col-sm-8">
                                                    <input type="date" class="form-control  fix-day  fix-height-34" data-val="" id="ngaysinh" />
                                                </div>
                                            </div>

                                            <div class="form-group form-horizontal">
                                                <label class="control-label col-sm-4">Đảng viên:</label>
                                                <div class="col-sm-8">
                                                    <input type="checkbox" class="form-control fix-checkbox" data-val="" id="dang" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Ngày vào đảng:</label>
                                                <div class="col-sm-8">
                                                    <input type="date" class="form-control  fix-day fix-height-34" data-val="" id="ngaydang" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Di động:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="didong" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Email:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="email" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Địa chỉ:</label>
                                                <div class="col-sm-8">
                                                    <textarea class="form-control  fix-day" rows="5" id="diachi"></textarea>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Tài khoản:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="taikhoan" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Mật khẩu:</label>
                                                <div class="col-sm-8">
                                                    <input type="password" class="form-control" data-val="" id="matkhau" />
                                                </div>
                                            </div>

                                        </div>


                                        <!--right-->
                                        <div class="col-sm-6 form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Dân tộc:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="dantoc" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Tôn giáo:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="tongiao" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Trình độ:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="trinhdo" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Giới tính:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="gioitinh" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Ngày vào ngành:</label>
                                                <div class="col-sm-8">
                                                    <input type="date" class="form-control  fix-day fix-height-34" data-val="" id="ngaynganh" />
                                                </div>
                                            </div>
                                            <div class="form-group form-horizontal">
                                                <label class="control-label col-sm-4">Đoàn viên:</label>
                                                <div class="col-sm-8">
                                                    <input type="checkbox" class="form-control fix-checkbox" data-val="" id="doan" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Số CMND:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="cmnd" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Ngày cấp:</label>
                                                <div class="col-sm-8">
                                                    <input type="date" class="form-control  fix-day fix-height-34" data-val="" id="ngaycmnd" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Nơi cấp:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="noicmnd" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Nơi sinh:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="noisinh" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Quê quán:</label>
                                                <div class="col-sm-8">
                                                    <textarea class="form-control  fix-day" rows="5" id="quequan"></textarea>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-success" id="btnSave">Thêm nhân viên</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
                                </div>
                            </div>

                        </div>


                        <div id="chitiet" class="modal fade " role="dialog">
                            <div class="modal-dialog fix-modal">
                                <div class="modal-content row">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title" style="text-align: center">Chi tiết thông tin nhân viên</h4>
                                    </div>
                                    <input type="hidden" id="txtid_sua" />
                                    <!--left-->
                                    <div class="modal-body list-nhanvien">
                                        <div class="col-sm-6 form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Họ và tên:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtten" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Chức vụ:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtchucvu" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Chức danh:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtchucdanh" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Đơn vị:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtdonvi" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Ngày sinh:</label>
                                                <div class="col-sm-8">
                                                    <input type="date" class="form-control  fix-day  fix-height-34" data-val="" id="datengaysinh" />
                                                </div>
                                            </div>

                                            <div class="form-group form-horizontal">
                                                <label class="control-label col-sm-4">Đảng viên:</label>
                                                <div class="col-sm-8">
                                                    <input type="checkbox" class="form-control fix-checkbox" data-val="" id="cbdang" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Ngày vào đảng:</label>
                                                <div class="col-sm-8">
                                                    <input type="date" class="form-control  fix-day fix-height-34" data-val="" id="cbngaydang" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Di động:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtdidong" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Email:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtemail" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Địa chỉ:</label>
                                                <div class="col-sm-8">
                                                    <textarea class="form-control  fix-day" rows="5" id="txtcm"></textarea>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Tài khoản:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" readonly data-val="" id="txttaikhoan" />
                                                </div>
                                            </div>
                                        </div>


                                        <!--right-->
                                        <div class="col-sm-6 form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Dân tộc:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtdantoc" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Tôn giáo:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txttongiao" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Trình độ:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txttrinhdo" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Giới tính:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtgioitinh" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Ngày vào ngành:</label>
                                                <div class="col-sm-8">
                                                    <input type="date" class="form-control  fix-day fix-height-34" data-val="" id="datenganh" />
                                                </div>
                                            </div>
                                            <div class="form-group form-horizontal">
                                                <label class="control-label col-sm-4">Đoàn viên:</label>
                                                <div class="col-sm-8">
                                                    <input type="checkbox" class="form-control fix-checkbox" data-val="" id="checkdoan" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Số CMND:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtcmnd" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Ngày cấp:</label>
                                                <div class="col-sm-8">
                                                    <input type="date" class="form-control  fix-day fix-height-34" data-val="" id="datengaycmnd" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Nơi cấp:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtnoicap" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Nơi sinh:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtnoisinh" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Quê quán:</label>
                                                <div class="col-sm-8">
                                                    <textarea class="form-control  fix-day" rows="5" id="txtquequan"></textarea>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-success" id="btnEdit">Thêm đơn vị</a>
                                        <a class="btn btn-default" data-dismiss="modal">Đóng</a>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>



        function chitietdata(nv_id, nv_ten, datengaysinh, nv_donvi, nv_dantoc, nv_tongiao, nv_trinhdo, nv_gioitinh, nv_quequan,nv_noisinh, nv_diachi, nv_cmnd, nv_ngaycap, nv_noicap, nv_doanvien, nv_dangvien,
            nv_ngayvaodang, nv_ngayvaonganh, nv_didong, nv_email, nv_chucvu, nv_chucdanh, nv_taikhoan) {
            $('#txtid_sua').val(nv_id);
            $('#txtten').val(nv_ten);
            $('#datengaysinh').val(datengaysinh);
            $('#txtdonvi').val(nv_donvi);
            $('#txtchucdanh').val(nv_chucdanh);
            $('#txtchucvu').val(nv_chucvu);
            if (nv_dangvien == "True") {
                $('#cbdang').attr("checked", "true");
            }
            $('#cbngaydang').val(nv_ngayvaodang);
            $('#txtdidong').val(nv_didong);
            $('#txtemail').val(nv_email);
            $('#txtcm').val(nv_diachi);
            $('#txttaikhoan').val(nv_taikhoan);
            $('#txtdantoc').val(nv_dantoc);
            $('#txttongiao').val(nv_tongiao);
            $('#txttrinhdo').val(nv_trinhdo);
            $('#txtgioitinh').val(nv_gioitinh);
            $('#datenganh').val(nv_ngayvaonganh);
            $('#checkdoan').val(nv_doanvien);
            $('#txtcmnd').val(nv_cmnd);
            $('#datengaycmnd').val(nv_ngaycap);
            $('#txtnoicap').val(nv_noicap);
            $('#txtnoisinh').val(nv_noisinh);
            $('#txtquequan').val(nv_quequan);
        }


        $(document).ready(function () {

            $("#btnSave").click(function () {
                var nv_ten = $("#ten").val();
                var nv_ngaysinh = $('#ngaysinh').val();
                var nv_donvi = $("#donvi").val();
                var nv_dantoc = $("#dantoc").val();
                var nv_tongiao = $("#tongiao").val();
                var nv_trinhdo = $('#trinhdo').val();
                var nv_gioitinh = $("#gioitinh").val();
                var nv_noisinh = $("#noisinh").val();
                var nv_quequan = $("#quequan").val();
                var nv_diachi = $('#diachi').val();
                var nv_cmnd = $("#cmnd").val();
                var nv_ngaycap = $("#ngaycmnd").val();
                var nv_noicap = $("#noicmnd").val();
                var nv_doanvien = $("#doan").is(":checked");
                var nv_dangvien = $("#dang").is(":checked");
                var nv_ngayvaodang = $("#ngaydang").val();
                var nv_ngayvaonganh = $("#ngaynganh").val();
                var nv_didong = $('#didong').val();
                var nv_email = $("#email").val();
                var nv_chucvu = $("#chucvu").val();
                var nv_chucdanh = $("#chucdanh").val();
                var nv_taikhoan = $("#taikhoan").val();
                var nv_matkhau = $("#matkhau").val();
                //var isCheck = checkItemThem();
                //if (!isCheck) {
                //    return false;
                //}
                var requestData = {
                    nv_tenA: nv_ten,
                    nv_ngaysinhA: nv_ngaysinh,
                    nv_donviA: nv_donvi,
                    nv_dantocA: nv_dantoc,
                    nv_tongiaoA: nv_tongiao,
                    nv_trinhdoA: nv_trinhdo,
                    nv_gioitinhA: nv_gioitinh,
                    nv_noisinhA: nv_noisinh,
                    nv_quequanA: nv_quequan,
                    nv_diachiA: nv_diachi,
                    nv_cmndA: nv_cmnd,
                    nv_ngaycapA: nv_ngaycap,
                    nv_noicapA: nv_noicap,
                    nv_doanvienA: nv_doanvien,
                    nv_dangvienA: nv_dangvien,
                    nv_ngayvaodangA: nv_ngayvaodang,
                    nv_ngayvaonganhA: nv_ngayvaonganh,
                    nv_didongA: nv_didong,
                    nv_emailA: nv_email,
                    nv_chucvuA: nv_chucvu,
                    nv_chucdanhA: nv_chucdanh,
                    nv_taikhoanA: nv_taikhoan,
                    nv_matkhauA: nv_matkhau
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "QL_NhanVien.aspx/SaveData",
                    data: szRequest,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.d) {
                            swal({
                                title: "Thành Công",
                                text: "Bạn đã thêm dữ liệu thành công",
                                type: "success",
                                timer: 1000,
                                showConfirmButton: false
                            },
                            function () {
                                window.location.reload();
                            });
                        }
                        else {
                            alert_loi();
                        }
                    },
                    error: function (msg) { alert(msg.d); }
                });
            });

            $("#table-nhanvien").DataTable({
                "searching": true,
                "info": true,
            });
        });
    </script>
</asp:Content>
