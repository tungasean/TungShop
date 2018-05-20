(function (app) {
    app.controller('contractAddController', contractAddController);

    contractAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function contractAddController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.contract = {};
        $scope.lstContract = [];
        $scope.ContractRoom = [];
        $scope.ContractStudent = [];
        $scope.StudentID = $stateParams.id;
        $scope.contract.StudentID = $scope.StudentID;
        $scope.StudentName = "";
        $scope.Sex = 0;
        

        $scope.Addcontract = function() {
            apiService.post('/api/contract/create', $scope.contract,
                function (result) {
                    var roomNew = $scope.Roomcontracts.find(function (data) {
                        return data.RoomID == $scope.contract.RoomID;
                    });
                    if (roomNew) {
                        roomNew.Amount = roomNew.Amount + 1;
                        if (roomNew.Amount <= roomNew.AmountMax)
                            UpdateRoom(roomNew);
                    }
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('approvals');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        function loadAllcontract() {
            // Lay thong tin all hop dong
            apiService.get('/api/contract/getallcontracts', null, function (result) {
                $scope.lstContract = result.data;
                if ($scope.ContractStudentTemp)
                    for (var i = 0; i < $scope.ContractStudentTemp.length; i++) {
                        var data = $scope.lstContract.find(function (da) {
                            return da.StudentID === $scope.ContractStudentTemp[i].StudentID;
                        });
                        if (!data) {
                            $scope.ContractStudent.push($scope.ContractStudentTemp[i]);
                    }
                }
            }, function () {
                console.log('Cannot get list Room');
            });
        }

        //cap nhat so sinh vien cua phong
        function UpdateRoom(room) {
            apiService.put('/api/room/update', room,
                function (result) {
                }, function (error) {
                    notificationService.displayError('Không thể cập nhật số sinh viên của phòng ' + room.RoomID);
                });
        }

        function loadRoomcontract() {
            // Lay thong tin cac phong
            apiService.get('/api/room/getallparents', null, function (result) {
                $scope.Roomcontracts = result.data;
                for (var i = 0; i < $scope.Roomcontracts.length; i++) {
                    if ($scope.Roomcontracts[i].AmountMax - $scope.Roomcontracts[i].Amount > 0 && $scope.Roomcontracts[i].Sex === $scope.Sex) {
                        $scope.ContractRoom.push($scope.Roomcontracts[i]);
                    }
                }
            }, function () {
                console.log('Cannot get list Room');
            });
        }

        function loadStudentcontract() {
            // Lay thong tin all sinh vien
            apiService.get('/api/student/getallparents', null, function (result) {
                $scope.ContractStudentTemp = result.data;
                loadAllcontract();
                if ($scope.StudentID !== "") {
                    var student = $scope.ContractStudentTemp.find(function (data) {
                        return data.StudentID === $scope.StudentID;
                    });
                    if (student) {
                        $scope.StudentName = student.Name;
                        $scope.Sex = student.Sex;
                    }
                }
                loadRoomcontract();
            }, function () {
                console.log('Cannot get list Student');
            });
        }

//        $scope.ChangeStudent = function (id) {
//            $scope.StudentName = "";
//            if ($scope.ContractStudent && $scope.ContractStudent.length > 0) {
//                var student = $scope.ContractStudent.find(function(data) {
//                    return data.StudentID === id;
//                });
//                if (student) $scope.StudentName = student.Name;
//            }
//        }
        
        loadStudentcontract();
        
    }

})(angular.module('tungshop.contracts'));