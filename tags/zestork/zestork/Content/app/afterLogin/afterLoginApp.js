
var ZestorkAppAfterLogin = angular.module('ZestorkAppAfterLogin', []);

ZestorkAppAfterLogin.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/afterLogin/contentView/searchJob.html" }).
                   when("/services", { templateUrl: "../../Resource/templates/beforeLogin/contentView/services.html" }).
                   when("/about", { templateUrl: "../../Resource/templates/beforeLogin/contentView/about.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/signup", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signup.html" }).
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

});

ZestorkAppAfterLogin.run(function ($rootScope, $location) { //Insert in the function definition the dependencies you need.
    //Do your $on in here, like this:  

    $rootScope.$on("$locationChangeStart", function (event, next, current) {
        //Do your things        
        //var path2 = $location.path();

        var searc = $location.search()

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

ZestorkAppAfterLogin.controller('masterPageController', function ($scope, $rootScope) {

    $scope.firstTimeUserLoginViaSocialLinkPopUpTemplate = '../../Resource/templates/afterLogin/contentView/index/firstTimeLoginViaSocialLinkePopUpModal.html';

    $scope.classRadioButtonClient = "iradio_square-blue checked";
    $scope.classRadioButtonUser = "iradio_square-blue";

    $scope.infoClicked = function (message) {
        if (message == "user") {
            $scope.classRadioButtonUser = "iradio_square-blue checked";
            $scope.classRadioButtonClient = "iradio_square-blue";
        }
        else if (message == "client") {
            $scope.classRadioButtonClient = "iradio_square-blue checked";
            $scope.classRadioButtonUser = "iradio_square-blue";
        }
    }
    $('#firstTimeUserLoginViaSocialLinkPopUp').click();

    $scope.person = {
        name: "Controller Sumit Chourasia after login"
    };

});

//getting user info..
ZestorkAppAfterLogin.controller('getUserInfoController', function ($scope, $http, $rootScope) {
    $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Profile Loading...</h1>' });
    $http({
        url: '/' + getParameterByName('uid') + 'User/details',
        method: "GET",
        headers: { 'Content-Type': 'application/json' }
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
        $scope.status = status;
    });
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}