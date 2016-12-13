'use strict';
alert("k");
app.controller("FileUploadCtrl", ["$scope", "$routeParams", "$location", "FileUploadService", function ($scope, $routeParams, $location, FileUploadService) {
    $scope.FileUpload = function () {
        alert("k2");
        debugger;
        $scope.DocumentoClienteTipo = FileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.DocumentoClienteTipo = data;
        });
    };
}]);