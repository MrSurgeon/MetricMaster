using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.MyMetrics.Models.Entities
{
    public class Halstead
    {
        public double H1 { get; set; }
        public double H2 { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }
        public double Vocabulary { get; set; }
        public double Length { get; set; }
        public double CalculatedLength { get; set; }
        public double Volume { get; set; }
        public double Difficulty { get; set; }
        public double Effort { get; set; }
        public double Time { get; set; }
        public double Bugs { get; set; }

    }
}
