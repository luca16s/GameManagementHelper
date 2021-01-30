namespace GameSaveManager.OneDriveApi
{
    using System.Threading.Tasks;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;

    using Microsoft.Identity.Client;

    public class OneDriveConnection : IConnection
    {
        public IPublicClientApplication PublicClientApp { get; private set; }

        private static readonly string ClientId = "275a06eb-7594-4495-94a7-02e86d1e180b";
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
