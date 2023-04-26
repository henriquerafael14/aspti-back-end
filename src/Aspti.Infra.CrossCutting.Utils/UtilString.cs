using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aspti.Infra.CrossCutting.Utils
{
	public static class UtilString
	{
		public static string SemFormatacao(string Codigo)
		{
			if (string.IsNullOrEmpty(Codigo))
				return string.Empty;

			var rgx = new Regex("[^a-zA-Z0-9]");
			return rgx.Replace(Codigo, "");
		}

		public static string FormatacaoCpfCnpj(string cpfCnpj)
		{
			if (string.IsNullOrEmpty(cpfCnpj)) return cpfCnpj;

			return (cpfCnpj.Length == 11) ? FormatCPF(cpfCnpj) : FormatCNPJ(cpfCnpj);
		}

		public static string FormatCNPJ(string CNPJ)
		{
			return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
		}

		public static string FormatCPF(string CPF)
		{
			return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
		}

		public static string NovoNomeUsuario(string nome)
		{
			var str = nome.Split(" ");
			var random = new Random();
			return string.Join(".", str.First(), str.Last(), random.Next(100, 999)).ToLower();
		}
	}
}
