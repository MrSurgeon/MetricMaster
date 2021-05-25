using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasMetricsProductor
{
    public class PythonBasMetricsManager : IRawMetricsCalculator, IMetricsStartable
    {

        private static async Task FileReaderOperator(string path)
        {
            string line;
            using (var file = new StreamReader(path))
            {
                while ((line = await file.ReadLineAsync()) != null)
                {

                }
            }
        }

        private void LogicLineOfCodeCalculator
            (string line, int?[] ucDegersBackSlash, int lineListCount, ref int logicLineOfCode,
            int?[] ucDegersParantheses)
        {
            int i = 0;
            if (!String.IsNullOrEmpty(line))
            {
                while (i < line.Length )
                {
                    //Scenario3
                    if (line[i] == '"' && line[i - 2] == '=')
                    {
                        ucDegersBackSlash[0] = lineListCount;
                    }
                    if (i == line.Length - 1 && line[i] == '"')
                    {
                        ucDegersBackSlash[1] = lineListCount;
                    }
                    //Scenario4
                    if (line[i] == '{' || line[i] == '(' || line[i] == '[')
                    {
                        ucDegersParantheses[0] = lineListCount;
                    }
                    if (line[i] == ']' || line[i] == '}' || line[i] == ')')
                    {
                        ucDegersParantheses[1] = lineListCount;
                    }
                    //Scenario1And2
                    if (i == line.Length - 1)
                    {
                        logicLineOfCode++;
                    }
                    else if (line[i] == ';')
                    {
                        logicLineOfCode++;
                    }
                    i++;
                }
            }
        }
        public async Task<int> LLOCCalculateAsync(string path)
        {
            int logicLineOfCode = 0;
            string line = null;
            List<string> lineList = new List<string>();
            int?[] ucDegersBackSlash = { null, null };
            int?[] ucDegersParantheses = { null, null };
            using (var file = new StreamReader(path))
            {
                while ((line = await file.ReadLineAsync()) != null)
                {
                    lineList.Add(line);
                    int lineListCount = lineList.Count;
                    LogicLineOfCodeCalculator(line, ucDegersBackSlash, lineListCount, ref logicLineOfCode, ucDegersParantheses);

                    if (!(ucDegersBackSlash.Any(a => a == null)))
                    {
                        if (ucDegersBackSlash[0] != ucDegersBackSlash[1] && ucDegersBackSlash[0] < ucDegersBackSlash[1])
                        {
                            logicLineOfCode -= Convert.ToInt32(ucDegersBackSlash[1] - ucDegersBackSlash[0]);
                            ucDegersBackSlash = new int?[2];
                        }
                    }

                    if (!(ucDegersParantheses.Any(a => a == null)))
                    {
                        if (ucDegersParantheses[0] != ucDegersParantheses[1] && ucDegersParantheses[0] < ucDegersParantheses[1])
                        {
                            logicLineOfCode -= Convert.ToInt32(ucDegersParantheses[1] - ucDegersParantheses[0]);
                            ucDegersParantheses = new int?[2];
                        }
                    }

                }
            }
            return logicLineOfCode;
        }

        public async Task<int> LOCCalculateAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SLOCCalculateAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CommentLineCalculateAsync(string path)
        {
            int commentLineOfCode = 0;
            string line = null;
            using (var file = new StreamReader(path))
            {
                while ((line = await file.ReadLineAsync()) != null)
                {
                    if (line[0].ToString() == "#")
                    {
                        commentLineOfCode++;
                    }
                }
                /* _basMetricsField.LogicLineOfCode -= _basMetricsField.CommentLineOfCode;*/ //Senaryo 1 ve 2 kodu
            }
            return commentLineOfCode;
        }

        public async Task<int> MultiLineCalculateAsync(string path)
        {
            string line;
            int multiLineOfCode = 0;

            using (var file = new StreamReader(path))
            {
                int i = 0;
                List<string> lineList = new List<string>();
                int[] limitValues = new int[2];
                while ((line = await file.ReadLineAsync()) != null)
                {
                    lineList.Add(line);
                    if (line.Contains("\"\"\""))
                    {
                        multiLineOfCode++;
                        limitValues[i] = lineList.Count;
                        if (i > 1)
                        {
                            limitValues = new int[2];
                            i = 0;
                            continue;
                        }
                        i++;
                    }
                    if (lineList.Count < limitValues[1] && lineList.Count > limitValues[0])
                    {
                        multiLineOfCode++;
                    }
                }
            }
            return multiLineOfCode;
        }

        public async Task<int> BlankLineCalculateAsync(string path)
        {
            throw new NotImplementedException();
        }
        public async Task<RawMetrics> StartMetricCalculateAsync(string path)
        {
            //int lLOC = await LLOCCalculateAsync(path);
            //int cLOC = await CommentLineCalculateAsync(path);
            int mLOC = await MultiLineCalculateAsync(path);

            RawMetrics rawMetrics = new RawMetrics()
            {
                //LLOC = lLOC - cLOC,
                //CommentLine = cLOC,
                MultiLine = mLOC
            };
            return rawMetrics;
        }
    }
}
