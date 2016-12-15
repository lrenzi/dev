'use strict';
debugger;
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
    ]);