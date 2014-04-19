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
    if (type == "client")
        window.location.href = "/Client";
    else
        window.location.href = "/secure";

});