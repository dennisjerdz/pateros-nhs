@ModelType WebsiteConfig
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
    @Using Html.BeginForm("EditWebsiteConfig", "Admin", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()

        @Html.HiddenFor(Function(m) m.WebSiteConfigId)

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Name, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.Name, New With {.class = "form-control", .readonly = "true"})
                </div>

                @If Model.Name.Contains("Color") Then
                    @<div Class="col-md-4">
                        @Html.LabelFor(Function(m) m.Value, New With {.class = "control-label"})
                        @Html.TextBoxFor(Function(m) m.Value, New With {.class = "form-control", .type = "color"})
                    </div>
                Else
                    @<div Class="col-md-4">
                        @Html.LabelFor(Function(m) m.Value, New With {.class = "control-label"})
                        @Html.TextBoxFor(Function(m) m.Value, New With {.class = "form-control"})
                    </div>
                End If

                <div class="col-md-4">
                    <label style="margin-bottom:5px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Save" />
                </div>
            </div>
        </text> End Using
</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
