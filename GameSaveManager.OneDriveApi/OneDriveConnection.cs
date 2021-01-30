namespace GameSaveManager.OneDriveApi
{
    using System.Threading.Tasks;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;

    using Microsoft.Identity.Client;

    public class OneDriveConnection : IConnection
    {
        public IPublicClientApplication PublicClientApp { get; private set; }

        private static readonly string ClientId = "";
        private static readonly string Tenant = "consumers";
        private static readonly string Instance = "https://login.microsoftonline.com/";

        public Task<dynamic> ConnectAsync(Secrets secrets)
        {
            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{Tenant}")
                .WithDefaultRedirectUri()
                .Build();

            return null;
        }
    }
}
