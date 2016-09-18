using System;
using System.Collections.Generic;
using System.Linq;

namespace MlitaPractices.Helpers
{
	/// <summary>
	/// Класс, позволяющий более гибко работать с <see cref="IEnumerable{T}"/>.
	/// </summary>
	public static class EnumerableHelper
	{
		/// <summary>
		/// Отбросить элемены, равные null.
		/// </summary>
		/// 
		/// <typeparam name="TElement">Тип элементов последовательности.</typeparam>
		/// <param name="source">Исходная последовательность.</param>
		/// <returns>Отфильтрованная последовательность, не содержащая null.</returns>
		public static IEnumerable<TElement> NotNull<TElement>(this IEnumerable<TElement> source)
		{
			return source.Where(x => x != null);
		}

		/// <summary>
		/// Выполнить указанное действие над всеми элементами последовательности.
		/// </summary>
		/// <typeparam name="TElement">Тип элементов последовательности.</typeparam>
		/// <param name="source">Исходная последовательность.</param>
		/// <param name="action">Действие, которое необходимо выполнить</param>
		public static void ForEach<TElement>(this IEnumerable<TElement> source, Action<TElement> action)
		{
			foreach (var element in source)
				action(element);
		}
	}
}