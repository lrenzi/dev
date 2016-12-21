
app.service("listaPropostasService", ["$http", "$q", 'ngAuthSettings', 'documentoCliente', function ($http, $q, ngAuthSettings, documentoCliente) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var listaPropostasServiceFactory = {};
    var _retornarPropostasCliente = function () {
        
        return $http.post(serviceBase + "api/Proposta/ListarPropostas?usuarioId=" + ngAuthSettings.usuarioId + "&clientId=" + ngAuthSettings.clienteId + "&numeroProposta=" + documentoCliente.numeroPesquisaProposta)
        .then(function (response) {

            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);                
            }
        },
        function (data, status, headers, config) {
            alert("Erro ao acessar o serviço de Consulta de Documentos do Cliente.")
        });
    };
  
    
    listaPropostasServiceFactory.ListarPropostas = _retornarPropostasCliente;
    
    return listaPropostasServiceFactory;

}]);