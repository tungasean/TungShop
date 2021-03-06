﻿(function (app) {
    app.controller('studentAddController', studentAddController);

    studentAddController.$inject = ['apiService','$scope','notificationService','$state'];

    function studentAddController(apiService, $scope, notificationService,$state) {
        $scope.student = {
            Name: "Tên sinh viên"
        }
       
        $scope.AddStudent = AddStudent;

        function AddStudent() {
            apiService.post('/api/student/create', $scope.student,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('students');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        function loadParentStudent() {
            apiService.get('/api/student/getallparents', null, function (result) {
                $scope.parentStudents = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentStudent();
    }

})(angular.module('tungshop.students'));