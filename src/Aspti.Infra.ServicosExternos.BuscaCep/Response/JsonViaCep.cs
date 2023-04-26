using System.Text.Json.Serialization;

namespace Aspti.Infra.ServicosExternos.BuscaCep.Response
{
	public class JsonViaCep
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }

        [JsonPropertyName("localidade")]
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
        public string DDD { get; set; }
        public string Siafi { get; set; }
        public string Erro { get; set; }
    }

}
