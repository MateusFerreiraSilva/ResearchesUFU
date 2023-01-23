namespace ResearchesUFU.API.Utils
{
    public class HttpResponseBase<T>
    {
        public int HttpStatusCode { get; set; }

        public T? Content { get; set; }

        public HttpResponseBase(T? content)
        {
            Content = content;
            HttpStatusCode = StatusCodes.Status400BadRequest;
        }

        public HttpResponseBase(int httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
