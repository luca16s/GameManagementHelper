using GameSaveManager.Core.Interfaces;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameSaveManager.GoogleIntegration
{
    public class CloudConnection : ICloudConnection
    {
        private string EndResponseString => @"<html>
                                                 <head>
                                                     <meta http-equiv='refresh' content='10;url=https://google.com'>
                                                 </head>
                                                 <body>
                                                     Please return to the app.
                                                 </body>
                                              </html>";

        public string BuildAuthorizationRequest(string redirectUri, string authEndpoint, string clientID)
        {
            string state = ConnectionUtils.RandomDatabase64Url(32);
            string code_verifier = ConnectionUtils.RandomDatabase64Url(32);
            string code_challenge = ConnectionUtils.Base64UrlEncodeNoPadding(ConnectionUtils.Sha256(code_verifier));
            const string code_challenge_method = "S256";

            string authorizationRequest =
                @$"{authEndpoint}?response_type=code&scope=openid%20profile&redirect_uri={Uri.EscapeDataString(redirectUri)}&client_id={clientID}&state={state}&code_challenge={code_challenge}&code_challenge_method={code_challenge_method}";

            return authorizationRequest;
        }

        public HttpListener StartHttpListener(string redirectUri)
        {
            var http = new HttpListener();
            http.Prefixes.Add(redirectUri);
            return http;
        }

        public async Task<HttpListenerContext> GetConnectionContextAsync(HttpListener httpListener) => await httpListener.GetContextAsync();

        public HttpListenerResponse GetListenerResponse(HttpListenerContext httpListenerContext) => httpListenerContext.Response;

        public void WriteResponseOutput(HttpListener httpListener, HttpListenerResponse listenerResponse)
        {
            var buffer = Encoding.UTF8.GetBytes(EndResponseString);
            listenerResponse.ContentLength64 = buffer.Length;
            var responseOutput = listenerResponse.OutputStream;
            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                httpListener.Stop();
            });
        }

        public bool CheckConnectionForErrors(HttpListenerContext listenerContext)
        {
            if (listenerContext.Request.QueryString.Get("error") != null)
            {
                return false;
            }
            if (listenerContext.Request.QueryString.Get("code") == null
                || listenerContext.Request.QueryString.Get("state") == null)
            {
                return false;
            }

            return true;
        }
    }
}
