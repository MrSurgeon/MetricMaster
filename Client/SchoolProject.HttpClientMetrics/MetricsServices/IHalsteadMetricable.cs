using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.Entities;

namespace SchoolProject.HttpClientMetrics.MetricsServices
{
    public interface IHalsteadMetricable
    {
        Task<List<Halstead>> ExecuteHalsteadMetrics(List<string> pathList);
    }
}
