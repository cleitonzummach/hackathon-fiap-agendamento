using FIAP.Hackathon.Application.Interfaces;
using FIAP.Hackathon.Application.Responses;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FIAP.Hackathon.Application.Services
{
    public class HttpPacienteService : IHttpPacienteService
    {
        private readonly IConfiguration _configuration;

        public HttpPacienteService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<PacienteResponse?> ConsultarDadosPaciente(Guid pacienteId)
        {
            var urlApiPaciente = Path.Combine(Environment.GetEnvironmentVariable("UrlApiPaciente"), pacienteId.ToString());
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(urlApiPaciente);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(responseBody))
            {
                return JsonSerializer.Deserialize<PacienteResponse>(responseBody);
            }

            return null;
        }

        public async Task<bool> PacienteValido(Guid pacienteId)
        {
            var paciente = await ConsultarDadosPaciente(pacienteId);
            return paciente != null;
        }
    }
}
