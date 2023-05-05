namespace ResearchesUFU.API.Utils
{
    public static class HttpUtils<T>
    {
        public static HttpResponseBase<T> GenerateHttpResponse(int status)
        {
            return new HttpResponseBase<T>
            {
                HttpStatusCode = status
            };
        }
        public static HttpResponseBase<T> GenerateHttpResponse(int status, T content)
        {
            return new HttpResponseBase<T>
            {
                HttpStatusCode = status,
                Content = content,
            };
        }
        
        public static HttpResponseBase<T> GenerateHttpSuccessResponse()
        {
            return GenerateHttpResponse(StatusCodes.Status200OK);
        }
        
        public static HttpResponseBase<T> GenerateHttpSuccessResponse(T content)
        {
            return GenerateHttpResponse(StatusCodes.Status200OK, content);
        }

        public static HttpResponseBase<T> GenerateHttpBadRequestResponse()
        {
            return GenerateHttpResponse(StatusCodes.Status400BadRequest);
        }
        
        public static HttpResponseBase<T> GenerateHttpBadRequestResponse(T content)
        {
            return GenerateHttpResponse(StatusCodes.Status400BadRequest, content);
        }

        public static HttpResponseBase<T> GenerateHttpErrorResponse()
        {
            return GenerateHttpResponse(StatusCodes.Status500InternalServerError);
        }
    }
}
