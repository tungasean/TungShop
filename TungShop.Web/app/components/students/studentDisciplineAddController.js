(function (app) {
    app.controller('studentDisciplineAddController', studentDisciplineAddController);

    studentDisciplineAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function studentDisciplineAddController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.studentId = $stateParams.id;
        $scope.studentDiscipline = {
            StudentId : $scope.studentId,
            Name: "name",
            InfoDiscipline : ""
        }


        $scope.AddStudent = function () {
            apiService.post('/api/studentDiscipline/create', $scope.studentDiscipline,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('student_disciplines', { id: $scope.studentId });
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    }

})(angular.module('tungshop.students'));