requestApp.controller("GamesController", ['$scope', '$location', '$timeout', '$routeParams', 'GamesService',
    function ($scope, $location, $timeout, $routeParams, GamesService) {

    $scope.gameId = $routeParams.id;

    getUserId();

    function getUserId() {
        $scope.userId = parseInt($scope.globals);
        getOneAuthorGames();
    }
    
    function getOneAuthorGames () {
        GamesService.getGamesOfAuthor($scope.userId)
            .then(function (responce) {
                $scope.MyGames = responce.data;

                if ($scope.gameId > 0) {
                    $scope.getOneGame();
                }
            });
    }
            
    $scope.getOneGame = function () {
        GamesService.getOneGame($scope.gameId)
            .then(function (responce) {
                var result = responce.data;

                if (result != null || result != undefined) {
                    $scope.OneGameId = result.Id,
                    $scope.Name = result.Name;
                    $scope.AuthorId = result.AuthorId,
                    $scope.Type = result.GameType;
                    $("#datestart").data("DateTimePicker").setDate(new Date(result.DateStart));
                    $("#dateend").data("DateTimePicker").setDate(new Date(result.DateEnd));
                    $scope.Info = result.Info;
                    $scope.Payment = result.Payment;

                    if ($scope.AuthorId !== $scope.userId) {
                        $location.path('/GameInfo/' + $scope.gameId);
                    }

                    if (result.Price == null) {
                        $scope.paidValue = 'no';
                    } else {
                        $scope.paidValue = 'yes';
                        $scope.Price = result.Price;
                    }
                } else {
                    $location.path('/nogame');
                }
            });
    }

    $scope.getActiveGames = function () {
        GamesService.getActiveGames()
            .then(function (responce) {
                $scope.ActiveGames = responce.data;
            });
        $timeout($scope.getActiveGames, 10000);
    }

    $scope.getArchiveGames = function () {
        GamesService.getArchiveGames()
            .then(function (responce) {
                $scope.ArchiveGames = responce.data;
            });
        $timeout($scope.getArchiveGames, 10000);
    }

    $scope.getAnouncementGames = function () {
        GamesService.getAnounceGames()
           .then(function (responce) {
               $scope.AnounceGames = responce.data;
           });
        $timeout($scope.getAnouncementGames, 10000);
    }

    $scope.saveNewGame = function () {
        var dateStart = new Date($("#datestart").data("DateTimePicker").getDate());
        var dateEnd = new Date($("#dateend").data("DateTimePicker").getDate());

        if ($scope.paidValue === 'no') {
            $scope.Price = null;
        }

        // get server time

        var Game = {
            Name: $scope.Name,
            AuthorId: $scope.userId,
            GameType: parseInt($scope.Type),
            DateStart: dateStart,
            DateEnd: dateEnd,
            Info: $scope.Info,
            Price: $scope.Price,
            Payment: 0,
        };

        GamesService.addNewGame(Game)
            .then(function (responce) {
                $location.path('/Authors/AllLevels/' + responce.data.Id);
            });
    }

    $scope.editGame = function () {
        var dateStart = new Date($("#datestart").data("DateTimePicker").getDate());
        var dateEnd = new Date($("#dateend").data("DateTimePicker").getDate());


        var Game = {
            Id: $scope.OneGameId,
            Name: $scope.Name,
            AuthorId: $scope.userId,
            GameType: parseInt($scope.Type),
            DateStart: dateStart,
            DateEnd: dateEnd,
            Info: $scope.Info,
            Price: $scope.Price,
            Payment: $scope.Payment,
        };

        GamesService.updateGame(Game);
    }

    $scope.deleteGame = function (gameId) {
        GamesService.deleteGame(gameId)
            .then(function (responce) {
                getOneAuthorGames();
            });
    }
}]);