@ModelType IEnumerable(Of GuidanceCounselling.ExamStudent)
@Code
    ViewBag.Title = "Exams"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / List
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
                        <th>Availability</th>
                        <th>Taken At</th>
                        <th>Date Created</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td>
                                @item.Exam.Name
                            </td>
                            <td>
                                @item.AvailabilityStart to @item.AvailabilityEnd
                            </td>
                            <td>
                                @item.TakenAt
                            </td>
                            <td>
                                @item.DateCreated
                            </td>
                            <td style="text-align:right;">

                                @If item.TakenAt IsNot Nothing Then
                                    @Html.ActionLink("View Exam Results", "ExamResults", New With {.id = item.ExamStudentId}, New With {.class = "btn btn-xs btn-warning"})
                                End If

                                @If item.TakenAt Is Nothing Then
                                    @Html.ActionLink("Take Exam", "TakeExamConfirm", New With {.id = item.ExamStudentId}, New With {.class = "btn btn-xs btn-warning"})
                                End If
                                
                                @*
                                  @Html.ActionLink("Review Results", "Review Results", New With {.id = item.ExamId}, New With {.class = "btn btn-xs btn-warning"})  
                                *@
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