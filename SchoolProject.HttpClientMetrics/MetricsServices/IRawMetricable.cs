using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.Entities;

namespace SchoolProject.HttpClientMetrics.MetricsServices
{
    public interface IRawMetricable
    {
        Task<List<Raw>> ExecuteRawMetrics(List<string> pathList);
    }
}
