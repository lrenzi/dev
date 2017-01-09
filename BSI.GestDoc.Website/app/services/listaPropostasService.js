
app.service("listaPropostasService", ["$http", "$q", 'ngAuthSettings', 'documentoCliente', 'localStorageService', function ($http, $q, ngAuthSettings, documentoCliente, localStorageService) {


    var infClientes = localStorageService.get('ngAuthSettings');
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var listaPropostasServiceFactory = {};
    var _retornarPropostasCliente = function () {

        var deferred = $q.defer();

        $http.post(serviceBase + "api/Proposta/ListarPropostas?usuarioId=" + infClientes.usuarioId + "&clientId=" + infClientes.clienteId + "&numeroProposta=" + documentoCliente.numeroPesquisaProposta)
        .then(function (response) {
            deferred.resolve(response);            
        }, function (response) {
            deferred.reject(response);
            response.data.message = "Erro ao executar o serviço de Cadastrar Usuário, entre em contato com um administrador.";
        });
        return deferred.promise;
    };


    listaPropostasServiceFactory.ListarPropostas = _retornarPropostasCliente;

    return listaPropostasServiceFactory;

}]);