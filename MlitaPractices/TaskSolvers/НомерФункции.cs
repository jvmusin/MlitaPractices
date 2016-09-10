using System;
using System.Collections.Generic;
using MlitaPractices.ConsoleRunner;
using NUnit.Framework;
using NumericHelper = MlitaPractices.Helpers.NumericHelper;

namespace MlitaPractices.TaskSolvers
{
	/// <summary>
	/// Решение задачи "Номер функции".
	/// </summary>
	public class НомерФункции : ITaskSolver<FunctionResults, int>
	{
		/// <summary>
		/// Чтобы найти порядковый номер функции, представим результаты работы функции в виде строки, состоящей из нулей и единиц.<br/>
		/// Заметим, что у нас получилось некоторое число, записанное в двоичной системе счисления.<br/>
		/// Переведём это число в десятичную систему счисления и получим ответ.
		/// </summary>
		/// 
		/// <param name="functionResults">Результаты работы булевой функции.</param>
		/// <returns>Порядковый номер функции (индексация с нуля).</returns>
		public int Solve(FunctionResults functionResults)
		{
			var binary = functionResults.ToString();
			return NumericHelper.ParseInt(binary, 2);
		}
	}

	/// <summary>
	/// Тесты для задачи "Номер функции".
	/// </summary>
	[TestFixture]
	public class НомерФункцииТесты
	{
		private static readonly НомерФункции Solver = new НомерФункции();

		[Test, TestCaseSource(nameof(TestCases))]
		public int Test(FunctionResults args)
		{
			return Solver.Solve(args);
		}

		private static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				yield return new TestCaseData(new FunctionResults(1, "01")).Returns(1);
				yield return new TestCaseData(new FunctionResults(2, "0101")).Returns(5);
				yield return new TestCaseData(new FunctionResults(3, "11100010")).Returns(226);
			}
		}
	}

	/// <summary>
	/// Консольный демонстратор задачи "Номер функции".
	/// </summary>
	public class НомерФункцииКонсольный : TaskSolverConsoleRunnerBase<FunctionResults, int>
	{
		public НомерФункцииКонсольный() : base("Номер функции", new НомерФункции())
		{
		}

		protected override FunctionResults InputArgs() => FunctionResults.ReadFromConsole();

		protected override void PrintResult(int result) => Console.WriteLine($"Номер функции = {result}");
	}
}