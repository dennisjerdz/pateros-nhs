<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    <link href="~/Content/sidebar.css" rel="stylesheet" />
    <link href="~/Content/datatable/dataTables.bootstrap.min.css" rel="stylesheet" />

    @RenderSection("styles", required:=False)
    
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro" rel="stylesheet">

    @code
        Dim ws As HttpCookie = Request.Cookies("ws")

        If ws IsNot Nothing Then
            Dim sidebarColor As String = ws("Sidebar-Color").ToString()
            Dim sidebarTextColor As String = ws("Sidebar-Text-color").ToString()
            Dim navTextColor As String = ws("Nav-Text-Color").ToString()
            Dim dataBodyColor As String = ws("Data-Body-Color").ToString()
            Dim logoLocation As String = ws("Logo-Location").ToString()

            @<text>
                <style>
                    #sidebar-wrapper{
                        background: @sidebarColor !important;
                    }

                    .sidebar-nav li a{
                        color: @sidebarTextColor !important;
                    }

                    .navbar-nav>li>a, .navbar-brand{
                        color: @navTextColor !important;
                    }

                    .body-data{
                        background: @dataBodyColor !important;
                    }
                </style>
            </text>
        End If
    End Code
</head>
<body>

    <div id="wrapper" class="toggled">
        <!-- Sidebar -->
        <div id="sidebar-wrapper">
            <ul class="sidebar-nav">
                <!--
                <li class="sidebar-brand">
                    <a href="#">
                        Start Bootstrap
                    </a>
                </li>
                -->
                @If User.IsInRole("Admin") Then
                    @<text>
                        <li>
                            <a href="@Url.Action("Students", "GuidanceCounselor", New With {.id = Nothing})">Students</a>
                        </li>
                        <li>
                            <a href="@Url.Action("LoginHistory", "Admin", New With {.id = Nothing})">Login History</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Conversations", "Admin", New With {.id = Nothing})">Conversations</a>
                        </li>
                        <li>
                            <a href="@Url.Action("WebsiteConfig", "Admin", New With {.id = Nothing})">Website UI Settings</a>
                        </li>
                    </text>
                End If

                @If User.IsInRole("IT Admin") Then
                    @<text>
                        <li>
                            <a href="@Url.Action("Accounts", "Admin", New With {.id = Nothing})">Accounts</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Families", "Admin", New With {.id = Nothing})">Families</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Conversations", "Admin", New With {.id = Nothing})">Conversations</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Grades", "Admin", New With {.id = Nothing})">Grades & Sections</a>
                        </li>
                    </text>
                End If

                @If User.IsInRole("Guidance Counselor") Then
                    @<text>
                        <li>
                            <a href="@Url.Action("Exams", "GuidanceCounselor", New With {.id = Nothing})">Exams</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Students", "GuidanceCounselor", New With {.id = Nothing})">Students</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Accounts", "GuidanceCounselor", New With {.id = Nothing})">Accounts</a>
                        </li>
                        <li>
                            <a href="@Url.Action("QuestionGroups", "GuidanceCounselor", New With {.id = Nothing})">Question Groups</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Conversations", "GuidanceCounselor", New With {.id = Nothing})">Conversations</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Announcements", "GuidanceCounselor", New With {.id = Nothing})">Announcements</a>
                        </li>
                        <li>
                            <a href="@Url.Action("EditAccount", "GuidanceCounselor", New With {.id = Nothing})">Edit Account</a>
                        </li>
                    </text>
                End If

                @If User.IsInRole("Student") Then
                    @<text>
                        <li>
                            <a href="@Url.Action("AssignedExams", "Student", New With {.id = Nothing})">Assigned Exams</a>
                        </li>
                        <li>
                            <a href="@Url.Action("MyGrades", "Student", New With {.id = Nothing})">My Grades</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Conversations", "Student", New With {.id = Nothing})">Conversations</a>
                        </li>
                        <li>
                            <a href="@Url.Action("EditAccount", "Student", New With {.id = Nothing})">Edit Account</a>
                        </li>
                    </text>
                End If

                @If User.IsInRole("Family Member") Then
                    @<text>
                        <li>
                            <a href="@Url.Action("Students", "FamilyMember", New With {.id = Nothing})">Students</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Conversations", "FamilyMember", New With {.id = Nothing})">Conversations</a>
                        </li>
                        <li>
                            <a href="@Url.Action("EditAccount", "FamilyMember", New With {.id = Nothing})">Edit Account</a>
                        </li>
                    </text>
                End If
                <li> <a Class="menu-toggle" href="#">Hide Sidebar</a></li>
            </ul>
        </div>

        <div id = "page-content-wrapper" >
            <div class="navbar">
                        <div class="container">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                                    <span class="glyphicon glyphicon-align-justify"></span>
                                </button>
                        @Html.ActionLink("Career Counselling System", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <li>@Html.ActionLink("Home", "Index", "Home")</li>
                            <li><a class="menu-toggle" href="#">Sidebar toggle</a></li>
                        </ul>
                        @Html.Partial("_LoginPartial")
                    </div>
                </div>
            </div>

            <div class="body-content">
                @RenderBody()
                <div class="container footer-container">
                    <footer>
                        <center>
                            <p>&copy; @DateTime.Now.Year - Pateros National High School Career Counselling System</p>
                        </center>
                    </footer>
                </div>
            </div>
        </div>

    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")

    <script src="~/Scripts/datatable/datatables.js"></script>
    <script src="~/Scripts/datatable/dataTables.bootstrap.js"></script>

    @RenderSection("scripts", required:=False)

    <!-- Menu Toggle Script -->
    <script>
    $(".menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
    </script>
</body>
</html>
