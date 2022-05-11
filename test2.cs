using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR13
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Ввести целое положительное трехзначное число N (N>0). Проверить истинность высказывания: "Сумма всех цифр введенного числа равна произведению первой и третьей цифры введенного числа".
			//2. Вводится строка. Длина строки может быть разной. Подсчитать количество содержащихся в ней строчных букв латинского алфавита.
			
            #region Первое задание
            {
                int N;
                Console.WriteLine("1. Ввести целое положительное трехзначное число N. Проверить истинность высказывания: 'Сумма всех цифр введенного числа равна произведению первой и третьей цифры введенного числа'.");
            m2:
                Console.Write("\nВведите целое положительное трёхзначное число - ");
                try
                {
                    N = Convert.ToInt32(Console.ReadLine());
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Слишком большое число.");
                    goto m2;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите ЧИСЛО!");
                    goto m2;
                }
                if (N < 1)
                {
                    Console.WriteLine("Введите ПОЛОЖИТЕЛЬНОЕ число.");
                    goto m2;
                }
                if (N / 100 < 1 || N / 100 > 9)
                {
                    Console.WriteLine("Введите ТРЁХЗНАЧНОЕ число.");
                    goto m2;
                }

                int F = N / 100;
                int S = (N % 100) / 10;
                int L = (N % 100) - (S * 10);

                if (F + S + L == F * L)
                {
                    Console.WriteLine("\nВысказывание верное.");
                }
                else
                {
                    Console.WriteLine("\nВысказывание неверное");
                }
            }
            #endregion

            Console.WriteLine("\n~~~~~~~~~~~~\n");

			#region Второе задание
            string str = "";
            int count = 0;
			Console.WriteLine("2. Вводится строка. Длина строки может быть разной. Подсчитать количество содержащихся в ней строчных букв латинского алфавита.");
			Console.Write("Введите строку: ");
            str = Console.ReadLine();
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLower(str[i]))
                {
                    count++;
                }
            }
            Console.Write("Ответ: {0}", count);
			#endregion
        }
    }
}
