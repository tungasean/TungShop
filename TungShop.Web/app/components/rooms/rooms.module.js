/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tungshop.rooms', ['tungshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('rooms', {
            url: "/rooms",
            templateUrl: "/app/components/rooms/roomListView.html",
            parent: 'base',
            controller: "roomListController"
        })
            .state('add_room', {
                url: "/add_room",
                parent: 'base',
                templateUrl: "/app/components/rooms/roomAddView.html",
                controller: "roomAddController"
            })
            .state('edit_room', {
                url: "/edit_room/:id",
                templateUrl: "/app/components/rooms/roomEditView.html",
                controller: "roomEditController",
                parent: 'base',
            });
    }
})();