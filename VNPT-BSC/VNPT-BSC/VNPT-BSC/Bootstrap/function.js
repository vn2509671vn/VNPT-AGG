function validateNumber(element) {
    $("#" + element).keydown(function () {
        $(this).keypress(function (e) {
            if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
        });
    });
}

function validateMonth(element) {
    var month = $("#" + element).val();
    if (month > 12 || month <= 0) {
        $("#" + element).css("border-color", "red");
        return false;
    }
    else {
        $("#" + element).css("border-color", "#ccc");
        return true;
    }
}

function validateYear(element) {
    var year = $("#" + element).val();
    if (year > 2100 || year < 2016) {
        $("#" + element).css("border-color", "red");
        return false;
    }
    else {
        $("#" + element).css("border-color", "#ccc");
        return true;
    }
}

function clearInputs() {
    $("input[type=text]").each(function(){
        $(this).val('');
    });

    $("input[type=checkbox]:checked").each(function () {
        $(this).attr("checked", false);
    });
}

function bodauTiengViet(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    return str;
}

// Export to excel for nghiệm thu 11 đơn vị huyện thị
function ExportToExcel(table, sheetName, month, year) {
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>';
    var base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) };
    var format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    if (!table.nodeType) table = document.getElementById(table);
    var ctx = { worksheet: sheetName || 'Worksheet', table: table.innerHTML };
    template += '<body>';

    // Phần header
    template += '<table>';
    template += '<thead>';

    template += '<tr>';
    template += '<th rowspan="2" colspan="2" style="text-align: center">TRUNG TÂM KINH DOANH VNPT - AN GIANG </th>';
    template += '<th></th><th></th>';
    template += '<th colspan="6" style="text-align: center">CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th></th><th></th>';
    template += '<th colspan="6" style="text-align: center">Độc lập - Tự do - Hạnh phúc </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="2" style="text-align: center"> ' + sheetName + ' </th>';
    template += '<th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="10" style="text-align: center"> BÁO CÁO KẾT QUẢ THỰC HIỆN CHỈ TIÊU BSC/KPI </th>';
    template += '</tr>';
    
    template += '<tr>';
    template += '<th colspan="10" style="text-align: center"> Tháng ' + month + ' ' + "/" + ' ' + year + '</th>';
    template += '</tr>';

    template += '<tr>';
    template += '</tr>';

    template += '</thead>';
    template += '</table>';

    // Phần nội dung
    template += '<table border="1">{table}</table>';

    // Phần ký tên
    template += '<table>';
    template += '<thead>';
    template += '<tr>';
    template += '<th></th><th></th><th></th><th></th><th></th><th></th>';
    template += '<th colspan="4" style="text-align: center">...., ngày .... tháng .... năm ' + year + '</th>';
    template += '</tr>';
    template += '<tr>';
    template += '<th></th>';
    template += '<th style="text-align: center">Người lập biểu</th>';
    template += '<th></th><th></th><th></th><th></th>';
    template += '<th colspan="4" style="text-align: center">Lãnh đạo đơn vị</th>';
    template += '</tr>';
    template += '</body></html>';
    //return window.location.href = uri + base64(format(template, ctx));
    var dataUri = uri + base64(format(template, ctx));
    return $("<a download='Nghiệm thu " + month + "-" + year + "-" + sheetName + "' href='" + dataUri + "'></a>")[0].click();
};

// Export to excel for bảng xếp hạng 11 đơn vị huyện thị
function ExportToExcelForRank(table, sheetName, month, year) {
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>';
    var base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) };
    var format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    if (!table.nodeType) table = document.getElementById(table);
    var ctx = { worksheet: sheetName || 'Worksheet', table: table.innerHTML };
    template += '<body>';

    // Phần header
    template += '<table>';
    template += '<thead>';

    template += '<tr>';
    template += '<th rowspan="2" colspan="2" style="text-align: center">TỔNG CÔNG TY DỊCH VỤ VIỄN THÔNG  TRUNG TÂM KINH DOANH VNPT - AN GIANG </th>';
    template += '<th colspan="3" style="text-align: center">CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="3" style="text-align: center">Độc lập - Tự do - Hạnh phúc </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="2" style="text-align: center"> Số:   /KQ-TTKD AGG-KHKT </th>';
    template += '<th></th><th></th><th></th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="5" style="text-align: center"> KẾT QUẢ THẨM ĐỊNH </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="5" style="text-align: center"> THỰC HIỆN CHỈ TIÊU KẾ HOẠCH BSC/KPI THÁNG ' + month + ' ' + "/" + ' ' + year + '</th>';
    template += '</tr>';

    template += '<tr>';
    template += '</tr>';

    template += '</thead>';
    template += '</table>';

    // Phần nội dung
    template += '<table border="1">{table}</table>';

    // Phần ký tên
    template += '<table>';
    template += '<thead>';
    template += '<tr></tr>';
    template += '<tr>';
    template += '<th></th><th></th>';
    template += '<th colspan="3" style="text-align: center">An Giang, ngày .... tháng .... năm ' + year + '</th>';
    template += '</tr>';
    template += '<tr>';
    template += '<th></th>';
    template += '<th colspan="2" style="text-align: center">Lập bảng</th>';
    template += '<th colspan="2" style="text-align: center">GIÁM ĐỐC</th>';
    template += '</tr>';
    template += '<th><tr></tr></th>';
    template += '<th>';
    template += '<tr><td>Nơi nhận</td></tr>';
    template += '<tr><td>- GĐ VTAG;</td></tr>';
    template += '<tr><td>- Ban GĐ TTKD;</td></tr>';
    template += '<tr><td>- Các đơn vị trực thuộc;</td></tr>';
    template += '<tr><td>- Lưu: VT.</td></tr>';
    template += '</th>';
    template += '</body></html>';
    //return window.location.href = uri + base64(format(template, ctx));
    var dataUri = uri + base64(format(template, ctx));
    return $("<a download='Điểm xếp hạng " + month + "-" + year + "' href='" + dataUri + "'></a>")[0].click();
};

