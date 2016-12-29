
'use strict';
app.controller("usuarioController", ["$scope", "$routeParams", "$location", "usuarioService", "localStorageService", "utilService", function ($scope, $routeParams, $location, usuarioService, localStorageService, utilService) {

    $scope.userNameUsuario = '';
    $scope.nomeUsuario = '';
    $scope.emailUsuario = '';
    $scope.senhaUsuario = '';
    $scope.perfilUsuario = '';
    $scope.showMessage = false;
    $scope.showDivAlteracao = false;
    $scope.showDivSenha = false;

    utilService.defirniTitulos('Consulta de Usuários', 'Informe abaixo o valor que deseja consultar');
    //utilService.defirniTitulos("Cadastrar Usuário", "Informe abaixo os dados para criação do novo usuáio");

    $scope.CadastrarUsuario = function (userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario) {

        if ($scope.userNameUsuario == undefined || $scope.userNameUsuario == ""
            || $scope.nomeUsuario == undefined || $scope.nomeUsuario == ""
            || $scope.emailUsuario == undefined || $scope.emailUsuario == ""
            || $scope.perfilUsuario == undefined || $scope.perfilUsuario == ""
            || $scope.senhaUsuario == undefined || $scope.senhaUsuario == ""
            || $scope.usuarioSenhaConfirmacao == undefined || $scope.usuarioSenhaConfirmacao == "") {

            return
        }
        
        if ($scope.usuarioSenhaConfirmacao != $scope.senhaUsuario) {
            utilService.mensagemAlerta("A senha não confere, favor informar novamente!");
            return
        }


        usuarioService.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario).then(function (data) {

            $scope.retornoCadastro = data;

            utilService.mensagemSucesso("Cadastro efetuado com sucesso!");

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

        $scope.listaPerfis = usuarioService.ConsultaPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao).then(function (data) {
            $scope.listaPerfis = data;
        }, function (error) {
            alert(error.data.message); utilService.mensagemErro(error.data.message);
        });
    }

    $scope.ConsultarUsuario = function (usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId) {

        $scope.showDivAlteracao = false;

        $scope.listaUsuarios = usuarioService.ConsultarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId).then(function (data) {
            $scope.listaUsuarios = data;

        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    }

    $scope.AlterarUsuario = function () {

        if ($scope.usuarioNome == "" || $scope.usuarioNome == undefined
            || $scope.usuarioEmail == "" || $scope.usuarioEmail == undefined
            || $scope.usuPerfilId == "" || $scope.usuPerfilId == undefined) {
            return
        }

        if ($scope.showDivSenha) {

            if ($scope.senhaUsuario == undefined || $scope.senhaUsuario == "" || $scope.confirmacaoSenhaUsuario == undefined || $scope.confirmacaoSenhaUsuario == "") {
                return
            }

            if ($scope.senhaUsuario != $scope.confirmacaoSenhaUsuario) {
                utilService.mensagemAlerta("A senha informada não confere, favor informar novamente!");
                $scope.showMessage = false;
                return
            } else {
                $scope.usuarioSenha = $scope.senhaUsuario;
            }
        } else {
            $scope.usuarioSenha = "";
        }

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
                                                                     $scope.ConsultarUsuario('', '', '', '', '', '', '', '')
                                                                     $scope.showDivSenha = false;
                                                                     $scope.senhaUsuario = ""
                                                                     $scope.confirmacaoSenhaUsuario = ""
                                                                     $scope.chkAlterarSenha = false
                                                                 }, function (error) {
                                                                     utilService.mensagemErro(error.data.message);
                                                                 });
    }

    $scope.AbrirTelaAlteracao = function (usuario) {
        var infClientes = localStorageService.get('ngAuthSettings');

        $scope.retornoUsuario = usuarioService.ConsultarUsuario(usuario.usuarioId, usuario.usuarioLogin, usuario.usuarioNome, usuario.usuarioEmail, usuario.usuarioAtivo, usuario.usuarioSenha, usuario.usuPerfilId, usuario.clienteId).then(function (data) {
            $scope.retornoUsuario = data;
            $scope.chkAlterarSenha = false;

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
                $scope.showDivSenha = false;
                $scope.$broadcast("focusTextInput");
            }

        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    }

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;
        $scope.reverse = !$scope.reverse;
    }

    $scope.AtivarUsuario = function (usuario) {

        usuario.usuarioAtivo = true; //ativa usuario

        $scope.retornoAtivacao = usuarioService.AlterarUsuario(usuario.usuarioId, usuario.usuarioLogin, usuario.usuarioNome, usuario.usuarioEmail, '', usuario.usuarioAtivo, usuario.usuPerfilId, usuario.clienteId).then(function (data) {

            $scope.retornoAlteracao = data;
            $scope.showMessage = true;
            $scope.showDivAlteracao = false;
            $scope.ConsultarUsuario('', '', '', '', '', '', '', '')
            //utilService.mensagemSucesso("Usuário ativado com sucesso!");
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    }


    $scope.InativarUsuario = function (usuario) {

        usuario.usuarioAtivo = false; //inativa usuario

        $scope.retornoAtivacao = usuarioService.AlterarUsuario(usuario.usuarioId, usuario.usuarioLogin, usuario.usuarioNome, usuario.usuarioEmail, '', usuario.usuarioAtivo, usuario.usuPerfilId, usuario.clienteId).then(function (data) {
            
            $scope.retornoAlteracao = data;

            $scope.showMessage = true;
            $scope.showDivAlteracao = false;

            $scope.ConsultarUsuario('', '', '', '', '', '', '', '')
           // utilService.mensagemSucesso("Usuário inativado com sucesso!");
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    }


    $scope.ExibirCampoSenha = function () {

        if ($scope.showDivSenha) {
            $scope.showDivSenha = false;
        } else {
            $scope.showDivSenha = true;
        }
    }


}]);
