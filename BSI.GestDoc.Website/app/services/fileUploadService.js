
app.service("fileUploadService", ["$http", "$q", 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var fileUploadServiceFactory = {};

    var _retornarDocumentoClienteTipo = function () {
        
        //return $http.post(serviceBase + "api/FileUpload/RetornarDocumentoClienteTipo", { ClienteId: ngAuthSettings.clienteId })
        return $http.post(serviceBase + "api/FileUpload/RetornarDocumentoClienteTipo", { ClienteId: 1 })
        .then(function (response) {
            if (typeof response.data === 'object') {
                return response.data;
            } else {
                return $q.reject(response.data);
            }
        },
        function (data, status, headers, config) {
            alert("Erro ao acessar o serviço de Consulta de Tipo de Documento.")
        });
    };

    var _retornarArquivo = function (docClienteId) {
        debugger;
        
        $http.post(serviceBase + "api/FileUpload/RetornarArquivo", { DocClienteId: docClienteId })
        .then(function (response) {
            debugger;
            var file = new Blob([response.data], { type: 'application/pdf' });
            saveAs(file, documento.getElementById("idNameFile_"+docCliTipoId).value);
        },
        function (data, status, headers, config) {
            debugger;
            alert("Erro ao acessar o serviço de Consulta de Arquivo.")
        });
        /*
        $http({
            method: 'GET',
            cache: false,
            url: serviceBase + 'api/FileUpload/RetornarArquivo?docClienteId=1',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data, status) {
            debugger;
            console.log(data) // displays text data if the file is a text file, binary if it's an image            
            // now what should I write here to download the file I receive from the WebAPI method.
        }).error(function (data, status) {
            debugger;
        });*/
    }

    fileUploadServiceFactory.RetornarDocumentoClienteTipo = _retornarDocumentoClienteTipo;
    fileUploadServiceFactory.RetornarArquivo = _retornarArquivo;

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