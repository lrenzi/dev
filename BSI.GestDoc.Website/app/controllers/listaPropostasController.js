
'use strict';
app.controller("listaPropostasController", ["$scope", "$routeParams", "$location", "listaPropostasService", 'documentoCliente', 'fileUploadService', 'utilService', function ($scope, $routeParams, $location, listaPropostasService, documentoCliente, fileUploadService, utilService) {
    documentoCliente.numeroPesquisaProposta = '';
    $scope.showListaProposta = false;
    $scope.showMensagemListaVazia = false;
    $scope.listaSituacoesProposta = [];
    $scope.valorMensagem = "";
    
    utilService.defirniTitulos('Consulta de propostas', 'Informe abaixo o valor que deseja consultar');

    $scope.RetornarArquivo = function (docClienteId_, fileName_) {
        fileUploadService.RetornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    };

    $scope.ListarPropostas = function (keySearch) {
        
        if (keySearch == undefined || keySearch.trim() == "") {
            return
        }

        documentoCliente.numeroPesquisaProposta = keySearch;
        $scope.listaPropostas = listaPropostasService.ListarPropostas().then(function (data) {
            $scope.listaPropostas = data;
            
            if ($scope.listaPropostas.length == 0) {
                $scope.showListaProposta = false;
                $scope.showMensagemListaVazia = true;
                $scope.valorMensagem = "Proposta não encontrada";
            } else {
                $scope.showListaProposta = true;
                $scope.showMensagemListaVazia = false;
            }
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    };
}]);
