//getting user info..
ZestorkAppAfterLogin.controller('welcomeAfterLoginController', function ($scope, $http, $rootScope, CookieUtil) {

    //CookieUtil.storeAuthCookie();            
    if (CookieUtil.CookieValue() != null) {
        $rootScope.Authentication = CookieUtil.CookieValue();

    }
    else {
        alert('authentication cookie is null');
        //window.location.href = "/?mssg=your session expired";
    }
    var type = getParameterByName('type');
    alert("wecome page after login template js  type " + type);
    if (type == "client")
        window.location.href = "/Client";
    else
        window.location.href = "/secure";

});

ZestorkAppAfterLogin.controller('welcomeAfterLoginController', function ($scope, $http, $rootScope, CookieUtil) {

    //CookieUtil.storeAuthCookie();            
    if (CookieUtil.CookieValue() != null) {
        $rootScope.Authentication = CookieUtil.CookieValue();
        var headers = {
            'Content-Type': 'application/json',
            'Authorization': $rootScope.Authentication
        };

        $http({
            url: '/Auth/isUserClient',
            method: "GET",
            headers: headers
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here

            if (data == "true")
                window.location.href = "/Client";
            else
                window.location.href = "/userpage";
        }).error(function (data, status, headers, config) {
            //alert('Internal Server Error Occured !!');
        });
    }
    else {
        alert('authentication cookie is null');
        //window.location.href = "/?mssg=your session expired";
    }
    var type = getParameterByName('type');
    
    window.location.href = "/secure";

});
ZestorkAppAfterLogin.controller('secureLoginCheckUserType', function ($scope, $http, $rootScope, CookieUtil) {

    $rootScope.Authentication = CookieUtil.CookieValue();
    var headers = {
        'Content-Type': 'application/json',
        'Authorization': $rootScope.Authentication
    };

    $http({
        url: '/Auth/isUserClient',
        method: "GET",
        headers: headers
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        
        if (data == "true")
            window.location.href = "/Client";
        else
            window.location.href = "/userpage";
    }).error(function (data, status, headers, config) {
        alert('Internal Server Error Occured !!');
    });

});