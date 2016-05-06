requestApp.controller("TeamController", ['$scope', '$location', 'TeamService', 'UTSService', 
    
    function ($scope, $location, TeamService, UTSService) {

    $scope.getUserId = function () {
        $scope.userId = parseInt($scope.globals);
        getUTS();
    }

    function getUTS() {
        UTSService.getUser($scope.userId)
            .then(function (responce) {
                if (responce.data === null) {
                    $scope.UTSreq = true;
                } else {
                    $scope.myUTS = responce.data;
                    $location.path('/MyTeam');
                }
            });
    }

    $scope.getAllTeam = function() {
        TeamService.getAllTeams()
            .then(function (responce) {
                $scope.Teams = responce.data;
            });
    }

    $scope.saveNewTeam = function () {
        var Team = {
            Name: $scope.Name,
            Register: new Date(),
            CaptainId: $scope.userId,
            Points: 0
        }

        TeamService.addNewTeam(Team)
            .then(function (responce) {
                var teamId = responce.data.Id;
                $scope.saveUTS(teamId);
            });               
    }

    $scope.saveUTS = function (teamId) {
        if ($scope.UTSreq) {
            var UTS = {
                UserId: $scope.userId,
                TeamId: teamId,
                StatusId: 1
            }

            UTSService.addNewUTS(UTS)
                .then(function (responce) {
                    $location.path('/MyTeam');
                });
        } else {
            var myUTS = {
                UserId: $scope.myUTS.UserId,
                TeamId: teamId,
                StatusId: 1
            }

            UTSService.updateUTS(UTS)
                .then(function (responce) {
                    $location.path('/MyTeam');
                });
        }
    }
}]);