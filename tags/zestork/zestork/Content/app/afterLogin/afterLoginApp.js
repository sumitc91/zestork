
var ZestorkAppAfterLogin = angular.module('ZestorkAppAfterLogin', ['ngCookies']);

ZestorkAppAfterLogin.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/afterLogin/contentView/searchJob.html" }).
                   when("/search", { templateUrl: "../../Resource/templates/afterLogin/contentView/searchJob.html" }).
                   when("/initialize", { templateUrl: "../../Resource/templates/afterLogin/contentView/initializing.html" }).
                   when("/edit", { templateUrl: "../../Resource/templates/afterLogin/contentView/edit.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/signup", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signup.html" }).
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

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

ZestorkAppAfterLogin.controller('masterPageController', function ($scope, $rootScope, $http, $location, CookieUtil, afterLoginServices) {

    $scope.firstTimeUserLoginViaSocialLinkPopUpTemplate = '../../Resource/templates/afterLogin/contentView/index/firstTimeLoginViaSocialLinkePopUpModal.html';
    $rootScope.Username = CookieUtil.getUsername();
    $rootScope.Key = CookieUtil.getKey();
    //alert(CookieUtil.getUsername());
    $http({
        url: '/Account/isValidToken/' + CookieUtil.getGuid() + '?username=' + CookieUtil.getUsername() + '&key=' + CookieUtil.getKey(),
        method: "GET"
        //headers: { 'Content-Type': 'application/json' }            
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here                        
        
        //console.log(data);
    }).error(function (data, status, headers, config) {       
        alert('Internal Server Error Occured !!');
    });

    afterLoginServices.setUserPrivateKeyValue(); // set user private key value;
    //$rootScope.pageThemeColor = afterLoginServices.pageThemeColor();
    $rootScope.classRadioButtonClient = "iradio_square-blue checked";
    $rootScope.classRadioButtonUser = "iradio_square-blue";

    $scope.submitUserTemplateColordDetails = function (color) {
        afterLoginServices.setPageThemeColor(color);
    }

    $scope.submitUserTemplateLayoutWidth = function (Layout) {
        afterLoginServices.setPageLayoutWidth(Layout);
    }

    $scope.submitUserTemplateSetTopBar = function (TopbarType) {
        afterLoginServices.setPageTopBar(TopbarType);
    }

    $scope.submitUserTemplateSetSideBar = function (SidebarType) {
        afterLoginServices.setPageSideBar(SidebarType);
    }
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
    var headers = {
        'Content-Type': 'application/json',
        'Authorization': $rootScope.Authentication
    };

    $http({
        url: '/Auth/userTypeInfoAvailable',
        method: "GET",
        headers: headers
    }).success(function (data, status, headers, config) {

        if (data != null) {
            //alert(data);
            if (data == "false") {
                $('#firstTimeUserLoginViaSocialLinkPopUp').click();
                $rootScope.firstTimeUserLoginViaSocialLinkPopUpOpened = true;
            }
            else {
                $rootScope.firstTimeUserLoginViaSocialLinkPopUpOpened = false;
                //$location.path("search");
            }
        }
        else {
            alert("user type info data not available");
        }
        //console.log(data);
    }).error(function (data, status, headers, config) {

        //alert('Internal Server Error Occured !!');
    });

    $scope.person = {
        name: "Controller Sumit Chourasia after login"
    };

});

ZestorkAppAfterLogin.controller('submitUserTypeDetailController', function ($scope, $route, $http, $rootScope, $location, CookieUtil) {
    $scope.userType = "NA";
    $scope.submitUserTypeDetails = function () {
        //alert("clicked");
        $rootScope.Authentication = CookieUtil.CookieValue();
        if ($rootScope.classRadioButtonUser.indexOf("checked") >= 0)
            $scope.userType = "user";
        else
            $scope.userType = "client";
        var headers = {
            'Content-Type': 'application/json',
            'Authorization': $rootScope.Authentication
        };

        $http({
            url: '/Auth/submitUserTypeInfo/' + $scope.userType,
            method: "GET",
            //headers: { 'Content-Type': 'application/json' }
            headers: headers
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here            
            if (data == "200") {
                //$route.reload();
                window.location.href = "/secure";
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

ZestorkAppAfterLogin.controller('submitUserPasswordDetailController', function ($scope, $http, $rootScope, $location, CookieUtil) {
    $scope.userType = "NA";
    $scope.submitUserPasswordDetails = function () {
        //alert("clicked");
        if ($scope.password == $scope.confirmPassword) {
            $rootScope.Authentication = CookieUtil.CookieValue();
            $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Changing Password...</h1>' });
            var headers = {
                'Content-Type': 'application/json',
                'Authorization': $rootScope.Authentication
            };

            var changePasswordRequestData = {
                password: $scope.password
            }

            $http({
                url: '/Auth/changeUserPassword',
                method: "POST",
                //headers: { 'Content-Type': 'application/json' }
                headers: headers,
                data: changePasswordRequestData
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here  
                
                if (data == "200") {
                    //alert("password successfully changed !!!");
                    $.unblockUI();
                    $('#firstTimeUserLoginViaSocialLinkChangePasswordPopUpClose').click();
                }
                else {
                    //alert("some error occured while submitting your data.");
                    $('#firstTimeUserLoginViaSocialLinkChangePasswordPopUpClose').click();
                    alert("error occured while resetting your password, try again.");
                    $.unblockUI();
                }
                //console.log(data);
            }).error(function (data, status, headers, config) {
                alert('Internal Server Error Occured !!');
                //window.location.href = "/";
                $('#firstTimeUserLoginViaSocialLinkChangePasswordPopUpClose').click();
            });
        }
        else {
            alert("password didn't match");
        }



    }
});

//getting user info..
ZestorkAppAfterLogin.controller('getUserInfoController', function ($scope, $http, $rootScope, CookieUtil) {

    //CookieUtil.storeAuthCookie();            
    if (CookieUtil.CookieValue() != null) {
        $rootScope.Authentication = CookieUtil.CookieValue();
        //alert($rootScope.Authentication);
        //$cookieStore.Authentication = getParameterByName('guid');
    }
    else {
        alert('authentication cookie is null');
        //window.location.href = "/?mssg=your session expired";
    }
    $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Profile Loading...</h1>' });
    $rootScope.Authentication = CookieUtil.CookieValue();
    $scope.masterPageUserDetailImageLink = "#/edit";

    var headers = {
        'Content-Type': 'application/json',
        'Authorization': $rootScope.Authentication
    };

    $http({
        url: '/Auth/details',
        method: "GET",
        //headers: { 'Content-Type': 'application/json' }
        headers: headers
    }).success(function (ResponseData, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        $.unblockUI();
        var data = ResponseData.details;
        //alert("after Login APP " +ResponseData.Autherized);
        if (ResponseData.Autherized == false)
            window.location.href = "/Client";
        if (data != null) {
            $rootScope.Username = data.Username;
            if (data.Locked == true) {
                location.href = "/Locked/index/" + $rootScope.Authentication;
            }
            $scope.details = data;
            $rootScope.pageThemeColor = data.PageThemeColor;
            $rootScope.pageLayoutWidth = data.pageLayoutWidth;
            $rootScope.pageTopbar = data.pageTopbar;
            if (data.pageLayoutWidth == "container") {
                $rootScope.pageLayoutWidthFixedActive = "active set-fixed";
                $rootScope.pageLayoutWidthFluidActive = "set-fluid";
            }
            else {
                $rootScope.pageLayoutWidthFixedActive = "set-fixed";
                $rootScope.pageLayoutWidthFluidActive = "active set-fluid";
            }

            if (data.pageTopbar == "navbar-fixed-top") {
                $rootScope.pageLayoutTopBarFixedActive = "active set-topbar-fixed";
                $rootScope.pageLayoutTopBarDefaultActive = "set-topbar-default";
                $rootScope.pageLayoutTopBarNavFixedValue = "nav-fixed"; //nav-fixed
            }
            else {
                $rootScope.pageLayoutTopBarFixedActive = "set-topbar-fixed";
                $rootScope.pageLayoutTopBarDefaultActive = "active set-topbar-default";
                $rootScope.pageLayoutTopBarNavFixedValue = "";
            }

            if (data.pageSidebar == "Fixed") {
                  $('#set_sidebar_fixed_id').click();
            }
            else {
                  $('#set_sidebar_default_id').click();
            }
           
            //$rootScope..navigationTopBarClass navbar-fixed-top container-fluid nav-fixed
            //container-fluid  && data.pageTopbar == ""
            var imageUrlSplitted = data.ImageUrl.split(".");
            var uploadedImageLinkSmall = imageUrlSplitted[0] + "." + imageUrlSplitted[1] + "." + imageUrlSplitted[2] + "s." + imageUrlSplitted[3];
            $scope.details.ImageUrl = uploadedImageLinkSmall;

            //console.log(data);
            //alert($scope.details.FirstName);
        }
        else {
            //if data is null
        }
        //console.log(data);
    }).error(function (data, status, headers, config) {
        $.unblockUI();
        //alert('Internal Server Error Occured !!');
        //window.location.href = "/";

    });
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
