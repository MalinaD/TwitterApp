$(document).ready(function () {
    //TOTO calls
    function UserOrEmailAvailability() { //This function call on text change.             
        $.ajax({
            type: "POST",
            url: "Account/Register", // this for calling the web method function in cs code.  
            data: '{username: "' + $("#username")[0].value + '" }',// username  value  
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response);
            }
        });
    }

    // function OnSuccess  
    function OnSuccess(response) {
        var msg = $("#username")[0];
        switch (response.d) {
            case "true":
                msg.style.display = "block";
                msg.style.color = "red";
                msg.innerHTML = "Sorry, username already exists.";
                break;
            case "false":
                msg.style.display = "block";
                msg.style.color = "green";
                msg.innerHTML = "Username is Available";
                break;
        }
    }
});