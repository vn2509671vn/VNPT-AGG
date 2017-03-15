<%@ Page Title="" Language="C#" MasterPageFile="~/MasterLayout.Master" AutoEventWireup="true" CodeBehind="QL_NhanVien.aspx.cs" Inherits="VNPT_BSC.DanhMuc.QL_NhanVien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <!-- Customize css -->
    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>--%>

    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/function.js"></script>
    <script src="../Bootstrap/Alert.js"></script>
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
                <h3 class="panel-title">DANH MỤC NHÂN VIÊN</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-12 col-xs-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#themNV">Thêm nhân viên</a>
                        <table id="table-nhanvien" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Họ tên</th>
                                    <th>Đơn vị</th>
                                    <th>Di động</th>
                                    <th>Chức danh</th>
                                    <th>Tài khoản</th>
                                    <th class="fix-table-edit-edit">Chỉnh sửa</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (dtnhanvien.Rows.Count == 0)
                                   { %>
                                <tr>
                                    <td colspan="7" class="text-center">No item</td>
                                </tr>
                                <% }
                                   else
                                   { %>
                                <% for (int i = 0; i < dtnhanvien.Rows.Count; i++)
                                   { %>
                                <%
                                       string nv_id = dtnhanvien.Rows[i]["nhanvien_id"].ToString();
                                       string nv_ten = dtnhanvien.Rows[i]["nhanvien_hoten"].ToString();
                                       string nv_ngaysinh = dtnhanvien.Rows[i]["nhanvien_ngaysinh"].ToString();
                                       string nv_donvi = dtnhanvien.Rows[i]["donvi_ten"].ToString();
                                       string nv_dantoc = dtnhanvien.Rows[i]["nhanvien_dantoc"].ToString();
                                       string nv_tongiao = dtnhanvien.Rows[i]["nhanvien_tongiao"].ToString();
                                       string nv_trinhdo = dtnhanvien.Rows[i]["nhanvien_trinhdo"].ToString();
                                       string nv_gioitinh = dtnhanvien.Rows[i]["nhanvien_gioitinh"].ToString();
                                       string nv_noisinh = dtnhanvien.Rows[i]["nhanvien_noisinh"].ToString();
                                       string nv_quequan = dtnhanvien.Rows[i]["nhanvien_quequan"].ToString();
                                       string nv_diachi = dtnhanvien.Rows[i]["nhanvien_diachi"].ToString();
                                       string nv_cmnd = dtnhanvien.Rows[i]["nhanvien_cmnd"].ToString();
                                       string nv_ngaycap = dtnhanvien.Rows[i]["nhanvien_ngaycapcmnd"].ToString();
                                       string nv_noicap = dtnhanvien.Rows[i]["nhanvien_noicapcmnd"].ToString();
                                       string nv_doanvien = dtnhanvien.Rows[i]["nhanvien_doanvien"].ToString();
                                       string nv_dangvien = dtnhanvien.Rows[i]["nhanvien_dangvien"].ToString();
                                       string nv_ngayvaodang = dtnhanvien.Rows[i]["nhanvien_ngayvaodang"].ToString();
                                       string nv_ngayvaonganh = dtnhanvien.Rows[i]["nhanvien_ngayvaonganh"].ToString();
                                       string nv_didong = dtnhanvien.Rows[i]["nhanvien_didong"].ToString();
                                       string nv_email = dtnhanvien.Rows[i]["nhanvien_email"].ToString();
                                       string nv_chucdanh = dtnhanvien.Rows[i]["chucdanh_ten"].ToString();
                                       string nv_taikhoan = dtnhanvien.Rows[i]["nhanvien_taikhoan"].ToString();
                                       string nv_matkhau = dtnhanvien.Rows[i]["nhanvien_matkhau"].ToString();
                                       string nv_chucdanh_id = dtnhanvien.Rows[i]["chucdanh_id"].ToString();
                                       string nv_donvi_id = dtnhanvien.Rows[i]["donvi_id"].ToString();
                                     
                                %>
                                <tr>
                                    <td><%= nv_id %></td>
                                    <td><%=  nv_ten%></td>
                                    <td><%=  nv_donvi%></td>
                                    <td><%=  nv_didong%></td>
                                    <td><%=  nv_chucdanh%></td>
                                    <td><%=  nv_taikhoan%></td>
                                    <td>
                                        <a class="btn btn-primary btn-xs" type="button" data-target="#chitiet" data-toggle="modal" onclick="chitietdata
                                            ('<%=nv_id %>',
                                            '<%=nv_ten %>',
                                            '<%=nv_ngaysinh %>',
                                            '<%=nv_donvi_id %>',
                                            '<%=nv_dantoc %>',
                                            '<%=nv_tongiao %>',
                                            '<%=nv_trinhdo %>',
                                            '<%=nv_gioitinh %>',
                                            '<%=nv_quequan %>',
                                            '<%=nv_noisinh %>',
                                            '<%=nv_diachi %>',
                                            '<%=nv_cmnd %>',
                                            '<%=nv_ngaycap %>',
                                            '<%=nv_noicap %>',
                                            '<%=nv_doanvien %>',
                                            '<%=nv_dangvien %>',
                                            '<%=nv_ngayvaodang %>',
                                            '<%=nv_ngayvaonganh %>',
                                            '<%=nv_didong %>',
                                            '<%=nv_email %>',
                                            '<%=nv_chucdanh_id %>',
                                            '<%=nv_taikhoan %>')">Thông tin chi tiết</a>
                                        <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=nv_id %>')">Xóa</a>

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
                                                <label class="control-label col-sm-4">Chức danh:</label>
                                                <div class="col-sm-8">
                                                     <select class="form-control fix-day col-sm-8" id="chucdanh_edit">
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
                                                    <select class="form-control fix-day col-sm-8" id="donvi_edit">
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
                                                    <input type="date" class="form-control  fix-day  fix-height-34" data-val="" id="datengaysinh" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Nơi sinh:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control" data-val="" id="txtnoisinh" />
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
                                                <label class="control-label col-sm-4">Quê quán:</label>
                                                <div class="col-sm-8">
                                                    <textarea class="form-control  fix-day" rows="5" id="txtquequan"></textarea>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <a class="btn btn-success" id="btnEdit">Chỉnh sửa</a>
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
        function chitietdata
            (nv_id,
            nv_ten,
            datengaysinh,
            nv_donvi_id,
            nv_dantoc,
            nv_tongiao,
            nv_trinhdo,
            nv_gioitinh,
            nv_quequan,
            nv_noisinh,
            nv_diachi,
            nv_cmnd,
            nv_ngaycap,
            nv_noicap,
            nv_doanvien,
            nv_dangvien,
            nv_ngayvaodang,
            nv_ngayvaonganh,
            nv_didong,
            nv_email,
            nv_chucdanh_id,
            nv_taikhoan) {
            $('#txtid_sua').val(nv_id);
            $('#txtten').val(nv_ten);
            $('#datengaysinh').val(datengaysinh);
            $('#donvi_edit').val(nv_donvi_id);
            $('#txtdantoc').val(nv_dantoc);
            $('#txttongiao').val(nv_tongiao);
            $('#txttrinhdo').val(nv_trinhdo);
            $('#txtgioitinh').val(nv_gioitinh);
            $('#txtquequan').val(nv_quequan);
            $('#txtnoisinh').val(nv_noisinh);
            $('#txtcm').val(nv_diachi);
            $('#txtcmnd').val(nv_cmnd);
            $('#datengaycmnd').val(nv_ngaycap);
            $('#txtnoicap').val(nv_noicap);
            if (nv_doanvien == "True") {
                $('#checkdoan').attr("checked", "true");
            }
            if (nv_dangvien == "True") {
                $('#cbdang').attr("checked", "true");
            }
            $('#cbngaydang').val(nv_ngayvaodang);
            $('#datenganh').val(nv_ngayvaonganh);
            $('#txtdidong').val(nv_didong);
            $('#txtemail').val(nv_email);
            $('#chucdanh_edit').val(nv_chucdanh_id);
            $('#txttaikhoan').val(nv_taikhoan);
           
        }



        function deletedata(nv_id) {
            swal({
                title: "Bạn có chắc không?",
                text: "Bạn sẽ xóa dữ liệu này!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Xóa dữ liệu",
                cancelButtonText: "Đóng",
                closeOnConfirm: false,
                closeOnCancel: false
            },
                   function (isConfirm) {
                       if (isConfirm) {
                           var requestData = {
                               nv_id_xoaAprove: nv_id,
                           };
                           var szRequest = JSON.stringify(requestData);
                           $.ajax({
                               type: "POST",
                               url: "QL_Nhanvien.aspx/DeleteData",
                               data: szRequest,
                               contentType: "application/json; charset=utf-8",
                               dataType: "json",
                               success: function (result) {
                                   if (result.d) {
                                       swal({
                                           title: "Thành Công",
                                           text: "Bạn đã Delete dữ liệu thành công",
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
                           });
                       } else {
                           swal({
                               title: "Đóng",
                               text: "Hủy xóa dữ liệu",
                               type: "error",
                               timer: 1000,
                               showConfirmButton: false
                           })
                       }
                   });
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

            $('#btnEdit').click(function () {
                var nv_id_sua = $("#txtid_sua").val();
                var nv_ten_sua = $("#txtten").val();
                var nv_chucdanh_sua = $("#chucdanh_edit").val();
                var nv_donvi_sua = $("#donvi_edit").val();
                var nv_datengaysinh_sua = $("#datengaysinh").val();
                var nv_dang_sua = $("#cbdang").is(":checked");
                var nv_ngaydang_sua = $("#cbngaydang").val();
                var nv_didong_sua = $("#txtdidong").val();
                var nv_email_sua = $("#txtemail").val();
                var nv_diachi_sua = $("#txtcm").val();
                var nv_dantoc_sua = $("#txtdantoc").val();
                var nv_tongiao_sua = $("#txttongiao").val();
                var nv_trinhdo_sua = $("#txttrinhdo").val();
                var nv_gioitinh_sua = $("#txtgioitinh").val();
                var nv_datenganh_sua = $("#datenganh").val();
                var nv_doan_sua = $("#checkdoan").is(":checked");
                var nv_cmnd_sua = $("#txtcmnd").val();
                var nv_ngaycmnd_sua = $("#datengaycmnd").val();
                var nv_noicmnd_sua = $("#txtnoicap").val();
                var nv_noisinh_sua = $("#txtnoisinh").val();
                var nv_quequan_sua = $("#txtquequan").val();
             
              
                //var isCheck = checkItemSua();
                //if (!isCheck) {
                //    return false;
                //}
                var requestData = {
                    nv_id_suaA: nv_id_sua,
                    nv_ten_suaA: nv_ten_sua,
                    nv_chucdanh_suaA: nv_chucdanh_sua,
                    nv_donvi_suaA:nv_donvi_sua,
                    nv_datengaysinh_suaA:nv_datengaysinh_sua,
                    nv_dang_suaA:nv_dang_sua,
                    nv_ngaydang_suaA:nv_ngaydang_sua,
                    nv_didong_suaA:nv_didong_sua,
                    nv_email_suaA:nv_email_sua,
                    nv_diachi_suaA:nv_diachi_sua,
                    nv_dantoc_suaA:nv_dantoc_sua,
                    nv_tongiao_suaA:nv_tongiao_sua,
                    nv_trinhdo_suaA:nv_trinhdo_sua,
                    nv_gioitinh_suaA:nv_gioitinh_sua,
                    nv_datenganh_suaA:nv_datenganh_sua,
                    nv_doan_suaA:nv_doan_sua,
                    nv_cmnd_suaA:nv_cmnd_sua,
                    nv_ngaycmnd_suaA:nv_ngaycmnd_sua,
                    nv_noicmnd_suaA:nv_noicmnd_sua,
                    nv_noisinh_suaA:nv_noisinh_sua,
                    nv_quequan_suaA:nv_quequan_sua
                };

                var szRequest = JSON.stringify(requestData);
                $.ajax({
                    type: "POST",
                    url: "QL_Nhanvien.aspx/EditData",
                    data: szRequest,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.d) {
                            swal({
                                title: "Thành Công",
                                text: "Bạn đã thay đổi dữ liệu thành công",
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
