using System;
using System.Collections.Generic;
using MlitaPractices.ConsoleRunner;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers
{
    public class Подформула : ITaskSolver<string, ISet<string>>
    {
        public ISet<string> Solve(string s)
        {
            var result = new SortedSet<string>();
            Find(s, 0, result);
            return result;
        }

        private static string Find(string s, int at, ISet<string> result)
        {
            if (at >= s.Length || s[at] == ')')
                return null;

            string curResult;
            if (s[at] == '-')
            {
                var right = Find(s, at + 1, result);
                result.Add(right);
                curResult = '-' + right;
            }
            else if (s[at] == '(')
            {
                var right = Find(s, at + 1, result);
                result.Add(right);
                curResult = '(' + right + ')';
                var nextAt = at + right.Length + 2;
                if (nextAt < s.Length && s[nextAt] != ')')
                {
                    result.Add(curResult);
                    curResult += s[nextAt] + Find(s, nextAt + 1, result);
                }
            }
            else
            {
                curResult = s[at].ToString();
                if (at + 2 < s.Length && s[at+1] != ')')
                {
                    result.Add(curResult);
                    var right = Find(s, at + 2, result);
                    result.Add(right);
                    curResult = s.Substring(at, 2) + right;
                }
            }
            result.Add(curResult);
            return curResult;
        }
    }

    [TestFixture]
    public class ПодформулаТесты
    {
        private static readonly Подформула Solver = new Подформула();

        [Test, TestCaseSource(nameof(TestCases))]
        public ISet<string> Test(string args)
        {
            return Solver.Solve(args);
        }

        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData("a+b").Returns(new SortedSet<string>
                {
                    "a", "b", "a+b"
                });
                yield return new TestCaseData("a+(b*c)").Returns(new SortedSet<string>
                {
                    "a", "b", "c", "b*c", "(b*c)", "a+(b*c)"
                });
                yield return new TestCaseData("(a+b)*c").Returns(new SortedSet<string>
                {
                    "a", "b", "c", "a+b", "(a+b)", "(a+b)*c"
                });
                yield return new TestCaseData("a*-((b/e)+-c)").Returns(new SortedSet<string>
                {
                    "a", "b", "c", "-c", "e", "b/e", "(b/e)", "(b/e)+-c", "((b/e)+-c)", "-((b/e)+-c)", "a*-((b/e)+-c)"
                });
            }
        }
    }

    public class ПодформулаКонсольный : TaskSolverConsoleRunnerBase<string, ISet<string>>
    {
        public ПодформулаКонсольный() : base("Подформула", new Подформула())
        {
        }

        protected override string InputArgs()
        {
            Console.Write("Введите формулу: ");
            return Console.ReadLine().Replace(" ", "");
        }

        protected override void PrintResult(ISet<string> result)
        {
            Console.WriteLine(string.Join(" ", result));
        }
    }
}