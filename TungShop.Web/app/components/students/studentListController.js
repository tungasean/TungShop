(function (app) {
    app.controller("studentListController", studentListController);

    studentListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];

    function studentListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.students = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getstudents = getstudents;
        $scope.keyword = '';


        $scope.search = function() {
            getstudents();
        };
        function getstudents(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/student/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.students = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load student failed.');
            });
        };

        $scope.deleteStudent = deleteStudent;

        function deleteStudent(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/student/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    $scope.search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        $scope.getstudents();
    }
})(angular.module("tungshop.students"));