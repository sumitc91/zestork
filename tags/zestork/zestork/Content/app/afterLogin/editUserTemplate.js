//getting user info..
ZestorkAppAfterLogin.controller('editUserDetailsController', function ($scope, $http, $rootScope, CookieUtil) {
    //alert("inside edit user controller");
    //CookieUtil.storeAuthCookie();
    $scope.skillTags = ['php', 'laravel', 'java', 'testing'];
    if (CookieUtil.CookieValue() != null) {
        $rootScope.Authentication = CookieUtil.CookieValue();
    }
    else {
        alert('authentication cookie is null');
    }
    $.blockUI({ message: '<h1><img src="../../Content/third-party/bootstrap-modal-master/img/ajax-loader.gif" /> Profile Loading...</h1>' });
    $rootScope.Authentication = CookieUtil.CookieValue();

    var headers = { 'Content-Type': 'application/json',
        'Authorization': $rootScope.Authentication
    };

    $http({
        url: '/Auth/details',
        method: "GET",
        //headers: { 'Content-Type': 'application/json' }
        headers: headers
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        $.unblockUI();
        if (data != null) {
            $scope.details = data;
        }
        else {
            alert("data is null for this user");
        }
        //console.log(data);
    }).error(function (data, status, headers, config) {
        $.unblockUI();
        alert('Internal Server Error Occured !!');
    });

    $scope.addTagInputButtonClick = function () {
        if ($scope.skillTags.some(function (skill) { return skill === $scope.newTagModel })) {
            alert("already exists");
        }
        else {
            $scope.skillTags.push($scope.newTagModel);
            $scope.newTagModel = "";
        }
        
    }

    $scope.RemoveTagInputButtonClick = function (index) {
        //alert('inside remove tag');
        //alert(index);
        $scope.skillTags.splice(index, 1);
        //$scope.skillTags.push($scope.newTagModel);        
    }
});