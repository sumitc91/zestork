ZestorkAppAfterLogin.controller('LockedUserPageController', function ($scope, $rootScope,$http) {
    //afterLoginServices.initPageTheme();
    //alert("locked screen "+ $rootScope.GlobalPageThemeColor = );
    //$rootScope.pageThemeColor = afterLoginServices.pageThemeColor();

    $scope.forgetLockedPassword = function () {
        //alert("clicked");

        $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Resetting your Password...</h1>' });
        $http({
            url: '/Account/forgetPassword/' + $('#LockedScreenUsername').val(),
            method: "GET"
            //headers: { 'Content-Type': 'application/json' }           
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here                               
            if (data == "200") {
                $.unblockUI();
                alert("Your Password resetted successfully. Check your Mail for details.");
            }
            else if (data == "404") {
                alert("This user is not registered yet !!!");
                $.unblockUI();
            }

            //console.log(data);
        }).error(function (data, status, headers, config) {
            $.unblockUI();
            alert('Internal Server Error Occured !!');
        });

    }

});
