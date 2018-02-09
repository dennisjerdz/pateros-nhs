@ModelType IEnumerable(Of GuidanceCounselling.Announcement)
@Code
    ViewBag.Title = "Pateros NHS CCS"
End Code

<div class="container">
    <div class="row" style="margin-top:20px;"></div>

    @If (User.IsInRole("IT Admin")) Then

    Else
        @For Each a As Announcement In Model.OrderByDescending(Function(x) x.DateCreated)
            @<div class="row" style="margin-bottom:10px;">
                <div class="col-md-12">
                    <div class="announcement-border">
                        <p style="font-weight:800; font-size:1.4em; letter-spacing:-0.5px;">@a.Name <font style="float:right; font-weight:300; color:#b7b7b7;">@a.DateCreated</font></p>
                        <p style="text-align:justify; text-align-last:left;">@a.Content</p>
                    </div>
                </div>
            </div>
        Next
    End If
</div>

@Section styles
    <style>
        .announcement-border{
            border:2px solid #dcdcdc;
            border-radius:3px;
            margin-top:5px;
            margin-bottom:5px;
            padding:12px;
        }
    </style>
End Section