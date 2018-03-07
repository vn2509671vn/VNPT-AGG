<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_QR_Code.aspx.cs" Inherits="VNPT_BSC.QRCode.Test_QR_Code" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test QR Code</title>
    <script src="/Bootstrap/jquery.js"></script>
    <script src="/Bootstrap/bootstrap.js"></script>
    <link href="/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="/Bootstrap/hien_custom.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Bootstrap/sweetalert.css">
</head>
<body>
    <form id="form1" runat="server">
    <div class="col-md-4 col-md-offset-4 col-xs-12 margin-top-120">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title" style="text-align:center">Test QR Code</h3>
            </div>
            <div class="panel-body">
                <div style="margin-bottom: 10px" class="input-group">
                    <textarea id="mathanhtoan" class="form-control"placeholder="Nhập Mã thanh toán" rows="5" cols="55"></textarea>
                </div>
                <a id="btn-login"  class="btn btn-success col-sm-12">Mã hóa</a>
            </div>
            <div class="text-center">
                <img class="img-rounded" alt="" title=""/>
                <br />
            </div>
        </div>
        <a href="../index.aspx">Trở về trang chủ</a>
    </div>
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btn-login").click(function () {
            var url = "https://api.qrserver.com/v1/create-qr-code/?data=" + $("#mathanhtoan").val() + "&size=100x100";
            $("img").attr("src", url);
        });
    });
</script>
</html>