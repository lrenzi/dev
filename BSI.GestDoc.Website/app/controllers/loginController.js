'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings', function ($scope, $location, authService, ngAuthSettings) {

    $scope.loginData = {
        userName: "",
        nomeUsuario: "",
        usuarioId: "",
        loginUsuario: "",
        perfilUsuario: "",
        clienteId: "",
        nomeCliente: "",
        pathDocumentosCliente: "",
        useRefreshTokens: false
    };

    $scope.message = "";

    $scope.login = function () {
        debugger;
        authService.login($scope.loginData).then(function (response) {
           
            $location.path('/listaPropostas');

        },
         function (err) {
             debugger;
             //$scope.message = err.error_description;
             alert(err.error_description);
         });
    };

    
}]);
