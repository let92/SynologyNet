using System.Text.Json.Serialization;

namespace SynologyNet.Models.Requests.Photo.Filters;

/// <summary>
/// Album get filter with parameters for getting additional data
/// </summary>
public class AlbumGetFilter : IFilter
{
	// TODO: find way to build or obtain from user these fields
	// Example: "[\"sharing_info\",\"flex_section\",\"provider_count\",\"thumbnail\"]"
	private const string _additionalFields = "[\"sharing_info\"]";

	/// <summary>
	/// Passphrase of the album
	/// </summary>
	[JsonPropertyName("passphrase")]
	public string Passphrase { get; set; } = string.Empty;

	/// <summary>
	/// Additional data of the album to get
	/// </summary>
	[JsonPropertyName("additional")]
	public string Additional { get; set; } = _additionalFields;

	/// <summary>
	/// Category of the album
	/// </summary>
	[JsonPropertyName("category")]
	public string Category { get; set; } = string.Empty;

	/// <summary>
	/// Album get filter with parameters for getting additional data
	/// </summary>
	public AlbumGetFilter() { }

	/// <summary>
	/// Album get filter with parameters for getting additional data
	/// </summary>
	/// <param name="passphrase">Passphrase of the album</param>
	public AlbumGetFilter(string passphrase) : this()
	{
		Passphrase = passphrase;
	}

	/// <summary>
	/// Set album category to shared
	/// </summary>
	public void SetCategoryToShared()
	{
		Category = "shared";
	}
}