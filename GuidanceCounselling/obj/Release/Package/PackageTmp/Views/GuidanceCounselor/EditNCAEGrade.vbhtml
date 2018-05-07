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
                                            <input data-count="@c" class="inputPercentageScore form-control" name="NCAEGradeSubjects[@c].PercentageScore" value="@item.PercentageScore"/>
                                        </td>
                                        <td>
                                            <input id="@iPL" class="inputPrefenceLevel form-control" name="NCAEGradeSubjects[@c].PreferenceLevel" readonly/>
                                        </td>
                                        <td>
                                            <input class="form-control" name="NCAEGradeSubjects[@c].RankOverall" value="@item.RankOverall" />
                                            <input type="hidden" name="NCAEGradeSubjects[@c].NCAEGradeSubjectID" value="@item.NCAEGradeSubjectId"/>
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
                            End Code

                            @For Each item In Model.NCAEGradeAptitudes.Where(Function(t) t.Type = 1)
                                @<text>
                                    <tr>
                                        <td>@item.Name</td>
                                        <td>
                                            <input name="NCAEGradeAptitudes[@b].StandardScore" class="form-control" value="@item.StandardScore"/>
                                        </td>
                                        <td>
                                            <input name="NCAEGradeAptitudes[@b].PercentileRank" class="form-control" value="@item.PercentileRank"/>
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeAptitudeId" value="@item.NCAEGradeAptitudeId"/>
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeId" value="@item.NCAEGradeId" />
                                        </td>
                                    </tr>
                                </text>

                                b += 1
                            Next

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
                                            <input name="NCAEGradeAptitudes[@b].StandardScore" class="form-control" value="@item.StandardScore"/>
                                        </td>
                                        <td>
                                            <input name="NCAEGradeAptitudes[@b].PercentileRank" class="form-control" value="@item.PercentileRank"/>
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeAptitudeId" value="@item.NCAEGradeAptitudeId" />
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeId" value="@item.NCAEGradeId" />
                                        </td>
                                    </tr>
                                </text>

                                b += 1
                            Next

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
                                            <input name="NCAEGradeAptitudes[@b].StandardScore" class="form-control" value="@item.StandardScore"/>
                                        </td>
                                        <td>
                                            <input name="NCAEGradeAptitudes[@b].PercentileRank" class="form-control" value="@item.PercentileRank"/>
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeAptitudeId" value="@item.NCAEGradeAptitudeId" />
                                            <input type="hidden" name="NCAEGradeAptitudes[@b].NCAEGradeId" value="@item.NCAEGradeId" />
                                        </td>
                                    </tr>
                                </text>

                                b += 1
                            Next
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

            <!--<div class="row">
                <div class="col-md-6">
                    @For Each item In Model.NCAEGradeSubjects
                        @<p>@item.Name</p>
                    Next
                </div>

                <div class="col-md-6">
                    @For Each item In Model.NCAEGradeAptitudes
                        @<p>@item.Name</p>
                    Next
                </div>
            </div>-->

            <div class="row">
                <div class="col-md-8"></div>

                <div class="col-md-4">
                    <label style="margin-bottom:0px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Save NCAE Grade" />
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