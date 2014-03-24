
var ZestorkApp = angular.module('ZestorkApp', []);
ZestorkApp.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/beforeLogin/contentView/home.html" }).
                   when("/services", { templateUrl: "../../Resource/templates/beforeLogin/contentView/services.html" }).
                   when("/about", { templateUrl: "../../Resource/templates/beforeLogin/contentView/about.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/signup", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signup.html" }).
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

});

ZestorkApp.controller('myController', function ($scope) {

    $scope.person = {
        name: "Controller Sumit Chourasia"
    };

});


ZestorkApp.controller('loginControllerPlaceHolders', function ($scope) {

    
    $scope.userName= "Username";
    $scope.password= "password";
    $scope.userNamePlaceholder= "Username";
    

});

ZestorkApp.controller('beforeLoginHeaderController', function ($scope) {


    $scope.beforeLoginHeaderInfo = {

        logoUrl: "#/",
        logoImage: "../../Resource/templates/afterLogin/web/img/logo.png"
    };

    $scope.showBeforeLoginMenuTab = false;
    $scope.showLoginButton = true;
    $scope.showSignUpButton = true;
    $scope.showLabelAlreadyRegistered = false;
    $scope.signUpClick = function () {
        $scope.showSignUpButton = false;
        $scope.showLabelAlreadyRegistered = true;
    };
    $scope.beforeLoginLogoClick = function () {
        $scope.showSignUpButton = true;
        $scope.showLabelAlreadyRegistered = false;
    };
    $scope.beforeLoginMenuTab = [
          { tabName: "Home", tabUrl: "#/" },
          { tabName: "About", tabUrl: "#/about" },
          { tabName: "Services", tabUrl: "#/services" },
          { tabName: "Clients", tabUrl: "#/Clients" },
          { tabName: "Contact", tabUrl: "#/Contact" }
        ];

    $scope.beforeLoginFooterInfo = {

        locationIconImage: "../../Resource/templates/beforeLogin/web/images/location.png",
        locationIconAltImage: "22-56-2-9 Sit Amet,USA",
        locationAddress: "22-56-2-9 Sit Amet,USA",
        phoneIconImage: "../../Resource/templates/beforeLogin/web/images/phone.png",
        phoneIconAltImage: "(000)1234-5678",
        phoneIconTex: "Call Us Now",
        phoneIconNumber: "(000)1234-5678",
    };

    $scope.beforeLoginFooterCopyRightInfo = {

        companyName : "Zestork",
        designedByText : "Zestork",
        designedByUrl : "http://zestork.com"
        //copyRightInfo: "<p>Company Name © All Rights Reseverd | Design by  <a href=\"http://zestork.com\">W3Layouts</a></p>"
        
    };
    
});

