using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;
/// <summary>
/// Summary description for BasicAuthenticationAttribute
/// </summary>
public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
{
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        try
        {
            if (actionContext.Request.Headers.Authorization == null)
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                // decoding authToken we get decode value in 'Username:Password' format 
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                // spliting decodeauthToken using ':' 
                string[] userNamePassword = decodedAuthenticationToken.Split(':');
                string userName = userNamePassword[0];
                string password = userNamePassword[1];
                string str_msg = "";
                // at 0th postion of array we get username and at 1st we get password  
                if (ExUserController.GetUserProfile(userName, password,out str_msg))
                {
                    // setting current principle  
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
        }
        catch(Exception ex)
        {
            actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.BadRequest);
        }

    }
    //private bool IsAuthorizedUser(string Username, string Password)
    //{
    //    // In this method we can handle our database logic here...  
    //    return Username == "bhushan" && Password == "demo";
    //}
}