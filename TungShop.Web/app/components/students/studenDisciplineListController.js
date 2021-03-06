﻿(function (app) {
    app.controller("studenDisciplinetListController", studenDisciplinetListController);

    studenDisciplinetListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$state', '$stateParams'];

    function studenDisciplinetListController($scope, apiService, notificationService, $ngBootbox, $state, $stateParams) {

        $scope.StudentId = $stateParams.id;
        $scope.lstKyLuat = [];
        
        $scope.getstudents = function() {
            apiService.get('/api/studentDiscipline/getbystudentid/' + $scope.StudentId, null, function (result) {
                if (result.data === null) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                if (result.data) {
                    $scope.lstKyLuat = result.data;
                }
            }, function () {
                console.log('Load student failed.');
            });
        };

        $scope.deleteStudentDiscipline = deleteStudentDiscipline;

        function deleteStudentDiscipline(id) {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/api/studentDiscipline/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    $scope.getstudents();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                });
        }

        $scope.deleteStudent = deleteStudent;

        function deleteStudent(id) {
            $ngBootbox.confirm('Khi bạn xóa sinh viên này khỏi danh sách, đồng nghĩa với việc sinh viên này sẽ bị loại khỏi ký túc xá. Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/student/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    $state.go('students');
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        $scope.getstudents();
    }
})(angular.module("tungshop.students"));