// Export to excel for bảng xếp hạng 11 đơn vị huyện thị
function ExportTableToExcel(table, sheetName) {
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>';
    var base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) };
    var format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    if (!table.nodeType) table = document.getElementById(table);
    var ctx = { worksheet: sheetName || 'Worksheet', table: table.innerHTML };
    template += '<body>';
    // Phần nội dung
    template += '<table border="1">{table}</table>';
    template += '</body></html>';

    var dataUri = uri + base64(format(template, ctx));
    //return window.location.href = uri + base64(format(template, ctx));
    return $("<a download='" + sheetName + "' href='" + dataUri + "'></a>")[0].click();
};

function Export_NghiemThuBSCNV_ToExcel(table, nhanviennhan_ma, nhanviennhan_ten, tendonvi, thang, nam, loaimaubsc) {
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>';
    var base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) };
    var format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    if (!table.nodeType) table = document.getElementById(table);
    var ctx = { worksheet: nhanviennhan_ten || 'Worksheet', table: table.innerHTML };
    template += '<body>';

    // Phần header
    template += '<table>';
    template += '<thead>';

    template += '<tr>';
    template += '<th colspan="10" style="text-align: center;">BẢNG TỔNG HỢP GIAO VÀ KẾT QUẢ KPI THÁNG ' + thang + '/' + nam + '</th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="10" style="text-align: center;">ĐƠN VỊ: ' + tendonvi.toUpperCase() + '</th>';
    template += '</tr>';

    template += '<tr>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="10" style="text-align: left;">Họ và tên: ' + nhanviennhan_ten + ' </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="10" style="text-align: left;"> Mã nhân viên: ' + nhanviennhan_ma + ' </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="10" style="text-align: left;"> Chức danh: ' + loaimaubsc + ' </th>';
    template += '</tr>';

    template += '<tr>';
    template += '</tr>';

    template += '</thead>';
    template += '</table>';

    // Phần nội dung
    template += '<table border="1">{table}</table>';

    // Phần ký tên
    template += '<table>';
    template += '<thead>';
    template += '<tr></tr>';
    template += '<tr>';
    template += '<th colspan="10" style="text-align: right;">An Giang, ngày .... tháng .... năm ' + nam + '</th>';
    template += '</tr>';
    template += '<tr>';
    template += '<th colspan="2" style="text-align: center">KÝ XÁC NHẬN KẾT QUẢ ĐẠT</th>';
    template += '<th></th>';
    template += '<th></th>';
    template += '<th colspan="6" style="text-align: center">LÃNH ĐẠO ĐƠN VỊ</th>';
    template += '</tr>';
    template += '<tr>';
    template += '<th colspan="2" style="text-align: center">ĐƯỢC CỦA CÁ NHÂN</th>';
    template += '<th></th>';
    template += '<th></th>';
    template += '<th colspan="6" style="text-align: center">KÝ XÁC NHẬN KẾT QUẢ</th>';
    template += '</tr>';
    template += '</thead>';

    template += '</body></html>';
    var dataUri = uri + base64(format(template, ctx));
    //return window.location.href = uri + base64(format(template, ctx));
    return $("<a download='Nghiệm Thu BSC " + thang + "-" + thang + "-" + nhanviennhan_ten + "' href='" + dataUri + "'></a>")[0].click();
}

