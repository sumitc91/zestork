

ZestorkApp.controller('signUpUserController', function ($scope, $http) {

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

        $http({
            url: '/Account/CreateAccount',
            method: "POST",
            data: CreateAccountRequest,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here 
            console.log(data);
        }).error(function (data, status, headers, config) {
            $scope.status = status;
        });

    }

});



