@ModelType GuidanceCounselling.ExamStudent

@Code
    ViewBag.Title = "Exam"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.ExamStudentId / Results
            </p>
        </div>
    </div>
</div>

<div class="container">
    @Code
        Dim cTFRank As Integer = 0
        Dim cTFList As Integer = 0
        Dim cEssay As Integer = 0
        Dim cOneToFive As Integer = 0

        Dim cq As Integer = 1

        Dim CountTFRankQuestion As Integer = 0
        Dim CountTFListQuestion As Integer = 0
    End Code

    @For Each e In Model.Exam.ExamQuestionGroups
                Select Case e.QuestionGroup.ExamType.ToString()
                    Case "TFRank"
                @<text>

                    @Code
                        Dim rankTrueCount As Integer = 0
                    End Code

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
                                        @Code
                                            Dim answer As ExamStudentTFRank = q.ExamStudentTFRanks.FirstOrDefault(Function(s) s.ExamStudentId = Model.ExamStudentId)

                                            If answer.Answer = True Then
                                                rankTrueCount += 1
                                            End If
                                        End Code

                                        <strong>@answer.Answer</strong>
                                    </div>

                                    <div class="col-md-10">
                                        <p><strong>@cq .</strong> @q.Question</p>
                                    </div>
                                </div>
                            </text>

                                                cq += 1
                                                cTFRank += 1
                                                CountTFRankQuestion += 1
                                            Next
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <p style="color:#ff3f1c;">
                                Results:

                                @If rankTrueCount > 0 Then
                                    Dim rd As Decimal = Convert.ToDecimal(rankTrueCount / CountTFRankQuestion)

                                    @<text>
                                        @Math.Round((rd * 100), 2) % or @rankTrueCount / @CountTFRankQuestion
                                    </text>
                                Else
                                    @<text>
                                        0% or 0 / @CountTFRankQuestion
                                    </text>
                                End If
                            </p>
                            <br />
                        </div>
                    </div>
                </text>
                                            Case "TFList"
                @<text>
                    
                    @Code
                        Dim listTrueCount As Integer = 0
                    End Code
                    
                    <div class="exam-module-border">
                        <div class="row">
                            <div class="col-md-4">
                                <strong>@e.QuestionGroup.DisplayName</strong>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                @For Each q As QuestionTFList In e.QuestionGroup.QuestionTFLists
                                @Code
                                    Dim answer As ExamStudentTFList = q.ExamStudentTFLists.FirstOrDefault(Function(s) s.ExamStudentId = Model.ExamStudentId)
                                End Code

                                @If answer.Answer = True Then
                                    @<p>@q.Question</p>

                                    @Code
                                        listTrueCount += 1
                                    End Code

                                        End If

                                            cq += 1
                                            cTFList += 1
                                            CountTFListQuestion += 1
                                        Next
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <p style="color:#ff3f1c;">Results: @listTrueCount / @CountTFListQuestion Problems selected.</p>
                            <br />
                        </div>
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
                                        @Code
                                            Dim answer As ExamStudentEssay = q.ExamStudentEssays.FirstOrDefault(Function(s) s.ExamStudentId = Model.ExamStudentId)
                                        End Code

                                        <p>@answer.Answer</p>
                                    </div>
                                </div>
                            </text>
                                                cq += 1
                                                cEssay += 1
                                            Next
                    </div>
                </text>
                                            Case "OneToFive"
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
                                        @Code
                                            Dim answer As ExamStudentOneToFive = q.ExamStudentOneToFives.FirstOrDefault(Function(s) s.ExamStudentId = Model.ExamStudentId)
                                        End Code

                                        <p>@answer.Answer</p>
                                    </div>

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

                                                CountTFListQuestion = 0
                                                CountTFRankQuestion = 0
                                            Next
</div>

@Section styles
    <style>
        .exam-module-border {
            border-radius: 3px;
            margin-bottom: 10px;
            border: 1px solid #cfcfcf;
            padding: 20px;
        }
    </style>
End Section