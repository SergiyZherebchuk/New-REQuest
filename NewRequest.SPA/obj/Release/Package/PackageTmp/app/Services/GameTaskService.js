requestApp.service('GameTaskService', ['$http', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getOne = function (levelId) {
        return $http.get(pre + "api/GameTaskAPI/" + levelId);
    }

    this.addNewTask = function (Task) {
        var request = $http({
            method: "post",
            url: pre + "api/GameTaskAPI",
            data: Task
        });

        return request;
    }

    this.updateTask = function (Task) {
        var request = $http({
            method: "put",
            url: pre + "api/GameTaskAPI",
            data: Task
        });
    }

    this.deleteTask = function (taskId) {
        var request = $http({
            method: "delete",
            url: pre + "api/GameTaskAPI/" + taskId
        });
    }
}]);