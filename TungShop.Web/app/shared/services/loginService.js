﻿(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData',
        function ($http, $q, authenticationService, authData) {
            var userInfo;
            var deferred;
            // xu ly nghiep vu login
            this.login = function (userName, password) {
                deferred = $q.defer();
                var data = "grant_type=password&username=" + userName + "&password=" + password;
                $http.post('/oauth/token', data, {
                    headers:
                        { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (response) {
                    userInfo = {
                        accessToken: response.access_token,
                        userName: userName
                    };
                    authenticationService.setTokenInfo(userInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = userName;
                    authData.authenticationData.roles = {};
                    if (response.roles) {
                        var assignRole = JSON.parse(response.roles);
                        if (assignRole && assignRole.length > 0) {
                            for (var i = 0; i < assignRole.length; i++) {
                                authData.authenticationData.roles[assignRole[i]] = assignRole[i];
                            }
                        }
                    }
                    deferred.resolve(null);
                })
                    .error(function (err, status) {
                        authData.authenticationData.IsAuthenticated = false;
                        authData.authenticationData.userName = "";
                        authData.authenticationData.roles = {};
                        deferred.resolve(err);
                    });
                return deferred.promise;
            }

            this.logOut = function () {
                authenticationService.removeToken();
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
                //authData.authenticationData.roles = {};
            }
        }]);
})(angular.module('tungshop.common'));