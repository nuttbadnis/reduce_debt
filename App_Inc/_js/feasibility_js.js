
function callDatePicker() {
    $('.dateselect').datepicker({
        format: 'dd/MM/yyyy'

    });
}

function AlertSuccess(header, callback) {
    bootbox.dialog({
        title: "<span style='font-size:25px;'>Complete</span>",
        message: "<div class='row'><div class='col-md-2 pr-0'><i class='fas fa-4x fa-check-circle text-success align-middle '></i></div><div class='col-md-10 pl-0'><span style='font-size: 18px;'>" + header + "</span></div></div>",
        className: "bootbox-success",
        buttons: {
            ok: {
                label: 'OK',
                className: 'btn-success',
                callback: function (confirmed) {
                    console.log("This was logged in the callback: " + confirmed);
                    console.log(confirmed);
                    if (callback) {
                        callback();
                    } 
                }
            }
        }
    });
    return false;
}

function AlertNotification(header, callback) {
    bootbox.dialog({
        title: "<span style='font-size:25px;'>Alert</span>",
        message: "<div class='row'><div class='col-md-2 pr-0'><i class='fas fa-4x fa-exclamation-circle text-warning align-middle'></i></div><div class='col-md-10 pl-0'><span style='font-size: 18px;'>" + header + "</span></div></div>",
        className: 'bootbox-warning',
        buttons: {
            ok: {
                label: 'OK',
                className: 'btn-warning text-white',
                callback: function (confirmed) {
                    console.log("This was logged in the callback: " + confirmed);
                    console.log(confirmed);
                    if (callback) {
                        callback();
                    } 
                }
            }
        }
    });
    return false;

}

function AlertError(header, callback) {
    bootbox.dialog({
        title: "<span style='font-size:25px;'>Error</span>",
        message: "<div class='row'><div class='col-md-2 pr-0'><i class='fas fa-4x fa-times-circle text-danger align-middle'></i></div><div class='col-md-10 pl-0'><span style='font-size: 18px;'>" + header + "</span></div></div>",
        className: 'bootbox-danger',
        buttons: {
            ok: {
                label: 'OK',
                className: 'btn-danger text-white',
                callback: function (confirmed) {
                    console.log("This was logged in the callback: " + confirmed);
                    console.log(confirmed);
                    if (callback) {
                        callback();
                    } else {
                        
                    }
                }
            }
        }
    });

}

function confirmDelete(sender,header) {
    //ต้องการลบข้อมูลอุปกรณ์นี้ใช่หรือไม่

    if ($(sender).attr("confirmed") == "true"){return true;}
    bootbox.confirm({
        title: "<span style='font-size:25px;'>Confirm</span>",
        message: "<div class='row'><div class='col-md-2 pr-0'><i class='fas fa-3x fa-exclamation-circle text-warning align-middle'></i></div><div class='col-md-10 pl-0'><span style='font-size: 18px;'>" + header + " ?</span></div></div>",
        className: 'bootbox-warning',
        buttons: {
            confirm: {
                label: "<i class='fas fa-check'> ตกลง",
                className: "btn-success"
            },
            cancel: {
                label: "<i class='fas fa-times'> ยกเลิก",
                className: "btn-danger"
            }
        },
        callback: function (confirmed) {
            console.log("This was logged in the callback: "+confirmed);
            if (confirmed === true) {
                //console.log("aa");
                $(sender).attr("confirmed", confirmed);
                sender.click();
            }else{
                //console.log("dd");
            }

        }
    });
    return false;
}

function ConfirmNotification() {
    //ต้องการลบข้อมูลอุปกรณ์นี้ใช่หรือไม่
    if ($(sender).attr("confirmed") == "true") { return true; }
    bootbox.confirm({
        title: "<span style='font-size:25px;'>Confirm</span>",
        message: "<div class='row'><div class='col-md-2 pr-0'><i class='fas fa-3x fa-exclamation-circle text-warning align-middle'></i></div><div class='col-md-10 pl-0'><span style='font-size: 18px;'>" + header + " ?</span></div></div>",
        className: 'bootbox-warning',
        buttons: {
            confirm: {
                label: "<i class='fas fa-check'> ตกลง",
                className: "btn-success"
            },
            cancel: {
                label: "<i class='fas fa-times'> ยกเลิก",
                className: "btn-danger"
            }
        },
        callback: function (confirmed) {
            return confirmed;
        }
    });
    return false;
}

