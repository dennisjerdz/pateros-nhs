@ModelType Grade
@Code
    ViewBag.Title = "Grades"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / Add
                <a class="header-btn btn btn-default" href="@Url.Action("Grades")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("GradesAdd", "Admin", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Name, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.Name, New With {.class = "form-control"})
                </div>

                <div class="col-md-4 col-md-offset-4">
                    <label style="margin-bottom:5px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Add Grade" />
                </div>
            </div>
        </text> End Using
</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
