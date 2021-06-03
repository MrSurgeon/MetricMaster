using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.Entities;
using SchoolProject.HttpClientMetrics.MetricsServices;

namespace SchoolProject.HttpClientMetrics.HttpClientServices
{
    public class RadonMaintainMetricsManager : HttpClientBaseManager, IMaintainMetricable
    {
        public async Task<List<Maintainability>> ExecuteMaintainMetrics(List<string> pathList)
        {
            var pathListJson = JsonSerializer.Serialize(pathList);
            var importedMaintainMetricsList = new List<Maintainability>();
            var requestContent = new StringContent(pathListJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("mi", requestContent);
            response.EnsureSuccessStatusCode();

            var convertedJsonDocument = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            foreach (var path in pathList)
            {
                var pathBody = convertedJsonDocument.RootElement.GetProperty(path);
                importedMaintainMetricsList.Add(new Maintainability()
                {
                    MaintainScore = pathBody.GetProperty("mi").GetDouble(),
                    Rank = pathBody.GetProperty("rank").GetString()
                }); 
            }

            return importedMaintainMetricsList;
        }
    }
}
