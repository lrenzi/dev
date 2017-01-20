
app.service("listaPropostasService", ["$http", "$q", 'ngAuthSettings', 'localStorageService', function ($http, $q, ngAuthSettings, localStorageService) {


    var infClientes = localStorageService.get('ngAuthSettings');
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var listaPropostasServiceFactory = {};

    var _retornarPropostasCliente = function () {

        var deferred = $q.defer();

        $http.post(serviceBase + "api/Proposta/ListarPropostas?usuarioId=" + infClientes.usuarioId + "&clientId=" + infClientes.clienteId )
        .then(function (response) {
            deferred.resolve(response);            
        }, function (response) {
            deferred.reject(response);
            response.data = "Erro ao executar o serviço de Consulta de Propostas, entre em contato com um administrador.";
        });
        return deferred.promise;
    };

    var _consultaProposta = function (numeroProposta) {

        var deferred = $q.defer();
        $http.post(serviceBase + "api/Proposta/ConsultaProposta?usuarioId=" + infClientes.usuarioId + "&clientId=" + infClientes.clienteId + "&numeroProposta=" + numeroProposta )
        .then(function (response) {
            deferred.resolve(response);
        }, function (response) {
            deferred.reject(response);
            response.data = "Erro ao executar o serviço de Consulta de Proposta, entre em contato com um administrador.";
        });
        return deferred.promise;
    };


    listaPropostasServiceFactory.ListarPropostas = _retornarPropostasCliente;
    listaPropostasServiceFactory.ConsultaProposta = _consultaProposta

    return listaPropostasServiceFactory;

}]);