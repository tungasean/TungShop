(function (app) {
    app.controller('electricityEditController', electricityEditController);

    electricityEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function electricityEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.electricity = {};
        $scope.RoomID = $stateParams.id;
//        $scope.GetSeoTitle = GetSeoTitle;
//
//        function GetSeoTitle() {
//            $scope.electricity.Alias = commonService.getSeoTitle($scope.electricity.Name);
//        }

        function loadelectricityDetail() {
            apiService.get('/api/electricityWater/getbyid/' + $stateParams.id, null, function (result) {
                $scope.electricityWater = result.data;
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
            $scope.electricityWater.Money = priceW + priceE;
        }
        
        $scope.UpdateElectricity = function() {
            apiService.put('/api/electricityWater/update', $scope.electricityWater,
                function (result) {
                    notificationService.displaySuccess('Cập nhật thành công điện nước phòng ' + $scope.RoomID);
                    $state.go('rooms');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
//        function loadelectricity() {
//            apiService.get('/api/room/getallparents', null, function (result) {
//                $scopeelectricitys = result.data;
//            }, function () {
//                console.log('Cannot get list parent');
//            });
//        }
//
//        loadelectricity();
        loadelectricityDetail();
    }

})(angular.module('tungshop.rooms'));