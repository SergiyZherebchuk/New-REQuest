requestApp.service('TimesService', function ($http) {
    var pre = "http://nwrqwapi.gear.host/";

    this.getTimeZone = function () {
        var minutes = new Date().getTimezoneOffset();
        
        return (minutes / 60);
    }

    this.getTime = function () {
        return $http.get(pre + "api/TimesAPI/Time");
    }
    
});