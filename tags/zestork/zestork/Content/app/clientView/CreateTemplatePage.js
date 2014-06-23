//getting user info..
ZestorkAppClientView.controller('createTemplateController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //alert("create product controller");    
    var editableInstructions = "";
    var totalQuestionHtmlData = "";
    var totalEditableInstruction = 0;
    var totalSingleQuestionList = 0;
    $scope.jobTemplate = [{ type: "AddInstructions",visible:false,buttonText:"Add Instructions", editableInstructionsList: [{ Number: totalEditableInstruction, Text: "Instruction 1" }] }];

    $scope.singleQuestionsList = [{Number: totalSingleQuestionList, Question:"What is your gender ?" , Options : "Male1;Female2" }];

    $.each($scope.jobTemplate[0].editableInstructionsList, function () {
        editableInstructions += "<li>";
        editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
        editableInstructions += "</li>";
    });

    $.each($scope.singleQuestionsList, function () {       
        
        totalQuestionHtmlData += "<fieldset>";

        totalQuestionHtmlData += "<label>";
        totalQuestionHtmlData += this.Number + ". "+ this.Question;
        totalQuestionHtmlData += "</label>";

        var singleQuestionsOptionList = this.Options.split(';');
        for (var j = 0; j < singleQuestionsOptionList.length;j++)
        {
            totalQuestionHtmlData += "<div class='radio'>";
            totalQuestionHtmlData += "<label>";
            totalQuestionHtmlData += "<input type='radio' value='" + singleQuestionsOptionList[j] + "' name='" + singleQuestionsOptionList[j] + "'>" + singleQuestionsOptionList[j] + "";
            totalQuestionHtmlData += "</label>";
            totalQuestionHtmlData += "</div>";
        }

        totalQuestionHtmlData += "</fieldset>";
    });

    $('#editableInstructionsListID').html(editableInstructions);
    $('#addSingleAnswerQuestionID').html(totalQuestionHtmlData);    
    initAddInstructionClass();

    $scope.addEditableInstructions = function () {
        totalEditableInstruction = totalEditableInstruction + 1;
        var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: $('#AddInstructionsTextArea').val() };
        $scope.jobTemplate[0].editableInstructionsList.push(editableInstructionDataToBeAdded);
        refreshInstructionList();
        //$('#AddInstructionsTextArea').val(''); // TODO: clearing the text area not working
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

    // single questions..
    $scope.InsertSingleQuestionRow = function () {
        alert("InsertSingleQuestionRow");
    }

});


