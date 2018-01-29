@ModelType IEnumerable(Of GuidanceCounselling.StudentGrade)
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
            @Html.DisplayNameFor(Function(model) model.User.FirstName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DateCreated)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.User.FirstName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.DateCreated)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.StudentGradeId }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.StudentGradeId }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.StudentGradeId })
        </td>
    </tr>
Next

</table>
