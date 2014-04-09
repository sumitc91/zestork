ZestorkApp.controller('signInController', function ($scope,$http) {
    $scope.data = "this is my testing data sign in page";
    $scope.signInUsingFacebook = function () {
        alert("working");
    }
    $scope.test = function () {
        alert('1');
    };
});

ZestorkApp.controller('facebookLoginController', function ($scope,$http) {
    $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Loggin in via Facebook..</h1>' });
    $('.closeModalBox').click();
    $http({
        url: '/Account/Login/facebook',
        method: "GET",
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        $.unblockUI();
        if (data.User == null) {
            window.location.href = data.ReturnUrl;
        }
        else {
            alert("successfully Logged in using facebook");
            console.log(data);
        }
        console.log(data);
    }).error(function (data, status, headers, config) {
        $scope.status = status;
    });
});

ZestorkApp.controller('googleLoginController', function ($scope, $http) {
    $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Loggin in via google..</h1>' });
    $('.closeModalBox').click();
    $http({
        url: '/Account/Login/google',
        method: "GET",
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        $.unblockUI();
        if (data.User == null) {
            window.location.href = data.ReturnUrl;
        }
        else {
            alert("successfully Logged in using google");
            console.log(data);
        }
        console.log(data);
    }).error(function (data, status, headers, config) {
        $scope.status = status;
    });
});

ZestorkApp.controller('linkedinLoginController', function ($scope, $http) {
    $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Loggin in via linkedin..</h1>' });
    $('.closeModalBox').click();
    $http({
        url: '/Account/Login/linkedin',
        method: "GET",
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        $.unblockUI();
        if (data.User == null) {
            window.location.href = data.ReturnUrl;
        }
        else {
            alert("successfully Logged in using linkedin");
            console.log(data);
        }
        console.log(data);
    }).error(function (data, status, headers, config) {
        $scope.status = status;
    });

});

ZestorkApp.controller('webLoginController', function ($scope) {
   
    $scope.submitLogintDataRequest = function () {

     
        alert("HI.........................");
        
    }

});

ZestorkApp.controller('LoginViewController', function ($scope, $routeParams) {

    $scope.showMssg = false;
    if ($routeParams.code == "403") {
        $scope.showMssg = true;
        $scope.mssg = "User Not Validated !";
        $scope.content = " Click on the Activation code sent to you on your mail. please check your inbox/spam.";
    }
    else if ($routeParams.code == "401") {
        $scope.showMssg = true;
        $scope.mssg = "Unauthenticated !";
        $scope.content = "either username or password is wrong ! try again.";
    }

    $scope.test = "testing";
    
});