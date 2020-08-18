using Dropbox.Api;

using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;

using System;
using System.Diagnostics;
using System.IO;
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

            var accessToken = secrets.AppToken ?? await GetAccessToken(secrets.AppKey).ConfigureAwait(true);

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
            string accessToken;
            try
            {
                var state = Guid.NewGuid().ToString("N");
                var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, appkey, RedirectUri, state: state);
                using var http = new HttpListener();
                http.Prefixes.Add(LoopbackHost);

                http.Start();

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = authorizeUri.ToString(),
                    UseShellExecute = true
                };
                Process.Start(psi);

                await HandleOAuth2Redirect(http).ConfigureAwait(true);

                var result = await HandleJSRedirect(http).ConfigureAwait(true);

                if (result.State != state)
                {
                    return null;
                }

                accessToken = result.AccessToken;
                var uid = result.Uid;
            }
            catch (Exception)
            {
                return null;
            }

            return accessToken;
        }

        private async Task HandleOAuth2Redirect(HttpListener http)
        {
            var context = await http.GetContextAsync().ConfigureAwait(true);

            // We only care about request to RedirectUri endpoint.
            while (context.Request.Url.AbsolutePath != RedirectUri.AbsolutePath)
            {
                context = await http.GetContextAsync().ConfigureAwait(true);
            }

            context.Response.ContentType = "text/html";

            // Respond with a page which runs JS and sends URL fragment as query string
            // to TokenRedirectUri.
            using (var file = File.OpenRead("index.html"))
            {
                file.CopyTo(context.Response.OutputStream);
            }

            context.Response.OutputStream.Close();
        }

        private async Task<OAuth2Response> HandleJSRedirect(HttpListener http)
        {
            var context = await http.GetContextAsync().ConfigureAwait(true);

            // We only care about request to TokenRedirectUri endpoint.
            while (context.Request.Url.AbsolutePath != JSRedirectUri.AbsolutePath)
            {
                context = await http.GetContextAsync().ConfigureAwait(true);
            }

            var redirectUri = new Uri(context.Request.QueryString["url_with_fragment"]);

            var result = DropboxOAuth2Helper.ParseTokenFragment(redirectUri);

            return result;
        }
    }
}