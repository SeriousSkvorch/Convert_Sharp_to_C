using System;
using System.IO;

namespace ProjectCSharp_V2
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      int pos = 0;
      //ПЕРЕМЕННЫЕ ДЛЯ ПЕРЕВОДА КОДА
      string start = "#include \"stdafx.h\"\n#include <locale.h>\n#include <string.h>\n\nint _tmain(int argc, _TCHAR* argv[])\n{";
      string code = "";
      string tab = "\n\t";
      string end = "\treturn 0;\n}";
      //КОНЕЦ ПЕРЕМЕННЫХ
      string[] data = File.ReadAllLines(@"D:\Program Files (x86)\ProjectVisualStudio\test.cs");
      //ПОИСК НАЧАЛА КОДА
      for (int i = 0; i < data.Length; i++)
      {
        string line = data[i];
        if (line.Contains("internal class Program"))
        {
          pos = i;
          break;
        }
      }
      //ПОИСК НАЧАЛА ФУНКЦИИ MAIN
      for (int i = pos; i < data.Length; i++)
      {
        string line = data[i];
        if (line.Contains("public static void Main"))
        {
          pos = i + 2;
          break;
        }
      }
      //ПРОХОД ПО ФУНКЦИИ MAIN
      for (int i = pos; i < data.Length; i++)
      {
        string line = data[i];
        if (line.Contains("Console.WriteLine"))
        {
          string[] print_1 = line.Split('(');
          string[] print_2 = print_1[1].Split(')');
          code += tab + "printf(" + print_2[0] + ");";
        }
      }
      
      StreamWriter sw = new StreamWriter("test.cpp");
      sw.WriteLine(start);
      sw.WriteLine(tab + code);
      sw.WriteLine(end);
      sw.Close();
    }
  }
}