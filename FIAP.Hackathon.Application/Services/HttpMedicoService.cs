using FIAP.Hackathon.Application.Interfaces;
using FIAP.Hackathon.Application.Responses;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FIAP.Hackathon.Application.Services
{
    public class HttpMedicoService : IHttpMedicoService
    {
        private readonly IConfiguration _configuration;

        public HttpMedicoService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<MedicoResponse?> ConsultarDadosMedico(Guid medicoId)
        {
            var urlApiMedico = Path.Combine(Environment.GetEnvironmentVariable("UrlApiMedico"), medicoId.ToString());
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(urlApiMedico);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(responseBody))
            {
                return JsonSerializer.Deserialize<MedicoResponse>(responseBody);
            }

            return null;
        }

        public async Task<bool> MedicoValido(Guid medicoId)
        {
            var medico = await ConsultarDadosMedico(medicoId);
            return medico != null;
        }
    }
}
