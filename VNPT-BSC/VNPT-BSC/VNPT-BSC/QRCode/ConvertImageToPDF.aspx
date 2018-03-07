<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvertImageToPDF.aspx.cs" Inherits="VNPT_BSC.QRCode.ConvertImageToPDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chuyển Danh Sách Hình Sang PDF</title>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="../Bootstrap/jspdf.debug.js"></script>
    <script src="../Bootstrap/function.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="text-align:center">
                <h3 class="panel-title" style="text-align:center">Chuyển Danh Sách Hình Sang PDF</h3>
            </div>
            <div class="panel-body" style="margin-left: 30%">
                <div style="margin-bottom: 10px" class="input-group">
                    <input id="name" type="text" style="width:400px" placeholder="Nhập tên file cần lưu"/>
                </div>
                <div style="margin-bottom: 10px" class="input-group">
                    <label>Hình ảnh:</label>
                    <input type="file" multiple accept="image/*" id="uploadFile"/>
                </div>
                <br />
                <br />
                <button id="btnChuyenDoi" style="background-color:orange">Chuyển đổi</button>
                <br />
                <br />
                <div id="files-list"></div>
                <br />
                <br />
                <a href="../index.aspx">Trở về trang chủ</a>
            </div>
            <div class="text-center">

            </div>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript">
    var tmpArr = new Array();
    function readImage(file) {
        var reader = new FileReader();

        // Once a file is successfully readed:
        reader.addEventListener("load", function (e) {
            tmpArr.push({
                'name': file.name,
                'url': e.target.result
            });
        });

        reader.readAsDataURL(file);
    }

    $(document).ready(function () {
        $("#uploadFile").change(function () {
            $("#btnChuyenDoi").prop('disabled', true);
            tmpArr = [];
            var cnt = 1;
            var files = this.files;
            if (files && files[0]) {
                // Iterate over every File object in the FileList array
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    readImage(file);
                }
                setTimeout(function () { $("#btnChuyenDoi").prop('disabled', false); }, 500 * files.length);
            }
        });

        $("#form1").submit(function () {
            var doc = new jsPDF();
            doc.setFontSize(40);
            tmpArr.sort(dynamicSort("name"));
            for (i = 0; i < tmpArr.length; i++) {
                doc.addImage(tmpArr[i].url, 'JPEG', 15, 40, 180, 180);
                if (i < tmpArr.length - 1) {
                    doc.addPage();
                }
            }
            doc.save($("#name").val() + '.pdf');
            window.location.reload();
            return false;
        });
    });
</script>
</html>
