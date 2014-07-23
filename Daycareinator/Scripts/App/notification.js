//jQuery.fn.notification = function (notification) {
//    $(this).hide().html("").html("<button type='button' class='close' data-dismiss='alert'>&times;</button>" + notification.Message);
//    if (!notification.IsValid) {
//        $(this).addClass("alert alert-error");
//    }
//    else if (notification.IsValid) {
//        $(this).addClass("alert alert-success");
//    }

//    $(this).fadeIn();
//}

var notification = (function () {

    toastr.options = {
        "debug": false,
        "positionClass": "toast-bottom-right",
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 1000,
        "timeOut": 5000,
        "extendedTimeOut": 1000
    };

    return {
        show: function (result) {
            if (!result.IsValid) {
                toastr.error(result.Message);
            }
            else if (result.IsValid) {
                toastr.success(result.Message);
            }
        }
    }
}())