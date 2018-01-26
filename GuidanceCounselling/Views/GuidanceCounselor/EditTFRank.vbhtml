@ModelType GuidanceCounselling.QuestionTFRank
@Code
    ViewBag.Title = "Question Group"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.QuestionGroupId / True or False (Ranked Results) Questions / @Model.QuestionTFRankId / Edit
                <a class="header-btn btn btn-default" href="@Url.Action("ManageQuestions", New With {.id = Model.QuestionGroupId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditTFRank", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.QuestionTFRankId, New With {.Value = Model.QuestionTFRankId})
        @Html.HiddenFor(Function(m) m.QuestionGroupId, New With {.Value = Model.QuestionGroupId})
        @Html.HiddenFor(Function(m) m.DateCreated, New With {.Value = Model.DateCreated})

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
                    <input type="submit" class="btn btn-primary btn-block" value="Save Changes" />
                </div>
            </div>
        </text> End Using
</div>