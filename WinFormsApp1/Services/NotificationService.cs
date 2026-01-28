using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Models.Notification;

namespace WinFormsApp1.Services
{
    public class NotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService()
        {
            _httpClient = ApiClient.Http;
        }

        public async Task<List<NotificacionResponseDTO>> GetAllNotificationsAsync()
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

            var response = await _httpClient.GetAsync("api/notifications/my");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error obteniendo notificaciones: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<List<NotificacionResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir las notificaciones");
            }

            return respuesta;
        }

        public async Task MarkAsReadAsync(int id)
        {
            var authHeader = _httpClient.DefaultRequestHeaders.Authorization;
            MessageBox.Show(
                $"HEADER AUTH = {authHeader?.Scheme} {authHeader?.Parameter}",
                "DEBUG",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            var response = await _httpClient.PatchAsync($"api/notifications/{id}/read", null);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw new Exception($"Error marcando como leida la notificacion: {body}");
            }
        }
    }
}
