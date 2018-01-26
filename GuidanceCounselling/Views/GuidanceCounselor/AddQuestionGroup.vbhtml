@ModelType GuidanceCounselling.QuestionGroup
@Code
    ViewBag.Title = "Question Groups"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / Add Question Group
                <a class="header-btn btn btn-default" href="@Url.Action("QuestionGroups")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("AddQuestionGroup", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()

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
                <div class="col-md-8"></div>
                <div class="col-md-4">
                    <label style="margin-bottom:0px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Add Question Group" />
                </div>
            </div>
        </text> End Using
</div>