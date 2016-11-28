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
    if (year > 2100 || year < 1900) {
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