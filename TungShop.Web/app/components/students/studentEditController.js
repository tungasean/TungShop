(function (app) {
    app.controller('studentEditController', studentEditController);

    studentEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function studentEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.student = {};

        $scope.UpdateStudent = UpdateStudent;
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

        function loadStudentDetail() {
            apiService.get('/api/student/getbyid/' + $stateParams.id, null, function (result) {
                $scope.student = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateStudent() {
            apiService.put('/api/student/update', $scope.student,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('students');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadStudent() {
            apiService.get('/api/student/getallparents', null, function (result) {
                $scopestudents = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadStudent();
        loadStudentDetail();
    }

})(angular.module('tungshop.students'));