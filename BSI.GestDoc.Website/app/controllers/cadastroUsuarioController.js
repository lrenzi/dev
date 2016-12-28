
'use strict';
app.controller("cadastroUsuarioController", ["$scope", "$routeParams", "$location", "cadastroUsuarioService", function ($scope, $routeParams, $location, cadastroUsuarioService) {

    $scope.userNameUsuario = '';
    $scope.nomeUsuario = '';
    $scope.emailUsuario = '';
    $scope.senhaUsuario = '';
    $scope.perfilUsuario = '';
    $scope.showMessage = false;
    
    $scope.CadastrarUsuario = function (userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario) {
        cadastroUsuarioService.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario).then(function (data) {
            $scope.retornoCadastro = data;
            
            if ($scope.retornoCadastro != '' && $scope.retornoCadastro != null) {
                $scope.showMessage = true;
            } else {
                $scope.showMessage = false;
            }

        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.ConsultaPerfil = function (usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao) {
        
        $scope.listaPerfis = cadastroUsuarioService.ConsultaPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao).then(function (data) {
            $scope.listaPerfis = data;            
        }, function (error) {
            alert(error.data.message);
        });

    }

}]);
