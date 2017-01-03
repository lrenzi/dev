﻿'use strict';
app.controller("fileUploadController", ["$scope", "$routeParams", "$location", "fileUploadService", 'Upload', 'ngAuthSettings', 'localStorageService', 'utilService', function ($scope, $routeParams, $location, fileUploadService, Upload, ngAuthSettings, localStorageService, utilService) {

    $scope.mostraBotaoEnviar = true;
    $scope.mostraBotaoNovo = false;
    $scope.desabilitaFile = false;

    utilService.definirTitulos('Upload de Documentos', 'Procure os documentos para envio');

    $scope.IniciarTela = function () {

        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
            iniciarListaDocumentoClienteTipo();
        }, function (error) {
            utilService.mensagemErro(error.data.message);
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

       
       
        //userPersistenceService.setCookieData("WESLEY");
        //alert(userPersistenceService.getCookieData());

        //alert(ngAuthSettings.clienteId);
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
            iniciarListaDocumentoClienteTipo();
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    };

    $scope.RetornarArquivo = function (docClienteId_, fileName_) {
        fileUploadService.RetornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    };

    $scope.ReenviarArquivo = function (docCliTipoId, index_) {

        var file_ = document.getElementById('idFile_' + docCliTipoId);
        if (file_.value != "") {
            $scope.EnviarArquivoWebAPI(index_, file_, true, docCliTipoId);
        }
        else {
            utilService.mensagemInformativo("Selecione ao menos um arquivo para reenvio.");
        }
    };

    $scope.EnviarArquivo = function () {

        $scope.contador = 0;
        var quantidadeArquivoEncontrados = 0;
        utilService.limparMensagem();

        for (var contador = 0; contador < $scope.listaDocumentosClienteTipo.length; contador++) {

            var file_ = document.getElementById('idFile_' + $scope.listaDocumentosClienteTipo[contador].docCliTipoId);

            if (file_.value != "") {
                quantidadeArquivoEncontrados++;
                $scope.contador = contador;
                $scope.EnviarArquivoWebAPI(contador, file_, false, "");
            }
        }
        
        if (quantidadeArquivoEncontrados == 0) {
            utilService.mensagemInformativo("Selecione ao menos um arquivo para envio.");
            //testes wesley wk
            //mensagemService.mensagemInformativo(["Selecione ao menos um arquivo para envio.", "Outra mensagem qualquer para testar a mensageria."]);
            //mensagemService.mensagemErro("Ocorreu um erro de processamento no servidor durante o request do capiroto. ER3302x.");
            //mensagemService.mensagemSucesso("Operação executada com sucesso.");
            //mensagemService.mensagemAlerta("Se você soltar o cabo da marreta vai feder, tem certeza que deseja soltar?");
        }
    };

    $scope.EnviarArquivoWebAPI = function (index_, file_, reenvio_, docCliTipoId) {

        
        var infClientes = localStorageService.get('ngAuthSettings');

        if (docCliTipoId == "") {
            docCliTipoId = $scope.listaDocumentosClienteTipo[index_].docCliTipoId;
        }
        Upload.upload({
            url: ngAuthSettings.apiServiceBaseUri + "api/FileUpload/EnviarArquivos?usuarioId=" + infClientes.usuarioId + "&clienteId=" + infClientes.clienteId + "&docCliTipoId=" + docCliTipoId + "&reenvio=" + reenvio_,
            file: file_.files[0]
        }).progress(function (evt) {
            $scope.listaDocumentosClienteTipo[index_].progressbar = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";
        }).success(function (data, status, headers, config) {
            if (data.tipoErro == 1) {
                $scope.listaDocumentosClienteTipo[index_].reenvio = false;
                $scope.listaDocumentosClienteTipo[index_].nameFile = file_.value.split("\\")[file_.value.split("\\").length - 1];
                $scope.listaDocumentosClienteTipo[index_].docClienteId = data.dados.cryptoDocClienteId;
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