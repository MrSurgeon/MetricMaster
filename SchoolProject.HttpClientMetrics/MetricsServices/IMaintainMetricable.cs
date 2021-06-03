using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.Entities;

namespace SchoolProject.HttpClientMetrics.MetricsServices
{
    public interface IMaintainMetricable
    {
        Task<List<Maintainability>> ExecuteMaintainMetrics(List<string> pathList);
    }
}
