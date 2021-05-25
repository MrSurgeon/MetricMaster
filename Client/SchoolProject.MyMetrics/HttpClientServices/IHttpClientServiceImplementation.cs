using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolProject.MyMetrics.Models.Entities;

namespace SchoolProject.MyMetrics.HttpClientServices
{
    public interface IHttpClientServiceImplementation
    {
        Task Execute(List<string> pathList);
    }
}
