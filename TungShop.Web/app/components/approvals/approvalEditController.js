(function (app) {
    app.controller('approvalEditController', approvalEditController);

    approvalEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function approvalEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.approval = {};

        $scope.UpdateApproval = UpdateApproval;
        $scope.listType = [
            {
                ID: 0,
                Name: "Nam"
            },
            {
                ID: 1,
                Name: "Nữ"
            }
        ];

        function loadApprovalDetail() {
            apiService.get('/api/approval/getbyid/' + $stateParams.id, null, function (result) {
                $scope.approval = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateApproval() {
            apiService.put('/api/approval/update', $scope.approval,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('approvals');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadApproval() {
            apiService.get('/api/approval/getallparents', null, function (result) {
                $scopeapprovals = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadApproval();
        loadApprovalDetail();
    }

})(angular.module('tungshop.approvals'));