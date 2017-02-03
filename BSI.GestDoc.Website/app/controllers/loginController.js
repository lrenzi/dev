'use strict';
app.controller('loginController', ['$uibModal', '$scope', '$location', 'authService', 'ngAuthSettings', 'localStorageService', function ($uibModal, $scope, $location, authService, ngAuthSettings, localStorageService) {

    $scope.loginData = {
        userName: "",
        nomeUsuario: "",
        usuarioId: "",
        loginUsuario: "",
        perfilUsuario: "",
        clienteId: "",
        nomeCliente: "",
        useRefreshTokens: false
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
              if (err === null) {
                  $scope.message = "Erro desconhecido.";
              }
              else if (err.error_description === undefined) {
                  var modalInstance = $uibModal.open({
                      ariaLabelledBy: 'modal-title',
                      ariaDescribedBy: 'modal-body',
                      controllerAs: 'vm',
                      controller: 'erroModal',
                      templateUrl: '/app/views/modal/erroModal.html',
                      resolve: {
                          html: function () {
                              return encodeURI(err);
                          }
                      }
                  });
              } else {
                  $scope.message = err.error_description;
              }

          });
    };


}]);
