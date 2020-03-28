using System;

namespace CongaInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataPath = args[0];
            var outPath = args[1];
            var engine = new MergeEngine(dataPath, outPath);
            engine.Run();
        }
    }
}
