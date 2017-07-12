$(document).ready(function () {
    $("#editing").click(function () {
        alert("hhhh");
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: 'Home/Views',
            data: {},
            success: function (data) {

                for (var x = 0; x < data.length; x++) {
                    console.log("id:" + data[x].LogginId + "nme:" + data[x].Firstname + "snme:" + data[x].Surname);
                    var html = '<img src="' + data[x].Profilepic + '" style="height:35px; margin-left:2px; "/> <label>' + data[x].Firstname + '</label> <label>' + data[x].Surname + '</label><br><label>' + data[x].Email + '</label><br><label>day:-' + data[x].day + '</label><br><label>Month:-' + data[x].month + '</label><br><label>Year:-' + data[x].Year + '</label><br><label>Gender:-' + data[x].Gender + '</label><input type="button" style="margin-left:15px; background-color:green" value="Delete" id="used"/><input type="button" value="suspend" id="btn_spnd" data-id="' + data[x].LogginId + '"/> <br/><hr>'
                    $(".PF").append(html);
                }
                //  $(".PF").show();

            }
        })
    });
});