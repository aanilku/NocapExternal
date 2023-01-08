using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

/// <summary>
/// Summary description for SSOAuthentication
/// </summary>
public class SSOAuthentication
{
    string access_token;
    string expires_in;
    string refresh_expires_in;
    string refresh_token;
    string token_type;
    string not_before_policy;
    string session_state;
    string scope;
    public SSOAuthentication()
    {


    }

    [JsonProperty("Access_Token")]
    public string AccessToken
    {
        get
        {
            return access_token;
        }
        set
        {
            access_token = value;
        }
    }

    [JsonProperty("Expires_In")]
    public string ExpiresIn
    {
        get
        {
            return expires_in;
        }
        set
        {
            expires_in = value;
        }
    }

    [JsonProperty("Refresh_Expires_In")]
    public string RefreshExpiresIn
    {
        get
        {
            return refresh_expires_in;
        }
        set
        {
            refresh_expires_in = value;
        }
    }

    [JsonProperty("Refresh_Token")]
    public string RefreshToken
    {
        get
        {
            return refresh_token;
        }
        set
        {
            refresh_token = value;
        }
    }

    [JsonProperty("Token_Type")]
    public string TokenType
    {
        get
        {
            return token_type;
        }
        set
        {
            token_type = value;
        }
    }


    [JsonProperty("Not-Before-Policy")]
    public string NotBeforePolicy
    {
        get
        {
            return not_before_policy;
        }
        set
        {
            not_before_policy = value;
        }
    }
    [JsonProperty("Session_State")]
    public string SessionState
    {
        get
        {
            return session_state;
        }
        set
        {
            session_state = value;
        }
    }
    public string Scope
    {
        get
        {
            return scope;
        }
        set
        {
            scope = value;
        }
    }
}