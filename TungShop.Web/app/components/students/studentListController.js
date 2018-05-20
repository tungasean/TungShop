(function (app) {
    app.controller("studentListController", studentListController);

    studentListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];

    function studentListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.students = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getstudents = getstudents;
        $scope.keyword = '';
        $scope.DicKyLuat = {};
        $scope.ListFilter = [
            {
                ID: 0,
                Name: "Tất cả"
            },
            {
                ID: 1,
                Name: "Bị kỷ luật"
            },
            {
                ID: 2,
                Name: "Không bị kỷ luật"
            },
        ];
        $scope.Filter = $scope.ListFilter[0].ID;

        $scope.search = function() {
            getstudents();
        };

        //lay danh sach ky luat
        $scope.GetKyLuat = function() {
            apiService.get('/api/studentDiscipline/getallparents', null, function (result) {
                if (result.data != null) {
                    for (var i = 0; i < result.data.length; i++) {
                        if ($scope.DicKyLuat[result.data[i].StudentID] == null)
                            $scope.DicKyLuat[result.data[i].StudentID] = result.data[i].StudentID;
                    }
                }
                $scope.getstudents();
            }, function () {
                console.log('Load student failed.');
            });
        };

        function getstudents(page) {
            //lay danh sach sinh vien
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
                var list = [];
                for (var i = 0; i < result.data.TotalCount; i++) {

                    if ($scope.Filter === 1) {
                        if ($scope.DicKyLuat[result.data.Items[i].StudentID])
                            continue;
                    }
                    if ($scope.Filter === 2) {
                        if (!$scope.DicKyLuat[result.data.Items[i].StudentID])
                            continue;
                    }

                        if (result.data.Items[i].Sex === 0)
                            result.data.Items[i].SexString = 'Nam';
                        else
                        result.data.Items[i].SexString = 'Nữ';

                    list.push(result.data.Items[i]);
                }
                $scope.students = list;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = list.length;
            }, function () {
                console.log('Load student failed.');
            });
        };

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
                    $scope.search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        $scope.GetKyLuat();
        
    }
})(angular.module("tungshop.students"));