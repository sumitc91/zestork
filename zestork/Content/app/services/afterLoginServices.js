
ZestorkAppAfterLogin.factory('afterLoginServices', function ($http, $rootScope, $location, $cookieStore, CookieUtil) {

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
        }
    };

});