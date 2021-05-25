using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolProject.MyMetrics.Models.Entities;
using CyclomaticComplexity = SchoolProject.HttpClientMetrics.Entities.CyclomaticComplexity;
using Halstead = SchoolProject.HttpClientMetrics.Entities.Halstead;
using Maintainability = SchoolProject.HttpClientMetrics.Entities.Maintainability;
using Raw = SchoolProject.HttpClientMetrics.Entities.Raw;

namespace SchoolProject.MyMetrics.ViewModels
{
    public class MetricsViewModel
    {
        public List<Raw> RawMetricsList { get; set; }
        public List<Halstead> HalsteadMetricsList { get; set; }
        public List<Maintainability> MaintainMetricsList { get; set; }
        public List<CyclomaticComplexity[]> CCMetricsList { get; set; }
    }
}
