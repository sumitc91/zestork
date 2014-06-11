//getting user info..
ZestorkAppClientView.controller('homeController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //$scope.openChangePasswordModal = $routeParams.openChangePasswordModal;
    $rootScope.Authentication = CookieUtil.CookieValue();    
    var headers = { 'Content-Type': 'application/json',
        'Authorization': $rootScope.Authentication
    };
    $http({
        url: '/Client/CheckIfUserNewPasswordIsSet',
        method: "GET",
        //headers: { 'Content-Type': 'application/json' }
        headers: headers
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here        
        if (data == "false" && $rootScope.firstTimeUserLoginViaSocialLinkPopUpOpened == false) {
            $('#ClientfirstTimeUserLoginViaSocialLinkChangePasswordPopUp').click(); 
        }
        else {
            
        }
        //console.log(data);
    }).error(function (data, status, headers, config) {       
        //alert('Internal Server Error Occured !!');
    });
    
    $scope.ClientfirstTimeUserLoginViaSocialLinkChangePasswordPopUpTemplate = '../../Resource/templates/afterLogin/contentView/index/firstTimeLoginViaSocialLinkePopUpModalChangePassword.html';

});
