@ModelType List(Of WebsiteConfig)
@Code
    ViewBag.Title = "Website Config"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
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
                        <th>Value</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td>@item.Name</td>
                            <td>@item.Value</td>
                            <td style="text-align:right;"><a class="btn btn-xs btn-primary" href="@Url.Action("EditWebsiteConfig", New With {.id = item.WebSiteConfigId})">Edit</a></td>
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
