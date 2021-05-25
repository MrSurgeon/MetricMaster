using System;
using System.Collections.Generic;
using System.Text;

namespace BasMetricsProductor
{
    public class RawMetrics
    {
        public int LLOC { get; set; } = 0;
        public int LOC { get; set; } = 0;
        public int SLOC { get; set; } = 0;
        public int CommentLine { get; set; } = 0;
        public int BlankLine { get; set; } = 0;
        public int MultiLine { get; set; } = 0;

    }
}
