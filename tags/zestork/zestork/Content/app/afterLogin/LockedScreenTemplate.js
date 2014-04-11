ZestorkAppAfterLogin.controller('LockedUserPageController', function ($scope, $rootScope, $http) {
    //afterLoginServices.initPageTheme();
    //alert("locked screen "+ $rootScope.GlobalPageThemeColor = );
    //$rootScope.pageThemeColor = afterLoginServices.pageThemeColor();

    $scope.forgetLockedPassword = function () {
        //alert("clicked");

        $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Resetting your Password...</h1>' });
        $http({
            url: '/Locked/forgetPassword/' + $('#LockedScreenUsername').val() + '?guidSession=' + $('#LockedScreenGuid').val(),
            method: "GET"
            //headers: { 'Content-Type': 'application/json' }           
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here                               
            if (data == "200") {
                $.unblockUI();
                alert("Email Reset information sent the email id. check email for further information.");
                window.location.href = "/";
            }
            else if (data == "210") {
                $.unblockUI();
                alert("your account it unlocked. please relogin using facebook to your account.");
                window.location.href = "/";
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
