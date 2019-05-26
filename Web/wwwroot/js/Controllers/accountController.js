app.controller('AccountController', ['$scope', '$rootScope', '$timeout', 'LoginService',
    function ($scope, $rootScope, $timeout, LoginService) {
        $scope.userName = '';
        $scope.password = '';
        $scope.errorMessage = '';

        this.$onInit = function () {
            
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
                            $scope.errorMessage = 'Not valid credentials';
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

        $scope.defaultSubmit = function (e) {
            if (e.keyCode == 13) {
                var elementScope = angular.element(document.getElementById('loginForm')).scope();
                if (!elementScope.loginForm.$invalid) {
                    $scope.login();
                }
            }
        }
    }
]);

