
namespace Pixum.API.Model.Auth
{
    public class PSIUserLogin
    {
        public string userId { get; set; }
        public string lastLogin { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }

    public class PSISessionId
    {
        public string sessionId { get; set; }
    }

    public class PSILoginToken
    {
        public string token { get; set; }
    }
}
