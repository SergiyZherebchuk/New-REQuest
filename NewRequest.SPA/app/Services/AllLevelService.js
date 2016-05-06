requestApp.service('AllLevelService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getGameIdByLevel = function (levelId) {
        return $http.get(pre + "api/AllLevelAPI/Level/" + levelId);
    }

    this.getLevelsGame = function (gameId) {
        return $http.get(pre + "api/AllLevelAPI/Game/" + gameId);
    };

    this.getLevelById = function (levelId) {
        return $http.get(pre + "api/AllLevelAPI/OneLevel/" + levelId);
    }

    this.addNewLevel = function (Level) {
        var request = $http({
            method: "post",
            url: pre + "api/AllLevelAPI",
            data: Level
        });

        return request;
    };

    this.updateLevel = function (Level) {
        var request = $http({
            method: "put",
            url: pre + "api/AllLevelAPI",
            data: Level
        });
    };

    this.deleteLevel = function (levelId) {
        var request = $http({
            method: "delete",
            url: pre + "api/AllLevelAPI/" + levelId
        });
    }
}]);