'use strict';
app.controller('tokensManagerController', ['$scope', 'tokensManagerService', 'utilService', function ($scope, tokensManagerService, utilService) {

    $scope.refreshTokens = [];

    tokensManagerService.getRefreshTokens().then(function (results) {
        
        $scope.refreshTokens = results.data;

    }, function (error) {
        utilService.mensagemErro(error.data.message);
    });

    $scope.deleteRefreshTokens = function (index, tokenid) {

        tokenid = window.encodeURIComponent(tokenid);

        tokensManagerService.deleteRefreshTokens(tokenid).then(function (results) {

            $scope.refreshTokens.splice(index, 1);

        }, function (error) {
            utilService.mensagemErro(error.data.message);
        });
    }

}]);