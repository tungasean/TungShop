﻿(function (app) {
    app.controller("assetListController", assetListController);

    assetListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$state', '$stateParams'];

    function assetListController($scope, apiService, notificationService, $ngBootbox, $state, $stateParams) {
        $scope.RoomId = $stateParams.id;
        $scope.assets = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.search = search;
        function search() {
            getassets();
        }
        $scope.getassets = function () {
            var config = {
                params: {
                    id: $scope.RoomId
                }
            }
            apiService.get('/api/listAsset/getbyid/' + $scope.RoomId , null, function (result) {
                if (result.data == null) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.assets = result.data;
            }, function () {
                console.log('Load asset failed.');
            });
        }
        

        $scope.deleteAsset = function(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/asset/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        $scope.getassets();
    }
})(angular.module("tungshop.rooms"));