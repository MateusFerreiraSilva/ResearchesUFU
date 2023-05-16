namespace ResearchesUFU.API.Utils
{
    public class HttpResponseBase<T>
    {
        public int HttpStatusCode { get; set; }

        public T? Content { get; set; }
    }
}
