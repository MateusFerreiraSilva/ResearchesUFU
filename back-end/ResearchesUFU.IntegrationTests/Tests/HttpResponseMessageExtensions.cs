using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ResearchesUFU.API;

namespace ResearchesUFU.IntegrationTests;

public static class HttpResponseMessageExtensions
{
    
    public static async Task<T> GetContent<T>(this HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage?.Content == null)
        {
            throw new NullReferenceException(Constants.NO_CONTENT_EXCEPTION_MESSAGE);
        }
    
        var messageString = await httpResponseMessage.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<T>(messageString);
    
        return content;
    }
}