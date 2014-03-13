
var ZestorkApp = angular.module('ZestorkApp', []);

ZestorkApp.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/beforeLogin/contentView/home.html" }).
                   when("/services", { templateUrl: "../../Resource/templates/beforeLogin/contentView/services.html" }).
//                   when("/contact", { templateUrl: "partials/contact.html" }).
                   otherwise({ redirectTo: '/' });

});

ZestorkApp.controller('myController', function ($scope) {

    $scope.person = {
        name: "Controller Sumit Chourasia"
    };

});
ZestorkApp.controller('beforeLoginHeaderController', function ($scope) {


    $scope.beforeLoginHeaderInfo = {

        logoUrl: "../../Resource/templates/beforeLogin/web/index.html",
        logoImage: "../../Resource/templates/beforeLogin/web/images/logo.png"
    };

    $scope.beforeLoginMenuTab = [
          { tabName: "Home", tabUrl: "#/" },
          { tabName: "About", tabUrl: "../../Resource/templates/beforeLogin/web/about.html" },
          { tabName: "Services", tabUrl: "#/services" },
          { tabName: "Clients", tabUrl: "../../Resource/templates/beforeLogin/web/404.html" },
          { tabName: "Contact", tabUrl: "../../Resource/templates/beforeLogin/web/contact.html" }
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
        designedByText : "Sumit Chourasia",
        designedByUrl : "http://zestork.com"
        //copyRightInfo: "<p>Company Name © All Rights Reseverd | Design by  <a href=\"http://zestork.com\">W3Layouts</a></p>"
        
    };
    
});

