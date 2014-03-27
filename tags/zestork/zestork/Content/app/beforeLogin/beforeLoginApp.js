
var ZestorkApp = angular.module('ZestorkApp', []);

ZestorkApp.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/beforeLogin/contentView/home.html" }).
                   when("/signup/user", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signupuser.html" }).
                   when("/validate/:userName/:guid", { templateUrl: "../../Resource/templates/beforeLogin/contentView/validateAccount.html" }).
                   when("/about", { templateUrl: "../../Resource/templates/beforeLogin/contentView/about.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/signup", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signup.html" }).
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

});

ZestorkApp.run(function ($rootScope, $location) { //Insert in the function definition the dependencies you need.
    //Do your $on in here, like this:
    $rootScope.showSignUpButton = true; //global variable
    $rootScope.showLabelAlreadyRegistered = false; //global variable

    $rootScope.$on("$locationChangeStart",function(event, next, current){
        //Do your things        
        //var path = $location.path();        
        var path = next.split('#');
        var contextPath = path[1]; 
        if(contextPath=="/signup" || contextPath == "/signup/user")
        {
            $rootScope.showSignUpButton = false;
            $rootScope.showLabelAlreadyRegistered = true;
        }
        else
        {
            $rootScope.showSignUpButton = true;
            $rootScope.showLabelAlreadyRegistered = false;
        }
        
    });
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

ZestorkApp.controller('beforeLoginHeaderController', function ($scope, $route, $routeParams, $location) {


    $scope.beforeLoginHeaderInfo = {

        logoUrl: "#/",
        logoImage: "../../Resource/templates/afterLogin/web/img/logo.png"
    };


    $scope.showBeforeLoginMenuTab = false;
    $scope.showLoginButton = true;   
    
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

