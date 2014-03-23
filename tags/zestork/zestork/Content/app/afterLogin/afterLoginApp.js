
var ZestorkAppAfterLogin = angular.module('ZestorkAppAfterLogin', []);

ZestorkAppAfterLogin.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../Resource/templates/afterLogin/contentView/searchJob.html" }).
                   when("/services", { templateUrl: "../../Resource/templates/beforeLogin/contentView/services.html" }).
                   when("/about", { templateUrl: "../../Resource/templates/beforeLogin/contentView/about.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/signup", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signup.html" }).
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

});

ZestorkAppAfterLogin.controller('myController', function ($scope) {

    $scope.person = {
        name: "Controller Sumit Chourasia after login"
    };

});



