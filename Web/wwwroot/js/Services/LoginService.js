app.factory("LoginService", ['$http', function ($http) {
    return {
        login: function (userName, password) {
            var data = { userName: userName, password: password };

            var config = {
                headers: {
                    'Content-Type': 'application/json;charset=utf-8;'
                }
            };

            return $http.post("/Account/Login", JSON.stringify(data), config);
        }
    }
}]);