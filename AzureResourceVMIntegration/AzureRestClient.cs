using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AzureResourceVMIntegration
{
    internal class AzureRestClient
    {
        internal static async Task<string> ExecuteRequest(string accessToken, string url)
        {
            string serviceBaseAddress = url;
            string responseMessage = null;

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(serviceBaseAddress);

            if (response.IsSuccessStatusCode)
            {
                responseMessage = await response.Content.ReadAsStringAsync();
                return responseMessage;
            }
            else
            {
                responseMessage = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return "Access denied.";
                }
                return responseMessage;
            }
        }
    }
}
