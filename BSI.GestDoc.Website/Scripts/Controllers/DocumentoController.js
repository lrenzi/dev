'use strict';
app.controller("DocumentoController", ["$scope", "$routeParams", "$location", "FileUploadService", function ($scope, $routeParams, $location, FileUploadService) {
    $scope.ConsultarArquivos = function () {
        $scope.listaDocumentosClienteTipo = FileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        });
    };
    $scope.EnviarArquivos = function () {
        $scope.listaDocumentosClienteTipo = FileUploadService.EnviarArquivos().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        });
    };
}]);