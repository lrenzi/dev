
'use strict';
app.controller("usuarioController", ["$scope", "$routeParams", "$location", "usuarioService", "localStorageService", "utilService", '$anchorScroll', '$timeout', '$rootScope', function ($scope, $routeParams, $location, usuarioService, localStorageService, utilService, $anchorScroll, $timeout, $rootScope) {

    $scope.userNameUsuario = '';
    $scope.nomeUsuario = '';
    $scope.emailUsuario = '';
    $scope.senhaUsuario = '';
    $scope.perfilUsuario = '';
    $scope.showMessage = false;
    $scope.showDivAlteracao = false;
    $scope.showDivSenha = false;
    //$scope.filters = {};

    //$scope.headers = [
    //  { column: "usuarioLogin" },
    //  { column: "usuarioNome" }
    //];

    //$scope.mensagemSucesso = function (mensagem) {
    //    debugger
    //    var objMensagem = {};
    //    objMensagem.tipoMensagem = 'alert alert-success';
    //    $rootScope.exibirMensagem = true;
    //    objMensagem.titulo = 'Sucesso';
    //    objMensagem.mensagem = mensagem;
    //    $rootScope.mensagens.push(objMensagem)
    //}

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
        usuarioService.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario).then(function (data) {
            $scope.retornoCadastro = data;

            if (typeof $scope.retornoCadastro === 'object') {
                utilService.mensagemSucesso("Cadastro efetuado com sucesso!");
                $scope.LimparFormularioCadastro();
                //$scope.reset();

            } else if (typeof $scope.retornoCadastro === 'string') {
                utilService.mensagemAlerta($scope.retornoCadastro);
            }


            if ($scope.retornoCadastro !== '' && $scope.retornoCadastro !== null) {
                $scope.showMessage = true;
            } else {
                $scope.showMessage = false;
            }

        }, function (error) {
            utilService.mensagemErro(error.data.message);
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



    //efetua consulta de perfil do usuario
    $scope.ConsultaPerfil = function (usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao) {

        $scope.listaPerfis = usuarioService.ConsultaPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao).then(function (response) {
            $scope.listaPerfis = response.data;
        }, function (response) {
            utilService.mensagemErro(response.data.message);
        });
    }

    //efetua consulta do usuario
    $scope.ConsultarUsuario = function (usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId) {
        
        $scope.showDivAlteracao = false;

        $scope.listaUsuarios = usuarioService.ConsultarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId).then(function (response) {
            
            $scope.listaUsuarios = response.data;

        }, function (response) {
            utilService.mensagemErro(response.data.message);
        });
    }

    //efetua alteração do usuario
    $scope.AlterarUsuario = function () {
        
        utilService.limparMensagem();

        if ($scope.usuarioNome === "" || $scope.usuarioNome === undefined
            || $scope.usuarioEmail === "" || $scope.usuarioEmail === undefined
            || $scope.usuPerfilId === "" || $scope.usuPerfilId === undefined) {
            return
        }

        //exibe campos para alteração do registro do usuário
        if ($scope.showDivSenha) {

            if ($scope.senhaUsuario === undefined || $scope.senhaUsuario === "" || $scope.confirmacaoSenhaUsuario === undefined || $scope.confirmacaoSenhaUsuario === "") {
                return
            }

            //valida se a senha digitada confere na confirmação
            if ($scope.senhaUsuario !== $scope.confirmacaoSenhaUsuario) {
                utilService.mensagemAlerta("A senha informada não confere, favor informar novamente!");
                $scope.gotoTop();
                return
            } else {
                $scope.usuarioSenha = $scope.senhaUsuario;
            }
        } else {
            $scope.usuarioSenha = "";
        }

        //efetua alteração do registro do usuário
        $scope.retornoAlteracao = usuarioService.AlterarUsuario($scope.usuarioId,
                                                                $scope.usuarioLogin,
                                                                $scope.usuarioNome,
                                                                $scope.usuarioEmail,
                                                                $scope.usuarioSenha,
                                                                $scope.usuarioAtivo,
                                                                $scope.usuPerfilId,
                                                                 $scope.clienteId).then(function (response) {
                                                                     utilService.mensagemSucesso("Cadastro efetuado com sucesso!");
                                                                     $scope.retornoAlteracao = response;
                                                                     $scope.ConsultarUsuario('', '', '', '', '', '', '', '');
                                                                     $scope.showMessage = true;
                                                                     $scope.showDivAlteracao = false;
                                                                     $scope.ConsultarUsuario('', '', '', '', '', '', '', '');
                                                                     $scope.showDivSenha = false;
                                                                     $scope.senhaUsuario = "";
                                                                     $scope.confirmacaoSenhaUsuario = "";
                                                                     $scope.chkAlterarSenha = false;

                                                                 }, function (response) {
                                                                     utilService.mensagemErro(response.data.message);
                                                                     $scope.gotoTop();
                                                                 });
    }

    //exibe tela de lista/alteração de usuário
    $scope.AbrirTelaAlteracao = function (usuario) {
        var infClientes = localStorageService.get('ngAuthSettings');

        //retorna lista de usuarios
        $scope.retornoUsuario = usuarioService.ConsultarUsuario(usuario.usuarioId, usuario.usuarioLogin, usuario.usuarioNome, usuario.usuarioEmail, usuario.usuarioAtivo, usuario.usuarioSenha, usuario.usuPerfilId, usuario.clienteId).then(function (response) {
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
                $scope.showDivAlteracao = true;
                $scope.showDivSenha = false;
                $scope.gotoBottom();              
            }

        }, function (response) {
            utilService.mensagemErro(response.data.message);
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
