
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ngFileUpload']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.when("/fileUpload", {
        controller: "fileUploadController",
        templateUrl: "/app/views/fileUpload.html"
    });

    $routeProvider.when("/fileUploadTeste", {
        controller: "fileUploadTesteController",
        templateUrl: "/app/views/fileUploadTeste.html"
    });

    $routeProvider.when("/listaPropostas", {
        controller: "listaPropostasController",
        templateUrl: "/app/views/listaPropostas.html"
    });

    $routeProvider.otherwise({ redirectTo: "/login" });

});

var serviceBase = 'http://localhost:44857/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 1,
    usuarioId: 1,
    nomeCliente: "",
    nomeUsuario:"Wesley Klein",
    loginUsuario: "Wesley",
    perfilUsuario: "",
    clienteId: "",
    pathDocumentosCliente: ""
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


