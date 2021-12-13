namespace iso.gmh.dropboxService
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Dropbox.Api;
    using Dropbox.Api.Sharing;

    using iso.gmh.Core.Interfaces;
    using iso.gmh.Core.Models;

    public class DropboxConnection : IConnection
    {
        private const string responseString = @"
                <html>
                <body onload='redirect()'>Por favor retorne para o App.
                </body>
                </html>
                <script type='text/javascript'>
                        function redirect()
                        {
                            document.location.href ='/token?url_with_fragment='+encodeURIComponent(document.location.href); close();
                        }
                </script>";

        private readonly string LoopbackHost = "http://127.0.0.1:52475/";
        private readonly Uri JSRedirectUri = new("http://127.0.0.1:52475/token");
        private readonly Uri RedirectUri = new("http://127.0.0.1:52475/authorize");

        private async Task<string> GetAccessToken(string appkey)
        {
            string state = Guid.NewGuid().ToString("N");
            Uri authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, appkey, RedirectUri, state: state);
            using var httpListener = new HttpListener();
            httpListener.Prefixes.Add(LoopbackHost);

            httpListener.Start();

            var processStartInfo = new ProcessStartInfo
            {
                FileName = authorizeUri.ToString(),
                UseShellExecute = true
            };

            _ = Process.Start(processStartInfo);

            await HandleOAuth2Redirect(httpListener).ConfigureAwait(true);

            OAuth2Response result = await HandleJSRedirect(httpListener).ConfigureAwait(true);

            return result.State != state ? null : result.AccessToken;
        }

        private async Task HandleOAuth2Redirect(HttpListener http)
        {
            HttpListenerContext httpListenerContext = await http.GetContextAsync().ConfigureAwait(true);

            while (httpListenerContext.Request.Url.AbsolutePath != RedirectUri.AbsolutePath)
                httpListenerContext = await http.GetContextAsync().ConfigureAwait(true);

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            httpListenerContext.Response.ContentLength64 = buffer.Length;

            await httpListenerContext.Response.OutputStream.WriteAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(true);

            httpListenerContext.Response.OutputStream.Close();
        }

        private async Task<OAuth2Response> HandleJSRedirect(HttpListener httpListener)
        {
            HttpListenerContext httpListenerContext = await httpListener.GetContextAsync().ConfigureAwait(true);

            while (httpListenerContext.Request.Url.AbsolutePath != JSRedirectUri.AbsolutePath)
                httpListenerContext = await httpListener.GetContextAsync().ConfigureAwait(true);

            var redirectUri = new Uri(httpListenerContext.Request.QueryString["url_with_fragment"]);

            httpListener.Stop();

            return DropboxOAuth2Helper.ParseTokenFragment(redirectUri);
        }

        public dynamic PublicClientApp { get; private set; }

        public async Task ConnectAsync(Secrets secrets)
        {
            if (secrets == null)
                return;

            if (PublicClientApp == null)
            {
                DropboxCertHelper.InitializeCertPinning();

                string accessToken = string.IsNullOrWhiteSpace(secrets.AppToken)
                                        ? await GetAccessToken(secrets.AppKey).ConfigureAwait(true)
                                        : secrets.AppToken;

                var httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(20) };

                var config = new DropboxClientConfig("Game Save Manager")
                {
                    HttpClient = httpClient,
                };

                PublicClientApp = new DropboxClient(accessToken, "", secrets.AppKey, secrets.AppSecret, config);
            }
        }

        public async Task<UserModel> GetUserInformation()
        {
            UserInfo user = await PublicClientApp
                .Users
                .GetCurrentAccountAsync()
                .ConfigureAwait(true);

            return new(user.DisplayName, user.Email);
        }
    }
}