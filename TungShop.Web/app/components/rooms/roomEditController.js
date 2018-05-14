(function (app) {
    app.controller('roomEditController', roomEditController);

    roomEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function roomEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.room = {};
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

        $scope.UpdateRoom = UpdateRoom;
//        

        function loadRoomDetail() {
            apiService.get('/api/room/getbyid/' + $stateParams.id, null, function (result) {
                $scope.room = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateRoom() {
            apiService.put('/api/room/update', $scope.room,
                function (result) {
                    notificationService.displaySuccess('Phòng ' + $scope.room + ' đã được cập nhật.');
                    $state.go('rooms');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadRoom() {
            apiService.get('/api/room/getallparents', null, function (result) {
                $scoperooms = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadRoom();
        loadRoomDetail();
    }

})(angular.module('tungshop.rooms'));