'use strict';
app.service("fileUploadService", ["$http", "$q", function ($http, $q) {
    var urlbase = "http://localhost:44857/api/";
    return {
        RetornarDocumentoClienteTipo: function () {
            debugger;
            return $http.post(urlbase + "FileUpload/RetornarDocumentoClienteTipo", { ClienteId: 1})
			.then(function (response) {
			    if (typeof response.data === 'object') {
			        return response.data;
			    } else {
			        return $q.reject(response.data);
			    }
			},
			function () {
			    debugger;
			    return $q.reject(response.data);
			});
        }
        ,
        EnviarArquivos: function () {
            debugger;
            /*this.login = function (userlogin) {

                var resp = $http({
                    url: "/TOKEN",
                    method: "POST",
                    data: $.param({ grant_type: 'password', username: userlogin.username, password: userlogin.password }),
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                });
                return resp;
            };

            var files = $('#fileInput').get(0).files;
            var formData = new FormData();
            formData.append("files", files[0]);

            var formData = new FormData();
            var files = $('#arquivo').get(0).files;
            formData.append("files", JSON.stringify(files));
            */
            return $http({
                url: urlbase + "FileUpload/EnviarArquivos",
                method: "POST",
                data:formData,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            })
			.then(function (response) {
			    debugger;
			    if (typeof response.data === 'object') {
			        return response.data;
			    } else {
			        return $q.reject(response.data);
			    }
			},
			function () {
			    debugger;
			    return $q.reject(response.data);
			});
        }
    };
}]);