$(document).on('click', '#btn_spnd', function () {
    var loginId = $(this).data("id");
   
    alert("yyyy");
   // alert(mail);
    $.ajax({
        type: 'GET',
        dataType: 'Json',
        url: 'Home/SUS',
        data: { loginid: loginId },
        success: function (data) {
        }

    });
});