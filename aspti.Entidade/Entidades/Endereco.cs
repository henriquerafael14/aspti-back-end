using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Domain.Entidades
{
	public class Endereco : EntidadeBase
	{
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string CEP { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CidadeNome { get; private set; }
        public string EstadoNome { get; private set; }
        public string CidadeCodigoIBGE { get; private set; }

        public void AtualizarLogradouro(string logradouro)
        {
            Logradouro = logradouro;
        }

        public void AtualizarNumero(string numero)
        {
            Numero = numero;
        }

        public void AtualizarCEP(string cep)
        {
            CEP = cep;
        }

        public void AtualizarComplemento(string complemento)
        {
            Complemento = complemento;
        }

        public void AtualizarBairro(string bairro)
        {
            Bairro = bairro;
        }

        public void AtualizarCidade(string cidadeNome)
        {
            CidadeNome = cidadeNome;
        }

        public void AtualizarEstado(string estadoNome)
        {
            EstadoNome = estadoNome;
        }

        public void AlterarCidadeCodigoIbge(string cidadeCodigoIbge)
        {
            CidadeCodigoIBGE = cidadeCodigoIbge;
        }

        public void AtualizarEndereco(string logradouro, string numero, string cep, string complemento, string bairro, string cidade, string estado)
        {
            AtualizarLogradouro(logradouro);
            AtualizarNumero(numero);
            AtualizarCEP(cep);
            AtualizarComplemento(complemento);
            AtualizarBairro(bairro);
            AtualizarCidade(cidade);
            AtualizarEstado(estado);
        }
    }
}
