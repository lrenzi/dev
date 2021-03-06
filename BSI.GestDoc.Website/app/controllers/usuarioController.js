﻿
'use strict';
app.controller("usuarioController", ['$uibModal',"$scope", "$routeParams", "$location", "usuarioService", "localStorageService", "utilService", '$anchorScroll', '$timeout', '$rootScope', function ($uibModal,$scope, $routeParams, $location, usuarioService, localStorageService, utilService, $anchorScroll, $timeout, $rootScope) {

    $scope.userNameUsuario = '';
    $scope.nomeUsuario = '';
    $scope.emailUsuario = '';
    $scope.senhaUsuario = '';
    $scope.perfilUsuario = '';
    $scope.showMessage = false;
    $scope.showDivAlteracao = false;
    $scope.showDivSenha = false;

    //verifica a rota para definir o titulo da página
    if ($location.$$path === "/consultarUsuario") {
        utilService.definirTitulos('Consulta de Usuários', 'Informe abaixo o valor que deseja consultar');

    } else if ($location.$$path === "/cadastrarUsuario") {
        utilService.definirTitulos("Cadastrar Usuário", "Informe abaixo os dados para criação do novo usuário");
    }

    //efetua cadastro do usuario
    $scope.CadastrarUsuario = function (userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario) {

        utilService.limparMensagem();

        if ($scope.userNameUsuario === undefined || $scope.userNameUsuario === ""
            || $scope.nomeUsuario === undefined || $scope.nomeUsuario === ""
            || $scope.emailUsuario === undefined || $scope.emailUsuario === ""
            || $scope.perfilUsuario === undefined || $scope.perfilUsuario === ""
            || $scope.senhaUsuario === undefined || $scope.senhaUsuario === ""
            || $scope.usuarioSenhaConfirmacao === undefined || $scope.usuarioSenhaConfirmacao === "") {

            return
        }

        //verifica se a senha digitada na confirmação confere 
        if ($scope.usuarioSenhaConfirmacao !== $scope.senhaUsuario) {
            utilService.mensagemAlerta("A senha não confere, favor informar novamente!");

            return
        }


        //efetua cadastro do usuario
        usuarioService.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario).then(function (response) {
            
            $scope.retornoCadastro = response.data;

            if (typeof $scope.retornoCadastro === 'object') {
                utilService.mensagemSucesso("Cadastro efetuado com sucesso!");
                $scope.LimparFormularioCadastro();
              
            } else if (typeof $scope.retornoCadastro === 'string') {
                utilService.mensagemAlerta($scope.retornoCadastro);
            }

            if ($scope.retornoCadastro !== '' && $scope.retornoCadastro !== null) {
                $scope.showMessage = true;
            } else {
                $scope.showMessage = false;
            }
        }, function (response) {
            
            utilService.mensagemErro(response.data);
        });
    }

    //limpa campos de cadastro
    $scope.LimparFormularioCadastro = function () {
        $scope.userNameUsuario = "";
        $scope.nomeUsuario = "";
        $scope.emailUsuario = "";
        $scope.perfilUsuario = "";
        $scope.senhaUsuario = "";
        $scope.usuarioSenhaConfirmacao = "";      
    }


    //efetua consulta do usuario
    $scope.ConsultarUsuario = function (usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId) {
        
        $scope.showDivAlteracao = false;

        $scope.listaUsuarios = usuarioService.ConsultarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId).then(function (response) {
            
            $scope.listaUsuarios = response.data;

        }, function (response) {
            utilService.mensagemErro(response.data);
        });
    }

    //efetua consulta de perfil do usuario
    $scope.ConsultaPerfil = function (usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao) {

        $scope.listaPerfis = usuarioService.ConsultaPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao).then(function (response) {
            $scope.listaPerfis = response.data;
        }, function (response) {
            utilService.mensagemModalErro(response.data);
        });
    };

    //exibe tela de lista/alteração de usuário
    $scope.AbrirTelaAlteracao = function (usuario) {

        utilService.limparMensagem();
        
        //retorna lista de usuarios
        $scope.retornoUsuario = usuarioService.ConsultarUsuario(usuario.usuarioId, usuario.usuarioLogin,'', usuario.usuarioEmail, usuario.usuarioAtivo, usuario.usuarioSenha, usuario.usuPerfilId, usuario.clienteId).then(function (response) {
            $scope.retornoUsuario = response.data;
            $scope.chkAlterarSenha = false;

            if ($scope.retornoUsuario !== null && $scope.retornoUsuario.length > 0) {
                $scope.usuarioLogin = $scope.retornoUsuario[0].usuarioLogin;
                $scope.usuarioId = $scope.retornoUsuario[0].usuarioId;
                $scope.usuarioNome = $scope.retornoUsuario[0].usuarioNome;
                $scope.usuarioEmail = $scope.retornoUsuario[0].usuarioEmail;
                $scope.usuarioSenha = $scope.retornoUsuario[0].usuarioSenha;
                $scope.usuarioAtivo = $scope.retornoUsuario[0].usuarioAtivo;
                $scope.usuPerfilId = $scope.retornoUsuario[0].usuPerfilId;
                $scope.clienteId = $scope.retornoUsuario[0].clienteId;
                //$scope.showDivAlteracao = true;
                $scope.showDivSenha = false;
                //$scope.gotoBottom();              
                
                var modalInstance = $uibModal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    controllerAs: 'vm',
                    controller: 'alterarUsuarioModalCtrl',
                    templateUrl: '/app/views/modal/edicaoUsuarioModal.html',
                     scope: $scope,
                    resolve: {
                        items: function () {
                            return $scope.retornoUsuario;
                        },
                        custom: function () {
                            return usuario.usuarioLogin;
                        }
                    }
                });               
            } else {
                alert("usuario nao encontrado")
            }

        }, function (response) {
            utilService.mensagemErro(response.data);
        });
    }

    //ordena lista de usuarios
    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;
        $scope.reverse = !$scope.reverse;
    }

    //efetua ativação do usuário selecionado
    $scope.AtivarUsuario = function (usuario) {

        usuario.usuarioAtivo = true; //ativa usuario
        utilService.limparMensagem();

        $scope.retornoAtivacao = usuarioService.AlterarUsuario(usuario.usuarioId, usuario.usuarioLogin, usuario.usuarioNome, usuario.usuarioEmail, '', usuario.usuarioAtivo, usuario.usuPerfilId, usuario.clienteId).then(function (data) {

            $scope.retornoAlteracao = data;
            $scope.showMessage = true;
            $scope.showDivAlteracao = false;
            $scope.ConsultarUsuario('', '', '', '', '', '', '', '');
            utilService.mensagemSucesso("Usuário ativado com sucesso!", true);
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    }

    //efetua desativação do usuário selecionado
    $scope.InativarUsuario = function (usuario) {

        usuario.usuarioAtivo = false; //inativa usuario
        utilService.limparMensagem();

        $scope.retornoAtivacao = usuarioService.AlterarUsuario
            (usuario.usuarioId, usuario.usuarioLogin, usuario.usuarioNome, usuario.usuarioEmail, '',
                    usuario.usuarioAtivo, usuario.usuPerfilId, usuario.clienteId).then(function (data) {
                        $scope.retornoAlteracao = data;
                        $scope.showMessage = true;
                        $scope.showDivAlteracao = false;
                        $scope.ConsultarUsuario('', '', '', '', '', '', '', '');
                        utilService.mensagemSucesso("Usuário inativado com sucesso!", true);
                    }, function (error) {
                        utilService.mensagemErro(error.data.message);
                    });
    }

    //cancela alteração do registro do usuario
    $scope.CancelarAlteracao = function () {
        utilService.limparMensagem();
        $scope.showDivAlteracao = false;
        $scope.showDivSenha = false;
    }

    //exibe campos de senha
    $scope.ExibirCampoSenha = function () {

        if ($scope.showDivSenha) {
            $scope.showDivSenha = false;
        } else {
            $scope.showDivSenha = true;
        }
    }

    //move o scroll para rodapé da tela
    $scope.gotoBottom = function () {
        window.setTimeout("window.scrollTo(0, document.body.scrollHeight + 100)", 100);    
    };

    //move o scroll para topo da tela
    $scope.gotoTop = function () {
        window.setTimeout("window.scrollTo(0, document.body.scrollHeight -5000)", 100);       
    };

}]);
