var app = angular.module('app', ['ngRoute', 'angular.directives-round-progress']);
app.config(function ($routeProvider, $locationProvider) {
    debugger;
    $routeProvider
        .when('/', {
            templateUrl: 'views/Documento/Index.html',
            controller: 'DocumentoController'
        })
		.when('/login', {
		    templateUrl: 'views/login.html',
		    controller: 'LoginCtrl'
		})
		.when('/FileUpload', {
		    templateUrl: 'views/FileUpload/Index.html',
		    controller: 'FileUploadController'
		})
        .when('/Documento', {
            templateUrl: 'views/Documento/Index.html',
            controller: 'DocumentoController'
        })
		/*.when('/funcionarios', {
		    templateUrl: 'views/funcionarios/funcionarios.html',
		    controller: 'FuncionariosCtrl'
		})
		.when('/funcionarios/detalhes/:id', {
		    templateUrl: 'views/funcionarios/detalhes.html',
		    controller: 'FuncionariosCtrl'
		})
		.when('/cargos', {
		    templateUrl: 'views/cargos/cargos.html',
		    controller: 'CargosCtrl'
		})
		.when('/cargos/alterar/:id', {
		    templateUrl: 'views/cargos/alterar.html',
		    controller: 'CargosCtrl'
		})
		.when('/cargos/incluir', {
		    templateUrl: 'views/cargos/incluir.html',
		    controller: 'CargosCtrl'
		})
		.when('/cargos/excluir/:id', {
		    templateUrl: 'views/cargos/excluir.html',
		    controller: 'CargosCtrl'
		})*/

		.otherwise({
		    redirectTo: '/'
		});

});

