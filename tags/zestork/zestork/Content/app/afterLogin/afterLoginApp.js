
var ZestorkAppAfterLogin = angular.module('ZestorkAppAfterLogin', []);

ZestorkAppAfterLogin.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/afterLogin/contentView/searchJob.html" }).
                   when("/services", { templateUrl: "../../Resource/templates/beforeLogin/contentView/services.html" }).
                   when("/about", { templateUrl: "../../Resource/templates/beforeLogin/contentView/about.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/signup", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signup.html" }).
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

});

ZestorkAppAfterLogin.controller('masterPageController', function ($scope) {

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



