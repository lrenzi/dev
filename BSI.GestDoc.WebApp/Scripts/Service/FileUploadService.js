'use strict';
app.service("FileUploadService", ["$http", "$q", function ($http, $q) {
    var urlbase = "http://localhost:44857/api/";
    return {
        RetornarDocumentoClienteTipo: function () {
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
        },
        EnviarArquivos: function () {
            return $http.post(urlbase + "FileUpload/EnviarArquivos", { ClienteId: 1 }, { DocCliSituId: 1})
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