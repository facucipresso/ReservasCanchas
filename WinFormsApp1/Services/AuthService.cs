using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Auth;
using WinFormsApp1.Security;

namespace WinFormsApp1.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService()
        {
            _http = ApiClient.Http;
        }

        public async Task<LoginResponse?> LoginAsync(string username, string password)
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password
            };

            try
            {
                var response = await _http.PostAsJsonAsync("api/account/login", request);

                if (!response.IsSuccessStatusCode)
                {
                    //var error = await response.Content.ReadAsStringAsync();
                    //throw new InvalidOperationException($"Error en login: {response.StatusCode} - {error}");

                    var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                    var message = problem?.Detail ?? "Error desconocido";
                    throw new Exception(message);
                }

                var loginData = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (loginData == null || string.IsNullOrEmpty(loginData.Token))
                    throw new InvalidOperationException("La API no devolvió un token válido.");


                Session.SetSession(new LoginResponse
                {
                    UserName = loginData.UserName,
                    Email = loginData.Email,
                    Token = loginData.Token
                });
                return loginData;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("No se pudo conectar con la API.", ex);
            }
        }

        public void Logout()
        {
            Session.Clear();
        }
    }
}
