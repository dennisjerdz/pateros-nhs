@ModelType IEnumerable(Of GuidanceCounselling.QuestionGroup)
@Code
    ViewBag.Title = "Question Groups"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / List
                <a class="header-btn btn btn-info" href="@Url.Action("AddQuestionGroup")"><span class="glyphicon glyphicon-plus"></span>Add</a>
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
                        <th>Display Name</th>
                        <th>Exam Type</th>
                        <th>Used At</th>
                        <th>Content</th>
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
                                @item.DisplayName
                            </td>
                            <td>
                                @item.ExamType
                            </td>
                            <td>
                                @item.ExamQuestionGroups.Count() Exams
                            </td>
                            <td>
                                @Code
                                    Dim c As Integer = 0

                                    c += item.QuestionEssays.Count()
                                    c += item.QuestionOneToFives.Count()
                                    c += item.QuestionTFLists.Count()
                                    c += item.QuestionTFLists.Count()
                                    c += item.QuestionTFRanks.Count()

                                End Code
                                @c Questions
                            </td>
                            <td>
                                @item.DateCreated
                            </td>
                            <td style = "text-align:right;" >
                                @Html.ActionLink("Edit Info", "EditQuestionGroup", New With {.id = item.QuestionGroupId}, New With {.class = "btn btn-xs btn-warning"})
                                @Html.ActionLink("Manage Questions", "ManageQuestions", New With {.id = item.QuestionGroupId}, New With {.class = "btn btn-xs btn-primary"})

                                @If item.ExamQuestionGroups.Count() = 0 And c = 0 Then
                                    @Html.ActionLink("Delete", "DeleteQuestionGroup", New With {.id = item.QuestionGroupId}, New With {.class = "btn btn-xs btn-danger"})
                                End If
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
                    { "orderable": false, "targets": 6 }
                ]
            });

            $(".all-search").keyup(function () {
                datatable.search($(this).val()).draw();
            })
        });
    </script>
End Section