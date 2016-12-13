var app = angular.module('app', ['ngRoute', 'angular.directives-round-progress']);
debugger;
alert("app.j");
app.config(function($routeProvider, $locationProvider) {
    debugger;
    alert("app2.j");
    $routeProvider
		.when('/login', {
		    templateUrl: 'views/login.html',
		    controller: 'LoginCtrl'
		})
		.when('/FileUpload/Index', {
		    templateUrl: 'views/FileUpload/Index.html',
		    controller: 'FileUploadController'
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

		.otherwise({ redirectTo: '/' });

});

