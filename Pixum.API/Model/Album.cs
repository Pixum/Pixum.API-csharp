using System;
using System.Collections.Generic;
using Pixum.API.Model.Image;

namespace Pixum.API.Model.Album
{
    public class PSIAlbumInfoDetails
    {
        public string album_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string share_status { get; set; }
        public int imagecount { get; set; }
        public PSIAlbumInfoCover cover { get; set; }
    }

    public class PSIAlbumInfoCover
    {
        public PSIImageInfoThumb thumb_small { get; set; }
        public PSIImageInfoThumb thumb_big { get; set; }
        public string aspect { get; set; }
    }

    public class PSIAlbumInfo
    {
        public List<PSIAlbumInfoDetails> items { get; set; }
        public PSIMeta meta { get; set; }
    }

    public class PSIAlbumInfoReadResponse
    {
        public PSIAlbumInfo inbox { get; set; }
        public PSIAlbumInfo trash { get; set; }
        public PSIAlbumInfo album { get; set; }
        public string inboxFilter { get; set; }
        public PSIAlbumInfo sharedAlbum { get; set; }
    }

    public class PSIAlbumInfoSingleResponse
    {
        public string album_id { get; set; }
        public int imagecount { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public DateTime created_at { get; set; }
        public string share_status { get; set; }
        public int visitor_count { get; set; }
        public string password { get; set; }
        public string order_by { get; set; }
        public string order_direction { get; set; }
        public PSIImageInfoSingleResponse cover { get; set; }
    }

    public class PSIAlbumInfoCreateResponse
    {
        public int albumId { get; set; }
    }

    public class PSIAlbumImagesReadResponse
    {
        public List<PSIImageInfoSingleResponse> items { get; set; }
        public PSIMeta meta { get; set; }
    }
}
