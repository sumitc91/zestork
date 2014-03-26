

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

        //        <option selected="">libnotify</option>
        //        <option>bigbox</option>
        //        <option>boldlight</option>
        //        <option>jackedup</option>
        //        <option>original</option>
        //        <option>flatty</option>

        humane.timeout = 30000;
        humane.baseCls = "humane-boldlight";
        humane.log("Please wait While Creating your Account....");

        $http({
            url: '/Account/CreateAccount',
            method: "POST",
            data: CreateAccountRequest,
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            humane.remove(function () { });
            if (data.code == "200") {
                humane.timeout = 2000;
                humane.baseCls = "humane-bigbox";
                humane.log("Account Successfully Created.");
            }
            else if (data.code == "402") {
                humane.timeout = 2000;
                humane.baseCls = "humane-bigbox";
                humane.log($scope.userName + "  Already Exists.");
            }            
            console.log(data);
        }).error(function (data, status, headers, config) {
            $scope.status = status;
        });

    }

    NProgress.done();
});



