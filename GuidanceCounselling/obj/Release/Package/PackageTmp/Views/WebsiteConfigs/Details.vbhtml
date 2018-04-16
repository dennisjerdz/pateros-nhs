@ModelType GuidanceCounselling.WebsiteConfig
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>WebsiteConfig</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Value)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Value)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.WebSiteConfigId }) |
    @Html.ActionLink("Back to List", "Index")
</p>
