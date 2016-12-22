
'use strict';
app.controller("listaPropostasController", ["$scope", "$routeParams", "$location", "listaPropostasService", 'documentoCliente', 'fileUploadService', function ($scope, $routeParams, $location, listaPropostasService, documentoCliente, fileUploadService) {
    documentoCliente.numeroPesquisaProposta = '';
    $scope.showListaProposta = false;
    $scope.showMensagemListaVazia=false;
    
    $scope.RetornarArquivo = function (docClienteId_, fileName_) {
        fileUploadService.RetornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            alert(error.data.message);
        });
    };

    $scope.ListarPropostas = function (keySearch) {
        documentoCliente.numeroPesquisaProposta = keySearch;
        $scope.listaPropostas = listaPropostasService.ListarPropostas().then(function (data) {
            $scope.listaPropostas = data;
            
            if ($scope.listaPropostas.length == 0) {
                $scope.showListaProposta = false;
                $scope.showMensagemListaVazia = true;
            } else {
                
                $scope.listaPropostas = $scope.listaPropostas[0];
                $scope.showListaProposta = true;
                $scope.showMensagemListaVazia = false;
            }
        }, function (error) {
            alert(error.data.message);
        });
    };
}]);
