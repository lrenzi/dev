
app.service("cadastrarUsuarioService", ["$http", "$q", 'ngAuthSettings', 'localStorageService', function ($http, $q, ngAuthSettings, localStorageService) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;   

    var _cadastrarUsuario = function () {

        return $http.post(serviceBase + "api/Usuario/CadastrarUsuario?userName=" + infClientes.usuarioId + "&nomeUsuario=" + infClientes.clienteId + "&emailUsuario=" + documentoCliente.numeroPesquisaProposta)
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


}]);