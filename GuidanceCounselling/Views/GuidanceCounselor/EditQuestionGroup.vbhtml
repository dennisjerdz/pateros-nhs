@ModelType GuidanceCounselling.QuestionGroup
@Code
    ViewBag.Title = "Question Groups"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / Edit Question Group
                <a class="header-btn btn btn-default" href="@Url.Action("QuestionGroups")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditQuestionGroup", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.QuestionGroupId)
        @Html.HiddenFor(Function(m) m.DateCreated)

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Name, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.DisplayName, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.DisplayName, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.ExamType, New With {.class = "control-label"})
                    @Html.EnumDropDownListFor(Function(model) model.ExamType, htmlAttributes:=New With {.class = "form-control"})
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.DateCreated, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.DateCreated, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "True"}})
                </div>
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <label style="margin-bottom:8px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Save Question Group" />
                </div>
            </div>
        </text> End Using
</div>