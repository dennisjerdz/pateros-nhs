@ModelType GuidanceCounselling.Exam
@Code
    ViewBag.Title = "Exams"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.ExamId (@Model.Name) / Assignments
                <a class="header-btn btn btn-info" href="@Url.Action("Exams")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
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
                        <th>Student Name</th>
                        <th>Availability Date</th>
                        <th>Taken At</th>
                        <th>Date Created</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model.ExamStudents
                        @<tr>
                            <td>
                                @item.User.getFullName
                            </td>
                            <td>
                                @item.AvailabilityStart to @item.AvailabilityEnd
                            </td>
                            <td>@item.TakenAt</td>
                            <td>
                                @item.DateCreated
                            </td>
                            <td style="text-align:right;">
                                @Html.ActionLink("Edit Assignment", "EditAssignment", New With {.id = item.ExamStudentId}, New With {.class = "btn btn-xs btn-warning"})
                                @Html.ActionLink("Delete Assignment", "DeleteAssignment", New With {.id = item.ExamStudentId}, New With {.class = "btn btn-xs btn-danger"})
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
                    { "orderable": false, "targets": 4 }
                ]
            });

            $(".all-search").keyup(function () {
                datatable.search($(this).val()).draw();
            })
        });
    </script>
End Section