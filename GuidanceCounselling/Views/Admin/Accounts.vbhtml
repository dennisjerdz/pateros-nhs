@ModelType List(Of AccountsViewModel)
@Code
    ViewBag.Title = "Accounts"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>@ViewBag.Title / List 
            <a class="header-btn btn btn-info" href="@Url.Action("AddAccount")"><span class="glyphicon glyphicon-plus"></span>Add</a></p>
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
                        <th>Email</th>
                        <th>Role</th>
                        <th>Status</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td>@item.Name</td>
                            <td>@item.Email</td>
                            <td>@item.Role</td>
                            <td>
                                @Code
                                    If item.IsDisabled = True Then
                                        @<font style="color:#da8e8e">Disabled</font>
                                    Else
                                        @<font style="color:#8eda8e">Active</font>
                                    End If
                                End Code
                            </td>
                            <td style="text-align:right;">
                                @If item.IsDisabled = True Then
                                    @<a class="btn btn-xs btn-success" href="@Url.Action("ChangeStatus", New With {.id = item.UserId})">Activate</a>
                                Else
                                    @<a class="btn btn-xs btn-danger" href="@Url.Action("ChangeStatus", New With {.id = item.UserId})">Disable</a>
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
                    { "orderable": false, "targets": 4 }
                ]
            });

            $(".all-search").keyup(function () {
                datatable.search($(this).val()).draw();
            })
        });
    </script>
End Section

