function validateNumber(element) {
    $("#" + element).change(function () {
        $(this).keypress(function (e) {
            if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
        });
    });
}