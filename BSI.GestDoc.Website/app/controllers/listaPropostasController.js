﻿
'use strict';
app.controller("listaPropostasController", ["$scope", "$routeParams", "$location", "listaPropostasService", 'documentoCliente', 'fileUploadService', 'utilService', function ($scope, $routeParams, $location, listaPropostasService, documentoCliente, fileUploadService, utilService) {
    documentoCliente.numeroPesquisaProposta = '';
    $scope.showListaProposta = false;
    $scope.showMensagemListaVazia = false;
    $scope.listaSituacoesProposta = [];
    $scope.valorMensagem = "";

    utilService.definirTitulos('Consulta de propostas', 'Informe abaixo o valor que deseja consultar');

    $scope.RetornarArquivo = function (docClienteId_, fileName_) {
        fileUploadService.RetornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            if (error.status == 409)
                utilService.mensagemAlerta(error.data.message);
            else
                utilService.mensagemErro(error.data.message);
        });
    };

    $scope.ListarPropostas = function (keySearch) {

        if (keySearch == undefined || keySearch.trim() == "") {
            return
        }

        utilService.limparMensagem();

        documentoCliente.numeroPesquisaProposta = keySearch;
        $scope.listaPropostas = listaPropostasService.ListarPropostas().then(function (data) {
            $scope.listaPropostas = data;

            if ($scope.listaPropostas.length == 0) {
                $scope.showListaProposta = false;
                //$scope.showMensagemListaVazia = true;
                //$scope.valorMensagem = "Proposta não encontrada";
                utilService.mensagemAlerta("Proposta não encontrada");
            } else {
                $scope.showListaProposta = true;
                //$scope.showMensagemListaVazia = false;
            }
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    };
}]);
