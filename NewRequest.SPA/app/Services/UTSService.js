requestApp.service('UTSService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getUser = function (userId) {
        return $http.get(pre + "api/UserTeamStatusAPI/User/" + userId);
    }

    this.getCaptain = function (teamId) {
        return $http.get(pre + "api/UserTeamStatusAPI/Captain/" + teamId);
    };

    this.getActive = function (teamId) {
        return $http.get(pre + "api/UserTeamStatusAPI/Active/" + teamId);
    };

    this.getSpare = function (teamId) {
        return $http.get(pre + "api/UserTeamStatusAPI/Spare/" + teamId);
    };

    this.getJoined = function (teamId) {
        return $http.get(pre + "api/UserTeamStatusAPI/Join/" + teamId);
    };

    this.addNewUTS = function (UTS) {
        var request = $http({
            method: "post",
            url: pre + "api/UserTeamStatusAPI",
            data: UTS
        });

        return request;
    };

    this.updateUTS = function (utsId, UTS) {
        var request = $http({
            method: "put",
            url: pre + "api/UserTeamStatusAPI/" + utsId,
            data: UTS
        });
    };
}]);