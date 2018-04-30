(function (app) {
    app.controller('electricityEditController', electricityEditController);

    electricityEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function electricityEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.electricity = {};
        $scope.RoomID = $stateParams.id;
        $scope.NumberWaterOld = 0;
        $scope.NumberElectricOld = 0;

        function loadelectricityDetail() {
            apiService.get('/api/electricityWater/getbyid/' + $stateParams.id, null, function (result) {
                $scope.electricityWater = result.data;
                if ($scope.electricityWater) {
                    $scope.NumberWaterOld = $scope.electricityWater.WaterNew;
                    $scope.NumberElectricOld = $scope.electricityWater.EletricityNew;
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function loadelectricityHistory() {
            apiService.get('/api/electricityWaterHistory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.RoomHistorys = result.data;
                loadelectricityDetail();
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        $scope.Change = function() {
            if (!$scope.electricityWater.WaterNew) $scope.electricityWater.WaterNew = 0;
            if (!$scope.electricityWater.WaterOld) $scope.electricityWater.WaterOld = 0;
            if (!$scope.electricityWater.PriceWater) $scope.electricityWater.PriceWater = 0;
            if (!$scope.electricityWater.EletricityOld) $scope.electricityWater.EletricityOld = 0;
            if (!$scope.electricityWater.EletricityNew) $scope.electricityWater.EletricityNew = 0;
            if (!$scope.electricityWater.PriceElectricity) $scope.electricityWater.PriceElectricity = 0;

            var priceW = ($scope.electricityWater.WaterNew - $scope.electricityWater.WaterOld) *
                $scope.electricityWater.PriceWater;
            var priceE = ($scope.electricityWater.EletricityNew - $scope.electricityWater.EletricityOld) *
                $scope.electricityWater.PriceElectricity;
            if (priceW <= 0) priceW = 0;
            if (priceE <= 0) priceE = 0;
            $scope.electricityWater.WaterOld = $scope.NumberWaterOld;
            $scope.electricityWater.EletricityOld = $scope.NumberElectricOld;
            $scope.electricityWater.Money = priceW + priceE;
        }
        
        $scope.UpdateElectricity = function () {
            if ($scope.electricityWater.EletricityNew <= $scope.electricityWater.EletricityOld) {
                notificationService.displayError('Số điện mới  phải lơn hơn số điện cũ');
                return;
            }
            if ($scope.electricityWater.WaterNew <= $scope.electricityWater.WaterOld) {
                notificationService.displayError('Số nước mới  phải lơn hơn số nước cũ');
                return;
            }
            
            apiService.put('/api/electricityWater/update', $scope.electricityWater,
                function (result) {
                    notificationService.displaySuccess('Cập nhật thành công điện nước phòng ' + $scope.RoomID);
                    $state.go('rooms');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        loadelectricityHistory();
    }

})(angular.module('tungshop.rooms'));   