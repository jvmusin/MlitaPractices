using System;
using MlitaPractices.ConsoleRunner;
using MlitaPractices.TaskSolvers;

namespace MlitaPractices
{
	public class Program
	{
		public static void Main()
		{
			var runners = new ITaskSolverConsoleRunner[]
			{
				new ЕдиничныеНаборыКонсольный(),
				new ЧтоФиктивноКонсольный(),
				new НомерФункцииКонсольный(),
                new ФормулаКонсольный(),
				new МинимизацияКонсольный(),
                new СДНФКонсольный(),
                new СКНФКонсольный(),
                new ПодформулаКонсольный(),
                new МашинаТьюринга9Консольный()
			};
			var taskSolverConsoleSelector = new TaskSolverConsoleSelector(runners);

			while (true)
			{
				taskSolverConsoleSelector.Run();
				Console.WriteLine();
			}
		}
	}
}