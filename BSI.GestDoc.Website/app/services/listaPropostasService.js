
app.service("listaPropostasService", ["$http", "$q", 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var listaPropostasServiceFactory = {};
    var _retornarPropostasCliente = function () {
        
        return $http.post(serviceBase + "api/Propostas/ListarPropostas?usuarioId=" + ngAuthSettings.usuarioId + "&clientId=" + ngAuthSettings.clienteId)
        .then(function (response) {

            if (typeof response.data === 'object') {
                debugger;
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