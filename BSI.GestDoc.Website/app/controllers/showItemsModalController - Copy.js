'use strict';
app.controller('showItemsModalCtrl', ['$scope', '$uibModalInstance', 'items', 'custom', "fileUploadService", "usuarioService", 'utilService', function ($scope, $uibModalInstance, items, custom, fileUploadService, usuarioService, utilService) {

    init();

    function init() {
        $scope.items = items;
        $scope.custom = custom;
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.RetornarArquivo = function (docClienteId_, fileName_) {
        utilService.limparMensagemModal();
        fileUploadService.RetornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            utilService.mensagemModalErro(error.data.message);
        });
    };

    //efetua consulta de perfil do usuario
    $scope.ConsultaPerfil = function (usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao) {
        
        $scope.listaPerfis = usuarioService.ConsultaPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao).then(function (response) {
            $scope.listaPerfis = response.data;
        }, function (response) {
            utilService.mensagemModalErro(response.data.message);
        });
    };


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
                utilService.mensagemModalAlerta("A senha informada não confere, favor informar novamente!");
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
                                                                     $scope.showMessage = true;
                                                                     $scope.showDivSenha = false;
                                                                     $scope.senhaUsuario = "";
                                                                     $scope.confirmacaoSenhaUsuario = "";
                                                                     $scope.chkAlterarSenha = false;
                                                                     $scope.cancel();
                                                                     $scope.retornoAlteracao = response;
                                                                     $scope.ConsultarUsuario('', '', '', '', '', '', '', '');
                                                                     utilService.mensagemSucesso("Alteração efetuada com sucesso!");
                                                                     
                                                                 }, function (response) {
                                                                     $scope.cancel();
                                                                     utilService.mensagemModalErro(response.data.message);
                                                                     $scope.gotoTop();
                                                                 });
    }

}]);