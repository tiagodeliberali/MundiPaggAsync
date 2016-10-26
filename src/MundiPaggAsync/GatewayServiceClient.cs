using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using GatewayApiClient.DataContracts;
using GatewayApiClient.Utility;

namespace MundiPaggAsync
{
    public class GatewayServiceClient
    {
        private Guid MerchantKey { get; set; }
        private Uri SalesEndPointUrl { get; set; }

        public GatewayServiceClient(Guid merchantKey, Uri endPointUrl)
        {
            MerchantKey = merchantKey;
            SalesEndPointUrl = new Uri(endPointUrl.ToString() + "/Sale");
        }

        public async Task<HttpResponse<CreateSaleResponse>> CreateSale(CreateSaleRequest createSaleRequest)
        {
            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(Serialize(createSaleRequest), Encoding.UTF8);

                content.Headers.Add("MerchantKey", MerchantKey.ToString());

                content.Headers.Remove("Content-type");
                content.Headers.Add("Content-type", "application/json");

                //content.Headers.Add("Accept", "application/json");

                var result = await client.PostAsync(SalesEndPointUrl, content);

                var stream = await result.Content.ReadAsStreamAsync();
                var rawContent = await result.Content.ReadAsStringAsync();

                return new HttpResponse<CreateSaleResponse>(Deserialize<CreateSaleResponse>(stream), rawContent, result.StatusCode);
            }
        }

        public string Serialize(object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            string json;

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            return json;
        }

        public T Deserialize<T>(Stream obj)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            return (T)serializer.ReadObject(obj);
        }
    }
}
