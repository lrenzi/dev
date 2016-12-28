
'use strict';
app.controller("usuarioController", ["$scope", "$routeParams", "$location", "usuarioService", "localStorageService", function ($scope, $routeParams, $location, usuarioService, localStorageService) {

    $scope.userNameUsuario = '';
    $scope.nomeUsuario = '';
    $scope.emailUsuario = '';
    $scope.senhaUsuario = '';
    $scope.perfilUsuario = '';
    $scope.showMessage = false;
    $scope.showDivAlteracao = false;

    $scope.CadastrarUsuario = function (userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario) {
        usuarioService.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario).then(function (data) {
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

        $scope.listaPerfis = usuarioService.ConsultaPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao).then(function (data) {
            $scope.listaPerfis = data;
        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.ConsultarUsuario = function (usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId) {

        $scope.showDivAlteracao = false;

        $scope.listaUsuarios = usuarioService.ConsultarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId).then(function (data) {
            $scope.listaUsuarios = data;
        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.AlterarUsuario = function () {
        
        $scope.retornoAlteracao = usuarioService.AlterarUsuario($scope.usuarioId,
                                                                $scope.usuarioLogin,
                                                                $scope.usuarioNome,
                                                                $scope.usuarioEmail,
                                                                $scope.usuarioSenha,
                                                                $scope.usuarioAtivo,
                                                                $scope.usuPerfilId,
                                                                 $scope.clienteId).then(function (data) {

            $scope.retornoAlteracao = data;

            $scope.showMessage = true;
            $scope.showDivAlteracao = false;

            $scope.ConsultarUsuario('','','','','','','','')

        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.AbrirTelaAlteracao = function (usuario) {
        var infClientes = localStorageService.get('ngAuthSettings');


        $scope.retornoUsuario = usuarioService.ConsultarUsuario(usuario.usuarioId, usuario.usuarioLogin, usuario.usuarioNome, usuario.usuarioEmail, usuario.usuarioAtivo, usuario.usuarioSenha, usuario.usuPerfilId, usuario.clienteId).then(function (data) {
            $scope.retornoUsuario = data;

            if ($scope.retornoUsuario != null && $scope.retornoUsuario.length > 0) {
                $scope.usuarioLogin = $scope.retornoUsuario[0].usuarioLogin;
                $scope.usuarioId = $scope.retornoUsuario[0].usuarioId;
                $scope.usuarioNome = $scope.retornoUsuario[0].usuarioNome;
                $scope.usuarioEmail = $scope.retornoUsuario[0].usuarioEmail;
                $scope.usuarioSenha = $scope.retornoUsuario[0].usuarioSenha;
                $scope.usuarioAtivo = $scope.retornoUsuario[0].usuarioAtivo;
                $scope.usuPerfilId = $scope.retornoUsuario[0].usuPerfilId;
                $scope.clienteId = $scope.retornoUsuario[0].clienteId;
                $scope.showDivAlteracao = true;
            }


        }, function (error) {
            alert(error.data.message);
        });
    }

}]);
