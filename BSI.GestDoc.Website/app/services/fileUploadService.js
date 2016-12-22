
app.service("fileUploadService", ["$http", "$q", 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var fileUploadServiceFactory = {};

    var _retornarDocumentoClienteTipo = function () {
        
        return $http.post(serviceBase + "api/FileUpload/RetornarDocumentoClienteTipo", { ClienteId: 1 })
        .then(function (response) {
            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (data, status, headers, config) {
            alert("Erro ao acessar o serviço de Consulta de Tipo de Documento.")
        });
    };

    var _retornarArquivo = function (docClienteId_, nameFile_) {
        
        $http({
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
            alert("Erro ao acessar o serviço de Consulta de Arquivo.")
        });

    }

    fileUploadServiceFactory.RetornarDocumentoClienteTipo = _retornarDocumentoClienteTipo;
    fileUploadServiceFactory.RetornarArquivo = _retornarArquivo;

    return fileUploadServiceFactory;


}]);