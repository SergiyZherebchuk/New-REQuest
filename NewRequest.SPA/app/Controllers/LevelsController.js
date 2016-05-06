requestApp.controller("LevelsController", ['$scope', '$location', '$routeParams', '$timeout', 'AllLevelService', 
    'GamesService',
    
    function ($scope, $location, $routeParams, $timeout, AllLevelService, GamesService) {

    $scope.gameId = $routeParams.id;
    $scope.LevelNum = 0;
        
    getUserId();

    function getUserId() {
        $scope.userId = parseInt($scope.globals);
        isGameExist();
    }

    function isGameExist() {
        GamesService.getOneGame($scope.gameId)
            .then(function (responce) {
                if (responce.data === null) {
                    $location.path('/nogame');
                } else if (responce.data.AuthorId !== $scope.userId) {
                    $location.path('/GameInfo/' + $scope.gameId);
                } else {
                    getAllLevels();
                }
            });
    }

    function getAllLevels () {
        AllLevelService.getLevelsGame($scope.gameId)
            .then(function (responce) {
                $scope.AllLevels = responce.data;
                $scope.LevelNum = $scope.AllLevels.length;
                
                $timeout(getAllLevels, 5000);
            });
    }

    $scope.addNewLevel = function () {
        if ($scope.OperTypeLevel === 1) {
            var Level = {
                Id: $scope.LevelId,
                GameId: $scope.gameId,
                JNumber: $scope.ThisLevelNum,
                Name: $scope.Name
            }

            AllLevelService.updateLevel(Level);

            clearAll();
        } else {
            var Level = {
                GameId: $scope.gameId,
                JNumber: $scope.LevelNum + 1,
                Name: $scope.Name
            }

            AllLevelService.addNewLevel(Level);
        }        
        
        getAllLevels();
    }

    $scope.editThisLevel = function (allLevel) {
        $scope.LevelId = allLevel.Id;
        $scope.Name = allLevel.Name;
        $scope.ThisLevelNum = allLevel.JNumber;

        $scope.OperTypeLevel = 1;
    }

    $scope.editUp = function (allLevel) {
        var array = $scope.AllLevels;

        for (i = 0; i < array.length; i++) {
            if (array[i].JNumber === allLevel.JNumber - 1) {
                allLevel.JNumber = allLevel.JNumber - 1;
                array[i].JNumber = array[i].JNumber + 1;

                AllLevelService.updateLevel(allLevel);
                AllLevelService.updateLevel(array[i]);
            }
        }

        getAllLevels();
    }

    $scope.editDown = function (allLevel) {
        var array = $scope.AllLevels;

        for (i = 0; i < array.length; i++) {
            if (array[i].JNumber === allLevel.JNumber + 1) {
                allLevel.JNumber = allLevel.JNumber + 1;
                array[i].JNumber = array[i].JNumber - 1;

                AllLevelService.updateLevel(allLevel);
                AllLevelService.updateLevel(array[i]);
            }
        }

        getAllLevels();
    }

    $scope.deleteOneLevel = function (id) {
        AllLevelService.deleteLevel(id);

        getAllLevels();
    }

    function clearAll() {
        $scope.LevelId = '';
        $scope.Name = '';

        $scope.OperTypeLevel = 2;
    }
}]);