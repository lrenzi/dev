'use strict';
app.service("FileUploadService", ["$http", "$q", function ($http, $q) {
    var urlbase = "http://localhost:44857/api/";
    return {
        RetornarDocumentoClienteTipo: function () {
            alert("k3");
            return $http.get(urlbase + "FileUpload/RetornarDocumentoClienteTipo")
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