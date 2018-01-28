@ModelType GuidanceCounselling.AssignExamModel
@Code
    ViewBag.Title = "Exams"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.ExamId (@Model.Name) / Assign
                <a class="header-btn btn btn-default" href="@Url.Action("Exams", New With {.id = Model.ExamId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

@Using Html.BeginForm("AssignExam", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

    @Html.AntiForgeryToken()
    @Html.HiddenFor(Function(m) m.ExamId, New With {.Value = Model.ExamId})
    @Html.HiddenFor(Function(m) m.Name, New With {.Value = Model.Name})

    @<text>
        <div class="container body-header">
            <div class="row">
                <div class="col-md-2">
                    Select Students

                    @Html.ValidationSummary("", New With {.class = "text-danger"})
                </div>
                <div class="col-md-3">
                    @Html.LabelFor(Function(m) m.AvailabilityStart, New With {.class = "control-label"})
                    @Html.EditorFor(Function(m) m.AvailabilityStart, New With {.htmlAttributes = New With {.class = "form-control", .type = "datetime-local"}})
                </div>

                <div class="col-md-1"></div>

                <div class="col-md-3">
                    @Html.LabelFor(Function(m) m.AvailabilityEnd, New With {.class = "control-label"})
                    @Html.EditorFor(Function(m) m.AvailabilityEnd, New With {.htmlAttributes = New With {.class = "form-control", .type = "datetime-local"}})
                </div>

                <div class="col-md-1"></div>

                <div class="col-md-2">
                    <label style="margin-bottom:8px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Submit & Assign" />
                </div>
            </div>
        </div>
        
        @code
            Dim sCount As Integer = 0

            For Each i As Grade In Model.Grades
                Dim gradeTarget As String = "GradeCollapse" + i.GradeId.ToString()

                @<text>
                    <div class="container body-data">
                        <div class="row" style="margin-top:10px;">
                            <div class="col-md-12">
                               <strong>@i.Name</strong>
                               <button type="button" data-toggle="collapse" class="btn btn-xs btn-primary grade-expand-btn" style="float:right;" data-target="#@gradeTarget">Open Sections</button> 
                            </div>
                        </div>

                        <div class="collapse" id="@gradeTarget">
                            @code
                                For Each s As Section In i.Sections
                                    Dim sectionTarget As String = "SectionCollapse" + s.SectionId.ToString()
                                    Dim sectionCheck As String = "SectionCheck" + s.SectionId.ToString()

                                    @<text>
                                        <div class="section-border">
                                            <div class="row" style="margin-top:2px; margin-bottom:2px;">
                                                <div class="col-md-12">
                                                    <strong style="color:#44b1df; margin-left:5px;">@s.Name</strong>
                                                    <button type="button" data-toggle="collapse" class="btn btn-xs btn-primary section-expand-btn" style="float:right;" data-target="#@sectionTarget">Open Students</button>
                                                </div>
                                            </div>

                                            <div class="row collapse" id="@sectionTarget">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <table class="table table-hover table-condensed">
                                                            <thead>
                                                                <tr>
                                                                    <th>Name</th>
                                                                    <th>Email</th>
                                                                    <th>Last Assigned</th>
                                                                    <th>Last Taken</th>
                                                                    <th>
                                                                        <button value="@sectionCheck" type="button" class="btn btn-xs btn-info include-btn">✓ Include All in Section</button>
                                                                    </th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                @code
                                                                    For Each u As ApplicationUser In s.Students.OrderByDescending(Function(l) l.LastName)
                                                                        @<text>
                                                                            <tr>
                                                                                <td>@u.getFullName</td>
                                                                                <td>@u.Email</td>
                                                                                <td>
                                                                                    @If u.ExamStudents.Any(Function(a) a.ExamId = Model.ExamId) Then
                                                                                        @u.ExamStudents.OrderByDescending(Function(e) e.DateCreated).FirstOrDefault(Function(x) x.ExamId = Model.ExamId).DateCreated
                                                                                    Else
                                                                                        @<text>-</text>
                                                                                    End If
                                                                                </td>
                                                                                <td>
                                                                                    @If u.ExamStudents.Any(Function(a) a.ExamId = Model.ExamId) Then
                                                                                        @u.ExamStudents.OrderByDescending(Function(e) e.DateCreated).FirstOrDefault(Function(x) x.ExamId = Model.ExamId).TakenAt
                                                                                    Else
                                                                                        @<text>-</text>
                                                                                    End If
                                                                                </td>
                                                                                <td>
                                                                                    <div class="checkbox">
                                                                                        <label>Included <input type="checkbox" value="True" class="studentRadio @sectionCheck" name="Students[@sCount].Included"></label>
                                                                                    </div>
                                                                                    <input type="hidden" name="Students[@sCount].UserId" value="@u.Id"/>
                                                                                </td>
                                                                            </tr>
                                                                        </text>

                                                                        sCount += 1
                                                                    Next
                                                                End Code
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </text>
                                                                    Next
                            End Code
                        </div>
                    </div>
                </text>
                                                                    Next
        End Code

</text> End Using

@Section scripts
    <script>
        $(document).on("click", ".grade-expand-btn", function () {
            if ($(this).html() == "Open Sections") {
                $(this).html("Hide Sections");
                $(this).removeClass("btn-primary")
                $(this).addClass("btn-danger")
            } else {
                $(this).html("Open Sections")
                $(this).removeClass("btn-danger")
                $(this).addClass("btn-primary")
            }
        });

        $(document).on("click", ".section-expand-btn", function () {
            if ($(this).html() == "Open Students") {
                $(this).html("Hide Students");
                $(this).removeClass("btn-primary")
                $(this).addClass("btn-danger")
            } else {
                $(this).html("Open Students")
                $(this).removeClass("btn-danger")
                $(this).addClass("btn-primary")
            }
        });

        $(document).on("click", ".include-btn", function () {
            var check = "."+$(this).val()

            if ($(this).html() == "✓ Include All in Section") {
                $(this).html("Clear");
                $(this).removeClass("btn-info")
                $(this).addClass("btn-danger")

                $(check).each(function () {
                    $(this).prop("checked", true)
                })
            } else {
                $(this).html("✓ Include All in Section")
                $(this).removeClass("btn-danger")
                $(this).addClass("btn-info")

                $(check).each(function () {
                    $(this).prop("checked", false)
                })
            }
        });

        $(document).on("click", ".studentRadio", function () {
            /*if ($(this).is(":checked")) {
                $(this).prop("checked", false);
            }

            if (!$(this).is(":checked")) {
                $(this).prop("checked", true);
            }*/
        });
    </script>
End Section

@Section styles
    <style>
        .section-border{
            border:2px solid #dcdcdc;
            border-radius:3px;
            margin-top:5px;
            margin-bottom:5px;
            padding:4px;
        }

        .checkbox{
            padding-top:0px !important;
        }
    </style>
End Section