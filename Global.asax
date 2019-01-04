<%@ Application Language="VB" %>
<%@ Import Namespace="System.Security.Principal" %>
<script runat="server">
    
    Protected Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        If HttpContext.Current.User IsNot Nothing Then
            If HttpContext.Current.User.Identity.IsAuthenticated Then
                If TypeOf HttpContext.Current.User.Identity Is FormsIdentity Then
                    Dim id As FormsIdentity = DirectCast(HttpContext.Current.User.Identity, FormsIdentity)
                    Dim ticket As FormsAuthenticationTicket = id.Ticket
                
                    ' Get the stored user-data, in this case, our roles 
                    Dim userData As String = ticket.UserData
                    Dim roles As String() = userData.Split(",")
                    HttpContext.Current.User = New GenericPrincipal(id, roles)
                End If
            End If
        End If
    End Sub
    Sub Application_OnStart(ByVal Sender As Object, ByVal E As EventArgs)
        ' Set our user count to 0 when we start the server 
        'Application("ActiveUsers") = 0
    End Sub

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        Application("ActiveUsers") = 0
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
        Application.Lock()
        Application("ActiveUsers") = Application("ActiveUsers") + 1
        Application.UnLock()
        
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Application.Lock()
        Application("ActiveUsers") = Application("ActiveUsers") - 1
        Application.UnLock()

        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>