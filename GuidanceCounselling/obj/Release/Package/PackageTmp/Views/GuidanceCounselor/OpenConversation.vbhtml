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
                <div class="row">
                    <div class="col-md-6">
                        <div style="background-color:#bababa; font-size:1.2em; padding:12px; border-radius:4px; text-align:justify; text-align-last:left; color:white; display:inline-block;">
                            What is Lorem Ipsum?
                        </div>
                        <div style="margin-bottom:10px; color:#cacaca;">02-19-2018 3 PM</div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-md-offset-6">
                        <div style="background-color:#208bff; font-size:1.2em; padding:16px; border-radius:4px; text-align:justify; text-align-last:right; color:white; display:inline-block; float:right;">
                            It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.
                        </div>
                        <div style="margin-bottom:10px; color:#cacaca; float:right;">02-19-2018 4 PM</div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div style="background-color:#bababa; font-size:1.2em; padding:16px; border-radius:4px; text-align:justify; text-align-last:left; color:white; display:inline-block;">
                            Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.
                        </div>
                        <div style="margin-bottom:10px; color:#cacaca;">02-19-2018 4 PM</div>
                    </div>
                </div>
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
