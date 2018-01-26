﻿<!DOCTYPE html>
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
                @If User.IsInRole("IT Admin") Then
                    @<text>
                        <li>
                            <a href="@Url.Action("Accounts", "Admin", New With {.id = Nothing})">Accounts</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Families", "Admin", New With {.id = Nothing})">Families</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Grades", "Admin", New With {.id = Nothing})">Grades & Sections</a>
                        </li>
                    </text>
                End If

                @If User.IsInRole("Guidance Counselor") Then
                    @<text>
                        <li>
                            <a href="@Url.Action("QuestionGroups", "GuidanceCounselor", New With {.id = Nothing})">Question Groups</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Exams", "GuidanceCounselor", New With {.id = Nothing})">Exams</a>
                        </li>
                    </text>
                End If
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
                            <li><a id="menu-toggle" href="#">Sidebar toggle</a></li>
                        </ul>
                        @Html.Partial("_LoginPartial")
                    </div>
                </div>
            </div>

            <div class="body-content">
                @RenderBody()
                <div class="container footer-container">
                    <footer>
                        <p style="float:right;">&copy; @DateTime.Now.Year - Pateros National High School Career Counselling System</p>
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
    $("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
    </script>
</body>
</html>
