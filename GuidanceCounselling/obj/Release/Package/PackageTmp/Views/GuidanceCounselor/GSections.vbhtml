@ModelType List(Of Section)
@Code
    ViewBag.Title = "Sections"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                Grade / @ViewBag.GradeId / @ViewBag.Title / List
                <a class="header-btn btn btn-default" href="@Url.Action("GGrades", New With {.id = Nothing})"><span class="glyphicon glyphicon-chevron-left"></span>Back to Grades List</a>
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
                        <th>Number of Students</th>
                        <th>Date Created</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td>@item.Name</td>
                            <td>@item.Students.Count() Students</td>
                            <td>@item.DateCreated</td>
                            <td style="text-align:right;">
                                <!--<a class="btn btn-xs btn-info" href="@Url.Action("ViewStudents", New With {.id = item.SectionId})">Manage Students</a>-->
                                <a class="btn btn-xs btn-warning" href="@Url.Action("SectionSummaryReport", New With {.id = item.SectionId})">Summary Report</a>
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
                    { "orderable": false, "targets": 3 }
                ]
            });

            $(".all-search").keyup(function () {
                datatable.search($(this).val()).draw();
            })
        });
    </script>
End Section

