using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.MyMetrics.Models.Entities
{
    public class Raw
    {
        public string Path { get; set; }
        public uint Loc { get; set; }
        public uint Lloc { get; set; }
        public uint Sloc { get; set; }
        public uint Comments { get; set; }
        public uint Multi { get; set; }
        public uint Blank { get; set; }
        public uint SingleComments { get; set; }
    }
}
