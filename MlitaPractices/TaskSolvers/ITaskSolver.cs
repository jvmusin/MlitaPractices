namespace MlitaPractices.TaskSolvers
{
	/// <summary>
	/// Класс, представляющий "решателя" задачи.
	/// </summary>
	/// <typeparam name="TInputArgs">Тип входных аргументов.</typeparam>
	/// <typeparam name="TResult">Тип результата.</typeparam>
	public interface ITaskSolver<in TInputArgs, out TResult>
	{
		TResult Solve(TInputArgs args);
	}
}