@ModelType IEnumerable(Of GuidanceCounselling.Conversation)
@Code
    ViewBag.Title = "Conversations"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / List
                <a class="header-btn btn btn-info" href="@Url.Action("AddAnnouncement")"><span class="glyphicon glyphicon-plus"></span>Add</a>
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
                        <th>Conversation with</th>
                        <th>Message Count</th>
                        <th>Date Created</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item As Conversation In Model.OrderByDescending(Function(a) a.Messages.OrderByDescending(Function(b) b.DateCreated))
                        @<tr>
                            <td>
                                <strong>
                                    @If User.Identity.Name = item.Receiver.Email Then
                                        @<text>@item.Sender.getFullName</text>
                                    Else
                                        @<text>@item.Receiver.getFullName</text>
                                    End If  
                                </strong>
                            </td>
                            <td>
                                @item.Messages.Count()
                            </td>
                            <td>
                                @item.DateCreated
                            </td>
                            <td style="text-align:right;">
                                @Html.ActionLink("Open Conversation", "OpenConversation", New With {.id = item.ConversationId}, New With {.class = "btn btn-xs btn-info"})
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