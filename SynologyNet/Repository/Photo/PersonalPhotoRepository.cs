﻿using RestSharp;
using SynologyNet.Attributes;
using SynologyNet.Helpers;
using SynologyNet.Models.Requests.Photo.Filters;
using SynologyNet.Models.Responses;
using SynologyNet.Models.Responses.Photo;
using System.Text.Json;
using System.Threading.Tasks;

namespace SynologyNet.Repository
{
	[SynologyRepository(DefaultPath = "entry.cgi", RequiresAuthentication = true)]
	class PersonalPhotoRepository : BaseRepository
	{
		[Request(Api = "SYNO.Foto.Browse.Folder", Method = "list")]
		public async Task<BaseDataResponse<ListObject<Folder>>> GetFolders(PagingFilter? pagingFilter = null, SortableFilter? sortableFilter = null)
		{
			pagingFilter ??= new();
			sortableFilter ??= new();

			return await _client.GetAsync<BaseDataResponse<ListObject<Folder>>>(PrepareRequest(pagingFilter, sortableFilter)) ?? new();
		}

		[Request(Api = "SYNO.Foto.Browse.NormalAlbum", Method = "create")]
		public async Task<BaseDataResponse<AlbumObject>> CreateNormalAlbum(string albumName)
		{
			var request = PrepareRequest();
			request.AddParameter("name", $"{albumName}");

			return await _client.GetAsync<BaseDataResponse<AlbumObject>>(request) ?? new();
		}

		[Request(Api = "SYNO.Foto.Browse.Album", Method = "list")]
		public async Task<BaseDataResponse<ListObject<Album>>> GetAlbums(PagingFilter? pagingFilter = null, SortableFilter? sortableFilter = null)
		{
			pagingFilter ??= new();
			sortableFilter ??= new();

			return await _client.GetAsync<BaseDataResponse<ListObject<Album>>>(PrepareRequest(pagingFilter, sortableFilter)) ?? new();
		}

		[Request(Api = "SYNO.Foto.Sharing.Misc", Method = "list_shared_with_me_album", Version = 1)]
		public async Task<BaseDataResponse<ListObject<Album>>> GetSharedAlbums(PagingFilter? pagingFilter = null, SortableFilter? sortableFilter = null)
		{
			pagingFilter ??= new();
			sortableFilter ??= new();

			return await _client.GetAsync<BaseDataResponse<ListObject<Album>>>(PrepareRequest(pagingFilter, sortableFilter)) ?? new();
		}

		[Request(Api = "SYNO.Foto.Browse.Item", Method = "list")]
		public async Task<BaseDataResponse<ListObject<Photo>>> GetAlbumPhotos(Album album, PagingFilter? pagingFilter = null)
		{
			pagingFilter ??= new();

			var request = PrepareRequest(pagingFilter);
			request.AddParameterIfNotNull("album_id", album.Passphrase == null || album.Passphrase == string.Empty ? album.Id : null);
			request.AddParameterIfNotNull("passphrase", album.Passphrase == null || album.Passphrase == string.Empty ? null : album.Passphrase);

			return await _client.GetAsync<BaseDataResponse<ListObject<Photo>>>(request) ?? new();
		}

		[Request(Api = "SYNO.Foto.Browse.Item", Method = "list")]
		public async Task<BaseDataResponse<ListObject<Photo>>> GetPhotos(PagingFilter? pagingFilter = null)
		{
			pagingFilter ??= new();

			var request = PrepareRequest(pagingFilter);
			request.AddParameter("type", "photo");

			return await _client.GetAsync<BaseDataResponse<ListObject<Photo>>>(request) ?? new();
		}

		[Request(Api = "SYNO.Foto.Browse.RecentlyAdded", Method = "list", Version = 3)]
		public async Task<BaseDataResponse<ListObject<Photo>>> GetRecentlyAddedPhotos(PagingFilter? pagingFilter = null)
		{
			pagingFilter ??= new();

			return await _client.GetAsync<BaseDataResponse<ListObject<Photo>>>(PrepareRequest(pagingFilter)) ?? new();
		}

		public Task<BaseDataResponse<byte[]>> DownloadPhoto(Photo photo, Album album)
		{
			return DownloadPhoto(photo.Id, album.Passphrase);
		}

		[Request(Api = "SYNO.Foto.Download", Method = "download")]
		public async Task<BaseDataResponse<byte[]>> DownloadPhoto(int photoId, string? passphrase)
		{
			var request = PrepareRequest();
			request.AddParameter("unit_id", $"[{photoId}]");
			request.AddParameterIfNotNull("passphrase", passphrase);

			var response = await _client.GetAsync(request);
			var dataResponse = new BaseDataResponse<byte[]>() { Success = true };

			if (response.ContentType == "application/json")
				dataResponse = JsonSerializer.Deserialize<BaseDataResponse<byte[]>>(response?.Content ?? "") ?? new();
			else
				dataResponse.Data = response.RawBytes;

			return dataResponse;
		}

		[Request(Api = "SYNO.Foto.Browse.NormalAlbum", Method = "add_item")]
		public async Task<BaseDataResponse<ErrorListObject>> AddItemToAlbum(Photo item, Album album)
		{
			var request = PrepareRequest();
			request.AddParameterIfNotNull("item", $"[{item.Id}]");

			// If album is owned and shared by other user, it must be used passphrase and not id of album.
			if (!string.IsNullOrWhiteSpace(album.Passphrase))
			{
				request.AddParameterIfNotNull("passphrase", album.Passphrase);
			}
			else
			{
				request.AddParameterIfNotNull("id", album.Id);
			}

			return await _client.GetAsync<BaseDataResponse<ErrorListObject>>(request) ?? new();
		}

		[Request(Api = "SYNO.Foto.Search.Search", Method = "list_item")]
		public async Task<BaseDataResponse<ListObject<Photo>>> SearchForPhotos(SearchFilter? searchFilter = null)
		{
			searchFilter ??= new();

			return await _client.GetAsync<BaseDataResponse<ListObject<Photo>>>(PrepareRequest(searchFilter)) ?? new();
		}
	}
}
