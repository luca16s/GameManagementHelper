namespace iso.gmh.dropbox;

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Dropbox.Api;
using Dropbox.Api.Users;

using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;
using iso.gmh.dropbox.Properties;

public class DropboxConnection : IConnection<DropboxClient>
{
    private const string Format = "N";
    private readonly Uri JSRedirectUri = new(Resources.LOOPBACK_HOST + Resources.TOKEN);
    private readonly Uri RedirectUri = new(Resources.LOOPBACK_HOST + Resources.AUTHORIZE);

    private async Task<OAuth2Response> GetAccessToken(
        string appKey
    )
    {
        var OAuthFlow = new PKCEOAuthFlow();
        string state = Guid.NewGuid().ToString(Format);

        Uri authorizeUri = OAuthFlow.GetAuthorizeUri(
            OAuthResponseType.Code,
            appKey,
            state: state,
            scopeList: null,
            redirectUri: RedirectUri.ToString(),
            tokenAccessType: TokenAccessType.Offline,
            includeGrantedScopes: IncludeGrantedScopes.User
        );

        using var listener = new HttpListener();
        listener.Prefixes.Add(Resources.LOOPBACK_HOST);

        listener.Start();

        _ = Process.Start(new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = authorizeUri.ToString(),
        });

        await HandleOAuth2Redirect(listener);

        return await OAuthFlow.ProcessCodeFlowAsync(
            await HandleJSRedirect(listener),
            appKey,
            RedirectUri.ToString(),
            state
        );
    }

    private async Task HandleOAuth2Redirect(
        HttpListener http
    )
    {
        HttpListenerContext context = await http.GetContextAsync();

        while (context.Request.Url.AbsolutePath != RedirectUri.AbsolutePath)
            context = await http.GetContextAsync();

        context.Response.ContentType = "text/html";

        using (FileStream file = File.OpenRead("index.html"))
            file.CopyTo(context.Response.OutputStream);

        context.Response.OutputStream.Close();
    }

    private async Task<Uri> HandleJSRedirect(
        HttpListener httpListener
    )
    {
        HttpListenerContext context = await httpListener.GetContextAsync();

        while (context.Request.Url.AbsolutePath != JSRedirectUri.AbsolutePath)
            context = await httpListener.GetContextAsync();

        return new Uri(context.Request.QueryString["url_with_fragment"]);
    }

    public DropboxClient Client { get; set; }

    public async Task<DropboxClient> ConnectAsync(
        Secrets secrets
    )
    {
        if (secrets is null)
            return null;

        OAuth2Response response = await GetAccessToken(
            secrets.AppKey
        );

        return new DropboxClient(
            response?.RefreshToken,
            secrets.AppKey,
            secrets.AppSecret,
            new DropboxClientConfig(
                Resources.APP_NAME,
                5
            )
            { HttpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(20) } }
        );
    }

    public async Task<User> GetUserInformation(
    )
    {
        FullAccount user = await Client.Users.GetCurrentAccountAsync();

        return new(user.Name.DisplayName, user.Email);
    }
}