namespace ResearchesUFU.API.Utils
{
    public static class HttpUtils<T>
    {
        public static HttpResponseBase<T> GenerateHttpSuccessResponse(int status, T content)
        {
            return new HttpResponseBase<T>
            {
                HttpStatusCode = status,
                Content = content,
            };
        }
        
        public static HttpResponseBase<T> GenerateHttpSuccessResponse()
        {
            return new HttpResponseBase<T>
            {
                HttpStatusCode = StatusCodes.Status200OK
            };
        }
        
        public static HttpResponseBase<T> GenerateHttpSuccessResponse(T content)
        {
            return new HttpResponseBase<T>
            {
                HttpStatusCode = StatusCodes.Status200OK,
                Content = content,
            };
        }

        public static HttpResponseBase<T> GenerateHttpBadRequestResponse()
        {
            return new HttpResponseBase<T>
            {
                HttpStatusCode = StatusCodes.Status400BadRequest
            };
        }
        
        public static HttpResponseBase<T> GenerateHttpBadRequestResponse(T content)
        {
            return new HttpResponseBase<T>
            {
                HttpStatusCode = StatusCodes.Status400BadRequest,
                Content = content,
            };
        }

        public static HttpResponseBase<T> GenerateHttpErrorResponse()
        {
            return new HttpResponseBase<T>
            {
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
