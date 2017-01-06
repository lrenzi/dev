
app.service("listaPropostasService", ["$http", "$q", 'ngAuthSettings', 'documentoCliente', 'localStorageService', function ($http, $q, ngAuthSettings, documentoCliente, localStorageService) {

    var deferred = $q.defer();
    var infClientes = localStorageService.get('ngAuthSettings');
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var listaPropostasServiceFactory = {};


    var _retornarPropostasCliente = function (keySearch) {
        
        $http.post(serviceBase + "api/Proposta/ListarPropostas?usuarioId=" + infClientes.usuarioId + "&clientId=" + infClientes.clienteId + "&numeroProposta=" + keySearch)
        .then(function (response) {
            debugger
            if (typeof response.data === 'object') {
                //deferred.resolve(response);
                return response.data;
            } else {
                return deferred.reject(response);
            }
        },
        function (response) {
            debugger
            deferred.reject(response);
            response.data.message = "Erro ao acessar o serviço de Consulta de Documentos do Cliente.";
        });
        return deferred.promise;
    };
  
    
    listaPropostasServiceFactory.ListarPropostas = _retornarPropostasCliente;
    
    return listaPropostasServiceFactory;

}]);