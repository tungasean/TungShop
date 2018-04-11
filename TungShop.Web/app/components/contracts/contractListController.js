(function (app) {
    app.controller("contractListController", contractListController);

    contractListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function contractListController($scope, apiService, notificationService) {
        $scope.contracts = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getcontracts = getcontracts;
        $scope.keyword = '';

        $scope.search = search;

        function search() {
            getcontracts();
        }
        function getcontracts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/contract/getall', config, function (result) {
                if (result.data.TotalCount === 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.contracts = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load contract failed.');
            });
        }

        $scope.getcontracts();
    }
})(angular.module("tungshop.contracts"));