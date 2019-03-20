using System;
using System.IO;
using System.Collections.Generic;

namespace LexicalAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(FilePaths.InputFile);
            Analyze lexicalAnalyzer = new Analyze(text);

            File.WriteAllText(FilePaths.OutputFile, lexicalAnalyzer.GetResult());
        }
    }
}
