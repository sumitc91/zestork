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
    window.location.href = "/";

});