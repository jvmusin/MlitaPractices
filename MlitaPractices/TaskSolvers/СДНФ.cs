using System;
using System.Collections.Generic;
using MlitaPractices.ConsoleRunner;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers
{
    public class СДНФ : ITaskSolver<FunctionResults, List<string>>
    {
        public List<string> Solve(FunctionResults args)
        {
            var result = new List<string>();
            for (int i = 0; i < args.ResultsCount; i++)
                if (args[i])
                    result.Add(ToString(i, args.VariablesCount));
            return result;
        }

        private static string ToString(int value, int variablesCount)
        {
            var res = "";
            for (int i = variablesCount - 1, c = 'a'; i >= 0; i--, c++)
                if (((value >> i) & 1) == 1)
                    res += char.ToUpper((char)c);
                else res += (char)c;
            return res;
        }
    }

    [TestFixture]
    public class СДНФТесты
    {
        private static readonly СДНФ Solver = new СДНФ();

        [Test, TestCaseSource(nameof(TestCases))]
        public string[] Test(FunctionResults args)
        {
            return Solver.Solve(args).ToArray();
        }

        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(new FunctionResults(2, "0110")).Returns(new[]
                {
                    "aB", "Ab"
                });
                yield return new TestCaseData(new FunctionResults(3, "11100010")).Returns(new[]
                {
                    "abc", "abC", "aBc", "ABc"
                });
                yield return new TestCaseData(new FunctionResults(2, "00000000")).Returns(new string[0]);
            }
        }
    }

    public class СДНФКонсольный : TaskSolverConsoleRunnerBase<FunctionResults, List<string>>
    {
        public СДНФКонсольный() : base("СДНФ", new СДНФ())
        {
        }

        protected override FunctionResults InputArgs() => FunctionResults.ReadFromConsole();

        protected override void PrintResult(List<string> result)
        {
            if (result.Count == 0) Console.WriteLine("FALSE");
            else Console.WriteLine(string.Join(" v ", result));
        }
    }
}