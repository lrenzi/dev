'use strict';
app.controller("LoginController", ["$scope", "$routeParams", "$location", "LoginService", function ($scope, $routeParams, $location, LoginService) {
    alert("nois1")
    $scope.EfetuarLogin = function () {
        $scope.retornoLogin = LoginService.EfetuarLogin().then(function (data) {
            $scope.retornoLogin = data;
        });
    };
}]);