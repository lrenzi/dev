﻿'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings','localStorageService', function ($scope, $location, authService, ngAuthSettings,localStorageService) {
    
    $scope.loginData = {
        userName: "",
        nomeUsuario: "",
        usuarioId: "",
        loginUsuario: "",
        perfilUsuario: "",
        clienteId: "",
        nomeCliente: "",
        pathDocumentosCliente: "",
        useRefreshTokens: false,    
    };
    
    if (authService.authentication.isAuth) {
        $location.path('/listaPropostas');
    }
    $scope.message = "";

    $scope.login = function () {
        
        authService.login($scope.loginData).then(function (response) {
           
            $location.path('/listaPropostas');

        },
         function (err) {
             
             $scope.message = err.error_description;
         });
    };

    
}]);
