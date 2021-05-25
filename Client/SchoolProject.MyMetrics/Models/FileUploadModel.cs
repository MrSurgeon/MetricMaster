using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SchoolProject.MyMetrics.Models
{
    public class FileUploadModel
    {
        public IFormFile FileToUpload { get; set; }
    }
}
