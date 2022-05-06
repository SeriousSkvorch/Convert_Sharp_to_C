using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectCSharp_V2
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      // ЦЕЛЬ - 10-12 ключевых слов (основные типы данных, if, for, while)
      // ДОСТИГНУТО - 5 типов данных, printf (6)
      // ОСТАЛОСЬ - 6-8 ключевых слов

      #region СООБЩЕНИЕ ДЛЯ ПОЛЬЗОВАТЕЛЯ
      Console.WriteLine("Транслятор C# в C");
      
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("ПРОЧТИТЕ ПЕРЕД НАЧАЛОМ ИСПОЛЬЗОВАНИЯ!");
      Console.ResetColor();
      
      Console.WriteLine("Для приложения необходимо:");
      Console.WriteLine("1) Файл с кодом C#, который соответствует возможностям приложения (см. ниже)");
      Console.WriteLine("2) Функция MAIN в коде C#");
      
      Console.Write("Приложение может:\n");
      Console.Write("1) Переводить вывод на консоль с неограниченным количеством аргументов");
      Console.Write("\nПРИМЕЧАНИЕ ДЛЯ п1: в качестве аргументов НЕ РЕКОМЕНДУЕТСЯ использовать примеры в исходном коде (например, x+y).\n");
      Console.Write("Приложение может переводить ТОЛЬКО примеры с двумя переменными!\n");
      Console.Write("2) Переводимые типы данных:\n");
      Console.Write("- int\n");
      Console.Write("- float\n");
      Console.Write("- double");
      
      Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("Главное меню");
      #endregion
      
      #region ДОПОЛНИТЕЛЬНЫЕ ПЕРЕМЕННЫЕ
      // Позиция для начала функции Main
      int pos = 0;
      // Позиция для переменных в Console.WriteLine
      int pos2 = 0;
      #endregion
      
      #region ПЕРЕМЕННЫЕ ДЛЯ ПЕРЕВОДА КОДА
      Dictionary<string, string> vars = new Dictionary<string, string>();
      string start = "#include \"stdafx.h\"\n#include <locale.h>\n#include <string.h>\n#include <stdlib.h>\n#include <math.h>\n\nint _tmain(int argc, _TCHAR* argv[])\n{";
      string code = "";
      string tab = "\n\t";
      string end = "\treturn 0;\n}";
      string[] data = File.ReadAllLines(@"D:\Program Files (x86)\ProjectVisualStudio\test.cs");
      #endregion
      
      #region ПОИСК НАЧАЛА КОДА
      for (int i = 0; i < data.Length; i++)
      {
        string line = data[i];
        if (line.Contains("internal class Program"))
        {
          pos = i;
          break;
        }
      }
      #endregion
      
      #region ПОИСК НАЧАЛА ФУНКЦИИ MAIN
      for (int i = pos; i < data.Length; i++)
      {
        string line = data[i];
        if (line.Contains("public static void Main"))
        {
          pos = i + 2;
          break;
        }
      }
      #endregion
      
      // Прохождение функции MAIN
      for (int i = pos; i < data.Length; i++)
      {
        string line = data[i]; 
        #region КОММЕНТАРИИ
        if (line.Contains("//") && !line.Contains("Console.WriteLine") && !line.Contains("Console.Write"))
        {
          string result = line.Trim();
          code += tab + result;
          continue;
        }
        #endregion 
        
        #region Console.Write
        if (line.Contains("Console.WriteLine") || line.Contains("Console.Write"))
        {
          string[] print1 = line.Split('(');
          string[] print2 = print1[1].Split(')');
          string param = print2[0];
          if (print2[0].Contains("{0}"))
          {
            for (int j = param.Length - 1; j >= 0; j--)
            {
              if (param[j] == ',' && param[j - 1] == '"' && param.Contains("\","))
              {
                pos2 = j + 1;
                break;
              }
            }
          }
          // ПЕРЕВОД АРГУМЕНТОВ
          if (pos2 != 0)
          {
            string param2 = "";

            for (int j = pos2; j < param.Length; j++)
            {
              param2 += param[j];
            }

            string[] allparams = param2.Split(',');
            int changecount = 0;
            foreach (string oneparam in allparams)
            {
              if (oneparam.Contains("+") || oneparam.Contains("-") || oneparam.Contains("*") || oneparam.Contains("/"))
              {
                string[] mulparam = oneparam.Split(' ');
                string result1 = mulparam[1].Trim(' ');
                if (vars[result1] == "int")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%d");
                  changecount++;
                }

                if (vars[result1] == "float")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%f");
                  changecount++;
                }

                if (vars[result1] == "double")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%f");
                  changecount++;
                }
                
                if (vars[result1] == "char")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%c");
                  changecount++;
                }
                
                if (vars[result1] == "string")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%s");
                  changecount++;
                }
              }
              else
              {
                string result = oneparam.Trim();
                if (vars[result] == "int")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%d");
                  changecount++;
                }

                if (vars[result] == "float")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%f");
                  changecount++;
                }

                if (vars[result] == "double")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%f");
                  changecount++;
                }
                
                if (vars[result] == "char")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%c");
                  changecount++;
                }
                
                if (vars[result] == "string")
                {
                  print2[0] = print2[0].Replace("{" + changecount + "}", "%s");
                  changecount++;
                }
              }
            }
          }
          code += tab + "printf(" + print2[0] + ");";
          if (line.Contains("Console.WriteLine"))
          {
            code += tab + "printf(\"\\n\");";
          }
          continue;
        }
        #endregion 
        
        #region INT
        if (line.Contains("int"))
        {
          string result = line.Trim();
          string[] variable = result.Split();
          code += tab + result;
          for (int j = 0; j < variable.Length; j++)
          {
            if (variable[j] != "")
            {
              vars.Add(variable[j + 1], variable[j]);
              break;
            }
          }
          continue;
        }
        #endregion
        
        #region FLOAT
        if (line.Contains("float"))
        {
          string result = line.Trim();
          string[] variable = result.Split();
          code += tab + result;
          for (int j = 0; j < variable.Length; j++)
          {
            if (variable[j] != "")
            {
              vars.Add(variable[j + 1], variable[j]);
              break;
            }
          }
          continue;
        }
        #endregion
        
        #region DOUBLE
        if (line.Contains("double"))
        {
          string result = line.Trim();
          string[] variable = result.Split();
          code += tab + result;
          for (int j = 0; j < variable.Length; j++)
          {
            if (variable[j] != "")
            {
              vars.Add(variable[j + 1], variable[j]);
              break;
            }
          }
          continue;
        }
        #endregion

        #region STRING
        if (line.Contains("string"))
        {
          string result = line.Trim();
          string[] variable = result.Split();
          for (int j = 0; j < variable.Length; j++)
          {
            if (variable[j] != "")
            {
              vars.Add(variable[j + 1], variable[j]);
              break;
            }
          }
          variable[0] = "char";
          variable[1] += "[]";
          result = "";
          for (int j = 0; j < variable.Length; j++)
          {
            result += variable[j];
            if (j + 1 != variable.Length)
            {
              result += " ";
            }
          }
          code += tab + result;
          continue;
        }
        #endregion
        
        #region CHAR
        if (line.Contains("char"))
        {
          string result = line.Trim();
          string[] variable = result.Split();
          code += tab + result;
          for (int j = 0; j < variable.Length; j++)
          {
            if (variable[j] != "")
            {
              vars.Add(variable[j + 1], variable[j]);
              break;
            }
          }
        }
        #endregion
      }

      #region Запись кода в файл .CPP
      StreamWriter sw = new StreamWriter("test.cpp");
      sw.WriteLine(start);
      sw.WriteLine(tab + code);
      sw.WriteLine(end);
      sw.Close();
      #endregion
    }
  }
}