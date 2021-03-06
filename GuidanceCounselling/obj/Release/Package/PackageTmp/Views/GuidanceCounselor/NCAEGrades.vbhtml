﻿@ModelType GuidanceCounselling.ApplicationUser
@Code
    ViewBag.Title = "NCAE Grades"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.getFullName / Grading Periods List
                <a class="header-btn btn btn-default" href="@Url.Action("Students")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
                <a class="header-btn btn btn-info" href="@Url.Action("AddNCAEGrade", New With {.id = Model.Id})"><span class="glyphicon glyphicon-plus"></span>Add</a>
            </p>
        </div>

        <div class="col-md-3">
            <input class="form-control all-search" placeholder="Search ..." />
        </div>
    </div>
</div>

<div class="container body-data">
    <div class="row">
        <div class="col-md-12">
            <table class="table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Date Created</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model.NCAEGrades
                        @<tr>
                            <td>
                                @item.Name
                            </td>
                            <td>
                                @item.DateCreated
                            </td>
                            <td style="text-align:right;">
                                @Html.ActionLink("Edit Info", "EditNCAEGrade", New With {.id = item.NCAEGradeId}, New With {.class = "btn btn-xs btn-warning"})
                                @Html.ActionLink("View Grade", "ViewNCAEGrade", New With {.id = item.NCAEGradeId}, New With {.class = "btn btn-xs btn-primary"})
                            </td>
                        </tr>
                    Next
                </tbody>
            </table>
        </div>
    </div>

</div>

@Section scripts
    <script>
        $(document).ready(function () {
            var datatable = $("table").DataTable({
                paging: true,
                "pageLength": 10,
                "dom": "<'table-responsive'rt><'window-footer'<'col-md-6'i><'col-md-6'p>>",
                "columnDefs": [
                    { "orderable": false, "targets": 2 }
                ]
            });

            $(".all-search").keyup(function () {
                datatable.search($(this).val()).draw();
            })
        });
    </script>
End Section