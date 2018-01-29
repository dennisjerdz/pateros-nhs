@Code
    ViewData("Title") = "ExamRestriction"
End Code

<div class="container">
    <div class="row" style="margin-top:50px;">
        <div class="col-md-6 col-md-offset-3">
            <div class="error-border">
                <center>
                    <h4>Oops.</h4>
                    <p>Exam cannot be accessed; Exam availability (schedule) has passed.</p>
                </center>
            </div>

            <br />

            <a href="@Url.Action("Index", "Home")"><span class="glyphicon glyphicon-chevron-left"></span> Back to Home</a>
        </div>
    </div>
</div>

@Section styles
    <style>
        .error-border{
            border-radius:4px;
            background-color:#ff3b3b;
            padding:10px;
        }

        .error-border > *{
            color:white;
        }
    </style>
End Section

