﻿@ModelType LoginViewModel
@Code
    ViewBag.Title = "Log in"
    Layout = "../Shared/_LoginLayout.vbhtml"
End Code

<div class="container">
    <div style="position:absolute; z-index:9999; top:10px; right:20px;">
        <a href="@Url.Action("OTPLogin")" class="btn btn-sm btn-primary">OTP Login</a>
    </div>

    <div class="row" style="margin-top:40px;">
        <div class="col-md-4 col-md-offset-4">
            <section id="loginForm">
                @Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                    @Html.AntiForgeryToken()
                    @<text>
                        <center>
                            @code
                                Dim ws As HttpCookie = Request.Cookies("ws")

                                If ws IsNot Nothing Then
                                    Dim logoLocation As String = ws("Logo-Location").ToString()

                                    @<img Class="img-responsive" src="@logoLocation" style="margin-bottom:20px;"/>
                                Else
                                    @<img class="img-responsive" src="~/Content/logo.jpg" style="margin-bottom:20px;"/>
                                End If
                            End Code

                            
                        </center>
                        
                        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                        <div class="form-group">
                            @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .placeholder = "Email"})
                            @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
                        </div>
                        <div class="form-group">
                            @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control", .placeholder = "Password"})
                            @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
                        </div>
                        <div class="form-group">
                            <input type="submit" value="Log in" class="btn btn-info btn-block" style="margin-top:8px;" />
                            <br />
                            <a class="btn btn-sm btn-default pull-left" href="@Url.Action("Index", "Home", Nothing)"><span class="glyphicon glyphicon-chevron-left"></span> Back to Home</a>
                            <a class="btn btn-sm btn-warning pull-right" href="@Url.Action("ForgotPassword", "Account", Nothing)">Forgot Password?</a>
                        </div>
                        @* Enable this once you have account confirmation enabled for password reset functionality
                    <p>
                        @Html.ActionLink("Forgot your password?", "ForgotPassword")
                    </p>*@
                    </text>
                                End Using
            </section>
        </div>
    </div>
</div>
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
