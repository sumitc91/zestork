ZestorkApp.controller('signUpTemplate', function ($scope) {
    NProgress.start();
    
    $scope.sampleData = {
        imageurl: "../../Resource/templates/beforeLogin/web/images/service-6.png",
        header: "My header",
        imageurl2: "../../Resource/templates/beforeLogin/web/images/service-2.png",
        header2: "D.G."
    };
    NProgress.done();
});