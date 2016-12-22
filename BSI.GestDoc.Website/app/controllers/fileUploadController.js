'use strict';
app.controller("fileUploadController", ["$scope", "$routeParams", "$location", "fileUploadService", 'Upload', 'ngAuthSettings', function ($scope, $routeParams, $location, fileUploadService, Upload, ngAuthSettings) {

    $scope.mostraBotaoEnviar = true;
    $scope.mostraBotaoNovo = false;
    $scope.desabilitaFile = false;

    

    $scope.IniciarTela = function () {

        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
            iniciarListaDocumentoClienteTipo();
        }, function (error) {
            alert(error.data.message);
        });
        $scope.mostraBotaoEnviar = true;
        $scope.mostraBotaoNovo = false;
        $scope.desabilitaFile = false;
    }

    function iniciarListaDocumentoClienteTipo() {
        angular.forEach($scope.listaDocumentosClienteTipo, function (value, key) {
            $scope.listaDocumentosClienteTipo[key].nameFile = "";
            $scope.listaDocumentosClienteTipo[key].docClienteId = 0;
            $scope.listaDocumentosClienteTipo[key].status = "";
            $scope.listaDocumentosClienteTipo[key].progressbar = "";
            $scope.listaDocumentosClienteTipo[key].reenvio = false;
            $scope.listaDocumentosClienteTipo[key].download = false;
            $scope.listaDocumentosClienteTipo[key].cssclass = "";
        });
    }

    $scope.ConsultarArquivos = function () {
        alert(ngAuthSettings.clienteId);
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
            iniciarListaDocumentoClienteTipo();
        }, function (error) {
            alert(error.data.message);
        });
    };

    $scope.RetornarArquivo = function (docClienteId_, fileName_) {
        fileUploadService.RetornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            alert(error.data.message);
        });
    };

    $scope.ReenviarArquivo = function (docCliTipoId, index_) {

        var file_ = document.getElementById('idFile_' + docCliTipoId);
        if (file_.value != "") {
            $scope.EnviarArquivoWebAPI(index_, file_, true, docCliTipoId);
        }
        else {
            alert("Selecione um arquivo para Reenvio.")
        }
    };

    $scope.EnviarArquivo = function () {

        $scope.contador = 0;

        for (var contador = 0; contador < $scope.listaDocumentosClienteTipo.length; contador++) {

            var file_ = document.getElementById('idFile_' + $scope.listaDocumentosClienteTipo[contador].docCliTipoId);

            if (file_.value != "") {
                $scope.contador = contador;
                $scope.EnviarArquivoWebAPI(contador, file_, false, "");
            }
        }
    };

    $scope.EnviarArquivoWebAPI = function (index_, file_, reenvio_, docCliTipoId) {

        if (docCliTipoId == "") {
            docCliTipoId = $scope.listaDocumentosClienteTipo[index_].docCliTipoId;
        }
        Upload.upload({
            url: ngAuthSettings.apiServiceBaseUri + "api/FileUpload/EnviarArquivos?usuarioId=" + ngAuthSettings.usuarioId + "&clienteId=" + ngAuthSettings.clienteId + "&docCliTipoId=" + docCliTipoId + "&reenvio=" + reenvio_,
            file: file_.files[0]
        }).progress(function (evt) {
            $scope.listaDocumentosClienteTipo[index_].progressbar = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";
        }).success(function (data, status, headers, config) {
            if (data.tipoErro == 1) {
                $scope.listaDocumentosClienteTipo[index_].reenvio = false;
                $scope.listaDocumentosClienteTipo[index_].nameFile = file_.value.split("\\")[file_.value.split("\\").length - 1];
                $scope.listaDocumentosClienteTipo[index_].docClienteId = data.dados.cryptoDocClienteId;
                alert($scope.listaDocumentosClienteTipo[index_].docClienteId);
                $scope.listaDocumentosClienteTipo[index_].download = true;
                $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-success";
            } else if (data.tipoErro == 2) {
                $scope.listaDocumentosClienteTipo[index_].reenvio = false;
                $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-info";
            }
            else if (data.tipoErro == 3) {
                $scope.listaDocumentosClienteTipo[index_].reenvio = true;
                $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-info";
            }
            $scope.listaDocumentosClienteTipo[index_].status = data.mensagem;

            $scope.mostraBotaoEnviar = false;
            $scope.mostraBotaoNovo = true;
            $scope.desabilitaFile = true;

        }).error(function (data, status, headers, config) {
            $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-danger";
            if (data.message == undefined) {
                $scope.listaDocumentosClienteTipo[index_].status = data;
            } else {
                $scope.listaDocumentosClienteTipo[index_].status = data.message;
            }
            $scope.mostraBotaoEnviar = false;
            $scope.mostraBotaoNovo = true;
            $scope.desabilitaFile = true;
        });
    };

}]);