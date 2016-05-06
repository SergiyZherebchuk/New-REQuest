requestApp.service('CodesService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getOneCode = function (levelId) {
        return $http.get(pre + "api/CodesAPI/One/" + levelId);
    }

    this.getSectorCodes = function (levelId) {
        return $http.get(pre + "api/CodesAPI/Sector/" + levelId);
    }

    this.addNewCode = function (Code) {
        var request = $http({
            method: "post",
            url: pre + "api/CodesAPI/One",
            data: Code
        });

        return request;
    }

    this.addNewSectorCodes = function (Codes) {
        var request = $http({
            method: "post",
            url: pre + "api/CodesAPI/Sector",
            data: Codes
        });
    }

    this.isTrueCode = function (levelId, code) {
        return $http({
            method: "GET",
            url: pre + "api/CodesAPI/True/" + levelId,
            params: { code: code }
        });
    }    

    this.isSectorTrueCode = function (levelId, code) {
        return $http({
            method: "GET",
            url: pre + "api/CodesAPI/Sector/True/" + levelId,
            params: { code: code }
        });
    }

    this.updateOneCode = function (Code) {
        var request = $http({
            method: "put",
            url: pre + "api/CodesAPI/One",
            data: Code
        });
    }

    this.updateSectorCode = function (Codes) {
        var request = $http({
            method: "put",
            url: pre + "api/CodesAPI/Sector/",
            data: Codes
        });
    }

    this.deleteOneCode = function (codesId) {
        var request = $http({
            method: "delete",
            url: pre + "api/CodesAPI/One/" + codesId
        });
    }

    this.deleteSectorCode = function (levelId) {
        var request = $http({
            method: "delete",
            url: pre + "api/CodesAPI/Sector/" + levelId
        });
    }
}]);