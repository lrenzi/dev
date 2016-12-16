﻿
app.service("fileUploadService", ["$http", "$q", function ($http, $q) {
    var urlbase = "http://localhost:44857/api/";

    var fileUploadServiceFactory = {};

    var _retornarDocumentoClienteTipo = function () {
        //debugger;
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
    };

    var _enviarArquivos = function (dados_) {

        debugger;

        var _url = urlbase + "FileUpload/EnviarArquivos";

        $http({
            url: _url,
            method: "POST",
            data: dados_,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            debugger;
        }).error(function (data, status) {
            debugger;
        });
    };

    fileUploadServiceFactory.RetornarDocumentoClienteTipo = _retornarDocumentoClienteTipo;
    fileUploadServiceFactory.EnviarArquivos = _enviarArquivos

    return fileUploadServiceFactory;

    /*
    return fileUploadService;

    return {
        RetornarDocumentoClienteTipo: function () {
            //debugger;
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
            //debugger;


            $http({
                url: file,
                method: "POST",
                data: data,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                debugger;
            }).error(function (data, status) {
                debugger;
            });

            return $http.post(urlbase + "FileUpload/EnviarArquivos", { ClienteId: 1 })
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
        //,
        

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
            /*
            var files = document.getElementsByName("arquivo");
            var formData = new FormData();
            formData.append("files", files[0]);

            return $http({
                url: urlbase + "FileUpload/EnviarArquivos",
                method: "Post",
                data: { documentoClienteTipo: { ClienteId: 1 }, documentoClienteSituacao: { DocCliSituId :2} },
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
        //}
    };*/

}]);