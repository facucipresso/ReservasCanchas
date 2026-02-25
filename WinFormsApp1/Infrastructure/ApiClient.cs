using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Infrastructure
{
    public class ApiClient
    {
        // HttpClient estático para reusar conexion
        public static readonly HttpClient Http;

        static ApiClient()
        {
            Http = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.ApiBaseUrl),
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        // Método helper para reconfigurar el bearer token VER ESTO
        public static void SetBearerToken(string token)
        {
            
            Http.DefaultRequestHeaders.Authorization =
                string.IsNullOrWhiteSpace(token)
                    ? null
                    : new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }
    }
}
