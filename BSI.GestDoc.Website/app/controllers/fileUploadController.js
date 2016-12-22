'use strict';
app.controller("fileUploadController", ["$scope", "$routeParams", "$location", "fileUploadService", 'Upload', 'ngAuthSettings', function ($scope, $routeParams, $location, fileUploadService, Upload, ngAuthSettings) {

    $scope.mostraBotaoEnviar = true;
    $scope.mostraBotaoNovo = false;
    $scope.desabilitaFile = false;

    $scope.IniciarTela = function () {
        
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            $scope.listaDocumentosClienteTipo = data;
            angular.forEach($scope.listaDocumentosClienteTipo, function (value, key) {
                $scope.listaDocumentosClienteTipo[key].nameFile = "";
                $scope.listaDocumentosClienteTipo[key].docClienteId = 0;
                $scope.listaDocumentosClienteTipo[key].status = "";
                $scope.listaDocumentosClienteTipo[key].progressbar = "";
                $scope.listaDocumentosClienteTipo[key].reenvio = false;
                $scope.listaDocumentosClienteTipo[key].download = false;
            });
        }, function (error) {
            alert(error.data.message);
        });
        $scope.mostraBotaoEnviar = true;
        $scope.mostraBotaoNovo = false;
        $scope.desabilitaFile = false;
        

        
     
        //$scope.progressbar = new Array($scope.listaDocumentosClienteTipo.length);
    }

    $scope.ConsultarArquivos = function () {
        
        $scope.listaDocumentosClienteTipo = fileUploadService.RetornarDocumentoClienteTipo().then(function (data) {
            
            $scope.listaDocumentosClienteTipo = data;
            angular.forEach($scope.listaDocumentosClienteTipo, function (value, key) {
                $scope.listaDocumentosClienteTipo[key].nameFile = "";
                $scope.listaDocumentosClienteTipo[key].docClienteId = 0;
                $scope.listaDocumentosClienteTipo[key].status = "";
                $scope.listaDocumentosClienteTipo[key].progressbar = "";
                $scope.listaDocumentosClienteTipo[key].reenvio = false;
                $scope.listaDocumentosClienteTipo[key].download = false;
                $scope.listaDocumentosClienteTipo[key].cssclass = "";
            });
            
            
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
            //var file_ = $scope.listaDocumentosClienteTipo[contador].nameFile;
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
            
            //document.getElementById('idProgressbar_' + docCliTipoId).innerText = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";
            //$scope.progressbar[docCliTipoId].innerText = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";
            $scope.listaDocumentosClienteTipo[index_].progressbar = parseInt(100.0 * evt.loaded / evt.total, 10) + " %";

        }).success(function (data, status, headers, config) {

            if (data.tipoErro == 2) {
                //angular.element(document.querySelector('#reenvio_' + $scope.listaDocumentosClienteTipo[index_].docCliTipoId)).value = "S";
                //document.getElementById('reenvio_' + $scope.listaDocumentosClienteTipo[index_].docCliTipoId).value = "S";
                $scope.listaDocumentosClienteTipo[index_].reenvio = true;
                $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-info";
            } else {
                $scope.listaDocumentosClienteTipo[index_].reenvio = false;
                
                //document.getElementById("idNameFile_" + docCliTipoId).value = file_.value.split("\\")[file_.value.split("\\").length - 1];
                $scope.listaDocumentosClienteTipo[index_].nameFile = file_.value.split("\\")[file_.value.split("\\").length - 1];
                //document.getElementById("idDocClienteId_" + docCliTipoId).value = data.dados.docClienteId;
                
                $scope.listaDocumentosClienteTipo[index_].docClienteId = data.dados.docClienteId;
                $scope.listaDocumentosClienteTipo[index_].download = true;
                $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-success";
            }
            //document.getElementById('idStatus_' + docCliTipoId).innerText = data.mensagem;
            $scope.listaDocumentosClienteTipo[index_].status = data.mensagem;
            
            
            

            $scope.mostraBotaoEnviar = false;
            $scope.mostraBotaoNovo = true;
            $scope.desabilitaFile = true;


        }).error(function (data, status, headers, config) {

            $scope.listaDocumentosClienteTipo[index_].cssclass = "bg-danger";
            if (data.message == undefined)
                //document.getElementById('idStatus_' + docCliTipoId).innerText = data;
                $scope.listaDocumentosClienteTipo[index_].status = data;
            else
                //document.getElementById('idStatus_' + docCliTipoId).innerText = data.message;
                $scope.listaDocumentosClienteTipo[index_].status = data.message;
        });
    };

}]);