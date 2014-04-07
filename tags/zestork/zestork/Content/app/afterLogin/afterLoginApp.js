
var ZestorkAppAfterLogin = angular.module('ZestorkAppAfterLogin', ['ngCookies']);

ZestorkAppAfterLogin.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/afterLogin/contentView/initializing.html" }).
                   when("/search", { templateUrl: "../../Resource/templates/afterLogin/contentView/searchJob.html" }).
                   when("/edit", { templateUrl: "../../Resource/templates/afterLogin/contentView/edit.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/signup", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signup.html" }).
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

});

ZestorkAppAfterLogin.factory('CookieUtil',function($rootScope, $location, $cookieStore){
   
   return {
        CookieValue: function() {

            $rootScope.Authentication = null;
            var guidParam = getParameterByName('guid');
        
            if(guidParam != null && guidParam != '')                
                $rootScope.Authentication = guidParam; //global variable

            if ($rootScope.Authentication != null) {
                //$cookies.Authentication = $rootScope.Authentication;
                    $cookieStore.put("Authentication", $rootScope.Authentication);
            }
                    //$cookieStore.put("Authentication",$rootScope.Authentication)  

            return $cookieStore.get('Authentication');
        }
    };

});


ZestorkAppAfterLogin.run(function ($rootScope, $location) { //Insert in the function definition the dependencies you need.
    //Do your $on in here, like this:  

    $rootScope.$on("$locationChangeStart", function (event, next, current) {
    
        
        //getParameterByName('guid')
        //alert(getParameterByName('uid'));
        //getParameterByName('uid'); //global variable
        //        if (contextPath == "/signup" || contextPath == "/signup/user") {
        //            $rootScope.showSignUpButton = false;
        //            $rootScope.showLabelAlreadyRegistered = true;
        //        }
        //        else {
        //            $rootScope.showSignUpButton = true;
        //            $rootScope.showLabelAlreadyRegistered = false;
        //        }

    });
});

ZestorkAppAfterLogin.controller('masterPageController', function ($scope, $rootScope, $http, $location, CookieUtil) {

    $scope.firstTimeUserLoginViaSocialLinkPopUpTemplate = '../../Resource/templates/afterLogin/contentView/index/firstTimeLoginViaSocialLinkePopUpModal.html';

    $rootScope.classRadioButtonClient = "iradio_square-blue checked";
    $rootScope.classRadioButtonUser = "iradio_square-blue";

    $scope.infoClicked = function (message) {
        if (message == "user") {
            $rootScope.classRadioButtonUser = "iradio_square-blue checked";
            $rootScope.classRadioButtonClient = "iradio_square-blue";
        }
        else if (message == "client") {
            $rootScope.classRadioButtonClient = "iradio_square-blue checked";
            $rootScope.classRadioButtonUser = "iradio_square-blue";
        }
    }

    $rootScope.Authentication = CookieUtil.CookieValue();
    var headers = { 'Content-Type': 'application/json',
        'Authorization': $rootScope.Authentication
    };

    $http({
        url: '/Auth/userTypeInfoAvailable',
        method: "GET",
        headers: headers
    }).success(function (data, status, headers, config) {

        if (data != null) {
            //alert(data);
            if (data == "false")
                $('#firstTimeUserLoginViaSocialLinkPopUp').click();
            else {
                //$location.path("search");
            }
        }
        else {
            alert("user type info data not available");
        }
        //console.log(data);
    }).error(function (data, status, headers, config) {
        alert('Internal Server Error Occured !!');
    });

    $scope.person = {
        name: "Controller Sumit Chourasia after login"
    };

});

ZestorkAppAfterLogin.controller('submitUserTypeDetailController', function ($scope, $http, $rootScope,$location, CookieUtil) {
    $scope.userType = "NA";
    $scope.submitUserTypeDetails = function () {
        //alert("clicked");
        $rootScope.Authentication = CookieUtil.CookieValue();
        if ($rootScope.classRadioButtonUser.indexOf("checked") >= 0)
            $scope.userType = "user";
        else
            $scope.userType = "client";
        var headers = { 'Content-Type': 'application/json',
            'Authorization': $rootScope.Authentication
        };

        $http({
            url: '/Auth/submitUserTypeInfo/' + $scope.userType,
            method: "GET",
            //headers: { 'Content-Type': 'application/json' }
            headers: headers
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            $.unblockUI();
            if (data == "200") {
                $location.path("search");
            }
            else {
                alert("some error occured while submitting your data.");
            }
            //console.log(data);
        }).error(function (data, status, headers, config) {
            $.unblockUI();
            alert('Internal Server Error Occured !!');
        });

    }
});

//getting user info..
ZestorkAppAfterLogin.controller('getUserInfoController', function ($scope, $http, $rootScope,CookieUtil) {

        //CookieUtil.storeAuthCookie();            
        if(CookieUtil.CookieValue() != null)
        {            
            $rootScope.Authentication =  CookieUtil.CookieValue();
            //alert($rootScope.Authentication);
            //$cookieStore.Authentication = getParameterByName('guid');
        }
        else
        {          
                alert('authentication cookie is null'); 
                //window.location.href = "/?mssg=your session expired";
        }
    $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Profile Loading...</h1>' });
    $rootScope.Authentication=CookieUtil.CookieValue();
    $scope.masterPageUserDetailImageLink = "#/edit";

    var headers = { 'Content-Type': 'application/json',
        'Authorization': $rootScope.Authentication
    };
    
    $http({
        url: '/Auth/details',
        method: "GET",
        //headers: { 'Content-Type': 'application/json' }
        headers: headers
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        $.unblockUI();
        if (data != null) {
            $scope.details = data;
            //console.log(data);
            //alert($scope.details.FirstName);
        }
        else {
            //if data is null
        }
        //console.log(data);
    }).error(function (data, status, headers, config) {
        $.unblockUI();
        alert('Internal Server Error Occured !!');
    });
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}