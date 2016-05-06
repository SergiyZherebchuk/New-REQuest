requestApp.controller("AuthController", ['$scope', '$location', '$rootScope', 'AuthService', 'UsersService',
    function ($scope, $location, $rootScope, AuthService, UsersService) {


    getUser();

    function getUser() {
        UsersService.getUserById($scope.globals)
            .then(function (responce) {
                $rootScope.MyLogin = responce.data.Login;
            });
    }

    $scope.register = function () {
        var dataLoading = true;

        var user = {
            Name: $scope.firstName,
            Surname: $scope.lastName,
            Login: $scope.login,
            Password: $scope.password,
            TypeId: 1
        }

        UsersService.addUser(user)
            .then(function (responce) {
                if (responce.data !== null) {
                    $scope.IsRegister = true;
                    $location.path('/login');
                } else {
                    $scope.IsRegister = false;
                    dataLoading = false;
                }
            });
    };
 
    $scope.signIn = function () {
        var dataLoading = true;

        AuthService.Login($scope.login, $scope.password, function (response) {
            if (response.data) {
                $scope.getUser($scope.login);
                $scope.IsSignIn = false;
            } else {
                $scope.IsSignIn = true;
                dataLoading = false;
            }
        });
    };

    $scope.getUser = function (login) {
        UsersService.getUserByLogin(login)
            .then(function (responce) {
                AuthService.SetCredentials(responce.data.TypeId, responce.data.Id);

                $location.path('/');
            });
    }

    $scope.logout = function () {
        AuthService.RemoveAll();

        $location.path('/login');
    }
}]);