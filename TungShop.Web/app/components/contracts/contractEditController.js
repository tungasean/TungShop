(function (app) {
    app.controller('contractEditController', contractEditController);

    contractEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function contractEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.contract = null; //hop dong moi
        $scope.contractOld = null; //hop dong cu
        $scope.ContractStudent = null; //thong tin sinh vien cua hop dong
        $scope.ContractRoom = []; //Danh sach cac phong con cho trong
        $scope.Roomcontracts = []; // Danh sach tat ca cac phong
        $scope.StudentName = ""; //Ten sinh vien

        $scope.Updatecontract = Updatecontract;
        

        //lay thong tin hop dong
        function loadcontractDetail() {
            apiService.get('/api/contract/getbyid/' + $stateParams.id, null, function (result) {
                $scope.contract = result.data;
                $scope.contractOld = result.data;
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

        //cap nhat so sinh vien cua phong
        function UpdateRoom(room) {
            apiService.put('/api/room/update', room,
                function (result) {
                    $state.go('rooms');
                }, function (error) {
                    notificationService.displayError('Không thể cập nhật số sinh viên của phòng ' + room.RoomID);
                });
        }

        //Cap nhat hop dong
        function Updatecontract() {
            apiService.put('/api/contract/update', $scope.contract,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    //Neu nhu sinh vien chuyen phong
                    if ($scope.contract.RoomID != $scope.contractOld.RoomID) {

                        //bot sinh vien do khoi phong cu
                        var roomOld = $scope.Roomcontracts.find(function (data) {
                            return data.RoomID == $scope.contractOld.RoomID;
                        });
                        if (roomOld) {
                            roomOld.Amount = roomOld.Amount - 1 < 0 ? 0 : roomOld.Amount - 1;
                            UpdateRoom(roomOld);
                        }

                        //Them sinh vien vao phong moi
                        var roomNew = $scope.Roomcontracts.find(function (data) {
                            return data.RoomID == $scope.contract.RoomID;
                        });
                        if (roomNew) {
                            roomNew.Amount = roomNew.Amount + 1;
                            if (roomNew.Amount <= roomNew.AmountMax)
                                UpdateRoom(roomNew);
                        }
                    }
                    $state.go('contracts');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        // Lay thong tin sinh vien
        function loadcontract() {
            apiService.get('/api/student/getallparents', null, function (result) {
                $scope.ContractStudent = result.data;
                loadcontractDetail();
            }, function () {
                console.log('Cannot get list Room');
            });
        }

        // Lay thong tin cac phong
        function loadRoomcontract() {
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