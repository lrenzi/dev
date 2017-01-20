
app.service("fileUploadService", ["$http", "$q", 'ngAuthSettings', 'localStorageService', function ($http, $q, ngAuthSettings, localStorageService) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var fileUploadServiceFactory = {};

    var _retornarDocumentoClienteTipo = function () {

        var deferred = $q.defer();
        var infClientes = localStorageService.get('ngAuthSettings');

        $http.post(serviceBase + "api/FileUpload/RetornarDocumentoClienteTipo", { ClienteId: infClientes.clienteId })
        .then(function (response) {
            deferred.resolve(response);
        },
        function (response, status, headers, config) {
            deferred.reject(response);
            response.data = "Erro ao acessar o serviço de Consulta de Tipo de Documento, entre em contato com um administrador.";
        });

        return deferred.promise;
    };

    var _retornarArquivo = function (docClienteId_, nameFile_) {
        
        return $http({
            url: serviceBase + "api/FileUpload/RetornarArquivo?docClienteId=" + docClienteId_,
            method: 'POST',
            params: {},
            headers: {
                'Content-type': 'application/pdf',
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {
            
            var file = new Blob([data], {
                type: 'application/pdf'
            });

            saveAs(file, nameFile_);
            
        }).error(function (data, status, headers, config) {
            switch (status) {
                case 409:
                    data.message = "Arquivo não encontrado no servidor, entre em contato com um administrador.";
                    break;
                default:
                    data.message = "Erro ao acessar o serviço de Consulta de Arquivo.";
                    break;
            }
        });

    }

    fileUploadServiceFactory.RetornarDocumentoClienteTipo = _retornarDocumentoClienteTipo;
    fileUploadServiceFactory.RetornarArquivo = _retornarArquivo;

    return fileUploadServiceFactory;


}]);