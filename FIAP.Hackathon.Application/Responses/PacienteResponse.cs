using System.Text.Json.Serialization;

namespace FIAP.Hackathon.Application.Responses
{
    public class PacienteResponse
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("endereco")]
        public string Endereco { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
