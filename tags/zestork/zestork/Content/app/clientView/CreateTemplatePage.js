//getting user info..
ZestorkAppClientView.controller('createTemplateController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //alert("create product controller");    
    var editableInstructions = "";
    var totalEditableInstruction = 0;
    $scope.jobTemplate = [{ type: "AddInstructions",visible:false,buttonText:"Add Instructions", editableInstructionsList: { Number: totalEditableInstruction, Text: "Instruction 1" } }];
    
    $.each($scope.jobTemplate[0].editableInstructionsList, function () {
        editableInstructions += "<li>";
        editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
        editableInstructions += "</li>";
    });

    $('#editableInstructionsListID').html(editableInstructions);
    initAddInstructionClass();

    $scope.addEditableInstructions = function () {
        totalEditableInstruction = totalEditableInstruction + 1;
        var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: $('#AddInstructionsTextArea').val() };
        $scope.jobTemplate[0].editableInstructionsList.push(editableInstructionDataToBeAdded);
        refreshInstructionList();
        $('#AddInstructionsTextArea').val('');
    }

    $scope.addSingleAnswer = function () {
        alert("addSingleAnswer");
    }

    $scope.addInstructionsRow = function () {
        if ($scope.jobTemplate[0].visible == true) {
            $scope.jobTemplate[0].buttonText = "Add Instructions";            
            $scope.jobTemplate[0].visible = false;
        } else {
            $scope.jobTemplate[0].visible = true;
            $scope.jobTemplate[0].buttonText = "Remove Instructions";
        }
    }

    function initAddInstructionClass() {
        $('.addInstructionClass').click(function () {
            var i;
            for (i = 0; i < $scope.jobTemplate[0].editableInstructionsList.length; i++) {
                if ($scope.jobTemplate[0].editableInstructionsList[i].Number == this.id) {
                    break;
                }
            }
            $scope.jobTemplate[0].editableInstructionsList.splice(i, 1);
            refreshInstructionList();
        });
    }

    function refreshInstructionList() {
        editableInstructions = "";
        $.each($scope.jobTemplate[0].editableInstructionsList, function () {
            editableInstructions += "<li>";
            editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
            editableInstructions += "</li>";
        });
        $('#editableInstructionsListID').html(editableInstructions);
        initAddInstructionClass();
        $('#addInstructionCloseButton').click();
    }

});


