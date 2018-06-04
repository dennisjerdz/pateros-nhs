@ModelType ApplicationUser
@Code
    ViewBag.Title = "Summary Results"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @Model.FirstName / @ViewBag.Title 
                <a class="header-btn btn btn-default" href="@Url.Action("Students")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>

    @code
        Dim list As List(Of ScoreViewModel) = New List(Of ScoreViewModel)()
    End Code
</div>

<div class="container body-data" style="background-color:white !important;">
    <div class="row">
        <div class="col-md-12">
            <table class="table">
                <thead>
                    <tr>
                        <th colspan="2" style="text-align:center;">Summary Results</th>
                    </tr>
                    <tr>
                        <th style="text-align:center;">My Problem Checklist</th>
                        <th style="text-align:center;">Needs Inventory</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>
                            <table class="table table-bordered">
                                <tbody>
                                    @Code
                                        Dim mp As ExamStudent = Model.ExamStudents.Where(Function(a) a.Exam.Name.Contains("My Problem Checklist") And a.TakenAt IsNot Nothing).OrderByDescending(Function(a) a.TakenAt).FirstOrDefault()
                                    End Code

                                    @If mp IsNot Nothing Then
                                        Dim myProblemPercentageTotal As Integer = 0
                                        Dim myProblemQuestionGroupTotal As Integer = 0

                                        For Each a In mp.Exam.ExamQuestionGroups
                                            If (a.QuestionGroup.DisplayName.Contains("About")) Then
                                                Dim questionTotal As Integer = a.QuestionGroup.QuestionTFRanks.Count()
                                                Dim trueTotal As Integer = 0

                                                For Each q As QuestionTFRank In a.QuestionGroup.QuestionTFRanks
                                                    Dim answer As ExamStudentTFRank = q.ExamStudentTFRanks.FirstOrDefault(Function(s) s.ExamStudentId = mp.ExamStudentId)

                                                    If answer.Answer = True Then
                                                        trueTotal += 1
                                                    End If
                                                Next

                                                Dim percentage As Double = Math.Round(((trueTotal / questionTotal) * 100), 2)

                                                myProblemPercentageTotal += percentage
                                                myProblemQuestionGroupTotal += 1

                                                @<text>
                                                    <tr>
                                                        <td>@a.QuestionGroup.DisplayName</td>
                                                        <td style="text-align:right;">@percentage %</td>
                                                    </tr>
                                                </text>

                                                list.Add(New ScoreViewModel() With {.Name = a.QuestionGroup.DisplayName, .Score = percentage})
                                            End If
                                        Next

                                        Dim average As Double = Math.Round((myProblemPercentageTotal / myProblemQuestionGroupTotal), 2)

                                        @<text>
                                            <tr>
                                                <td><strong>Average TOTAL:</strong></td>
                                                <td style="text-align:right;"><strong>@average %</strong></td>
                                            </tr>
                                        </text>
                                    End If
                                </tbody>
                            </table>
                        </td>

                        <td>
                            <table class="table table-bordered">
                                <tbody>
                                    @Code
                                        Dim ni As ExamStudent = Model.ExamStudents.Where(Function(a) a.Exam.Name.Contains("Needs Inventory") And a.TakenAt IsNot Nothing).OrderByDescending(Function(a) a.TakenAt).FirstOrDefault()
                                    End Code

                                    @If ni IsNot Nothing Then
                                        Dim needsInventoryPercentageTotal As Integer = 0
                                        Dim needsInventoryQuestionGroupTotal As Integer = 0

                                        For Each a In ni.Exam.ExamQuestionGroups
                                            Dim questionTotal As Integer = a.QuestionGroup.QuestionTFRanks.Count()
                                            Dim trueTotal As Integer = 0

                                            For Each q As QuestionTFRank In a.QuestionGroup.QuestionTFRanks
                                                Dim answer As ExamStudentTFRank = q.ExamStudentTFRanks.FirstOrDefault(Function(s) s.ExamStudentId = ni.ExamStudentId)

                                                If answer.Answer = True Then
                                                    trueTotal += 1
                                                End If
                                            Next

                                            Dim percentage As Double = Math.Round(((trueTotal / questionTotal) * 100), 2)

                                            needsInventoryPercentageTotal += percentage
                                            needsInventoryQuestionGroupTotal += 1

                                            @<text>
                                                    <tr>
                                                        <td>@a.QuestionGroup.DisplayName</td>
                                                        <td style="text-align:right;">@percentage %</td>
                                                    </tr>
                                            </text>

                                            list.Add(New ScoreViewModel() With {.Name = a.QuestionGroup.DisplayName, .Score = percentage})
                                        Next

                                        Dim average As Double = Math.Round((needsInventoryPercentageTotal / needsInventoryQuestionGroupTotal), 2)
                                        list.Add(New ScoreViewModel() With {.Name = "Needs", .Score = average})

                                        @<text>
                                            <tr>
                                                <td><strong>Average TOTAL:</strong></td>
                                                <td style="text-align:right;"><strong>@average %</strong></td>
                                            </tr>
                                        </text>
                                    End If
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>

                <thead>
                    <tr>
                        <th style="text-align:center;">Personality-Work Orientation</th>
                        <th style="text-align:center;">Interest Inventory</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>
                            <table class="table table-bordered">
                                <tbody>
                                    @Code
                                        Dim pw As ExamStudent = Model.ExamStudents.Where(Function(a) a.Exam.Name.Contains("Personality-Work Orientation Profile") And a.TakenAt IsNot Nothing).OrderByDescending(Function(a) a.TakenAt).FirstOrDefault()
                                    End Code

                                    @If pw IsNot Nothing Then
                                        Dim personalityWorkPercentageTotal As Integer = 0
                                        Dim personalityWorkQuestionGroupTotal As Integer = 0

                                        For Each a In pw.Exam.ExamQuestionGroups
                                            Dim questionTotal As Integer = a.QuestionGroup.QuestionTFRanks.Count()
                                            Dim trueTotal As Integer = 0

                                            For Each q As QuestionTFRank In a.QuestionGroup.QuestionTFRanks
                                                Dim answer As ExamStudentTFRank = q.ExamStudentTFRanks.FirstOrDefault(Function(s) s.ExamStudentId = pw.ExamStudentId)

                                                If answer.Answer = True Then
                                                    trueTotal += 1
                                                End If
                                            Next

                                            Dim percentage As Double = Math.Round(((trueTotal / questionTotal) * 100), 2)

                                            personalityWorkPercentageTotal += percentage
                                            personalityWorkQuestionGroupTotal += 1

                                            @<text>
                                                <tr>
                                                    <td>@a.QuestionGroup.DisplayName</td>
                                                    <td style="text-align:right;">@percentage %</td>
                                                </tr>
                                            </text>

                                            list.Add(New ScoreViewModel() With {.Name = a.QuestionGroup.DisplayName, .Score = percentage})
                                        Next

                                        Dim average As Double = Math.Round((personalityWorkPercentageTotal / personalityWorkQuestionGroupTotal), 2)
                                        list.Add(New ScoreViewModel() With {.Name = "Personality-Work Orientation", .Score = average})

                                        @<text>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td><strong>Average TOTAL:</strong></td>
                                                <td style="text-align:right;"><strong>@average %</strong></td>
                                            </tr>
                                        </text>
                                    End If
                                </tbody>
                            </table>
                        </td>

                        <td>
                            <table class="table table-bordered">
                                <tbody>
                                    @Code
                                        Dim ii As ExamStudent = Model.ExamStudents.Where(Function(a) a.Exam.Name.Contains("Interest Inventory") And a.TakenAt IsNot Nothing).OrderByDescending(Function(a) a.TakenAt).FirstOrDefault()
                                    End Code

                                    @If ii IsNot Nothing Then
                                        Dim interestInventoryPercentageTotal As Integer = 0
                                        Dim interestInventoryQuestionGroupTotal As Integer = 0

                                        For Each a In ii.Exam.ExamQuestionGroups
                                            Dim questionTotal As Integer = a.QuestionGroup.QuestionTFRanks.Count()
                                            Dim trueTotal As Integer = 0

                                            For Each q As QuestionTFRank In a.QuestionGroup.QuestionTFRanks
                                                Dim answer As ExamStudentTFRank = q.ExamStudentTFRanks.FirstOrDefault(Function(s) s.ExamStudentId = ii.ExamStudentId)

                                                If answer.Answer = True Then
                                                    trueTotal += 1
                                                End If
                                            Next

                                            Dim percentage As Double = Math.Round(((trueTotal / questionTotal) * 100), 2)

                                            interestInventoryPercentageTotal += percentage
                                            interestInventoryQuestionGroupTotal += 1

                                            @<text>
                                                <tr>
                                                    <td>@a.QuestionGroup.DisplayName</td>
                                                    <td style="text-align:right;">@percentage %</td>
                                                </tr>
                                            </text>

                                            list.Add(New ScoreViewModel() With {.Name = a.QuestionGroup.DisplayName, .Score = percentage})
                                        Next

                                        Dim average As Double = Math.Round((interestInventoryPercentageTotal / interestInventoryQuestionGroupTotal), 2)
                                        list.Add(New ScoreViewModel() With {.Name = "Interest Inventory", .Score = average})

                                        @<text>
                                            <tr>
                                                <td><strong>Average TOTAL:</strong></td>
                                                <td style="text-align:right;"><strong>@average %</strong></td>
                                            </tr>
                                        </text>
                                    End If
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th style="text-align:center;">Aptitude</th>
                    </tr>
                </thead>

                <tbody>
                    @Code
                        Dim aa As ExamStudent = Model.ExamStudents.Where(Function(a) a.Exam.Name.Contains("Aptitudes Assessment") And a.TakenAt IsNot Nothing).OrderByDescending(Function(a) a.TakenAt).FirstOrDefault()

                        Dim aptitudesList As List(Of String) = New List(Of String)

                        Dim questionCount As Integer = 0
                    End Code

                    @If aa IsNot Nothing Then
                        For Each a In aa.Exam.ExamQuestionGroups
                            For Each q As QuestionTFList In a.QuestionGroup.QuestionTFLists
                                Dim answer As ExamStudentTFList = q.ExamStudentTFLists.FirstOrDefault(Function(s) s.ExamStudentId = aa.ExamStudentId)

                                If answer.Answer = True Then
                                    aptitudesList.Add(q.Question)
                                End If

                                questionCount += 1
                            Next
                        Next
                    End If

                    @For Each item In aptitudesList
                        @<text>
                            <tr><td style="text-align:center;">@item</td></tr>
                        </text>
                    Next

                    <tr>
                        <td style="text-align:center;"><strong>Results: @aptitudesList.Count() / @questionCount</strong></td>
                    </tr>

                    @Code
                        list.Add(New ScoreViewModel() With {.Name = "Aptitudes", .Score = aptitudesList.Count()})
                    End Code
                </tbody>
            </table>
        </div>

        <div class="col-md-6">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th style="text-align:center;"><strong>Suggested Track</strong></th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align:center;">
                            @code
                                Dim relationshipConserver = 0
                                Dim conventional = 0
                                Dim aptitudes = 0
                                Dim groupPreserver = 0
                                Dim artistic = 0
                                Dim realistic = 0
                                Dim resultsDriver = 0
                                Dim investigative = 0
                                Dim enterprising = 0
                                Dim dataAnalyzer = 0

                                If list.Any(Function(s) s.Name = "Relationship Conserver") Then
                                    relationshipConserver = list.FirstOrDefault(Function(s) s.Name = "Relationship Conserver").Score
                                End If

                                If list.Any(Function(s) s.Name = "Conventional") Then
                                    conventional = list.FirstOrDefault(Function(s) s.Name = "Conventional").Score
                                End If

                                If list.Any(Function(s) s.Name = "Aptitudes") Then
                                    aptitudes = list.FirstOrDefault(Function(s) s.Name = "Aptitudes").Score
                                End If

                                If list.Any(Function(s) s.Name = "Group Preserver") Then
                                    groupPreserver = list.FirstOrDefault(Function(s) s.Name = "Group Preserver").Score
                                End If

                                If list.Any(Function(s) s.Name = "Artistic") Then
                                    artistic = list.FirstOrDefault(Function(s) s.Name = "Artistic").Score
                                End If

                                If list.Any(Function(s) s.Name = "Realistic") Then
                                    artistic = list.FirstOrDefault(Function(s) s.Name = "Realistic").Score
                                End If

                                If list.Any(Function(s) s.Name = "Data Analyzer") Then
                                    dataAnalyzer = list.FirstOrDefault(Function(s) s.Name = "Data Analyzer").Score
                                End If

                                If list.Any(Function(s) s.Name = "Results Driver") Then
                                    resultsDriver = list.FirstOrDefault(Function(s) s.Name = "Results Driver").Score
                                End If

                                If list.Any(Function(s) s.Name = "Enterprising") Then
                                    enterprising = list.FirstOrDefault(Function(s) s.Name = "Enterprising").Score
                                End If

                                If list.Any(Function(s) s.Name = "Investigative") Then
                                    investigative = list.FirstOrDefault(Function(s) s.Name = "Investigative").Score
                                End If

                                If (relationshipConserver >= 55) And (conventional >= 55) And (aptitudes >= 5) Then
                                @<strong style="padding-right:2px;">Academic Track</strong>
                                End If

                                If (dataAnalyzer >= 55) And (groupPreserver >= 55) And (artistic >= 55) And (realistic >= 55) And (aptitudes >= 5) Then
                                @<strong style="padding-right:2px;">Arts & Design Track</strong>
                                End If

                                If (resultsDriver >= 55) And (groupPreserver >= 55) And (investigative >= 55) And (enterprising >= 55) Then
                                @<strong style="padding-right:2px;">Sports Track</strong>
                                End If

                                If (list.FirstOrDefault(Function(s) s.Name = "Needs").Score >= 55) And
                                                                        (list.FirstOrDefault(Function(s) s.Name = "Personality-Work Orientation").Score >= 55) And
                                                                        (list.FirstOrDefault(Function(s) s.Name = "Interest Inventory").Score >= 55) And
                                                                        (list.FirstOrDefault(Function(s) s.Name = "Aptitudes").Score >= 5) Then
                                @<strong style="padding-right:2px;">Technical Vocational-Livelihood (TVL) Track</strong>
                            End If

                            End Code
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
