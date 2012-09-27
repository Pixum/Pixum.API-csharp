namespace Pixum.API.Model
{
    public class PSIResponse<T>
    {
        public int code { get; set; }
        public T data { get; set; }
        public string message { get; set; }
    }

    public class PSIEmptyResponse
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class PSIResult<T>
    {
        public int version { get; set; }
        public float time { get; set; }
        public PSIResponse<T> response { get; set; }
    }

    public class PSIResult
    {
        public int version { get; set; }
        public float time { get; set; }
        public PSIEmptyResponse response { get; set; }
    }

    public class PSIMeta
    {
        public int start { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
    }
}