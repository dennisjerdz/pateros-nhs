@ModelType IEnumerable(Of GuidanceCounselling.QuestionEssay)
@Code
    ViewBag.Title = "Question Group"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @ViewBag.Id / Essay Questions
                <a class="header-btn btn btn-default" href="@Url.Action("QuestionGroups")"><span class="glyphicon glyphicon-chevron-left"></span>Back</a>
                <a class="header-btn btn btn-info" href="@Url.Action("AddEssay", New With {.id = ViewBag.Id})"><span class="glyphicon glyphicon-plus"></span>Add</a>
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
                        <th>Question</th>
                        <th>Date Created</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td>
                                @item.Question
                            </td>
                            <td>
                                @item.DateCreated
                            </td>
                            <td style="text-align:right;">
                                @Html.ActionLink("Edit Info", "EditEssay", New With {.id = item.QuestionEssayId}, New With {.class = "btn btn-xs btn-warning"})
                                @Html.ActionLink("Delete", "DeleteEssay", New With {.id = item.QuestionEssayId}, New With {.class = "btn btn-xs btn-danger"})
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