﻿'use strict';
app.factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', 'utilService', function ($q, $injector, $location, localStorageService, utilService) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {
        
        utilService.limparMensagem();

        config.headers = config.headers || {};
       
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        } else
            $location.path('/login');

        return config;
    }

    var _responseError = function (rejection) {
        
        if (rejection.status === 401) {
            var authService = $injector.get('authService');
            var authData = localStorageService.get('authorizationData');

            if (authData) {
                
                if (authData.useRefreshTokens) {
                    $location.path('/refresh');
                    return $q.reject(rejection);
                }
            }
            authService.logOut();
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);