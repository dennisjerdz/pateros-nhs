﻿@ModelType GuidanceCounselling.Exam
@Code
    ViewBag.Title = "Exams"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.ExamId / Edit
                <a class="header-btn btn btn-default" href="@Url.Action("Exams")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditExam", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.ExamId)
        @Html.HiddenFor(Function(m) m.DateCreated)

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})

            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Name, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>

                <div class="col-md-4"></div>

                <div class="col-md-4">
                    <label style="margin-bottom:0px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Save Edits" />
                </div>
            </div>
        </text> End Using
</div>