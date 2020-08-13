using Dropbox.Api;
using Dropbox.Api.Files;

using GameSaveManager.Core.Models;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameSaveManager.DropboxIntegration
{
    public class DropboxConnection
    {
        private readonly Uri RedirectUri = new Uri($"{AppSettings.LoopbackHost}authorize");
        private readonly Uri JSRedirectUri = new Uri($"{AppSettings.LoopbackHost}token");

        public DropboxClient Client { get; private set; }

        public async Task ConnectAsync(string appKey, string appSecret)
        {
            DropboxCertHelper.InitializeCertPinning();

            var accessToken = await GetAccessToken(appKey).ConfigureAwait(true);

            var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(20)
            };

            var config = new DropboxClientConfig("GameSaveManager")
            {
                HttpClient = httpClient
            };

            Client = new DropboxClient(accessToken, "", appKey, appSecret, config);
        }

        public async Task UploadGameSave(string filePath, string folder, string fileName)
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(filePath));
            using var streamBody = new MemoryStream(Encoding.UTF8.GetBytes(filePath));
            var response = await Client
                .Files
                .UploadAsync($"{folder}/{fileName}", WriteMode.Overwrite.Instance, body: streamBody)
                .ConfigureAwait(true);
        }

        public static void DownloadGameSave()
        {

        }

        private async Task<string> GetAccessToken(string appkey)
        {
            string accessToken;
            try
            {
                var state = Guid.NewGuid().ToString("N");
                var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, appkey, RedirectUri, state: state);
                using var http = new HttpListener();
                http.Prefixes.Add(AppSettings.LoopbackHost);

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