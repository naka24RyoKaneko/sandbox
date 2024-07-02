using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NotVisualBasic.FileIO;
using RefrectionPerformanceTest.data;
using System.Dynamic;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.XPath;

namespace RefrectionPerformanceTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ref https://qiita.com/SY81517/items/79f6c5905e758279831a
            var summary = BenchmarkRunner.Run<TypeCastMethodImplEvaluation>();
        }
    }
}