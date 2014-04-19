
ZestorkAppClientView.factory('afterLoginServices', function ($http, $rootScope, $location, $cookieStore, CookieUtil) {

    return {

        setPageThemeColor: function (color) {

            $rootScope.Authentication = CookieUtil.CookieValue();
            var headers = {
                'Content-Type': 'application/json',
                'Authorization': $rootScope.Authentication
            };

            $http({
                url: '/Auth/submitUserPageThemeColor/' + color,
                method: "GET",
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here            
                $rootScope.pageThemeColor = color;
                //console.log(data);
            }).error(function (data, status, headers, config) {
                alert('Internal Server Error Occured !!');
            });

            return "";
        },
        setPageLayoutWidth: function (Layout) {

            $rootScope.Authentication = CookieUtil.CookieValue();
            var headers = {
                'Content-Type': 'application/json',
                'Authorization': $rootScope.Authentication
            };

            $http({
                url: '/Auth/submitUserPageLayoutWidth/' + Layout,
                method: "GET",
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here            
                $rootScope.pageLayoutWidth = Layout;
                //console.log(data);
            }).error(function (data, status, headers, config) {
                alert('Internal Server Error Occured !!');
            });

            return "";
        },
        setPageTopBar: function (TopbarType) {

            $rootScope.Authentication = CookieUtil.CookieValue();
            var headers = {
                'Content-Type': 'application/json',
                'Authorization': $rootScope.Authentication
            };

            $http({
                url: '/Auth/submitUserPageTopbar/' + TopbarType,
                method: "GET",
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here            
                $rootScope.pageTopbar = TopbarType;
                //console.log(data);
            }).error(function (data, status, headers, config) {
                alert('Internal Server Error Occured !!');
            });

            return "";
        },
        setPageSideBar: function (SidebarType) {

            $rootScope.Authentication = CookieUtil.CookieValue();
            var headers = {
                'Content-Type': 'application/json',
                'Authorization': $rootScope.Authentication
            };

            $http({
                url: '/Auth/submitUserPageSidebar/' + SidebarType,
                method: "GET",
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here            
                $rootScope.pageSidebar = SidebarType;
                //console.log(data);
            }).error(function (data, status, headers, config) {
                alert('Internal Server Error Occured !!');
            });

            return "";
        },
        setUserPrivateKeyValue: function () {

            $rootScope.Authentication = CookieUtil.CookieValue();
            var headers = {
                'Content-Type': 'application/json',
                'Authorization': $rootScope.Authentication
            };

            $http({
                url: '/Auth/getKeyVal',
                method: "GET",
                headers: headers
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here            
                var retVal = setUserPrivateKey(data.key);
                //alert(retVal);
                //console.log(data);
            }).error(function (data, status, headers, config) {
                alert('Internal Server Error Occured !!');
            });

            return "";
        },

    };

});

function setUserPrivateKey(key) {
    $.cookie('key', key, { expires: 365, path: '/' });
    return "set";
}
function getUserPrivateKey() {
    return $.cookie('key');
}
function removeUserPrivateKey(key) {
    $.removeCookie('key', { path: '/' });
    return "removed";
}