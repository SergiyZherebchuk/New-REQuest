requestApp.controller("OneTeamController", ['$scope', '$location', '$routeParams', 'TeamService', 'UTSService', 
    function ($scope, $location, $routeParams, TeamService, UTSService) {
    
    $scope.teamId = $routeParams.id;
    
    getUser();

    function getUser() {
        $scope.userId = parseInt($scope.globals);
        getOneTeam();
    }

    function getOneTeam() {
        TeamService.getOneTeam($scope.teamId)
            .then(function (responce) {
                if (responce.data === null) {
                    $location.path('/no team')
                } else {
                    $scope.OneTeam = responce.data;
                    anoutherFunc();
                }
            });
    }

    function anoutherFunc() {
        getUTS();
        getTeam();
        isCaptain();
        getActive();
        getSpare();
    }

    function getUTS() {
        UTSService.getUser($scope.userId)
            .then(function (responce) {
                $scope.myUTS = responce.data;
            });
    }

    function getTeam() {
        TeamService.getLastTeam($scope.userId)
            .then(function (responce) {
                $scope.Team = responce.data;

                if ($scope.Team == null) {
                    $scope.isTeam = 1;
                } else if ($scope.Team.Id === $scope.teamId) {
                    $scope.isTeam = 2;
                } else {
                    $scope.isTeam = 0;
                }
            });
    }

    function isCaptain() {
        UTSService.getCaptain($scope.teamId)
            .then(function (responce) {
                $scope.Captain = responce.data.Name;
            });
    }

    function getActive() {
        UTSService.getActive($scope.teamId)
            .then(function (responce) {
                $scope.ActivePlayers = responce.data;
            });
    }

    function getSpare() {
        UTSService.getSpare($scope.teamId)
            .then(function (responce) {
                $scope.SparePlayers = responce.data;
            });
    }    

    $scope.addToTeam = function () {
        var UTS = {
            UserId: $scope.userId,
            TeamId: $scope.teamId,
            StatusId: 4
        }

        if ($scope.myUTS === null) {
            UTSService.addNewUTS(UTS);
        } else {
            UTSService.updateUTS($scope.myUTS.Id, UTS);
        }
    }

    $scope.exitTeam = function () {
        var UTS = {
            UserId: $scope.userId,
            TeamId: null,
            StatusId: 5
        }

        if ($scope.myUTS === null) {
            UTSService.addNewUTS(UTS);
        } else {
            UTSService.updateUTS($scope.myUTS.Id, UTS);
        }
    }
}]);