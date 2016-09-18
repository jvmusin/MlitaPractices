using System;
using System.Collections.Generic;
using System.Linq;
using MlitaPractices.Helpers;
using NUnit.Framework;

namespace MlitaPractices.TaskSolvers.Archive
{
	public class КодыГрея : ITaskSolver<int, IEnumerable<string>>
	{
		public IEnumerable<string> Solve(int functionResults)
		{
			var initialNumber = new bool[functionResults];
			return Solve(0, initialNumber, functionResults);
		}

		private static IEnumerable<string> Solve(int index, IList<bool> currentNumber, int totalWidth)
		{
			if (index == totalWidth)
			{
				yield return string.Join("", currentNumber.Select(b => b ? "1" : "0"));
				yield break;
			}

			foreach (var result in Solve(index + 1, currentNumber, totalWidth))
				yield return result;
			currentNumber[index] ^= true;
			foreach (var result in Solve(index + 1, currentNumber, totalWidth))
				yield return result;
		}
	}

	[TestFixture]
	public class ТестыКодовГрея
	{
		private static readonly КодыГрея Solver = new КодыГрея();

		[Test]
		public void Test()
		{
			Solver.Solve(3).ForEach(Console.WriteLine);
		}
	}
}