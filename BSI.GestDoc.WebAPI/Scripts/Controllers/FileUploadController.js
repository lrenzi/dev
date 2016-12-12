'use strict';
app.controller("FileUploadCtrl", ["$scope", "$routeParams", "$location", "FileUploadService", function ($scope, $routeParams, $location, CargosService) {
    $scope.FileUpload = function(){
        $scope.DocumentoClienteTipo = FileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.DocumentoClienteTipo = data;
        });
    };
}]);