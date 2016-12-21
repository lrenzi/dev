'use strict';
app.controller("fileUploadController", ["$scope", "$routeParams", "$location", "fileUploadService", 'Upload', 'ngAuthSettings', function ($scope, $routeParams, $location, fileUploadService, Upload, ngAuthSettings) {

    $scope.mostraBotaoEnviar = true;
    $scope.mostraBotaoNovo = false;
    $scope.desabilitaFile = false;

    $scope.ConsultarArquivos = function () {
        
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        }, function (error) {
            alert(error.data.message);
        });
    };

    $scope.IniciarTela = function () {

        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        }, function (error) {
            alert(error.data.message);
        });
        $scope.mostraBotaoEnviar = true;
        $scope.mostraBotaoNovo = false;
        $scope.desabilitaFile = false;
    }

    $scope.RetornarArquivo = function (docCliTipoId) {
        debugger;
        var docClienteId = document.getElementById("idDocClienteId_" + docCliTipoId).value;
        fileUploadService.RetornarArquivo(docClienteId).then(function (data) {
           
        }, function (error) {
           
            alert(error.data.message);
        });
    };

    
    /*$scope.RetornarArquivo = function () {

        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
        }, function (error) {
            alert(error.data.message);
        });
    };*/

    $scope.EnviarArquivosWebAPI = function (index_, file_, reenvio_, docCliTipoId) {
        
        if (docCliTipoId == "") {
            docCliTipoId = $scope.listaDocumentosClienteTipo[index_].docCliTipoId;
        }
        debugger;

        Upload.upload({
            url: ngAuthSettings.apiServiceBaseUri + "api/FileUpload/EnviarArquivos?usuarioId=" + ngAuthSettings.usuarioId + "&clienteId=" + ngAuthSettings.clienteId + "&docCliTipoId=" + docCliTipoId + "&reenvio=" + reenvio_,
            file: file_.files[0]
        }).progress(function (evt) {
            document.getElementById('idProgressbar_' + docCliTipoId).innerText = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";
        }).success(function (data, status, headers, config) {

            if (data.tipoErro == 2) {
                //angular.element(document.querySelector('#reenvio_' + $scope.listaDocumentosClienteTipo[index_].docCliTipoId)).value = "S";
                //document.getElementById('reenvio_' + $scope.listaDocumentosClienteTipo[index_].docCliTipoId).value = "S";
                $scope.listaDocumentosClienteTipo[index_].reenvio = "S";
            } else {
                $scope.listaDocumentosClienteTipo[index_].reenvio = "N";
                debugger;
                document.getElementById("idNameFile_" + docCliTipoId).value = file_.value.split("\\")[file_.value.split("\\").length - 1];
                document.getElementById("idDocClienteId_" + docCliTipoId).value = data.dados.docClienteId;
            }
            document.getElementById('idStatus_' + docCliTipoId).innerText = data.mensagem;

            $scope.mostraBotaoEnviar = false; 
            $scope.mostraBotaoNovo = true;
            $scope.desabilitaFile = true;


        }).error(function (data, status, headers, config) {
            
            if (data.message == undefined)
                document.getElementById('idStatus_' + docCliTipoId).innerText = data;
            else
                document.getElementById('idStatus_' + docCliTipoId).innerText = data.message;
        });
    };

    $scope.ReenviarArquivo = function (docCliTipoId, index_) {

        var file_ = document.getElementById('idFile_' + docCliTipoId);
        if (file_.value != "") {
            $scope.EnviarArquivosWebAPI(index_, file_, "S", docCliTipoId);
        }
        else {
            alert("Selecione um arquivo para Reenvio.")
        }
    }

    $scope.EnviarArquivos = function () {

        $scope.contador = 0;

        for (var contador = 0; contador < $scope.listaDocumentosClienteTipo.length; contador++) {

            var file_ = document.getElementById('idFile_' + $scope.listaDocumentosClienteTipo[contador].docCliTipoId);
            if (file_.value != "") {

                $scope.contador = contador;

                $scope.EnviarArquivosWebAPI(contador, file_, "N", "");


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