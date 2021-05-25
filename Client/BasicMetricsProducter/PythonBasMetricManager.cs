using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BasicMetricsProducter
{
    class PythonBasMetricManager : IBasMetricProducter
    {
        private BasicMetricsField _basicMetricsField;
        public PythonBasMetricManager()
        {
            _basicMetricsField = new BasicMetricsField();
        }
        public async Task<BasicMetricsField> PythonBasicMetrics()
        {
            String fileName = @"C:\TestFields\PythonTest\PythonTest\PythonTest.py";

            string line;
            using (var file = new StreamReader(fileName))
            {
                while ((line = await file.ReadLineAsync()) != null)
                {
                    _basicMetricsField.CodeRowCount++;
                }
            }
            return _basicMetricsField;
        }
    }
}
