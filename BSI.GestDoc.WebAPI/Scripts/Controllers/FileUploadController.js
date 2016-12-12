'use strict';
app.controller("FileUploadCtrl",["$scope", "$routeParams", "$location", "CargosService", function($scope, $routeParams, $location, CargosService){

    $scope.ConsultarCargos = function(){
        $scope.cargos = CargosService.ConsultarCargos().then(function(data){
            $scope.cargos = data;
        });
    };
}]);