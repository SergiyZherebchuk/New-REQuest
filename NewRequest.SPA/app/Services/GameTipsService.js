requestApp.service('GameTipsService',['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getOne = function (tipId) {
        return $http.get(pre + "api/GameTipsAPI?tipId=" + tipId);
    }

    this.getOneLevelTip = function (levelId) {
        return $http.get(pre + "api/GameTipsAPI?levelId=" + levelId);
    }

    this.addNewTip = function (Tip) {
        var request = $http({
            method: "post",
            url: pre + "api/GameTipsAPI",
            data: Tip
        });

        return request;
    }

    this.updateTip = function (Tip) {
        var request = $http({
            method: "put",
            url: pre + "api/GameTipsAPI",
            data: Tip
        });
    }

    this.deleteTip = function (tipId) {
        var request = $http({
            method: "delete",
            url: pre + "api/GameTipsAPI/" + tipId
        });
    }
}]);