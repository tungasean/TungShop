(function (app) {
    app.controller("roomListController", roomListController);

    roomListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function roomListController($scope, apiService, notificationService) {
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
                $scope.rooms = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load room failed.');
            });
        }

        $scope.getrooms();
    }
})(angular.module("tungshop.rooms"));