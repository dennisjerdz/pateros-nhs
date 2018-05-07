@ModelType GuidanceCounselling.NCAEGradeEditModel
@Code
    ViewBag.Title = "NCAE Grade"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / Edit
                <a class="header-btn btn btn-default" href="@Url.Action("NCAEGrades", New With {.id = Model.UserId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditNCAEGrade", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.UserId, New With {.Value = Model.UserId})
        @Html.HiddenFor(Function(m) m.NCAEGradeId, New With {.Value = Model.NCAEGradeId})
        @Html.HiddenFor(Function(m) m.Name, New With {.Value = Model.Name})

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})

            <div class="row">
                <div class="col-md-4">
                    <label class="control-label">Student Name:</label>
                    <p>@Model.StudentName</p>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Name, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "NCAE Grade"}})
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Areas Of Occupational Interest</th>
                                <th>Percentage Score</th>
                                <th>Preference Level</th>
                                <th>Rank Overall</th>
                            </tr>
                        </thead>

                        <tbody>
                            @code
                                Dim c As Integer = 0
                            End Code

                            @For Each item In Model.NCAEGradeSubjects
                                @<text>
                                    @code
                                        Dim iPL As String = "inputPreferenceLevel" + c.ToString()
                                    End Code

                                    <tr>
                                        <td>@item.Name</td>
                                        <td>
                                            @item.PercentageScore
                                        </td>
                                        <td>
                                            @If item.PercentageScore <= 25 Then
                                                @<text>VLP</text>
                                            End If

                                            @If item.PercentageScore <= 50 And item.PercentageScore >= 26 Then
                                                @<text>LP</text>
                                            End If

                                            @If item.PercentageScore <= 75 And item.PercentageScore >= 51 Then
                                                @<text>MP</text>
                                            End If

                                            @If item.PercentageScore <= 100 And item.PercentageScore >= 76 Then
                                                @<text>HP</text>
                                            End If
                                        </td>
                                        <td>
                                            @item.RankOverall
                                            <input type="hidden" name="NCAEGradeSubjects[@c].NCAEGradeSubjectID" value="@item.NCAEGradeSubjectId" />
                                            <input type="hidden" name="NCAEGradeSubjects[@c].NCAEGradeId" value="@item.NCAEGradeId" />
                                            <input type="hidden" name="NCAEGradeSubjects[@c].Name" value="@item.Name" />
                                        </td>
                                    </tr>
                                </text>

                                            c += 1
                                        Next
                        </tbody>
                    </table>

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th colspan="2">Levels of Preference for the Occupational Interest</th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr>
                                <td>76% - 100% High Preference (HP)</td>
                                <td>26% - 50% Low Preference (LP)</td>
                            </tr>

                            <tr>
                                <td>51% - 75% Moderate Preference (MP)</td>
                                <td>0% - 25% Very Low Preference (VLP)</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="col-md-6">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Areas</th>
                                <th>Standard Score</th>
                                <th>Percentile Rank</th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr>
                                <td colspan="3">
                                    <center>
                                        General Scholastic Aptitude (GSA)
                                    </center>
                                </td>
                            </tr>
                            @code
                                Dim b As Integer = 0
                                Dim gsaStandardScoreTotal As Decimal = 0
                                Dim gsaPercentileRankTotal As Decimal = 0
                                Dim gsaCount As Decimal = 0

                                Dim tvaStandardScoreTotal As Decimal = 0
                                Dim tvaPercentileRankTotal As Decimal = 0
                                Dim tvaCount As Decimal = 0

                                Dim atStandardScoreTotal As Decimal = 0
                                Dim atPercentileRankTotal As Decimal = 0
                                Dim atCount As Decimal = 0
                            End Code

                            @For Each item In Model.NCAEGradeAptitudes.Where(Function(t) t.Type = 1)
                                @<text>
                                    <tr>
                                        <td>@item.Name</td>
                                        <td>
                                            @item.StandardScore
                                        </td>
                                        <td>
                                            @item.PercentileRank
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeAptitudeId" value="@item.NCAEGradeAptitudeId" />
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeId" value="@item.NCAEGradeId" />
                                        </td>
                                    </tr>
                                </text>

                                b += 1
                                gsaCount += 1

                                gsaStandardScoreTotal += item.StandardScore
                                gsaPercentileRankTotal += item.PercentileRank
                            Next

                            <tr>
                                <td><strong>OVERALL GSA</strong></td>
                                <td style="font-weight:600;">
                                    @Code
                                        Dim gsaStandardScoreAve As Decimal = gsaStandardScoreTotal / gsaCount
                                        Dim gsaPercentileRankAve As Decimal = gsaPercentileRankTotal / gsaCount
                                    End Code

                                    @gsaStandardScoreAve
                                </td>
                                <td style="font-weight:600;">
                                    @gsaPercentileRankAve
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">
                                    <center>
                                        Technical Vocational Aptitude (TVA)
                                    </center>
                                </td>
                            </tr>

                            @For Each item In Model.NCAEGradeAptitudes.Where(Function(t) t.Type = 2)
                                @<text>
                                    <tr>
                                        <td>@item.Name</td>
                                        <td>
                                            @item.StandardScore
                                        </td>
                                        <td>
                                            @item.PercentileRank
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeAptitudeId" value="@item.NCAEGradeAptitudeId" />
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeId" value="@item.NCAEGradeId" />
                                        </td>
                                    </tr>
                                </text>

                                b += 1
                                tvaCount += 1

                                tvaStandardScoreTotal += item.StandardScore
                                tvaPercentileRankTotal += item.PercentileRank
                            Next

                            <tr>
                                <td><strong>OVERALL TVA</strong></td>
                                <td style="font-weight:600;">
                                    @Code
                                        Dim tvaStandardScoreAve As Decimal = tvaStandardScoreTotal / tvaCount
                                        Dim tvaPercentileRankAve As Decimal = tvaPercentileRankTotal / tvaCount
                                    End Code

                                    @tvaStandardScoreAve
                                </td>
                                <td style="font-weight:600;">
                                    @tvaPercentileRankAve
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">
                                    <center>
                                        Academic Track (AT)
                                    </center>
                                </td>
                            </tr>

                            @For Each item In Model.NCAEGradeAptitudes.Where(Function(t) t.Type = 3)
                                @<text>
                                    <tr>
                                        <td>@item.Name</td>
                                        <td>
                                            @item.StandardScore
                                        </td>
                                        <td>
                                            @item.PercentileRank
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeAptitudeId" value="@item.NCAEGradeAptitudeId" />
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeId" value="@item.NCAEGradeId" />
                                        </td>
                                    </tr>
                                </text>

                                b += 1
                                atCount += 1

                                atStandardScoreTotal += item.StandardScore
                                atPercentileRankTotal += item.PercentileRank
                            Next

                            <tr>
                                <td><strong>OVERALL AT</strong></td>
                                <td style="font-weight:600;">
                                    @Code
                                        Dim atStandardScoreAve As Decimal = atStandardScoreTotal / atCount
                                        Dim atPercentileRankAve As Decimal = atPercentileRankTotal / atCount
                                    End Code

                                    @atStandardScoreAve
                                </td>
                                <td style="font-weight:600;">
                                    @atPercentileRankAve
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th colspan="2"><center>Preferred Track & Strand/Concentration</center></th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr>
                                <td>Track Choice:</td>
                                <td>
                                    @Html.EditorFor(Function(model) model.TrackChoice, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = ""}})
                                </td>
                            </tr>
                            <tr>
                                <td>Strand/Concentration Choice:</td>
                                <td>
                                    @Html.EditorFor(Function(model) model.StrandConcentrationChoice, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = ""}})
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </text> End Using
</div>

@Section Scripts
    <script>
        $(".inputPercentageScore").keyup(function () {
            var base = $(this).val();
            //alert(base);

            if (base <= 25) {
                var num = $(this).data("count");
                var find = "#inputPreferenceLevel" + num;
                $(find).val("VLP");
            }
            else if(base >= 26 && base <= 50){
                var num = $(this).data("count");
                var find = "#inputPreferenceLevel" + num;
                $(find).val("LP");
            }
            else if(base >= 51 && base <= 75){
                var num = $(this).data("count");
                var find = "#inputPreferenceLevel" + num;
                $(find).val("MP");
            }
            else if(base >= 76 && base <= 100){
                var num = $(this).data("count");
                var find = "#inputPreferenceLevel" + num;
                $(find).val("HP");
            }
        });
    </script>
End Section