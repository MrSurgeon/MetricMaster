using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasMetricsProductor
{
    public interface IRawMetricsCalculator
    {
        Task<int> LLOCCalculateAsync(string path) ;
        Task<int> LOCCalculateAsync(string path);
        Task<int> SLOCCalculateAsync(string path);
        Task<int> CommentLineCalculateAsync(string path);
        Task<int> MultiLineCalculateAsync(string path);
        Task<int> BlankLineCalculateAsync(string path);
    }
}
