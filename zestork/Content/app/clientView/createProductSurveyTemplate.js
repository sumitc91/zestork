//getting user info..
ZestorkAppClientView.controller('createProductSurveyController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //alert("create product controller");    
    var editableInstructions = "";
    var totalEditableInstruction = 1;
    $scope.editableInstructionsList = [{ Number: 1, Text: "Instruction 1"}];

    $.each($scope.editableInstructionsList, function () {
        editableInstructions += "<li>";
        editableInstructions += "<a data-original-title='Enter Instruction #" + this.Number + "' data-placeholder='Required' data-placement='right' data-pk='1' data-type='text'  href='#' id='clientEditableInstruction" + this.Number + "' class='editable editable-click editable-empty editableInstruction'>" + this.Text + "</a>";
        editableInstructions += "</li>";
    });

    $('#editableInstructionsListID').html(editableInstructions);

    //editables 
    $('.editableInstruction').editable({
        validate: function (value) {
            if ($.trim(value) == '') return 'This field is required';
        }
    });

    $scope.addEditableInstructions = function (index) {
        totalEditableInstruction = totalEditableInstruction + 1;
        var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: "Instruction " + totalEditableInstruction };
        $scope.editableInstructionsList.push(editableInstructionDataToBeAdded);
        editableInstructions = "";
        $.each($scope.editableInstructionsList, function () {
            editableInstructions += "<li>";
            editableInstructions += "<a data-original-title='Enter Instruction #" + this.Number + "' data-placeholder='Required' data-placement='right' data-pk='1' data-type='text'  href='#' id='clientEditableInstruction" + this.Number + "' class='editable editable-click editable-empty editableInstruction'>" + this.Text + "</a>";
            editableInstructions += "</li>";
        });
        $('#editableInstructionsListID').html(editableInstructions);

        //editables 
        $('.editableInstruction').editable({
            validate: function (value) {
                if ($.trim(value) == '') return 'This field is required';
            }
        });
    }
});

