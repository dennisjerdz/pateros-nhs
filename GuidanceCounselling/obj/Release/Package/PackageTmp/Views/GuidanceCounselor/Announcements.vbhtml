@ModelType IEnumerable(Of GuidanceCounselling.Announcement)
@Code
    ViewBag.Title = "Announcements"
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
                        <th>Name</th>
                        <th>Date Created</th>
                        <th>Expiration Date</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item As Announcement In Model.OrderByDescending(Function(a) a.DateCreated)
                        @<tr>
                            <td>
                                @item.Name
                            </td>                             
                            <td>
                                @item.DateCreated
                            </td>
                            <td>
                                @item.DateExpired
                            </td>
                            <td style="text-align:right;">
                                @Html.ActionLink("Edit", "EditAnnouncement", New With {.id = item.AnnouncementId}, New With {.class = "btn btn-xs btn-warning"})
                                @If item.Active = True Then
                                    @<!--<a class="btn btn-xs btn-danger" href="@Url.Action("ChangeStatus", New With {.id = item.AnnouncementId})">Disable</a>-->
                                Else
                                    @<!--<a class="btn btn-xs btn-success" href="@Url.Action("ChangeStatus", New With {.id = item.AnnouncementId})">Activate</a>-->
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
                    { "orderable": false, "targets": 3 }
                ]
            });

            $(".all-search").keyup(function () {
                datatable.search($(this).val()).draw();
            })
        });
    </script>
End Section