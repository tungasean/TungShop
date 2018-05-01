(function (app) {
    app.controller('approvalAddController', approvalAddController);

    approvalAddController.$inject = ['apiService','$scope','notificationService','$state'];

    function approvalAddController(apiService, $scope, notificationService,$state) {
        $scope.approval = {
            Status: 1,
        }
       
        $scope.AddApproval = AddApproval;

        function AddApproval() {
            apiService.post('/api/approval/create', $scope.approval,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('approvals');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        function loadParentApproval() {
            apiService.get('/api/approval/getallparents', null, function (result) {
                $scope.parentApprovals = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentApproval();
    }

})(angular.module('tungshop.approvals'));