using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SchoolProject.MyMetrics.Models.Entities;

namespace SchoolProject.MyMetrics.HttpClientServices
{
    public class HttpClientMetricsOperator : IHttpClientServiceImplementation
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly JsonSerializerOptions _options;
        public HttpClientMetricsOperator()
        {
            _httpClient.BaseAddress = new Uri("http://127.0.0.1:5000/api/metrics/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
        public async Task Execute(List<string> pathList)
        {
            //await GetCompanies();
            //await GetCompaniesWithXMLHeader();
            //return await CalculateRawMetrics(pathList);
            //await CalculateHalstead(pathList);
            //await CalculateMaintainability(pathList);
            await CalculateComplexity(pathList);
        }
        public async Task<Raw> CalculateRawMetrics(List<string> pathList)
        {
            var pathListJson = JsonSerializer.Serialize(pathList);


            var request = new HttpRequestMessage(HttpMethod.Post, "raw");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(pathListJson, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var rawConvert = JsonDocument.Parse(content);
            var jsonRootBody = rawConvert.RootElement.GetProperty(pathList[0]);

            var rawMetrics = new Raw()
            {
                Blank = jsonRootBody.GetProperty("blank").GetUInt32(),
                Lloc = jsonRootBody.GetProperty("lloc").GetUInt32(),
                Loc = jsonRootBody.GetProperty("loc").GetUInt32(),
                Multi = jsonRootBody.GetProperty("multi").GetUInt32(),
                Sloc = jsonRootBody.GetProperty("sloc").GetUInt32(),
                Comments = jsonRootBody.GetProperty("comments").GetUInt32(),
                SingleComments = jsonRootBody.GetProperty("single_comments").GetUInt32()
            };

            return rawMetrics;

        }

        public async Task<List<CyclomaticComplexity>> CalculateComplexity(List<string> pathList)
        {

            var pathListJson = JsonSerializer.Serialize(pathList);
            var requestContent = new StringContent(pathListJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("cc", requestContent);
            response.EnsureSuccessStatusCode();

            var convertedJsonDocument = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var pathBody = convertedJsonDocument.RootElement.GetProperty(pathList[0]);
            var cyclomaticComplexityList = new List<CyclomaticComplexity>();
            var jsonCcFunctionsArray = pathBody.EnumerateArray()
                                                .Select(sm => new CyclomaticComplexity()
                                                {
                                                    name = sm.GetProperty("name").GetString(),
                                                    rank = sm.GetProperty("rank").GetString(),
                                                    lineno = sm.GetProperty("lineno").GetInt32(),
                                                })
                                                .ToList();
            return jsonCcFunctionsArray;
        }

        public async Task CalculateHalstead(List<string> pathList)
        {

            var pathListJson = JsonSerializer.Serialize(pathList);
            var requestContent = new StringContent(pathListJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("hc", requestContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var rawConvert = JsonDocument.Parse(content);

            var jsonHalsteadArray = rawConvert.RootElement.GetProperty(pathList[0]).EnumerateObject()
                .Where(it => it.Name.Contains("total"))
                .SelectMany(sm => sm.Value.EnumerateArray().Select(s => s.GetDouble()))
                .ToArray();

            var halsteadMetrics = new Halstead()
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
            };
        }

        public async Task CalculateMaintainability(List<string> pathList)
        {
            var pathListJson = JsonSerializer.Serialize(pathList);
            var requestContent = new StringContent(pathListJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("mi", requestContent);
            response.EnsureSuccessStatusCode();

            var convertedJsonDocument = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var pathBody = convertedJsonDocument.RootElement.GetProperty(pathList[0]);
            var maintainability = new Maintainability()
            {
                MaintainScore = pathBody.GetProperty("mi").GetDouble(),
                Rank = pathBody.GetProperty("rank").GetString()
            };
        }
    }
}
