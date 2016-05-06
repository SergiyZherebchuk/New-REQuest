requestApp.service('StatisticService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getLastPossition = function (gameId, level) {
        return $http.get(pre + "api/StatisticAPI/Possition/" + gameId + "/" + level);
    }

    this.getUserStatistic = function (gameId) {
        return $http.get(pre + "api/StatisticAPI/User/" + gameId);
    };

    this.getLastUserStat = function (gameId, memberId) {
        return $http.get(pre + "api/StatisticAPI/Last/User/" + gameId + "/" + memberId);
    }

    this.getTeamStatistic = function (gameId) {
        return $http.get(pre + "api/StatisticAPI/Teams/" + gameId);
    };

    this.getLastTeamStat = function (gameId, memberId) {
        return $http.get(pre + "api/StatisticAPI/Last/Team/" + gameId + "/" + memberId);
    }

    this.addNewStat = function (Statistic) {
        var request = $http({
            method: "post",
            url: pre + "api/StatisticAPI",
            data: Statistic
        });

        return request;
    };
}]);