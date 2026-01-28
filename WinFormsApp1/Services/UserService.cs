using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Models.User;

namespace WinFormsApp1.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService() 
        {
            _httpClient = ApiClient.Http;
        }

        /*
        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var authHeader = _httpClient.DefaultRequestHeaders.Authorization;


            var response = await _httpClient.GetAsync("api/users");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
);

                throw new Exception($"Error obteniendo los usuarios: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<List<UserResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los usuarios");
            }

            return respuesta;
        }
        */

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/users");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error obteniendo los usuarios: {body}");
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var respuesta = JsonSerializer.Deserialize<List<UserResponseDTO>>(json, options);

            if (respuesta == null)
            {
                throw new Exception("Respuesta inválida de la API al conseguir los usuarios");
            }

            return respuesta;
        }

        public async Task<List<UserResponseWithRoleDTO>> GetAllUsersWithRoleAsync()
        {
            var response = await _httpClient.GetAsync("api/users/withRole");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error obteniendo los usuarios: {body}");
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var respuesta = JsonSerializer.Deserialize<List<UserResponseWithRoleDTO>>(json, options);

            if (respuesta == null)
            {
                throw new Exception("Respuesta inválida de la API al conseguir los usuarios");
            }

            return respuesta;
        }

        public async Task<ComplexOwnerDTO> GetComplxOwnerByIdAsync(int idComplex)
        {
            var response = await _httpClient.GetAsync($"api/complexes/getComplexOwner/{idComplex}");
            /*
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error obteniendo los usuarios: {body}");
            }
            */

            var respuesta = await response.Content.ReadFromJsonAsync<ComplexOwnerDTO>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir nombre y apellido del dueño del complejo");
            }

            return respuesta;
        }



        public async Task BlockUserByIdAsync(int id)
        {
            var authHeader = _httpClient.DefaultRequestHeaders.Authorization;
            /*
            MessageBox.Show(
                $"HEADER AUTH = {authHeader?.Scheme} {authHeader?.Parameter}",
                "DEBUG",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            */

            var response = await _httpClient.PutAsync($"api/users/{id}/block", null);

            //ver bien esto porque me retorna no content si sale todo bien el endpoint
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
);

                throw new Exception($"Error bloqueando en usuario: {body}");
            }
            /*
            var respuesta = await response.Content.ReadFromJsonAsync<List<UserResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los usuarios");
            }
            */
        }

        public async Task UnBlockUserByIdAsync(int id)
        {
            var authHeader = _httpClient.DefaultRequestHeaders.Authorization;
            /*
            MessageBox.Show(
                $"HEADER AUTH = {authHeader?.Scheme} {authHeader?.Parameter}",
                "DEBUG",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            */

            var response = await _httpClient.PutAsync($"api/users/{id}/unBlock", null);

            //ver bien esto porque me retorna no content si sale todo bien el endpoint
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
);

                throw new Exception($"Error desbloqueando en usuario: {body}");
            }
            /*
            var respuesta = await response.Content.ReadFromJsonAsync<List<UserResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los usuarios");
            }
            */
        }

    }
}
