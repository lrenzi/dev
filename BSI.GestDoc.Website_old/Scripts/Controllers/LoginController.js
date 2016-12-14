'use strict';
app.controller("LoginCtrl", ["$scope", "$routeParams", "$location", "LoginService", function ($scope, $routeParams, $location, LoginService) {
    //alert("nois1")
    //$scope.EfetuarLogin = function () {
    //    $scope.retornoLogin = LoginService.EfetuarLogin().then(function (data) {
    //        $scope.retornoLogin = data;
    //    });
    //};

    alert("entrou na controler login")
    //Scope Declaration
    $scope.responseData = "";

    $scope.userName = "";

    $scope.userRegistrationEmail = "";
    $scope.userRegistrationPassword = "";
    $scope.userRegistrationConfirmPassword = "";

    $scope.userLoginEmail = "";
    $scope.userLoginPassword = "";

    $scope.accessToken = "";
    $scope.refreshToken = "";
    //Ends Here

    //Function to Login. This will generate Token 
    $scope.EfetuarLogin = function () {
        debugger;
        //This is the information to pass for token based authentication
        var userLogin = {
            grant_type: 'password',
            username: $scope.userLoginEmail,
            password: $scope.userLoginPassword
        };

        var promiselogin = LoginService.login(userLogin);

        promiselogin.then(function (resp) {

            $scope.userName = resp.data.userName;
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            sessionStorage.setItem('userName', resp.data.userName);
            sessionStorage.setItem('accessToken', resp.data.access_token);
            sessionStorage.setItem('refreshToken', resp.data.refresh_token);
            window.location.href = '/Employee/Index';
        }, function (err) {

            $scope.responseData = "Error " + err.status;
        });

    };
}]);