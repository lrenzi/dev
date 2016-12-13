'use strict';
app.service("FileUploadService", ["$http", "$q", function ($http, $q) {
    var urlbase = "http://localhost:44857/api/";
    return {
        RetornarDocumentoClienteTipo: function () {
            debugger;
            return $http.post(urlbase + "FileUpload/RetornarDocumentoClienteTipo", { ClienteId: 1 })
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
    };
}]);