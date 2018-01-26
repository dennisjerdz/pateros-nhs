@ModelType RegisterViewModel
@Code
    ViewBag.Title = "Accounts"

    Dim r = New List(Of SelectListItem)()
    For Each item In ViewBag.Roles
        r.Add(New SelectListItem With {.Text = item.Name, .Value = item.Name})
    Next
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>@ViewBag.Title / Add Account 
            <a class="header-btn btn btn-default" href="@Url.Action("Accounts")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a></p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("AddAccount", "Admin", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

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
                    @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
                </div>

                <div class="col-md-4">
                    <label class="control-label">Role</label>
                    @Html.DropDownList("Role", r, New With {.Class = "form-control"})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.BirthDate, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.BirthDate, New With {.class = "form-control", .type = "date"})
                </div>
            </div>
        
            <div class="row">
                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.Password, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.Password, New With {.class = "form-control", .type = "password"})
                </div>

                <div class="col-md-4">
                    @Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "control-label"})
                    @Html.TextBoxFor(Function(m) m.ConfirmPassword, New With {.class = "form-control", .type = "password"})
                </div>

                <div class="col-md-4">
                    <label style="margin-bottom:5px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Add Account" />
                </div>
            </div>      
        </text> End Using
</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
