using System;
using System.Collections.Generic;
using System.Linq;
using MlitaPractices.ConsoleRunner;
using MlitaPractices.Helpers;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers
{
    public class МашинаТьюринга9 : ITaskSolver<int[], int[]>
    {
        public int[] Solve(int[] a)
        {
            a = a.ToArray();
            var at = 1;
            var state = 1;
            while (state != 0)
            {
                var op = Operations[state, at >= 0 ? a[at] : 0];
                if (at >= 0) a[at] = op.NewValue;
                at += (int) op.Delta;
                state = op.NewState;
            }
            return a;
        }

        /// <summary>
        /// ab - если n чётно
        /// xn - если n нечётно
        /// </summary>
        private static readonly Operation[,] Operations = new Operation[6, 3];

        static МашинаТьюринга9()
        {
            const int a = 1;
            const int b = 2;
            const int c = 0; // c is lambda (empty)

            Operations[1, a] = new Operation(a, Direction.Right, 2);
            Operations[1, b] = new Operation(b, Direction.Right, 2);
            Operations[1, c] = new Operation(b, Direction.Left,  3);

            Operations[2, a] = new Operation(a, Direction.Right, 1);
            Operations[2, b] = new Operation(b, Direction.Right, 1);
            Operations[2, c] = new Operation(c, Direction.Left,  5);

            Operations[3, a] = Operations[3, b] = Operations[3, c] = new Operation(a, Direction.Left, 4);

            Operations[4, a] = Operations[4, b] = new Operation(c, Direction.Left, 4);
            Operations[4, c] = new Operation(c, Direction.Stay, 0);

            Operations[5, a] = new Operation(a, Direction.Left, 4);
            Operations[5, b] = new Operation(b, Direction.Left, 4);
        }

        private class Operation
        {
            public int NewValue { get; }
            public Direction Delta { get; }
            public int NewState { get; }

            public Operation(int newValue, Direction delta, int newState)
            {
                NewValue = newValue;
                Delta = delta;
                NewState = newState;
            }

            public override string ToString()
                => $"{nameof(NewValue)}: {NewValue}, {nameof(Delta)}: {Delta}, {nameof(NewState)}: {NewState}";
        }

        private enum Direction
        {
            Left = -1,
            Stay = 0,
            Right = 1
        }
    }

    [TestFixture]
    public class МашинаТьюринга9Тесты
    {
        private static readonly МашинаТьюринга9 Solver = new МашинаТьюринга9();

        [Test, TestCaseSource(nameof(TestCases))]
        public int[] Test(int[] test)
        {
            return Solver.Solve(test);
        }

        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                const int a = 1;
                const int b = 2;
                const int c = 0;
                yield return new TestCaseData(new[] {c, c}).Returns(new[] {a, b});
                yield return new TestCaseData(new[] {c, a, c}).Returns(new[] {c, a, c});
                yield return new TestCaseData(new[] {c, a, a, c}).Returns(new[] {c, c, a, b});
                yield return new TestCaseData(new[] {c, a, b, b, c}).Returns(new[] {c, c, c, b, c});
            }
        }
    }

    public class МашинаТьюринга9Консольный : TaskSolverConsoleRunnerBase<int[], int[]>
    {
        public МашинаТьюринга9Консольный() : base("Машина Тьюринга 9", new МашинаТьюринга9())
        {
        }

        protected override int[] InputArgs()
        {
            var n = ConsoleHelper.AskInt("Введите количество элементов: ");
            var a = new int[n + 2];
            Console.Write("Введите элементы: ");
            var input = Console.ReadLine().Split(' ');
            for (var i = 1; i <= n; i++)
                a[i] = input[i - 1][0] - 'a' + 1;
            return a;
        }

        protected override void PrintResult(int[] result)
        {
            const string alp = "cab";
            Console.WriteLine($"Результат: {string.Join(" ", result.Select(c => alp[c]))}");
        }
    }
}