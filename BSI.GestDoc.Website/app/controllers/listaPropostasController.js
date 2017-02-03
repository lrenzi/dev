
'use strict';
app.controller("listaPropostasController", ['$uibModal', "$scope", "$routeParams", "$location", "listaPropostasService", 'uploadArquivosService', 'utilService', function ($uibModal, $scope, $routeParams, $location, listaPropostasService, fileUploadService, utilService) {
    $scope.showListaProposta = false;
    $scope.showPropostaDetalhe = false;
    $scope.showMensagemListaVazia = false;
    $scope.listaSituacoesProposta = [];
    $scope.valorMensagem = "";

    utilService.definirTitulos('Consulta de propostas', 'Digite abaixo o número da proposta que deseja consultar');

    $scope.RetornarArquivo = function (docClienteId_, fileName_) {
        
        uploadArquivosService.retornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            if (error.status === 409)
                utilService.mensagemAlerta(error.data.message);
            else
                utilService.mensagemErro(error.data.message);
        });
    };


    $scope.ListarPropostas = function () {

        utilService.limparMensagem();

        $scope.listaPropostas = listaPropostasService.ListarPropostas().then(function (response) {
            $scope.listaPropostas = response.data;

            if ($scope.listaPropostas.length === 0) {
                $scope.showListaProposta = false;
                utilService.mensagemAlerta("Não foram encontradas propostas para o cliente.");
            } else {
                $scope.showListaProposta = true;
            }
        }, function (response) {
            utilService.mensagemErro(response.data);
        });
    };

    
    //abre modal para visualização da informações da proposta selecionada
    $scope.open = function (detalhesProposta, numeroPropota) {
        
        var modalInstance = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            controllerAs: 'vm',
            controller: 'showItemsModalCtrl',
            templateUrl: '/app/views/modal/detalhesPropostaModal.html',
           // scope: $scope,
            resolve: {
                items: function () {
                    return  detalhesProposta;
                },
                custom: function () {
                    return numeroPropota;
                }
            }
        });
        modalInstance.result.then(function (selectedUser) {
            $scope.selected = selectedUser;
        });        
    };    

    $scope.ConsultaProposta = function (idProposta, numeroPropota) {

        utilService.limparMensagem();

        $scope.DetalhesProposta = listaPropostasService.ConsultaProposta(idProposta).then(function (response) {
            $scope.DetalhesProposta = response.data;

            if ($scope.listaPropostas.length === 0) {
                $scope.showPropostaDetalhe = false;
                utilService.mensagemAlerta("Proposta não encontrada.");
            } else {
                $scope.open($scope.DetalhesProposta, numeroPropota);
            }
        }, function (response) {
            utilService.mensagemErro(response.data);
        });
    };
}]);
