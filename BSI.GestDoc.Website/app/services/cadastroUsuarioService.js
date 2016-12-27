
app.service("cadastroUsuarioService", ["$http", "$q", 'ngAuthSettings', 'localStorageService', function ($http, $q, ngAuthSettings, localStorageService) {
    
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var cadastroUsuarioServiceFactory = {};
    var infClientes = localStorageService.get('ngAuthSettings');

    var _cadastrarUsuario = function (userNameUsuario, nomeUsuario, emailUsuario,perfilUsuario, senhaUsuario) {
        
        if (userNameUsuario.trim() == "" || nomeUsuario.trim() == "" || emailUsuario.trim() == "" ||
           perfilUsuario.usuPerfilId == "" || senhaUsuario.trim() == "") {
            return
        }

        return $http.post(serviceBase + "api/Usuario/CadastrarUsuario?userNameUsuario="
            + userNameUsuario + "&nomeUsuario=" + nomeUsuario + "&emailUsuario=" + emailUsuario
            + "&perfilUsuario=" + perfilUsuario.usuPerfilId + "&senhaUsuario=" + senhaUsuario + "&clientId=" + infClientes.clienteId)
        .then(function (response) {

            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (data, status, headers, config) {
            alert("Erro ao acessar o serviço de Cadastro de Usuarios.")
        });        
    };



    var _consultaPerfil = function (usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao) {
        
        return $http.post(serviceBase + "api/UsuarioPerfil/Consultar?usuPerfilId="
            + '' + "&clienteId=" + '' + "&usuPerfilNome=" + '' + "&usuPerfilDescricao=" + '')
        .then(function (response) {
            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (data, status, headers, config) {
            alert("Erro ao acessar o serviço de Cadastro de Usuarios.")
        });
    };

    cadastroUsuarioServiceFactory.CadastrarUsuario = _cadastrarUsuario;
    cadastroUsuarioServiceFactory.ConsultaPerfil = _consultaPerfil;

    return cadastroUsuarioServiceFactory;

}]);