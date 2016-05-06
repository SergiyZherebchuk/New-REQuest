requestApp.service('TeamService',['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getLastTeam = function (userId) {
        return $http.get(pre + "api/TeamsAPI/Last/" + userId);
    };

    this.getOneTeam = function (teamId) {
        return $http.get(pre + "api/TeamsAPI/" + teamId);
    };

    this.getAllTeams = function () {
        return $http.get(pre + "api/TeamsAPI");
    };

    this.getTop10 = function () {
        return $http.get(pre + "api/TeamsAPI/Top10");
    };

    this.getAllRank = function () {
        return $http.get(pre + "api/TeamsAPI/All");
    };

    this.addNewTeam = function (Team) {
        var request = $http({
            method: "post",
            url: pre + "api/TeamsAPI",
            data: Team
        });

        return request;
    };

    this.updatePoint = function (teamId, teamPoints) {
        var request = $http({
            method: "put",
            url: pre + "api/TeamsAPI/Point" + teamId,
            data: teamPoints
        });
    }

    this.updateTeam = function (teamId, Team) {
        var request = $http({
            method: "put",
            url: pre + "api/AllLevelAPI/" + teamId,
            data: Team
        });
    };

    this.deleteLevel = function (teamId) {
        var request = $http({
            method: "delete",
            url: pre + "api/AllLevelAPI/" + teamId
        });
    }
}]);