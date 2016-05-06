requestApp.service('LevelTypeService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getLevelType = function (levelId) {
        return $http.get(pre + "api/LevelTypeAPI/" + levelId);
    }

    this.getName = function (levelId) {
        return $http.get(pre + "api/LevelTypeAPI/Name/" + levelId);
    }

    this.getType = function (levelId) {
        return $http.get(pre + "api/LevelTypeAPI/Type/" + levelId);
    }

    this.add = function (LevelType) {
        var request = $http({
            method: "post",
            url: pre + "api/LevelTypeAPI",
            data: LevelType
        });

        return request;
    }

    this.update = function (LevelType) {
        var request = $http({
            method: "put",
            url: pre + "api/LevelTypeAPI",
            data: LevelType
        });
    }

    this.delete = function (levelTypeId) {
        var request = $http({
            method: "delete",
            url: pre + "api/LevelTypeAPI/" + levelTypeId
        });
    }
}]);