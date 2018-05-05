(function (app) {
    app.controller('assetEditController', assetEditController);

    assetEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function assetEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.asset = {};
        
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

        $scope.loadAssetDetail = function () {
            apiService.get('/api/listAsset/getAssetbyid/' + $stateParams.id, null, function (result) {
                $scope.asset = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        $scope.UpdateAsset = function() {
            apiService.put('/api/listAsset/update', $scope.asset,
                function (result) {
                    notificationService.displaySuccess(result.data.AssetName + ' đã được cập nhật.');
                    $state.go('assets');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        $scope.loadAssetDetail();
    }

})(angular.module('tungshop.rooms'));