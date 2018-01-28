@ModelType GuidanceCounselling.ExamStudent
@Code
    ViewBag.Title = "Exams"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / Assignment / @Model.ExamStudentId / Edit
                <a class="header-btn btn btn-default" href="@Url.Action("ExamAssignments", New With {.id = Model.ExamId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditAssignment", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.UserId, New With {.Value = Model.UserId})
        @Html.HiddenFor(Function(m) m.ExamId, New With {.Value = Model.ExamId})
        @Html.HiddenFor(Function(m) m.ExamStudentId, New With {.Value = Model.ExamStudentId})
        @Html.HiddenFor(Function(m) m.DateCreated, New With {.Value = Model.DateCreated})
        @Html.HiddenFor(Function(m) m.TakenAt, New With {.Value = Model.TakenAt})

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            
            <div class="row">
                <div class="col-md-4">
                    <label class="control-label">Student Name</label>
                    <input class="form-control" value="@Model.User.getFullName" readonly/>
                </div>

                <div class="col-md-4">
                    <label class="control-label">Exam Name</label>
                    <input class="form-control" value="@Model.Exam.Name" readonly/>
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.DateCreated, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.DateCreated, New With {.htmlAttributes = New With {.class = "form-control", .readonly = True}})
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.AvailabilityStart, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.AvailabilityStart, New With {.htmlAttributes = New With {.class = "form-control", .type = "datetime-local"}})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.AvailabilityEnd, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.AvailabilityEnd, New With {.htmlAttributes = New With {.class = "form-control", .type = "datetime-local"}})
                </div>

                <div class="col-md-4">
                    <label style="margin-bottom:8px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Save Changes" />
                </div>
            </div>
        </text> End Using
</div>