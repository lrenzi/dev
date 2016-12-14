'use strict';
app.service("LoginService", ["$http", "$q", function ($http, $q) {
    alert("nois2")
    var urlbase = "http://localhost:44857/api/";
    return {
        EfetuarLogin: function () {
            
            return $http.post(urlbase + "Login/EfetuarLogin", { ClienteId: 1 })
			.then(function (response) {
			    if (typeof response.data === 'object') {
			        return response.data;
			    } else {
			        return $q.reject(response.data);
			    }
			},
			function () {
			    
			    return $q.reject(response.data);
			});
        }
    };
}]);