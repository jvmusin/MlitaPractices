using System;
using System.Linq;
using MlitaPractices.Helpers;

namespace MlitaPractices.ConsoleRunner
{
	/// <summary>
	/// Консольный "выбиратель" задачи для запуска.
	/// </summary>
	public class TaskSolverConsoleSelector
	{
		private readonly ITaskSolverConsoleRunner[] taskSolverRunners;

		public TaskSolverConsoleSelector(params ITaskSolverConsoleRunner[] availableTaskSolverRunners)
		{
			taskSolverRunners = availableTaskSolverRunners;
		}

		public void Run()
		{
			Console.WriteLine("Для выбора программы введите её номер.");
			Console.WriteLine("Доступные программы: ");
			PrintRunners();
			taskSolverRunners[ConsoleHelper.AskInt()].RunSolver();
		}

		private void PrintRunners()
		{
			taskSolverRunners
				.Select((solver, index) => $"{index}: {solver.SolverName}")
				.ForEach(Console.WriteLine);
		}
	}
}