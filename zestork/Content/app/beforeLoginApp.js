
var ZestorkApp = angular.module('ZestorkApp', []);

ZestorkApp.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/beforeLogin/contentView/home.html" }).
//                   when("/profile", { templateUrl: "partials/profile.html" }).
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
          { tabName: "Home", tabUrl: "../../Resource/templates/beforeLogin/web/index.html" },
          { tabName: "About", tabUrl: "../../Resource/templates/beforeLogin/web/about.html" },
          { tabName: "Services", tabUrl: "../../Resource/templates/beforeLogin/web/services.html" },
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

ZestorkApp.controller('homeTemplate', function ($scope) {

    $scope.sliderImageList = [
          { ImageAlt: "slider1", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider1.jpg" },
          { ImageAlt: "slider2", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider2.jpg" },
          { ImageAlt: "slider3", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider3.jpg" },
          { ImageAlt: "slider4", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider4.jpg" },          
        ];


});