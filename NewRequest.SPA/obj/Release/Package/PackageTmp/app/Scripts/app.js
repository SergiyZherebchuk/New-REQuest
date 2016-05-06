var requestApp = angular.module("requestApp", ['ngRoute', 'ngCookies']);

requestApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'app/Views/Home/Index.html',
        controller: 'HomeController'
    }); // +

    $routeProvider.when('/Games/Active', {
        templateUrl: 'app/Views/Games/Active.html',
        controller: 'GamesController'
    });// ++
    $routeProvider.when('/Games/Archives', {
        templateUrl: 'app/Views/Games/Archives.html',
        controller: 'GamesController'
    }); // ++
    $routeProvider.when('/Games/Announcement', {
        templateUrl: 'app/Views/Games/Announcement.html',
        controller: 'GamesController'
    }); // ++
    /* $routeProvider.when('/Games/Info', {
        templateUrl: 'app/Views/Games/Info.html',
        controller: 'InfoController'
    }); */ // 

    $routeProvider.when('/Team', {
        templateUrl: 'app/Views/Team/Index.html',
        controller: 'TeamController'
    }); // ++
    $routeProvider.when('/Team/:id', {
        templateUrl: 'app/Views/Team/OneTeam.html',
        controller: 'OneTeamController'
    }); // ++
    $routeProvider.when('/MyTeam', {
        templateUrl: 'app/Views/Team/MyTeam.html',
        controller: 'MyTeamController'
    }); // +-?
    $routeProvider.when('/Teams/New', {
        templateUrl: 'app/Views/Team/NewTeam.html',
        controller: 'TeamController'
    }); // ++

    $routeProvider.when('/Authors/NewGame', {
        templateUrl: 'app/Views/Authors/Newgame.html',
        controller: 'GamesController'
    }); // ++
    $routeProvider.when('/Authors/MyGames', {
        templateUrl: 'app/Views/Authors/Mygame.html',
        controller: 'GamesController'
    }); // ++
    $routeProvider.when('/Authors/EditGame/:id', {
        templateUrl: 'app/Views/Authors/Editgame.html',
        controller: 'GamesController'
    }); // ++
    $routeProvider.when('/Authors/AllLevels/:id', {
        templateUrl: 'app/Views/Authors/AllLevels.html',
        controller: 'LevelsController'
    }); // +
    $routeProvider.when('/Authors/Level/:id', {
        templateUrl: 'app/Views/Authors/Level.html',
        controller: 'LevelController'
    }); // +

    $routeProvider.when('/Info/About', {
        templateUrl: 'app/Views/Info/About.html',
        controller: 'InfoController'
    }); // 
    $routeProvider.when('/Info/Contacts', {
        templateUrl: 'app/Views/Info/Contacts.html',
        controller: 'InfoController'
    }); // 
    $routeProvider.when('/Info/Ranking', {
        templateUrl: 'app/Views/Info/Ranking/Index.html',
        controller: 'RankingController'
    }); // +
    $routeProvider.when('/Info/Ranking/Player', {
        templateUrl: 'app/Views/Info/Ranking/TopPlayer.html',
        controller: 'RankingController'
    }); // +
    $routeProvider.when('/Info/Ranking/TeamPlayer', {
        templateUrl: 'app/Views/Info/Ranking/TopTeamPlayer.html',
        controller: 'RankingController'
    }); // +
    $routeProvider.when('/Info/Ranking/Team', {
        templateUrl: 'app/Views/Info/Ranking/TopTeam.html',
        controller: 'RankingController'
    }); // +

    $routeProvider.when('/GameInfo/:id', {
        templateUrl: 'app/Views/Games/GameInfo.html',
        controller: 'GameInfoController'
    }); // +
    
    $routeProvider.when('/RealGame/:id', {
        templateUrl: 'app/Views/RealGame/Index.html',
        controller: 'RealGameController'
    }); // 

    $routeProvider.when('/Statistic/:id', {
        templateUrl: 'app/Views/Statistic/Index.html',
        controller: 'StatisticController'
    }); // +

    $routeProvider.when('/User/:id', {
        templateUrl: 'app/Views/Users/Index.html',
        controller: 'UsersController'
    }); // 
    
    $routeProvider.when('/login', {
        templateUrl: 'app/Views/Auth/Login.html',
        controller: 'AuthController'
    }); // 
    $routeProvider.when('/register', {
        templateUrl: 'app/Views/Auth/Register.html',
        controller: 'AuthController'
    }); // +

    $routeProvider.when('/nogame', {
        templateUrl:'app/Views/Error/NoGame.html'
    }); // +
    $routeProvider.when('/noteam', {
        templateUrl: 'app/Views/Error/NoTeam.html'
    }); // +
    $routeProvider.when('/noauthor', {
        templateUrl: 'app/Views/Error/NoRules.html'
    }); // +

    $routeProvider.otherwise({
        redirectTo: '/'
    }); // 
}]);

requestApp.run(['$rootScope', '$location', '$timeout', '$cookies', '$http',
    function ($rootScope, $location, $timeout, $cookies, $http) {
    // keep user logged in after page refresh

    $rootScope.globals = $cookies.get('globals');
    $rootScope.MyRole = $cookies.get('role');

    if ($rootScope.globals !== undefined) {
        //$location.path('/');// jshint ignore:line
    }
 
    $rootScope.$on('$locationChangeStart', function (event, next, current) {

        $rootScope.globals = $cookies.get('globals');
        $rootScope.MyRole = $cookies.get('role');

        // redirect to login page if not logged in and trying to access a restricted page
        var restrictedPage = $.inArray($location.path(), ['/login', '/register']) === -1;
        var loggedIn = $rootScope.globals;
        if (restrictedPage && loggedIn === undefined) {
            $location.path('/login');
        }
    });
}]);

/*
requestApp.directive('draggable', function () {
    return function (scope, element) {

        var el = element[0];

        el.draggable = true;

        el.addEventListener(
          'dragstart',
          function (e) {
              e.dataTransfer.effectAllowed = 'move';
              e.dataTransfer.setData('Text', this.id);
              this.classList.add('drag');
              return false;
          },
          false
        );

        el.addEventListener(
          'dragend',
          function (e) {
              this.classList.remove('drag');
              return false;
          },
          false
        );
    }
});

requestApp.directive('droppable', function () {
    return {
        scope: {
            drop: '&',
            bin: '='
        },
        link: function (scope, element) {
            // again we need the native object
            var el = element[0];

            el.addEventListener(
              'dragover',
              function (e) {
                  e.dataTransfer.dropEffect = 'move';
                  // allows us to drop
                  if (e.preventDefault) e.preventDefault();
                  this.classList.add('over');
                  return false;
              },
              false
            );

            el.addEventListener(
              'dragenter',
              function (e) {
                  this.classList.add('over');
                  return false;
              },
              false
            );

            el.addEventListener(
              'dragleave',
              function (e) {
                  this.classList.remove('over');
                  return false;
              },
              false
            );

            el.addEventListener(
              'drop',
              function (e) {
                  // Stops some browsers from redirecting.
                  if (e.stopPropagation) e.stopPropagation();

                  this.classList.remove('over');

                  var binId = this.id;
                  var item = document.getElementById(e.dataTransfer.getData('Text'));
                  this.appendChild(item);
                  // call the passed drop function
                  scope.$apply(function (scope) {
                      var fn = scope.drop();
                      if ('undefined' !== typeof fn) {
                          fn(item.id, binId);
                      }
                  });

                  return false;
              },
              false
            );
        }
    }
});
*/