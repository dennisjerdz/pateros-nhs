@ModelType IEnumerable(Of GuidanceCounselling.Exam)
@Code
    ViewBag.Title = "Exams"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / List
                <a class="header-btn btn btn-info" href="@Url.Action("AddExam")"><span class="glyphicon glyphicon-plus"></span>Add</a>
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
                        <th># of Question Groups</th>
                        <th>Date Created</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td>
                                @item.Name
                            </td>
                            <td>
                                @item.ExamQuestionGroups.Count()
                            </td>
                            <td>
                                @item.DateCreated
                            </td>
                            <td style="text-align:right;">
                                @Html.ActionLink("Edit Info", "EditExam", New With {.id = item.ExamId}, New With {.class = "btn btn-xs btn-warning"})
                                @Html.ActionLink("Exam Assignments", "ExamAssignments", New With {.id = item.ExamId}, New With {.class = "btn btn-xs btn-default"})
                                @Html.ActionLink("Exam Setup", "SetupExam", New With {.id = item.ExamId}, New With {.class = "btn btn-xs btn-primary"})
                                @Html.ActionLink("Assign Exam", "AssignExam", New With {.id = item.ExamId}, New With {.class = "btn btn-xs btn-info"})
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