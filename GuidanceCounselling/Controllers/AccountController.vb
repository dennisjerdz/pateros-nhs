﻿Imports System.Globalization
Imports System.Net
Imports System.Net.Mail
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

<Authorize>
Public Class AccountController
    Inherits Controller
    Private _signInManager As ApplicationSignInManager
    Private _userManager As ApplicationUserManager

    Private db As New ApplicationDbContext

    Public Sub New()
    End Sub

    Public Sub New(appUserMan As ApplicationUserManager, signInMan As ApplicationSignInManager)
        UserManager = appUserMan
        SignInManager = signInMan
    End Sub

    Public Property SignInManager() As ApplicationSignInManager
        Get
            Return If(_signInManager, HttpContext.GetOwinContext().[Get](Of ApplicationSignInManager)())
        End Get
        Private Set
            _signInManager = Value
        End Set
    End Property

    Public Property UserManager() As ApplicationUserManager
        Get
            Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
        End Get
        Private Set
            _userManager = Value
        End Set
    End Property

    '
    ' GET: /Account/Login
    <AllowAnonymous>
    Public Function Login(returnUrl As String) As ActionResult
        If User.IsInRole("IT Admin") Or User.IsInRole("Family Member") Or User.IsInRole("Guidance Counselor") Or User.IsInRole("Student") Or User.IsInRole("Academic Adviser") Then
            Return RedirectToAction("Index", "Home", New With {.id = Nothing})
        End If

        ViewData!ReturnUrl = returnUrl

        Dim wsData As List(Of WebsiteConfig) = db.WebsiteConfig.ToList()

        Dim ws As New HttpCookie("ws")
        ws("Sidebar-Color") = wsData.FirstOrDefault(Function(w) w.Name = "Sidebar-Color").Value.ToString()
        ws("Sidebar-Text-color") = wsData.FirstOrDefault(Function(w) w.Name = "Sidebar-Text-Color").Value.ToString()
        ws("Nav-Text-Color") = wsData.FirstOrDefault(Function(w) w.Name = "Nav-Text-Color").Value.ToString()
        ws("Data-Body-Color") = wsData.FirstOrDefault(Function(w) w.Name = "Data-Body-Color").Value.ToString()
        ws("Logo-Location") = wsData.FirstOrDefault(Function(w) w.Name = "Logo-Location").Value.ToString()
        ws.Expires.AddDays(1)
        Response.Cookies.Add(ws)

        Return View()
    End Function

    '
    ' POST: /Account/Login
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Login(model As LoginViewModel, returnUrl As String) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If

        Dim User = UserManager.FindByEmailAsync(model.Email).Result

        If (User Is Nothing) Then
            ModelState.AddModelError("", "Invalid login attempt.")
            Return View(model)
        End If

        If (User.IsDisabled = True) Then
            ModelState.AddModelError("", "Account is disabled.")
            Return View(model)
        End If

        ' This doesn't count login failures towards account lockout
        ' To enable password failures to trigger account lockout, change to shouldLockout := True
        Dim result = Await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout:=False)
        Select Case result
            Case SignInStatus.Success
                Dim newLogin As LoginHistory = New LoginHistory() With {.Name = User.getFullName, .Login = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))}
                db.LoginHistory.Add(newLogin)
                db.SaveChanges()

                Return RedirectToLocal(returnUrl)
            Case SignInStatus.LockedOut
                Return View("Lockout")
            Case SignInStatus.RequiresVerification
                Return RedirectToAction("SendCode", New With {
                    .ReturnUrl = returnUrl,
                    .RememberMe = model.RememberMe
                })
            Case Else
                ModelState.AddModelError("", "Invalid login attempt.")
                Return View(model)
        End Select
    End Function

    '
    ' GET: /Account/VerifyCode
    <AllowAnonymous>
    Public Async Function VerifyCode(provider As String, returnUrl As String, rememberMe As Boolean) As Task(Of ActionResult)
        ' Require that the user has already logged in via username/password or external login
        If Not Await SignInManager.HasBeenVerifiedAsync() Then
            Return View("Error")
        End If
        Return View(New VerifyCodeViewModel() With {
            .Provider = provider,
            .ReturnUrl = returnUrl,
            .RememberMe = rememberMe
        })
    End Function

    '
    ' POST: /Account/VerifyCode
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function VerifyCode(model As VerifyCodeViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If

        ' The following code protects for brute force attacks against the two factor codes. 
        ' If a user enters incorrect codes for a specified amount of time then the user account 
        ' will be locked out for a specified amount of time. 
        ' You can configure the account lockout settings in IdentityConfig
        Dim result = Await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:=model.RememberMe, rememberBrowser:=model.RememberBrowser)
        Select Case result
            Case SignInStatus.Success
                Return RedirectToLocal(model.ReturnUrl)
            Case SignInStatus.LockedOut
                Return View("Lockout")
            Case Else
                ModelState.AddModelError("", "Invalid code.")
                Return View(model)
        End Select
    End Function

    '
    ' GET: /Account/Register
    <AllowAnonymous>
    Public Function Register() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/Register
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Register(model As RegisterViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Dim user = New ApplicationUser() With {
                .UserName = model.Email,
                .Email = model.Email
            }
            Dim result = Await UserManager.CreateAsync(user, model.Password)
            If result.Succeeded Then
                Await SignInManager.SignInAsync(user, isPersistent:=False, rememberBrowser:=False)

                ' For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                ' Send an email with this link
                ' Dim code = Await UserManager.GenerateEmailConfirmationTokenAsync(user.Id)
                ' Dim callbackUrl = Url.Action("ConfirmEmail", "Account", New With { .userId = user.Id, .code = code }, protocol := Request.Url.Scheme)
                ' Await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=""" & callbackUrl & """>here</a>")

                Return RedirectToAction("Index", "Home")
            End If
            AddErrors(result)
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    '
    ' GET: /Account/ConfirmEmail
    <AllowAnonymous>
    Public Async Function ConfirmEmail(userId As String, code As String) As Task(Of ActionResult)
        If userId Is Nothing OrElse code Is Nothing Then
            Return View("Error")
        End If
        Dim result = Await UserManager.ConfirmEmailAsync(userId, code)
        Return View(If(result.Succeeded, "ConfirmEmail", "Error"))
    End Function

    '
    ' GET: /Account/ForgotPassword
    <AllowAnonymous>
    Public Function OTPLogin() As ActionResult
        ViewBag.Success = TempData("Success")

        Return View()
    End Function

    <AllowAnonymous>
    Public Function OTPCodeLogin(Id As String) As ActionResult
        Dim otp As OTPLink = db.OTPLinks.FirstOrDefault(Function(o) o.OTPLinkId = Id)

        If otp Is Nothing Then
            Return HttpNotFound()
        Else
            If otp.Usable = False Then
                Return HttpNotFound()
            Else
                Return View(otp)
            End If
        End If
    End Function

    <AllowAnonymous>
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function OTPCodeLogin(model As OTPLink) As ActionResult

        'ViewBag.Success = TempData("Success")
        Dim original = db.OTPLinks.FirstOrDefault(Function(o) o.OTPLinkId = model.OTPLinkId)

        'Return Content(Request("original_code") + " Original:" + original.Code.ToString() + " Input:" + model.Code.ToString())

        If model.Code = original.Code Then
            original.Usable = False
            model.Code = original.Code
            db.SaveChanges()

            Dim identity = UserManager.CreateIdentity(original.User, DefaultAuthenticationTypes.ApplicationCookie)
            HttpContext.GetOwinContext().Authentication.SignIn(New AuthenticationProperties With {.IsPersistent = False}, identity)

            ' Await SignInManager.SignInAsync(find.User, isPersistent:=False, rememberBrowser:=False)
            Return RedirectToAction("Index", "Home")
        Else
            ViewBag.Success = "0"

            Return View(original)
        End If
    End Function

    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function OTPLogin(model As ForgotPasswordViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            'dim user = await usermanager.findbynameasync(model.email)

            'if user is nothing then
            '    ' don't reveal that the user does not exist or is not confirmed
            '    return view("otplogin")
            'end if

            'dim newcode as otplink = new otplink() with {.availabilityend = datetimeoffset.now.tooffset(new timespan(8, 0, 0)).addhours(1), .otplinkid = guid.newguid().tostring(), .userid = user.id}
            'db.otplinks.add(newcode)
            'db.savechanges()

            'dim callbackurl = url.action("otploginproc", "account", new with {.id = newcode.otplinkid.tostring()}, protocol:=request.url.scheme)

            'dim fromaddress as new mailaddress("testintingone@gmail.com", "pateros-nhs no reply")
            'dim toaddress as new mailaddress(model.email, user.getfullname)
            'dim frompassword as string = "testinting"
            'dim subject as string = "otp login pateros-nhs"

            'dim smtp as new smtpclient() with {
            '    .host = "smtp.gmail.com",
            '    .port = 587,
            '    .enablessl = true,
            '    .deliverymethod = smtpdeliverymethod.network,
            '    .usedefaultcredentials = false,
            '    .credentials = new networkcredential(fromaddress.address, frompassword)
            '}

            'dim message as new mailmessage(fromaddress, toaddress) with {
            '    .subject = subject,
            '    .body = "please click the link to login; " & callbackurl & " . the link will expire in 1 hour."
            '}

            'smtp.send(message)
            'tempdata("success") = "1"
            'Return RedirectToAction("otplogin", "account")

            Dim user = Await UserManager.FindByNameAsync(model.Email)

            If user Is Nothing Then
                ' don't reveal that the user does not exist or is not confirmed
                Return View("otplogin")
            End If

            Dim rnd As Random = New Random()
            Dim code As Integer = rnd.Next(10000, 99999)

            Dim newcode As OTPLink = New OTPLink() With {.AvailabilityEnd = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0)).AddHours(1), .OTPLinkId = Guid.NewGuid().ToString(), .UserId = user.Id, .Code = code, .Usable = True}
            db.OTPLinks.Add(newcode)
            db.SaveChanges()

            Dim callbackurl = Url.Action("otpcodelogin", "account", New With {.id = newcode.OTPLinkId.ToString()}, protocol:=Request.Url.Scheme)

            Dim fromaddress As New MailAddress("testintingone@gmail.com", "Pateros-NHS No Reply")
            Dim toaddress As New MailAddress(model.Email, user.getFullName)
            Dim frompassword As String = "Testinting"
            Dim subject As String = "otp login pateros-nhs"

            Dim smtp As New SmtpClient() With {
                .Host = "smtp.gmail.com",
                .Port = 587,
                .EnableSsl = True,
                .DeliveryMethod = SmtpDeliveryMethod.Network,
                .UseDefaultCredentials = False,
                .Credentials = New NetworkCredential(fromaddress.Address, frompassword)
            }

            Dim message As New MailMessage(fromaddress, toaddress) With {
                .Subject = subject,
                .Body = "Your login code is " & code & "."
            }

            smtp.Send(message)
            Return RedirectToAction("otpcodelogin", "account", New With {.Id = newcode.OTPLinkId})
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    ' Link login
    <AllowAnonymous>
    Public Async Function OTPLoginProc(Id As String) As Task(Of ActionResult)
        Dim find As OTPLink = db.OTPLinks.FirstOrDefault(Function(o) o.OTPLinkId = Id)

        If find Is Nothing Then
            Return HttpNotFound()
        End If

        If DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0)) > find.AvailabilityEnd Then
            Return HttpNotFound()
        End If

        Dim identity = UserManager.CreateIdentity(find.User, DefaultAuthenticationTypes.ApplicationCookie)
        HttpContext.GetOwinContext().Authentication.SignIn(New AuthenticationProperties With {.IsPersistent = False}, identity)

        ' Await SignInManager.SignInAsync(find.User, isPersistent:=False, rememberBrowser:=False)
        Return RedirectToAction("Index", "Home")
    End Function

    '
    ' GET: /Account/ForgotPassword
    <AllowAnonymous>
    Public Function ForgotPassword() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/ForgotPassword
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ForgotPassword(model As ForgotPasswordViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Dim user = Await UserManager.FindByNameAsync(model.Email)
            'If user Is Nothing OrElse Not (Await UserManager.IsEmailConfirmedAsync(user.Id)) Then
            ' Don't reveal that the user does not exist or is not confirmed
            'Return View("ForgotPasswordConfirmation")
            'End If

            If user Is Nothing Then
                ' Don't reveal that the user does not exist or is not confirmed
                Return View("ForgotPasswordConfirmation")
            End If

            ' For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            ' Send an email with this link
            Dim code = Await UserManager.GeneratePasswordResetTokenAsync(user.Id)
            Dim callbackUrl = Url.Action("ResetPassword", "Account", New With {.userId = user.Id, .code = code}, protocol:=Request.Url.Scheme)

            'Await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=""" & callbackUrl & """>here</a>")
            Dim fromAddress As New MailAddress("testintingone@gmail.com", "Pateros-NHS No Reply")
            Dim toAddress As New MailAddress(model.Email, user.getFullName)
            Dim fromPassword As String = "Testinting"
            Dim subject As String = "Reset Password Pateros-NHS"

            Dim smtp As New SmtpClient() With {
                .Host = "smtp.gmail.com",
                .Port = 587,
                .EnableSsl = True,
                .DeliveryMethod = SmtpDeliveryMethod.Network,
                .UseDefaultCredentials = False,
                .Credentials = New NetworkCredential(fromAddress.Address, fromPassword)
            }

            Dim message As New MailMessage(fromAddress, toAddress) With {
                .Subject = subject,
                .Body = "Please reset your password by clicking the link; " & callbackUrl
            }

            smtp.Send(message)
            Return RedirectToAction("ForgotPasswordConfirmation", "Account")
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    '
    ' GET: /Account/ForgotPasswordConfirmation
    <AllowAnonymous>
    Public Function ForgotPasswordConfirmation() As ActionResult
        Return View()
    End Function

    '
    ' GET: /Account/ResetPassword
    <AllowAnonymous>
    Public Function ResetPassword(code As String) As ActionResult
        Return If(code Is Nothing, View("Error"), View())
    End Function

    '
    ' POST: /Account/ResetPassword
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ResetPassword(model As ResetPasswordViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If
        Dim user = Await UserManager.FindByNameAsync(model.Email)
        If user Is Nothing Then
            ' Don't reveal that the user does not exist
            Return RedirectToAction("ResetPasswordConfirmation", "Account")
        End If
        Dim result = Await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password)
        If result.Succeeded Then
            Return RedirectToAction("ResetPasswordConfirmation", "Account")
        End If
        AddErrors(result)
        Return View()
    End Function

    '
    ' GET: /Account/ResetPasswordConfirmation
    <AllowAnonymous>
    Public Function ResetPasswordConfirmation() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/ExternalLogin
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Function ExternalLogin(provider As String, returnUrl As String) As ActionResult
        ' Request a redirect to the external login provider
        Return New ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", New With {
            .ReturnUrl = returnUrl
        }))
    End Function

    '
    ' GET: /Account/SendCode
    <AllowAnonymous>
    Public Async Function SendCode(returnUrl As String, rememberMe As Boolean) As Task(Of ActionResult)
        Dim userId = Await SignInManager.GetVerifiedUserIdAsync()
        If userId Is Nothing Then
            Return View("Error")
        End If
        Dim userFactors = Await UserManager.GetValidTwoFactorProvidersAsync(userId)
        Dim factorOptions = userFactors.[Select](Function(purpose) New SelectListItem() With {
            .Text = purpose,
            .Value = purpose
        }).ToList()
        Return View(New SendCodeViewModel() With {
            .Providers = factorOptions,
            .ReturnUrl = returnUrl,
            .RememberMe = rememberMe
        })
    End Function

    '
    ' POST: /Account/SendCode
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function SendCode(model As SendCodeViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View()
        End If

        ' Generate the token and send it
        If Not Await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider) Then
            Return View("Error")
        End If
        Return RedirectToAction("VerifyCode", New With {
            .Provider = model.SelectedProvider,
            .ReturnUrl = model.ReturnUrl,
            .RememberMe = model.RememberMe
        })
    End Function

    '
    ' GET: /Account/ExternalLoginCallback
    <AllowAnonymous>
    Public Async Function ExternalLoginCallback(returnUrl As String) As Task(Of ActionResult)
        Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync()
        If loginInfo Is Nothing Then
            Return RedirectToAction("Login")
        End If

        ' Sign in the user with this external login provider if the user already has a login
        Dim result = Await SignInManager.ExternalSignInAsync(loginInfo, isPersistent:=False)
        Select Case result
            Case SignInStatus.Success
                Return RedirectToLocal(returnUrl)
            Case SignInStatus.LockedOut
                Return View("Lockout")
            Case SignInStatus.RequiresVerification
                Return RedirectToAction("SendCode", New With {
                    .ReturnUrl = returnUrl,
                    .RememberMe = False
                })
            Case Else
                ' If the user does not have an account, then prompt the user to create an account
                ViewData!ReturnUrl = returnUrl
                ViewData!LoginProvider = loginInfo.Login.LoginProvider
                Return View("ExternalLoginConfirmation", New ExternalLoginConfirmationViewModel() With {
                    .Email = loginInfo.Email
                })
        End Select
    End Function

    '
    ' POST: /Account/ExternalLoginConfirmation
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ExternalLoginConfirmation(model As ExternalLoginConfirmationViewModel, returnUrl As String) As Task(Of ActionResult)
        If User.Identity.IsAuthenticated Then
            Return RedirectToAction("Index", "Manage")
        End If

        If ModelState.IsValid Then
            ' Get the information about the user from the external login provider
            Dim info = Await AuthenticationManager.GetExternalLoginInfoAsync()
            If info Is Nothing Then
                Return View("ExternalLoginFailure")
            End If
            Dim userInfo = New ApplicationUser() With {
                .UserName = model.Email,
                .Email = model.Email
            }
            Dim result = Await UserManager.CreateAsync(userInfo)
            If result.Succeeded Then
                result = Await UserManager.AddLoginAsync(userInfo.Id, info.Login)
                If result.Succeeded Then
                    Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
                    Return RedirectToLocal(returnUrl)
                End If
            End If
            AddErrors(result)
        End If

        ViewData!ReturnUrl = returnUrl
        Return View(model)
    End Function

    '
    ' POST: /Account/LogOff
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function LogOff() As ActionResult
        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
        Return RedirectToAction("Login", "Account")
    End Function

    '
    ' GET: /Account/ExternalLoginFailure
    <AllowAnonymous>
    Public Function ExternalLoginFailure() As ActionResult
        Return View()
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If _userManager IsNot Nothing Then
                _userManager.Dispose()
                _userManager = Nothing
            End If
            If _signInManager IsNot Nothing Then
                _signInManager.Dispose()
                _signInManager = Nothing
            End If
        End If

        MyBase.Dispose(disposing)
    End Sub

