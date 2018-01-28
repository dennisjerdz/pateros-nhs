@ModelType GuidanceCounselling.ExamStudent
@Code
    ViewBag.Title = "Take Exam"
End Code

<div class="container">
    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <br />
            <a class="btn btn-xs btn-default" href="@Url.Action("AssignedExams")"><span class="glyphicon glyphicon-chevron-left"></span> Back</a>
            <br />
            <br />
            <div class="body-data">
                <br />
                <p>Take Exam?</p>
                <a class="btn btn-block btn-info" href="@Url.Action("TakeExamConfirmed", New With {.id = Model.ExamStudentId})">Confirm & Proceed to Exam</a>
                <br />
            </div>
        </div>
    </div>
</div>