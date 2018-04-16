@modeltype GuidanceCounselling.ExamStudent

@code
    ViewBag.title = "Exam"
end code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.title / @Model.ExamStudentId / Results
                <a class="header-btn btn btn-default" href="@Url.Action("AssignedExams", New With {.id = Nothing})"><span class="glyphicon glyphicon-chevron-left"></span> Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container">
    @code
        Dim ctfrank As Integer = 0
        Dim ctflist As Integer = 0
        Dim cessay As Integer = 0
        Dim conetofive As Integer = 0

        Dim cq As Integer = 1

        Dim counttfrankquestion As Integer = 0
        Dim counttflistquestion As Integer = 0
    end code

    @for each e In Model.Exam.ExamQuestionGroups
        Select Case e.QuestionGroup.ExamType.ToString()
            Case "TFRank"
                @<text>

                    @code
                        dim ranktruecount As Integer = 0
                    end code

                    <div class="exam-module-border">
                        <div class="row">
                            <div class="col-md-12">
                                <strong>@e.QuestionGroup.DisplayName</strong>
                            </div>
                        </div>
                        @For Each q As QuestionTFRank In e.QuestionGroup.QuestionTFRanks
                            @<text>
                                <div class="row">
                                    <div class="col-md-2">
                                        @code
                                            dim answer As ExamStudentTFRank = q.ExamStudentTFRanks.FirstOrDefault(Function(s) s.ExamStudentId = Model.ExamStudentId)

                                            If answer.Answer = True Then
                                                ranktruecount += 1
                                            End If
                                        end code

                                        <strong>@answer.Answer</strong>
                                    </div>

                                    <div class="col-md-10">
                                        <p><strong>@cq .</strong> @q.Question</p>
                                    </div>
                                </div>
                            </text>

                                                cq += 1
                                                ctfrank += 1
                                                counttfrankquestion += 1
                                            Next
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <p style="color:#ff3f1c;">
                                Results:

                                @if ranktruecount > 0 Then
                                    Dim rd As Decimal = Convert.ToDecimal(ranktruecount / counttfrankquestion)

                                    @<text>
                                        @Math.Round((rd * 100), 2) % or @ranktruecount / @counttfrankquestion
                                    </text>
                                Else
                                    @<text>
                                        0% or 0 / @counttfrankquestion
                                    </text>
                                End If
                            </p>
                            <br />
                        </div>
                    </div>
                </text>
                                            Case "TFList"
                                                @<text>

                                                    @code
                                                        dim listtruecount As Integer = 0
                                                    end code

                                                    <div class="exam-module-border">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <strong>@e.QuestionGroup.DisplayName</strong>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                @for Each q As QuestionTFList In e.QuestionGroup.QuestionTFLists
                                                                    @code
                                                                        dim answer As ExamStudentTFList = q.ExamStudentTFLists.FirstOrDefault(Function(s) s.ExamStudentId = Model.ExamStudentId)
                                                                    end code

                                                                        @if answer.Answer = True Then
                                                                            @<p>@q.Question</p>

                                                                            @code
    listtruecount += 1
                                                                            end code

                                                                                end if

                                                                                    cq += 1
                                                                                    ctflist += 1
                                                                                    counttflistquestion += 1
                                                                                Next
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <p style="color:#ff3f1c;">Results: @listtruecount / @counttflistquestion</p>
                                                            <br />
                                                        </div>
                                                    </div>
                                                </text>
                                                                                Case "Essay"
                                                                                    @<text>
                                                                                        <div class="exam-module-border">
                                                                                            <div class="row">
                                                                                                <div class="col-md-12">
                                                                                                    <strong>@e.QuestionGroup.DisplayName</strong>
                                                                                                </div>
                                                                                            </div>
                                                                                            @for Each q As QuestionEssay In e.QuestionGroup.QuestionEssays
                                                                                                @<text>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-8">
                                                                                                            <p><strong>@cq .</strong> @q.Question</p>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="row">
                                                                                                        <div class="col-md-8">
                                                                                                            @code
                                                                                                                Dim answer As ExamStudentEssay = q.ExamStudentEssays.FirstOrDefault(Function(s) s.ExamStudentId = Model.ExamStudentId)
                                                                                                            end code

                                                                                                            <p>@answer.Answer</p>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </text>
                                                                                                                    cq += 1
                                                                                                                    cessay += 1
                                                                                                                Next
                                                                                        </div>
                                                                                    </text>
                                                                                                                Case "OneToFive"
                                                                                                                    @<text>
                                                                                                                        <div class="exam-module-border">
                                                                                                                            <div class="row">
                                                                                                                                <div class="col-md-12">
                                                                                                                                    <strong>@e.QuestionGroup.DisplayName</strong>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            @for Each q As QuestionOneToFive In e.QuestionGroup.QuestionOneToFives
                                                                                                                                @<text>
                                                                                                                                    <div class="row">
                                                                                                                                        <div class="col-md-2">
                                                                                                                                            @code
                                                                                                                                                dim answer As ExamStudentOneToFive = q.ExamStudentOneToFives.FirstOrDefault(Function(s) s.ExamStudentId = Model.ExamStudentId)
                                                                                                                                            end code

                                                                                                                                            <p>@answer.Answer</p>
                                                                                                                                        </div>

                                                                                                                                        <div class="col-md-10">
                                                                                                                                            <p><strong>@cq .</strong> @q.Question</p>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </text>

                                                                                                                                                    cq += 1
                                                                                                                                                    ctfrank += 1
                                                                                                                                                Next
                                                                                                                        </div>
                                                                                                                    </text>
                                                                                                                                                    End Select

                                                                                                                                                    ' exam counter reset
                                                                                                                                                    cq = 1

                                                                                                                                                    counttflistquestion = 0
                                                                                                                                                    counttfrankquestion = 0
                                                                                                                                                Next
</div>

@section styles
    <style>
        .exam-module-border {
            border-radius: 3px;
            margin-bottom: 10px;
            border: 1px solid #cfcfcf;
            padding: 20px;
        }
    </style>
end section