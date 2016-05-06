requestApp.controller("RealGameController", ['$scope', '$route', '$rootScope', '$routeParams', '$timeout', '$location', 'GamesService', 
    'CodesService', 'RealGameService', 'GameTaskService', 'GameTipsService', 'LevelTypeService', 'StatisticService',
    'CloseSectorService', 'RankingService',

    function ($scope, $route, $rootScope, $routeParams, $timeout, $location, GamesService, CodesService, RealGameService, 
        GameTaskService, GameTipsService, LevelTypeService, StatisticService, CloseSectorService, RankingService) {
    
    $scope.gameId = $routeParams.id;
    $scope.userId = parseInt($scope.globals);
    $scope.ASTimer = "";


    $scope.getGame = function() {
        GamesService.getOneGame($scope.gameId)
            .then(function (responce) {
                $scope.Game = responce.data;

                if (responce.data !== null) {

                    if ($scope.Game.AuthorId === $scope.userId) {
                        $location.path('/Authors/EditGame/' + $scope.gameId);
                    }

                    if ($scope.Game.DateEnd < new Date()) {
                        $location.path('/Statistic/' + $scope.gameId);
                    } else if (new Date($scope.Game.DateStart) > new Date()) {
                        $scope.ToStart = false;

                        getTimer('GS', new Date($scope.Game.DateStart));
                    } else {
                        $scope.ToStart = true;

                        if ($scope.Game.GameType === 2) {
                            getTeam();
                        } else {
                            $scope.memberId = $scope.userId;
                            getAllLevel();
                        }
                    }
                }
            });
    }

    function getTeam() {
        TeamService.getLastTeam($scope.userId)
            .then(function (responce) {
                if (responce.data !== null) {
                    $scope.memberId = responce.data.Id;
                    getAllLevel();
                }
            });
    }

    function getAllLevel() {
        RealGameService.getLevelNumber($scope.gameId)
            .then(function (responce) {
                $scope.AllLevels = responce.data;

                getCurrent();
            })
    }

    function getCurrent() {
        RealGameService.getCurrent($scope.memberId, $scope.gameId)
            .then(function (responce) {
                $scope.Current = responce.data;

                if (responce.data === null) {
                    $scope.OperTypeCurrent = 1;
                    $scope.saveCurrent();
                } else {
                    $scope.OperTypeCurrent = 2;

                    if ($scope.Current.Level > $scope.AllLevels) {
                        $location.path('/Statistic/' + $scope.gameId);
                    } else if ($scope.Current.Level > 1) {
                        getStatistic();
                    } else {
                        getThisLevel();
                    }
                }
            });
    }

    function getStatistic() {
        if ($scope.Game.GameType === 1) {
            StatisticService.getLastUserStat($scope.gameId, $scope.memberId)
                .then(function (responce) {
                    if (responce.data !== null) {
                        $scope.Statistic = responce.data;
                        $scope.LastDate = responce.data.DateEnd;
                        getThisLevel();
                    }
                });
        } else {
            StatisticService.getLastTeamStat($scope.gameId, $scope.memberId)
                .then(function (responce) {
                    if (responce.data !== null) {
                        $scope.Statistic = responce.data;
                        $scope.LastDate = responce.data.DateEnd;
                        getThisLevel();
                    }
                });
        }

    }

    function getThisLevel() {
        RealGameService.getAllLevel($scope.Current.Level, $scope.gameId)
            .then(function (responce) {
                if (responce.data !== null) {
                    $scope.Level = responce.data;
                    $scope.levelId = responce.data.Id;
                    getLevelType();
                }
            });
    }

    function getLevelType() {
        LevelTypeService.getLevelType($scope.levelId)
            .then(function (responce) {
                if (responce.data !== null) {
                    $scope.levelType = responce.data.Type;
                    getLevelTypeName();
                } else {
                    getTask();
                }
            });
    }

    function getLevelTypeName() {
        LevelTypeService.getName($scope.levelId)
            .then(function (responce) {
                $scope.MyLevelType = responce.data;
                
                getTask();
            });
    }
    
    function getTask() {
        GameTaskService.getOne($scope.levelId)
            .then(function (responce) {
                $scope.Task = responce.data.Task;
                
                $scope.Autoswitch = responce.data.Autoswitch;
                $scope.StopTimer = false;

                if ($scope.Autoswitch !== null) {
                    var d;

                    if ($scope.Current.Level > 1) {
                        d = new Date($scope.Statistic.DateEnd);
                    }
                    else if ($scope.Current.Level == 1) {
                        d = new Date($scope.Game.DateStart);
                    }
                    d.setSeconds(d.getSeconds() + $scope.Autoswitch);
                    var dateFuture = new Date(d);

                    getTimer('AS', dateFuture);
                }

                getAllTips();
            });
    }

    function getAllTips() {
        RealGameService.getTimerTips($scope.levelId)
            .then(function (responce) {
                $scope.Tips = responce.data;

                if (responce.data !== null) {
                    for (i = 0; i < $scope.Tips.length; i++) {
                        var d;

                        if ($scope.Current.Level > 1) {
                            d = new Date($scope.Statistic.DateEnd);
                        }
                        else if ($scope.Current.Level == 1) {
                            d = new Date($scope.Game.DateStart);
                        }
                        d.setSeconds(d.getSeconds() + $scope.Tips[i].Times);
                        var dateFuture = new Date(d);

                        getTimer(i, dateFuture);
                    }

                    if ($scope.levelType === 2 || $scope.levelType === 3) {
                        getSectorCodes();
                    }
                }                
            });        
    }

    function getOneTip (i, Tip) {
        GameTipsService.getOne(Tip.Id)
            .then(function(responce){
                $scope.Tips[i] = responce.data;
            });
    }
    
    function getSectorCodes() {
        RealGameService.getMyCodes($scope.levelId, $scope.memberId)
            .then(function (responce) {
                $scope.SectorCodes = responce.data;
                var n = 0;

                var length = $scope.SectorCodes.length;

                for (i = 0; i < length; i++) {
                    if ($scope.SectorCodes[i].Code1 !== null) {
                        n++;
                    }
                }

                if (n === length) {
                    $scope.ASTimer = " ";
                    $scope.saveCurrent();
                }
            });        
    }
    

    $scope.saveCurrent = function () {
        if ($scope.OperTypeCurrent == 1) {
            var CS = {
                GameId: $scope.gameId,
                Level: 1,
                MemberId: $scope.memberId
            }

            RealGameService.addCurrent(CS)
                .success(function () {
                    getAllLevel();
                });
        } else {
            var CS = {
                Id: $scope.Current.Id,
                GameId: $scope.gameId,
                Level: $scope.Level.JNumber + 1,
                MemberId: $scope.memberId
            }

            RealGameService.updateCurrent(CS);
            
            $scope.ASTimer = " ";
            $scope.StopTimer = true;

            $scope.saveStatistic();
        }
    };

    $scope.saveCloseSector = function (sector) {
        var CS = {
            GameId: $scope.gameId,
            Level: $scope.Current.Level,
            MemberId: $scope.memberId,
            Sector: sector
        };

        CloseSectorService.addNewCode(CS)
            .then(function (responce) {
                getSectorCodes();
            });
    };

    $scope.saveStatistic = function () {
        if ($scope.MyATSTime === null || $scope.MyATSTime === undefined) {
            $scope.MyATSTime = new Date()
        }
        var Stat = {
            GameId: $scope.gameId,
            Level: $scope.Level.JNumber,
            MemberId: $scope.memberId,
            DateEnd: $scope.MyATSTime
        }

        StatisticService.addNewStat(Stat)
            .success(function () {
                $scope.MyATSTime = null;
            });

        if ($scope.Level.JNumber === $scope.AllLevels) {
            getPossition();
        } else {
            $route.reload();
        }
    };

    $scope.postCodes = function () {
        var result;

        if ($scope.levelType === 1 || $scope.levelType === 4) {
            CodesService.isTrueCode($scope.levelId, $scope.MyCode)
                .then(function (responce) {
                    result = responce.data;
                    
                    if (result) {
                        $scope.ASTimer = "";
                        $scope.saveCurrent();
                    } else {
                        $scope.MyCode = "";
                    }

                    clearMyCodes();
                });
        } else if ($scope.levelType === 2 || $scope.levelType === 3) {
            CodesService.isSectorTrueCode($scope.levelId, $scope.MyCode)
                .then(function (responce) {
                    var res = responce.data;

                    if (res.IsTrue) {
                        $scope.MyCode = "";
                        $scope.saveCloseSector(res.Sector);
                    }
                });
        }
    };

    function getPossition() {
        StatisticService.getLastPossition($scope.gameId, $scope.Level.JNumber)
            .then(function (responce) {
                $scope.MyPossition = responce.data;

                getRanking();
            });
    }

    function getRanking() {
        RankingService.getOneUserRank($scope.userId)
            .then(function (responce) {
                $scope.MyRank = responce.data;

                if (responce.data === null) {
                    $scope.OperTypeRank = 1;
                    $scope.saveRanking();
                } else {
                    $scope.OperTypeRank = 2;
                    $scope.saveRanking();
                }
            });

    }

    $scope.saveRanking = function () {
        if ($scope.MyPossition < 11) {
            var pos = $scope.MyPossition;

            if(pos === 1) {
                $scope.Point = 100;
            } else if (pos === 2) {
                $scope.Point = 80;
            } else if (pos === 3) {
                $scope.Point = 60;
            } else if (pos === 4) {
                $scope.Point = 50;
            } else if (pos === 5) {
                $scope.Point = 40;
            } else if (pos === 6) {
                $scope.Point = 20;
            } else if (pos === 7) {
                $scope.Point = 10;
            } else if (pos === 8) {
                $scope.Point = 8;
            } else if (pos === 9) {
                $scope.Point = 6;
            } else if (pos === 10) {
                $scope.Point = 2;
            }
        }

        if ($scope.OperTypeRank === 1) {
            var Rank = {
                UserId: $scope.userId,
                Point: $scope.Point
            }

            RankingService.addNewRank(Rank);

            $location.path('/Statistic/' + $scope.gameId);
        } else {
            var Rank = {
                Id: $scope.MyRank.Id,
                UserId: $scope.userId,
                Point: $scope.MyRank.Point + $scope.Point
            }

            RankingService.updateRank(Rank);

            $location.path('/Statistic/' + $scope.gameId);
        }
    }

    $scope.deleteCS = function (levelId, gameId, memberId) {
        CloseSectorService.deleteOneSector(levelId, gameId, memberId);
        
        getSectorCodes();
    }


    function clearMyCodes() {
        $scope.MyCode = null;
    }

    function getTimer(i, dateFuture) {
        var timer = function () {
            dateNow = new Date();

            var amount = dateFuture.getTime() - dateNow.getTime();

            delete dateNow;

            if (amount <= 1) {
                if (i === 'AS') {
                    $scope.MyATSTime = new Date(dateFuture);
                    $scope.ASTimer = "";
                    $scope.saveCurrent();
                } else if (i === 'GS') {
                    $scope.ToStart = true;
                    $scope.ASTimer = "";
                    $scope.getGame();
                } else {
                    getOneTip(i, $scope.Tips[i]);
                }
            } else {
                days = 0;
                hours = 0;
                mins = 0;
                secs = 0;
                out = "";

                amount = Math.floor(amount / 1000);

                days = Math.floor(amount / 86400);
                amount = amount % 86400;

                hours = Math.floor(amount / 3600);
                amount = amount % 3600;

                mins = Math.floor(amount / 60);
                amount = amount % 60;

                secs = amount;

                if (i === 'AS') {
                    out = "Autoswitch";
                } else if (i === 'GS') {
                    out = "Start the game";
                } else {
                    out = "Tip";
                }

                out += " will be ";

                if (days != 0) {
                    out += days + " day ";
                }

                if (hours != 0) {
                    out += hours + " hour ";
                }

                if (mins != 0) {
                    out += mins + " minute ";
                }

                out += secs + " seconds";

                if (i === 'AS' || i === 'GS') {
                    $scope.ASTimer = out;
                } else {
                    $scope.Tips[i].Tip = out;
                }

                $timeout(timer, 1000);
            }
        }

        timer();
    }
}]);