(function (app) {
    app.controller('contractEditController', contractEditController);

    contractEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function contractEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.contract = {};

        $scope.Updatecontract = Updatecontract;
//        $scope.GetSeoTitle = GetSeoTitle;
//
//        function GetSeoTitle() {
//            $scope.contract.Alias = commonService.getSeoTitle($scope.contract.Name);
//        }

        function loadcontractDetail() {
            apiService.get('/api/contract/getbyid/' + $stateParams.id, null, function (result) {
                $scope.contract = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function Updatecontract() {
            apiService.put('/api/contract/update', $scope.contract,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('contracts');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadcontract() {
            apiService.get('/api/contract/getallparents', null, function (result) {
                $scopecontracts = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadcontract();
        loadcontractDetail();
    }

})(angular.module('tungshop.contracts'));