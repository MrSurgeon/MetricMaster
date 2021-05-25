using System;
using System.IO;
using System.Threading.Tasks;

namespace MyFileManager
{
    class Program
    {

        static async Task Main(string[] args)
        {
            await ReadAndDisplayFilesAsync();
        }

        static async Task ReadAndDisplayFilesAsync()
        {

            String fileName = @"C:\TestFields\PythonTest\PythonTest\PythonTest.py";
            
            int rowCounter = 0;
            int commentCounter = 0;
            string line;

            // Read the file and display it line by line.  
            using (var file = new StreamReader(fileName))
            {
                while ((line = await file.ReadLineAsync()) != null)
                {
                    
                    Console.WriteLine(line);
                    rowCounter++;
                }
            }
            Console.WriteLine("There were {0} lines.", rowCounter);
            Console.ReadLine();
        }

    }
}
