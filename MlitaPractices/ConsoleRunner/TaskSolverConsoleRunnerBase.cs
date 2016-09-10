using MlitaPractices.TaskSolvers;

namespace MlitaPractices.ConsoleRunner
{
	public interface ITaskSolverConsoleRunner
	{
		string SolverName { get; }
		void RunSolver();
	}

	/// <summary>
	/// Класс, используемый для консольного представления задач.
	/// </summary>
	/// 
	/// <typeparam name="TInputArgs">Тип входных аргументов.</typeparam>
	/// <typeparam name="TResult">Тип результата.</typeparam>
	public abstract class TaskSolverConsoleRunnerBase<TInputArgs, TResult> : ITaskSolverConsoleRunner
	{
		public string SolverName { get; }
		private readonly ITaskSolver<TInputArgs, TResult> solver;

		protected TaskSolverConsoleRunnerBase(string solverName, ITaskSolver<TInputArgs, TResult> solver)
		{
			SolverName = solverName;
			this.solver = solver;
		}

		public void RunSolver()
		{
			var args = InputArgs();
			var result = solver.Solve(args);
			PrintResult(result);
		}

		protected abstract TInputArgs InputArgs();
		protected abstract void PrintResult(TResult result);
	}
}