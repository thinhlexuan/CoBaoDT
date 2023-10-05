using CBClient.Library;
using CBClient.Models;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CBClient.Services
{

    public static class HttpHelper
    {
        // In my case this is https://localhost:44366/
        //List<ClaimTerm> claimTerms = await HttpHelper.Get<List<ClaimTerm>>("/api/values/");
        //ClaimTerm processedClaimImage = await HttpHelper.Get<ClaimTerm>($"/api/values/{id}");
        //await HttpHelper.Post<Setting>($"/api/values/{id}", setting);
        //await HttpHelper.Delete($"/api/values/{id}");
        //private async void CongTac()
        //{
        //    //List<CongTac> congtacs = await HttpHelper.Get<List<CongTac>>("/api/CongTacs/");
        //    //CongTac congtac = await HttpHelper.Get<CongTac>($"/api/CongTacs/{11}");
        //    //congtac.CongTacId = "12";
        //    //congtac.CongTacName = "Kiêm dồn sdi";
        //    //await HttpHelper.Post<CongTac>($"/api/CongTacs", congtac);
        //    //await HttpHelper.Put<CongTac>($"/api/CongTacs/{12}", congtac);
        //    //await HttpHelper.Delete($"/api/CongTacs/{12}");
        //}
        private static readonly string apiBasicUri = Configuration.UrlCBApi;
        public static List<T> GetList<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                List<T> obj = null;
                try
                {
                    var task = client.GetAsync(url)
                      .ContinueWith((taskwithresponse) =>
                      {
                          var response = taskwithresponse.Result;
                          var jsonString = response.Content.ReadAsStringAsync();
                          jsonString.Wait();
                          obj = JsonConvert.DeserializeObject<List<T>>(jsonString.Result);

                      });
                    task.Wait();

                }
                catch (Exception ex)
                {
                    DialogHelper.Error(ex.Message);
                }
                client.Dispose();
                return obj;
            }
        }
        public static async Task<T> Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                var result = await client.GetAsync(url).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                client.Dispose();
                return resultContent;
            }
        }
        public static async Task<string> GetString<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                var result = await client.GetAsync(url).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();                
                client.Dispose();
                return resultContentString;
            }
        }
        public static async Task<decimal> GetDecimal<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                var result = await client.GetAsync(url).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                client.Dispose();
                return decimal.Parse(resultContentString);
            }
        }
        public static async Task<T> Post<T>(string url, T contentValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                client.Dispose();
                return resultContent;
            }
        }
        public async static Task<T> Put<T>(string url, T stringValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                var content = new StringContent(JsonConvert.SerializeObject(stringValue), Encoding.UTF8, "application/json");
                var result = await client.PutAsync(url, content).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                client.Dispose();
                return resultContent;
            }
        }       
        public async static Task<T> Delete<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                var result = await client.DeleteAsync(url).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                client.Dispose();
                return resultContent;
            }
        }  
    }
}

