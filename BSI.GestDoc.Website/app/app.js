
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

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

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

app.factory("userPersistenceService", [
	"$cookies", function ($cookies) {
	    var userName = "";

	    return {
	        setCookieData: function (username) {
	            userName = username;
	            $cookies.put("userName", username);
	        },
	        getCookieData: function () {
	            userName = $cookies.get("userName");
	            return userName;
	        },
	        clearCookieData: function () {
	            userName = "";
	            $cookies.remove("userName");
	        }
	    }
	}
]);

var serviceBase = 'http://localhost:44857/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    userName: "",
    nomeCliente: "",
    nomeUsuario: "",
    usuarioId: 0,
    loginUsuario: "",
    perfilUsuario: "",
    clienteId: 0,
    pathDocumentosCliente: ""
});

app.constant('documentoCliente', {
    numeroPesquisaProposta: "",
});

//app.constant('cadastroUsuario', {
//    userNameUsuario: "",
//    nomeUsuario: "",
//    emailUsuario: ""
//});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});

app.directive('titulos', function () {
    return {
        template: '<h2>{{titulo}}</h2> <h4>{{subTitulo}}</h4>'
    };
});


app.run(function ($rootScope) {
    //Efetua a limpeza das mensagens e títulos do rootscope
    $rootScope.$on("$locationChangeStart", function (event, next, current) {
        $rootScope.exibirMensagem = false;
        $rootScope.mensagens = [];

        $rootScope.titulo = '';
        $rootScope.subTitulo = '';
    });
});

app.directive("focusOn", function ($timeout) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            scope.$on(attrs.focusOn, function (e) {
                $timeout((function () {
                    element[0].focus();
                }), 10);
            });
        }
    };
});