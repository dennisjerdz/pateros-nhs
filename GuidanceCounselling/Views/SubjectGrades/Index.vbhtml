@ModelType IEnumerable(Of GuidanceCounselling.SubjectGrade)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.StudentGrade.UserId)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Subject)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Grade)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DateCreated)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.StudentGrade.UserId)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Subject)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Grade)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.DateCreated)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.SubjectGradeId }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.SubjectGradeId }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.SubjectGradeId })
        </td>
    </tr>
Next

</table>
