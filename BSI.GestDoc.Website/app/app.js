
var app = angular.module('AngularAuthApp', ['ngRoute', 'ui.bootstrap', 'LocalStorageModule', 'angular-loading-bar', 'ngFileUpload', 'angularUtils.directives.dirPagination', 'ngSanitize']);

app.config(function ($routeProvider) {

    /*$routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });*/

    $routeProvider.when("/", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });
    /*
    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });*/

    /*$routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });*/

    /*$routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });*/

    $routeProvider.when("/uploadArquivos", {
        controller: "uploadArquivosController",
        templateUrl: "/app/views/uploadArquivos.html"
    });

    $routeProvider.when("/listaPropostas", {
        controller: "listaPropostasController",
        templateUrl: "/app/views/listaPropostas.html"
    });

    $routeProvider.when("/cadastrarUsuario", {
        controller: "usuarioController",
        templateUrl: "/app/views/cadastroUsuario.html"
    });

    $routeProvider.when("/consultarUsuario", {
        controller: "usuarioController",
        templateUrl: "/app/views/listaUsuarios.html"
    });

    $routeProvider.otherwise({ redirectTo: "/login" });

});



var serviceBase = 'http://localhost:44857/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    userName: "",
    nomeCliente: "",
    nomeUsuario: "",
    usuarioId: 0,
    loginUsuario: "",
    perfilUsuario: "",
    clienteId: 0
});

app.constant('documentoCliente', {
    numeroPesquisaProposta: ""
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

app.run(function ($rootScope) {
    //Efetua a limpeza das mensagens e títulos do rootscope
    $rootScope.$on("$locationChangeStart", function (event, next, current) {
        $rootScope.exibirMensagem = false;
        $rootScope.mensagens = [];

        $rootScope.titulo = '';
        $rootScope.subTitulo = '';
    });
});
