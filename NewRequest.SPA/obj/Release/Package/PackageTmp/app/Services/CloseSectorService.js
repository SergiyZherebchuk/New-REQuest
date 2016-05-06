requestApp.service('CloseSectorService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getCloseList = function (levelId, gameId, memberId) {
        $http.get(pre + "api/CloseSectorAPI/levelId=" + levelId + "&gameId=" + gameId + "&memberId=" + memberId);
    }

    this.getSector = function (levelId, gameId, memberId) {
        $http.get(pre + "api/CloseSectorAPI/Sector/levelId=" + levelId + "&gameId=" + gameId + "&memberId=" + memberId);
    }

    this.addNewCode = function (CloseSector) {
        var request = $http({
            method: "post",
            url: pre + "api/CloseSectorAPI",
            data: CloseSector
        });

        return request;
    }

    this.deleteOneSector = function (levelId, gameId, memberId) {
        var request = $http({
            method: "delete",
            url: pre + "api/CloseSectorAPI/levelId=" + levelId + "&gameId=" + gameId + "&memberId=" + memberId
        });
    }
}]);