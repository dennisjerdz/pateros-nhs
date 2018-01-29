@ModelType GuidanceCounselling.StudentGrade
@Code
    ViewBag.Title = "Student Grades"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.User.getFullName
                <a class="header-btn btn btn-default" href="@Url.Action("Grades", New With {.id = Model.UserId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
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
                        <th>Subject</th>
                        <th>Grade</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model.SubjectGrades
                        @<tr>
                            <td>
                                @item.Subject
                            </td>
                            <td>
                                @item.Grade
                            </td>
                            <td style="text-align:right;">
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