requestApp.controller("RankingController", ['$scope', '$location', 'RankingService', 'TeamService', 
    function ($scope, $location, RankingService, TeamService) {

    $scope.getTopPlayer = function () {
        $scope.link = "Player";
        $scope.getTopPlayRank();
    }

    $scope.getTopTeam = function () {
        $scope.link = "Team";
        $scope.getTopTeamRank();
    }

    $scope.getTopTeamPlayer = function () {
        $scope.link = "TeamPlayer";
        $scope.getTopTeamPlayRank();
    }

    $scope.getTopPlayRank = function () {
        RankingService.getTop10Point()
            .then(function (responce) {
                $scope.Rank = responce.data;
            });
    }

    $scope.getTopTeamRank = function () {
        TeamService.getTop10()
            .then(function (responce) {
                $scope.Rank = responce.data;
            });
    }

    $scope.getTopTeamPlayRank = function () {
        RankingService.getTop10TeamPoint()
            .then(function (responce) {
                $scope.Rank = responce.data;
            });
    }

    $scope.getAllPlayerRank = function () {
        RankingService.getAllPoint()
            .then(function (responce) {
                $scope.Ranking = responce.data;
            });
    }

    $scope.getAllTeamPlayerRank = function () {
        RankingService.getAllTeamPoint()
            .then(function (responce) {
                $scope.Ranking = responce.data;
            });
    }

    $scope.getAllTeamRank = function () {
        TeamService.getAllRank()
            .then(function (responce) {
                $scope.Ranking = responce.data;
            });
    }
}]);
