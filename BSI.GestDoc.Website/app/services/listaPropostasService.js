
app.service("listaPropostasService", ["$http", "$q", 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var listaPropostasServiceFactory = {};
    
    var _retornarPropostasCliente = function () {
        
        return $http.post(serviceBase + "api/Propostas/ListarPropostas?usuarioId=" + ngAuthSettings.usuarioId + "&clientId=" + ngAuthSettings.clientId)
        .then(function (response) {

            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
                
            }
        },
        function (response) {
            return $q.reject(response.data);
        });
    };
  

    listaPropostasServiceFactory.ListarPropostas = _retornarPropostasCliente;
    
    return listaPropostasServiceFactory;

}]);