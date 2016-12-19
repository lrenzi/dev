'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    
    $scope.logOut = function () {
        alert("aqui");
        authService.logOut();
        $location.path('/login');
    }

    $scope.authentication = authService.authentication;

}]);