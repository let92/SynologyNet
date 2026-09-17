using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SynologyNet.Models.Responses.Photo
{
	public class SharingInfo
	{
		[JsonPropertyName("owner")]
		public Owner Owner { get; set; } = new();
		
		[JsonPropertyName("permission")]
		public List<Permission> Permission { get; set; } = new();
	}
}