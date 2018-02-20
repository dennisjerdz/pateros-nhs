@ModelType Message
@Code
    ViewBag.Title = "Conversation"
End Code

<div class="container body-header">
    <div class="row">
        <div class="col-md-9">
            <p>
                @ViewBag.Title / @Model.Conversation.Receiver.FirstName & @Model.Conversation.Sender.FirstName
                <a class="header-btn btn btn-default" href="@Url.Action("Conversations")"><span class="glyphicon glyphicon-chevron-left"></span>Conversations</a>
            </p>
        </div>

        <div class="col-md-3">
            <input class="form-control all-search" placeholder="Search ..." />
        </div>
    </div>
</div>

<div class="container">
    <div class="row">
        <div class="col-md-12">
            @Using Html.BeginForm("OpenConversation", "GuidanceCounselor", FormMethod.Post, New With {.class = "form-horizontal form-bottom-space", .role = "form"})

                @Html.AntiForgeryToken()
                @Html.HiddenFor(Function(m) m.ConversationId)

                @<text>
                    @Html.ValidationSummary("", New With {.class = "text-danger"})
                    <div class="row">
                        <div class="col-md-8">
                            @Html.TextBoxFor(Function(m) m.Content, New With {.class = "form-control", .placeholder = "Input message here..."})
                        </div>

                        <div class="col-md-4">
                            <input type="submit" class="btn btn-primary btn-block" value="Send Message" />
                        </div>
                    </div>
                </text> End Using
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div style="background-color:#fafafa; padding:15px; border-radius:4px; height:600px; overflow-y:scroll;">
                @If Model.Conversation.Messages IsNot Nothing Then
                    @For Each item As Message In Model.Conversation.Messages.OrderByDescending(Function(n) n.DateCreated)
                    If item.User.Email = User.Identity.Name Then
                            @<text>
                                <div Class="row">
                                    <div Class="col-md-6 col-md-offset-6">
                                        <div style="width:100%; overflow:hidden; margin-top:10px;">
                                            <div style="background-color:#208bff; font-size:1.1em; padding:10px; border-radius:4px; text-align:justify; text-align-last:right; color:white; display:inline-block; float:right;">
                                                @item.Content
                                            </div>
                                        </div>
                                        <div style="margin-top:4px; color:#cacaca; float:right; display:block;">@item.DateCreated</div>
                                    </div>
                                </div>
                            </text>
                        Else
                            @<text>
                                <div Class="row">
                                    <div Class="col-md-6">
                                        <div style="width:100%; overflow:hidden; margin-top:10px;">
                                            <div style="background-color:#bababa; font-size:1.1em; padding:10px; border-radius:4px; text-align:justify; text-align-last:left; color:white; display:inline-block;">
                                                @item.Content
                                            </div>
                                        </div>
                                        <div style="margin-top:4px; color:#cacaca; display:block;">@item.DateCreated</div>
                                    </div>
                                </div>
                            </text>
                        End If
                    Next
                End If
            </div>
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
