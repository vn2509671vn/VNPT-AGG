<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VNPT_BSC.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Bootstrap/jquery.js"></script>
    <script src="/Bootstrap/bootstrap.js"></script>
    <link href="/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="/Bootstrap/hien_custom.css" rel="stylesheet" />
    <script src="../Bootstrap/sweetalert.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Bootstrap/sweetalert.css">

</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
        <div class="col-md-4 col-md-offset-4 col-xs-12 margin-top-120">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" style="text-align:center">ĐĂNG NHẬP HỆ THỐNG</h3>
                </div>
                <div class="panel-body">
                    <%--<h3 style="color: red">Hệ thống bảo trì để cập nhật BSC theo quy chế mới. Dự kiến bảo trì hoàn tất lúc 17h 13/06/2017</h3>--%>
                    <div style="margin-bottom: 10px" class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <input id="login-username" type="text" class="form-control" name="username" value="" placeholder="Tên đăng nhập">
                    </div>

                    <div style="margin-bottom: 10px" class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                        <input id="login-password" type="password" class="form-control" name="password" placeholder="Mật khẩu" value="">
                    </div>
                   
                    <a id="btn-login"  class="btn btn-success col-sm-12">Login  </a>
                   
                </div>
            </div>
        </div>
         <script>
             function check() {
                 var id = $('#login-username').val();
                 var pass = $('#login-password').val();
                 if (id == "" || pass == "") {
                     swal({
                         title: "Lỗi Dữ Liệu",
                         text: "Nhập thiếu trường dữ liệu!!!!",
                         type: "error",
                         timer: 1000,
                         showConfirmButton: false
                     });
                     return false;
                 }
                 else {
                     return true;
                 }
             };

             $(document).ready(function () {
                 $(document).keyup(function (event) {
                     if (event.keyCode == 13) {
                         $("#btn-login").click();
                     }
                 });


                 $("#btn-login").click(function () {
                     var id = $("#login-username").val();
                     var pass = $('#login-password').val();

                     var isCheck = check();
                     if (!isCheck) {
                         return false;
                     }
                     var requestData = {
                         idApprove: id,
                         passApprove: pass
                     };
                     var szRequest = JSON.stringify(requestData);
                     $.ajax({
                         type: "POST",
                         url: "Login.aspx/dangnhap",
                         data: szRequest,
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (result) {
                             if (result.d) {
                                 window.location.replace("/index.aspx");
                             }
                             else {
                                 swal({
                                     title: "Lỗi",
                                     text: "Sai tên đăng nhập hoặc mật khẩu, vui lòng kiểm tra lại",
                                     type: "error",
                                     timer: 1000,
                                     showConfirmButton: false
                                 });
                             }
                         },
                         error: function (msg) { alert(msg.d); }
                     });
                 });
             });
            </script>
        </form>
    </div>
</body>
</html>
