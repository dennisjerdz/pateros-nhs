@ModelType GuidanceCounselling.StudentGrade
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>StudentGrade</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.User.FirstName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.User.FirstName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
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
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.StudentGradeId }) |
    @Html.ActionLink("Back to List", "Index")
</p>
