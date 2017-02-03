'use strict';
app.controller("uploadArquivosController", ["$scope", "$routeParams", "$location", "uploadArquivosService", 'Upload', 'ngAuthSettings', 'localStorageService', 'utilService', function ($scope, $routeParams, $location, uploadArquivosService, Upload, ngAuthSettings, localStorageService, utilService) {

    $scope.mostraBotaoEnviar = true;
    $scope.mostraBotaoNovo = false;
    $scope.desabilitaFile = false;
    $scope.reenvio = false;

    utilService.definirTitulos('Upload de Documentos', 'Procure os documentos para envio');

    $scope.$on('reenviarArquivos', function (event) {
        $scope.reenviarArquivos();
    });
    
    $scope.iniciarTela = function () {
        $scope.listaDocumentosClienteTipo = uploadArquivosService.retornarDocumentoClienteTipo().then(function (response) {
            $scope.listaDocumentosClienteTipo = response.data;
            iniciarListaDocumentoClienteTipo();
        }, function (response) {
            utilService.mensagemErro(response.data);
        });
        $scope.mostraBotaoEnviar = true;
        $scope.mostraBotaoNovo = false;
        $scope.desabilitaFile = false;
        utilService.limparMensagem();
    };

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

    $scope.consultarArquivos = function () {
        $scope.listaDocumentosClienteTipo = uploadArquivosService.retornarDocumentoClienteTipo().then(function (response) {
            $scope.listaDocumentosClienteTipo = response.data;
            iniciarListaDocumentoClienteTipo();
        }, function (response) {
            utilService.mensagemErro(response.data);
        });
    };

    $scope.retornarArquivo = function (docClienteId_, fileName_) {
        uploadArquivosService.retornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    };

    $scope.reenviarArquivo = function (docCliTipoId, index_) {

        var file_ = document.getElementById('idFile_' + docCliTipoId);
        if (file_.value !== "") {
            $scope.enviarArquivoWebAPI(index_, file_, true, docCliTipoId);
        }
        else {
            utilService.mensagemInformativo("Selecione ao menos um arquivo para reenvio.");
        }
    };

    $scope.reenviarArquivos = function() {
        $scope.reenvio = true;
        $scope.enviarArquivos();
    };

    $scope.enviarArquivo = function () {

        $scope.contador = 0;
        var quantidadeArquivoEncontrados = 0;
        utilService.limparMensagem();

        for (var contador = 0; contador < $scope.listaDocumentosClienteTipo.length; contador++) {

            var file_ = document.getElementById('idFile_' + $scope.listaDocumentosClienteTipo[contador].docCliTipoId);

            if (file_.value !== "") {
                quantidadeArquivoEncontrados++;
                $scope.contador = contador;
                $scope.enviarArquivoWebAPI(contador, file_, $scope.reenvio, "");
            }
        }

        if (quantidadeArquivoEncontrados === 0) {
            utilService.mensagemInformativo("Selecione ao menos um arquivo para envio.");
        }
    };

    $scope.enviarArquivos = function () {
        $scope.contador = 0;
        var quantidadeArquivoEncontrados = 0;
        utilService.limparMensagem();
        var docCliTipoId = "";
        var arryFiles = [];

        for (var contador = 0; contador < $scope.listaDocumentosClienteTipo.length; contador++) {

            var file_ = document.getElementById('idFile_' + $scope.listaDocumentosClienteTipo[contador].docCliTipoId);

            if (file_.value !== "") {
                quantidadeArquivoEncontrados++;
                $scope.contador = contador;
                arryFiles.push(file_);
                if (docCliTipoId === "")
                    docCliTipoId = $scope.listaDocumentosClienteTipo[contador].docCliTipoId;
                else
                    docCliTipoId += "|" + $scope.listaDocumentosClienteTipo[contador].docCliTipoId;
            }
        }
        
        if (quantidadeArquivoEncontrados === 0) {
            utilService.mensagemInformativo("Selecione ao menos um arquivo para envio.");
        } else {
            $scope.enviarArquivosWebAPI(arryFiles, $scope.reenvio, docCliTipoId);
        }
    };

    $scope.enviarArquivosWebAPI = function (files_, reenvio_, docCliTipoId) {

        var infClientes = localStorageService.get('ngAuthSettings');
        var _arryFiles = [];
        for (var contador = 0; contador < files_.length; contador++) {
            _arryFiles.push(files_[contador].files[0]);
        }

        Upload.upload({
            url: ngAuthSettings.apiServiceBaseUri + "api/FileUpload/EnviarArquivos?usuarioId=" + infClientes.usuarioId + "&clienteId=" + infClientes.clienteId + "&docCliTipoId=" + docCliTipoId + "&reenvio=" + reenvio_,
            file: _arryFiles
        }).progress(function (evt) {
            $scope.listaDocumentosClienteTipo[0].progressbar = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";
        }).success(function (data, status, headers, config) {
            
            if (data.tipoMensagem === 1) {
                utilService.mensagemSucesso(data.mensagem);
            }
            if (data.tipoMensagem === 2) {
                utilService.mensagemErro(data.mensagem);
            }
            if (data.tipoMensagem === 3) {
                utilService.mensagemAlertaButtom(data.mensagem, "Reenviar", "reenviarArquivos");
            }

            $scope.mensagens.push(data.mensagem);
            
            var index = 0;
            for (var contador = 0; contador < $scope.listaDocumentosClienteTipo.length; contador++) {
                var file_ = document.getElementById('idFile_' + $scope.listaDocumentosClienteTipo[contador].docCliTipoId);
                if (file_.value !== "") {

                    if (data.tipoMensagem === 1) {
                        $scope.listaDocumentosClienteTipo[contador].reenvio = false;
                        $scope.listaDocumentosClienteTipo[contador].nameFile = _arryFiles[index].name;
                        $scope.listaDocumentosClienteTipo[contador].docClienteId = data.dados[index].cryptoDocClienteId;
                        $scope.listaDocumentosClienteTipo[contador].download = true;
                    }
                    
                    ++index;
                }
            }

            $scope.mostraBotaoEnviar = false;
            $scope.mostraBotaoNovo = true;
            $scope.desabilitaFile = true;

        }).error(function (data, status, headers, config) {
            
            if (data.message === undefined) {
                utilService.mensagemAlerta(data);
            } else {
                utilService.mensagemAlerta(data.message);
            }

            $scope.mostraBotaoEnviar = false;
            $scope.mostraBotaoNovo = true;
            $scope.desabilitaFile = true;

           
        });
    };

    $scope.enviarArquivoWebAPI = function (index_, file_, reenvio_, docCliTipoId) {

        
        var infClientes = localStorageService.get('ngAuthSettings');

        if (docCliTipoId === "") {
            docCliTipoId = $scope.listaDocumentosClienteTipo[index_].docCliTipoId;
        }
        Upload.upload({
            url: ngAuthSettings.apiServiceBaseUri + "api/FileUpload/EnviarArquivo?usuarioId=" + infClientes.usuarioId + "&clienteId=" + infClientes.clienteId + "&docCliTipoId=" + docCliTipoId + "&reenvio=" + reenvio_,
            file: file_.files[0]
        }).progress(function (evt) {
            $scope.listaDocumentosClienteTipo[index_].progressbar = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";
        }).success(function (data, status, headers, config) {
            $scope.exibirModalMensagem = true;
            if (data.tipoMensagem === 1) {
                $scope.listaDocumentosClienteTipo[index_].reenvio = false;
                $scope.listaDocumentosClienteTipo[index_].nameFile = file_.value.split("\\")[file_.value.split("\\").length - 1];
                $scope.listaDocumentosClienteTipo[index_].docClienteId = data.dados.cryptoDocClienteId;
                $scope.listaDocumentosClienteTipo[index_].download = true;
                $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-success";
            } else if (data.tipoMensagem === 2) {
                $scope.listaDocumentosClienteTipo[index_].reenvio = false;
                $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-info";
            }
            else if (data.tipoMensagem === 3) {
                $scope.listaDocumentosClienteTipo[index_].reenvio = true;
                $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-info";
            }
            $scope.listaDocumentosClienteTipo[index_].status = data.mensagem;

            $scope.mostraBotaoEnviar = false;
            $scope.mostraBotaoNovo = true;
            $scope.desabilitaFile = true;

        }).error(function (data, status, headers, config) {
            $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-danger";
            if (data.message === undefined) {
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