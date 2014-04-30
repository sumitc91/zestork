ZestorkApp.controller('homeTemplate', function ($scope) {

    NProgress.start();
    $scope.dataLoaded = {
        status: true
    }

    $scope.showImageSlider = false;

    $scope.sliderImageList = [
          { ImageAlt: "slider1", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider1.jpg" },
          { ImageAlt: "slider2", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider2.jpg" },
          { ImageAlt: "slider3", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider3.jpg" },
          { ImageAlt: "slider4", ImageUrl: "../../Resource/templates/beforeLogin/web/images/slider4.jpg" },
        ];

    $scope.gridIconDataList = [          
          {
              ImageLink: "#/signup/client",
              ImageIconUrl: "../../Resource/templates/beforeLogin/web/images/ClientPresentation2.gif",
              Headline: "I need it done",
              Content: "Want to hire? Post a job!",
              BottomButtonUrl: "#/signup/client",
              BottomButtonText: "Read More"
          },
          { ImageLink: "#/signup/user",
          ImageIconUrl: "../../Resource/templates/beforeLogin/web/images/UserPresentation2.gif",
              Headline: "I will do it",
              Content: "Want to work? Sign up!",
              BottomButtonUrl: "#/signup/user",
              BottomButtonText: "Read More"
          }
//          ,
//          { ImageLink: "../../Resource/templates/beforeLogin/web/services.html",
//              ImageIconUrl: "../../Resource/templates/beforeLogin/web/images/g3.png",
//              Headline: "Customer Support",
//              Content: "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
//              BottomButtonUrl: "../../Resource/templates/beforeLogin/web/services.html",
//              BottomButtonText: "Read More"
//          }
        ];

    NProgress.done();
});