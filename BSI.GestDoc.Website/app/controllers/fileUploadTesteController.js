'use strict';
//app.controller('UploadCtrl', function ($scope, $http, $timeout, $upload) {
//app.controller("fileUploadTesteController", ["$scope", "$routeParams", "$location", "$upload", "fileUploadService", function ($scope, $routeParams, $location, $upload, fileUploadService) {


    //inject directives and services.
    //var app = angular.module('fileUpload', ['ngFileUpload']);

    app.controller('fileUploadTesteController', ['$scope', 'Upload', function ($scope, Upload) {
        /*$scope.$watch('file', function (file) {
            $scope.upload($scope.file);
        });*/

        $scope.model = {};
        $scope.uploadProgress = 0;

        /* optional: set default directive values */
        //Upload.setDefaults( {ngf-keep:false ngf-accept:'image/*', ...} );

        $scope.upload = function () {

            debugger;

            var _formData = new FormData();
            _formData.append("documentoClienteTipo", { DocCliTipoId: 1, ClienteId: 1, DocCliTipoNome: "" });
            _formData.append("documentoClienteSituacao", { DocCliSituId: 1, DocCliSituDescricao: "" });
            var _file = document.getElementById('file').files[0]

            var _fields = {
                documentoClienteTipo: { DocCliTipoId: 1, ClienteId: 1, DocCliTipoNome: "" },
                documentoClienteSituacao: { DocCliSituId: 1, DocCliSituDescricao: "" }
            };

            Upload.upload({
                url: 'http://localhost:44857/api/FileUpload/EnviarArquivos?{documentoClienteTipo:{docCliTipoId:1,clienteId:1}}',
                data: _formData,
                file: _file
            }).progress(function (evt) {

                $scope.uploadProgress = parseInt(100.0 * evt.loaded / evt.total, 10);

            }).success(function (data, status, headers, config) {

                debugger;

                console.log('file ' + config.file.name + 'uploaded. Response: ' + data);
            }).error(function (data, status, headers, config) {

                debugger;

                console.log('error status: ' + status);
            })
        };
    }]);

    /*

    debugger;

    $scope.upload = [];
    $scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };

    $scope.onFileSelect = function ($files) {

        debugger;

        //$files: an array of files selected, each file has name, size, and type.
        for (var i = 0; i < $files.length; i++) {
            var $file = $files[i];
            (function (index) {
                $scope.upload[index] = $upload.upload({
                    url: "http://localhost:44857/api/FileUpload/EnviarArquivos", // webapi url
                    method: "POST",
                    data: { fileUploadObj: $scope.fileUploadObj },
                    file: $file
                }).progress(function (evt) {
                    // get upload percentage
                    console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                }).success(function (data, status, headers, config) {
                    // file is uploaded successfully
                    console.log(data);
                }).error(function (data, status, headers, config) {
                    // file failed to upload
                    console.log(data);
                });
            })(i);
        }
    }

    $scope.abortUpload = function (index) {
        $scope.upload[index].abort();
    }
}]);
/*
app.controller('uploadCtrl', [
        '$scope',
        '$upload',
        function ($scope, $upload) {

            debugger;

            $scope.model = {};
            $scope.selectedFile = [];
            $scope.uploadProgress = 0;

            $scope.uploadFile = function () {
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
                }).error(function (data, status) {
                    debugger;
                });
            };

            $scope.onFileSelect = function ($files) {
                $scope.uploadProgress = 0;
                $scope.selectedFile = $files;
            };
        }
    ])
    .directive('progressBar', [
        function () {
            return {
                link: function ($scope, el, attrs) {
                    $scope.$watch(attrs.progressBar, function (newValue) {
                        el.css('width', newValue.toString() + '%');
                    });
                }
            };
        }
    ]);*/