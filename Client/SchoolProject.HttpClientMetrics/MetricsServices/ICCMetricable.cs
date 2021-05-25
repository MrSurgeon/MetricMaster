using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.Entities;

namespace SchoolProject.HttpClientMetrics.MetricsServices
{
    public interface ICCMetricable
    {
        Task<List<CyclomaticComplexity[]>> ExecuteCCMetrics(List<string> pathList);
    }
}
