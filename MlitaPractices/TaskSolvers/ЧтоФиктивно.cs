using System;
using System.Collections.Generic;
using System.Linq;
using MlitaPractices.ConsoleRunner;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers
{
	/// <summary>
	/// Решение задачи "Что фиктивно?".
	/// </summary>
	public class ЧтоФиктивно : ITaskSolver<FunctionResults, IEnumerable<Tuple<int, bool>>>
	{
		/// <summary>
		/// Решение перебором.<br/>
		/// 
		/// Переберём индексы всех переменных функции и для каждого набора аргументов функции посмотрим,
		/// равен ли результат функции для набора переменных,
		/// где переменная по выбранному индексу имеет текущее значение и где обратное.
		/// <br/>
		/// 
		/// То есть, для функции двух переменных, если мы смотрим на индекс 0, то если f(0, 1) == f(1, 1),
		/// переменная с индексом 0 считается фиктивной, так как значение функции от неё не зависит.
		/// </summary>
		/// 
		/// <param name="functionResults">Результаты работы булевой функции.</param>
		/// <returns>Перечисление пар вида (индекс переменной, фиктивна ли (true = фиктивна, false = существенна)).</returns>
		public IEnumerable<Tuple<int, bool>> Solve(FunctionResults functionResults)
		{
			return Enumerable.Range(0, functionResults.VariablesCount)
				.Select(i => Tuple.Create(i, IsFictitious(i, functionResults)));
		}

		/// <summary>
		/// Проверить, является ли переменная с индексом <paramref name="variableIndex"/> фиктивной.
		/// </summary>
		/// <param name="variableIndex">Индекс проверяемой переменной.</param>
		/// <param name="results">Результаты работы функции.</param>
		/// <returns>Фиктивна ли переменная.</returns>
		private static bool IsFictitious(int variableIndex, FunctionResults results)
		{
			return Enumerable.Range(0, 1 << results.VariablesCount)
				.All(i =>
				{
					var inversedBit = i ^ (1 << (results.VariablesCount - variableIndex - 1));
					return results[i] == results[inversedBit];
				});
		}
	}

	/// <summary>
	/// Тесты для задачи "Что фиктивно?".
	/// </summary>
	[TestFixture]
	public class ЧтоФиктивноТесты
	{
		private static readonly ЧтоФиктивно Solver = new ЧтоФиктивно();

		[Test, TestCaseSource(nameof(TestCases))]
		public Tuple<int, bool>[] Test(FunctionResults args)
		{
			return Solver.Solve(args).ToArray();
		}

		private static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				yield return new TestCaseData(new FunctionResults(3, "11110011")).Returns(new[]
				{
					Tuple.Create(0, false),
					Tuple.Create(1, false),
					Tuple.Create(2, true)
				});

				yield return new TestCaseData(new FunctionResults(3, "00000000")).Returns(new[]
				{
					Tuple.Create(0, true),
					Tuple.Create(1, true),
					Tuple.Create(2, true)
				});

				yield return new TestCaseData(new FunctionResults(3, "11111100")).Returns(new[]
				{
					Tuple.Create(0, false),
					Tuple.Create(1, false),
					Tuple.Create(2, true)
				});

				yield return new TestCaseData(new FunctionResults(3, "10111011")).Returns(new[]
				{
					Tuple.Create(0, true),
					Tuple.Create(1, false),
					Tuple.Create(2, false)
				});

				yield return new TestCaseData(new FunctionResults(3, "11001100")).Returns(new[]
				{
					Tuple.Create(0, true),
					Tuple.Create(1, false),
					Tuple.Create(2, true)
				});
			}
		}
	}

	/// <summary>
	/// Консольный демонстратор задачи "Что фиктивно?".
	/// </summary>
	public class ЧтоФиктивноКонсольный : TaskSolverConsoleRunnerBase<FunctionResults, IEnumerable<Tuple<int, bool>>>
	{
		public ЧтоФиктивноКонсольный() : base("Что фиктивно?", new ЧтоФиктивно())
		{
		}

		protected override FunctionResults InputArgs() => FunctionResults.ReadFromConsole();

		protected override void PrintResult(IEnumerable<Tuple<int, bool>> result)
		{
			var results = result.Select(tuple => $"переменная {tuple.Item1} {(tuple.Item2 ? "фиктивна" : "существенна")}");
			Console.WriteLine($"Результат: {string.Join(", ", results)}");
		}
	}
}