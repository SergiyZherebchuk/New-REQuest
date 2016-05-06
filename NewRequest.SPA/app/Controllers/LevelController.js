requestApp.controller("LevelController", ['$scope', '$location', '$routeParams', '$timeout', 'LevelTypeService',
    'GamesService', 'AllLevelService', 'GameTaskService', 'GameTipsService', 'CodesService',

    function ($scope, $location, $routeParams, $timeout, LevelTypeService, GamesService, AllLevelService, 
              GameTaskService, GameTipsService, CodesService) {

    $scope.levelId = $routeParams.id;

    $scope.tab = 'task';
    $scope.OperTypeTask = 1;
    $scope.OperTypeTip = 1;
    $scope.disableMark = false;
    $scope.disableMarkSector = new Array();
    $scope.disableCode = false;

    $scope.Codes = new Array();

    getUserId();
    anotherFunc();

    function getUserId () {
        $scope.userId = parseInt($scope.globals);
        getLevel();
    }

    function getLevel() {
        AllLevelService.getLevelById($scope.levelId)
            .then(function (responce) {
                $scope.Level = responce.data;
                getGameId();
            });
    }

    function getGameId() {
        AllLevelService.getGameIdByLevel($scope.levelId)
            .then(function (responce) {
                $scope.gameId = responce.data;

                if ($scope.gameId == 0) {
                    $location.path('/nogame');
                } else {
                    getGame();
                    isAuthor();
                }
            });
    }

    function getGame() {
        GamesService.getOneGame($scope.gameId)
            .then(function (responce) {
                $scope.Game = responce.data;
            });
    }

    function isAuthor() {
        GamesService.getOneGame($scope.gameId)
            .then(function (responce) {
                if (responce.data.AuthorId == $scope.userId) {
                    getTask();
                    getTips();
                    getLevelType();
                    getCodes();
                } else {
                    $location.path('/noauthor');
                }
            });
    }

    function anotherFunc() {
        getTask();
        getTips();
        getLevelType();
        getCodes();
    }

    function getTask() {
        GameTaskService.getOne($scope.levelId)
            .then(function (responce) {
                var result = responce.data;

                if (result != null) {
                    $scope.TaskId = result.Id;
                    $scope.Task = result.Task;

                    if (result.Autoswitch !== null) {
                        $scope.ats = 'yes';
                        $scope.Hours = parseInt(result.Autoswitch / 3600);
                        $scope.Minute = parseInt(result.Autoswitch % 3600 / 60);
                        $scope.Second = result.Autoswitch % 60;
                    } else {
                        $scope.ats = 'no';
                    }

                    $scope.OperTypeTask = 2;
                }
            });
    }

    function getTips() {
        GameTipsService.getOneLevelTip($scope.levelId)
            .then(function (responce) {
                $scope.AllTips = responce.data;

                $timeout(getTips, 10000);
            });
    }

    function getLevelType() {
        LevelTypeService.getLevelType($scope.levelId)
            .then(function (responce) {
                var res = responce.data;

                if (res != null) {
                    $scope.CodeType = res.Type;
                    $scope.CodeTypeId = res.Id;
                    $scope.OperTypeLvlType = 2;
                }

                getCodes();
            });
    }

    function getCodes () {
        if ($scope.CodeType == 1 || $scope.CodeType == 4) {
            getOneCode();
        } else if ($scope.CodeType == 2 || $scope.CodeType == 3) {
            getSectorCodes();
        }
    }

    function getOneCode () {
        CodesService.getOneCode($scope.levelId)
            .then(function (responce) {
                var res = responce.data;

                if (res != null) {
                    $scope.Code = res.Code1;
                    $scope.CodeId = res.Id;
                    $scope.disableCodeTypeMark = true;
                    $scope.delMark = true;
                    $scope.disableCode = 2;
                } else {
                    $scope.disableCodeTypeMark = false;
                    $scope.delMark = false;
                    $scope.disableCode = 1;
                }
            });
    }
    
    function getSectorCodes () {
        CodesService.getSectorCodes($scope.levelId)
            .then(function (responce) {
                $scope.ManyCodes = responce.data;

                if ($scope.ManyCodes.length > 0) {
                    $scope.SecNum = $scope.ManyCodes.length;
                    $scope.NumSectors = $scope.ManyCodes.length;

                    if ($scope.CodeType === 3) {
                        getOlympianCode($scope.ManyCodes);
                    }

                    $scope.disableCodeTypeMark = true;
                    $scope.OperTypeManyCode = 2;
                } else {
                    $scope.OlympType = 1;
                    $scope.SecNum = 1;
                    $scope.disableCodeTypeMark = false;
                    $scope.OperTypeManyCode = 1;
                }
            });
    }

    function getOlympianCode(ManyCodes) {
        var max = 0;

        for (i = 0; i < ManyCodes.length; i++) {
            if (ManyCodes[i].Sector > max) {
                max = ManyCodes[i].Sector;
            }
        }

        if (max > 7) {
            $scope.OlympType = 2;
        } else {
            $scope.OlympType = 1;
        }
    }


    $scope.getOneTip = function (tipId) {
        $http.get("http://localhost:13840/api/TipAPI/" + tipId)
            .then(function (responce) {
                $scope.TipId = responce.data.Id;
                $scope.Tip = responce.data.Tip;
                $scope.TipMinute = parseInt(responce.data.Time / 60);
                $scope.TipSecond = responce.data.Time % 60;
            });
        $scope.OperTypeTip = 2;
    }

    $scope.saveTask = function () {
        if ($scope.ats == 'yes') {
            $scope.Autoswitch = $scope.Hours * 3600 + $scope.Minute * 60 + $scope.Second;
        }
        else {
            $scope.Autoswitch = null;
        }
        
        if ($scope.OperTypeTask == 1) {

            var Task = {
                LevelId: $scope.levelId,
                Task: $scope.Task,
                Autoswitch: $scope.Autoswitch
            }

            GameTaskService.addNewTask(Task)
                .then(function (responce) {
                    getTask();
                });
        }
        else {
            var Task = {
                Id: $scope.TaskId,
                LevelId: $scope.levelId,
                Task: $scope.Task,
                Autoswitch: $scope.Autoswitch
            }

            GameTaskService.updateTask(Task)
            
            getTask();
        }        
    }
    

    $scope.editOneTip = function (oneTip) {
        $scope.TipId = oneTip.Id;
        $scope.Tip = oneTip.Tip;
        $scope.TipMinute = parseInt(oneTip.Times / 60);
        $scope.TipSecond = oneTip.Times % 60;

        $scope.OperTypeTip = 2;
    }

    $scope.saveTip = function () {
        $scope.Time = $scope.TipMinute * 60 + $scope.TipSecond;
        
        if ($scope.OperTypeTip == 1) {

            var Tip = {
                LevelId: $scope.levelId,
                Tip: $scope.Tip,
                Times: $scope.Time
            }

            GameTipsService.addNewTip(Tip)
                .then(function (responce) {
                    clearTipPanel();
                });
        }
        else {

            var Tip = {
                Id: $scope.TipId,
                LevelId: $scope.levelId,
                Tip: $scope.Tip,
                Times: $scope.Time
            }

            GameTipsService.updateTip(Tip);

            clearTipPanel();
        }
    }

    $scope.deleteOneTip = function (tipId) {
        GameTipsService.deleteTip(tipId);
        
        getTips();
    }


    $scope.saveLevelType = function () {
        if ($scope.OperTypeLvlType == 2) {
            var LevelType = {
                Id: $scope.CodeTypeId,
                LevelId: $scope.levelId,
                Type: $scope.CodeType
            }

            LevelTypeService.update(LevelType)

            getLevelType();
        } else {
            var LevelType = {
                LevelId: $scope.levelId,
                Type: $scope.CodeType
            }

            LevelTypeService.add(LevelType)
                .then(function (responce) {
                    $scope.CodeType = responce.data.Type;
                });

            getLevelType();
        }        
    }
    
    $scope.saveOlymp = function () {
        var length;
        var name;
        var osn;

        if($scope.OlympType == 1) {
            length = 7;
            osn = 4;
        } else {
            length = 15;
            osn = 8;
        }

        for (i = 1; i < length + 1; i++) {
            if (length == 7) {
                if (i < 5) {
                    name = i.toString();
                } else {
                    name = (i - osn) + '+' + (i - (osn - 1)) + '=' + i;
                    osn--;
                }
            } else {

                if (i < 9) {
                    name = i.toString();
                } else {
                    name = (i - osn) + '+' + (i - (osn - 1)) + '=' + i;
                    osn--;
                }
            }

            if($scope.ManyCodes.length >= i-1) {
                $scope.ManyCodes[i-1] = {
                    LevelId: $scope.levelId,
                    Sector: i,
                    Name: name,
                    Code1: null
                };
            } else if ($scope.ManyCodes[i].Sector !== i) {
                $scope.ManyCodes.push({
                    LevelId: $scope.levelId,
                    Sector: i,
                    Name: name,
                    Code1: null
                });
            }
        }

        $scope.SaveManyCode = true;
    }

    $scope.addSector = function () {
        for (i = 0; i < $scope.NumSectors; i++) {
            if($scope.ManyCodes[i] == null) {
                $scope.ManyCodes[i] = {
                    LevelId: $scope.levelId,
                    Sector: i + 1,
                    Name: null,
                    Code1: null
                };
            }
        }

        $scope.SaveManyCode = true;
    }

    $scope.saveOneCode = function () {
        if ($scope.disableCode == 2) {
            var Code = {
                Id: $scope.CodeId,
                LevelId: $scope.levelId,
                Code1: $scope.Code
            }

            CodesService.updateOneCode(Code);

            getOneCode();
        }
        else {
            var Code = {
                LevelId: $scope.levelId,
                Code1: $scope.Code
            }

            CodesService.addNewCode(Code);

            getOneCode();
        }
    }

    $scope.deleteOneCode = function (codeId) {
        CodesService.deleteOneCode(codeId);
        getOneCode();
    }



    $scope.saveManyCodes = function () {       
        if ($scope.OperTypeManyCode == 1) {
            CodesService.addNewSectorCodes($scope.ManyCodes);
        } else {
            CodesService.updateSectorCode($scope.ManyCodes);
        }

        getCodes();
    }

    $scope.saveSectorCode = function () {
        if ($scope.OperTypeManyCode == 2) {
            CodesService.updateSectorCode($scope.ManyCodes);
        }
        else {
            CodesService.addNewSectorCodes($scope.ManyCodes);
        }

        getCodes();
    }

    $scope.deleteManyCodes = function () {
        CodesService.deleteSectorCode($scope.levelId);

        getCodes();
    }

    function clearTipPanel() {
        $scope.Id = '';
        $scope.TipId = '';
        $scope.Tip = '';
        $scope.TipMinute = '';
        $scope.TipSecond = '';
        
        getTips();
    }
}]);