
'use strict';
app.controller("cadastrarUsuarioController", ["$scope", "$routeParams", "$location", "cadastrarUsuarioService", function ($scope, $routeParams, $location, cadastrarUsuarioService) {
    
    $scope.CadastrarUsuario = cadastrarUsuarioService.CadastrarUsuario().then(function (data) {
        $scope.CadastrarUsuario = data;        
    }, function (error) {
        alert(error.data.message);
    });

}]);
