
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
ZestorkApp.controller('homeTemplate', function ($scope) {

    $scope.dataLoaded={
        status:true
    }

    $scope.sliderImageList = [
          { ImageAlt: "slider1", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider1.jpg" },
          { ImageAlt: "slider2", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider2.jpg" },
          { ImageAlt: "slider3", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider3.jpg" },
          { ImageAlt: "slider4", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider4.jpg" },          
        ];

    $scope.gridIconDataList = [
          { ImageLink: "../../Resource/templates/beforeLogin/web/about.html", 
            ImageIconUrl: "../../Resource/templates/beforeLogin/web/images/g1.png",
            Headline: "Success Stories",
            Content: "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
            BottomButtonUrl:"../../Resource/templates/beforeLogin/web/about.html",
            BottomButtonText:"Read More"
             },
          { ImageLink: "../../Resource/templates/beforeLogin/web/services.html", 
            ImageIconUrl: "../../Resource/templates/beforeLogin/web/images/g2.png",
            Headline: "Custom Design",
            Content: "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
            BottomButtonUrl:"../../Resource/templates/beforeLogin/web/about.html",
            BottomButtonText:"Read More"
             },
          { ImageLink: "../../Resource/templates/beforeLogin/web/services.html", 
            ImageIconUrl: "../../Resource/templates/beforeLogin/web/images/g3.png",
            Headline: "Customer Support",
            Content: "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
            BottomButtonUrl:"../../Resource/templates/beforeLogin/web/services.html",
            BottomButtonText:"Read More"
             }
        ];

        $scope.bottomContent = {

            leftColumnHeading : "Our Vision",
            leftColumnContent : "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."+
						"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
            leftColumnLink : "moreinfo.html",

            middleColumnHeading : "What we do",
            middleColumnContentList : [
              { Content: "Lorem ipsum dolor sit amet qui officia" },
              { Content: "Duis aute irure dolor in culpa qui" },
              { Content: "Duis aute irure dolor in culpa qui2" },
              { Content: "Duis aute irure dolor in culpa qui3" },
              { Content: "Sunt in culpa qui officia vel illum qui"},
              { Content: "Sunt in culpa qui officia vel illum qui2" },
              { Content: "vel illum qui dolorem eum wise man therefore" },
              { Content: "vel illum qui dolorem eum wise man therefore" }
        ],
            rightColumnHeading :"Our Stafff",
            rightColumnImageList: [
              { imgUrl: "../../Resource/templates/beforeLogin/web/images/management-1.jpg" },
              { imgUrl: "../../Resource/templates/beforeLogin/web/images/management-2.jpg" },
              { imgUrl: "../../Resource/templates/beforeLogin/web/images/management-3.jpg" },
              { imgUrl: "../../Resource/templates/beforeLogin/web/images/management-4.jpg" },
              { imgUrl: "../../Resource/templates/beforeLogin/web/images/management-5.jpg"},
              { imgUrl: "../../Resource/templates/beforeLogin/web/images/management-6.jpg" },              
        ],
            rightColumnLink:"viewall.html"
            
        //copyRightInfo: "<p>Company Name © All Rights Reseverd | Design by  <a href=\"http://zestork.com\">W3Layouts</a></p>"        
    };

});
ZestorkApp.controller('serviceTemplate', function ($scope) {

    $scope.sampleData = {
        imageurl: "../../Resource/templates/beforeLogin/web/images/service-6.png",
        header: "My header",
        imageurl2: "../../Resource/templates/beforeLogin/web/images/service-2.png",
        header2: "D.G."
    };

});