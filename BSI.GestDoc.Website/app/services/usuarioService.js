
app.service("usuarioService", ["$http", "$q", 'ngAuthSettings', 'localStorageService', function ($http, $q, ngAuthSettings, localStorageService) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var usuarioServiceFactory = {};
    var infClientes = localStorageService.get('ngAuthSettings');

    var _cadastrarUsuario = function (userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario) {

        if (userNameUsuario.trim() == "" || nomeUsuario.trim() == "" || emailUsuario.trim() == "" ||
           perfilUsuario.usuPerfilId == "" || senhaUsuario.trim() == "") {
            return
        }

        return $http.post(serviceBase + "api/Usuario/CadastrarUsuario?userNameUsuario="
            + userNameUsuario + "&nomeUsuario=" + nomeUsuario + "&emailUsuario=" + emailUsuario
            + "&perfilUsuario=" + perfilUsuario.usuPerfilId + "&senhaUsuario=" + senhaUsuario + "&clientId=" + infClientes.clienteId)
        .then(function (response) {
            if (typeof response.data === 'object' || typeof response.data === 'string') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (data, status, headers, config) {
            data.message = "Erro ao acessar o serviço de Cadastro de Usuarios.";
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
            data.message = "Erro ao acessar o serviço de Cadastro de Usuarios.";
        });
    };

    var _consultarUsuarios = function (usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId) {
        
        return $http.post(serviceBase + "api/Usuario/Consultar?usuarioId=" + usuarioId + "&usuarioLogin="
            + usuarioLogin + "&clienteId=" + '' + "&usuarioNome=" + usuarioNome + "&usuarioEmail=" + usuarioEmail +
            "&usuarioSenha=" + usuarioAtivo + "&usuarioAtivo=" + '' + "&usuPerfilId=" + usuPerfilId + "&usuClienteId=" + usuClienteId)
        .then(function (response) {
            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (data, status, headers, config) {
            data.message = "Erro ao acessar o serviço de Consulta de Usuarios.";
        });
    };

    var _alterarUsuario = function (usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId) {

        return $http.post(serviceBase + "api/Usuario/Alterar?usuarioId=" + usuarioId + "&usuarioLogin="
            + usuarioLogin + "&clienteId=" + '' + "&usuarioNome=" + usuarioNome + "&usuarioEmail=" + usuarioEmail +
              "&usuarioSenha=" + usuarioSenha + "&usuarioAtivo=" + usuarioAtivo + "&usuPerfilId=" + usuPerfilId + "&usuClienteId=" + usuClienteId)
        .then(function (response) {
            
            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (data, status, headers, config) {
            data.message = "Erro ao acessar o serviço de Alterar de Usuario.";
        });
    };

    var _abrirTelaAlteracao = function (usuarioId, usuarioLogin, usuarioNome, usuarioEmail, usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId) {

        return $http.post(serviceBase + "api/Usuario/Consultar?usuarioId=" + usuarioId + "&usuarioLogin="
            + usuarioLogin + "&clienteId=" + '' + "&usuarioNome=" + usuarioNome + "&usuarioEmail=" + usuarioEmail +
            "&usuarioSenha=" + usuarioAtivo + "&usuarioAtivo=" + '' + "&usuPerfilId=" + usuPerfilId + "&usuClienteId=" + usuClienteId)
        .then(function (response) {
            
            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (data, status, headers, config) {
            data.message = "Erro ao acessar o serviço de Consulta de Usuario.";
        });
    };


    usuarioServiceFactory.CadastrarUsuario = _cadastrarUsuario;
    usuarioServiceFactory.ConsultaPerfil = _consultaPerfil;
    usuarioServiceFactory.ConsultarUsuario = _consultarUsuarios;
    usuarioServiceFactory.AlterarUsuario = _alterarUsuario;
    usuarioServiceFactory.AbrirTelaAlteracao = _abrirTelaAlteracao;

    return usuarioServiceFactory;

}]);