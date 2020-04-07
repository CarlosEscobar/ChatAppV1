function getUrlParameter(name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    return decodeURI(results[1]) || 0;
}

function onLogoutSuccess(data) {
    window.location = viewRoutes.login;
}

function onAjaxError(request, error) {
    alert("ERROR. [" + JSON.stringify(request) + "]");
}

function doLogout() {
    $.ajax({
        url: apiRoutes.logout,
        type: htmlVerbs.post,
        contentType: contentTypes.json,
        data: JSON.stringify({}),
        success: onLogoutSuccess,
        error: onAjaxError
    });
}

function getMessagesSuccess(data) {
    $('#messages').empty();
    var currentHtml, currentPos;
    for (var i = 0; i < data.length; i++) {
        currentPos = data.length - i - 1;
        currentHtml = '<div>' + data[currentPos].owner + ' : ' + data[currentPos].content + '<br/>' + data[currentPos].timestamp + '</div><hr>';
        $('#messages').append(currentHtml);
    }
}

function getMessages() {
    $.ajax({
        url: apiRoutes.messages,
        type: htmlVerbs.get,
        contentType: contentTypes.json,
        data: JSON.stringify({}),
        success: getMessagesSuccess,
        error: onAjaxError
    });
}

function sendMessage(connection) {
    var messageInput = $('#chatMessage');
    var message = messageInput.val().trim();
    if (message.length == 0) {
        return false;
    }

    var newMessage = {
        SocketId: connection.connectionId,
        Owner: getUrlParameter('user'),
        Content: message
    };
    $.ajax({
        url: apiRoutes.messages,
        type: htmlVerbs.post,
        contentType: contentTypes.json,
        data: JSON.stringify(newMessage),
        success: function (data) {
            connection.invoke("TriggerClients");
            messageInput.val("");
        },
        error: onAjaxError
    });
}

function initializeChatConnection() {
    var connection = new WebSocketManager.Connection("ws://localhost:5000/chatapp");
    connection.enableLogging = true;

    connection.connectionMethods.onConnected = () => {}

    connection.connectionMethods.onDisconnected = () => {}

    connection.clientMethods["clientCallback"] = () => {
        getMessages();
    }

    connection.start();

    $('#chatMessage').keyup(function (e) {
        if (e.keyCode == 13) {
            sendMessage(connection);
        }
    });
}

$(function() {
    $('#chatUser').text("Hello " + getUrlParameter('user') + "!");
    getMessages();
    initializeChatConnection();
});