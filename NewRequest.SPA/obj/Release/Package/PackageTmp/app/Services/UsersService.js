requestApp.service('UsersService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getUserByLogin = function (myLogin) {
        return $http({
            url: pre + "api/UsersAPI",
            method: "GET",
            params: { myLogin: myLogin }
        });
    };

    this.getUserById = function (id) {
        return $http.get(pre + "api/UsersAPI/" + id);
    };

    this.getAllUser = function () {
        return $http.get(pre + "api/UsersAPI");
    };

    this.isUserTrue = function (login, password) {
        return $http({
            url: pre + "api/UsersAPI",
            method: "GET",
            params: { login: login, pass: password }
        });
    }

    this.addUser = function (User) {
        var request = $http({
            method: "post",
            url: pre + "api/UsersAPI",
            data: User
        });

        return request;
    };

    this.updateUser = function (userId, User) {
        var request = $http({
            method: "put",
            url: pre + "api/UserAPI/" + userId,
            data: User
        });
    };
}]);