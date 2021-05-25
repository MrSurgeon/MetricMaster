using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasMetricsProductor
{
    public interface IMetricsStartable
    {
        Task<RawMetrics> StartMetricCalculateAsync(string path);
    }
}
