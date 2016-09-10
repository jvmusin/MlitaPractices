using System;
using System.Linq;

namespace MlitaPractices.Helpers
{
	/// <summary>
	/// Класс, позволяющий более гибко работать с числами.
	/// </summary>
	public static class NumericHelper
	{
		/// <summary>
		/// Получить двоичное представление строки.
		/// </summary>
		/// 
		/// <param name="number">Исходное число.</param>
		/// <returns>Двоичное представление числа.</returns>
		public static string ToBinaryString(this int number)
		{
			if (number < 0)
				throw new ArgumentException($"{nameof(ToBinaryString)} works only with non-negative integers", nameof(number));
			if (number == 0) return "0";

			var binary = "";
			while (number != 0)
			{
				binary = number%2 + binary;
				number /= 2;
			}
			return binary;
		}

		/// <summary>
		/// Пролучить двоичное представление числа, дополненное нулями слева до указанной ширины.
		/// </summary>
		/// 
		/// <param name="number">Исходное число.</param>
		/// <param name="totalWidth">Необходимая ширина строки.</param>
		/// <returns>Двоичное представление числа.</returns>
		public static string ToBinaryString(this int number, int totalWidth)
		{
			return ToBinaryString(number).PadLeft(totalWidth, '0');
		}

		/// <summary>
		/// Перевести число из указанной системы счисления в десятичную систему счисления.
		/// </summary>
		/// 
		/// <param name="s">Исходное число, записанное в указанной системе счисления.</param>
		/// <param name="fromBase">Исходная система счисления.</param>
		/// <returns>Число, переведённое в десятичную систему счисления.</returns>
		public static int ParseInt(string s, int fromBase)
		{
			var result = 0;
			var multiplier = 1;
			foreach (var digit in s.Reverse().Select(c => c - '0'))
			{
				result += digit*multiplier;
				multiplier *= fromBase;
			}
			return result;
		}
	}
}