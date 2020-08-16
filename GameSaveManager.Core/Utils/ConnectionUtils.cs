using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace GameSaveManager.Core.Utils
{
    public static class ConnectionUtils
    {
        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        public static Uri RandomDatabase64Url(uint length)
        {
            using var rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return Base64UrlEncodeNoPadding(bytes);
        }

        public static byte[] Sha256(string inputString)
        {
            using var sha = new SHA256Managed();
            return sha.ComputeHash(Encoding.ASCII.GetBytes(inputString));
        }

        public static Uri Base64UrlEncodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            base64 = base64.Replace("+", "-", StringComparison.InvariantCultureIgnoreCase);
            base64 = base64.Replace("/", "_", StringComparison.InvariantCultureIgnoreCase);
            base64 = base64.Replace("=", "", StringComparison.InvariantCultureIgnoreCase);

            return new Uri(base64);
        }
    }
}