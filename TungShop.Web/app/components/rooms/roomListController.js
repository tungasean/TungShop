(function (app) {
    app.controller("roomListController", roomListController);

    roomListController.$inject = ['$scope', 'apiService', 'notificationService','$ngBootbox'];

    function roomListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.rooms = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getrooms = getrooms;
        $scope.keyword = '';

        $scope.search = search;

        function search() {
            getrooms();
        }
        function getrooms(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/room/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                if (result.data.Items)
                for (var i = 0; i < result.data.Items.length; i++) {
                    if (result.data.Items[i].Sex === 0)
                        result.data.Items[i].Type = 'Nam';
                    else {
                        result.data.Items[i].Type = 'Nữ';
                    }
                }
                $scope.rooms = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load room failed.');
            });
        }

        $scope.deleteRoom = deleteRoom;

        function deleteRoom(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/room/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        $scope.getrooms();
    }
})(angular.module("tungshop.rooms"));