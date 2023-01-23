namespace ResearchesUFU.API.Utils
{
    public static class HttpUtils<T>
    {
        public static HttpResponseBase<T> GenerateHttpResponse(T? content)
        {
            var httpResponse = new HttpResponseBase<T>(content);

            try
            {
                if (content == null)
                {
                    httpResponse.HttpStatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    httpResponse.HttpStatusCode = StatusCodes.Status200OK;
                }

                return httpResponse;
            }
            catch (Exception ex)
            {
                return httpResponse; // HttpStatusCode will be the default (400) in this case
            }
        }

        public static HttpResponseBase<T> GenerateHttpSuccessResponse()
        {
            return new HttpResponseBase<T>(StatusCodes.Status200OK);
        }

        public static HttpResponseBase<T> GenerateHttpErrorResponse()
        {
            return new HttpResponseBase<T>(StatusCodes.Status500InternalServerError);
        }

        public static bool CheckIfIsValidHttpResponse(HttpResponseBase<T>? httpResponse)
        {
            if (
                httpResponse != null &&
                httpResponse.Content != null &&
                httpResponse.HttpStatusCode.Equals(StatusCodes.Status200OK)
            )
            {
                return true;
            }

            return false;
        }
    }
}
