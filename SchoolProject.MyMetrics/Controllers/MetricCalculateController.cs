using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.MetricsServices;
using SchoolProject.MyMetrics.ViewModels;

namespace SchoolProject.MyMetrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricCalculateController : BaseController
    {
        private readonly IRawMetricable _rawMetricable;
        private readonly ICCMetricable _ccMetricable;
        private readonly IHalsteadMetricable _halsteadMetricable;
        private readonly IMaintainMetricable _maintainMetricable;
        public MetricCalculateController(IRawMetricable rawMetricable, ICCMetricable ccMetricable, IHalsteadMetricable halsteadMetricable, IMaintainMetricable maintainMetricable)
        {
            _rawMetricable = rawMetricable;
            _ccMetricable = ccMetricable;
            _halsteadMetricable = halsteadMetricable;
            _maintainMetricable = maintainMetricable;
        }

        [HttpGet]
        public async Task<IActionResult> ViewMetrics()
        {
            string path= "";
            //if (path == null)
            //    ModelState.AddModelError(string.Empty, "Path değeri boş geçilmiştir.");
            List<string> pathList = new List<string>();
            pathList.Add(path);
            try
            {
                var metricsViewModel = new MetricsViewModel()
                {
                    RawMetricsList = await _rawMetricable.ExecuteRawMetrics(pathList),
                    CCMetricsList = await _ccMetricable.ExecuteCCMetrics(pathList),
                    HalsteadMetricsList = await _halsteadMetricable.ExecuteHalsteadMetrics(pathList),
                    MaintainMetricsList = await _maintainMetricable.ExecuteMaintainMetrics(pathList)
                };
                return Ok(metricsViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Data);
            }
        }
    }
}
