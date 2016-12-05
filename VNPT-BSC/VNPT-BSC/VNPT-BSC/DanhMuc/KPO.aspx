<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="KPO.aspx.cs" Inherits="VNPT_BSC.DanhMuc.KPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Bootstrap/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Bootstrap/sweetalert.css">
    <link href="../Bootstrap/hien_custom.css" rel="stylesheet" />
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>
    <script src="../Bootstrap/Alert.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">DANH MỤC KPI</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="table-responsive fix-border-table">
                        <a class="btn btn-success btn-xl fix-label-margin-top" data-toggle="modal" data-target="#listBSC">Thêm KPI</a>
                        <table id="table-kpi" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tên KPO</th>
                                    <th>Mô tả KPO</th>
                                    <th>Ngày tạo KPO</th>
                                    <th class="fix-table-edit-edit">Người tạo KPO</th>
                                    <th class="fix-table-edit-edit">Chỉnh sửa</th>
                                </tr>
                            </thead>
                            <tbody>
                            <% if (dtkpo.Rows.Count == 0)
                                { %>
                            <tr>
                                <td colspan="2" class="text-center">No item</td>
                            </tr>
                            <% }
                                else
                                { %>
                            <% for (int i = 0; i < dtkpo.Rows.Count; i++)
                                { %>
                            <%
                                string kpo_id = dtkpo.Rows[i][0].ToString();
                                string kpo_ten = dtkpo.Rows[i][1].ToString();
                                string kpo_mota = dtkpo.Rows[i][2].ToString();
                                string kpo_ngaytao = dtkpo.Rows[i][3].ToString();
                                string kpo_nguoitao = dtkpo.Rows[i]["nhanvien_hoten"].ToString();

                            %>
                            <tr>
                                <td><%= kpo_id %></td>
                                <td><%= kpo_ten%></td>
                                <td><%= kpo_mota%></td>
                                <td><%= kpo_ngaytao %></td>
                                <td><%= kpo_nguoitao%></td>
                                <td>
                                    <a class="btn btn-primary btn-xs" type="button" data-target="#Editkpo" data-toggle="modal" onclick="editdata('<%=kpo_id %>','<%=kpo_ten%>','<%=kpo_mota%>','<%=kpo_ngaytao%>','<%=kpo_nguoitao%>')">Chỉnh sửa</a>
                                    <a class="btn btn-danger btn-xs" type="button" id="btnXoa" onclick="deletedata('<%=kpo_id %>')">Xóa</a>

                                </td>
                            </tr>
                            <% } %>
                            <% } %>
                        </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $("#table-kpo").DataTable({
                "searching": true,
                "info": true,
            });
        });
    </script>
</asp:Content>
