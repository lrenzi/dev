'use strict';
app.controller('showItemsModalCtrl', ['$scope', '$uibModalInstance', 'items', 'custom', "fileUploadService", 'utilService', function ($scope, $uibModalInstance, items, custom, fileUploadService,utilService) {

    init();

    function init() {
        $scope.items = items;
        $scope.custom = custom;
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.RetornarArquivo = function (docClienteId_, fileName_) {
        utilService.limparMensagemModal();
        fileUploadService.RetornarArquivo(docClienteId_, fileName_).then(function (data) {
        }, function (error) {
            utilService.mensagemModalErro(error.data.message);
        });
    };

}]);