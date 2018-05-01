/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tungshop.approvals', ['tungshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('approvals', {
            url: "/approvals",
            templateUrl: "/app/components/approvals/approvalListView.html",
            parent: 'base',
            controller: "approvalListController"
        })
            .state('add_approval', {
                url: "/add_approval",
                parent: 'base',
                templateUrl: "/app/components/approvals/approvalAddView.html",
                controller: "approvalAddController"
            })
            .state('edit_approval', {
                url: "/edit_approval/:id",
                templateUrl: "/app/components/approvals/approvalEditView.html",
                controller: "approvalEditController",
                parent: 'base',
            });
    }
})();