requestApp.controller("GameInfoController", ['$scope', '$routeParams', 'GamesService',
    function ($scope, $routeParams, GamesService) {
    
    $scope.gameId = $routeParams.id;

    getGameInfo();

    function getGameInfo() {
        GamesService.getOneGame($scope.gameId)
            .then(function (responce) {
                $scope.Game = responce.data;
        });
    }
}]);