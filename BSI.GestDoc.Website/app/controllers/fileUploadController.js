'use strict';
app.controller("fileUploadController", ["$scope", "$routeParams", "$location", "fileUploadService", function ($scope, $routeParams, $location, fileUploadService) {
    //alert("nois");

    $scope.model = {};
    $scope.selectedFile = [];
    $scope.uploadProgress = 0;

    $scope.ConsultarArquivos = function () {
        //debugger;
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;

           

        });
    };
    $scope.EnviarArquivos = function () {
        $scope.listaDocumentosClienteTipo = null;


        debugger;

       

        debugger;

        var file = $scope.selectedFile[0];
        $scope.upload = $upload.upload({
            url: 'http://localhost:44857/api/FileUpload/EnviarArquivos',
            method: 'POST',
            data: angular.toJson($scope.model),
            file: file
        }).progress(function (evt) {
            debugger;
            $scope.uploadProgress = parseInt(100.0 * evt.loaded / evt.total, 10);
        }).success(function (data) {
            debugger;
            //do something
        }).error(function (data, status) {
            debugger;
        })

        alert("k");
      
        $scope.onFileSelect = function ($files) {
            $scope.uploadProgress = 0;
            $scope.selectedFile = $files;
        };
            
        /*
        fileUploadService.EnviarArquivos().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        });*/
    };
}]);