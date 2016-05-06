requestApp.service('RealGameService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getCurrent = function (memberId, gameId) {
        return $http.get(pre + "api/CurrentAPI/" + memberId + "/" + gameId);
    }

    this.getAllLevel = function (level, gameId) {
        return $http.get(pre + "api/CurrentAPI/Level/" + level + "/" + gameId);
    }

    this.getLevelNumber = function (gameId) {
        return $http.get(pre + "api/CurrentAPI/AllLevel/" + gameId);
    }

    this.getTimerTips = function (levelId) {
        return $http.get(pre + "api/GameTips/Timer/" + levelId);
    }

    this.getMyCodes = function (levelId, memberId) {
        return $http.get(pre + "api/CurrentAPI/MyCode/" + levelId + "/" + memberId);
    }


    this.addCurrent = function (CS) {
        var request = $http({
            method: "post",
            url: pre + "api/CurrentAPI",
            data: CS
        });

        return request;
    }
    


    this.updateCurrent = function (CS) {
        var request = $http({
            method: "put",
            url: pre + "api/CurrentAPI/",
            data: CS
        });
    }



    this.deleteSectorCode = function (levelId) {
        var request = $http({
            method: "delete",
            url: pre + "api/CodesAPI/Sector/" + levelId
        });
    }
}]);