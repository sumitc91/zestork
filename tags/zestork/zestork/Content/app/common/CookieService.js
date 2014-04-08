ZestorkAppAfterLogin.factory('CookieUtil', function ($rootScope, $location, $cookieStore) {

    return {
        CookieValue: function () {

            $rootScope.Authentication = null;
            var guidParam = getParameterByName('guid');
            var username = getParameterByName('username');

            if (guidParam != null && guidParam != '')
                $rootScope.Authentication = guidParam; //global variable

            if ($rootScope.Authentication != null) {
                $.cookie('Authentication', $rootScope.Authentication, { expires: 365, path: '/' });
                $.cookie('Username', username, { expires: 365, path: '/' });
            }
            //$cookieStore.put("Authentication",$rootScope.Authentication)  

            //return $cookieStore.get('Authentication');
            return $.cookie('Authentication');
        }
    };

});