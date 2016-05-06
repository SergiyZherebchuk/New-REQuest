requestApp.controller("MyTeamController", ['$scope', '$location', 'TeamService', 'UTSService', 
    function ($scope, $location, TeamService, UTSService) {
    $scope.userId = null;

    getUser();

    function getUser() {
        $scope.userId = parseInt($scope.globals);
        getTeam();
    }

    function getTeam() {
        TeamService.getLastTeam($scope.userId)
            .then(function (responce) {
                $scope.Team = responce.data;

                if ($scope.Team !== null) {
                    $scope.teamId = responce.data.Id;
                }

                if ($scope.Team == null || $scope.Team == undefined) {
                    $location.path('/Teams/New')
                } else {
                    isCaptain();
                }
            });
    }

    function isCaptain() {
        UTSService.getCaptain($scope.teamId)
            .then(function (responce) {
                $scope.Captain = responce.data.Name;

                if ($scope.userId === responce.data.Id) {
                    getActive();
                    getSpare();
                    getJoined();
                } else {
                    $location.path('/Team/' + $scope.Team.Id);
                }
            });
    }

    function getActive() {
        UTSService.getActive($scope.Team.Id)
            .then(function (responce) {
                $scope.ActivePlayers = responce.data;
            });
    }

    function getSpare() {
        UTSService.getSpare($scope.Team.Id)
            .then(function (responce) {
                $scope.SparePlayers = responce.data;
            });
    }

    function getJoined() {
        UTSService.getJoined($scope.Team.Id)
            .then(function (responce) {
                $scope.JoinPlayers = responce.data;
            });
    }



    $scope.handleDrop = function (item, bin) {

    }
}]);