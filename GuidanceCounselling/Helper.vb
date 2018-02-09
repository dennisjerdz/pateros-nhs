Imports System.Net
Imports System.Net.Mail
Imports System.Threading.Tasks

Public Class Helper
    Public Class Email
        Function ResetPasswordEmail(ByVal email As String, ByVal name As String, ByVal body As String) As Task
            Dim fromAddress As New MailAddress("testintingone@gmail.com", "Testinting")
            Dim toAddress As New MailAddress(email, name)
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
                .Body = body
            }

            smtp.Send(message)
        End Function
    End Class
End Class
