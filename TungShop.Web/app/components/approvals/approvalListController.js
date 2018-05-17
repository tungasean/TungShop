(function (app) {
    app.controller("approvalListController", approvalListController);

    approvalListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$state'];

    function approvalListController($scope, apiService, notificationService, $ngBootbox,$state) {
        $scope.approvals = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getapprovals = getapprovals;
        $scope.contract = {};
        $scope.ContractRoom = [];
        $scope.keyword = '';
        $scope.student = {};


        $scope.search = function () {
            getapprovals();
        };

        function getapprovals(page) {
            loadRoomcontract();
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/approval/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                var lst = [];
                if (result.data.Items && result.data.TotalCount > 0)
                    for (var i = 0; i < result.data.TotalCount; i++) {
                        if (result.data.Items[i] && result.data.Items[i].Status == 1)
                        {
                            if (result.data.Items[i].Sex === 0)
                                result.data.Items[i].SexString = 'Nam';
                            else
                                result.data.Items[i].SexString = 'Nữ';

                            lst.push(result.data.Items[i]);
                        }
                    }
                $scope.approvals = lst;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load approval failed.');
            });
        };

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

        $scope.deleteApproval = deleteApproval;

        function deleteApproval(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/approval/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    $scope.search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        };

        $scope.Approval = function (id) {
            if (!$scope.ContractRoom) {
                notificationService.displayError('Đã hết phòng, không thể thêm sinh viên vào ký túc xá');
                return;
            }
            var value = null;
            if ($scope.approvals && $scope.approvals.length > 0)
                for (var i = 0; i < $scope.approvals.length; i++) {
                    if ($scope.approvals[i].ApprovalId === id) {
                        value = $scope.approvals[i];
                        break;
                    }
                }
            if (value !== null) {
                value.Status = 2;
                apiService.put('/api/approval/update', value,
                    function (result) {
                        notificationService.displaySuccess(result.data.Name + ' đã phê duyệt thành công.');
                        $scope.search();
                        //Them vao danh sach sinh vien
                        $scope.student.StudentID = value.StudentId;
                        $scope.student.Name = value.Name;
                        $scope.student.BirthDay = value.BirthDay;
                        $scope.student.Sex = value.Sex;
                        $scope.student.CardNo = value.CardNo;
                        $scope.student.Address = value.Address;
                        apiService.post('/api/student/create', $scope.student,
                            function (result) {
                                //Neu them moi sinh vien thanh cong thi them moi hop dong cho sinh vien

                                //Lay ra phong phu hop cho sinh vien
                                var resultValue = {};
                                if ($scope.student.Sex == 0) {
                                    
                                    for (var i = 0; i < $scope.ContractRoom.length; i++) {
                                        if ($scope.ContractRoom[i].Sex == 0) {
                                            resultValue = $scope.ContractRoom[i];
                                            break;
                                        }
                                    }
                                } else {
                                    for (var i = 0; i < $scope.ContractRoom.length; i++) {
                                        if ($scope.ContractRoom[i].Sex == 1) {
                                            resultValue = $scope.ContractRoom[i];
                                            break;
                                        }
                                    }
                                }
                                //Them hop dong

                                $state.go('add_contract',{id:value.StudentId});
                                return;
                                $scope.contract.RoomID = resultValue.RoomID;
                                $scope.contract.StudentID = value.StudentId;
                                $scope.contract.TimeSign = new Date(Date.now());
                                $scope.contract.Term = 12;
                                $scope.contract.Status = 1;

                                apiService.post('/api/contract/create', $scope.contract,
                                    function (result) {
                                        // sau khi them hop dong thanh cong thi se cap nhat lai so nguoi cua Phong
                                        $scope.room = resultValue;
                                        $scope.room.Amount = $scope.room.Amount + 1;
                                        apiService.put('/api/room/update', $scope.room,
                                            function (result) {
                                            }, function (error) {
                                                notificationService.displayError('Cập nhật không thành công số người của phòng.');
                                            });
                                    }, function (error) {
                                        notificationService.displayError('Thêm mới không thành công hợp đồng.');
                                    });

                            }, function (error) {
                                notificationService.displayError('Thêm mới sinh viên thất bại');
                            });
                    }, function (error) {
                        notificationService.displayError('Phê duyệt không thành công.');
                    });
            }

        };

        $scope.RejectApproval = function (id) {
            var value = null;
            if ($scope.approvals && $scope.approvals.length > 0)
                for (var i = 0; i < $scope.approvals.length; i++) {
                    if ($scope.approvals[i].ApprovalId === id) {
                        value = $scope.approvals[i];
                        break;
                    }
                }
            if (value !== null) {
                value.Status = 3;
                apiService.put('/api/approval/update', value,
                    function (result) {
                        notificationService.displaySuccess(result.data.Name + ' đã từ chối thành công.');
                        $scope.search();
                    }, function (error) {
                        notificationService.displayError('Từ chối không thành công.');
                    });
            }

        };

        $scope.getapprovals();
    }
})(angular.module("tungshop.approvals"));