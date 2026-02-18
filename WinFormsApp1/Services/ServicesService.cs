using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Service;

namespace WinFormsApp1.Services
{
    public class ServicesService
    {
        private readonly HttpClient _httpClient;

        public ServicesService()
        {
            _httpClient = ApiClient.Http;
        }

        public async Task<List<ServiceResponseDTO>> GetAllServicesAsync()
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

            var response = await _httpClient.GetAsync("api/services");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                /*
                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                */
                throw new Exception($"Error obteniendo servicios: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<List<ServiceResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los servicios");
            }

            return respuesta;
        }

        public async Task<ServiceResponseDTO> UpdateServiceByIdAsync(int id, ServiceUpdateDTO serviceDto)
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
            var response = await _httpClient.PutAsJsonAsync("api/services/"+id, serviceDto);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                /*
                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                 );
                */
                throw new Exception($"Error obteniendo servicios: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<ServiceResponseDTO>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los servicios");
            }

            return respuesta;
        }

        public async Task<ServiceResponseDTO> CreateServiceAsync(ServiceUpdateDTO serviceCreateDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/services", serviceCreateDto);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                /*
                MessageBox.Show(
                    $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                    "ERROR DEBUG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                */
                throw new Exception($"Error creando servicio: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<ServiceResponseDTO>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al crear el servicio");
            }

            return respuesta;

        }

        public async Task<bool> DeleteServiceByIdAsync(int id) 
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
            var response = await _httpClient.DeleteAsync("api/services/" + id);//aca tambien toma el id solo

            if (response.IsSuccessStatusCode) return true;



           var body = await response.Content.ReadAsStringAsync();
            /*
           MessageBox.Show(
                $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                "ERROR DEBUG",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            */
            return false;
        }
    }
}
