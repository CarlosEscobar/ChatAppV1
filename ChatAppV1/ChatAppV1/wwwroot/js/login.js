function goToRegister() {
    window.location = viewRoutes.register;
}

function clearLoginValues() {
    $('#loginUsername').val("");
    $('#loginPassword').val("");
}

function onLoginSuccess(data) {
    window.location = viewRoutes.chat + "?user=" + data; 
    clearLoginValues();
}

function onLoginError(request, error) {
    alert("ERROR. [" + JSON.stringify(request) + "]");
    clearLoginValues();
}

function doLogin() {
    var userModel = {
        UserName: $('#loginUsername').val(),
        Password: $('#loginPassword').val(),
    };
    $.ajax({
        url: apiRoutes.login,
        type: htmlVerbs.post,
        contentType: contentTypes.json,
        data: JSON.stringify(userModel),
        success: onLoginSuccess,
        error: onLoginError
    });
}