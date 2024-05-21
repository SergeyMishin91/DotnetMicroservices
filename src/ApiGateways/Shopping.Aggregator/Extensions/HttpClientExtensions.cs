using System.Text.Json;

namespace Shopping.Aggregator.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new ApplicationException(
                    $"Somthing went wrong calling the API: {httpResponseMessage.ReasonPhrase}");
            }

            var dataAsString = await httpResponseMessage.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(
                dataAsString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
