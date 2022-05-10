using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST
{
    internal class Program
    {
        public static void Main(string[] args)
        {
			// ПЕРЕМЕННЫЕ INT
			int x1 = 111;
			int x2 = 222;
			// ПЕРЕМЕННЫЕ FLOAT
			float y1 = 333;
			float y2 = 444;
			// ПЕРЕМЕННЫЕ DOUBLE
			double z1 = 555;
			double z2 = 666;
			// ПЕРЕМЕННЫЕ CHAR
			char c1	= 'a';
			char c2 = 'b';
			// ПЕРЕМЕННЫЕ STRING
			string s1 = "abc";
			string s2 = "dfg";
			// Пример 1 - Простой вывод на консоль
			Console.WriteLine("Hello, World!");
			// Пример 2 - INT
			Console.WriteLine("{0} _____ {1} _____ {2}", x1, x2, x1 + x2);
			// Пример 3 - FLOAT
			Console.WriteLine("{0} _____ {1} _____ {2}", y1, y2, y1 + y2);
			// Пример 4 - DOUBLE
			Console.WriteLine("{0} _____ {1} _____ {2}", z1, z2, z1 + z2);
			// Пример 5 - CHAR
			Console.WriteLine("{0} _____ {1} _____ {2}", c1, c2);
			// Пример 6 - STRING
			Console.WriteLine("{0} _____ {1}", s1, s2);
			// Пример 7 - простые примеры
			int sum = 0;
			int mul = 0;
			sum = x1 + x2;
			mul = x1 * y1;
			Console.WriteLine("Сумма: {0}\nПроизведение: {1}", sum, mul);
			// Пример 8 - чтение с консоли
			string newstr;
			Console.Write("Введите строку: ");
			newstr = Console.ReadLine();
			Console.WriteLine("Введенное число: {0}", newstr);
			// Пример 9 - чтение чисел с консоли
			int newnum1;
			Console.Write("Введите целое число: ");
			newnum1 = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Введенное число: {0}", newnum1);
			// Пример 10 - FOR
			int x3 = 0;
			for (int i = 0; i < 10; i++)
			{
				x3 += i;
			}
			Console.WriteLine("10: {0}", x3);
			// Пример 11 - IF
			int x4 = 0;
			if (x4 == 0)
			{
				Console.WriteLine("True");
			}
			else
			{
				Console.WriteLine("False");
			}
			// Пример 12 - While
			int x5 = 0;
			while (x5 != 10)
			{
				x5 += 2;
			}	
			Console.WriteLine("12: {0}", x5);
        }
    }
}