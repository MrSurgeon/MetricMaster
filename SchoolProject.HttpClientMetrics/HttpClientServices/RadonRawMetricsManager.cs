using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.Entities;
using SchoolProject.HttpClientMetrics.MetricsServices;

namespace SchoolProject.HttpClientMetrics.HttpClientServices
{
    public class RadonRawMetricsManager : HttpClientBaseManager, IRawMetricable
    {
        public async Task<List<Raw>> ExecuteRawMetrics(List<string> pathList)
        {
            var pathListJson = JsonSerializer.Serialize(pathList);
            var importRawMetricList = new List<Raw>();
            var request = new HttpRequestMessage(HttpMethod.Post, "raw");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(pathListJson, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var rawConvert = JsonDocument.Parse(content);

            foreach (var path in pathList)
            {
                var jsonRootBody = rawConvert.RootElement.GetProperty(path);
                importRawMetricList.Add(new Raw()
                {
                    Blank = jsonRootBody.GetProperty("blank").GetUInt32(),
                    Lloc = jsonRootBody.GetProperty("lloc").GetUInt32(),
                    Loc = jsonRootBody.GetProperty("loc").GetUInt32(),
                    Multi = jsonRootBody.GetProperty("multi").GetUInt32(),
                    Sloc = jsonRootBody.GetProperty("sloc").GetUInt32(),
                    Comments = jsonRootBody.GetProperty("comments").GetUInt32(),
                    SingleComments = jsonRootBody.GetProperty("single_comments").GetUInt32()
                });
            }
            return importRawMetricList;
        }
    }
}
