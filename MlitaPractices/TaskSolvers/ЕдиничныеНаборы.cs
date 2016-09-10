using System;
using System.Collections.Generic;
using System.Linq;
using MlitaPractices.ConsoleRunner;
using MlitaPractices.Helpers;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers
{
	/// <summary>
	/// Решение задачи "Единичные наборы".
	/// </summary>
	public class ЕдиничныеНаборы : ITaskSolver<FunctionResults, IEnumerable<string>>
	{
		/// <summary>
		/// Для того, чтобы сгенерировать все единичные наборы булевой функции,
		/// пронумеруем результаты работы функции от 0 до 2^n - 1, где n - количество переменных функции.<br/>
		/// Затем пройдёмся по результатам работы функции, возьмём только те, где значение функции = 1,
		/// и переведём сопоставленный с ней номер в двоичную систему счисления, дополнив нулями слева до ширины,
		/// равной количеству переменных функции.
		/// 
		/// </summary>
		/// <param name="functionResults">Результаты работы функции.</param>
		/// <returns>Единичные наборы функции.</returns>
		public IEnumerable<string> Solve(FunctionResults functionResults)
		{
			return functionResults
				.Select((result, i) => result
					? i.ToBinaryString(functionResults.VariablesCount)
					: null)
				.NotNull();
		}
	}

	/// <summary>
	/// Тесты для задачи "Единичные наборы".
	/// </summary>
	[TestFixture]
	public class ЕдиничныеНаборыТесты
	{
		private static readonly ЕдиничныеНаборы Solver = new ЕдиничныеНаборы();

		[Test, TestCaseSource(nameof(TestCases))]
		public IEnumerable<string> Test(FunctionResults args)
		{
			return Solver.Solve(args).ToArray();
		}

		private static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				yield return new TestCaseData(new FunctionResults(2, "0110")).Returns(new[]
				{
					"01",
					"10"
				});

				yield return new TestCaseData(new FunctionResults(3, "11100010")).Returns(new[]
				{
					"000",
					"001",
					"010",
					"110"
				});
			}
		}
	}

	/// <summary>
	/// Консольный демонстратор задачи "Единичные наборы".
	/// </summary>
	public class ЕдиничныеНаборыКонсольный : TaskSolverConsoleRunnerBase<FunctionResults, IEnumerable<string>>
	{
		public ЕдиничныеНаборыКонсольный() : base("Единичные наборы", new ЕдиничныеНаборы())
		{
		}

		protected override FunctionResults InputArgs() => FunctionResults.ReadFromConsole();

		protected override void PrintResult(IEnumerable<string> result)
			=> Console.WriteLine($"Единичные наборы = {{{string.Join(", ", result)}}}");
	}
}