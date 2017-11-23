(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};
        // chua cac thong tin auth sau khi dang nhap
        var authentication = {
            IsAuthenticated: false,
            userName: ""
        };
        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})(angular.module('tungshop.common'));
