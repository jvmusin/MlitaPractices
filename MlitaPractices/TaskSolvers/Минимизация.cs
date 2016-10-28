using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MlitaPractices.ConsoleRunner;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers
{
	public class Минимизация : ITaskSolver<FunctionResults, IEnumerable<string>>
	{
		public IEnumerable<string> Solve(FunctionResults functionResults)
		{
			var allValues = new List<MinimizationValue>();
			for (var i = 0; i < functionResults.ResultsCount; i++)
				if (functionResults[i])
					allValues.Add(new MinimizationValue(functionResults.VariablesCount, i));

			var solutions = FindSolutions(allValues).ToList();
			var minSize = solutions.Min(x => x.Count);
			var results = solutions.Where(x => x.Count == minSize)
				.Select(sol => string.Join(" v ", sol.Select(x => x.ToResultingString())))
				.OrderBy(x => x.Length)
				.ToList();
			var minResultLen = results.Min(x => x.Length);
			return results.Where(x => x.Length == minResultLen);
		}

		private static IEnumerable<List<MinimizationValue>> FindSolutions(List<MinimizationValue> currentValues)
		{
			var newValues = new List<MinimizationValue>();
			for (var i = 0; i < currentValues.Count; i++)
			{
				var x = currentValues[i];
				for (var j = i + 1; j < currentValues.Count; j++)
				{
					var y = currentValues[j];
					var merged = x.TryMerge(y);
					if (merged != null)
						newValues.Add(merged);
				}
			}
			if (!newValues.Any())
			{
				yield return currentValues;
			}
			else
			{
				for (var mask = 1; mask < 1 << newValues.Count; mask++)
				{
					var nextValues = new List<MinimizationValue>();
					for (var bit = 0; bit < newValues.Count; bit++)
						if (((mask >> bit) & 1) == 1)
							nextValues.Add(newValues[bit]);
					foreach (var result in FindSolutions(nextValues))
						yield return result;
				}

				foreach (var result in FindSolutions(newValues))
					yield return result;
			}
		}
	}

	public class MinimizationValue
	{
		public int VariablesCount { get; }
		public int Variables { get; }

		private readonly bool[] removed;

		public MinimizationValue(int variablesCount, int variables)
		{
			Variables = variables;
			VariablesCount = variablesCount;
			removed = new bool[variablesCount];
		}

		public MinimizationValue TryMerge(MinimizationValue other)
		{
			var differentValues = 0;
			var result = new MinimizationValue(VariablesCount, Variables);
			for (var i = 0; i < VariablesCount; i++)
			{
				if (removed[i] || other.removed[i])
				{
					if (removed[i] != other.removed[i])
						return null;
					result.removed[i] = true;
				}
				else
				{
					if (BitValueFromStart(i) == other.BitValueFromStart(i))
						continue;

					if (differentValues == 1)
						return null;
					result.removed[i] = true;
					differentValues++;
				}
			}
			return result;
		}

		private int BitValueFromStart(int bit)
		{
			return (Variables & BitNumberFromStart(bit)) > 0 ? 1 : 0;
		}

		private int BitNumberFromStart(int bit)
		{
			return 1 << (VariablesCount - bit - 1);
		}

		public bool Equals(MinimizationValue other)
		{
			if (other == null)
				return false;

			var removedEquals = StructuralComparisons.StructuralEqualityComparer
				.Equals(removed, other.removed);

			return removedEquals &&
			       VariablesCount == other.VariablesCount &&
			       Variables == other.Variables;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as MinimizationValue);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = StructuralComparisons.StructuralEqualityComparer.GetHashCode(removed);
				hashCode = (hashCode*397) ^ VariablesCount;
				hashCode = (hashCode*397) ^ Variables;
				return hashCode;
			}
		}

		public override string ToString()
		{
			var s = "";
			for (var i = 0; i < VariablesCount; i++)
				s += removed[i] ? "*" : BitValueFromStart(i).ToString();
			return s;
		}

		public string ToResultingString()
		{
			var s = "";
			for (var i = 0; i < VariablesCount; i++)
				if (!removed[i])
				{
					var res = (char) ('a' + 26 - 3 + i);
					if (BitValueFromStart(i) == 1)
						res = char.ToUpper(res);
					s += res;
				}
			return s;
		}
	}

	public class МинимизацияТесты
	{
		private static readonly Минимизация Solver = new Минимизация();

		[Test, TestCaseSource(nameof(TestCases))]
		public string[] Test(FunctionResults args)
		{
			return Solver.Solve(args).ToArray();
		}

		private static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				yield return new TestCaseData(new FunctionResults(3, "10110010")).Returns(new[]
				{
					"ac v aB v Bc"
				});
			}
		}
	}

	public class МинимизацияКонсольный : TaskSolverConsoleRunnerBase<FunctionResults, IEnumerable<string>>
	{
		public МинимизацияКонсольный() : base("Минимизация", new Минимизация())
		{
		}

		protected override FunctionResults InputArgs() => FunctionResults.ReadFromConsole();

		protected override void PrintResult(IEnumerable<string> result)
			=> Console.WriteLine($"Минимизированная функция = {{{string.Join(" v ", result)}}}");
	}
}