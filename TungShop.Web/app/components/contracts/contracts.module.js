/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tungshop.contracts', ['tungshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('contracts', {
            url: "/contracts",
            templateUrl: "/app/components/contracts/contractListView.html",
            parent: 'base',
            controller: "contractListController"
        })
            .state('add_contract', {
                url: "/add_contract",
                parent: 'base',
                templateUrl: "/app/components/contracts/contractAddView.html",
                controller: "contractAddController"
            })
            .state('edit_contract', {
                url: "/edit_contract/:id",
                templateUrl: "/app/components/contracts/contractEditView.html",
                controller: "contractEditController",
                parent: 'base',
            });
    }
})();