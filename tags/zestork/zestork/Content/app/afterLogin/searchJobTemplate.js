//getting user info..
ZestorkAppAfterLogin.controller('searchJobDetailsController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //$scope.openChangePasswordModal = $routeParams.openChangePasswordModal;
    $rootScope.Authentication = CookieUtil.CookieValue();    
    var headers = { 'Content-Type': 'application/json',
        'Authorization': $rootScope.Authentication
    };
    $http({
        url: '/Auth/CheckIfUserNewPasswordIsSet',
        method: "GET",
        //headers: { 'Content-Type': 'application/json' }
        headers: headers
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here        
        if (data == "false" && $rootScope.firstTimeUserLoginViaSocialLinkPopUpOpened == false) {
            $('#firstTimeUserLoginViaSocialLinkChangePasswordPopUp').click(); 
        }
        else {
            
        }
        //console.log(data);
    }).error(function (data, status, headers, config) {       
        //alert('Internal Server Error Occured !!');
    });
    
    $scope.firstTimeUserLoginViaSocialLinkChangePasswordPopUpTemplate = '../../Resource/templates/afterLogin/contentView/index/firstTimeLoginViaSocialLinkePopUpModalChangePassword.html';

    $scope.jobDetailList = [
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" },
   { jobImageLink: "http://www.placehold.it/80", heading: "Article Writing Job", jobLink: "www.loremasdasdd.com", content: "Hi HEllo" }
    ];

});
