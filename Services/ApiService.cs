using System.Net.Http.Json;
using Recordatorios.Models;

namespace Recordatorios.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        // Cambia esta URL por la dirección IP de tu API local o backend
        private readonly string _baseUrl = "https://tu-api.com/api";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Recordatorio>> ObtenerRecordatoriosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Recordatorio>>($"{_baseUrl}/recordatorios") ?? new();
        }

        public async Task<Recordatorio?> ObtenerRecordatorioPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Recordatorio>($"{_baseUrl}/recordatorios/{id}");
        }

        public async Task<bool> GuardarRecordatorioAsync(Recordatorio recordatorio)
        {
            HttpResponseMessage response;
            if (recordatorio.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/recordatorios", recordatorio);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/recordatorios/{recordatorio.Id}", recordatorio);
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarRecordatorioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/recordatorios/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}