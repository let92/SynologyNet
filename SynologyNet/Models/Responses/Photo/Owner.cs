using System.Text.Json.Serialization;

namespace SynologyNet.Models.Responses.Photo
{
	public class Owner
	{
		/// <summary>
		/// Id of album user owner
		/// </summary>
		[JsonPropertyName("id")]
		public int Id { get; set; }

		/// <summary>
		/// Name of album user owner
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}