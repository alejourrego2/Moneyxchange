app.controller('ExchangeController', ['$scope', '$rootScope', '$timeout','ExchangeService',
    function ($scope, $rootScope, $timeout, ExchangeService) {
        $scope.euro = 0;
        $scope.usd = 0;

        this.$onInit = function () {

        }

        $scope.changeUSD = function () {
            if ($scope.euro && $scope.euro != 0) {
                $scope.euro = 0;
            }
        }

        $scope.exchange = function () {
            ExchangeService.exchangeCurrency($scope.usd, 'USD', 'EUR')
                .then(function (response) {
                    $scope.euro = response.data;
                }, function (error) {
                    console.log(error);
                });
        }
        $scope.reset = function () {
            $scope.euro = 0;
            $scope.usd = 0;
            $timeout(function () {
                var el = document.getElementById("usd");
                el.select();
                el.focus();
            }, 1);
        }
    }
]);

