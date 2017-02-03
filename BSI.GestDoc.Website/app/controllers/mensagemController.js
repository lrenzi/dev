'use strict';
app.controller('mensagemController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    $scope.chamaFunction = function (functionName) {
        $scope.$broadcast(functionName);
    };
}]);