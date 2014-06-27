//getting user info..
ZestorkAppClientView.controller('createTemplateController', function ($scope, $http, $rootScope, $routeParams, CookieUtil) {
    //alert("create product controller");    
    var editableInstructions = "";
    var totalQuestionSingleAnswerHtmlData = "";
    var totalQuestionMultipleAnswerHtmlData = "";
    var totalEditableInstruction = 0;
    var totalSingleQuestionList = 0;
    var totalMultipleQuestionList = 0;
    $scope.jobTemplate = [{ type: "AddInstructions", visible: false, buttonText: "Add Instructions", editableInstructionsList: [{ Number: totalEditableInstruction, Text: "Instruction 1"}] },
        { type: "AddSingleQuestionsList", visible: false, buttonText: "Add Ques. (single Ans.)", singleQuestionsList: [{ Number: totalSingleQuestionList, Question: "What is your gender ?", Options: "Male1;Female2"}] },
        { type: "AddMultipleQuestionsList", visible: false, buttonText: "Add Ques. (Multiple Ans.)", multipleQuestionsList: [{ Number: totalMultipleQuestionList, Question: "What is your multiple gender ?", Options: "Malem1;Femalem2"}] }
    ];

    $.each($scope.jobTemplate[0].editableInstructionsList, function () {
        editableInstructions += "<li>";
        editableInstructions += this.Text + "&nbsp;&nbsp<a style='cursor:pointer' class='addInstructionClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
        editableInstructions += "</li>";
    });

    var quesCount = 1;
    $.each($scope.jobTemplate[1].singleQuestionsList, function () {

        totalQuestionSingleAnswerHtmlData += "<fieldset>";

        totalQuestionSingleAnswerHtmlData += "<label>";
        totalQuestionSingleAnswerHtmlData += "<b>" + quesCount + ". " + this.Question + "</b><a style='cursor:pointer' class='addQuestionSingleAnswerClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
        totalQuestionSingleAnswerHtmlData += "</label>";

        var singleQuestionsOptionList = this.Options.split(';');
        for (var j = 0; j < singleQuestionsOptionList.length; j++) {
            totalQuestionSingleAnswerHtmlData += "<div class='radio'>";
            totalQuestionSingleAnswerHtmlData += "<label>";
            totalQuestionSingleAnswerHtmlData += "<input type='radio' value='" + quesCount + "' name='" + quesCount + "'>" + singleQuestionsOptionList[j] + "";
            totalQuestionSingleAnswerHtmlData += "</label>";
            totalQuestionSingleAnswerHtmlData += "</div>";
        }

        totalQuestionSingleAnswerHtmlData += "</fieldset>";
        quesCount++;
    });

    quesCount = 1;
    $.each($scope.jobTemplate[2].multipleQuestionsList, function () {

        totalQuestionMultipleAnswerHtmlData += "<fieldset>";

        totalQuestionMultipleAnswerHtmlData += "<label>";
        totalQuestionMultipleAnswerHtmlData += "<b>" + quesCount + ". " + this.Question + "</b><a style='cursor:pointer' class='addQuestionMultipleAnswerClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
        totalQuestionMultipleAnswerHtmlData += "</label>";

        var multipleQuestionsOptionList = this.Options.split(';');
        for (var j = 0; j < multipleQuestionsOptionList.length; j++) {
            totalQuestionMultipleAnswerHtmlData += "<div class='radio'>";
            totalQuestionMultipleAnswerHtmlData += "<label>";
            totalQuestionMultipleAnswerHtmlData += "<input type='checkbox' value='" + quesCount + "' name='" + quesCount + "'>" + multipleQuestionsOptionList[j] + "";
            totalQuestionMultipleAnswerHtmlData += "</label>";
            totalQuestionMultipleAnswerHtmlData += "</div>";
        }

        totalQuestionMultipleAnswerHtmlData += "</fieldset>";
        quesCount++;
    });

    $('#editableInstructionsListID').html(editableInstructions);
    $('#addSingleAnswerQuestionID').html(totalQuestionSingleAnswerHtmlData);
    $('#addMultipleAnswerQuestionID').html(totalQuestionMultipleAnswerHtmlData);
    initAddInstructionClass();
    initAddQuestionSingleAnswerClass();
    initAddQuestionMultipleAnswerClass();

    $scope.addEditableInstructions = function () {
        totalEditableInstruction = totalEditableInstruction + 1;
        var editableInstructionDataToBeAdded = { Number: totalEditableInstruction, Text: $('#AddInstructionsTextArea').val() };
        $scope.jobTemplate[0].editableInstructionsList.push(editableInstructionDataToBeAdded);
        refreshInstructionList();
        //$('#AddInstructionsTextArea').val(''); // TODO: clearing the text area not working
    }

    // single questions..
    $scope.InsertSingleQuestionRow = function () {
        totalSingleQuestionList = totalSingleQuestionList + 1;
        var singleQuestionsList = { Number: totalSingleQuestionList, Question: $('#SingleQuestionTextBoxQuestionData').val(), Options: $('#SingleQuestionTextBoxAnswerData').val() };
        $scope.jobTemplate[1].singleQuestionsList.push(singleQuestionsList);
        refreshSingleQuestionsList();
    }

    // multiple questions..
    $scope.InsertMultipleQuestionRow = function () {
        totalMultipleQuestionList = totalMultipleQuestionList + 1;
        var multipleQuestionsList = { Number: totalMultipleQuestionList, Question: $('#MultipleQuestionTextBoxQuestionData').val(), Options: $('#MultipleQuestionTextBoxAnswerData').val() };
        $scope.jobTemplate[2].multipleQuestionsList.push(multipleQuestionsList);
        refreshMultipleQuestionsList();
    }
    $scope.addSingleAnswer = function () {
        if ($scope.jobTemplate[1].visible == true) {
            $scope.jobTemplate[1].buttonText = "Add Ques. (single Ans.)";
            $scope.jobTemplate[1].visible = false;
        } else {
            $scope.jobTemplate[1].visible = true;
            $scope.jobTemplate[1].buttonText = "Remove Ques. (single Ans.)";
        }
    }

    $scope.addMultipleAnswer = function () {
        if ($scope.jobTemplate[2].visible == true) {
            $scope.jobTemplate[2].buttonText = "Add Ques. (Multiple Ans.)";
            $scope.jobTemplate[2].visible = false;
        } else {
            $scope.jobTemplate[2].visible = true;
            $scope.jobTemplate[2].buttonText = "Remove Ques. (Multiple Ans.)";
        }
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

    function initAddQuestionSingleAnswerClass() {
        $('.addQuestionSingleAnswerClass').click(function () {
            var i;
            for (i = 0; i < $scope.jobTemplate[1].singleQuestionsList.length; i++) {
                if ($scope.jobTemplate[1].singleQuestionsList[i].Number == this.id) {
                    break;
                }
            }
            $scope.jobTemplate[1].singleQuestionsList.splice(i, 1);
            refreshSingleQuestionsList();
        });
    }

    function initAddQuestionMultipleAnswerClass() {
        $('.addQuestionMultipleAnswerClass').click(function () {
            var i;
            for (i = 0; i < $scope.jobTemplate[2].multipleQuestionsList.length; i++) {
                if ($scope.jobTemplate[2].multipleQuestionsList[i].Number == this.id) {
                    break;
                }
            }
            $scope.jobTemplate[2].multipleQuestionsList.splice(i, 1);
            refreshMultipleQuestionsList();
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

    function refreshSingleQuestionsList() {
        totalQuestionSingleAnswerHtmlData = "";
        var innerQuesCount = 1;
        $.each($scope.jobTemplate[1].singleQuestionsList, function () {
            totalQuestionSingleAnswerHtmlData += "<fieldset>";

            totalQuestionSingleAnswerHtmlData += "<label>";
            totalQuestionSingleAnswerHtmlData += "<b>" + innerQuesCount + ". " + this.Question + "</b> <a style='cursor:pointer' class='addQuestionSingleAnswerClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
            totalQuestionSingleAnswerHtmlData += "</label>";

            var singleQuestionsOptionList = this.Options.split(';');
            for (var j = 0; j < singleQuestionsOptionList.length; j++) {
                totalQuestionSingleAnswerHtmlData += "<div class='radio'>";
                totalQuestionSingleAnswerHtmlData += "<label>";
                totalQuestionSingleAnswerHtmlData += "<input type='radio' value='" + innerQuesCount + "' name='" + innerQuesCount + "'>" + singleQuestionsOptionList[j] + "";
                totalQuestionSingleAnswerHtmlData += "</label>";
                totalQuestionSingleAnswerHtmlData += "</div>";
            }

            totalQuestionSingleAnswerHtmlData += "</fieldset>";
            innerQuesCount++;
        });
        $('#addSingleAnswerQuestionID').html(totalQuestionSingleAnswerHtmlData);
        initAddQuestionSingleAnswerClass();
        $('#addQuestionSingleAnswerCloseButton').click();
    }

    function refreshMultipleQuestionsList() {
        totalQuestionMultipleAnswerHtmlData = "";
        var innerQuesCount = 1;
        $.each($scope.jobTemplate[2].multipleQuestionsList, function () {
            totalQuestionMultipleAnswerHtmlData += "<fieldset>";

            totalQuestionMultipleAnswerHtmlData += "<label>";
            totalQuestionMultipleAnswerHtmlData += "<b>" + innerQuesCount + ". " + this.Question + "</b> <a style='cursor:pointer' class='addQuestionMultipleAnswerClass' id='" + this.Number + "'><i class='icon-remove'></i></a>";
            totalQuestionMultipleAnswerHtmlData += "</label>";

            var multipleQuestionsOptionList = this.Options.split(';');
            for (var j = 0; j < multipleQuestionsOptionList.length; j++) {
                totalQuestionMultipleAnswerHtmlData += "<div class='radio'>";
                totalQuestionMultipleAnswerHtmlData += "<label>";
                totalQuestionMultipleAnswerHtmlData += "<input type='checkbox' value='" + innerQuesCount + "' name='" + innerQuesCount + "'>" + multipleQuestionsOptionList[j] + "";
                totalQuestionMultipleAnswerHtmlData += "</label>";
                totalQuestionMultipleAnswerHtmlData += "</div>";
            }

            totalQuestionMultipleAnswerHtmlData += "</fieldset>";
            innerQuesCount++;
        });
        $('#addMultipleAnswerQuestionID').html(totalQuestionMultipleAnswerHtmlData);
        initAddQuestionMultipleAnswerClass();
        $('#addQuestionMultipleAnswerCloseButton').click();
    }

});


