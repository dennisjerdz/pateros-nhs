@ModelType GuidanceCounselling.Announcement
@Code
    ViewBag.Title = "Announcements"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.AnnouncementId / Edit
                <a class="header-btn btn btn-default" href="@Url.Action("Announcements")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container body-data">
    @Using Html.BeginForm("EditAnnouncement", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(m) m.AnnouncementId, New With {.Value = Model.AnnouncementId})
        @Html.HiddenFor(Function(m) m.Active, New With {.Value = Model.Active})
        @Html.HiddenFor(Function(m) m.DateCreated, New With {.Value = Model.DateCreated})

        @<text>
            @Html.ValidationSummary("", New With {.class = "text-danger"})

            <div class="row">
                <div class="col-md-6">
                    @Html.LabelFor(Function(m) m.Name, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>

                <div class="col-md-6">
                    @Html.LabelFor(Function(m) m.DateExpired, New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.DateExpired, New With {.htmlAttributes = New With {.class = "form-control", .type = "datetime-local"}})
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    @Html.LabelFor(Function(m) m.Content, New With {.class = "control-label"})
                    @Html.TextAreaFor(Function(model) model.Content, New With {.class = "form-control"})
                </div>
            </div>

            <div class="row">
                <div class="col-md-4 col-md-offset-8">
                    <label style="margin-bottom:0px;">&nbsp;</label>
                    <input type="submit" class="btn btn-primary btn-block" value="Save Changes" />
                </div>
            </div>
        </text> End Using
</div>