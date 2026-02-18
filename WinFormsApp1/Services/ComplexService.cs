using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Complex;
using WinFormsApp1.Models.Field;
using WinFormsApp1.Models.Reservation;
using WinFormsApp1.Models.Review;

namespace WinFormsApp1.Services
{
    public class ComplexService
    {
        private readonly HttpClient _httpClient;

        public ComplexService()
        {
            _httpClient = ApiClient.Http;
        }

        public async Task<List<ComplexSuperAdminResponseDTO>> GetAllComplexesAsync()
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

            var response = await _httpClient.GetAsync("api/complexes/super-admin");

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
                throw new Exception($"Error obteniendo complejos: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<List<ComplexSuperAdminResponseDTO>>();
            if(respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir los complejos");
            }

            return respuesta;
        }

        public async Task<ComplexDetailResponseDTO> ChangeStateComplexAsync(int id, UpdateComplexStateDTO newState)
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

            var response = await _httpClient.PatchAsJsonAsync($"api/complexes/{id}/state", newState);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                if((int)response.StatusCode == 400)
                {
                    /*
                    MessageBox.Show(
                        $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                        "ERROR DEBUG",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    */
                }
                else
                {
                    /*
                    MessageBox.Show(
                        $"StatusCode: {(int)response.StatusCode}\n\nBody:\n{body}",
                        "ERROR DEBUG",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    */
                    throw new Exception($"Error cambiando el estado del complejo: {body}");
                }



            }

            var respuesta = await response.Content.ReadFromJsonAsync<ComplexDetailResponseDTO>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al intentar cambiar el estado del complejo");
            }

            return respuesta;
        }

        public async Task<ComplexDetailResponseDTO> GetComplexDetailByIdAsync(int id) 
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

            var response = await _httpClient.GetAsync($"api/complexes/{id}");

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
                throw new Exception($"Error obteniendo el detalle del complejo: {body}");
                //los throw new Exception me los tira automaticamente en forma de message box
                //y aca no tendria que retornar directametne antes de throw new Exception($"Error obteniendo el detalle del complejo: {body}");
                //porque me como dos carteles de error
            }

            //las clases anidadas se convierten solas porque ya tengo creadas dichas clases en este proyecto
            var respuesta = await response.Content.ReadFromJsonAsync<ComplexDetailResponseDTO>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir el detalle del complejo");
            }

            return respuesta;
        }

        public async Task<List<FieldDetailResponseDTO>> GetAllFieldsOfComplexAsync(int complexId)
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

            var response = await _httpClient.GetAsync($"api/fields/by-complex/{complexId}"); 

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
                throw new Exception($"Error obteniendo canchas: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<List<FieldDetailResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir las canchas");
            }

            return respuesta;
        }

        public async Task<List<ReservationResponseDTO>> GetAllReservationsForFieldAsync(int fieldId)
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

            var response = await _httpClient.GetAsync($"api/reservations/field/{fieldId}");

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
                throw new Exception($"Error obteniendo las reservas de la cancha: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<List<ReservationResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir las reservas de la cancha");
            }

            return respuesta;
        }

        public async Task<List<ReservationResponseDTO>> GetAllComplexReservationsAsync(int complexId)
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

            var response = await _httpClient.GetAsync($"api/reservations/complex/{complexId}");

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
                throw new Exception($"Error obteniendo las reservas del complejo: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<List<ReservationResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir las reservas del complejo");
            }

            return respuesta;
        }

        public async Task<List<ReviewResponseDTO>> GetAllComplexReviewsAsync(int complexId)
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

            //porque lo recibe como FromQuery
            var response = await _httpClient.GetAsync($"api/reviews?complexId={complexId}");

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
                throw new Exception($"Error obteniendo las reseñas del complejo: {body}");
            }

            var respuesta = await response.Content.ReadFromJsonAsync<List<ReviewResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al conseguir las reserñas del complejo");
            }

            return respuesta;
        }

        public async Task DeleteReviewsAsync(int reviewId)
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

            var response = await _httpClient.DeleteAsync($"api/reviews/{reviewId}");

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
                throw new Exception($"Error eliminando la reseña: {body}");
            }

            /* ESTO CREO QUE NO VA, PORQUE SI SALE TODO BIEN RETORNA 'NO CONTENT'
            var respuesta = await response.Content.ReadFromJsonAsync<List<ReviewResponseDTO>>();
            if (respuesta == null)
            {
                throw new Exception("Respuesta invalida de la api al eliminar la reseña");
            }
            */

        }




    }
}
