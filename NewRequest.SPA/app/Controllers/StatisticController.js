requestApp.controller("StatisticController", ['$scope', '$routeParams', '$timeout', 'GamesService', 'StatisticService', 
    
    function ($scope, $routeParams, $timeout, GamesService, StatisticService) {
    
    $scope.gameId = $routeParams.id;

    getGames();

    function getGames() {
        GamesService.getOneGame($scope.gameId)
            .success(function (data) {
                $scope.ThisGame = data;

                getStatistic();
            });
    }

    function getStatistic() {
        if ($scope.ThisGame.GameType == 1) {
            StatisticService.getUserStatistic($scope.gameId)
                .then(function (responce) {
                    $scope.Statistic = responce.data;
                });
        } else if ($scope.ThisGame.GameType == 2) {
            StatisticService.getTeamStatistic($scope.gameId)
                .then(function (responce) {
                    $scope.Statistic = responce.data;
                });
        }

        $timeout(getStatistic, 10000);
    }
}]);