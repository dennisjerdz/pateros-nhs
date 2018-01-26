@ModelType GuidanceCounselling.Exam
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Exam</h4>
    <hr />
    <dl class="dl-horizontal">
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
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.ExamId }) |
    @Html.ActionLink("Back to List", "Index")
</p>
