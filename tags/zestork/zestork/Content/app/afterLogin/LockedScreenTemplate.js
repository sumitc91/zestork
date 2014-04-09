ZestorkAppAfterLogin.controller('LockedUserPageController', function ($scope, $rootScope, afterLoginServices) {

    $rootScope.pageThemeColor = afterLoginServices.pageThemeColor();

});
