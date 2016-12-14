'use strict';
app.service("LoginService", ["$http", "$q", function ($http, $q) {
    alert("Entrou  no service login")
    var urlbase = "http://localhost:44857/api/";
    return {
        EfetuarLogin: function (userlogin) {
            alert("nois3")
            return $http.post(urlbase + "Login/EfetuarLogin", { password: userlogin.Password, username: userlogin.Name })
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

    //this.login = function (userlogin) {

    //    var resp = $http({
    //        url: "/TOKEN",
    //        method: "POST",
    //        data: $.param({ grant_type: 'password', username: userlogin.username, password: userlogin.password }),
    //        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    //    });
    //    return resp;
    //};

}]);