// Export to excel danh sách chi tiết CTV PTTB
function ExportToExcel_CTV_PTTB_TongHop(table, sheetName, month, year) {
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>';
    var base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) };
    var format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    if (!table.nodeType) table = document.getElementById(table);
    var ctx = { worksheet: sheetName || 'Worksheet', table: table.innerHTML };
    template += '<body>';

    // Phần header
    template += '<table>';
    template += '<thead>';

    template += '<tr>';
    template += '<th rowspan="4" colspan="3" style="text-align: center">TỔNG CÔNG TY<BR> DỊCH VỤ VIỄN THÔNG<BR> TRUNG TÂM<BR> KINH DOANH VNPT - AN GIANG </th>';
    template += '<th></th>';
    template += '<th colspan="3" style="text-align: center">CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th></th>';
    template += '<th colspan="3" style="text-align: center">Độc lập - Tự do - Hạnh phúc </th>';
    template += '</tr>';

    template += '<tr></tr><tr></tr>';

    template += '<tr>';
    template += '<th colspan="7" style="text-align: center"><h2>DANH SÁCH TỔNG HỢP CTV PHÁT TRIỂN TB TRẢ TRƯỚC<h2></th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="7" style="text-align: center">PHÒNG BÁN HÀNG ' + sheetName.toUpperCase() + '</th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="7" style="text-align: center"> Tháng ' + month + ' ' + "/" + ' ' + year + '</th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="7" style="text-align: center"><i>(Ban hành kèm theo số CV ....../TTKD AGG-ĐHNV của Giám đốc Trung tâm kinh doanh VNPT An Giang)</i></th>';
    template += '</tr>';

    template += '</thead>';
    template += '</table>';

    // Phần nội dung
    template += '<table>{table}</table>';

    // Phần ký tên
    template += '<table>';
    template += '<thead>';
    template += '<tr></tr>';
    template += '<tr>';
    template += '<th></th>';
    template += '<th style="text-align: center">LẬP BIỂU</th>';
    template += '<th></th><th></th><th></th>';
    template += '<th style="text-align: center">GIÁM ĐỐC</th>';
    template += '</tr>';
    template += '</body></html>';
    //return window.location.href = uri + base64(format(template, ctx));
    var dataUri = uri + base64(format(template, ctx));
    return $("<a download='Tổng Hợp CTV PTTB TT " + month + "-" + year + "-" + sheetName + "' href='" + dataUri + "'></a>")[0].click();
};

// Export to excel danh sách chi tiết CTV PTTB
function ExportToExcel_CTV_PTTB_ChiTiet(table, sheetName, month, year) {
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>';
    var base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) };
    var format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    if (!table.nodeType) table = document.getElementById(table);
    var ctx = { worksheet: sheetName || 'Worksheet', table: table.innerHTML };
    template += '<body>';

    // Phần header
    template += '<table>';
    template += '<thead>';

    template += '<tr>';
    template += '<th rowspan="4" colspan="3" style="text-align: center">TỔNG CÔNG TY<BR> DỊCH VỤ VIỄN THÔNG<BR> TRUNG TÂM<BR> KINH DOANH VNPT - AN GIANG </th>';
    template += '<th></th><th></th>';
    template += '<th colspan="3" style="text-align: center">CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM </th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th></th><th></th>';
    template += '<th colspan="3" style="text-align: center">Độc lập - Tự do - Hạnh phúc </th>';
    template += '</tr>';

    template += '<tr></tr><tr></tr>';

    template += '<tr>';
    template += '<th colspan="8" style="text-align: center"><h2>DANH SÁCH CHI TIẾT CTV PHÁT TRIỂN TB TRẢ TRƯỚC<h2></th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="8" style="text-align: center">PHÒNG BÁN HÀNG ' + sheetName.toUpperCase() + '</th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="8" style="text-align: center"> Tháng ' + month + ' ' + "/" + ' ' + year + '</th>';
    template += '</tr>';

    template += '<tr>';
    template += '<th colspan="8" style="text-align: center"><i>(Ban hành kèm theo số CV ....../TTKD AGG-ĐHNV của Giám đốc Trung tâm kinh doanh VNPT An Giang)</i></th>';
    template += '</tr>';

    template += '</thead>';
    template += '</table>';

    // Phần nội dung
    template += '<table border="1">{table}</table>';

    // Phần ký tên
    template += '<table>';
    template += '<thead>';
    template += '<tr></tr>';
    template += '<tr>';
    template += '<th></th>';
    template += '<th style="text-align: center">LẬP BIỂU</th>';
    template += '<th></th><th></th><th></th><th></th><th></th>';
    template += '<th style="text-align: center">GIÁM ĐỐC</th>';
    template += '</tr>';
    template += '</body></html>';
    //return window.location.href = uri + base64(format(template, ctx));
    var dataUri = uri + base64(format(template, ctx));
    return $("<a download='Chi Tiết CTV PTTB TT " + month + "-" + year + "-" + sheetName + "' href='" + dataUri + "'></a>")[0].click();
};

// Link: http://stackoverflow.com/questions/14446511/what-is-the-most-efficient-method-to-groupby-on-a-javascript-array-of-objects
function groupBy(array, col, col2, value) {
    var r = [], o = {};
    array.forEach(function (a) {
        if (!o[a[col]]) {
            o[a[col]] = {};
            o[a[col]][col] = a[col];
            o[a[col]][col2] = a[col2];
            o[a[col]][value] = 0;
            r.push(o[a[col]]);
        }
        o[a[col]][value] += +a[value];
    });
    return r;
};

function groupByThreeCol(array, col, col2, col3, value) {
    var r = [], o = {};
    array.forEach(function (a) {
        if (!o[a[col]]) {
            o[a[col]] = {};
            o[a[col]][col] = a[col];
            o[a[col]][col2] = a[col2];
            o[a[col]][col3] = a[col3];
            o[a[col]][value] = 0;
            r.push(o[a[col]]);
        }
        o[a[col]][value] += +a[value];
    });
    return r;
};