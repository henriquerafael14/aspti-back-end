using Aspti.Infra.ServicosExternos.BuscaCep.Response;
using System.Threading.Tasks;

namespace Aspti.Infra.ServicosExternos.BuscaCep.Repository
{
	public interface ICEPService
    {
        Task<JsonViaCep> BuscarViaCepAsync(string CEP);
        Task<JsonBrasilApi> BuscarBrasilApiAsync(string cep);
    }
}
