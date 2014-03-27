

ZestorkApp.controller('signUpUserController', function ($scope, $http) {

    NProgress.start();

    $scope.userName = "";
    $scope.emailAlreadyExists = "";
    $scope.firstName = "";
    $scope.lastName = "";
    $scope.password = "";
    $scope.confirmPassword = "";
    $scope.source = "web";
    $scope.type = "user";

    $scope.checkEmailAvailableOnFocusOut = function () {
        var checkUserExistsRequestData = {
            userName: $scope.userName
        }

        $http({
            url: '/Account/checkUsernameExists',
            method: "POST",
            data: checkUserExistsRequestData,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here

            if (data.code == "200") {
                $scope.emailAlreadyExists = "valid email id";
                //alert("Account Successfully Created.");
            }
            else if (data.code == "402") {
                $scope.emailAlreadyExists = "Email Id Already Exists";
                //alert("Account already exists.");
            }
            console.log(data);
        }).error(function (data, status, headers, config) {
            $scope.status = status;
        });
    }
    $scope.checkUserExists = function () {
        var checkUserExistsRequestData = {
            userName: $scope.userName
        }
        $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Checking username availability</h1>' });

        $http({
            url: '/Account/checkUsernameExists',
            method: "POST",
            data: checkUserExistsRequestData,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            $.unblockUI();
            if (data.code == "200") {
                $.blockUI({ message: '<h1>This is a new username' });
                setTimeout($.unblockUI, 2000);
                //alert("Account Successfully Created.");
            }
            else if (data.code == "402") {
                $.blockUI({ message: '<h1>username already exists' });
                setTimeout($.unblockUI, 2000);
                //alert("Account already exists.");
            }
            console.log(data);
        }).error(function (data, status, headers, config) {
            $scope.status = status;
        });
    }

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
                $.blockUI({ message: '<h1>Account Successfully Created.' });
                setTimeout($.unblockUI, 2000);
                //alert("Account Successfully Created.");
            }
            else if (data.code == "402") {
                $.blockUI({ message: '<h1>Account already exists.' });
                setTimeout($.unblockUI, 2000);
                //alert("Account already exists.");
            }
            console.log(data);
        }).error(function (data, status, headers, config) {
            $scope.status = status;
        });

    }

    NProgress.done();
});



