//getting user info..
ZestorkAppClientView.controller('createProductSurveyController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //alert("create product controller");    
    var editableInstructions = "";
    var totalEditableInstruction = 1;
    $scope.editableInstructionsList = [{ Number: 1, Text: "Instruction 1"}];

    $.each($scope.editableInstructionsList, function () {
        editableInstructions += "<li>";
        editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' onClick='alert(" + this.Number + ")'><i class='icon-remove'></i></a>";
        editableInstructions += "</li>";
    });

    $('#editableInstructionsListID').html(editableInstructions);

    $scope.addEditableInstructions = function () {        
        totalEditableInstruction = totalEditableInstruction + 1;
        var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: $('#AddInstructionsTextArea').val() };
        $scope.editableInstructionsList.push(editableInstructionDataToBeAdded);
        editableInstructions = "";
        $.each($scope.editableInstructionsList, function () {
            editableInstructions += "<li>";
            editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' onClick='alert(" + this.Number + ")'><i class='icon-remove'></i></a>";
            editableInstructions += "</li>";
        });
        $('#editableInstructionsListID').html(editableInstructions);
        $('#addInstructionCloseButton').click();
    }

    $.deleteEditableInstructions = function(index) {
        alert('delte editable instructions');
    }
});

