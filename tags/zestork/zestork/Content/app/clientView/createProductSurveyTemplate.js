//getting user info..
ZestorkAppClientView.controller('createProductSurveyController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //alert("create product controller");
    var editableInstructions = "";
    var totalEditableInstruction = 1;
    editableInstructions += "<li>";
    editableInstructions += "<a data-original-title='Enter Instruction #1' data-placeholder='Required' data-placement='right' data-pk='1' data-type='text'  href='#' id='clientEditableInstruction1' class='editable editable-click editable-empty editableInstruction'>Instructions 1</a>";
    editableInstructions += "</li>";
    //    editableInstructions += "<li>";
    //    editableInstructions += "<a data-original-title='Enter Instruction #1' data-placeholder='Required' data-placement='right' data-pk='1' data-type='text'  href='#' id='clientEditableInstruction2' class='editable editable-click editable-empty editableInstruction'>Instructions 2</a>";
    //    editableInstructions += "</li>";
    //    editableInstructions += "<li>";
    //    editableInstructions += "<a data-original-title='Enter Instruction #1' data-placeholder='Required' data-placement='right' data-pk='1' data-type='text'  href='#' id='clientEditableInstruction3' class='editable editable-click editable-empty editableInstruction'>Instructions 3</a>";
    //    editableInstructions += "</li>";

    $('#editableInstructionsListID').html(editableInstructions);

    //editables 
    $('.editableInstruction').editable({
        validate: function (value) {
            if ($.trim(value) == '') return 'This field is required';
        }
    });

    $scope.addEditableInstructions = function (index) {    
        totalEditableInstruction++;
        editableInstructions += "<li>";
        editableInstructions += "<a data-original-title='Enter Instruction #1' data-placeholder='Required' data-placement='right' data-pk='1' data-type='text'  href='#' id='clientEditableInstruction" + totalEditableInstruction + "' class='editable editable-click editable-empty editableInstruction'>Instructions " + totalEditableInstruction + "</a>";
        editableInstructions += "</li>";
        $('#editableInstructionsListID').html(editableInstructions);

        //editables 
        $('.editableInstruction').editable({
            validate: function (value) {
                if ($.trim(value) == '') return 'This field is required';
            }
        });
    }
});
