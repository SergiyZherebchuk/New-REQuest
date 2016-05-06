requestApp.service('HelpService', function ($http, $cookies, $rootScope) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getUserId = function () {
        var res = $rootScope.globals;
        return ; //$http.get(pre + "api/AuthAPI")
    }

    this.authUser = function (User) {
        var request = $http({
            method: "post",
            url: pre + "api/HelpedAPI/Login",
            data: User
        });

        return request;
    }

});