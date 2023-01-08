<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.Configuration" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {

        // Code that runs on application startup
        GlobalConfiguration.Configure(WebApiConfig.Register);

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    // void Application_AcquireRequestState(Object sender, EventArgs e)
    //  {
    //string cookieName = "ASP.NET_SessionId";
    //var sessionState = ConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;
    //var timeout = sessionState != null ? sessionState.Timeout : TimeSpan.FromMinutes(10);

    //try
    //{
    //    if (Request.Cookies[cookieName] != null)
    //    {
    //        Response.Cookies[cookieName].Value = HttpContext.Current.Session.SessionID;
    //        if (System.Configuration.ConfigurationManager.AppSettings["isSetCookiePath"] != null && Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["isSetCookiePath"]) == "Y")
    //        {
    //            //    Response.Cookies[cookieName].Path = "/site/";
    //            Response.Cookies[cookieName].Path = Request.ApplicationPath;
    //        }

    //        Response.Cookies[cookieName].Expires = DateTime.Now.Add(timeout);
    //    }
    //}
    //catch (Exception ex)
    //{

    //}

    //}
    protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
    {
        // only apply session cookie persistence to requests requiring session information
        if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
        {
            var sessionState = ConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;
            var cookieName = sessionState != null && !string.IsNullOrEmpty(sessionState.CookieName) ? sessionState.CookieName : "ASP.NET_SessionId";

            var timeout = sessionState != null ? sessionState.Timeout : TimeSpan.FromMinutes(20);
            try
            {
                // Ensure ASP.NET Session Cookies are accessible throughout the subdomains.
                if (Request.Cookies[cookieName] != null && Session != null && Session.SessionID != null)
                {
                    if (Request.ApplicationPath != "/")
                    {


                        Response.Cookies[cookieName].Value = Session.SessionID;
                        Response.Cookies[cookieName].Path = Request.ApplicationPath;
                        Response.Cookies[cookieName].Expires = DateTime.Now.Add(timeout);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
    }

</script>

