namespace Aspti.Infra.CrossCutting.Extensions
{
	public static class StringExtensions
	{
		public static string ApenasNumeros(this string str)
		{
			return new string(str.Where(char.IsDigit).ToArray());
		}
	}

}
