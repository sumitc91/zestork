//getting user info..
ZestorkApp.controller('forgetPasswordController', function ($scope, $http, $rootScope, CookieUtil) {

    $('.closeModalBox').click();

    $scope.submitForgetPasswordUsername = function () {
        //alert("clicked");

        $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Resetting your Password...</h1>' });
        $http({
            url: '/Account/forgetPassword/' + $scope.forgetPasswordUsername,
            method: "GET"
            //headers: { 'Content-Type': 'application/json' }           
        }).success(function (data, status, headers, config) {
                                
            $.unblockUI();
            alert("Your Password resetted successfully. Check your Mail for details.");
            //console.log(data);
        }).error(function (data, status, headers, config) {
            $.unblockUI();
            alert('Internal Server Error Occured !!');
        });

    }

});