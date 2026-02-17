using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Models.Complex;
using WinFormsApp1.Models.Dashboard;
using WinFormsApp1.Models.Review;
using WinFormsApp1.Models.User;

namespace WinFormsApp1.Services
{
    public class DashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService()
        {
            _httpClient = ApiClient.Http;
        }

        public async Task<SumaryDashboardDTO> GetDashboardDataAsync() 
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

            var response = await _httpClient.GetAsync("api/admin/dashboard-summary");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error obteniendo la data del dashboard: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<SumaryDashboardDTO>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir la data para el dashboard");
            }

            return respuesta;
        }

        public async Task<List<ComplexSuperrAdminResponseDTO>> GetLastFiveComplexesBySuperAdminAsync()
        {
            var response = await _httpClient.GetAsync("api/admin/dashboard-complex");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error obteniendo los datos de complejo para el dashboard: {body}");
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var respuesta = JsonSerializer.Deserialize<List<ComplexSuperrAdminResponseDTO>>(json, options);

            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los datos de complejos para el dashboard");
            }

            return respuesta;
        }

        public async Task<List<UserResponseWithRoleDTO>> GetLastSixUsersWithRoleAsync() 
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

            var response = await _httpClient.GetAsync("api/admin/dashboard-users");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error obteniendo los datos de ususarios para el dashboard: {body}");
            }

            
            //var respuesta = await response.Content.ReadFromJsonAsync<List<UserResponseWithRoleDTO>>();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var respuesta = JsonSerializer.Deserialize<List<UserResponseWithRoleDTO>>(json, options);

            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los datos de usuarios para el dashboard");
            }

            return respuesta;
        }

        public async Task<List<ReviewResponseDTO>> GetLastFourReviewsAsync()
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

            var response = await _httpClient.GetAsync("api/admin/dashboard-review");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error obteniendo los datos de reviews para el dashboard: {body}");
            }

            //var respuesta = await response.Content.ReadFromJsonAsync<List<ReviewResponseDTO>>();
            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var respuesta = JsonSerializer.Deserialize<List<ReviewResponseDTO>>(json, options);

            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los datos de reviews para el dashboard");
            }

            return respuesta;
        }


    }
}
