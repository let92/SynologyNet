using System.Text.Json.Serialization;

namespace SynologyNet.Models.Responses.Photo
{
	public class Permission
	{
		[JsonPropertyName("db_id")]
		public long DbId { get; set; }

		[JsonPropertyName("id")]
		public long Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("role")]
		public string Role { get; set; } = string.Empty;

		[JsonPropertyName("type")]
		public string Type { get; set; } = string.Empty;
	}
}