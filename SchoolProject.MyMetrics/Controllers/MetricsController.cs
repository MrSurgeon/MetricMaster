using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SchoolProject.HttpClientMetrics.MetricsServices;
using SchoolProject.MyMetrics.Models;
using SchoolProject.MyMetrics.ViewModels;

namespace SchoolProject.MyMetrics.Controllers
{
    public class MetricsController : BaseController
    {
        private readonly IRawMetricable _rawMetricable;
        private readonly ICCMetricable _ccMetricable;
        private readonly IHalsteadMetricable _halsteadMetricable;
        private readonly IMaintainMetricable _maintainMetricable;
        public MetricsController(IRawMetricable rawMetricable, ICCMetricable ccMetricable, IHalsteadMetricable halsteadMetricable, IMaintainMetricable maintainMetricable)
        {
            _rawMetricable = rawMetricable;
            _ccMetricable = ccMetricable;
            _halsteadMetricable = halsteadMetricable;
            _maintainMetricable = maintainMetricable;
        }
        public IActionResult FileUpload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FileUpload(FileUploadModel fileUploadModel)
        {
            if (ModelState.IsValid)
            {
                if (fileUploadModel.FileToUpload == null || fileUploadModel.FileToUpload.Length == 0)
                    return Content("file not selected");

                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    fileUploadModel.FileToUpload.FileName);
                if (Path.GetExtension(filePath) == ".py" || Path.GetExtension(filePath) == ".ipynb")
                {
                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileUploadModel.FileToUpload.CopyToAsync(stream);
                    }
                    
                    return RedirectToAction("ViewMetrics", new { path = filePath });
                }
            }
            return View();
        }
        public async Task<IActionResult> ViewMetrics(string path)
        {
            if(path==null)
                ModelState.AddModelError(string.Empty,"Path değeri boş geçilmiştir.");
            List<string> pathList = new List<string>();
            pathList.Add(path);
            var metricsViewModel = new MetricsViewModel()
            {
                RawMetricsList = await _rawMetricable.ExecuteRawMetrics(pathList),
                CCMetricsList = await _ccMetricable.ExecuteCCMetrics(pathList),
                HalsteadMetricsList = await _halsteadMetricable.ExecuteHalsteadMetrics(pathList),
                MaintainMetricsList = await  _maintainMetricable.ExecuteMaintainMetrics(pathList)
            };
            return View(metricsViewModel);
        }
    }
}
