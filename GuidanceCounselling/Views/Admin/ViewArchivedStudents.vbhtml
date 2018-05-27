@ModelType List(Of ArchivedSectionStudents)
@Code
    ViewBag.Title = "Archived Section Students"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.SectionSectionName / @ViewBag.Title
                <a class="header-btn btn btn-default" href="@Url.Action("Sections", New With {.id = ViewBag.GradeId})"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
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
                        <th>Current Section</th>
                        <th>Date Created</th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td>@item.User.getFullName()</td>
                            <td>
                                @If item.Section.Name IsNot Nothing Then
                                    @item.Section.Name
                                Else
                                    @<span>Not Assigned</span>
                                End If
                            </td>
                            <td>@item.DateCreated</td>
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

