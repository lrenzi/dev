
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ngFileUpload', 'angularUtils.directives.dirPagination']);

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

    $routeProvider.when("/fileUpload", {
        controller: "fileUploadController",
        templateUrl: "/app/views/fileUpload.html"
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

app.filter('customFilter', function () {
    return function (items, filter, columns) {
        if (filter == "" || filter == undefined) {
            return items;
        }
        var exists = false;
        var filtered = [];
        var nameColumn = "";
        var propertyColum = [];
        var valuePropoertyColum;
        columns = columns.split("|");
        for (var i = 0; i < items.length; i++) {
            exists = false;
            for (var j = 0; j < columns.length; j++) {
                if (!exists) {
                    nameColumn = columns[j];
                    if (nameColumn.indexOf(".") > -1) {
                        propertyColum = nameColumn.split(".");
                        valuePropoertyColum = items[i][propertyColum[0]];
                        for (var k = 1; k < propertyColum.length; k++) {
                            valuePropoertyColum = valuePropoertyColum[propertyColum[k]];
                        }
                    } else {
                        valuePropoertyColum = items[i][nameColumn];
                    }
                    if (valuePropoertyColum != undefined) {
                        if (valuePropoertyColum.indexOf(filter) > -1) {
                            filtered.push(items[i]);
                            exists = true;
                        }
                    }
                }
            }
        }
        return filtered;
    };
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
    numeroPesquisaProposta: "",
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