using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.Entities;
using SchoolProject.HttpClientMetrics.MetricsServices;

namespace SchoolProject.HttpClientMetrics.HttpClientServices
{
    public class RadonCCMetricsManager : HttpClientBaseManager, ICCMetricable
    {
        public async Task<List<CyclomaticComplexity[]>> ExecuteCCMetrics(List<string> pathList)
        {
            var importedCcMetricsList = new List<CyclomaticComplexity[]>();
            var pathListJson = JsonSerializer.Serialize(pathList);
            var requestContent = new StringContent(pathListJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("cc", requestContent);
            response.EnsureSuccessStatusCode();

            var convertedJsonDocument = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            foreach (var path in pathList)
            {
                var pathBody = convertedJsonDocument.RootElement.GetProperty(path);
                var cyclomaticComplexityList = new List<CyclomaticComplexity>();
                var jsonCcFunctionsArray = pathBody.EnumerateArray()
                    .Select(sm => new CyclomaticComplexity()
                    {
                        name = sm.GetProperty("name").GetString(),
                        rank = sm.GetProperty("rank").GetString(),
                        lineno = sm.GetProperty("lineno").GetInt32(),
                    })
                    .ToArray();
                importedCcMetricsList.Add(jsonCcFunctionsArray);
            }

            return importedCcMetricsList;
        }
    }
}
