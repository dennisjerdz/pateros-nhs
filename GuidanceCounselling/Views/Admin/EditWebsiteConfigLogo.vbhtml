@Code
    ViewBag.Title = "Website Config"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / Edit
                <a class="header-btn btn btn-default" href="@Url.Action("WebsiteConfig")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditWebsiteConfigLogo", "Admin", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form", .enctype = "multipart/form-data"})
        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div class="row">
                <div class="col-md-4">
                    <label class="control-label">Name</label>
                    <input class="form-control "value="Logo" readonly />
                </div>

                <div Class="col-md-4">
                    <label>Upload File (JPG/PNG)</label>
                    <input type="file" name="postedFile" class="form-control" required>
                </div>

                <div class="col-md-4">
                    <label style="margin-bottom:5px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Upload" />
                </div>
            </div>
        </text>
    End Using
</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
