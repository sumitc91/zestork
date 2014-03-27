ZestorkApp.controller('validateAccountTemplate', function ($scope,$http,$routeParams) {
    NProgress.start();
    $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Validating your account...</h1>' });
    var ValidateAccountRequest = {
        userName: $routeParams.userName,
        guid: $routeParams.guid     
    };
    $http({
        url: '/Account/ValidateAccount',
        method: "POST",
        data: ValidateAccountRequest,
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        $.unblockUI();
        if (data.code == "200") {
            $.blockUI({ message: '<h1>Account Successfully Validated.' });
            setTimeout($.unblockUI, 2000);
            //alert("Account Successfully Created.");
        }
        else if (data.code == "402") {
            $.blockUI({ message: '<h1>Link Seems to be Expired' });
            setTimeout($.unblockUI, 2000);
            //alert("Account already exists.");
        }
        console.log(data);
    }).error(function (data, status, headers, config) {
        $scope.status = status;
    });
    NProgress.done();
});