using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectCSharp_V2
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      // Позиция для начала функции Main
      int pos = 0;
      // Позиция для переменных в Console.WriteLine
      int pos2 = 0;
      #region ПЕРЕМЕННЫЕ ДЛЯ ПЕРЕВОДА КОДА
      Dictionary<string, string> vars = new Dictionary<string, string>();
      string start = "#include \"stdafx.h\"\n#include <locale.h>\n#include <string.h>\n\nint _tmain(int argc, _TCHAR* argv[])\n{";
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
      
      #region ПРОХОД ПО ФУНКЦИИ MAIN
      for (int i = pos; i < data.Length; i++)
      {
        string line = data[i];
        if (line.Contains("Console.WriteLine") || line.Contains("Console.Write"))
        {
          string[] print_1 = line.Split('(');
          string[] print_2 = print_1[1].Split(')');
          string param = print_2[0];
          if (print_2[0].Contains("{0}"))
          {
            for (int j = param.Length - 1; j >= 0; j--)
            {
              if (param[j] == ',' && param[j - 1] == '"')
              {
                pos2 = j + 1;
                break;
              }
            }
          }

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
              string result2 = mulparam[3].Trim(' ');
              if (vars[result1] == "int" && vars[result2] == "int")
              {
                print_2[0] = print_2[0].Replace("{" + changecount + "}", "%d");
                changecount++;
              }
            }
            else
            {
              string result = oneparam.Trim();
              if (vars[result] == "int")
              {
                print_2[0] = print_2[0].Replace("{" + changecount + "}", "%d");
                changecount++;
              }
            }
          }

          code += tab + "printf(" + print_2[0] + ");";
        }

        if (line.Contains("int"))
        {
          string result = line.Trim();
          code += tab + result;
          string[] variable = line.Split();
          for (int j = 0; j < variable.Length; j++)
          {
            if (variable[j] == "")
            {
              continue;
            }
            else
            {
              vars.Add(variable[j + 1], variable[j]);
              break;
            }
          }
        }
      }
      #endregion
      
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