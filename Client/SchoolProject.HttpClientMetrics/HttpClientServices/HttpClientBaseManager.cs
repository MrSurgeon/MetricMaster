using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SchoolProject.HttpClientMetrics.HttpClientServices
{
    public class HttpClientBaseManager
    {
        protected static readonly HttpClient _httpClient= new HttpClient();
        protected readonly JsonSerializerOptions _options;
        public HttpClientBaseManager()
        {
            _httpClient.BaseAddress = new Uri("http://127.0.0.1:5000/api/metrics/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }
    }
}
