function goToLogin() {
    window.location = viewRoutes.login;
}

function clearRegisterValues() {
    $('#registerUsername').val("");
    $('#registerPassword').val("");
    $('#registerEmail').val("");
    $('#registerPhoneNumber').val("");
}

function onRegisterSuccess(data) {
    alert(data);
    clearRegisterValues();
}

function onRegisterError(request, error) {
    alert("ERROR. [" + JSON.stringify(request) + "]");
    clearRegisterValues();
}

function doRegister() {
    var registerModel = {
        UserName: $('#registerUsername').val(),
        Password: $('#registerPassword').val(),
        Email: $('#registerEmail').val(),
        PhoneNumber: $('#registerPhoneNumber').val()
    };
    $.ajax({
        url: apiRoutes.register,
        type: htmlVerbs.post,
        contentType: contentTypes.json,
        data: JSON.stringify(registerModel),
        success: onRegisterSuccess,
        error: onRegisterError
    });
}