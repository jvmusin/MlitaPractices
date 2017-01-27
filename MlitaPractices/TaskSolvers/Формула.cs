using System;
using System.Collections.Generic;
using MlitaPractices.ConsoleRunner;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers
{
    public class Формула : ITaskSolver<string, bool>
    {
        public bool Solve(string s)
        {
            var len = Check(s, 0);
            if (len != -1 && len + 2 <= s.Length && IsOperation(s[len]))
                len += 1 + Check(s, len + 1);
            return len == s.Length;
        }

        private static int Check(string s, int at)
        {
            if (at >= s.Length)
                return -1;

            var cur = s[at];

            if (cur == '(')
            {
                var len = Check(s, at + 1);
                var afterFinish = at + 1 + len;
                if (len == -1 || afterFinish >= s.Length)
                    return -1;
                if (s[afterFinish] != ')')
                {
                    if (!IsOperation(s[afterFinish]))
                        return -1;
                    var partLen = Check(s, afterFinish + 1);
                    if (partLen == -1)
                        return -1;
                    afterFinish += 1 + partLen;
                    if (afterFinish >= s.Length || s[afterFinish] != ')')
                        return -1;
                    return 1 + len + 1 + partLen + 1;
                }
                return 1 + len + 1;
            }

            if (char.IsLetter(cur))
                return 1;

            if (cur == '-')
            {
                if (at > 0 && s[at - 1] == '-')
                    return -1;
                var len = Check(s, at + 1);
                if (len == -1)
                    return -1;
                return 1 + len;
            }

            return -1;
        }

        private static bool IsOperation(char c)
        {
            return c == '+' ||
                   c == '*' ||
                   c == '/';
        }
    }

    [TestFixture]
    public class ФормулаТесты
    {
        private static readonly Формула Solver = new Формула();

        [Test, TestCaseSource(nameof(TestCases))]
        public bool Test(string args)
        {
            return Solver.Solve(args);
        }

        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData("a+b").Returns(true);
                yield return new TestCaseData("a+-b").Returns(true);
                yield return new TestCaseData("a+b").Returns(true);
                yield return new TestCaseData("a+(b*c)").Returns(true);
                yield return new TestCaseData("a+(b*-c)").Returns(true);
                yield return new TestCaseData("a+-(b*-c)").Returns(true);
                yield return new TestCaseData("a+(b+(c+-d))").Returns(true);
                yield return new TestCaseData("(a+b)*c").Returns(true);
                yield return new TestCaseData("a*-((b/e)+-c)").Returns(true);
                yield return new TestCaseData("(a+b)").Returns(true);
                yield return new TestCaseData("a+(-a+b)").Returns(true);
                yield return new TestCaseData("a+").Returns(false);
                yield return new TestCaseData("(a+").Returns(false);
                yield return new TestCaseData("a+b)").Returns(false);
                yield return new TestCaseData("a(b").Returns(false);
                yield return new TestCaseData("a+bb").Returns(false);
                yield return new TestCaseData("aa+b").Returns(false);
                yield return new TestCaseData("a+b-c").Returns(false);
                yield return new TestCaseData("a+(b+(c+ddd").Returns(false);
                yield return new TestCaseData("a+(b+c+d)").Returns(false);
                yield return new TestCaseData("*").Returns(false);
                yield return new TestCaseData("**").Returns(false);
                yield return new TestCaseData("--").Returns(false);
                yield return new TestCaseData("a+(b-)").Returns(false);
                yield return new TestCaseData("()").Returns(false);
                yield return new TestCaseData("(a+b)+()").Returns(false);
            }
        }
    }

    public class ФормулаКонсольный : TaskSolverConsoleRunnerBase<string, bool>
    {
        public ФормулаКонсольный() : base("Формула", new Формула())
        {
        }

        protected override string InputArgs()
        {
            Console.Write("Введите формулу: ");
            return Console.ReadLine().Replace(" ", "");
        }

        protected override void PrintResult(bool result)
        {
            Console.WriteLine(result ? "Формула верная" : "Формула неверная");
        }
    }
}