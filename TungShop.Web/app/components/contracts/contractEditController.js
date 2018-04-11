(function (app) {
    app.controller('contractEditController', contractEditController);

    contractEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function contractEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.contract = null;
        $scope.ContractStudent = null;
        $scope.ContractRoom = [];
        $scope.StudentName = "";

        $scope.Updatecontract = Updatecontract;
//        $scope.GetSeoTitle = GetSeoTitle;
//
//        function GetSeoTitle() {
//            $scope.contract.Alias = commonService.getSeoTitle($scope.contract.Name);
//        }

        function loadcontractDetail() {
            apiService.get('/api/contract/getbyid/' + $stateParams.id, null, function (result) {
                $scope.contract = result.data;
                if ($scope.ContractStudent && $scope.contract) {
                    var student = $scope.ContractStudent.find(function(data) {
                        return data.StudentID == $scope.contract.StudentID;
                    });
                    if (student) {
                        $scope.StudentName = student.Name;
                    }
                }
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
            // Lay thong tin sinh vien
            apiService.get('/api/student/getallparents', null, function (result) {
                $scope.ContractStudent = result.data;
                loadcontractDetail();
            }, function () {
                console.log('Cannot get list Room');
            });
        }
        function loadRoomcontract() {
            // Lay thong tin cac phong
            apiService.get('/api/room/getallparents', null, function (result) {
                $scope.Roomcontracts = result.data;
                for (var i = 0; i < $scope.Roomcontracts.length; i++) {
                    if ($scope.Roomcontracts[i].AmountMax - $scope.Roomcontracts[i].Amount > 0) {
                        $scope.ContractRoom.push($scope.Roomcontracts[i]);
                    }
                }
                loadcontract();
            }, function () {
                console.log('Cannot get list Room');
            });
        }
        loadRoomcontract();
    }

})(angular.module('tungshop.contracts'));