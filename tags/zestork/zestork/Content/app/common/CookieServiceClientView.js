ZestorkAppClientView.factory('CookieUtil', function ($rootScope, $location, $cookieStore, $routeParams) {

    return {
        CookieValue: function () {

            $rootScope.Authentication = null;
            var guidParam = getParameterByName('guid');
            var username = getParameterByName('username');
            var keepMeSignedIn = getParameterByName('keepMeSignedIn');

            if (guidParam != null && guidParam != '')
                $rootScope.Authentication = guidParam; //global variable

            if ($rootScope.Authentication != null) {
                if (keepMeSignedIn == "true") {
                    $.cookie('Authentication', $rootScope.Authentication, { expires: 365, path: '/' });
                    $.cookie('Username', username, { expires: 365, path: '/' });
                }
                else {
                    $.cookie('Authentication', $rootScope.Authentication, { path: '/' });
                    $.cookie('Username', username, { path: '/' });
                }
            }
            //$cookieStore.put("Authentication",$rootScope.Authentication)  

            //return $cookieStore.get('Authentication');
            return $.cookie('Authentication');
        },
        getUsername: function () {
            return $.cookie('Username');
        },
        getGuid: function () {
            return $.cookie('Authentication');
        },
        removeUsername: function () {
            $.removeCookie('Username', { path: '/' });
            return "removed";
        },
        removeGuid: function () {
            $.removeCookie('Authentication', { path: '/' });
            return "removed";
        },
        getKey: function () {
            return $.cookie('key');
        },
        removeKey: function () {
            $.removeCookie('key', { path: '/' });
            return "removed";
        },
        setReferralKey: function () {
            var ReferralKey = $routeParams.ref;
            if (ReferralKey != null || ReferralKey != "")
                $.cookie('ReferralKey', ReferralKey, { expires: 365, path: '/' });
            return "added";
        },
        getReferralKey: function () {
            return $.cookie('ReferralKey');
        },
        setKeepMeSignedInKey: function () {                       
            $.cookie('KeepMeSignedInKey', ReferralKey, { expires: 365, path: '/' });
            return "added";
        },
        getKeepMeSignedInKey: function () {
            return $.cookie('KeepMeSignedInKey');
        },
    };

});
