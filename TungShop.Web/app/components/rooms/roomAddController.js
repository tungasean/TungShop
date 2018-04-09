(function (app) {
    app.controller('roomAddController', roomAddController);

    roomAddController.$inject = ['apiService','$scope','notificationService','$state'];

    function roomAddController(apiService, $scope, notificationService,$state) {
        $scope.room = {};
       
        $scope.AddRoom = AddRoom;

        function AddRoom() {
            apiService.post('/api/room/create', $scope.room,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
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