
ZestorkApp.controller('signUpUserController', function ($scope, $http) {

    NProgress.start();

    $scope.userName = "";
    $scope.emailAlreadyExists = false;
    $scope.firstName = "";
    $scope.lastName = "";
    $scope.password = "";
    $scope.confirmPassword = "";
    $scope.source = "web";
    $scope.type = "user";
    $scope.showEmailMessage = false;  

    $scope.checkEmailAvailableOnFocusOut = function () {
        var checkUserExistsRequestData = {
            userName: $scope.userName
        }

        if (isValidEmailAddress($scope.userName)) {
            $http({
                url: '/Account/checkUsernameExists',
                method: "POST",
                data: checkUserExistsRequestData,
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here

                if (data.code == "200") {
                    $scope.emailAlreadyExists = false;
                    $scope.showEmailMessage = true;
                    //$scope.emailAlreadyExists = "<span style='color:green'>Username Available</span>";
                    //alert("Account Successfully Created.");
                }
                else if (data.code == "402") {
                    $scope.emailAlreadyExists = true;
                    $scope.showEmailMessage = true;
                    //$scope.emailAlreadyExists = "<span style='color:red'>Username already taken</span>";
                    //alert("Account already exists.");
                }
                console.log(data);
            }).error(function (data, status, headers, config) {
                $scope.status = status;
            });
        }
        else {
            $scope.showEmailMessage = false;

            //alert("invalid email");
        }



    }
    $scope.checkUserExists = function () {
        var checkUserExistsRequestData = {
            userName: $scope.userName
        }
        if (isValidEmailAddress($scope.userName)) {
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
        
        if ($scope.confirmPassword == $scope.password && isValidEmailAddress($scope.userName)) {
            if ($scope.tncModel) {
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
            else {
                alert("Agree with terms and conditions to continue");
            }
            
        }
        else {
            alert("some field have invalid entries.");
            //password didn't match
        }
    }




    NProgress.done();
});


function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(emailAddress);
};

function isValidFormField(emailAddress) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(emailAddress);
};