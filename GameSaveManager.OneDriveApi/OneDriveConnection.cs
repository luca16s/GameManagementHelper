namespace GameSaveManager.OneDriveApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading.Tasks;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;

    using Microsoft.Identity.Client;

    public class OneDriveConnection : IConnection
    {
        private static readonly string Authority = "https://login.microsoftonline.com/consumers";
        private static readonly string[] scopes = new string[] { "user.read" };
        private static readonly string graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";
        private static readonly string ClientId = "";

        private static async Task<string> GetHttpContentWithToken(string url, string token)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await httpClient.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static dynamic publicClientApp;
        public static dynamic PublicClientApp
        {
            get
            {
                if (publicClientApp == null)
                {
                    publicClientApp = PublicClientApplicationBuilder
                        .Create(ClientId)
                        .WithAuthority(Authority)
                        .WithClientName("Game Save Manager")
                        .WithDefaultRedirectUri()
                        .Build();
                }

                return publicClientApp;
            }
        }

        public async Task ConnectAsync(Secrets secrets)
        {
            IEnumerable<IAccount> accounts = await PublicClientApp.GetAccountsAsync();
            IAccount firstAccount = accounts.FirstOrDefault();

            AuthenticationResult authResult;
            try
            {
                authResult = await PublicClientApp.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    authResult = await PublicClientApp.AcquireTokenInteractive(scopes)
                        .WithAccount(firstAccount)
                        .WithPrompt(Prompt.SelectAccount)
                        .ExecuteAsync();
                }
                catch (MsalException msalEx)
                {
                    throw new ApplicationException(msalEx.Message);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return;
        }

        public async Task<UserModel> GetUserInformation()
        {
            IEnumerable<IAccount> accounts = await PublicClientApp.GetAccountsAsync();
            IAccount firstAccount = accounts.FirstOrDefault();

            AuthenticationResult authResult = await PublicClientApp.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();

            return JsonSerializer.Deserialize<UserModel>(await GetHttpContentWithToken(graphAPIEndpoint, authResult.AccessToken));
        }
    }
}
