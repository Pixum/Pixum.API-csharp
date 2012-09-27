using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Pixum.API.Model.Album;
using Pixum.API.Model;

namespace Pixum.API.Services
{
    /// <summary>
    /// Holds Album specific methods.
    /// </summary>
    public class Album : PixumApiBase
    {
        const string ServiceName = "album";

        internal Album(IRestClient client) : base(client)
        {
            
        }

        /// <summary>
        /// Get the albums from a user.
        /// </summary>
        /// <param name="start">Start position.</param>
        /// <param name="limit">Result limit.</param>
        /// <param name="withInbox">Include Inbox.</param>
        /// <param name="withInboxFilter">Include Inbox Filter.</param>
        /// <param name="withTrash">Include Trash.</param>
        /// <param name="withAlbum">Include Albums.</param>
        /// <param name="withSharedAlbum">Include shared albums.</param>
        /// <returns></returns>
        public Task<PSIAlbumInfoReadResponse> GetAlbums(int start = 0, int limit = 25, bool withInbox = true, bool withInboxFilter = false, bool withTrash = true, bool withAlbum = true, bool withSharedAlbum = false)
        {
            var request = CreateRestPSIRequest(ServiceName, "info", 3);

            request.AddParameter(new {
                start = start,
                limit = limit,
                withInbox = Convert.ToInt32(withInbox),
                withInboxFilter = Convert.ToInt32(withInboxFilter),
                withTrash = Convert.ToInt32(withTrash),
                withAlbum = Convert.ToInt32(withAlbum),
                withSharedAlbum = Convert.ToInt32(withSharedAlbum)
            });

            return ExecuteAsync<PSIAlbumInfoReadResponse>(request);
        }

        /// <summary>
        /// Gets data for a single album.
        /// </summary>
        /// <param name="albumId">The albumId of the album.</param>
        /// <returns>Album data.</returns>
        public Task<PSIAlbumInfoSingleResponse> GetAlbum(string albumId)
        {
            var request = CreateNonRestPSIRequest(ServiceName, "info", "single");

            request.AddParameter("albumId", albumId);

            return ExecuteAsync<PSIAlbumInfoSingleResponse>(request);
        }

        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <param name="title">The title for the album.</param>
        /// <param name="description">The description for the album.</param>
        /// <param name="sharingStatus">The sharing status for the album.</param>
        /// <param name="sharingPassword">A password for the album used when shared.</param>
        /// <returns>Album id for the new album.</returns>
        public Task<PSIAlbumInfoCreateResponse> CreateAlbum(string title, string description = null, string sharingStatus = null, string sharingPassword = null)
        {
            // TODO: Make sharingStatus an ENUM

            var request = CreateRestPSIRequest(ServiceName, "info");

            request.Method = Method.POST;
            request.AddParameter(new {
                title = title,
                description = description,
                sharingStatus = sharingStatus,
                sharingPassword = sharingPassword
            });

            return ExecuteAsync<PSIAlbumInfoCreateResponse>(request);
        }

        /// <summary>
        /// Deletes a album.
        /// </summary>
        /// <param name="albumId">The album for the to be deleted album.</param>
        /// <returns>Empty response.</returns>
        public Task<List<dynamic>> DeleteAlbum(int albumId)
        {
            var request = CreateRestPSIRequest(ServiceName, "info");

            request.Method = Method.DELETE;
            request.AddParameter("albumId", albumId);

            return ExecuteAsync<List<dynamic>>(request);
        }

        /// <summary>
        /// Updates album information.
        /// </summary>
        /// <param name="albumId">The id of the album.</param>
        /// <param name="title">The new title.</param>
        /// <param name="description">The new description.</param>
        /// <param name="coverImageId">The new cover image.</param>
        /// <returns>Empty response.</returns>
        public Task<List<dynamic>> SetAlbumData(int albumId, string title = null, string description = null, int? coverImageId = null)
        {
            var request = CreateNonRestPSIRequest(ServiceName, "info", "setData");

            request.Method = Method.PUT;
            request.AddParameter(new
            {
                albumId = albumId,
                title = title,
                description = description,
                coverImageId = coverImageId
            });

            return ExecuteAsync<List<dynamic>>(request);
        }

        /// <summary>
        /// Returns the images from an album.
        /// </summary>
        /// <param name="albumId">The id of the album.</param>
        /// <param name="start">Start position of images.</param>
        /// <param name="limit">Result limit.</param>
        /// <param name="order">Order of the result.</param>
        /// <returns></returns>
        public Task<PSIAlbumImagesReadResponse> GetAlbumImages(string albumId, int start = 0, int limit = 25, string order = "upload")
        {
            var request = CreateNonRestPSIRequest(ServiceName, "images", "read", 2);

            request.AddParameter(new {
                id = albumId,
                start = start,
                limit = limit,
                order = order
            });

            return ExecuteAsync<PSIAlbumImagesReadResponse>(request);
        }
    }
}