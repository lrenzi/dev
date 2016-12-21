
'use strict';
app.controller("listaPropostasController", ["$scope", "$routeParams", "$location", "listaPropostasService", 'ngAuthSettings', function ($scope, $routeParams, $location, listaPropostasService, ngAuthSettings) {
    $scope.ListarPropostas = function () {
        $scope.listaPropostas = listaPropostasService.ListarPropostas().then(function (data) {
            $scope.listaPropostas = data;
        }, function (error) {
            alert(error.data.message);
        });
    };
    
}]);
