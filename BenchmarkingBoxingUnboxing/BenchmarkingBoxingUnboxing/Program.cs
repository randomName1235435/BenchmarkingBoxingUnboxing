using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace BenchmarkingBoxingUnboxing
{
    class Program
    {
        static void Main(string[] args)
        {
            new BenchmarkClass().WithBoxing();
            new BenchmarkClass().WithoutBoxing();
            new BenchmarkClass().WithoutUnBoxing();
            new BenchmarkClass().WithUnBoxing();
            BenchmarkRunner.Run<BenchmarkClass>();
        }
    }

    [MemoryDiagnoser]
    public class BenchmarkClass
    {
        private MagicProvider provider = new MagicProvider();
        private static readonly int testValueInt = 5;
        private static readonly object testValueObject = 5;

        [Benchmark]
        public int WithoutBoxing() => provider.WithoutBoxing(testValueInt);

        [Benchmark]
        public object WithBoxing() => provider.WithBoxing(testValueInt);

        [Benchmark]
        public object WithoutUnBoxing() => provider.WithoutUnBoxing(testValueInt);

        [Benchmark]
        public int WithUnBoxing() => provider.WithUnBoxing(testValueObject);
    }
    class MagicProvider
    {
        public object WithBoxing(int param)
        {
            return param;
        }
        public int WithUnBoxing(object param)
        {
            return (int)param;
        }

        public int WithoutBoxing(int param)
        {
            return param;
        }

        public object WithoutUnBoxing(object param)
        {
            return param;
        }
    }
}
