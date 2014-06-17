//getting user info..
ZestorkAppClientView.controller('createProductSurveyController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //alert("create product controller");    
    var editableInstructions = "";
    var totalEditableInstruction = 0;
    $scope.editableInstructionsList = [{ Number: totalEditableInstruction, Text: "Instruction 1"}];

    $.each($scope.editableInstructionsList, function () {
        editableInstructions += "<li>";
        editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='"+this.Number+"'><i class='icon-remove'></i></a>";
        editableInstructions += "</li>";
    });

    $('#editableInstructionsListID').html(editableInstructions);
    initAddInstructionClass();

    $scope.addEditableInstructions = function () {        
        totalEditableInstruction = totalEditableInstruction + 1;
        var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: $('#AddInstructionsTextArea').val() };
        $scope.editableInstructionsList.push(editableInstructionDataToBeAdded);
        refreshInstructionList();
    }

    function initAddInstructionClass() {
        $('.addInstructionClass').click(function () {            
            var i;
            for (i = 0; i < $scope.editableInstructionsList.length; i++) {
                if ($scope.editableInstructionsList[i].Number == this.id) {                    
                    break;
                }
            }
            $scope.editableInstructionsList.splice(i, 1);
            refreshInstructionList();
        });
    }

    function refreshInstructionList() {
        editableInstructions = "";
        $.each($scope.editableInstructionsList, function () {
            editableInstructions += "<li>";
            editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
            editableInstructions += "</li>";
        });
        $('#editableInstructionsListID').html(editableInstructions);
        initAddInstructionClass();
        $('#addInstructionCloseButton').click();        
    }
    
});


