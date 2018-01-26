@ModelType GuidanceCounselling.QuestionTFRank
@Code
    ViewBag.Title = "Question Group"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @ViewBag.QuestionGroupId / Rank 1 to 5 Questions / Add
                <a class="header-btn btn btn-default" href="@Url.Action("ManageQuestions", New With {.id = ViewBag.QuestionGroupId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("AddOneToFive", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.QuestionGroupId, New With {.Value = ViewBag.QuestionGroupId})

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})

            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Question, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Question, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>

                <div class="col-md-4"></div>

                <div class="col-md-4">
                    <label style="margin-bottom:0px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Add Question" />
                </div>
            </div>
        </text> End Using
</div>