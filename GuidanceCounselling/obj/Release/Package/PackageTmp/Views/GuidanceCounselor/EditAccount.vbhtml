@ModelType UserEditModelL
@Code
    ViewBag.Title = "Accounts"

    Dim g = New List(Of SelectListItem)()
    g.Add(New SelectListItem With {.Text = "Female", .Value = "0"})
    g.Add(New SelectListItem With {.Text = "Male", .Value = "1"})
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / Edit Account
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditAccount", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.FirstName, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.FirstName, New With {.class = "form-control"})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.MiddleName, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.MiddleName, New With {.class = "form-control"})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.LastName, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.LastName, New With {.class = "form-control"})
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Email, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .readonly = True})
                </div>

                <div class="col-md-4">
                    <label class="control-label">Gender</label>
                    @Html.DropDownList("Gender", g, New With {.Class = "form-control"})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.BirthDate, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.BirthDate, New With {.class = "form-control", .type = "date"})
                </div>
            </div>

            <div class="row">
                <div class="col-md-4 col-md-offset-8">
                    <label style="margin-bottom:5px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Save Edits" />
                </div>
            </div>
        </text> End Using
</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
