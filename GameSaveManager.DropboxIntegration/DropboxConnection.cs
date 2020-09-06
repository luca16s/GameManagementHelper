using Dropbox.Api;

using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameSaveManager.DropboxIntegration
{
    public class DropboxConnection : IConnection
    {
        private readonly string LoopbackHost = "http://127.0.0.1:52475/";
        private readonly Uri JSRedirectUri = new Uri("http://127.0.0.1:52475/token");
        private readonly Uri RedirectUri = new Uri("http://127.0.0.1:52475/authorize");

        public async Task<object> ConnectAsync(Secrets secrets)
        {
            if (secrets == null) return default;

            DropboxCertHelper.InitializeCertPinning();

            var accessToken = string.IsNullOrWhiteSpace(secrets.AppToken)
                                    ? await GetAccessToken(secrets.AppKey).ConfigureAwait(true)
                                    : secrets.AppToken;

            var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(20)
            };

            var config = new DropboxClientConfig("GameSaveManager")
            {
                HttpClient = httpClient
            };

            return new DropboxClient(accessToken, "", secrets.AppKey, secrets.AppSecret, config);
        }

        private async Task<string> GetAccessToken(string appkey)
        {
            var state = Guid.NewGuid().ToString("N");
            var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, appkey, RedirectUri, state: state);
            using var httpListener = new HttpListener();
            httpListener.Prefixes.Add(LoopbackHost);

            httpListener.Start();

            var psi = new ProcessStartInfo
            {
                FileName = authorizeUri.ToString(),
                UseShellExecute = true
            };
            Process.Start(psi);

            await HandleOAuth2Redirect(httpListener).ConfigureAwait(true);

            var result = await HandleJSRedirect(httpListener).ConfigureAwait(true);

            if (result.State != state)
            {
                return null;
            }

            return result.AccessToken;
        }

        private async Task HandleOAuth2Redirect(HttpListener http)
        {
            var context = await http.GetContextAsync().ConfigureAwait(true);

            while (context.Request.Url.AbsolutePath != RedirectUri.AbsolutePath)
            {
                context = await http.GetContextAsync().ConfigureAwait(true);
            }

            const string responseString = "<html><body onload='redirect()'>Por favor retorne para o App.</body></html><script type='text/javascript'> function redirect(){ document.location.href ='/token?url_with_fragment='+encodeURIComponent(document.location.href); close();}</script>";

            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;

            await context.Response.OutputStream.WriteAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(true);

            context.Response.OutputStream.Close();
        }

        private async Task<OAuth2Response> HandleJSRedirect(HttpListener httpListener)
        {
            var context = await httpListener.GetContextAsync().ConfigureAwait(true);

            while (context.Request.Url.AbsolutePath != JSRedirectUri.AbsolutePath)
            {
                context = await httpListener.GetContextAsync().ConfigureAwait(true);
            }

            var redirectUri = new Uri(context.Request.QueryString["url_with_fragment"]);

            httpListener.Stop();

            return DropboxOAuth2Helper.ParseTokenFragment(redirectUri);
        }
    }
}