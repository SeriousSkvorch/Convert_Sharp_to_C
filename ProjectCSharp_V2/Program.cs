using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectCSharp_V2
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        // ЦЕЛЬ - 10-12 ключевых слов (основные типы данных, if, for, while)
        // ДОСТИГНУТО - 5 типов данных, printf (6), scanf (7), if (8), for (9), while (10)
        // ОСТАЛОСЬ - 0-2 ключевых слов

        string[] data = {"0", "0"};
        int fileflag = 0;
        string name = "";

        #region ПОИСК ФАЙЛА
        string name2 = "Ра";
        if (!Directory.Exists(Environment.CurrentDirectory + @"\for_translate"))
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("Ошибка. Директория отсутствует.");
          Directory.CreateDirectory(Environment.CurrentDirectory + @"\for_translate");
          Console.ResetColor();
          Console.WriteLine("Директория для файла создана.\nПереместите файл для перевода в директорию \"for_translate\", после чего перезапустите приложение.");
          Console.WriteLine("Для продолжения нажмите любую клавишу...");
          Console.ReadKey();
          return;
        }
        else
        {
          string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\for_translate");
          if (files.Length == 0)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка. Файл отсутствует.");
            Console.ResetColor();
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
            return;
          }

          for (int i = 0; i < files.Length; i++)
          {
            if (Path.GetExtension(files[i]) == ".cs")
            {
              data = File.ReadAllLines(files[i]);
              name = Path.GetFileNameWithoutExtension(files[i]) + ".cpp";
              fileflag++;
              name2 += "зр";
              break;
            }
          }
        
          if (fileflag == 0)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка. Файл отсутствует.");
            Console.ResetColor();
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
            return; 
          }
        }
        name2 += "аб";
        #endregion
        
        #region СООБЩЕНИЕ ДЛЯ ПОЛЬЗОВАТЕЛЯ
        name2 += "от";
        Console.WriteLine("Транслятор C# в C");
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ПРОЧТИТЕ ПЕРЕД НАЧАЛОМ ИСПОЛЬЗОВАНИЯ!");
        Console.ResetColor();
        
        Console.WriteLine("Для приложения необходимо:");
        Console.WriteLine("1) Файл с кодом C#, который соответствует возможностям приложения (см. ниже)");
        Console.WriteLine("2) Функция MAIN в коде C#");
        Console.WriteLine("3) Корректно работающий код на C#");
        
        Console.Write("Приложение может:\n");
        Console.Write("1) Переводить вывод на консоль с неограниченным количеством аргументов\n");
        //Console.Write("ПРИМЕЧАНИЕ ДЛЯ п1: в качестве аргументов НЕ РЕКОМЕНДУЕТСЯ использовать примеры в исходном коде (например, x+y).\n");
        //Console.Write("Приложение может переводить ТОЛЬКО примеры с двумя переменными!\n");
        Console.Write("2) Переводимые типы данных:\n");
        Console.Write("- int\n");
        Console.Write("- float\n");
        Console.Write("- double\n");
        Console.Write("- char\n");
        Console.Write("- string\n");
        Console.Write("3) Перевод функций ввода данных с консоли Console.ReadLine()\n");
        Console.Write("4) Перевод Convert при вводе данных с консоли (т.е Convert.To***(Console.ReadLine()))\n");
        Console.Write("5) Перевод условий FOR, IF, WHILE");
        name2 += "ал ";
        Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Для перевода в папке \"for_translate\" необходим ОДИН файл с расширением .cs");
        Console.WriteLine("Если файлов с расширением несколько, будет переведён только ПЕРВЫЙ файл с данным расширением.");
        Console.WriteLine("Для начала перевода нажмите любую клавишу...");
        Console.ReadKey();
        #endregion
        
        #region ДОПОЛНИТЕЛЬНЫЕ ПЕРЕМЕННЫЕ
        // Позиция для начала функции Main
        name2 += "Ск";
        int pos = 0;
        // Позиция для переменных в Console.WriteLine
        int pos2;
        // Флаг для кода по умолчанию
        int flagFor = 0;
        int flagIf = 0;
        name2 += "во";
        int flagWhile = 0;
        int flagTry = 0;
        int flagCatch = 0;
        int flagSwitch = 0;
        #endregion
        
        #region ПЕРЕМЕННЫЕ ДЛЯ ПЕРЕВОДА КОДА
        Dictionary<string, string> vars = new Dictionary<string, string>();
        name2 += "рц";
        string start = "#include \"stdafx.h\"\n#include <locale.h>\n#include <string.h>\n#include <stdlib.h>\n#include <math.h>\n\nint _tmain(int argc, _TCHAR* argv[])\n{\n\tsetlocale(LC_ALL, \"ru\");";
        string code = "";
        string tab = "\n\t";
        string tab2 = "\t";
        int flagtab = 0;
        string end = "\treturn 0;\n}";
        name2 += "ов ";
        #endregion
        
        #region ПОИСК НАЧАЛА КОДА
        name2 += "Ан";
        for (int i = 0; i < data.Length; i++)
        {
          string line = data[i];
          if (line.Contains("class Program"))
          {
            pos = i;
            break;
          }
        }
        name2 += "др";
        #endregion
        
        #region ПОИСК НАЧАЛА ФУНКЦИИ MAIN
        name2 += "ей";
        for (int i = pos; i < data.Length; i++)
        {
          string line = data[i];
          if (line.Contains("static void Main"))
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
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += result;
            continue;
          }
          #endregion
          
          #region РЕГИОНЫ
          if (line.Contains("#region"))
          {
            line = line.Replace("#region", "#pragma region");
            string result = line.Trim();
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += result;
            continue;
          }
          if (line.Contains("#endregion"))
          {
            line = line.Replace("#endregion", "#pragma endregion");
            string result = line.Trim();
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += result;
            continue;
          }
          #endregion
          
          #region TryCatch
          if (line.Contains("try"))
          {
            flagTry = 1;
            continue;
          }

          if (line.Contains("catch"))
          {
            flagCatch = 1;
            continue;
          }          
          
          if (flagCatch != 0)
          {
            if (line.Contains("{"))
            {
              continue;
            }
            if (line.Contains("}"))
            {
              flagCatch = 0;
            }
            continue;
          }
          #endregion

          pos2 = 0;
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
                  string result11 = oneparam.Trim(' ');
                  int index = result11.IndexOf("+", 0);
                  if (index < 0)
                  {
                    index = result11.IndexOf("-", 0);
                  }

                  if (index < 0)
                  {
                    index = result11.IndexOf("*", 0);
                  }

                  if (index < 0)
                  {
                    index = result11.IndexOf("/", 0);
                  }
                  string var1 = result11.Substring(0, index).Trim(' ');

                  if (vars[var1] == "int")
                  {
                    print2[0] = print2[0].Replace("{" + changecount + "}", "%d");
                    changecount++;
                  }

                  if (vars[var1] == "float")
                  {
                    print2[0] = print2[0].Replace("{" + changecount + "}", "%f");
                    changecount++;
                  }

                  if (vars[var1] == "double")
                  {
                    print2[0] = print2[0].Replace("{" + changecount + "}", "%f");
                    changecount++;
                  }
                  
                  if (vars[var1] == "char")
                  {
                    print2[0] = print2[0].Replace("{" + changecount + "}", "%c");
                    changecount++;
                  }
                  
                  if (vars[var1] == "string")
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
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += "printf(" + print2[0] + ");";
            if (line.Contains("Console.WriteLine"))
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += "printf(\"\\n\");";
            }
            continue;
          }
          #endregion 
          
          #region Console.ReadLine
          // ДЛЯ СТРОК
          if (line.Contains("string") && line.Contains("Console.ReadLine"))
          {
            string result = line.Trim();
            string[] variable = result.Split();
            vars.Add(variable[1].Trim(';'), variable[0].Trim(';'));
            string varname = variable[1].Trim(';');
            variable[0] = "char";
            variable[1] = variable[1].Trim(';');
            variable[1] += "[]";
            result = variable[0] + " " + variable[1] + " = \"\"" + tab + "scanf(\"%s\", " + varname + ");";
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += result;
            continue;
          }
          if (line.Contains("Console.ReadLine") && !line.Contains("Convert"))
          {
            // string = Console.ReadLine();
            string[] words = line.Trim().Split();
            code += tab;
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += "scanf(\"%s\", &" + words[0] + ");";
            continue;
          }
          // ДЛЯ ЧИСЕЛ
          if (line.Contains("Console.ReadLine") && line.Contains("Convert"))
          {
            string[] words = line.Trim().Split();
            string[] conv = words[2].Split('.');
            if (conv[1].Contains("ToInt32") || conv[1].Contains("toInt32"))
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += "scanf(\"%d\", &" + words[0] + ");";
            }
            if (conv[1].Contains("ToDouble") || conv[1].Contains("toDouble"))
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += "scanf(\"%f\", &" + words[0] + ");";
            }
            continue;
          }
          #endregion
          
          #region FOR
          if (line.Contains("for ("))
          {
            string len = "";
            flagFor = 1;
            if (line.Contains(".Length"))
            {
              string[] len1 = line.Trim(' ').Split(' ');
              string len2 = len1[7];
              string[] len3 = len2.Split('.');
              len1[7] = "strlen(" + len3[0] + ");";
              for (int x = 0; x < len1.Length; x++)
              {
                len += len1[x] + " ";
              }
            }
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }

            if (len != "")
            {
              code += len;
            }
            else
            {
              code += line;
            }
            flagtab++;
            continue;
          }
          #endregion
          
          #region IF
          if (line.Contains("if ("))
          {
            flagIf = 1;
            if (line.Contains("char.IsLower"))
            {
              line = line.Replace("char.IsLower", "iswlower");
            }
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += line.Trim();
            flagtab++;
            continue;
          }
          #endregion
          
          #region ELSE
          if (line.Contains("else"))
          {
            flagIf = 1;
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += line.Trim();
            flagtab++;
            continue;
          }
          #endregion

          #region WHILE
          if (line.Contains("while ("))
          {
            flagWhile = 1;
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += line.Trim();
            flagtab++;
            continue;
          }
          #endregion

          #region SWITCH_CASE
          if (line.Contains("switch"))
          {
            flagSwitch = 1;
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += line.Trim();
            continue;
          }
          #endregion

          // ТИПЫ ДАННЫХ
          #region INT
          if (line.Contains("int"))
          {
            string result = line.Trim();
            string[] variable = result.Split();
            if (variable.Length > 2)
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += result;
            }
            else
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += variable[0] + " " + variable[1];
            }
            vars.Add(variable[1].Trim(';'), variable[0].Trim(';'));
            continue;
          }
          #endregion
          
          #region FLOAT
          if (line.Contains("float"))
          {
            string result = line.Trim();
            string[] variable = result.Split();
            if (variable.Length > 2)
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += result;
            }
            else
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += variable[0] + " " + variable[1];
            }
            vars.Add(variable[1].Trim(';'), variable[0].Trim(';'));
            continue;
          }
          #endregion
          
          #region DOUBLE
          if (line.Contains("double"))
          {
            string result = line.Trim();
            string[] variable = result.Split();
            if (variable.Length > 2)
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += result;
            }
            else
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += variable[0] + " " + variable[1];
            }
            vars.Add(variable[1].Trim(';'), variable[0].Trim(';'));
            continue;
          }
          #endregion

          #region STRING
          if (line.Contains("string"))
          {
            string result = line.Trim();
            string[] variable = result.Split();
            vars.Add(variable[1].Trim(';'), variable[0].Trim(';'));
            variable[0] = "char";
            variable[1] = variable[1].Trim(';');
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
            if (variable.Length > 2)
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += result;
            }
            else
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += variable[0] + " " + variable[1] + "= \"\";";
            }
            continue;
          }
          #endregion
          
          #region CHAR
          if (line.Contains("char"))
          {
            string result = line.Trim();
            string[] variable = result.Split();
            if (variable.Length > 2)
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += result;
            }
            else
            {
              code += tab;
              for (int l = flagtab; l > 0; l--)
              {
                code += tab2;
              }
              code += variable[0] + " " + variable[1];
            }
            vars.Add(variable[1].Trim(';'), variable[0].Trim(';'));
            continue;
          }
          #endregion

          // DEFAULT
          if (flagFor != 0)
          {
            code += tab;
            if (line.Contains("{"))
            {
              flagtab--;
            }
            if (line.Contains("}"))
            {
              flagFor = 0;
              flagtab--;
            }
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            if (line.Contains("{"))
            {
              flagtab++;
            }
            code += line.Trim();
            continue;
          }

          if (flagIf != 0)
          {
            code += tab;
            if (line.Contains("{"))
            {
              flagtab--;
            }
            if (line.Contains("}"))
            {
              flagIf = 0;
              flagtab--;
            }
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            if (line.Contains("{"))
            {
              flagtab++;
            }
            code += line.Trim();
            continue;
          }
          
          if (flagWhile != 0)
          {
            code += tab;
            if (line.Contains("{"))
            {
              flagtab--;
            }
            if (line.Contains("}"))
            {
              flagWhile = 0;
              flagtab--;
            }
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            if (line.Contains("{"))
            {
              flagtab++;
            }
            code += line.Trim();
            continue;
          }
          
          if (flagSwitch != 0)
          {
            code += tab;
            if (line.Contains("{"))
            {
              flagtab--;
            }
            if (line.Contains("}"))
            {
              flagSwitch = 0;
              flagtab--;
            }
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            if (line.Contains("{"))
            {
              flagtab++;
            }
            code += line.Trim();
            continue;
          }
          
          if (flagTry != 0)
          {
            code += tab;
            if (line.Contains("{"))
            {
              flagtab--;
              continue;
            }
            if (line.Contains("}"))
            {
              flagTry = 0;
              flagtab--;
              continue;
            }
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            if (line.Contains("{"))
            {
              flagtab++;
            }
            code += line.Trim();
            continue;
          }
          
          if (!line.Contains("}") && !line.Contains("{"))
          {
            code += tab;
            for (int l = flagtab; l > 0; l--)
            {
              code += tab2;
            }
            code += line.Trim();
          }
        }

        #region Запись кода в файл .CPP
        if (!Directory.Exists(Environment.CurrentDirectory + @"\translated"))
        {
          Directory.CreateDirectory(Environment.CurrentDirectory + @"\translated");
        }
        name2 += ". 2022 год";
        StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\translated\" + name);
        sw.WriteLine(start);
        sw.WriteLine(tab + code);
        sw.WriteLine(end);
        sw.Close();
        Console.WriteLine("Переведённый файл находится в директории \"translated\".");
        Console.WriteLine("Перевод завершен. Спасибо за использование данного приложения.");
        Console.WriteLine("\n" + name2);
        Console.WriteLine("Для завершения нажмите любую клавишу...");
        Console.ReadKey();
        #endregion
      }
      catch (Exception ex)
      {
        Console.WriteLine("Ошибка: {0}", ex.Message);
        Console.WriteLine("Для завершения нажмите любую клавишу...");
        Console.ReadKey();
      }
    }
  }
}