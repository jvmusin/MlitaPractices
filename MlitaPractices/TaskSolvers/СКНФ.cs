using System;
using System.Collections.Generic;
using System.Linq;
using MlitaPractices.ConsoleRunner;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers
{
    public class СКНФ : ITaskSolver<FunctionResults, List<string>>
    {
        public List<string> Solve(FunctionResults args)
        {
            var result = new List<string>();
            for (int i = 0; i < args.ResultsCount; i++)
                if (!args[i])
                    result.Add(ToString(i, args.VariablesCount));
            return result;
        }

        private static string ToString(int value, int variablesCount)
        {
            var res = "";
            for (int i = variablesCount - 1, c = 'a'; i >= 0; i--, c++)
                if (((value >> i) & 1) == 0)
                    res += char.ToUpper((char)c);
                else res += (char)c;
            return $"({string.Join(" v ", res.Select(c => c.ToString()))})";
        }
    }

    public class СКНФТесты
    {
        private static readonly СКНФ Solver = new СКНФ();

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
                    "(A v B)", "(a v b)"
                });
                yield return new TestCaseData(new FunctionResults(3, "11100010")).Returns(new[]
                {
                    "(A v b v c)", "(a v B v C)", "(a v B v c)", "(a v b v c)"
                });
                yield return new TestCaseData(new FunctionResults(3, "11111111")).Returns(new string[0]);
            }
        }
    }

    public class СКНФКонсольный : TaskSolverConsoleRunnerBase<FunctionResults, List<string>>
    {
        public СКНФКонсольный() : base("СКНФ", new СКНФ())
        {
        }

        protected override FunctionResults InputArgs() => FunctionResults.ReadFromConsole();

        protected override void PrintResult(List<string> result)
        {
            if (result.Count == 0) Console.WriteLine("TRUE");
            else Console.WriteLine(string.Join("", result));
        }
    }
}