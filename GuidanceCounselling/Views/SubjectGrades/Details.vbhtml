@ModelType GuidanceCounselling.SubjectGrade
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>SubjectGrade</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.StudentGrade.UserId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StudentGrade.UserId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Subject)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Subject)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Grade)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Grade)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateCreated)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateCreated)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.SubjectGradeId }) |
    @Html.ActionLink("Back to List", "Index")
</p>
