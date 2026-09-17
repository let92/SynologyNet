using System.Text.Json.Serialization;

namespace SynologyNet.Models.Responses.Photo
{
	public class AlbumAdditional
	{
		[JsonPropertyName("sharing_info")]
		public SharingInfo? SharingInfo { get; set; }
	}
}