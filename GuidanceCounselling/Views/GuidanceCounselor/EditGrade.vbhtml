@ModelType GuidanceCounselling.StudentGrade
@Code
    ViewBag.Title = "Student Grade"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.StudentGradeId / Edit
                <a class="header-btn btn btn-default" href="@Url.Action("Grades", New With {.id = Model.UserId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditGrade", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.UserId, New With {.Value = Model.UserId})
        @Html.HiddenFor(Function(m) m.DateCreated, New With {.Value = Model.DateCreated})
        @Html.HiddenFor(Function(m) m.StudentGradeId, New With {.Value = Model.StudentGradeId})

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})

            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Name, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "Grading Period"}})
                </div>

                <div class="col-md-4"></div>

                <div class="col-md-4">
                    <label style="margin-bottom:0px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Save Edits" />
                </div>
            </div>
        </text> End Using
</div>