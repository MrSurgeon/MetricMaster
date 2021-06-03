using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace SchoolProject.HttpClientMetrics.Entities
{
    public class CyclomaticComplexity
    {
        public string Type { get; set; }
        public string rank { get; set; }
        public string name { get; set; }
        public int col_offset { get; set; }
        public int lineno { get; set; }
        public int complexity { get; set; }
    }
}
