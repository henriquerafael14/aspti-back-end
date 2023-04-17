using Aspti.Infra.ServicosExternos.BuscaCep.Repository;
using Aspti.Infra.ServicosExternos.BuscaCep.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Aspti.Infra.ServicosExternos.BuscaCep
{
	public class CEPService : ICEPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CEPService> _logger;

        public CEPService(HttpClient httpClient, IConfiguration configuration, ILogger<CEPService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<JsonViaCep> BuscarViaCepAsync(string CEP)
        {
            var urlBase = _configuration["BuscarCep:ViaCep"];
            var url = $"{urlBase}{CEP}/json/";
            var result = await BuscarEnderecoAsync<JsonViaCep>(url);
            if (result != null && string.IsNullOrWhiteSpace(result.Erro))
                return result;
            return null;
        }

        public async Task<JsonBrasilApi> BuscarBrasilApiAsync(string CEP)
        {
            var urlBase = _configuration["BuscarCep:BrasilApi"];
            var url = $"{urlBase}{CEP}";
            var result = await BuscarEnderecoAsync<JsonBrasilApi>(url);
            if (result != null && string.IsNullOrWhiteSpace(result.Errors))
                return result;
            return null;
        }

        private async Task<TResult> BuscarEnderecoAsync<TResult>(string url) where TResult : class
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TResult>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu uma exceção ao buscar o CEP");
                return null;
            }
        }
    }
}
