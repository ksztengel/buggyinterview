using System;

namespace CongaInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataPath = "/Users/ksztengel/buggyinterview/ExampleData/exampleData.txt";
            var outPath = "/tmp/conga.csv";
            var engine = new MergeEngine(dataPath, outPath);
            engine.Run();
        }
    }
}
