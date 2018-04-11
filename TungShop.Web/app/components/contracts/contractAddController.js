(function (app) {
    app.controller('contractAddController', contractAddController);

    contractAddController.$inject = ['apiService','$scope','notificationService','$state'];

    function contractAddController(apiService, $scope, notificationService,$state) {
        $scope.contract = {};
        $scope.ContractRoom = [];
        $scope.ContractStudent = [];

        $scope.Addcontract = function() {
            apiService.post('/api/contract/create', $scope.contract,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('contracts');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
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
            }, function () {
                console.log('Cannot get list Room');
            });
        }
        function loadStudentcontract() {
            // Lay thong tin cac phong
            apiService.get('/api/student/getallparents', null, function (result) {
                $scope.ContractStudent = result.data;
            }, function () {
                console.log('Cannot get list Room');
            });
        }

        $scope.ChangeStudent = function (id) {
            $scope.StudentName = "";
            if ($scope.ContractStudent && $scope.ContractStudent.length > 0) {
                var student = $scope.ContractStudent.find(function(data) {
                    return data.StudentID === id;
                });
                if (student) $scope.StudentName = student.Name;
            }
        }

        loadRoomcontract();
        loadStudentcontract();
    }

})(angular.module('tungshop.contracts'));