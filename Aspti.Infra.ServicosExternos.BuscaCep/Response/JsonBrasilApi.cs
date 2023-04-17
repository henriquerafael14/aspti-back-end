using System.Text.Json.Serialization;

namespace Aspti.Infra.ServicosExternos.BuscaCep.Response
{
	public class JsonBrasilApi
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("state")]
        public string UF { get; set; }

        [JsonPropertyName("city")]
        public string Cidade { get; set; }

        [JsonPropertyName("neighborhood")]
        public string Bairro { get; set; }

        [JsonPropertyName("street")]
        public string Logradouro { get; set; }

        [JsonPropertyName("service")]
        public string Service { get; set; }

        [JsonPropertyName("errors")]
        public string Errors { get; set; }
    }
}
