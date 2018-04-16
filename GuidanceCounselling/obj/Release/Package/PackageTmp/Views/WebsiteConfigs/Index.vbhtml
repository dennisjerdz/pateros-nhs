@ModelType IEnumerable(Of GuidanceCounselling.WebsiteConfig)
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
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Value)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Value)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.WebSiteConfigId }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.WebSiteConfigId }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.WebSiteConfigId })
        </td>
    </tr>
Next

</table>
