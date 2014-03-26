

ZestorkApp.controller('signUpUserController', function ($scope, $http) {

    NProgress.start();

    $scope.userName = "";
    $scope.firstName = "";
    $scope.lastName = "";
    $scope.password = "";
    $scope.confirmPassword = "";
    $scope.source = "web";
    $scope.type = "user";

    $scope.submitCreateAccountData = function () {

        var CreateAccountRequest = {
            userName: $scope.userName,
            firstName: $scope.firstName,
            lastName: $scope.lastName,
            password: $scope.confirmPassword,
            source: $scope.source,
            type: $scope.type
        }

        $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Creating your account...</h1>' }); 

        $http({
            url: '/Account/CreateAccount',
            method: "POST",
            data: CreateAccountRequest,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            $.unblockUI();
            if (data.code == "200") {
                
                alert("Account Successfully Created.");
            }
            else if (data.code == "402") {
               
               alert("Account already exists.");
            }            
            console.log(data);
        }).error(function (data, status, headers, config) {
            $scope.status = status;
        });

    }

    NProgress.done();
});



