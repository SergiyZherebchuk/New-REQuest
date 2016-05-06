requestApp.service('GamesService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getOneGame = function (gameId) {
        return $http.get(pre + "api/GamesAPI/" + gameId);
    }

    this.getAll = function () {
        return $http.get(pre + "api/GamesAPI");
    }

    this.getGamesOfAuthor = function (userId) {
        return $http.get(pre + "api/GamesAPI/Author/" + userId);
    }

    this.getActiveGames = function () {
        return $http.get(pre + "api/GamesAPI/Active");
    }

    this.getArchiveGames = function () {
        return $http.get(pre + "api/GamesAPI/Archive");
    }

    this.getAnounceGames = function () {
        return $http.get(pre + "api/GamesAPI/Anounce");
    }

    this.addNewGame = function (Game) {
        var request = $http({
            method: "post",
            url: pre + "api/GamesAPI",
            data: Game
        });

        return request;
    }

    this.updateGame = function (Game) {
        var request = $http({
            method: "put",
            url: pre + "api/GamesAPI",
            data: Game
        });
    }

    this.deleteGame = function (gameId) {
        var request = $http({
            method: "delete",
            url: pre + "api/GamesAPI/" + gameId
        });
    }
}]);