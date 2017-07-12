$(document).ready(function () {
    $("#userprof").click(function () {
        alert("hhhh");
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: 'Home/ADV_post',
            data: {},
            success: function (data) {
                for (var x = 0; x < data.length; x++) {
                    console.log("id:" + data[x].LogginId + "nme:" + data[x].Firstname + "snme:" + data[x].Surname);
                    var html = '<img src="' + data[x].Profilepic + '" style="height:35px; margin-left:2px; "/> <label style="margin-left:5px;">' + data[x].Firstname + '</label> <label>' + data[x].Surname + '</label><input type="button" style= " margin-left:15px;"  id="View" value="View Profile" data-id="' + data[x].LogginId + '" ><br/><hr>'
                    $(".PF").append(html);
                }
                $(".PF").show();

            }
        })
    });
});