requestApp.service('RankingService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";


    this.getOneUserRank = function (userId) {
        return $http.get(pre + "api/RankingAPI/" + userId);
    };

    this.getTop10Point = function () {
        return $http.get(pre + "api/RankingAPI/Top10Point");
    };

    this.getTop10TeamPoint = function () {
        return $http.get(pre + "api/RankingAPI/Top10TeamPoint");
    };

    this.getAllPoint = function () {
        return $http.get(pre + "api/RankingAPI/AllPoint");
    };

    this.getAllTeamPoint = function () {
        return $http.get(pre + "api/RankingAPI/AllTeamPoint");
    };

    this.addNewRank = function (Rank) {
        var request = $http({
            method: "post",
            url: pre + "api/RankingAPI",
            data: Rank
        });

        return request;
    };

    this.updateRank = function (Rank) {
        var request = $http({
            method: "put",
            url: pre + "api/RankingAPI",
            data: Ranking
        });
    };
}]);