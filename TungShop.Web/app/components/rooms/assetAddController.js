(function (app) {
    app.controller('assetAddController', assetAddController);

    assetAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function assetAddController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.asset = {};
        $scope.RoomId = $stateParams.id;
        
        $scope.listTypes = [
            {
                ID: 1,
                Name: "Thiết bị"
            },
            {
                ID: 2,
                Name: "Đồ đạc"
            },
            {
                ID: 3,
                Name: "Khác"
            }
        ];
        $scope.listStatus = [
            {
                ID: 1,
                Name: "Tốt"
            },
            {
                ID: 2,
                Name: "Hư Hỏng"
            }
        ];

        $scope.AddAsset = function () {
            apiService.post('/api/listAsset/create', $scope.asset,
                function (result1) {
                    $scope.roomAsset = {};
                    $scope.roomAsset.RoomID = $scope.RoomId;
                    $scope.roomAsset.AssetsID = result1.data.AssetsID;
                    $scope.roomAsset.Amount = 1;

                    apiService.post('/api/roomAsset/create', $scope.roomAsset,
                        function (result2) {
                            notificationService.displaySuccess('Tài sản ' + result1.data.AssetName + ' đã được thêm mới.');
                            $state.go('rooms');
                        }, function (error) {
                            notificationService.displayError('Cập nhật không thành công.');
                        });
                    
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
    }

})(angular.module('tungshop.rooms'));