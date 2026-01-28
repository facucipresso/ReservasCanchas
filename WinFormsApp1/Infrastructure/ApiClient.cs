using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Infrastructure
{
    public class ApiClient
    {
        // HttpClient estático para reusar conexiones (mejor performance)
        public static readonly HttpClient Http;

        static ApiClient()
        {
            Http = new HttpClient
            {
                BaseAddress = new Uri(AppConfig.ApiBaseUrl),
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        // Método helper para (re)configurar el bearer token VER ESTO
        public static void SetBearerToken(string token)
        {
            
            Http.DefaultRequestHeaders.Authorization =
                string.IsNullOrWhiteSpace(token)
                    ? null
                    : new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            


            //La opcion de arriba antepone 'Bearer' al token y esta version no, ver cual es la que anda
            /*
            if (string.IsNullOrWhiteSpace(token))
            {
                Http.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                // Envía SOLO el token, sin "Bearer"
                Http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token);
            }
            */
        }
    }
}
