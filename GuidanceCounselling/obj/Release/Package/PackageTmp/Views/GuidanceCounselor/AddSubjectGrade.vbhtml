@ModelType GuidanceCounselling.SubjectGrade
@Code
    ViewBag.Title = "Student Grade"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.StudentGradeId / Subjects / Add
                <a class="header-btn btn btn-default" href="@Url.Action("ViewGrades", New With {.id = Model.StudentGradeId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("AddSubjectGrade", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.StudentGradeId, New With {.Value = Model.StudentGradeId})

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})

            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Subject, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Subject, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Grade, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Grade, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>

                <div class="col-md-4">
                    <label style="margin-bottom:8px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Add" />
                </div>
            </div>
        </text> End Using
</div>