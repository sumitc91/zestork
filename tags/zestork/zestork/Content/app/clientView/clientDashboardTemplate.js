//getting user info..
ZestorkAppClientView.controller('clientDashboardController', function ($scope, $http, $rootScope, CookieUtil) {
    //alert("inside edit user controller");
    //CookieUtil.storeAuthCookie();    
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
    }).success(function (ResponseData, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        $.unblockUI();
        var data = ResponseData.details;
        //alert("after Login APP " +ResponseData.Autherized);
        //if (ResponseData.Autherized == false)
        //    window.location.href = "/Client";
        if (data != null) {
            $scope.details = data;
            var imageUrlSplitted = data.ImageUrl.split(".");
            var uploadedImageLinkMedium = imageUrlSplitted[0] + "." + imageUrlSplitted[1] + "." + imageUrlSplitted[2] + "m." + imageUrlSplitted[3];
            $scope.details.ImageUrl = uploadedImageLinkMedium;
        }
        else {
            alert("data is null for this user");
        }
        //console.log(data);
    }).error(function (data, status, headers, config) {
        $.unblockUI();
        alert('Internal Server Error Occured !!');
    });


    $scope.clientTaskList = [
    { liClass: "bookmarked", taskIconClass: "icon-ok", taskIconText: "Approve new users", taskDeleteLink: "../../Resource/templates/afterLogin/web/#/delete", taskMarkImportantLink: "../../Resource/templates/afterLogin/web/#/important" },
    { liClass: "bookmarked", taskIconClass: "icon-ok", taskIconText: "Approve new users", taskDeleteLink: "../../Resource/templates/afterLogin/web/#/delete", taskMarkImportantLink: "../../Resource/templates/afterLogin/web/#/important" },
    { liClass: "bookmarked", taskIconClass: "icon-ok", taskIconText: "Approve new users", taskDeleteLink: "../../Resource/templates/afterLogin/web/#/delete", taskMarkImportantLink: "../../Resource/templates/afterLogin/web/#/important" },
    { liClass: "bookmarked", taskIconClass: "icon-ok", taskIconText: "Approve new users", taskDeleteLink: "../../Resource/templates/afterLogin/web/#/delete", taskMarkImportantLink: "../../Resource/templates/afterLogin/web/#/important" },
    { liClass: "bookmarked", taskIconClass: "icon-ok", taskIconText: "Approve new users", taskDeleteLink: "../../Resource/templates/afterLogin/web/#/delete", taskMarkImportantLink: "../../Resource/templates/afterLogin/web/#/important" },
    { liClass: "bookmarked", taskIconClass: "icon-ok", taskIconText: "Approve new users", taskDeleteLink: "../../Resource/templates/afterLogin/web/#/delete", taskMarkImportantLink: "../../Resource/templates/afterLogin/web/#/important" }
    ];


    $scope.RemoveTagInputButtonClick = function (index) {

        $rootScope.Authentication = CookieUtil.CookieValue();
        var headers = { 'Content-Type': 'application/json',
            'Authorization': $rootScope.Authentication
        };

        $http({
            url: '/Auth/deleteUsersTag/' + $scope.details.skillTags[index],
            method: "GET",
            headers: headers
        }).success(function (data, status, headers, config) {

            if (data == "200") {
                $scope.details.skillTags.splice(index, 1); //successfully removed.
            }
            else {
                alert("user type info data not available");
            }
            //console.log(data);
        }).error(function (data, status, headers, config) {
            alert('Internal Server Error Occured !!');
        });

    }
});
