'use strict';
alert("k");
app.controller("FileUploadController", ["$scope", "$routeParams", "$location", "FileUploadService", function ($scope, $routeParams, $location, CargosService) {
    $scope.FileUpload = function () {
        alert("k2");
        debugger;
        $scope.DocumentoClienteTipo = FileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.DocumentoClienteTipo = data;
        });
    };
}]);