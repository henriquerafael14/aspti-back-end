using Newtonsoft.Json;

namespace Aspti.Application.Response
{
	public class Response<T>
	{
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("data")]
		public T Data { get; set; }
	}

}
