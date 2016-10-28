using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static MlitaPractices.Helpers.ConsoleHelper;

namespace MlitaPractices.TaskSolvers
{
	/// <summary>
	/// Класс, представляющий результаты работы булевой функции.
	/// </summary>
	public class FunctionResults : IEnumerable<bool>
	{
		public int ResultsCount => 1 << VariablesCount;
		public int VariablesCount { get; }
		private readonly bool[] functionResults;

		public FunctionResults(int variablesCount, bool[] functionResults)
		{
			if (functionResults.Length != 1 << variablesCount)
				throw new ArgumentException($"Size of {nameof(functionResults)} should be 2^{nameof(variablesCount)}");
			VariablesCount = variablesCount;
			this.functionResults = functionResults;
		}

		public FunctionResults(int variablesCount, string functionResults)
			: this(variablesCount, functionResults.Select(f => f == '1').ToArray())
		{
		}

		public static FunctionResults ReadFromConsole(bool withAsking = true)
		{
			var variablesCount = withAsking
				? AskInt("Введите число логических переменных функции: ")
				: ReadInt();
			var variables = withAsking ? Ask("Введите значения функции: ") : ReadLine();
			return new FunctionResults(variablesCount, variables.Select(c => c == '1').ToArray());
		}

		public IEnumerator<bool> GetEnumerator() => functionResults.AsEnumerable().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public bool this[int index] => functionResults[index];

		public override string ToString() => string.Join("", functionResults.Select(f => f ? "1" : "0"));
	}
}