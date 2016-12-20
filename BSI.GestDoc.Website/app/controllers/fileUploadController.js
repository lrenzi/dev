'use strict';
app.controller("fileUploadController", ["$scope", "$routeParams", "$location", "fileUploadService", 'Upload', 'ngAuthSettings', function ($scope, $routeParams, $location, fileUploadService, Upload, ngAuthSettings) {
    
    $scope.ConsultarArquivos = function () {
        //debugger;
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        }, function (error) {
            alert(error.data.message);
        });
    };

    $scope.EnviarArquivosWebAPI = function (index_, file_) {
        
        var docCliTipoId = $scope.listaDocumentosClienteTipo[index_].docCliTipoId;
        
        Upload.upload({
            url: ngAuthSettings.apiServiceBaseUri + "/api/FileUpload/EnviarArquivos?usuarioId=" + ngAuthSettings.usuarioId + "&clienteId=" + ngAuthSettings.clientId + "&docCliTipoId=" + docCliTipoId,
            file: file_.files[0]
        }).progress(function (evt) {

            document.getElementById('idProgressbar_' + $scope.listaDocumentosClienteTipo[index_].docCliTipoId).innerText = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";

        }).success(function (data, status, headers, config) {
            debugger;
            document.getElementById('idStatus_' + $scope.listaDocumentosClienteTipo[index_].docCliTipoId).innerText = "Ok";

        }).error(function (data, status, headers, config) {

            if (data.message == undefined)
                document.getElementById('idStatus_' + $scope.listaDocumentosClienteTipo[index_].docCliTipoId).innerText = data;
            else
                document.getElementById('idStatus_' + $scope.listaDocumentosClienteTipo[index_].docCliTipoId).innerText = data.message;
        });
    };

    $scope.EnviarArquivos = function () {

        $scope.contador = 0;

        for (var contador = 0; contador < $scope.listaDocumentosClienteTipo.length; contador++) {

            var file_ = document.getElementById('idFile_' + $scope.listaDocumentosClienteTipo[contador].docCliTipoId);
            if (file_.value != "") {

                $scope.contador = contador;

                $scope.EnviarArquivosWebAPI(contador, file_);

                
            }
        }
        /*
        var files = document.getElementsByName('file');

        for (var i = 0; i < files.length; i++) {
            var file_ = files[i];
            if (file_.value != "") {

                debugger;

                Upload.upload({
                    url: ngAuthSettings.apiServiceBaseUri + "/api/FileUpload/EnviarArquivos?idCliente=" + ngAuthSettings.clientId,
                    file: file_.files[0]
                }).progress(function (evt) {

                    debugger;

                    $scope.uploadProgress[i] = parseInt(100.0 * evt.loaded / evt.total, 10);

                }).success(function (data, status, headers, config) {

                    debugger;
                    $scope.uploadProgress[i] = 100;
                    $scope.status[i] = "Ok";

                }).error(function (data, status, headers, config) {

                    debugger;
                    $scope.status[i] = data;
                    
                })
            }
        }

        
        /*

        debugger;

        $scope.listaDocumentosClienteTipo = fileUploadService.EnviarArquivos(Upload, file).then(function (retorno) {
            debugger;
            $scope.listaDocumentosClienteTipo = retorno;
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