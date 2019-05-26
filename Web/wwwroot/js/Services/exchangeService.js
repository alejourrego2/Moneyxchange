app.factory("ExchangeService", ['$http', function ($http) {
    return {
        exchangeCurrency: function (value, fromCurrency, targetCurrency) {
            var data = { value: value, fromCurrency: fromCurrency, targetCurrency: targetCurrency };

            var config = {
                headers: {
                    'Content-Type': 'application/json;charset=utf-8;'
                }
            };

            return $http.post("/Exchange/ExchangeCurrency", JSON.stringify(data), config);
        }
    }
}]);