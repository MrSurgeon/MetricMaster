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
    public class RadonHalsteadMetricsManager : HttpClientBaseManager, IHalsteadMetricable
    {
        public async Task<List<Halstead>> ExecuteHalsteadMetrics(List<string> pathList)
        {
            var pathListJson = JsonSerializer.Serialize(pathList);
            var importedHalsteadMetricsList = new List<Halstead>();
            var requestContent = new StringContent(pathListJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("hc", requestContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var rawConvert = JsonDocument.Parse(content);

            foreach (var path in pathList)
            {
                var jsonHalsteadArray = rawConvert.RootElement.GetProperty(path).EnumerateObject()
                .Where(it => it.Name.Contains("total"))
                .SelectMany(sm => sm.Value.EnumerateArray().Select(s => s.GetDouble()))
                .ToArray();

                importedHalsteadMetricsList.Add(new Halstead()
                {
                    N1 = jsonHalsteadArray[2],
                    N2 = jsonHalsteadArray[3],
                    H1 = jsonHalsteadArray[0],
                    H2 = jsonHalsteadArray[1],
                    Vocabulary = jsonHalsteadArray[4],
                    Length = jsonHalsteadArray[5],
                    CalculatedLength = jsonHalsteadArray[6],
                    Volume = jsonHalsteadArray[7],
                    Difficulty = jsonHalsteadArray[8],
                    Effort = jsonHalsteadArray[9],
                    Time = jsonHalsteadArray[10],
                    Bugs = jsonHalsteadArray[11]
                });
            }

            return importedHalsteadMetricsList;
        }
    }
}
