using System.Threading.Tasks;
using RestSharp;
using Pixum.API.Model;
using Pixum.API.Model.Image;
using System.Collections.Generic;
using System;

namespace Pixum.API.Services
{
    public enum ImageRotation : int
    {
        DEGREE_0 = 0,
        DEGREE_90 = 90,
        DEGREE_180 = 180,
        DEGREE_270 = 270,
    }

    /// <summary>
    /// Holds image specific methods.
    /// </summary>
    public class Image : PixumApiBase
    {
        const string ServiceName = "image";

        internal Image(IRestClient client) : base(client)
        {

        }

        /// <summary>
        /// Uploads an image into an album.
        /// </summary>
        /// <param name="imageData">Image data as a byte array.</param>
        /// <param name="filename">The filename of the image.</param>
        /// <param name="albumId">The album to upload the image into.</param>
        /// <returns>Information object for the uploaded image.</returns>
        public Task<PSIImageInfoSingleResponse> DataCreate(byte[] imageData, string filename, string albumId = null)
        {
            // TODO: Allow for progress function

            var request = CreateRestPSIRequest(ServiceName, "data", 3);

            request.Method = Method.POST;
            request.AddFile("file", imageData, filename);

            if (albumId != null)
            {
                request.AddParameter("albumId", albumId);
            }

            return ExecuteAsync<PSIImageInfoSingleResponse>(request);
        }

        /// <summary>
        /// Deletes given images.
        /// </summary>
        /// <param name="imageIds">The ids for the images to delete.</param>
        /// <returns></returns>
        public Task<List<dynamic>> DataDelete(int[] imageIds)
        {
            var request = CreateRestPSIRequest(ServiceName, "data");

            request.Method = Method.DELETE;

            foreach (var imageId in imageIds)
            {
                request.AddParameter("imageIds[]", imageId);
            }

            return ExecuteAsync<List<dynamic>>(request);
        }

        public Task<List<dynamic>> DataDelete(int imageId)
        {
            return DataDelete(new int[] { imageId });
        }

        /// <summary>
        /// Sets the title for the given image.
        /// </summary>
        /// <param name="imageId">The id of the image.</param>
        /// <param name="title">The new title for the image.</param>
        /// <returns></returns>
        public Task<PSIImageInfoSingleResponse> SetTitle(int imageId, string title)
        {
            var request = CreateNonRestPSIRequest(ServiceName, "data", "setTitle");

            request.AddParameter(new
            {
                imageId = imageId,
                title = title
            });

            return ExecuteAsync<PSIImageInfoSingleResponse>(request);
        }

        /// <summary>
        /// Sets the description for the given image.
        /// </summary>
        /// <param name="imageId">The id of the image.</param>
        /// <param name="description">The new description for the image.</param>
        /// <returns></returns>
        public Task<PSIImageInfoSingleResponse> SetDescription(int imageId, string description)
        {
            var request = CreateNonRestPSIRequest(ServiceName, "data", "setDescription");

            request.AddParameter(new
            {
                imageId = imageId,
                description = description
            });

            return ExecuteAsync<PSIImageInfoSingleResponse>(request);
        }

        /// <summary>
        /// Sets the rotation for the given image.
        /// </summary>
        /// <param name="imageId">The id of the image.</param>
        /// <param name="degrees">The new rotation for the image. Can only be 0, 90, 180 or 270.</param>
        /// <returns></returns>
        public Task<PSIImageInfoSingleResponse> SetRotation(int imageId, ImageRotation degrees)
        {
            var request = CreateNonRestPSIRequest(ServiceName, "data", "setRotation");

            request.AddParameter(new
            {
                imageId = imageId,
                degrees = (int)degrees
            });

            return ExecuteAsync<PSIImageInfoSingleResponse>(request);
        }

        /// <summary>
        /// Get image informations for given image id.
        /// </summary>
        /// <param name="imageId">The id of the image.</param>
        /// <param name="withDescription">Include description.</param>
        /// <returns></returns>
        public Task<PSIImageInfoSingleResponse> GetImage(int imageId, bool withDescription = true)
        {
            var request = CreateNonRestPSIRequest(ServiceName, "data", "single");

            request.AddParameter(new
            {
                imageId = imageId,
                withDescription = Convert.ToInt32(withDescription)
            });

            return ExecuteAsync<PSIImageInfoSingleResponse>(request);
        }

        /// <summary>
        /// Get image informations for given image specified by its position in an album.
        /// </summary>
        /// <param name="position">The position of the image.</param>
        /// <param name="albumId">The album where the image lives. Must be a number.</param>
        /// <param name="withDescription">Include description.</param>
        /// <returns></returns>
        public Task<PSIImageInfoSingleResponse> GetImageByPosition(int position, int albumId, bool withDescription = true)
        {
            var request = CreateNonRestPSIRequest(ServiceName, "data", "singleReadByAlbumPosition");

            request.AddParameter(new
            {
                albumId = albumId,
                position = position,
                withDescription = Convert.ToInt32(withDescription)
            });

            return ExecuteAsync<PSIImageInfoSingleResponse>(request);
        }
    }
}
