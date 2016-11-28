<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PhanPhoiBSCDonVi.aspx.cs" Inherits="VNPT_BSC.BSC.PhanPhoiBSCDonVi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" />

    <!-- Customize css -->
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>

    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
          <div class="panel-heading">
            <h3 class="panel-title">GIAO BSC CHO ĐƠN VỊ</h3>
          </div>
          <div class="panel-body">
              <div class="col-sm-3">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Danh Sách Đơn Vị</h3>
                        </div>
                        <ul class="list-group">
                            <% for(int i = 0; i < dtDonvi.Rows.Count; i++){ %>
                                <%
                                    string donvi_id =  dtDonvi.Rows[i]["donvi_id"].ToString();
                                    string donvi_ten =  dtDonvi.Rows[i]["donvi_ten"].ToString();
                                %>
                                <a href="#" class="list-group-item list-group-item-info text-left"><%= donvi_ten%></a>
                            <% } %>
                        </ul>
                    </div>
              </div>
              <div class="col-sm-9 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-3">Đơn vị nhận:</label>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" data-val="5" value="ABC" size="50" id="donvi"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Thời gian:</label>
                    <div class="col-sm-6 form-inline">
                        <select class="form-control" id="month">
                            <% for(int nMonth = 1; nMonth <= 12; nMonth++){ %>
                                <option><%= nMonth %></option>
                            <% } %>
                        </select>
                        <select class="form-control" id="year">
                            <% for(int nYear = 1900; nYear <= 2100; nYear++){ %>
                                <option><%= nYear %></option>
                            <% } %>
                        </select>
                        <a href="#" class="btn btn-success" id="getCurrentDate">Hiện tại</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Trạng thái giao:</label>
                    <div class="col-sm-8 form-inline">
                        <% string clsGiao = "label-default"; %>
                        <span class="label <%=clsGiao %>">Chưa giao</span>
                        <a href="#" class="btn btn-success btn-xs" id="updateGiaoStatus">Giao</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Trạng thái nhận:</label>
                    <div class="col-sm-8 form-inline">
                        <% string clsNhan = "label-default"; %>
                        <span class="label <%=clsNhan %>">Chưa nhận</span>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3">Trạng thái kết thúc:</label>
                    <div class="col-sm-8 form-inline">
                        <% string clsKetThuc = "label-default"; %>
                        <span class="label <%=clsKetThuc %>">Chưa kết thúc</span>
                        <a href="#" class="btn btn-success btn-xs" id="updateKTStatus">Kết thúc</a>
                    </div>
                </div>
                <div class="col-sm-12">
                      <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Danh sách KPI
                            <div class="pull-right">
                                <div class="btn-group">
                                    <a class="btn btn-primary btn-xs" data-toggle="modal" data-target="#listBSC">
                                        Mẫu BSC
                                    </a>
                                    <!-- Modal for BSC list -->
                                    <div id="listBSC" class="modal fade" role="dialog">
                                      <div class="modal-dialog">

                                        <!-- Modal content-->
                                        <div class="modal-content">
                                          <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Danh sách mẫu BSC</h4>
                                          </div>
                                          <div class="modal-body list-BSC">
                                            <% for(int iBSC = 0; iBSC < dtBSC.Rows.Count; iBSC++){ %>
                                                <%
                                                    string thang =  dtBSC.Rows[iBSC]["thang"].ToString();
                                                    string nam =  dtBSC.Rows[iBSC]["nam"].ToString();
                                                    string noidung =  dtBSC.Rows[iBSC]["content"].ToString();
                                                %>
                                              <div class="radio">
                                                <label><input type="radio" name="optradioBSC" data-thang="<%=thang %>" data-nam="<%=nam %>"><%= noidung%></label>
                                              </div>
                                          <% } %>
                                          </div>
                                          <div class="modal-footer">
                                            <button type="button" class="btn btn-success" data-dismiss="modal" id="loadBSC">Load</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                          </div>
                                        </div>

                                      </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body" id="gridBSC">
                            
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
             </div>
          </div>
        </div>
    </div>

<script type="text/javascript">
    $(document).ready(function () {

        /*Get current date when user click Now buttion*/
        $("#getCurrentDate").click(function () {
            var curMonth = "<%= DateTime.Now.ToString("MM") %>";
            var curYear = "<%= DateTime.Now.ToString("yyyy") %>";
            $("#month").val(curMonth);
            $("#year").val(curYear);
        });

        $("#loadBSC").click(function () {
            var thang = $("input[name=optradioBSC]:checked").attr("data-thang");
            var nam = $("input[name=optradioBSC]:checked").attr("data-nam");
            var requestData = {
                thang: thang,
                nam: nam
            };
            var szRequest = JSON.stringify(requestData);
            $.ajax({
                type: "POST",
                url: "PhanPhoiBSCDonVi.aspx/loadBSC",
                data: szRequest,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var arrKPI = result.d;
                    
                },
                error: function (msg) { alert(msg.d); }
            });
        });
    });
</script>
</asp:Content>
