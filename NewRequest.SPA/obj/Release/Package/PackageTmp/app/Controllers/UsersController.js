requestApp.controller("UsersController", ['$scope', 'UsersService',  function ($scope, UsersService) {
    $scope.userId = parseInt($scope.globals);

    var getUsers = function () {
        UsersService.getUsers($scope.userId)
            .then(function (response) {
                $scope.Users = response.data;
            });
    };

    getUsers();
}]);