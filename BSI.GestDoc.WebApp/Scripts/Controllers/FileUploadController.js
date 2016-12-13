'use strict';
app.controller("FileUploadCtrl", ["$scope", "$routeParams", "$location", "FileUploadService", function ($scope, $routeParams, $location, FileUploadService) {
    $scope.ConsultarArquivos = function () {
        $scope.listaDocumentosClienteTipo = FileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        });
    };
}]);