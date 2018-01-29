@ModelType GuidanceCounselling.ExamStudent
@Code
    ViewBag.Title = "Taking Exam " + Model.Exam.Name
End Code

<div class="container">
    <div class="row">
        <div class="col-md-6">
            <h4>@Model.Exam.Name <small style="color:#dddddd;">Exam ID: @Model.ExamId</small></h4>
        </div>
    </div>

    @Using Html.BeginForm("StudentExam", "Student", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.ExamStudentId, New With {.Value = Model.ExamStudentId})

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            
            @Code
                Dim cTFRank As Integer = 0
                Dim cTFList As Integer = 0
                Dim cEssay As Integer = 0
                Dim cOneToFive As Integer = 0

                Dim cq As Integer = 1
            End Code
            
            @For Each e In Model.Exam.ExamQuestionGroups
                Select Case e.QuestionGroup.ExamType.ToString()
                    Case "TFRank"
                        @<text>
                            <div class="exam-module-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <strong>@e.QuestionGroup.DisplayName</strong>
                                    </div>
                                </div>
                                @For Each q As QuestionTFRank In e.QuestionGroup.QuestionTFRanks
                                    @<text>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <select class="form-control" name="ExamStudentTFRank[@cTFRank].Answer">
                                                    <option value="True">Yes</option>
                                                    <option value="False">No</option>
                                                </select>
                                            </div>
                                            
                                            <input type="hidden" value="@q.QuestionTFRankId" name="ExamStudentTFRank[@cTFRank].QuestionTFRankId"/>
                                            <div class="col-md-10">
                                                <p><strong>@cq .</strong> @q.Question</p>
                                            </div>
                                        </div>
                                    </text>

                                    cq += 1
                                    cTFRank += 1
                                Next
                            </div>
                        </text>
                    Case "TFList"
                        @<text>
                            <div class="exam-module-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <strong>@e.QuestionGroup.DisplayName</strong>
                                    </div>
                                </div>
                                @For Each q As QuestionTFList In e.QuestionGroup.QuestionTFLists
                                    @<text>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <select class="form-control" name="ExamStudentTFList[@cTFList].Answer">
                                                    <option value="True">Yes</option>
                                                    <option value="False">No</option>
                                                </select>
                                            </div>
                                            
                                            <input type="hidden" value="@q.QuestionTFListId" name="ExamStudentTFList[@cTFList].QuestionTFListId"/>
                                            <div class="col-md-10">
                                                <p><strong>@cq .</strong> @q.Question</p>
                                            </div>
                                        </div>
                                    </text>

                                    cq += 1
                                    cTFList += 1
                                Next
                            </div>
                        </text>
                    Case "Essay"
                        @<text>
                            <div class="exam-module-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <strong>@e.QuestionGroup.DisplayName</strong>
                                    </div>
                                </div>
                                @For Each q As QuestionEssay In e.QuestionGroup.QuestionEssays
                                    @<text>
                                        <div class="row">
                                            <div class="col-md-8">
                                                <p><strong>@cq .</strong> @q.Question</p>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-8">
                                                <textarea name="ExamStudentEssay[@cEssay].Answer" class="form-control"></textarea>
                                                <input type="hidden" value="@q.QuestionEssayId" name="ExamStudentEssay[@cEssay].QuestionEssayId" />
                                            </div>
                                        </div>
                                    </text>

                                    cq += 1
                                    cEssay += 1
                                Next
                            </div>
                        </text>
                    Case "cOneToFive"
                        @<text>
                            <div class="exam-module-border">
                                <div class="row">
                                    <div class="col-md-4">
                                        <strong>@e.QuestionGroup.DisplayName</strong>
                                    </div>
                                </div>
                                @For Each q As QuestionOneToFive In e.QuestionGroup.QuestionOneToFives
                                    @<text>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <select class="form-control" name="ExamStudentOneToFive[@cOneToFive].Answer">
                                                    <option value="1">1</option>
                                                    <option value="2">2</option>
                                                    <option value="3">3</option>
                                                    <option value="4">4</option>
                                                    <option value="5">5</option>
                                                </select>
                                            </div>
                                            
                                            <input type="hidden" value="@q.QuestionOneToFiveId" name="ExamStudentOneToFive[@cOneToFive].QuestionOneToFiveId"/>
                                            <div class="col-md-10">
                                                <p><strong>@cq .</strong> @q.Question</p>
                                            </div>
                                        </div>
                                    </text>

                                    cq += 1
                                    cTFRank += 1
                                Next
                            </div>
                        </text>
                End Select

                ' Exam counter reset
                cq = 1
            Next
            
            <div class="row">
                <div class="col-md-9"></div>

                <div class="col-md-3">
                    <label style="margin-bottom:8px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Submit" />
                </div>
            </div>
        </text>
                End Using
</div>

@Section styles
    <style>
        .exam-module-border{
            border-radius:3px;
            margin-bottom:10px;
            border:1px solid #cfcfcf;
            padding:20px;
        }
    </style>
End Section