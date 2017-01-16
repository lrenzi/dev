'use strict';
app.controller('erroModal', ['$scope', '$uibModalInstance', 'html', function ($scope, $uibModalInstance, html) {

    init();

    function init() {
        $scope.html = decodeURI(html);
    }



    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}]);