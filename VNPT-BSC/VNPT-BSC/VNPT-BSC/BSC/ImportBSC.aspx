<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ImportBSC.aspx.cs" Inherits="VNPT_BSC.BSC.ImportBSC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Bootstrap/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- Customize css -->
    <link href="../Bootstrap/thangtgm_custom.css" rel="stylesheet" />

    <script src="../Bootstrap/jquery.js"></script>
    <script src="../Bootstrap/bootstrap.js"></script>
    <script src="../Bootstrap/function.js"></script>

    <!-- Plugin for datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.12/datatables.min.js"></script>
    <script src="../Bootstrap/dataTables.bootstrap.js"></script>
    <!-- Add for export data of datatable-->
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.4/css/buttons.dataTables.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.flash.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.print.min.js"></script>

    <!-- Plugin for swal alert -->
    <script src="../Bootstrap/sweetalert-dev.js"></script>
    <link href="../Bootstrap/sweetalert.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 margin-top-30">
        <div class="panel panel-primary">
          <div class="panel-heading">
            <h3 class="panel-title">Giao BSC đơn vị bằng Excel</h3>
          </div>
          <div class="panel-body">
              <div class="col-sm-12 form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-6">Chọn thời gian giao:</label>
                    <div class="col-sm-6 form-inline">
                        <asp:DropDownList runat="server" ID="DropDownListMonth" class="form-control">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="DropDownListYear" class="form-control">
                            <asp:ListItem Selected="True">2016</asp:ListItem>
                            <asp:ListItem>2017</asp:ListItem>
                            <asp:ListItem>2018</asp:ListItem>
                            <asp:ListItem>2019</asp:ListItem>
                            <asp:ListItem>2020</asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>2025</asp:ListItem>
                            <asp:ListItem>2026</asp:ListItem>
                            <asp:ListItem>2027</asp:ListItem>
                            <asp:ListItem>2028</asp:ListItem>
                            <asp:ListItem>2029</asp:ListItem>
                            <asp:ListItem>2030</asp:ListItem>
                            <asp:ListItem>2031</asp:ListItem>
                            <asp:ListItem>2032</asp:ListItem>
                            <asp:ListItem>2033</asp:ListItem>
                            <asp:ListItem>2034</asp:ListItem>
                            <asp:ListItem>2035</asp:ListItem>
                            <asp:ListItem>2036</asp:ListItem>
                            <asp:ListItem>2037</asp:ListItem>
                            <asp:ListItem>2038</asp:ListItem>
                            <asp:ListItem>2039</asp:ListItem>
                            <asp:ListItem>2040</asp:ListItem>
                            <asp:ListItem>2041</asp:ListItem>
                            <asp:ListItem>2042</asp:ListItem>
                            <asp:ListItem>2043</asp:ListItem>
                            <asp:ListItem>2044</asp:ListItem>
                            <asp:ListItem>2045</asp:ListItem>
                            <asp:ListItem>2046</asp:ListItem>
                            <asp:ListItem>2047</asp:ListItem>
                            <asp:ListItem>2048</asp:ListItem>
                            <asp:ListItem>2049</asp:ListItem>
                            <asp:ListItem>2050</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-6">Chọn file:</label>
                    <div class="col-sm-6 form-inline">
                        <asp:FileUpload ID="excelUpload" runat="server" class="form-control"/>
                        <asp:Button ID="btnUpload" runat="server" Text="Import" OnClick="btnUpload_Click" class="form-control"/>
                    </div>
                </div>
              </div>
              <div class="col-sm-12" id="gridBSC">
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
              </div>
          </div>
        </div>
    </div>
<script type="text/javascript">
    $(document).ready(function () {
        // Hiển thị danh sách các chức năng của ở BSC
        $(".qlybsc a").click();
    });
</script>
</asp:Content>
