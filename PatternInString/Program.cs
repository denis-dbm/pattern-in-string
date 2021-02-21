using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PatternInString
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = "zf3kabxcde224lkzf3mabxc51+crsdtzf3nab=";
            int patternLength = 3;
            var output = new Dictionary<ReadOnlyMemory<char>, int>(new ByValueEqualityComparer());

            for (int i = 0; i < data.Length - patternLength; i++)
            {
                var piece = data.AsMemory(i, patternLength);
                
                if (output.ContainsKey(piece))
                    output[piece] += 1;
                else
                    output[piece] = 1;
            }
            
            Console.WriteLine("Result:");
            
            foreach (var entry in output)
                if (entry.Value > 1)
                    Console.WriteLine($"Found '{entry.Key}' {entry.Value} times");
        }

        sealed class ByValueEqualityComparer : IEqualityComparer<ReadOnlyMemory<char>>
        {
            public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y) => x.Span.SequenceEqual(y.Span);

            public int GetHashCode([DisallowNull] ReadOnlyMemory<char> obj) => obj.Span[0].GetHashCode();
        }
    }
}