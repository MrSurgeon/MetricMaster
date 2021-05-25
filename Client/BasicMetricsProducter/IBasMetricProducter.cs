using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasicMetricsProducter
{
    interface IBasMetricProducter
    {
        Task<BasicMetricsField> PythonBasicMetrics();
    }
}
