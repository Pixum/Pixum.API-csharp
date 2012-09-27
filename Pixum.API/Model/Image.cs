using System;
using System.Collections.Generic;

namespace Pixum.API.Model.Image
{
    public class PSIImageInfoSingleResponse
    {
        public int imageid { get; set; }
        public string filename { get; set; }
        public string aspect { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int rotation { get; set; }
        public int hot { get; set; }
        public string status { get; set; }
        public List<string> albums { get; set; }
        public DateTime created { get; set; }

        public PSIImageInfoThumb thumb_small { get; set; }
        public PSIImageInfoThumb thumb_big { get; set; }
        public PSIImageInfoOriginal original { get; set; }
    }

    public class PSIImageInfoThumb
    {
        public int width { get; set; }
        public int height { get; set; }
        public int filesize { get; set; }
        public int rotation { get; set; }
        public string url { get; set; }
    }

    public class PSIImageInfoOriginal
    {
        public int width { get; set; }
        public int height { get; set; }
        public int filesize { get; set; }
        public int rotation { get; set; }
    }
}
