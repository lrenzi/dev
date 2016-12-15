'use strict';
app.controller("fileUploadController", ["$scope", "$routeParams", "$location", "FileUploadService", function ($scope, $routeParams, $location, fileUploadService) {
    alert("nois");
    $scope.ConsultarArquivos = function () {
        debugger;
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        });
    };
    $scope.EnviarArquivos = function () {
        $scope.listaDocumentosClienteTipo = fileUploadService.EnviarArquivos().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        });
    };
}]);