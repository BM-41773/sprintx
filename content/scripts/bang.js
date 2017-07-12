$(document).on('click', '#btn_dlt', function () {
    var loginId = $(this).data("id");
    var mail = $("#email").val();
    // $("#View").click(function () {
    // var serchedname1 = $("#X_mainloginid").val();
    // $(".PF").hide();
    alert(loginId);
    alert(mail);
    $.ajax({
        type: 'GET',
        dataType: 'Json',
        url: 'Home/DELT',
        data: { X_loginId: loginId, email: mail },
        success: function (data) {
        }

    });
});

