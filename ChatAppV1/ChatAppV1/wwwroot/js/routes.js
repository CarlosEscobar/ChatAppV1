var baseUrl = 'http://localhost:5000/';

var apiRoutes = {
    register: baseUrl + 'api/Authentication/register',
    login: baseUrl + 'api/Authentication/login',
    logout: baseUrl + 'api/Authentication/logout',
    messages: baseUrl + 'api/Messages'
}

var viewRoutes = {
    register: baseUrl + 'Views/register.html',
    login: baseUrl + 'Views/login.html',
    chat: baseUrl + 'Views/chat.html'
}

var htmlVerbs = {
    get: 'GET',
    post: 'POST',
    put: 'PUT',
    delete: 'DELETE',
    options: 'OPTIONS'
}

var contentTypes = {
    form: 'application/x-www-form-urlencoded; charset=utf-8',
    json: 'application/json; charset=utf-8'
}