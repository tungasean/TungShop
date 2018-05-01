(function (app) {
    app.controller('roomAddController', roomAddController);

    roomAddController.$inject = ['apiService','$scope','notificationService','$state'];

    function roomAddController(apiService, $scope, notificationService,$state) {
        $scope.room = {
            Amount: 0
        };
        var day = Date.now();
        var date = new Date(day);
        $scope.NewElectricity = {
            Month : (date.getMonth() + 1).toString()
    };

        $scope.AddRoom = AddRoom;

        function AddRoom() {
            if ($scope.room.Type === 'Nam')
                $scope.room.Sex = 0;
            else {
                $scope.room.Sex = 1;
            }
            apiService.post('/api/room/create', $scope.room,
                function (result) {
                    $scope.NewElectricity.RoomID = $scope.room.RoomID;
                    apiService.post('/api/electricityWater/create', $scope.NewElectricity,
                        function (resultElectricity) {
                            notificationService.displaySuccess('Phòng ' +  result.data.RoomID + ' đã được thêm mới thông tin điện nước.');
                        }, function (error) {
                            notificationService.displayError('Chưa tạo được thông tin điện nước cho phòng');
                        });
                    notificationService.displaySuccess('Phòng ' + result.data.RoomID + ' đã được thêm mới.');
                    $state.go('rooms');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
            
        }
        function loadParentRoom() {
            apiService.get('/api/room/getallparents', null, function (result) {
                $scope.parentRooms = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentRoom();
    }

})(angular.module('tungshop.rooms'));