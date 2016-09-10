using System;

namespace MlitaPractices.Helpers
{
	/// <summary>
	/// Класс, позволяющий более гибко работать с консолью.
	/// </summary>
	public static class ConsoleHelper
	{
		/// <summary>
		///	Задать в консоли вопрос <paramref name="phrase"/> и считать ответ.
		/// </summary>
		/// 
		/// <param name="phrase">Вопрос, который необходимо задать в консоли.</param>
		/// <returns>Введённая в консоли строка.</returns>
		public static string Ask(string phrase = "")
		{
			Console.Write(phrase);
			return Console.ReadLine();
		}

		/// <summary>
		/// Задать в консоли вопрос <paramref name="phrase"/> и считать ответ, преобразовав его в число.
		/// </summary>
		/// 
		/// <param name="phrase">Вопрос, который необходимо задать в консоли.</param>
		/// <returns>Введённая в консоли строка.</returns>
		public static int AskInt(string phrase = "")
		{
			return int.Parse(Ask(phrase));
		}

		/// <summary>
		/// Прочитать строку из консоли.
		/// </summary>
		/// 
		/// <returns>Прочитанная из консоли строка.</returns>
		public static string ReadLine()
		{
			return Console.ReadLine();
		}

		/// <summary>
		/// Прочитать строку из консоли и преобразовать её в число.
		/// </summary>
		/// 
		/// <returns>Введённое в консоли число.</returns>
		public static int ReadInt()
		{
			return int.Parse(ReadLine());
		}
	}
}