/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tungshop.students', ['tungshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('students', {
            url: "/students",
            templateUrl: "/app/components/students/studentListView.html",
            parent: 'base',
            controller: "studentListController"
        })
            .state('add_student', {
                url: "/add_student",
                parent: 'base',
                templateUrl: "/app/components/students/studentAddView.html",
                controller: "studentAddController"
            })
            .state('edit_student', {
                url: "/edit_student/:id",
                templateUrl: "/app/components/students/studentEditView.html",
                controller: "studentEditController",
                parent: 'base',
            })
            .state('student_disciplines', {
                url: "/student_disciplines/:id",
                templateUrl: "/app/components/students/studentDisciplineListView.html",
                controller: "studenDisciplinetListController",
                parent: 'base',
            });
    }
})();