#Region "Helpers"
    ' Used for XSRF protection when adding external logins
    Private Const XsrfKey As String = "XsrfId"

    Private ReadOnly Property AuthenticationManager() As IAuthenticationManager
        Get
            Return HttpContext.GetOwinContext().Authentication
        End Get
    End Property

    Private Sub AddErrors(result As IdentityResult)
        For Each [error] In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub

    Private Function RedirectToLocal(returnUrl As String) As ActionResult
        If Url.IsLocalUrl(returnUrl) Then
            Return Redirect(returnUrl)
        End If
        Return RedirectToAction("Index", "Home")
    End Function

    Friend Class ChallengeResult
        Inherits HttpUnauthorizedResult
        Public Sub New(provider As String, redirectUri As String)
            Me.New(provider, redirectUri, Nothing)
        End Sub

        Public Sub New(provider As String, redirect As String, user As String)
            LoginProvider = provider
            RedirectUri = redirect
            UserId = user
        End Sub

        Public Property LoginProvider As String
        Public Property RedirectUri As String
        Public Property UserId As String

        Public Overrides Sub ExecuteResult(context As ControllerContext)
            Dim properties = New AuthenticationProperties() With {
                .RedirectUri = RedirectUri
            }
            If UserId IsNot Nothing Then
                properties.Dictionary(XsrfKey) = UserId
            End If
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider)
        End Sub
    End Class
#End Region
End Class
