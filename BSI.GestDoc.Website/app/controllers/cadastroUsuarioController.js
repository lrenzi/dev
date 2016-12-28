
'use strict';
app.controller("cadastroUsuarioController", ["$scope", "$routeParams", "$location", "cadastroUsuarioService", "utilService", function ($scope, $routeParams, $location, cadastroUsuarioService, utilService) {

    $scope.userNameUsuario = '';
    $scope.nomeUsuario = '';
    $scope.emailUsuario = '';
    $scope.senhaUsuario = '';
    $scope.perfilUsuario = '';
    $scope.showMessage = false;
    
    utilService.defirniTitulos('Cadastrar Usuário', '');

    $scope.CadastrarUsuario = function (userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario) {
        cadastroUsuarioService.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario).then(function (data) {
            $scope.retornoCadastro = data;
            
            if ($scope.retornoCadastro != '' && $scope.retornoCadastro != null) {
                $scope.showMessage = true;
            } else {
                $scope.showMessage = false;
            }

        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    }

    $scope.ConsultaPerfil = function (usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao) {
        
        $scope.listaPerfis = cadastroUsuarioService.ConsultaPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao).then(function (data) {
            $scope.listaPerfis = data;            
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });

    }

}]);
