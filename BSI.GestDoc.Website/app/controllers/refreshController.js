'use strict';
app.controller('refreshController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    debugger;
    $scope.authentication = authService.authentication;
    $scope.tokenRefreshed = false;
    $scope.tokenResponse = null;

    $scope.refreshToken = function () {

        authService.refreshToken().then(function (response) {
            $scope.tokenRefreshed = true;
            $scope.tokenResponse = response;
        },
         function (err) {
             $location.path('/login');
         });
    };

}]);