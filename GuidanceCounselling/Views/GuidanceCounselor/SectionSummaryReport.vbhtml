@ModelType List(Of ApplicationUser)
@Code
    ViewBag.Title = "Summary Results"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @Model.FirstName / @ViewBag.Title
                <a class="header-btn btn btn-default" href="@Url.Action("GSection")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
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
                                        Dim userIDs() As String = Model.Select(Function(a) a.Id).ToArray()
                                        Dim examStudentIDs() As Integer = Model.Select(Function(m) m.ExamStudents.FirstOrDefault(Function(e) e.Exam.Name.Contains("My Problem Checklist") And e.TakenAt IsNot Nothing).ExamId)
                                        Dim mps As List(Of ExamStudent) = Model.Select(Function(m) m.ExamStudents.FirstOrDefault(Function(e) e.Exam.Name.Contains("My Problem Checklist") And e.TakenAt IsNot Nothing)).ToList()
                                        ' Get Curret Question Groups List
                                        'Dim currentQG As List(Of QuestionGroup) = allQG.Where(Function(a) arrayOfExamQG.Contains(a.QuestionGroupId)).ToList()

                                        'Dim mp As ExamStudent = Model.ExamStudents.Where(Function(a) a.Exam.Name.Contains("My Problem Checklist") And a.TakenAt IsNot Nothing).OrderByDescending(Function(a) a.TakenAt).FirstOrDefault()
                                    End Code

                                    @If mps IsNot Nothing Then
                                        Dim myProblemPercentageTotal As Integer = 0
                                        Dim myProblemQuestionGroupTotal As Integer = 0

                                        For Each m In mps.Select(Function(s) s.Exam.ExamQuestionGroups)
                                            If (m.Select(Function(s) s.QuestionGroup.DisplayName.Contains("About"))) Then

                                            End If
                                            Dim questionTotal As Integer = m.Sum(Function(c) c.QuestionGroup.QuestionTFRanks.Count())
                                            Dim trueTotal As Integer = 0

                                            For Each qs As List(Of QuestionTFRank) In m.Select(Function(a) a.QuestionGroup.QuestionTFRanks)
                                                For Each q As QuestionTFRank In qs
                                                    Dim answer As ExamStudentTFRank = q.ExamStudentTFRanks.FirstOrDefault(Function(s) s.ExamStudentId = mp.ExamStudentId)

                                                    If answer.Answer = True Then
                                                        trueTotal += 1
                                                    End If
                                                Next
                                            Next
                                        Next

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
                                        Next

                                        Dim average As Double = Math.Round((needsInventoryPercentageTotal / needsInventoryQuestionGroupTotal), 2)

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
                                        Next

                                        Dim average As Double = Math.Round((personalityWorkPercentageTotal / personalityWorkQuestionGroupTotal), 2)

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
                                        Next

                                        Dim average As Double = Math.Round((interestInventoryPercentageTotal / interestInventoryQuestionGroupTotal), 2)

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
        <div class="col-md-6 col-md-offset-3">
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
                </tbody>
            </table>
        </div>
    </div>
</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
