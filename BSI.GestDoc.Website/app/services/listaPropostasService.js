
app.service("listaPropostasService", ["$http", "$q", 'ngAuthSettings', 'documentoCliente', 'localStorageService', function ($http, $q, ngAuthSettings, documentoCliente, localStorageService) {


    var infClientes = localStorageService.get('ngAuthSettings');
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var listaPropostasServiceFactory = {};
    var _retornarPropostasCliente = function () {

        return $http.post(serviceBase + "api/Proposta/ListarPropostas?usuarioId=" + infClientes.usuarioId + "&clientId=" + infClientes.clienteId + "&numeroProposta=" + documentoCliente.numeroPesquisaProposta)
        .then(function (response) {

            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (err) {
            utilService.mensagemAlerta("Erro ao acessar o serviço de Consulta de Documentos do Cliente.")
        });
    };


    listaPropostasServiceFactory.ListarPropostas = _retornarPropostasCliente;

    return listaPropostasServiceFactory;

}]);