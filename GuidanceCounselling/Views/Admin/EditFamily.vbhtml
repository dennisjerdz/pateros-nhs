@ModelType FamilyEditModel
@Code
    ViewBag.Title = "Families"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / Edit Name & Manage Members
                <a class="header-btn btn btn-default" href="@Url.Action("Families")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
            </p>
        </div>
    </div>
</div>

<div class="container two-form">
    <div class="row">
        @Using Html.BeginForm("EditFamily", "Admin", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

            @Html.AntiForgeryToken()
            @Html.HiddenFor(Function(m) m.FamilyId)

            @<text>

                <div Class="col-md-6">
                    <div class="row">
                        <div class="col-md-7">
                            <p class="instructions">Select Family Members</p>
                            @Html.ValidationSummary("", New With {.class = "text-danger"})
                        </div>
                        <div class="col-md-5">
                            <input class="all-search form-control" placeholder="Search ..." />
                        </div>
                    </div>
                    <div Class="body-data">
                        <table class="table table-hover datatable">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Role</th>
                                    <th></th>
                                </tr>
                            </thead>

                            <tbody>
                                @For Each item In Model.NonMembers
                                    @<tr>
                                        <td>@item.Name</td>
                                        <td>@item.Role</td>
                                        <td>
                                            <button type="button" class="btn btn-info btn-xs add-btn"
                                                    data-Id="@item.UserId"
                                                    data-Name="@item.Name"
                                                    data-role="@item.Role">
                                                Add
                                            </button>
                                        </td>
                                    </tr>
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>

                <div Class="col-md-6">
                    <div class="row">
                        <div class="col-md-7">
                            <p class="instructions">Current Users & Family Name</p>
                            @Html.ValidationSummary("", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div Class="body-data">
                        <table class="table table-hover selected-table">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Role</th>
                                    <th></th>
                                </tr>
                            </thead>

                            <tbody>
                                @For Each item In Model.FamilyMembers
                                    @<tr>
                                        <td>@item.Name</td>
                                        <td>@item.Role</td>
                                        <td>
                                            <button class="btn btn-xs btn-danger remove-btn"
                                                    data-Id="@item.UserId" data-Name="@item.Name" data-Role="@item.Role">
                                                Remove
                                            </button>
                                        </td>
                                    </tr>
                                Next
                            </tbody>
                        </table>

                        <div class="row">
                            <div class="col-md-7 padding-fix">
                                <label>Family Name</label>
                                @Html.TextBoxFor(Function(m) m.Name, New With {.class = "form-control"})
                            </div>

                            <div class="col-md-5 padding-fix">
                                <label style="margin-bottom:5px;">&nbsp;</label>
                                <input type="submit" class="btn btn-primary btn-block" value="Save Edits" />
                            </div>
                        </div>

                        <div class="form-inputs" style="display:none;">
                            @Code
                                Dim c As Integer = 0
                            End Code
                            @For Each item In Model.FamilyMembers
                                @<input name="FamilyMembers[@c].UserId" value="@item.UserId" />
                                c += 1
                            Next
                        </div>
                    </div>
                </div>

            </text> End Using
    </div>
</div>

@Section scripts
    <script>
        /*$(document).ready(function () {
            var datatable = $("table").DataTable({
                paging: true,
                "pageLength": 10,
                "dom": "<'table-responsive'rt><'row'<'col-md-6'i><'col-md-6'p>>",
                "columnDefs": [
                    { "orderable": false, "targets": 2 }
                ]
            });

            $(".all-search").keyup(function () {
                datatable.search($(this).val()).draw();
            })
        });*/

        $(document).ready(function () {
            var datatable = $(".datatable").DataTable({
                paging: false,
                "dom": "<'table-responsive'rt><'row'<'col-md-12 padding-fix'i>>",
                "columnDefs": [
                    { "orderable": false, "targets": 2 }
                ]
            });

            $(".all-search").keyup(function () {
                datatable.search($(this).val()).draw();
            });

            $(document).on("click", ".add-btn", function () {
                var id = $(this).data("id");
                var name = $(this).data("name");
                var role = $(this).data("role");

                $(".selected-table tbody")
                    .append("<tr>" +
                    "<td>" + name + "</td>" +
                    "<td>" + role + "</td>" +
                    "<td><button class='btn btn-xs btn-danger remove-btn' data-Id='" + id + "' data-Name='" + name + "' data-Role='" + role + "'>Remove</button></td>" +
                    "</tr>");

                var n = $(".selected-table tbody tr").length - 1;
                $(".form-inputs").append("<input name='FamilyMembers[" + n + "].UserId' value='" + id + "' />");

                datatable.row($(this).parent().parent()).remove().draw();
            });

            $(document).on("click", ".remove-btn", function () {
                var id = $(this).data("id");
                var name = $(this).data("name");
                var role = $(this).data("role");

                /*$(".datatable tbody")
                    .append("<tr>" +
                    "<td>" + name + "</td>" +
                    "<td>" + role + "</td>" +
                    "<td style='text-align:right;'><button class='btn btn-xs btn-danger remove-btn' data-Id='" + id + "' data-Name='" + name + "' data-Role='" + role + "'>Remove</button></td>" +
                    "</tr>");*/

                datatable.row.add([name, role, "<button class='btn btn-xs btn-info add-btn' data-Id='" + id + "' data-Name='" + name + "' data-Role='" + role + "'>Add</button>"]).draw().node();

                $(".form-inputs input").each(function () {
                    if ($(this).attr("value") == id) {
                        $(this).remove();
                    }
                });
                reorder();

                $(this).parent().parent().remove();
            });

            function reorder() {
                var count = 0;
                $(".form-inputs input").each(function () {
                    $(this).attr("name", "FamilyMembers["+count+"].UserId");
                });
            }
        });
    </script>
End Section