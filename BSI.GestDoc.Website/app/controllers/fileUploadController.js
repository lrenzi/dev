'use strict';
app.controller("fileUploadController", ["$scope", "$routeParams", "$location", "fileUploadService", function ($scope, $routeParams, $location, fileUploadService) {
    //alert("nois");
    $scope.dados = {
        documentoClienteTipo: { DocCliTipoId: 1, ClienteId: 1, DocCliTipoNome: "" },
        documentoClienteSituacao: { DocCliSituId: 1, DocCliSituDescricao : ""},
        files: FormData()
    };

    $scope.ConsultarArquivos = function () {
        //debugger;
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {


            $scope.listaDocumentosClienteTipo = data;
        }, function (error) {
            alert(error.data.message);
        });
    };
    $scope.EnviarArquivos = function () {

        debugger;

        var file = document.getElementById('file').files[0]
        $scope.dados.files.append('file', file);

        debugger;

        $scope.listaDocumentosClienteTipo = fileUploadService.EnviarArquivos($scope.dados).then(function (data) {
            debugger;
            $scope.listaDocumentosClienteTipo = data;
        }, function (error) {
            debugger;
            alert(error.data.message);
        });
        /*
        fileUploadService.EnviarArquivos().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        });*/
    };
}]);