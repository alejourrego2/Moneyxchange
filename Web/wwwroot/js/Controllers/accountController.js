app.controller('AccountController', ['$scope', '$rootScope', '$timeout', 'LoginService',
    function ($scope, $rootScope, $timeout, LoginService) {
        $scope.userName = '';
        $scope.password = '';
        $scope.errorMessage = '';

        this.$onInit = function () {
            document.getElementById("userName").keypress(function (e) {
                if (e.keyCode == 13)
                    document.getElementById(('#login')).click();
            });
        }

        $scope.reset = function () {
            $scope.userName = '';
            $scope.password = '';
            $scope.errorMessage = '';
        }

        $scope.login = function () {
            $scope.errorMessage = '';
            LoginService.login($scope.userName, $scope.password)
                .then(function (response) {
                    if (response.data) {
                        if (response.data.authenticated) {
                            window.location = response.data.urlToRedirect;
                        }
                        else {
                            $scope.errorMessage = 'User name and/or password are not valid.';
                        }
                    }
                }, function (error) {
                    console.log(error);
                });
        }

        $scope.change = function () {
            if ($scope.errorMessage && $scope.errorMessage.length) {
                $scope.errorMessage = '';
            }
        }
    }
]);

