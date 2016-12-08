<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VNPT_BSC.index" %>

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
    <div class="col-md-12 margin-top-30" id="bscgiao">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">DANH SÁCH KPI ĐƯỢC GIAO</h3>
            </div>
            <table id="table-chucdanh" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th>Nhân viên giao</th>
                        <th>Nhân viên nhận</th>
                        <th>Tháng</th>
                        <th>Năm</th>
                        <th>KPI</th>
                        <th>Đơn vị tính</th>
                        <th>Trọng số</th>
                        <th>Kế hoạch</th>
                        <th>Thực hiện</th>
                        <th>Thẩm định</th>
                    </tr>
                </thead>
                <tbody>
                    <% if (dtkpiindex.Rows.Count == 0)
                       { %>
                    <tr>
                        <td colspan="9" class="text-center">No item</td>
                    </tr>
                    <% }
                       else
                       { %>
                    <% for (int i = 0; i < dtkpiindex.Rows.Count; i++)
                       { %>
                    <%
                           string nv_giao = dtkpiindex.Rows[i]["giao"].ToString();
                           string nv_nhan = dtkpiindex.Rows[i]["nhan"].ToString();
                           string nv_thang = dtkpiindex.Rows[i][2].ToString();
                           string nv_nam = dtkpiindex.Rows[i][3].ToString();
                           string nv_kpi = dtkpiindex.Rows[i]["tenkpi"].ToString();
                           string nv_dvt = dtkpiindex.Rows[i][5].ToString();
                           string nv_ts = dtkpiindex.Rows[i][6].ToString();
                           string nv_kh = dtkpiindex.Rows[i][7].ToString();
                           string nv_th = dtkpiindex.Rows[i][8].ToString();
                           string nv_td = dtkpiindex.Rows[i][9].ToString();
                                     
                    %>
                    <tr>
                        <td><%= nv_giao %></td>
                        <td><%=  nv_nhan%></td>
                        <td><%=  nv_thang%></td>
                        <td><%=  nv_nam%></td>
                        <td><%=  nv_kpi%></td>
                        <td><%=  nv_dvt%></td>
                        <td><%= nv_ts %></td>
                        <td><%=  nv_kh%></td>
                        <td><%=  nv_th%></td>
                        <td><%=  nv_td%></td>
                    </tr>
                    <% } %>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>
    <div class="col-md-12 margin-top-30" id="thamdinh">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">DANH SÁCH KPI THẨM ĐỊNH</h3>
            </div>
            <table id="table1" class="table table-striped table-bordered table-full-width" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th>Nhân viên giao</th>
                        <th>Nhân viên nhận</th>
                        <th>Nhân viên thẩm định</th>
                        <th>Tháng</th>
                        <th>Năm</th>
                        <th>Trạng thái giao</th>
                        <th>Trạng thái nhận</th>
                        <th>Trạng thái chấm</th>
                        <th>Trạng thái thẩm định</th>
                        <th>Hết thúc</th>
                    </tr>
                </thead>
                <tbody>
                    <% if (dtthamdinhindex.Rows.Count == 0)
                       { %>
                    <tr>
                        <td colspan="9" class="text-center">No item</td>
                    </tr>
                    <% }
                       else
                       { %>
                    <% for (int i = 0; i < dtthamdinhindex.Rows.Count; i++)
                       { %>
                    <%
                           string nv_giao = dtthamdinhindex.Rows[i]["giao"].ToString();
                           string nv_nhan = dtthamdinhindex.Rows[i]["nhan"].ToString();
                           string nv_thamdinh = dtthamdinhindex.Rows[i]["thamdinh"].ToString();
                           string nv_thang = dtthamdinhindex.Rows[i]["thang"].ToString();
                           string nv_nam = dtthamdinhindex.Rows[i]["nam"].ToString();
                           string nv_ttgiao = dtthamdinhindex.Rows[i]["trangthaigiao"].ToString();
                           string nv_ttnhan = dtthamdinhindex.Rows[i]["trangthainhan"].ToString();
                           string nv_cham = dtthamdinhindex.Rows[i]["trangthaicham"].ToString();
                           string nv_ttthamdinh = dtthamdinhindex.Rows[i]["trangthaithamdinh"].ToString();
                           string nv_ttketthuc = dtthamdinhindex.Rows[i]["trangthaiketthuc"].ToString();
                                     
                    %>
                    <tr>
                        <td><%= nv_giao %></td>
                        <td><%=  nv_nhan%></td>
                        <td><%=  nv_thamdinh%></td>
                        <td><%=  nv_thang%></td>
                        <td><%=  nv_nam%></td>
                        <td><%=  nv_ttgiao%></td>
                        <td><%= nv_ttnhan %></td>
                        <td><%=  nv_cham%></td>
                        <td><%=  nv_ttthamdinh%></td>
                        <td><%=  nv_ttketthuc%></td>
                    </tr>
                    <% } %>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>

    <script>

       <% if (dtkpiindex.Rows.Count == 0)
          { %>
        $('#bscgiao').hide();
        <% } %>

        <% if (dtthamdinhindex.Rows.Count == 0)
           { %>
        $('#thamdinh').hide();
        <% } %>
    </script>
</asp:Content>
