using System.Text.Json.Serialization;

namespace FIAP.Hackathon.Application.Responses
{
    public class MedicoResponse
    {
        [JsonPropertyName("especialidade")]
        public string Especialidade { get; set; }
        
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        
        [JsonPropertyName("crm")]
        public string CRM { get; set; }
        
        [JsonPropertyName("endereco")]
        public string Endereco { get; set; }
    }
}
